#define DELIVERY

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using MySql.Data.MySqlClient;

namespace ACID
{

    public partial class MenuForm : Form
    {

        public MenuForm(MySql.Data.MySqlClient.MySqlConnection Conn, String User)
        {
            InitializeComponent();
            myConn = Conn;
            this.UserID = User;
        }
        public MenuForm(MySql.Data.MySqlClient.MySqlConnection Conn, Customer Customer, String User)
        {
            InitializeComponent();
            myConn = Conn;
            MyCustomer = Customer;
            this.UserID = User;
        }

        private void SubMenu_1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Add_Item_Click(object sender, EventArgs e)
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

        private void Delete_Item_Click(object sender, EventArgs e)
        {
            if(this.OrderedList.Rows.Count > 0)
            {
                this.OrderedList.Rows[0].Selected = true;
            }
            DeleteItem();
        }

        private void CheckBox_Delivery_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox_TA_CheckedChanged(object sender, EventArgs e)
        {
            if(this.CheckBox_TA.Checked == true)
            {
                this.CheckBox_In.Enabled = false;
            }
            else
            {
                this.CheckBox_In.Enabled = true;
            }
        }

        private void CheckBox_In_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox_In.Checked == true)
            {
                this.CheckBox_TA.Enabled = false;
            }
            else
            {
                this.CheckBox_TA.Enabled = true;
            }
        }

        private void Finish_Order_Click(object sender, EventArgs e)
        {
            String CmdTxt;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader;
            DateTime NewDate = new DateTime();
            int SubOrderNo = 0, OrderID;
            DateTime LastOrderDate = new DateTime();
            String Date;


            CmdTxt = "SELECT current_timestamp()";

            cmd.Connection = myConn;
            cmd.CommandText = CmdTxt;

            try
            {
                myConn.Open();
                reader = cmd.ExecuteReader();
                Cursor.Current = Cursors.WaitCursor;
                while (reader.Read())
                {
                    NewDate = reader.GetDateTime("current_timestamp()");
                }
                myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                myConn.Close();
                return;
            }

#if !DELIVERY
            if((this.CheckBox_In.Checked == false) && (this.CheckBox_TA.Checked == false))
            {
                MessageBox.Show("--اختار نوع الطلب -- صالة أو تيك اواي");
                return;
            }
            else if (this.CheckBox_TA.Checked == true)
            {
                CmdTxt = "SELECT * FROM take_away ORDER BY DateTime DESC LIMIT 1";
                cmd.CommandText = CmdTxt;

                try
                {
                    myConn.Open();
                    reader = cmd.ExecuteReader();
                    Cursor.Current = Cursors.WaitCursor;
                    while (reader.Read())
                    {
                        LastOrderDate = reader.GetDateTime("DateTime");
                        SubOrderNo = reader.GetInt32("OrderSubID");
                    }
                    myConn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    myConn.Close();
                    return;
                }

                NewOrder = new Order(OrderTypes.TakeAway);
            }
            else
            {
                CmdTxt = "SELECT * FROM indoor ORDER BY DateTime DESC LIMIT 1";
                cmd.CommandText = CmdTxt;

                try
                {
                    myConn.Open();
                    reader = cmd.ExecuteReader();
                    Cursor.Current = Cursors.WaitCursor;
                    while (reader.Read())
                    {
                        LastOrderDate = reader.GetDateTime("DateTime");
                        SubOrderNo = reader.GetInt32("OrderSubID");
                    }
                    myConn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    myConn.Close();
                    return;
                }

                NewOrder = new Order(OrderTypes.Indoor);
            }

            Date = (LastOrderDate).Date.ToString();

            if (Date != NewDate.Date.ToString())
            {
                SubOrderNo = 1;
                OrderID = ConverToID(NewDate.Date.ToString(), SubOrderNo);
            }
            else
            {
                SubOrderNo++;
                OrderID = ConverToID(NewDate.Date.ToString(), SubOrderNo);
            }

            NewOrder.Order_SetOrderID(OrderID);
            NewOrder.Order_SetOrderSubID(SubOrderNo);
            NewOrder.Order_SetOrderTotal(GetTotalOrderValue());
            NewOrder.Order_SetTimestmp(NewDate.Date.ToString());
#else

            CmdTxt = "SELECT * FROM delivery_orders ORDER BY DateTime DESC LIMIT 1";
            cmd.CommandText = CmdTxt;
            
            try
            {
                myConn.Open();
                reader = cmd.ExecuteReader();
                Cursor.Current = Cursors.WaitCursor;
                while (reader.Read())
                {
                    LastOrderDate = reader.GetDateTime("DateTime");
                    SubOrderNo = reader.GetInt32("OrderSubID");
                }
                myConn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                myConn.Close();
                return;
            }

            Date = ((LastOrderDate.Date.ToString()).Split(new char[]{' '}))[0];

            if (Date != ((NewDate.Date.ToString()).Split(new char[] { ' ' }))[0])
            {
                SubOrderNo = 1;
                OrderID = ConverToID(((NewDate.Date.ToString()).Split(new char[] { ' ' }))[0], SubOrderNo);
            }
            else
            {
                SubOrderNo++;
                OrderID = ConverToID(((NewDate.Date.ToString()).Split(new char[] { ' ' }))[0], SubOrderNo);
            }

            NewOrder = new Order(OrderTypes.Delivery);
            NewOrder.Order_SetCustAddr(MyCustomer.GetAddr());
            NewOrder.Order_SetCustTel(MyCustomer.GetPhoneNum());
            NewOrder.Order_SetOrderID(OrderID);
            NewOrder.Order_SetOrderSubID(SubOrderNo);
            NewOrder.Order_SetOrderTotal(GetTotalOrderValue() + NewOrder.Order_GetDeliveryCharge());
            NewOrder.Order_SetTimestmp(NewDate.ToString());
#endif
            if (System.DateTime.Now.CompareTo(LastOrderDate) < 0 )
            {
                MessageBox.Show("You Cannot Save Order at Time earlier than the most recent one on DataBase");
                return;
            }
            if(!SaveOrder())
            {
                MessageBox.Show("Error Saving Order on DataBase");
                return;
            }

            PrintDocument Reciept = new PrintDocument();
            Reciept.PrintPage += new PrintPageEventHandler(PrintReciept);
            Reciept.Print();
#if DELIVERY
            this.Dispose();
#endif
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

            graphics.DrawString("مرحبا بكم في مطعم" + "\n" + "    شيخ البلد", new Font("Courier New", 14,FontStyle.Bold),
                                new SolidBrush(Color.Black), startX - 10, startY + Offset, Rformat);
            Offset = Offset + 90;

            graphics.DrawString("Order #:" + NewOrder.Order_GetOrderSubID(),
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX-240, startY + Offset);
            Offset = Offset + 20;
#if DELIVERY
            graphics.DrawString("Tel: " + NewOrder.Order_GetCustTel(),
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX-240, startY + Offset);
            Offset = Offset + 20;


            graphics.DrawString("خدمة توصيل",
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset, Rformat);
            Offset = Offset + 20;

            graphics.DrawString("اسم العميل : " + MyCustomer.GetName(),
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset, Rformat);

            Offset = Offset + 20;

            if (MyCustomer.GetAddr().Length > 40)
            {
                Split = SplitString(MyCustomer.GetAddr());
                graphics.DrawString("العنوان : " + Split[0] + "\n" + Split[1],
                    new Font("Courier New", 6),
                    new SolidBrush(Color.Black), startX, startY + Offset, Rformat);
            }
            else
            {
                graphics.DrawString("العنوان : " +MyCustomer.GetAddr(),
                    new Font("Courier New", 6),
                    new SolidBrush(Color.Black), startX, startY + Offset, Rformat);
            }

            Offset = Offset + 25;
#endif

            graphics.DrawString("Date/Time  " + NewOrder.Order_GetTimeStmp(),
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX-240, startY + Offset);

            Offset = Offset + 40;

            graphics.DrawString("طلب               الكمية     سعر",
                     new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX-5, startY + Offset, Rformat);

            Offset = Offset + 10;

            graphics.DrawString("-------------------------------", new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX-5, startY + Offset, Rformat);
            Offset = Offset + 10;
            for(int i = 0; i < this.dataSet2.Tables[0].Rows.Count; i++)
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
                     new SolidBrush(Color.Black), startX -220, startY + Offset);

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
                     new SolidBrush(Color.Black), startX -220, startY + Offset);

                    Offset = Offset + 15;
                }
            }

            graphics.DrawString("-------------------------------", new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);

            Offset = Offset + 15;
