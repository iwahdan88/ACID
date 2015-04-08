namespace ACID
{
    partial class Authentication
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
            this.AuthBox = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // AuthBox
            // 
            this.AuthBox.AutoSize = true;
            this.AuthBox.Location = new System.Drawing.Point(200, 27);
            this.AuthBox.Name = "AuthBox";
            this.AuthBox.Size = new System.Drawing.Size(57, 13);
            this.AuthBox.TabIndex = 0;
            this.AuthBox.Text = "Logging In";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(57, 54);
            this.progressBar1.MarqueeAnimationSpeed = 150;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(359, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            // 
            // Authentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 110);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.AuthBox);
            this.Location = new System.Drawing.Point(350, 350);
            this.Name = "Authentication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentication";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AuthBox;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}