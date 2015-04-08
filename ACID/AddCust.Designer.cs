namespace ACID
{
    partial class AddCust
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
            this.AddCustLabl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cust_Name = new System.Windows.Forms.TextBox();
            this.CustName = new System.Windows.Forms.Label();
            this.PhoneNum = new System.Windows.Forms.TextBox();
            this.l3 = new System.Windows.Forms.Label();
            this.Adresse = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddCustLabl
            // 
            this.AddCustLabl.AutoSize = true;
            this.AddCustLabl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddCustLabl.Location = new System.Drawing.Point(285, 35);
            this.AddCustLabl.Name = "AddCustLabl";
            this.AddCustLabl.Size = new System.Drawing.Size(75, 20);
            this.AddCustLabl.TabIndex = 0;
            this.AddCustLabl.Text = "اضافة عميل ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.Cust_Name);
            this.groupBox1.Controls.Add(this.CustName);
            this.groupBox1.Controls.Add(this.PhoneNum);
            this.groupBox1.Controls.Add(this.l3);
            this.groupBox1.Controls.Add(this.Adresse);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(641, 281);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "البيانات";
            // 
            // Cust_Name
            // 
            this.Cust_Name.BackColor = System.Drawing.Color.White;
            this.Cust_Name.Location = new System.Drawing.Point(161, 56);
            this.Cust_Name.Name = "Cust_Name";
            this.Cust_Name.Size = new System.Drawing.Size(403, 26);
            this.Cust_Name.TabIndex = 10;
            this.Cust_Name.TextChanged += new System.EventHandler(this.Cust_Name_TextChanged);
            // 
            // CustName
            // 
            this.CustName.AutoSize = true;
            this.CustName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustName.Location = new System.Drawing.Point(589, 56);
            this.CustName.Name = "CustName";
            this.CustName.Size = new System.Drawing.Size(40, 17);
            this.CustName.TabIndex = 9;
            this.CustName.Text = "الاسم :";
            // 
            // PhoneNum
            // 
            this.PhoneNum.BackColor = System.Drawing.Color.White;
            this.PhoneNum.Location = new System.Drawing.Point(357, 223);
            this.PhoneNum.Name = "PhoneNum";
            this.PhoneNum.Size = new System.Drawing.Size(207, 26);
            this.PhoneNum.TabIndex = 8;
            this.PhoneNum.TextChanged += new System.EventHandler(this.PhoneNum_TextChanged);
            // 
            // l3
            // 
            this.l3.AutoSize = true;
            this.l3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l3.Location = new System.Drawing.Point(583, 109);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(46, 17);
            this.l3.TabIndex = 6;
            this.l3.Text = "العنوان :";
            // 
            // Adresse
            // 
            this.Adresse.BackColor = System.Drawing.Color.White;
            this.Adresse.Location = new System.Drawing.Point(67, 106);
            this.Adresse.Multiline = true;
            this.Adresse.Name = "Adresse";
            this.Adresse.Size = new System.Drawing.Size(497, 91);
            this.Adresse.TabIndex = 5;
            this.Adresse.TextChanged += new System.EventHandler(this.Adresse_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(585, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "الهاتف :";
            // 
            // Add
            // 
            this.Add.Enabled = false;
            this.Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.Location = new System.Drawing.Point(254, 435);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(155, 34);
            this.Add.TabIndex = 11;
            this.Add.Text = "تسجيل";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // AddCust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 508);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AddCustLabl);
            this.Name = "AddCust";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddCust";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AddCustLabl;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.TextBox Cust_Name;
        protected System.Windows.Forms.Label CustName;
        protected System.Windows.Forms.TextBox PhoneNum;
        protected System.Windows.Forms.Label l3;
        protected System.Windows.Forms.TextBox Adresse;
        protected System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Add;
    }
}