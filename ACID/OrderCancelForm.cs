using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACID
{
    public partial class OrderCancelForm : Form
    {
        public OrderCancelForm()
        {
            InitializeComponent();
            bIsCancelAccepted = false;
            OrderID = "";
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if(this.PasswordField.Text == OrderCancelForm.Password)
            {
                bIsCancelAccepted = true;
                OrderID = this.OrderNumField.Text;
                this.Dispose();
            }
            else
            {
                MessageBox.Show("الرقم السري غير صحيح");
                this.PasswordField.Clear();
            }
        }
    }
}
