namespace ACID
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.Label_login = new System.Windows.Forms.Label();
            this.UserID = new System.Windows.Forms.Label();
            this.User_TextBox = new System.Windows.Forms.TextBox();
            this.PassText = new System.Windows.Forms.Label();
            this.Pass_textBox = new System.Windows.Forms.TextBox();
            this.LogInButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_login
            // 
            this.Label_login.AutoSize = true;
            this.Label_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_login.Location = new System.Drawing.Point(104, 22);
            this.Label_login.Name = "Label_login";
            this.Label_login.Size = new System.Drawing.Size(191, 39);
            this.Label_login.TabIndex = 4;
            this.Label_login.Text = "مطعم شيخ البلد ";
            // 
            // UserID
            // 
            this.UserID.AutoSize = true;
            this.UserID.Location = new System.Drawing.Point(104, 128);
            this.UserID.Name = "UserID";
            this.UserID.Size = new System.Drawing.Size(43, 13);
            this.UserID.TabIndex = 5;
            this.UserID.Text = "User ID";
            this.UserID.Click += new System.EventHandler(this.UserID_Click);
            // 
            // User_TextBox
            // 
            this.User_TextBox.Location = new System.Drawing.Point(163, 125);
            this.User_TextBox.Name = "User_TextBox";
            this.User_TextBox.Size = new System.Drawing.Size(132, 20);
            this.User_TextBox.TabIndex = 6;
            // 
            // PassText
            // 
            this.PassText.AutoSize = true;
            this.PassText.Location = new System.Drawing.Point(104, 180);
            this.PassText.Name = "PassText";
            this.PassText.Size = new System.Drawing.Size(53, 13);
            this.PassText.TabIndex = 7;
            this.PassText.Text = "Password";
            // 
            // Pass_textBox
            // 
            this.Pass_textBox.Location = new System.Drawing.Point(163, 177);
            this.Pass_textBox.Name = "Pass_textBox";
            this.Pass_textBox.Size = new System.Drawing.Size(132, 20);
            this.Pass_textBox.TabIndex = 8;
            this.Pass_textBox.UseSystemPasswordChar = true;
            this.Pass_textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pass_textBox_KeyDown);
            // 
            // LogInButton
            // 
            this.LogInButton.Location = new System.Drawing.Point(123, 249);
            this.LogInButton.Name = "LogInButton";
            this.LogInButton.Size = new System.Drawing.Size(143, 34);
            this.LogInButton.TabIndex = 9;
            this.LogInButton.Text = "LOGIN";
            this.LogInButton.UseVisualStyleBackColor = true;
            this.LogInButton.Click += new System.EventHandler(this.LogInButton_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 327);
            this.Controls.Add(this.LogInButton);
            this.Controls.Add(this.Pass_textBox);
            this.Controls.Add(this.PassText);
            this.Controls.Add(this.User_TextBox);
            this.Controls.Add(this.UserID);
            this.Controls.Add(this.Label_login);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_login;
        private System.Windows.Forms.Label UserID;
        private System.Windows.Forms.TextBox User_TextBox;
        private System.Windows.Forms.Label PassText;
        private System.Windows.Forms.TextBox Pass_textBox;
        private System.Windows.Forms.Button LogInButton;
    }
}