namespace ACID
{
    partial class CustListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CustListLabel = new System.Windows.Forms.Label();
            this.SearchCustBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CustListView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.CustListView)).BeginInit();
            this.SuspendLayout();
            // 
            // CustListLabel
            // 
            this.CustListLabel.AutoSize = true;
            this.CustListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.CustListLabel.Location = new System.Drawing.Point(344, 43);
            this.CustListLabel.Name = "CustListLabel";
            this.CustListLabel.Size = new System.Drawing.Size(98, 25);
            this.CustListLabel.TabIndex = 0;
            this.CustListLabel.Text = "قائمة العملاء";
            // 
            // SearchCustBtn
            // 
            this.SearchCustBtn.Location = new System.Drawing.Point(193, 104);
            this.SearchCustBtn.Name = "SearchCustBtn";
            this.SearchCustBtn.Size = new System.Drawing.Size(97, 25);
            this.SearchCustBtn.TabIndex = 1;
            this.SearchCustBtn.Text = "ابحث برقم الهاتف";
            this.SearchCustBtn.UseVisualStyleBackColor = true;
            this.SearchCustBtn.Click += new System.EventHandler(this.SearchCustBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(165, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // CustListView
            // 
            this.CustListView.AllowUserToAddRows = false;
            this.CustListView.AllowUserToDeleteRows = false;
            this.CustListView.AllowUserToResizeColumns = false;
            this.CustListView.AllowUserToResizeRows = false;
            this.CustListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustListView.Location = new System.Drawing.Point(22, 157);
            this.CustListView.MultiSelect = false;
            this.CustListView.Name = "CustListView";
            this.CustListView.ReadOnly = true;
            this.CustListView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CustListView.Size = new System.Drawing.Size(734, 304);
            this.CustListView.TabIndex = 3;
            this.CustListView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CustListView_RowHeaderMouseDoubleClick);
            // 
            // CustListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 511);
            this.Controls.Add(this.CustListView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SearchCustBtn);
            this.Controls.Add(this.CustListLabel);
            this.MaximizeBox = false;
            this.Name = "CustListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customer List";
            ((System.ComponentModel.ISupportInitialize)(this.CustListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CustListLabel;
        private System.Windows.Forms.Button SearchCustBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView CustListView;
        private AddCust EditCustForm;
        public Customer CustomerToEdit;
        public bool bIsSaveNeeded;
    }
}