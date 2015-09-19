namespace ACID
{
    partial class OrderCancelForm
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
            this.CancelOrderLabel = new System.Windows.Forms.Label();
            this.OrderNumLabel = new System.Windows.Forms.Label();
            this.OrderNumField = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordField = new System.Windows.Forms.TextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CancelOrderLabel
            // 
            this.CancelOrderLabel.AutoSize = true;
            this.CancelOrderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelOrderLabel.Location = new System.Drawing.Point(132, 24);
            this.CancelOrderLabel.Name = "CancelOrderLabel";
            this.CancelOrderLabel.Size = new System.Drawing.Size(123, 37);
            this.CancelOrderLabel.TabIndex = 0;
            this.CancelOrderLabel.Text = "الغاء طلب";
            // 
            // OrderNumLabel
            // 
            this.OrderNumLabel.AutoSize = true;
            this.OrderNumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderNumLabel.Location = new System.Drawing.Point(241, 84);
            this.OrderNumLabel.Name = "OrderNumLabel";
            this.OrderNumLabel.Size = new System.Drawing.Size(60, 20);
            this.OrderNumLabel.TabIndex = 1;
            this.OrderNumLabel.Text = "رقم الطلب";
            // 
            // OrderNumField
            // 
            this.OrderNumField.Location = new System.Drawing.Point(84, 86);
            this.OrderNumField.Name = "OrderNumField";
            this.OrderNumField.Size = new System.Drawing.Size(146, 20);
            this.OrderNumField.TabIndex = 2;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(237, 141);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(71, 20);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "الرقم السري";
            // 
            // PasswordField
            // 
            this.PasswordField.Location = new System.Drawing.Point(85, 140);
            this.PasswordField.Name = "PasswordField";
            this.PasswordField.Size = new System.Drawing.Size(145, 20);
            this.PasswordField.TabIndex = 4;
            this.PasswordField.UseSystemPasswordChar = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(122, 195);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(123, 28);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "الغاء";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // OrderCancelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 265);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.PasswordField);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.OrderNumField);
            this.Controls.Add(this.OrderNumLabel);
            this.Controls.Add(this.CancelOrderLabel);
            this.MaximizeBox = false;
            this.Name = "OrderCancelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OrderCancelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CancelOrderLabel;
        private System.Windows.Forms.Label OrderNumLabel;
        private System.Windows.Forms.TextBox OrderNumField;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PasswordField;
        private System.Windows.Forms.Button CancelBtn;
        const string Password = "shk2015";
        public bool bIsCancelAccepted;
        public string OrderID;
    }
}