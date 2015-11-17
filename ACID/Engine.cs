#define DELIVERY


using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using System.Threading;
using  System.IO.Ports;

namespace ACID
{
    public class Engine : Form1
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;
        private String myConnectionString;
        private MySql.Data.MySqlClient.MySqlConnection conn2;
        private Authentication AuthForm;
        private AddCust AddCustForm;
        public Button[] CatButtons;
        private MenuForm MenuWindow;
        private CustListForm CustomerForm;
        private Customer CustTobeSearched;
        private String CurrUserID;
        private String Password;
        private String ServerName;
        private OrderCancelForm CancelForm;
        private SettingsForm SettingsForm;
        private string CommPort;
        private string baudRate;
        private string initString;
        private SerialPort MySerialComm;
        delegate void SetTextCallback(string text);
        /*Start Menu */
        XmlDocument xmlFile;
        ProgressBar PBar;
        Thread ProgressTherad;
        private MySqlDataAdapter Adapter;
        public Engine(String UserID, String Pass): base(UserID, Pass)
        {
            Match mymatch;
            ServerName = "";
            xmlFile = new XmlDocument();
            Cursor.Current = Cursors.WaitCursor;
            CurrUserID = UserID;
            Password = Pass;
            PBar = new ProgressBar();
            CommPort = "";
            baudRate = "";
            initString = "";
            this.LEDBox.Hide();
            ProgressTherad = new Thread(PBar.Start);
            try
            {
                /*Read MySql IP address from local file*/
                String text = System.IO.File.ReadAllText(@"Server.txt");
                mymatch = Regex.Match(text, @"Server\s*\=\s*([0-9\.]+)");
                if(mymatch.Groups.Count < 2)
                {
                    ServerName = "localhost";
                    this.Server_Name.Text = ServerName;
                }
                else
                {
                    ServerName = mymatch.Groups[1].Value;
                    this.Server_Name.Text = "Remote";
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "  ....  using localhost instead");
                ServerName = "localhost";
                this.Server_Name.Text = ServerName;
            }
            /*Start Database Connection*/

            myConnectionString = "server=" + ServerName + ";uid=" + UserID + ";" +
            "pwd=" + Pass + ";" + "database=customers;";


            try
            {
                /*Load Log In Window*/
                ProgressTherad.Start();
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn2 = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                myConnectionString = "server=" + ServerName + ";uid=" + CurrUserID + ";" +
                "pwd=" + Password + ";" + "database=orders;";
                conn2.ConnectionString = myConnectionString;
                conn2.Open();

            }
            catch (Exception ex)
            {
                conn.Close();
                conn2.Close();
                MessageBox.Show(ex.Message);
                PBar.Auth.Invoke(PBar.myDelegate);
                ProgressTherad.Abort();
                System.Environment.Exit(0);
            }


            /*Get Menu Data*/
            try
            {
                xmlFile.Load(@"https://storage-download.googleapis.com/menudata/MenuItems_Final.xml");
                xmlFile.Save(@"./Backup/MenuItems.xml");
                PBar.Auth.Invoke(PBar.myDelegate);
                ProgressTherad.Abort();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                try
                {
                    xmlFile.Load(@"./Backup/MenuItems.xml");
                    PBar.Auth.Invoke(PBar.myDelegate);
                    ProgressTherad.Abort();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    PBar.Auth.Invoke(PBar.myDelegate);
                    ProgressTherad.Abort();
                    System.Environment.Exit(0);
                }
            }
            try
            {
                /*Read Comm settings from local file*/
                String[] CommSettings = System.IO.File.ReadAllLines(@"./modem.txt");

                CommPort = CommSettings[0];
                baudRate = CommSettings[1];
                initString = CommSettings[2];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                SettingsForm = new SettingsForm();
                SettingsForm.ShowDialog();
                CommPort = SettingsForm.PortName ;
                try
                {
                    baudRate = Convert.ToString(SettingsForm.baudRate);
                }
                catch(Exception ee)
                {
                    baudRate = "152200";
                    MessageBox.Show(ee.Message);
                }
                initString = SettingsForm.InitString;
            }
            Cursor.Current = Cursors.Default;

            MySerialComm = new SerialPort(CommPort, Convert.ToInt32(baudRate));
            MySerialComm.DataReceived += new SerialDataReceivedEventHandler(DataRecievedOnPort);

            try
            {
                MySerialComm.Open();
            }
            catch(Exception CommEx)
            {
                MessageBox.Show(CommEx.Message);
            }
            if(initString == "")
            {
                initString = "AT";
            }
            try
            {
                MySerialComm.WriteLine(initString + Environment.NewLine);
            }
            catch (Exception Commex)
            {
                MessageBox.Show(Commex.Message);
            }
        }


