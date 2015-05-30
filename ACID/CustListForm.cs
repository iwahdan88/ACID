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
    public partial class CustListForm : Form
    {
        public CustListForm( DataSet CustData)
        {
            InitializeComponent();
            this.CustListView.DataSource = CustData.Tables[0];
            this.CustListView.Columns[0].Width = 80;
            this.CustListView.Columns[1].Width = 350;
            this.CustListView.Columns[3].Width = 50;
            this.CustListView.Columns[4].Width = 93;

            this.bIsSaveNeeded = false;
            
            CustomerToEdit = new Customer("", "", "", 0,0);
        }
        public CustListForm(DataSet CustData, String Phone)
        {
            int RowIndex;

            InitializeComponent();
            this.CustListView.DataSource = CustData.Tables[0];
            this.CustListView.Columns[0].Width = 80;
            this.CustListView.Columns[1].Width = 350;
            this.CustListView.Columns[3].Width = 50;
            this.CustListView.Columns[4].Width = 93;

            this.textBox1.Text = Phone;

            this.bIsSaveNeeded = false;

            for (RowIndex = 0; RowIndex < this.CustListView.RowCount; RowIndex++)
            {
                if (this.CustListView.Rows[RowIndex].Cells[0].Value.ToString() == Phone)
                {
                    break;
                }
            }
            if (RowIndex == this.CustListView.RowCount)
            {
                MessageBox.Show("هذا الرقم غير موجود");
            }
            else
            {
                this.CustListView.CurrentCell = this.CustListView.Rows[RowIndex].Cells[0];
            }

            CustomerToEdit = new Customer("", "", "", 0, 0);
        }

        private void CustListView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            double DeliverChrg;

            if ((this.CustListView.Rows[CustListView.SelectedRows[0].Index].Cells[4].Value.ToString() == null) ||
                (!(System.Text.RegularExpressions.Regex.IsMatch(this.CustListView.Rows[CustListView.SelectedRows[0].Index].Cells[4].Value.ToString(), "^[\\d\\.]+$"))))
            {
                DeliverChrg = 0;
            }
            else
            {
                DeliverChrg = Convert.ToDouble(this.CustListView.Rows[CustListView.SelectedRows[0].Index].Cells[4].Value.ToString());
            }

            this.EditCustForm = new AddCust(this.CustListView.Rows[CustListView.SelectedRows[0].Index].Cells[2].Value.ToString(),
                this.CustListView.Rows[CustListView.SelectedRows[0].Index].Cells[1].Value.ToString(),
                this.CustListView.Rows[CustListView.SelectedRows[0].Index].Cells[0].Value.ToString(),
                DeliverChrg);

            this.EditCustForm.ShowDialog();
            if (EditCustForm.IsDataEntered == true)
            {
                /* Get Entered Onfo */
                CustomerToEdit.SetName(EditCustForm.Name);
                CustomerToEdit.SetAddr(EditCustForm.Address);
                CustomerToEdit.SetPhoneNum(EditCustForm.Phone);
                CustomerToEdit.SetDeliveryCharge(EditCustForm.DeliverCharge);

                this.bIsSaveNeeded = true;

                this.Dispose();

            }
            else
            {
                /*Do Nothing*/
            }
        }

        private void SearchCustBtn_Click(object sender, EventArgs e)
        {
            int RowIndex;

            if (!(System.Text.RegularExpressions.Regex.IsMatch(this.textBox1.Text, "^\\d+$")))
            {
                MessageBox.Show("ادخل رقم هاتف صحيح");
            }
            else
            {
                for (RowIndex = 0; RowIndex < this.CustListView.RowCount; RowIndex++)
                {
                    if(this.CustListView.Rows[RowIndex].Cells[0].Value.ToString() == this.textBox1.Text.Trim())
                    {
                        break;
                    }
                }

                if( RowIndex == this.CustListView.RowCount)
                {
                    MessageBox.Show("هذا الرقم غير موجود");
                    return;
                }
                else
                {
                    this.CustListView.CurrentCell = this.CustListView.Rows[RowIndex].Cells[0];
                }
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchCustBtn_Click(sender, e);
            }
        }
    }
}
