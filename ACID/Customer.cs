using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACID
{
    public class Customer
    {
        private String PhoneNum;
        private String Adresse;
        private String Name;
        private int Orders;

        public Customer(String Tel, String Addr, String name, int orders)
        {
            PhoneNum = Tel;
            Adresse = Addr;
            Name = name;
            Orders = orders;
        }
        public String GetPhoneNum()
        {
            return PhoneNum;
        }
        public String GetAddr()
        {
            return Adresse;
        }
        public String GetName()
        {
            return Name;
        }
        public void SetPhoneNum(String Tel)
        {
            PhoneNum = Tel;
        }
        public void SetAddr(String Addr)
        {
            Adresse = Addr;
        }
        public void SetName(String name)
        {
            Name = name;
        }
        public void SetOrderCount(int Order_Cnt)
        {
            Orders = Order_Cnt;
        }
        public int GetOrderCount()
        {
            return Orders;
        }
    }
}
