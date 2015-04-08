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
    public partial class Login : Form
    {
        private String userid;
        private String Pass;
        private bool Authentication;

        public Login()
        {
            Authentication = false;
            InitializeComponent();
        }
        public String GetUserID()
        {
            return userid;
        }
        public String GetPass()
        {
            return Pass;
        }
        public bool IsLoginValid()
        {
            return Authentication;
        }
        private void UserID_Click(object sender, EventArgs e)
        {

        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            userid = this.User_TextBox.Text;
            Pass = this.Pass_textBox.Text;
            if (userid == "")
            {
                MessageBox.Show("Enter User ID");
            }
            else if (Pass == "")
            {
                MessageBox.Show("Enter Password");
            }
            else
            {
                Authentication = true;
                this.Close();
            }
        }

        private void Pass_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LogInButton_Click(sender, (EventArgs)e);
            }
        }
    }
}
