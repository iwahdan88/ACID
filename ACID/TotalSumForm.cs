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
    public partial class TotalSumForm : Form
    {
        public TotalSumForm( double Sum)
        {
            this.Sum = Sum;
            InitializeComponent();
            this.Total.Text = this.Sum.ToString() + " L.E";
            this.bIsAbbort = true;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            this.bIsAbbort = false;
            this.Dispose();
        }
    }
}
