using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMS_Utility
{
    public partial class Form1 : Form
    {
        Form2 form2;
        public Form1()
        {
            InitializeComponent();
            form2 = new Form2(this);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void portButton_Click(object sender, EventArgs e)
        {

             
            form2.getSerialPort();

            // form2.comboBox1.DataValueField = "ID";
            // form2.comboBox1.DataTextField = "Description";
            // form2.comboBox1.DataBind();

            // Then add your first item
            

            form2.ShowDialog(); // Shows Form2
            

            // form2.comboBox1.DataSource = form2.getSerialPort();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Form getForm1Object()
        {

            return this;
        }
    }
}
