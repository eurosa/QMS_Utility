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
        string select = "---Select---";

        public Form2()
        {
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
    }
}
