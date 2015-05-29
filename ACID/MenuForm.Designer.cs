

namespace ACID
{
    partial class MenuForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.dataSet1 = new System.Data.DataSet();
            this.dataSet2 = new System.Data.DataSet();
            this.Add_Item = new System.Windows.Forms.Button();
            this.Delete_Item = new System.Windows.Forms.Button();
            this.Finish_Order = new System.Windows.Forms.Button();
            this.OrderedList = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Cat_Panel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderedList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // dataSet2
            // 
            this.dataSet2.DataSetName = "NewDataSet";
            // 
            // Add_Item
            // 
            this.Add_Item.Location = new System.Drawing.Point(216, 485);
            this.Add_Item.Name = "Add_Item";
            this.Add_Item.Size = new System.Drawing.Size(121, 32);
            this.Add_Item.TabIndex = 2;
            this.Add_Item.Text = "اضف طلب";
            this.Add_Item.UseVisualStyleBackColor = true;
            this.Add_Item.Click += new System.EventHandler(this.Add_Item_Click);
            // 
            // Delete_Item
            // 
            this.Delete_Item.Location = new System.Drawing.Point(649, 485);
            this.Delete_Item.Name = "Delete_Item";
            this.Delete_Item.Size = new System.Drawing.Size(121, 32);
            this.Delete_Item.TabIndex = 4;
            this.Delete_Item.Text = "امسح";
            this.Delete_Item.UseVisualStyleBackColor = true;
            this.Delete_Item.Click += new System.EventHandler(this.Delete_Item_Click);
            // 
            // Finish_Order
            // 
            this.Finish_Order.Location = new System.Drawing.Point(364, 810);
            this.Finish_Order.Name = "Finish_Order";
            this.Finish_Order.Size = new System.Drawing.Size(261, 31);
            this.Finish_Order.TabIndex = 5;
            this.Finish_Order.Text = "تسجيل وطباعة الطلب";
            this.Finish_Order.UseVisualStyleBackColor = true;
            this.Finish_Order.Click += new System.EventHandler(this.Finish_Order_Click);
            // 
            // OrderedList
            // 
            this.OrderedList.AllowUserToAddRows = false;
            this.OrderedList.AllowUserToDeleteRows = false;
            this.OrderedList.AllowUserToResizeColumns = false;
            this.OrderedList.AllowUserToResizeRows = false;
            this.OrderedList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OrderedList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.OrderedList.DefaultCellStyle = dataGridViewCellStyle1;
            this.OrderedList.Location = new System.Drawing.Point(22, 533);
            this.OrderedList.Name = "OrderedList";
            this.OrderedList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OrderedList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.OrderedList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrderedList.Size = new System.Drawing.Size(948, 237);
            this.OrderedList.TabIndex = 8;
            this.OrderedList.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OrderedList_RowHeaderMouseDoubleClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(22, 192);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(948, 237);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(422, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "قائمة الطلبات";
            // 
            // Cat_Panel
            // 
            this.Cat_Panel.Location = new System.Drawing.Point(56, 60);
            this.Cat_Panel.Name = "Cat_Panel";
            this.Cat_Panel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Cat_Panel.Size = new System.Drawing.Size(882, 115);
            this.Cat_Panel.TabIndex = 14;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(992, 868);
            this.Controls.Add(this.Cat_Panel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OrderedList);
            this.Controls.Add(this.Finish_Order);
            this.Controls.Add(this.Delete_Item);
            this.Controls.Add(this.Add_Item);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderedList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Data.DataSet dataSet1;
        public System.Data.DataSet dataSet2;
        private System.Windows.Forms.Button Add_Item;
        private System.Windows.Forms.Button Delete_Item;
        private System.Windows.Forms.Button Finish_Order;
        public System.Windows.Forms.DataGridView OrderedList;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.FlowLayoutPanel Cat_Panel;
        private MySql.Data.MySqlClient.MySqlConnection myConn;
        private Order NewOrder;
        private Customer MyCustomer;
        private string UserID;
        private string Server_Name;
        public int CurrentTblindex;
        private TotalSumForm TotalSumWindow;
    }
}