        private void StartLogForm()
        {
            AuthForm = new Authentication();
            AuthForm.Visible = true;
        }
        protected override void NewCust_Click(object sender, EventArgs e)
        {
            Customer CustTobeAdded = new Customer("","","",0,0);
            bool IsAddSuccess = false;
            AddCustForm = new AddCust(this.CustNum.Text);
            AddCustForm.ShowDialog();
            if (AddCustForm.IsDataEntered == true)
            {
                /* Get Entered Onfo */
                CustTobeAdded.SetName(AddCustForm.Name);
                CustTobeAdded.SetAddr(AddCustForm.Address);
                CustTobeAdded.SetPhoneNum(AddCustForm.Phone);

                Cursor.Current = Cursors.WaitCursor;
                /* Register Customer */
                IsAddSuccess = AddCustomer(CustTobeAdded);
                Cursor.Current = Cursors.Default;
            }
            else
            {
                /*Do Nothing*/
            }
        }
        protected override void CustNum_TextChanged(object sender, EventArgs e)
        {
            if (this.CustNum.Text.Trim().Length > 0)
            {
                this.Search.Enabled = true;
            }
            else
            {
                this.Search.Enabled = false;
            }
        }
        protected override void Search_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null ;
            CustTobeSearched = new Customer("","","",0,0);

