using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACID
{
    public partial class AddCust : Form
    {
        public String CustomerName;
        public String Address;
        public String Phone;
        public double DeliverCharge;
        public bool IsDataEntered;
        public AddCust()
        {
            InitializeComponent();
            CustomerName = Address = Phone = "";
            DeliverCharge = 0;
        }
        public AddCust(String Name, String Addresse, String Phone, double DeliveryCharge)
        {
            InitializeComponent();
            this.CustomerName = Name;
            this.Address = Addresse;
            this.Phone = Phone;
            this.DeliverCharge = DeliveryCharge;

            this.Cust_Name.Text = Name;
            this.Adresse.Text = Addresse;
            this.PhoneNum.Text = Phone;
            this.textBox1.Text = DeliveryCharge.ToString();
        }
        public AddCust(String PhoneNum)
        {
            InitializeComponent();
            this.Phone = PhoneNum;

            this.PhoneNum.Text = PhoneNum;
        }
        private void Cust_Name_TextChanged(object sender, EventArgs e)
        {
            if ((this.Cust_Name.Text.Trim().Length > 0) && (this.Adresse.Text.Trim().Length > 0) && (this.PhoneNum.Text.Trim().Length > 0))
            {
                this.Add.Enabled = true;
            }
            else
            {
                this.Add.Enabled = false;
            }
        }

        private void Adresse_TextChanged(object sender, EventArgs e)
        {
            if ((this.Cust_Name.Text.Trim().Length > 0) && (this.Adresse.Text.Trim().Length > 0) && (this.PhoneNum.Text.Trim().Length > 0))
            {
                this.Add.Enabled = true;
            }
            else
            {
                this.Add.Enabled = false;
            }
        }

        private void PhoneNum_TextChanged(object sender, EventArgs e)
        {
            if ((this.Cust_Name.Text.Trim().Length > 0) && (this.Adresse.Text.Trim().Length > 0) && (this.PhoneNum.Text.Trim().Length > 0))
            {
                this.Add.Enabled = true;
            }
            else
            {
                this.Add.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((this.Cust_Name.Text.Trim().Length > 0) && (this.Adresse.Text.Trim().Length > 0) && (this.PhoneNum.Text.Trim().Length > 0))
            {
                this.Add.Enabled = true;
            }
            else
            {
                this.Add.Enabled = false;
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            CustomerName = this.Cust_Name.Text;
            Address = this.Adresse.Text;
            if (this.textBox1.Text.Trim().Length == 0)
            {
                this.DeliverCharge = 0;
            }
            else
            {
                if(!(System.Text.RegularExpressions.Regex.IsMatch(this.textBox1.Text.Trim(), "^[\\d\\.]+$")))
                {
                    this.DeliverCharge = 0;
                }
                else
                {
                    this.DeliverCharge = Convert.ToDouble(this.textBox1.Text.Trim());
                }
            }
            if (!(System.Text.RegularExpressions.Regex.IsMatch(this.PhoneNum.Text, "^\\d+$")))
            {
                MessageBox.Show("رقم الهاتف غير صحيح");
            }
            else
            {
                Phone = this.PhoneNum.Text;
                IsDataEntered = true;
                this.Close();
            }
        }

        private void AddCust_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
