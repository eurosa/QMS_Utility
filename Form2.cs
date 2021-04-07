using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMS_Utility
{
    public partial class Form2 : Form
    {
        string select = "----------Select----------------";
        public string portName = "";
        Form1 f1;
        public Form2(Form1 form1)
        {
            f1 = form1;
            InitializeComponent();
            

            if (!comboBox1.Items.Contains(select))
            {

                comboBox1.Items.Add(select);

            }
            comboBox1.SelectedIndex = 0;
        }

        public string[] getSerialPort()
        {
             
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            
            Console.WriteLine("The following serial ports were found:");
            
            // Display each port name to the console.
            foreach (string port in ports)
             {
                
                if (!comboBox1.Items.Contains(port))
                {
                    comboBox1.Items.Add(port);
                    Console.WriteLine(port);
                }
             }
            
            Console.ReadLine();
            return ports;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBox1.Text);
            portName = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            portName = comboBox1.Text;
        
            this.Close();
        }
    }
}
