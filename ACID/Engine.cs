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

namespace ACID
{
    public class Engine : Form1
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;
        private String myConnectionString;

        private Authentication AuthForm;
        private AddCust AddCustForm;
        public Button[] CatButtons;
        private MenuForm Menu;
        private Customer CustTobeSearched;
        private String CurrUserID;
        private String Password;
        private String Server;
        /*Start Menu */
        XmlDocument xmlFile;
        ProgressBar PBar;
        Thread ProgressTherad;
        public Engine(String UserID, String Pass): base(UserID, Pass)
        {
            Match mymatch;
            Server = "";
            xmlFile = new XmlDocument();
            Cursor.Current = Cursors.WaitCursor;
            CurrUserID = UserID;
            Password = Pass;
            PBar = new ProgressBar();
            ProgressTherad = new Thread(PBar.Start);
            try
            {
                /*Read MySql IP address from local file*/
                String text = System.IO.File.ReadAllText(@"Server.txt");
                mymatch = Regex.Match(text, @"Server\s*\=\s*([0-9\.]+)");
                if(mymatch.Groups.Count < 2)
                {
                    Server = "localhost";
                    this.Server_Name.Text = Server;
                }
                else
                {
                    Server = mymatch.Groups[1].Value;
                    this.Server_Name.Text = "Remote";
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "  ....  using localhost instead");
                Server = "localhost";
                this.Server_Name.Text = Server;
            }
            /*Start Database Connection*/

            myConnectionString = "server=" + Server + ";uid=" + UserID + ";" +
            "pwd=" + Pass + ";" + "database=customers;";


            try
            {
                /*Load Log In Window*/
                ProgressTherad.Start();
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                conn.Close();
                ProgressTherad.Abort();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                System.Environment.Exit(0);
            }


            /*Get Menu Data*/
            try
            {
                xmlFile.Load(@"http://storage.googleapis.com/menudata/MenuItems.xml");

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            Cursor.Current = Cursors.Default;
        }


        private void StartLogForm()
        {
            AuthForm = new Authentication();
            AuthForm.Visible = true;
        }
        protected override void NewCust_Click(object sender, EventArgs e)
        {
            Customer CustTobeAdded = new Customer("","","",0);
            bool IsAddSuccess = false;
            AddCustForm = new AddCust();
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
            MySqlDataReader reader;
            CustTobeSearched = new Customer("","","",0);

            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            myConnectionString = "server=" + Server + ";uid=" + CurrUserID + ";" +
                                "pwd=" + Password + ";" + "database=customers;";
            conn.ConnectionString = myConnectionString;

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
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    Cursor.Current = Cursors.WaitCursor;
                    while (reader.Read())
                    {
                        CustTobeSearched.SetName(reader.GetString("Name"));
                        CustTobeSearched.SetAddr(reader.GetString("Address"));
                        CustTobeSearched.SetOrderCount(reader.GetInt32("No_of_Orders"));
                        CustTobeSearched.SetPhoneNum(reader.GetString("Phone"));
                    }
                    this.Cust_Name.Text = CustTobeSearched.GetName();
                    this.Adresse.Text = CustTobeSearched.GetAddr();
                    this.PhoneNum.Text = CustTobeSearched.GetPhoneNum();
                    Cursor.Current = Cursors.Default;
                    this.Order.Enabled = true;
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (CustTobeSearched.GetPhoneNum() == "")
                {
                    MessageBox.Show("هذا الرقم غير مسجل");
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
                /* Open Command Connection */
                conn.Open();
                /* Execute Command */
                cmd.ExecuteNonQuery();
                /* Close Connection */
                conn.Close();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
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
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected override void Order_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
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
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Orders = reader.GetUInt32("No_of_Orders");
                }
                conn.Close();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Orders++;
            CmdTxt = @"UPDATE cust_info SET No_of_Orders='" + Orders + "'" + "WHERE Phone='" + this.CustNum.Text + "'";
            cmd.CommandText = CmdTxt;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                //MessageBox.Show("تم تسجيل طلب بنجاح");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
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
                elemList = root.GetElementsByTagName("MenuItem");
                CatList = root.GetElementsByTagName("Category");
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
                MessageBox.Show("No Menu XML file to Import From!");
                return;
            }

            myConnectionString = "server=" + Server + ";uid=" + CurrUserID + ";" +
                        "pwd=" + Password + ";" + "database=orders;";
            conn.ConnectionString = myConnectionString;

            Menu = new MenuForm(conn, CustTobeSearched, CurrUserID, this.Server_Name.Text);
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
                Menu.Cat_Panel.Controls.AddRange(CatButtons);
                Menu.dataSet1.Tables.AddRange(ListOfTables);
            }