#if DELIVERY
            graphics.DrawString("خدمة التوصيل", new Font("Courier New", 8),
                    new SolidBrush(Color.Black), startX - 5, startY + Offset, Rformat);

            graphics.DrawString(NewOrder.Order_GetDeliveryCharge().ToString(), new Font("Courier New", 8),
                    new SolidBrush(Color.Black), startX - 220, startY + Offset);

            Offset = Offset + 15;
#endif

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
#if DELIVERY

            CmdTxt = @"INSERT INTO delivery_orders(OrderID, OrderSubID, Tel, Addresse, UserID, Delivery_Charge, OrderTotal)
                                                        VALUES(@ID, @SubID, @tel, @Addr, @User, @DeliveryCharge, @Total);";
#else
            if(this.CheckBox_TA.Checked == true)
            {

                CmdTxt = @"INSERT INTO take_away(OrderID, OrderSubID, UserID, OrderTotal)
                                                        VALUES(@ID, @SubID, @User, @Total);";
            }
            else if(this.CheckBox_In.Checked == true)
            {

                CmdTxt = @"INSERT INTO indoor(OrderID, OrderSubID, UserID, OrderTotal, ServiceCharge, TableNo)
                                                        VALUES(@ID, @SubID, @User, @Total, @Service, @Table);";
            }