            if (!(System.Text.RegularExpressions.Regex.IsMatch(this.CustNum.Text, "^\\d+$")))
            {
                MessageBox.Show("الرقم غير صحيح");
            }
            else
            {
                String CmdTxt = @"SELECT * FROM cust_info WHERE Phone = '" + this.CustNum.Text + "'";
                this.Cust_Name.Text = "";
                this.Adresse.Text = "";
                this.PhoneNum.Text = "";
                cmd.Connection = conn;
                cmd.CommandText = CmdTxt;
                try
                {
                    reader = cmd.ExecuteReader();
                    Cursor.Current = Cursors.WaitCursor;
                    while (reader.Read())
                    {
                        CustTobeSearched.SetName(reader.GetString("Name"));
                        CustTobeSearched.SetAddr(reader.GetString("Address"));
                        CustTobeSearched.SetOrderCount(reader.GetInt32("No_of_Orders"));
                        CustTobeSearched.SetPhoneNum(reader.GetString("Phone"));
                        CustTobeSearched.SetDeliveryCharge(reader.GetDouble("Delivery_Charge"));
                    }
                    this.Cust_Name.Text = CustTobeSearched.GetName();
                    this.Adresse.Text = CustTobeSearched.GetAddr();
                    this.PhoneNum.Text = CustTobeSearched.GetPhoneNum();
                    Cursor.Current = Cursors.Default;
                    this.Order.Enabled = true;

                    if (CustTobeSearched.GetPhoneNum() == "")
                    {
                        MessageBox.Show("هذا الرقم غير مسجل");
                        this.Order.Enabled = false;
                    }
                    reader.Close();
                }
                catch (MySqlException ex)
                {
                    //reader.Close();
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("هذه العملية لم تتم لعدم وجود اتصال بالسيرفر");
                }
            }
        }
        private bool AddCustomer (Customer Cust)
        {
            MySqlCommand cmd = new MySqlCommand();
            String CmdTxt = @"INSERT INTO cust_info(Phone,Address,Name,No_of_Orders)VALUES(@Phone_Num,@Addr,@CustName,@Orders);";
            bool IsOk = true;

            /* Fill Command attributes */
            cmd.Connection = conn;
            cmd.CommandText = CmdTxt;
            cmd.Parameters.AddWithValue("@Phone_num", Cust.GetPhoneNum());
            cmd.Parameters.AddWithValue("@Addr", Cust.GetAddr());
            cmd.Parameters.AddWithValue("@CustName", Cust.GetName());
            cmd.Parameters.AddWithValue("@Orders", Cust.GetOrderCount());

            try
            {
                /* Execute Command */
                cmd.ExecuteNonQuery();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message + "\n" + "هذه العملية لم تتم لعدم وجود اتصال بالسيرفر أو هذا الرقم مسجل بالفعل ....\n ملحوظة: لا يمكن تسجيل أكثر من عميل برقم تليفون واحد");
                IsOk = false;
            }

            return IsOk;
        }
        protected override void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                /* Close Connection */
                conn.Close();
                conn2.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                MySerialComm.Close();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        protected override void Order_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            uint Orders = 0;
            String CmdTxt = @"SELECT No_of_Orders FROM cust_info WHERE Phone='" + this.CustNum.Text +"'" ;
            cmd.CommandText = CmdTxt;
            cmd.Connection = conn;
            Cursor.Current = Cursors.WaitCursor;
            DataTable[] ListOfTables;
            XmlNodeList elemList;
            XmlElement root;
            int TotalMenuItems;

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Orders = reader.GetUInt32("No_of_Orders");
                }
                reader.Close();
            }
            catch(MySqlException ex)
            {
               // reader.Close();
                MessageBox.Show(ex.Message);
                MessageBox.Show("هذه العملية لم تتم لعدم وجود اتصال بالسيرفر");
                return;
            }
            Orders++;
            CmdTxt = @"UPDATE cust_info SET No_of_Orders='" + Orders + "'" + "WHERE Phone='" + this.CustNum.Text + "'";
            cmd.CommandText = CmdTxt;
            try
            {
                cmd.ExecuteNonQuery();
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("هذه العملية لم تتم لعدم وجود اتصال بالسيرفر");
                return;
            }
            Cursor.Current = Cursors.Default;

            this.Cust_Name.Text = "";
            this.Adresse.Text = "";
            this.PhoneNum.Text = "";
            this.CustNum.Text = "";

            this.Order.Enabled = false;

            ArrayList DataTables = new ArrayList();
            XmlNodeList CatList = null;

            if(xmlFile != null)
            {
                root = xmlFile.DocumentElement;
                try
                {
                    elemList = root.GetElementsByTagName("MenuItem");
                    CatList = root.GetElementsByTagName("Category");
                }
                catch(Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                    return;
                }
                TotalMenuItems = elemList.Count;
                String Temp = "";
                ArrayList CatArrayList = new ArrayList();
                for (int i = 0; i < TotalMenuItems; i++)
                {
                    Temp = CatList.Item(i).InnerText;
                    if(CatArrayList.Contains(Temp))
                    {
                        /*Do Nothing*/
                    }
                    else
                    {
                        CatArrayList.Add(Temp);
                        DataTables.Add(new DataTable(Temp));
                    }
                }
            }
            else
            {
                MessageBox.Show("No MenuWindow XML file to Import From!");
                return;
            }

            MenuWindow = new MenuForm(conn2, CustTobeSearched, CurrUserID, this.Server_Name.Text);
            ListOfTables = new DataTable[DataTables.Count];
            CatButtons = new Button[DataTables.Count];

            if (DataTables.Count > 0)
            {
                for (int Index = 0; Index < DataTables.Count; Index++ )
                {
                   ListOfTables[Index] = (DataTable)(DataTables[Index]);
                   CatButtons[Index] = new Button();
                   CatButtons[Index].Text = ListOfTables[Index].TableName;
                   CatButtons[Index].Click += new EventHandler(Cat_btn_Click);
                   CatButtons[Index].Width = 120;
                }
                MenuWindow.Cat_Panel.Controls.AddRange(CatButtons);
                MenuWindow.dataSet1.Tables.AddRange(ListOfTables);
            }

            for (int tbls = 0; tbls < MenuWindow.dataSet1.Tables.Count; tbls++)
            {
                //Add Columns
                MenuWindow.dataSet1.Tables[tbls].Columns.Add("الاسم", typeof(String));
                MenuWindow.dataSet1.Tables[tbls].Columns.Add("الحجم", typeof(String));
                MenuWindow.dataSet1.Tables[tbls].Columns.Add("السعر", typeof(String));
                MenuWindow.dataSet1.Tables[tbls].Columns.Add("الصنف", typeof(String));
                MenuWindow.dataSet1.Tables[tbls].Columns.Add("كود", typeof(String));
                MenuWindow.dataSet1.Tables[tbls].Columns.Add("القسم", typeof(String));
            }
            String ItemName = "";
            double ItemPrice = 0;
            String ItemSize = "";
            String CatName = "";
            String ItemCode = "";
            String Department = "";
            int CatIndex = 0;

            for(int items = 0; items < TotalMenuItems; items++)
            {
                ItemName = elemList.Item(items).ChildNodes.Item(0).InnerText;
                ItemSize = elemList.Item(items).ChildNodes.Item(2).InnerText;
                ItemPrice = Convert.ToDouble(elemList.Item(items).ChildNodes.Item(3).InnerText);
                ItemCode = elemList.Item(items).ChildNodes.Item(4).InnerText;
                CatName = elemList.Item(items).ChildNodes.Item(1).InnerText;
                CatIndex = MenuWindow.dataSet1.Tables.IndexOf(CatName);
                Department = elemList.Item(items).ChildNodes.Item(5).InnerText;
                // add row to corresponding table
                MenuWindow.dataSet1.Tables[CatIndex].Rows.Add(new String[] { ItemName, ItemSize, ItemPrice.ToString(), CatName, ItemCode, Department });
            }

            MenuWindow.dataGridView1.DataSource = MenuWindow.dataSet1.Tables[0];

            MenuWindow.dataGridView1.Columns[0].Width = 400;
            MenuWindow.dataGridView1.Columns[1].Width = 250;
            MenuWindow.dataGridView1.Columns[2].Width = 220;

            MenuWindow.dataSet2.Tables.Add(new DataTable("Orders"));

            MenuWindow.dataSet2.Tables[0].Columns.Add("الكمية", typeof(String));
            MenuWindow.dataSet2.Tables[0].Columns.Add("الاسم", typeof(String));
            MenuWindow.dataSet2.Tables[0].Columns.Add("الحجم", typeof(String));
            MenuWindow.dataSet2.Tables[0].Columns.Add("السعر", typeof(String));

            MenuWindow.dataSet2.Tables.Add(new DataTable("Codes"));

            MenuWindow.dataSet2.Tables[1].Columns.Add("Code", typeof(String));
            MenuWindow.dataSet2.Tables[1].Columns.Add("Category", typeof(String));
            MenuWindow.dataSet2.Tables[1].Columns.Add("القسم", typeof(String));

            MenuWindow.OrderedList.DataSource = MenuWindow.dataSet2.Tables[0];

            MenuWindow.OrderedList.Columns[1].ReadOnly = true;
            MenuWindow.OrderedList.Columns[2].ReadOnly = true;
            MenuWindow.OrderedList.Columns[3].ReadOnly = true;

            MenuWindow.OrderedList.Columns[0].Width = 50;
            MenuWindow.OrderedList.Columns[1].Width = 500;
            MenuWindow.OrderedList.Columns[2].Width = 250;
            MenuWindow.OrderedList.Columns[2].Width = 230;

            MenuWindow.ShowDialog();
        }
        protected override void CustNum_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Search_Click(sender, (EventArgs)e);
            }
        }
        private void Cat_btn_Click(Object sender, System.EventArgs e)
        {
            String CatName = "";
            int TblIndex = 0;
            CatName = ((Button)sender).Text;
            TblIndex = MenuWindow.dataSet1.Tables.IndexOf(CatName);
            MenuWindow.CurrentTblindex = TblIndex;
            MenuWindow.dataGridView1.DataSource = MenuWindow.dataSet1.Tables[TblIndex];
        }
        protected override void EditBtn_Click(object sender, EventArgs e)
        {
            DataSet CustomerDataSet;
            String CmdTxt;
            MySqlCommand cmd = new MySqlCommand();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Adapter = new MySqlDataAdapter("SELECT * FROM customers.cust_info;", this.conn);
            }
            catch(Exception exep)
            {
                MessageBox.Show(exep.Message);
                MessageBox.Show("هذه العملية لم تتم لعدم وجود اتصال بالسيرفر");
                Cursor.Current = Cursors.Default;
                return;
            }

            CustomerDataSet = new DataSet();

            try
            {
                Adapter.Fill(CustomerDataSet);
            } 
            catch(Exception e2)
            {
                MessageBox.Show(e2.Message);
                Cursor.Current = Cursors.Default;
                return;
            }

            if(this.PhoneNum.Text.Trim().Length > 0)
            {
                this.CustomerForm = new CustListForm(CustomerDataSet, this.PhoneNum.Text.Trim());
            }
            else
            {
                this.CustomerForm = new CustListForm(CustomerDataSet);
            }

            this.CustomerForm.ShowDialog();

            if(this.CustomerForm.bIsSaveNeeded == true)
            {
                CmdTxt = @"UPDATE cust_info SET Delivery_Charge= @Delivery, Address= @Addr, Name= @name WHERE Phone= @PhoneNum;";
          
                cmd.CommandText = CmdTxt;
                cmd.Connection = this.conn;

                cmd.Parameters.AddWithValue("@Delivery", this.CustomerForm.CustomerToEdit.GetDeliveryCharge());
                cmd.Parameters.AddWithValue("@Addr", this.CustomerForm.CustomerToEdit.GetAddr());
                cmd.Parameters.AddWithValue("@name", this.CustomerForm.CustomerToEdit.GetName());
                cmd.Parameters.AddWithValue("@PhoneNum", this.CustomerForm.CustomerToEdit.GetPhoneNum());
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("هذه العملية لم تتم لعدم وجود اتصال بالسيرفر");
                    Cursor.Current = Cursors.Default;
                }

                // Clear Fields (To Reload Delivery charge if changed in the contents of the Customer to be sent to MenuForm)

                this.Cust_Name.Text = "";
                this.Adresse.Text = "";
                this.PhoneNum.Text = "";
                this.CustNum.Text = "";

                this.Order.Enabled = false;
            }
            else
            {
                /*Do Nothing*/
            }
            Cursor.Current = Cursors.Default;
        }
        protected override void Btn_Order_Cancel_Click(object sender, EventArgs e)
        {
            String CmdTxt;
            String OrderID;
            MySqlDataReader reader = null;
            bool IsOrderExist = false;
            MySqlCommand cmd = new MySqlCommand();

            this.CancelForm = new OrderCancelForm();
            this.CancelForm.ShowDialog();
            if(this.CancelForm.bIsCancelAccepted == true)
            {

                OrderID = this.CancelForm.OrderID;

                CmdTxt = @"SELECT * FROM delivery_orders WHERE OrderID =" + OrderID + ";";

                cmd.CommandText = CmdTxt;
                cmd.Connection = this.conn2;
                try
                {
                    reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        IsOrderExist = true;
                    }
                    reader.Close();
                }
                catch (MySqlException exp2)
                {
                    MessageBox.Show(exp2.Message);
                    MessageBox.Show("هذه العملية لم تتم لعدم وجود اتصال بالسيرفر");
                    return;
                }

                CmdTxt = @"UPDATE delivery_orders SET OrderCancel= b'1', OrderTotal= '0' WHERE OrderID ="+ OrderID+";";

                cmd.CommandText = CmdTxt;

                try
                {
                    cmd.ExecuteNonQuery();

                    CmdTxt = @"DELETE FROM ordered_items WHERE OrderID =" + OrderID + ";";

                    cmd.CommandText = CmdTxt;

                    cmd.ExecuteNonQuery();

                    if (IsOrderExist == true)
                    {
                        MessageBox.Show("تم الغاء الطلب رقم : " + "  " + OrderID + "  " + "بنجاح");
                    }
                    else
                    {
                        MessageBox.Show("الطلب رقم : " + "  " + OrderID + "  " + "غير موجود في قاعدة البيانات");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("هذه العملية لم تتم لعدم وجود اتصال بالسيرفر");
                }

            }
        }
        protected override void modemSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm = new SettingsForm();
            SettingsForm.ShowDialog();
        }
        public void DataRecievedOnPort(Object sender, SerialDataReceivedEventArgs e)
        {
            if(!this.IsDisposed)
            {
                SetText(MySerialComm.ReadLine().ToString());
            }
        }
        private void SetText(string text)
        {
            string mtch;
            string mtch2;
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if ((this.CustNum.InvokeRequired) || (this.LEDBox.InvokeRequired))
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                Match mymatch;
                Match mymatch2;
                object sender = new object();
                EventArgs e = new EventArgs();
                mymatch = Regex.Match(text, @"NMBR\s*\=\s*([0-9\.]+)");
                mymatch2 = Regex.Match(text, @"^\s*OK[\\r\\n]*\s*");
                mtch2 = mymatch2.ToString();
                if (mymatch.Groups.Count >= 2)
                {
                    mtch = mymatch.ToString();
                    this.CustNum.Text = mtch.Substring(7);
                    Search_Click(sender, (EventArgs)e);
                }
                else if (mtch2 != "")
                {
                    mtch2 = mtch2.Substring(0, 2);
                    if(mtch2 == "OK")
                    {
                        this.LEDBox.Visible = true;
                    }
                }
            }
        }
        protected override void TestModemBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(!MySerialComm.IsOpen)
                {
                    MySerialComm.Open();
                    MySerialComm.WriteLine(initString + Environment.NewLine);
                }
                else
                {
                    MySerialComm.WriteLine(initString + Environment.NewLine);
                }
            }
            catch (Exception Commex)
            {
                MessageBox.Show(Commex.Message);
                this.LEDBox.Hide();
            }
        }
    }
}