            for (int tbls = 0; tbls < Menu.dataSet1.Tables.Count; tbls++)
            {
                //Add Columns
                Menu.dataSet1.Tables[tbls].Columns.Add("الاسم", typeof(String));
                Menu.dataSet1.Tables[tbls].Columns.Add("الحجم", typeof(String));
                Menu.dataSet1.Tables[tbls].Columns.Add("السعر", typeof(String));
                Menu.dataSet1.Tables[tbls].Columns.Add("الصنف", typeof(String));
                Menu.dataSet1.Tables[tbls].Columns.Add("كود", typeof(String));
            }
            String ItemName = "";
            double ItemPrice = 0;
            String ItemSize = "";
            String CatName = "";
            String ItemCode = "";
            int CatIndex = 0;

            for(int items = 0; items < TotalMenuItems; items++)
            {
                ItemName = elemList.Item(items).ChildNodes.Item(0).InnerText;
                ItemSize = elemList.Item(items).ChildNodes.Item(2).InnerText;
                ItemPrice = Convert.ToDouble(elemList.Item(items).ChildNodes.Item(3).InnerText);
                ItemCode = elemList.Item(items).ChildNodes.Item(4).InnerText;
                CatName = elemList.Item(items).ChildNodes.Item(1).InnerText;
                CatIndex = Menu.dataSet1.Tables.IndexOf(CatName);
                // add row to corresponding table
                Menu.dataSet1.Tables[CatIndex].Rows.Add(new String[] { ItemName, ItemSize, ItemPrice.ToString(),CatName, ItemCode });
            }

            Menu.dataGridView1.DataSource = Menu.dataSet1.Tables[0];

            Menu.dataGridView1.Columns[0].Width = 400;
            Menu.dataGridView1.Columns[1].Width = 250;
            Menu.dataGridView1.Columns[2].Width = 220;

            Menu.dataSet2.Tables.Add(new DataTable("Orders"));

            Menu.dataSet2.Tables[0].Columns.Add("الكمية", typeof(String));
            Menu.dataSet2.Tables[0].Columns.Add("الاسم", typeof(String));
            Menu.dataSet2.Tables[0].Columns.Add("الحجم", typeof(String));
            Menu.dataSet2.Tables[0].Columns.Add("السعر", typeof(String));

            Menu.dataSet2.Tables.Add(new DataTable("Codes"));

            Menu.dataSet2.Tables[1].Columns.Add("Code", typeof(String));
            Menu.dataSet2.Tables[1].Columns.Add("Category", typeof(String));

            Menu.OrderedList.DataSource = Menu.dataSet2.Tables[0];

            Menu.OrderedList.Columns[1].ReadOnly = true;
            Menu.OrderedList.Columns[2].ReadOnly = true;
            Menu.OrderedList.Columns[3].ReadOnly = true;

            Menu.OrderedList.Columns[0].Width = 50;
            Menu.OrderedList.Columns[1].Width = 500;
            Menu.OrderedList.Columns[2].Width = 250;
            Menu.OrderedList.Columns[2].Width = 230;

            Menu.ShowDialog();
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
            TblIndex = Menu.dataSet1.Tables.IndexOf(CatName);
            Menu.CurrentTblindex = TblIndex;
            Menu.dataGridView1.DataSource = Menu.dataSet1.Tables[TblIndex];
        }
    }
}
