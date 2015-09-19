namespace ACID
{
    partial class TotalSumForm
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
            this.TotalSumLabel = new System.Windows.Forms.Label();
            this.Total = new System.Windows.Forms.Label();
            this.OkBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TotalSumLabel
            // 
            this.TotalSumLabel.AutoSize = true;
            this.TotalSumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 35.75F);
            this.TotalSumLabel.Location = new System.Drawing.Point(83, 31);
            this.TotalSumLabel.Name = "TotalSumLabel";
            this.TotalSumLabel.Size = new System.Drawing.Size(235, 55);
            this.TotalSumLabel.TabIndex = 0;
            this.TotalSumLabel.Text = "اجمالي الطلب";
            // 
            // Total
            // 
            this.Total.AutoSize = true;
            this.Total.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.Total.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Total.Location = new System.Drawing.Point(166, 147);
            this.Total.Name = "Total";
            this.Total.Size = new System.Drawing.Size(78, 31);
            this.Total.TabIndex = 1;
            this.Total.Text = "####";
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(156, 245);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(97, 33);
            this.OkBtn.TabIndex = 2;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // TotalSumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 307);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.TotalSumLabel);
            this.Name = "TotalSumForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TotalSumForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TotalSumLabel;
        private System.Windows.Forms.Label Total;
        private double Sum;
        private System.Windows.Forms.Button OkBtn;
        public bool bIsAbbort;
    }
}