#define TAKE_AWAY


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
using System.Drawing.Printing;

namespace ACID
{
    public class Engine : MenuForm
    {
        private String myConnectionString;
        public Button[] CatButtons;
        private String Server_IP;
        /*Start Menu */
        XmlDocument xmlFile;
        ProgressBar PBar;
        Thread ProgressTherad;
        private Order NewOrder;
        private String UserName;

        public Engine(String UserID, String Pass): base(UserID, Pass)
        {
            Match mymatch;
            Server_IP = "";
            xmlFile = new XmlDocument();
            Cursor.Current = Cursors.WaitCursor;
            UserName = UserID;
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
                    Server_IP = "localhost";
                    this.Server_Name.Text = Server_IP;
                }
                else
                {
                    Server_IP = mymatch.Groups[1].Value;
                    this.Server_Name.Text = "Remote";
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "  ....  using localhost instead");
                Server_IP = "localhost";
                this.Server_Name.Text = Server_IP;
            }

            this.CurrUserID.Text = UserName;

            /*Start Database Connection*/

            myConnectionString = "server=" + Server_IP + ";uid=" + UserName + ";" +
            "pwd=" + Password + ";" + "database=orders;";


            try
            {
                /*Load Log In Window*/
                ProgressTherad.Start();
                myConn = new MySql.Data.MySqlClient.MySqlConnection();
                myConn.ConnectionString = myConnectionString;
                myConn.Open();

                myConn.Close();
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
                this.Close();
            }
            Cursor.Current = Cursors.Default;
            /*Start App*/
            Start_Form();
        }

        protected override void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                /* Close Connection */
                myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void Add_Item_Click(object sender, EventArgs e)
        {
            AddItem();
        }
        private DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                clonedRow.Cells[index].Value = row.Cells[index].Value;
            }
            return clonedRow;
        }

        protected override void Delete_Item_Click(object sender, EventArgs e)
        {
            if (this.OrderedList.Rows.Count > 0)
            {
                this.OrderedList.Rows[0].Selected = true;
            }
            DeleteItem();
        }


        protected override void Finish_Order_Click(object sender, EventArgs e)
        {
            String CmdTxt;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            DateTime NewDate = new DateTime();
            int SubOrderNo = 0, OrderID;
            DateTime LastOrderDate = new DateTime();
            String Date;

            String ChangeTimeZone = "SET @@session.time_zone='+02:00';";

            if (this.Server_Name.Text == "Remote")
            {
                CmdTxt = ChangeTimeZone + "\n"+ "SELECT current_timestamp()";
            }
            else
            {
                CmdTxt = "SELECT current_timestamp()";
            }

            cmd.Connection = this.myConn;
            cmd.CommandText = CmdTxt;

            try
            {
                this.myConn.Open();
                reader = cmd.ExecuteReader();
                Cursor.Current = Cursors.WaitCursor;
                while (reader.Read())
                {
                    NewDate = reader.GetDateTime("current_timestamp()");
                }
                this.myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                this.myConn.Close();
                return;
            }

            CmdTxt = "SELECT * FROM order_count;";
            cmd.CommandText = CmdTxt;

            try
            {
                myConn.Open();
                reader = cmd.ExecuteReader();
                Cursor.Current = Cursors.WaitCursor;
                while (reader.Read())
                {
                    SubOrderNo = reader.GetInt32("OrderCount");
                }
                myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                myConn.Close();
                return;
            }

            CmdTxt = "SELECT * FROM order_count";
            cmd.CommandText = CmdTxt;

            try
            {
                this.myConn.Open();
                reader = cmd.ExecuteReader();
                Cursor.Current = Cursors.WaitCursor;
                while (reader.Read())
                {
                    LastOrderDate = reader.GetDateTime("DateTime");
                }
                this.myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                this.myConn.Close();
                return;
            }

            NewOrder = new Order(OrderTypes.TakeAway);

            SubOrderNo++;

            Date = ((LastOrderDate.Date.ToString()).Split(new char[] { ' ' }))[0];

            if (Date != ((NewDate.Date.ToString()).Split(new char[] { ' ' }))[0])
            {
                /*Reset Sub ID*/
                SubOrderNo = 1;
                OrderID = ConverToID(((NewDate.Date.ToString()).Split(new char[] { ' ' }))[0], SubOrderNo);
            }
            else
            {
                OrderID = ConverToID(((NewDate.Date.ToString()).Split(new char[] { ' ' }))[0], SubOrderNo);
            }

            NewOrder.Order_SetOrderID(OrderID);
            NewOrder.Order_SetOrderSubID(SubOrderNo);
            NewOrder.Order_SetOrderTotal(GetTotalOrderValue());
            NewOrder.Order_SetTimestmp(NewDate.ToString());

            if (System.DateTime.Now.CompareTo(LastOrderDate) < 0)
            {
                MessageBox.Show("You Cannot Save Order at Time earlier than the most recent one on DataBase");
                return;
            }
            /*Update Daily Order Count*/

            if (this.Server_Name.Text == "Remote")
            {
                CmdTxt = ChangeTimeZone + "\n" + "UPDATE order_count SET OrderCount =" + "'" + SubOrderNo + "'" + " WHERE Row =0" + ";";
            }
            else
            {
                CmdTxt = "UPDATE order_count SET OrderCount =" + "'" + SubOrderNo + "'" + " WHERE Row =0" + ";";
            }

            cmd.CommandText = CmdTxt;

            try
            {
                /* Open Command Connection */
                this.myConn.Open();
                /* Execute Command */
                cmd.ExecuteNonQuery();
                /* Close Connection */
                this.myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            /*Update Daily Order Count*/
            CmdTxt = "UPDATE order_count SET OrderCount =" + "'" + SubOrderNo + "'" + " WHERE Row =0" + ";";
            cmd.CommandText = CmdTxt;

            try
            {
                /* Open Command Connection */
                myConn.Open();
                /* Execute Command */
                cmd.ExecuteNonQuery();
                /* Close Connection */
                myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            /*Save Order*/
            if (!SaveOrder())
            {
                MessageBox.Show("Error Saving Order on DataBase");
                return;
            }

            PrintDocument Reciept = new PrintDocument();
            Reciept.PrintPage += new PrintPageEventHandler(PrintReciept);
            Reciept.Print();

            /* Reset Orders*/
            this.dataSet2.Tables[0].Clear();

        }
        private void PrintReciept(Object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);

            float fontHeight = font.GetHeight();

            int startX = 250;
            int startY = 0;
            int Offset = 20;
            String ItemName = "";
            String ItemSize = "";
            String ItemPrice = "";
            String ItemQuantity = "";
            int stringCollLenth = 0;
            String Concat = "";
            String[] Split = { "", "" };

            StringFormat Rformat = new StringFormat(StringFormatFlags.DirectionRightToLeft);

            graphics.DrawString("Serial: " + NewOrder.Order_GetOrderID(), new Font("Courier New", 8),
                    new SolidBrush(Color.Black), startX - 160, startY + Offset);

            Offset = Offset + 20;

            graphics.DrawString("مرحبا بكم في مطعم" + "\n" + "    شيخ البلد", new Font("Courier New", 14, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX - 10, startY + Offset, Rformat);
            Offset = Offset + 90;

            graphics.DrawString("Order #:" + NewOrder.Order_GetOrderSubID(),
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 240, startY + Offset);
            Offset = Offset + 20;


            graphics.DrawString("Date/Time  " + NewOrder.Order_GetTimeStmp(),
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 240, startY + Offset);

            Offset = Offset + 40;

            graphics.DrawString("طلب               الكمية     سعر",
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);

            Offset = Offset + 10;

            graphics.DrawString("-------------------------------", new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);
            Offset = Offset + 10;
            for (int i = 0; i < this.dataSet2.Tables[0].Rows.Count; i++)
            {
                ItemName = this.dataSet2.Tables[0].Rows[i].ItemArray[1].ToString();
                ItemPrice = this.dataSet2.Tables[0].Rows[i].ItemArray[3].ToString();
                ItemSize = this.dataSet2.Tables[0].Rows[i].ItemArray[2].ToString();
                ItemQuantity = this.dataSet2.Tables[0].Rows[i].ItemArray[0].ToString();

                Concat = ItemName + " - " + ItemSize;

                stringCollLenth = Concat.Length;

                if (stringCollLenth > 14)
                {
                    Split = SplitString(Concat);
                    graphics.DrawString(Split[0], new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);

                    graphics.DrawString(ItemPrice + "   |   " + ItemQuantity, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 220, startY + Offset);

                    Offset = Offset + 10;

                    graphics.DrawString(Split[1], new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);
                    Offset = Offset + 15;
                }
                else
                {
                    graphics.DrawString(Concat, new Font("Courier New", 8),
                    new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);

                    graphics.DrawString(ItemPrice + "    |    " + ItemQuantity, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 220, startY + Offset);

                    Offset = Offset + 15;
                }
            }

            graphics.DrawString("-------------------------------", new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);

            Offset = Offset + 15;

            graphics.DrawString("الإجمالي", new Font("Courier New", 8, FontStyle.Bold),
                    new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);

            graphics.DrawString(NewOrder.Order_GetOrderTotal().ToString(), new Font("Courier New", 8, FontStyle.Bold),
                    new SolidBrush(Color.Black), startX - 220, startY + Offset);

            Offset = Offset + 40;

            graphics.DrawString("شكرا لزيارتكم مطعم" + "\n" + "    شيخ البلد", new Font("Courier New", 10, FontStyle.Bold),
                    new SolidBrush(Color.Black), startX - 40, startY + Offset, Rformat);

            Offset = Offset + 45;

            graphics.DrawString("خدمة التوصيل", new Font("Courier New", 10),
                    new SolidBrush(Color.Black), startX - 60, startY + Offset, Rformat);

            Offset = Offset + 30;

            graphics.DrawString("16748", new Font("Courier New", 20, FontStyle.Bold),
                    new SolidBrush(Color.Black), startX - 155, startY + Offset);

        }

        private bool SaveOrder()
        {
            String CmdTxt = "";
            MySqlCommand cmd = new MySqlCommand();
            bool IsOk = true;

            CmdTxt = @"INSERT INTO take_away(OrderID, OrderSubID, UserID, OrderTotal)
                                                    VALUES(@ID, @SubID, @User, @Total);";

            cmd.Connection = this.myConn;
            cmd.CommandText = CmdTxt;

            /* Fill Command attributes */

            cmd.Parameters.AddWithValue("@ID", NewOrder.Order_GetOrderID());
            cmd.Parameters.AddWithValue("@SubID", NewOrder.Order_GetOrderSubID());
            cmd.Parameters.AddWithValue("@User", this.UserID);
            cmd.Parameters.AddWithValue("@Total", NewOrder.Order_GetOrderTotal());

            try
            {
                /* Open Command Connection */
                myConn.Open();
                /* Execute Command */
                cmd.ExecuteNonQuery();
                /* Close Connection */
                myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                IsOk = false;
            }

            return IsOk;
        }


        protected override void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AddItem();
        }
        private void AddItem()
        {
            String ItemName;
            String ItemPrice;
            String ItemSize;
            String count;
            int ItemCount = 1;

            ItemName = this.dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString();
            ItemPrice = this.dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[2].Value.ToString();
            ItemSize = this.dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[1].Value.ToString();

            DataRow[] Names = this.dataSet2.Tables[0].Select("الاسم= '" + ItemName + "' and " + "الحجم= '" + ItemSize + "'");

            if (Names.Length > 0)
            {
                count = (String)((this.dataSet2.Tables[0].Select("الاسم= '" + ItemName + "' and " + "الحجم= '" + ItemSize + "'"))[0].ItemArray[0]);
                ItemCount = Convert.ToInt32(count);
                ItemCount++;
                this.dataSet2.Tables[0].Rows[this.dataSet2.Tables[0].Rows.IndexOf(this.dataSet2.Tables[0].Select("الاسم= '" + ItemName + "' and " + "الحجم= '" + ItemSize + "'")[0])].SetField(0, ItemCount.ToString());
            }
            else
            {
                this.dataSet2.Tables[0].Rows.Add(ItemCount.ToString(), ItemName, ItemSize, ItemPrice);
            }

        }
        private void DeleteItem()
        {
            try
            {
                this.OrderedList.Rows.Remove(this.OrderedList.SelectedRows[0]);
            }
            catch (Exception exp)
            {
                /*Do Nothing*/
            }
        }

        protected override void OrderedList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DeleteItem();
        }
        private String[] SplitString(String s)
        {
            char[] chars = s.ToCharArray();
            int mid = (s.Length / 2) - 1;
            int splitPoint = 0;
            int TempSlpitPoint = 0;
            String[] SplitString = { "", "" };
            int RemainningLength = 0;

            if (chars[mid] == ' ')
            {
                splitPoint = mid;
            }
            else
            {
                for (int i = mid; i < s.Length; i++)
                {
                    if (chars[i] == ' ')
                    {
                        TempSlpitPoint = i;
                        break;
                    }
                }
                for (int j = mid; j >= 0; j--)
                {
                    if (chars[j] == ' ')
                    {
                        splitPoint = j;
                        break;
                    }
                }
                if ((mid - splitPoint) > (TempSlpitPoint - mid))
                {
                    splitPoint = TempSlpitPoint;
                }
                else
                {
                    /*Do Nothing*/
                }
            }

            for (int k = (splitPoint + 1); k < s.Length; k++)
            {
                RemainningLength++;
            }
            SplitString[0] = s.Substring(0, (splitPoint + 1));
            SplitString[1] = s.Substring((splitPoint + 1), RemainningLength);

            return SplitString;
        }
        private int ConverToID(String date, int increment)
        {
            String[] Split = date.Split(new char[] { '/' });
            int year, month, day, ID;
            String inc;
            String retID;

            year = (Convert.ToInt32(Split[2])) - 2000;
            month = Convert.ToInt32(Split[1]);
            day = Convert.ToInt32(Split[0]);

            inc = increment.ToString();

            retID = year.ToString() + month.ToString() + day.ToString() + inc.ToString();

            ID = Convert.ToInt32(retID);

            return ID;
        }

        private double GetTotalOrderValue()
        {
            double Sum = 0;
            for (int i = 0; i < this.dataSet2.Tables[0].Rows.Count; i++)
            {
                Sum = Sum + ((Convert.ToInt32(this.dataSet2.Tables[0].Rows[i].ItemArray[0])) * (Convert.ToDouble(this.dataSet2.Tables[0].Rows[i].ItemArray[3])));
            }

            return Sum;
        }


        private void Cat_btn_Click(Object sender, System.EventArgs e)
        {
            String CatName = "";
            int TblIndex = 0;
            CatName = ((Button)sender).Text;
            TblIndex = this.dataSet1.Tables.IndexOf(CatName);
            this.dataGridView1.DataSource = this.dataSet1.Tables[TblIndex];
        }

        private void Start_Form()
        {
            DataTable[] ListOfTables;
            XmlNodeList elemList;
            XmlElement root;
            int TotalMenuItems;

            ArrayList DataTables = new ArrayList();
            XmlNodeList CatList = null;

            if (xmlFile != null)
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
                    if (CatArrayList.Contains(Temp))
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

            ListOfTables = new DataTable[DataTables.Count];
            CatButtons = new Button[DataTables.Count];

            if (DataTables.Count > 0)
            {
                for (int Index = 0; Index < DataTables.Count; Index++)
                {
                    ListOfTables[Index] = (DataTable)(DataTables[Index]);
                    CatButtons[Index] = new Button();
                    CatButtons[Index].Text = ListOfTables[Index].TableName;
                    CatButtons[Index].Click += new EventHandler(Cat_btn_Click);
                    CatButtons[Index].Width = 120;
                }
                this.Cat_Panel.Controls.AddRange(CatButtons);
                this.dataSet1.Tables.AddRange(ListOfTables);
            }

            for (int tbls = 0; tbls < this.dataSet1.Tables.Count; tbls++)
            {
                //Add Columns
                this.dataSet1.Tables[tbls].Columns.Add("الاسم", typeof(String));
                this.dataSet1.Tables[tbls].Columns.Add("الحجم", typeof(String));
                this.dataSet1.Tables[tbls].Columns.Add("السعر", typeof(String));
            }
            String ItemName = "";
            double ItemPrice = 0;
            String ItemSize = "";
            String CatName = "";
            int CatIndex = 0;

            for (int items = 0; items < TotalMenuItems; items++)
            {
                ItemName = elemList.Item(items).ChildNodes.Item(0).InnerText;
                ItemSize = elemList.Item(items).ChildNodes.Item(2).InnerText;
                ItemPrice = Convert.ToDouble(elemList.Item(items).ChildNodes.Item(3).InnerText);
                CatName = elemList.Item(items).ChildNodes.Item(1).InnerText;
                CatIndex = this.dataSet1.Tables.IndexOf(CatName);
                // add row to corresponding table
                this.dataSet1.Tables[CatIndex].Rows.Add(new String[] { ItemName, ItemSize, ItemPrice.ToString() });
            }

            this.dataGridView1.DataSource = this.dataSet1.Tables[0];

            this.dataGridView1.Columns[0].Width = 400;
            this.dataGridView1.Columns[1].Width = 250;
            this.dataGridView1.Columns[2].Width = 220;

            this.dataSet2.Tables.Add(new DataTable("Orders"));

            this.dataSet2.Tables[0].Columns.Add("الكمية", typeof(String));
            this.dataSet2.Tables[0].Columns.Add("الاسم", typeof(String));
            this.dataSet2.Tables[0].Columns.Add("الحجم", typeof(String));
            this.dataSet2.Tables[0].Columns.Add("السعر", typeof(String));

            //Menu.dataSet2.Tables[0].PrimaryKey = new DataColumn[]{Menu.dataSet2.Tables[0].Columns[1]};

            this.OrderedList.DataSource = this.dataSet2.Tables[0];

            this.OrderedList.Columns[1].ReadOnly = true;
            this.OrderedList.Columns[2].ReadOnly = true;
            this.OrderedList.Columns[3].ReadOnly = true;

            this.OrderedList.Columns[0].Width = 50;
            this.OrderedList.Columns[1].Width = 500;
            this.OrderedList.Columns[2].Width = 250;
            this.OrderedList.Columns[2].Width = 230;

            //this.ShowDialog();
        }
    }
}