#endif

            cmd.Connection = this.myConn;
            cmd.CommandText = CmdTxt;

            switch(NewOrder.Order_GetOrderType())
            {
                case OrderTypes.Delivery:

                    /* Fill Command attributes */

                    cmd.Parameters.AddWithValue("@ID", NewOrder.Order_GetOrderID());
                    cmd.Parameters.AddWithValue("@SubID", NewOrder.Order_GetOrderSubID());
                    cmd.Parameters.AddWithValue("@tel", NewOrder.Order_GetCustTel());
                    cmd.Parameters.AddWithValue("@Addr", NewOrder.Order_GetCustAddr());
                    cmd.Parameters.AddWithValue("@User", this.UserID);
                    cmd.Parameters.AddWithValue("@DeliveryCharge", NewOrder.Order_GetDeliveryCharge());
                    cmd.Parameters.AddWithValue("@Total", NewOrder.Order_GetOrderTotal());

                    break;
                case OrderTypes.TakeAway:

                    /* Fill Command attributes */

                    cmd.Parameters.AddWithValue("@ID", NewOrder.Order_GetOrderID());
                    cmd.Parameters.AddWithValue("@SubID", NewOrder.Order_GetOrderSubID());
                    cmd.Parameters.AddWithValue("@User", this.UserID);
                    cmd.Parameters.AddWithValue("@Total", NewOrder.Order_GetOrderTotal());

                    break;
                case OrderTypes.Indoor:

                    /* Fill Command attributes */

                    cmd.Parameters.AddWithValue("@ID", NewOrder.Order_GetOrderID());
                    cmd.Parameters.AddWithValue("@SubID", NewOrder.Order_GetOrderSubID());
                    cmd.Parameters.AddWithValue("@User", this.UserID);
                    cmd.Parameters.AddWithValue("@Service", 0);
                    cmd.Parameters.AddWithValue("@Table", 0);
                    cmd.Parameters.AddWithValue("@Total", NewOrder.Order_GetOrderTotal());

                    break;
                default:
                    break;
            }

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
        
        private void ListView1_ItemSelectionChanged(Object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
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

        private void OrderedList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DeleteItem();
        }
        private String[] SplitString(String s)
        {
            char[] chars = s.ToCharArray();
            int mid = (s.Length / 2) - 1;
            int splitPoint = 0;
            int TempSlpitPoint = 0;
            String[] SplitString = {"",""};
            int RemainningLength = 0;

            if(chars[mid] == ' ')
            {
                splitPoint = mid;
            }
            else
            {
                for(int i= mid; i < s.Length; i++)
                {
                    if(chars[i] == ' ')
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
                if((mid - splitPoint) > (TempSlpitPoint - mid))
                {
                    splitPoint = TempSlpitPoint;
                }
                else
                {
                    /*Do Nothing*/
                }
            }

            for(int k = (splitPoint+1); k < s.Length; k++)
            {
                RemainningLength++;
            }
            SplitString[0] = s.Substring(0, (splitPoint+1));
            SplitString[1] = s.Substring((splitPoint + 1), RemainningLength);

            return SplitString;
        }
        private int ConverToID(String date, int increment)
        {
            String[] Split = date.Split(new char[] { '/' });
            int year,month,day, ID;
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
            for(int i = 0; i < this.dataSet2.Tables[0].Rows.Count; i++)
            {
                Sum = Sum + ((Convert.ToInt32(this.dataSet2.Tables[0].Rows[i].ItemArray[0])) * (Convert.ToDouble(this.dataSet2.Tables[0].Rows[i].ItemArray[3])));
            }

            return Sum;
        }

    }
}
