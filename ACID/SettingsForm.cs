using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace ACID
{
    public partial class SettingsForm : Form
    {
        private string[] Ports;
        public SettingsForm()
        {
            Ports = SerialPort.GetPortNames();
            PortName = string.Empty;
            baudRate = 0;
            InitString = string.Empty;
            InitializeComponent();
            this.comboBox1.Items.AddRange(Ports);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text != string.Empty)
            {
                PortName = this.comboBox1.Text;
            }
            else
            {
                MessageBox.Show("Choose a COM port");
                return;
            }
            if (this.textBox1.Text != string.Empty)
            {
                try
                {
                    baudRate = Convert.ToInt32(this.textBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Choose a valid baud rate " + ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Choose a Baud rate");
                return;
            }
            if(this.textBox2.Text != string.Empty)
            {
                InitString = this.textBox2.Text;
            }
            SerializeData();
            this.Dispose();
        }

        private void SerializeData()
        {
            File.WriteAllText(@"modem.txt", PortName + Environment.NewLine + Convert.ToString(baudRate) + Environment.NewLine + InitString);
        }
    }
}
