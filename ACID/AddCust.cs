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
        public String Name;
        public String Address;
        public String Phone;
        public bool IsDataEntered;
        public AddCust()
        {
            InitializeComponent();
            Name = Address = Phone = "";
            IsDataEntered = false;
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

        private void Add_Click(object sender, EventArgs e)
        {
            Name = this.Cust_Name.Text;
            Address = this.Adresse.Text;
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
    }
}
