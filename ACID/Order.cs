using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ACID
{
    enum OrderTypes {Delivery=0, TakeAway, Indoor};
    class Order
    {
        private OrderTypes OrderType;
        private String Cust_Tel;
        private double OrderTotal;
        private String TimeStamp;
        private String Cust_Adrress;
        private int OrderID;
        private int OrderSubID;
        private const double Delivery_Charge = 0.0;
        private int table_no;

        public Order(OrderTypes typ)
        {
            OrderType = typ;
            Cust_Tel = null;
            OrderTotal = 0;
            TimeStamp = "";
            Cust_Adrress = null;
            OrderID = 0;
            OrderSubID = 0;
            table_no = 0;
        }
        public bool Order_SetCustTel(String Tel)
        {
            if(this.OrderType == OrderTypes.Delivery)
            {
                Cust_Tel = Tel;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Order_SetOrderTotal(double Total)
        {
            this.OrderTotal = Total;
        }
        public void Order_SetTimestmp(String stamp)
        {
            this.TimeStamp = stamp;
        }
        public bool Order_SetCustAddr(String Addr)
        {
            if (this.OrderType == OrderTypes.Delivery)
            {
                Cust_Adrress = Addr;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Order_SetOrderID(int ID)
        {
            this.OrderID = ID;
        }
        public void Order_SetOrderSubID(int SubID)
        {
            this.OrderSubID = SubID;
        }
        public bool Order_SetTableNo(int Table)
        {
            if(this.OrderType == OrderTypes.Indoor)
            {
                this.table_no = Table;
                return true;
            }
            else
            {
                return false;
            }
        }
        public String Order_GetCustTel()
        {
            return this.Cust_Tel;
        }
        public double Order_GetOrderTotal()
        {
            return this.OrderTotal;
        }
        public double Order_GetDeliveryCharge()
        {
            return Delivery_Charge;
        }
        public String Order_GetTimeStmp()
        {
            return this.TimeStamp;
        }
        public String Order_GetCustAddr()
        {
            return this.Cust_Adrress;
        }
        public int Order_GetOrderID()
        {
            return this.OrderID;
        }
        public int Order_GetOrderSubID()
        {
            return this.OrderSubID;
        }
        public int Order_GetTableNo()
        {
            return this.table_no;
        }
        public OrderTypes Order_GetOrderType()
        {
            return this.OrderType;
        }
    }
}
