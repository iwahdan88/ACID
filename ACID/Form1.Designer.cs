namespace ACID
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.CustNum = new System.Windows.Forms.TextBox();
            this.Search = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NewCust = new System.Windows.Forms.Button();
            this.Adresse = new System.Windows.Forms.TextBox();
            this.l3 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PhoneNum = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cust_Name = new System.Windows.Forms.TextBox();
            this.CustName = new System.Windows.Forms.Label();
            this.CurrUser = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.Label();
            this.Order = new System.Windows.Forms.Button();
            this.Server = new System.Windows.Forms.Label();
            this.Server_Name = new System.Windows.Forms.Label();
            this.EditBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(609, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "رقم الهاتف ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // CustNum
            // 
            this.CustNum.Location = new System.Drawing.Point(481, 141);
            this.CustNum.Name = "CustNum";
            this.CustNum.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CustNum.Size = new System.Drawing.Size(186, 20);
            this.CustNum.TabIndex = 1;
            this.CustNum.TextChanged += new System.EventHandler(this.CustNum_TextChanged);
            this.CustNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CustNum_KeyDown);
            // 
            // Search
            // 
            this.Search.Enabled = false;
            this.Search.Location = new System.Drawing.Point(297, 138);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(138, 23);
            this.Search.TabIndex = 2;
            this.Search.Text = "أبحث برقم الهاتف ";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(274, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "مطعم شيخ البلد ";
            // 
            // NewCust
            // 
            this.NewCust.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.NewCust.Location = new System.Drawing.Point(35, 96);
            this.NewCust.Name = "NewCust";
            this.NewCust.Size = new System.Drawing.Size(203, 39);
            this.NewCust.TabIndex = 4;
            this.NewCust.Text = "تسجيل عميل جديد ";
            this.NewCust.UseVisualStyleBackColor = true;
            this.NewCust.Click += new System.EventHandler(this.NewCust_Click);
            // 
            // Adresse
            // 
            this.Adresse.BackColor = System.Drawing.Color.White;
            this.Adresse.Enabled = false;
            this.Adresse.Location = new System.Drawing.Point(66, 125);
            this.Adresse.Multiline = true;
            this.Adresse.Name = "Adresse";
            this.Adresse.Size = new System.Drawing.Size(497, 73);
            this.Adresse.TabIndex = 5;
            // 
            // l3
            // 
            this.l3.AutoSize = true;
            this.l3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l3.Location = new System.Drawing.Point(582, 128);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(46, 17);
            this.l3.TabIndex = 6;
            this.l3.Text = "العنوان :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(584, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "الهاتف :";
            // 
            // PhoneNum
            // 
            this.PhoneNum.BackColor = System.Drawing.Color.White;
            this.PhoneNum.Enabled = false;
            this.PhoneNum.Location = new System.Drawing.Point(356, 230);
            this.PhoneNum.Name = "PhoneNum";
            this.PhoneNum.Size = new System.Drawing.Size(207, 26);
            this.PhoneNum.TabIndex = 8;
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
            this.groupBox1.Location = new System.Drawing.Point(26, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(641, 281);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "البيانات";
            // 
            // Cust_Name
            // 
            this.Cust_Name.BackColor = System.Drawing.Color.White;
            this.Cust_Name.Enabled = false;
            this.Cust_Name.Location = new System.Drawing.Point(160, 65);
            this.Cust_Name.Name = "Cust_Name";
            this.Cust_Name.Size = new System.Drawing.Size(403, 26);
            this.Cust_Name.TabIndex = 10;
            // 
            // CustName
            // 
            this.CustName.AutoSize = true;
            this.CustName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustName.Location = new System.Drawing.Point(588, 65);
            this.CustName.Name = "CustName";
            this.CustName.Size = new System.Drawing.Size(40, 17);
            this.CustName.TabIndex = 9;
            this.CustName.Text = "الاسم :";
            // 
            // CurrUser
            // 
            this.CurrUser.AutoSize = true;
            this.CurrUser.Location = new System.Drawing.Point(15, 19);
            this.CurrUser.Name = "CurrUser";
            this.CurrUser.Size = new System.Drawing.Size(46, 13);
            this.CurrUser.TabIndex = 10;
            this.CurrUser.Text = "UserID :";
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserName.Location = new System.Drawing.Point(77, 17);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(0, 17);
            this.UserName.TabIndex = 11;
            // 
            // Order
            // 
            this.Order.BackColor = System.Drawing.SystemColors.Control;
            this.Order.Enabled = false;
            this.Order.Location = new System.Drawing.Point(296, 513);
            this.Order.Name = "Order";
            this.Order.Size = new System.Drawing.Size(139, 38);
            this.Order.TabIndex = 12;
            this.Order.Text = "تسجيل طلب";
            this.Order.UseVisualStyleBackColor = false;
            this.Order.Click += new System.EventHandler(this.Order_Click);
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Location = new System.Drawing.Point(15, 57);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(41, 13);
            this.Server.TabIndex = 13;
            this.Server.Text = "Server:";
            // 
            // Server_Name
            // 
            this.Server_Name.AutoSize = true;
            this.Server_Name.Location = new System.Drawing.Point(71, 56);
            this.Server_Name.Name = "Server_Name";
            this.Server_Name.Size = new System.Drawing.Size(33, 13);
            this.Server_Name.TabIndex = 14;
            this.Server_Name.Text = "None";
            // 
            // EditBtn
            // 
            this.EditBtn.Location = new System.Drawing.Point(35, 153);
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(201, 34);
            this.EditBtn.TabIndex = 15;
            this.EditBtn.Text = "تعديل بيانات عميل";
            this.EditBtn.UseVisualStyleBackColor = true;
            this.EditBtn.Click += new System.EventHandler(this.EditBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 575);
            this.Controls.Add(this.EditBtn);
            this.Controls.Add(this.Server_Name);
            this.Controls.Add(this.Server);
            this.Controls.Add(this.Order);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.CurrUser);
            this.Controls.Add(this.NewCust);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.CustNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.TextBox CustNum;
        protected System.Windows.Forms.Button Search;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button NewCust;
        protected System.Windows.Forms.TextBox Adresse;
        protected System.Windows.Forms.Label l3;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.TextBox PhoneNum;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.TextBox Cust_Name;
        protected System.Windows.Forms.Label CustName;
        protected System.Windows.Forms.Label CurrUser;
        protected System.Windows.Forms.Label UserName;
        protected System.Windows.Forms.Button Order;
        protected System.Windows.Forms.Label Server;
        protected System.Windows.Forms.Label Server_Name;
        private System.Windows.Forms.Button EditBtn;
    }
}

