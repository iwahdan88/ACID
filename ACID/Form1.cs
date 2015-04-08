using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ACID
{
    public partial class Form1 : Form
    {
        private String CurrentUser;
        private String Password;
        public Form1(String UserID, String Passwrd)
        {
            CurrentUser = UserID;
            Password = Passwrd;
            InitializeComponent();
            this.UserName.Text = CurrentUser;
            this.Validate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        protected virtual void NewCust_Click(object sender, EventArgs e){}

        protected virtual void CustNum_TextChanged(object sender, EventArgs e) { }

        protected virtual void Search_Click(object sender, EventArgs e) { }


        protected virtual void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        protected virtual void Order_Click(object sender, EventArgs e)
        {

        }

        protected virtual void CustNum_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}
