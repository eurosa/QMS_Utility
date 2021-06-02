using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMS_Utility
{
    public partial class Form1 : Form
    {
        Model model;
        SQLiteConnection m_dbConnection;
        string select = "New Record";
        Dictionary<int, string> userListDictionary;

        private PrintPreviewControl ppc;
        private PrintDocument docToPrint = new PrintDocument();

        public Form1()
        {
            InitializeComponent();
            groupBox1.Paint += comSerialPort_Paint;
            groupBox2.Paint += groupBox1_Paint;
            groupBox3.Paint += groupBox1_Paint;
            groupBox4.Paint += cntLabel_Paint;
            cmbPortName.Height = 120;
            // this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Auto;
            sendBankID.Enabled = false;
            copiesSend.Enabled = false;
            closingTimeSend.Enabled = false;
            sendCounterNo.Enabled = false;
            // Tab Color control
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            //  tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.DrawItem += TcRemontas_DrawItem;
            // Tab Color control
            //  tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem_1);

            // form2 = new Form2(this);
            model = new Model();
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");

            userListDictionary = new Dictionary<int, string>();

            createDB();

            newRecordValue();
            Fillcombobox();

            // SetPlaceholder(recordFileName, " Enter Record Name");
            // SetPlaceholder(institutionTextBox, "Enter Institute Name");
            // SetPlaceholder(bankIdTextBox, "Enter Bank Id");
            // SetPlaceholder(timeTextBox, "Enter Time and Date");
 


        }


        private void TcRemontas_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (e.Index == tabControl.SelectedIndex)
            {
                e.Graphics.DrawString(tabControl.TabPages[e.Index].Text,
                    new Font(tabControl.Font, FontStyle.Bold),
                    Brushes.Blue,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else
            {
                e.Graphics.DrawString(tabControl.TabPages[e.Index].Text,
                    tabControl.Font,
                    Brushes.Blue,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
        }

        private void tabControl1_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            if (e.Index == tabControl1.SelectedIndex)
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    new Font(tabControl1.Font, FontStyle.Bold),
                    Brushes.Blue,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    tabControl1.Font,
                    Brushes.Blue,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
        }

        public void newRecordValue() {

            if (!qmsComboBox.Items.Contains(select))
            {

                qmsComboBox.Items.Add(select);

            }
            qmsComboBox.SelectedIndex = 0;

        }

        private void comSerialPort_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = (GroupBox)sender;
            Graphics gfx = e.Graphics;

            Pen p = new Pen(Color.WhiteSmoke, 3);

            e.Graphics.Clear(SystemColors.Control);
            e.Graphics.DrawString(box.Text, box.Font, Brushes.Red, 10, 0);//Group box Text x,y position

            gfx.DrawLine(p, 0, 5, 0, e.ClipRectangle.Height - 2);//Left Y and up x
            gfx.DrawLine(p, 0, 5, 10, 5);//LEFT y

            gfx.DrawLine(p, 140, 5, e.ClipRectangle.Width - 2, 5);//Right y and up x

            gfx.DrawLine(p, e.ClipRectangle.Width - 2, 5, e.ClipRectangle.Width - 2, e.ClipRectangle.Height);// Right Y
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2, 0, e.ClipRectangle.Height - 2);//Bottom X


        }


        private void cntLabel_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = (GroupBox)sender;
            Graphics gfx = e.Graphics;

            Pen p = new Pen(Color.WhiteSmoke, 3);

            e.Graphics.Clear(SystemColors.Control);
            e.Graphics.DrawString(box.Text, box.Font, Brushes.Red, 10, 0);//Group box Text x,y position

            gfx.DrawLine(p, 0, 5, 0, e.ClipRectangle.Height - 2);//Left Y and up x
            gfx.DrawLine(p, 0, 5, 10, 5);//LEFT y

            gfx.DrawLine(p, 132, 5, e.ClipRectangle.Width - 2, 5);//Right y and up x

            gfx.DrawLine(p, e.ClipRectangle.Width - 2, 5, e.ClipRectangle.Width - 2, e.ClipRectangle.Height);// Right Y
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2, 0, e.ClipRectangle.Height - 2);//Bottom X


        }


        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = (GroupBox)sender;
            Graphics gfx = e.Graphics;
            
            Pen p = new Pen(Color.WhiteSmoke, 3);

            e.Graphics.Clear(SystemColors.Control);
            e.Graphics.DrawString(box.Text, box.Font, Brushes.Red, 10, 0);//Group box Text x,y position

            gfx.DrawLine(p, 0, 5, 0, e.ClipRectangle.Height - 2);//Left Y and up x
            gfx.DrawLine(p, 0, 5, 10, 5);//LEFT y

            gfx.DrawLine(p, 110, 5, e.ClipRectangle.Width - 2, 5);//Right y and up x

            gfx.DrawLine(p, e.ClipRectangle.Width - 2, 5, e.ClipRectangle.Width - 2, e.ClipRectangle.Height  );// Right Y
            gfx.DrawLine(p, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2, 0, e.ClipRectangle.Height - 2);//Bottom X
           
            
        }

        private void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(SystemColors.Control);
            p.Graphics.DrawString(box.Text, box.Font, Brushes.Black, 0, 0);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ComPort.Close();
            this.Close();
            
        }

        public Form getForm1Object()
        {

            return this;
        }

        
        private void Form1_Load(object sender, EventArgs e) 
            {
            updatePorts();           //Call this function everytime the page load 
                                     //to update port names
            CheckForIllegalCrossThreadCalls = false;
            }
        private void updatePorts()
            {
            // Retrieve the list of all COM ports on your Computer
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                {
                if (!cmbPortName.Items.Contains(port))
                { 
                    cmbPortName.Items.Add(port);
                }
            }
            }
        private SerialPort ComPort = new SerialPort();  //Initialise ComPort Variable as SerialPort
        private void connect()
            {
            bool error = false;

            // Check if all settings have been selected

            if (cmbPortName.SelectedIndex != -1)
                {
                    //if yes than Set The Port's settings
                    ComPort.PortName = cmbPortName.Text;
                    ComPort.BaudRate = int.Parse("9600");      //convert Text to Integer
                    ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None"); //convert Text to Parity
                    //ComPort.DataBits = int.Parse(cmbDataBits.Text);        //convert Text to Integer
                    //ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);  //convert Text to stop bits
                    
                try  //always try to use this try and catch method to open your port. 
                     //if there is an error your program will not display a message instead of freezing.
                    {
                    //Open Port
                    ComPort.Open();
                    ComPort.DataReceived += SerialPortDataReceived;  //Check for received data. When there is data in the receive buffer,
                                                                   //it will raise this event, we need to subscribe to it to know when there is data
                    }
                catch (UnauthorizedAccessException) { error = true; }
                catch (System.IO.IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error) MessageBox.Show(this, "Could not open the COM port. Most likely it is already in use, has been removed, or is unavailable.", "COM Port unavailable", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            else
                {
                MessageBox.Show("Please select all the COM Serial Port Settings", "Serial Port Interface", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
               //if the port is open, Change the Connect button to disconnect, enable the send button.
               //and disable the groupBox to prevent changing configuration of an open port.
            if (ComPort.IsOpen)
                {
                btnConnect.Text = "Disconnect";
                // btnSend.Enabled = true;
                /* if (!rdText.Checked & !rdHex.Checked)  //if no data mode is selected, then select text mode by default
                     {
                     rdText.Checked = true;
                     }    */
                // groupBox1.Enabled = false;
                cmbPortName.Enabled = false;

                }
             }
              // Call this function to close the port.
        private void disconnect()
            {
            ComPort.Close();
            btnConnect.Text = "Connect";
            //btnSend.Enabled = false;
            // groupBox1.Enabled = true;
            cmbPortName.Enabled = true;

        }
              //whenever the connect button is clicked, it will check if the port is already open, call the disconnect function.
              // if the port is closed, call the connect function.
        private void btnConnect_Click(object sender, EventArgs e)
                                  
            {
            if (ComPort.IsOpen)
                {
                disconnect();
                }
            else
                {
                connect();
                }
            }

        private void btnClear_Click(object sender, EventArgs e)
            {
            //Clear the screen
            //rtxtDataArea.Clear();
            //txtSend.Clear();
            }
        // Function to send data to the serial port
        private void sendData()
            {
           /* bool error = false;
            if (rdText.Checked == true)        //if text mode is selected, send data as tex
                {
                // Send the user's text straight out the port 
                ComPort.Write(txtSend.Text );
               
                // Show in the terminal window 
                rtxtDataArea.ForeColor = Color.Green;    //write sent text data in green colour              
                txtSend.Clear();                       //clear screen after sending data

                }
            else                    //if Hex mode is selected, send data in hexadecimal
                {
                try
                    {
                    // Convert the user's string of hex digits (example: E1 FF 1B) to a byte array
                    byte[] data = HexStringToByteArray(txtSend.Text);

                    // Send the binary data out the port
                  ComPort.Write(data, 0, data.Length);

                    // Show the hex digits on in the terminal window
                  rtxtDataArea.ForeColor = Color.Blue;   //write Hex data in Blue
                  rtxtDataArea.AppendText(txtSend.Text.ToUpper() + "\n");
                  txtSend.Clear();                       //clear screen after sending data
                    }
                catch (FormatException) { error = true; }
                    
                    // Inform the user if the hex string was not properly formatted
                    catch (ArgumentException) { error = true; }

                if (error) MessageBox.Show(this, "Not properly formatted hex string: " + txtSend.Text + "\n" + "example: E1 FF 1B", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                      
                }*/
            }
        //Convert a string of hex digits (example: E1 FF 1B) to a byte array. 
        //The string containing the hex digits (with or without spaces)
        //Returns an array of bytes. </returns>

        private void sendDataToPort(string textData)
        {
            
            Console.WriteLine(textData);
            bool error = false;
             
                 try
                     {
                /*******************************************************************************************************************************
                 *To add fixed string in of 28 alphabet 
                 *******************************************************************************************************************************/

                // string textBoxData = textData.PadRight(28, ' ').Substring(0, 28);
                if (ComPort.IsOpen)
                {
                    ComPort.Write(textData);
                    AutoClosingMessageBox.Show(textData+" data sent successfully", "Data", 1000);
                }
                else
                {
                    MessageBox.Show(this, "Could not open the COM port. Most likely it is already in use, has been removed, or is unavailable.", "COM Port unavailable", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
                //Convert the user's string of hex digits (example: E1 FF 1B) to a byte array
                //byte[] data = HexStringToByteArray(textData);
                //ComPort.Write(textData);
                //Send the binary data out the port
                //ComPort.Write(data, 0, data.Length);

                //Show the hex digits on in the terminal window
                //rtxtDataArea.ForeColor = Color.Blue;   //write Hex data in Blue
                //rtxtDataArea.AppendText(txtSend.Text.ToUpper() + "\n");
                //txtSend.Clear();                       
                //clear screen after sending data
            }
                 catch (FormatException) { error = true; }

                     // Inform the user if the hex string was not properly formatted
                     catch (ArgumentException) { error = true; }

                 if (error) MessageBox.Show(this, "Not properly formatted hex string: " + textData + "\n" + "example: E1 FF 1B", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                 
        }


        private byte[] HexStringToByteArray(string s)
            {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
            }
        
        private void btnSend_Click(object sender, EventArgs e)
            {
            sendData();
            }
            //This event will be raised when the form is closing.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
            {
            if (ComPort.IsOpen) ComPort.Close();  //close the port if open when exiting the application.
            }
        //Data recived from the serial port is coming from another thread context than the UI thread.
        //Instead of reading the content directly in the SerialPortDataReceived, we need to use a delegate.
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            //invokeRequired required compares the thread ID of the calling thread to the thread of the creating thread.
            // if these threads are different, it returns true
           /* if (this.rtxtDataArea.InvokeRequired)
            {
                rtxtDataArea.ForeColor = Color.Green;    //write text data in Green colour
                
                SetTextCallback d = new SetTextCallback(SetText);              
                this.Invoke(d, new object[] { text });
            }

            else
            {
                this.rtxtDataArea.AppendText(text ); 
            }*/
        }
        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort)sender;
            var data = serialPort.ReadExisting();
            SetText(data);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            updatePorts();           //Call this function to update port names
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {

        }

        private void institutionTextBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void sendName_Click_1(object sender, EventArgs e)
        {
            string institutionText = fixedLengthString(institutionTextBox.Text, 28);
            sendDataToPort("$BnkL"+ institutionText + ";");

            
        }

        private void sendBankID_Click_1(object sender, EventArgs e)
        {
            string bankIdText = fixedLengthString(bankIdTextBox.Text, 2);
            sendDataToPort("$BANK" + "" + bankIdText + ";");
            
        }

        private void sendTime_Click_1(object sender, EventArgs e)
        {
            string timeTex = fixedLengthString(timeTextBox.Text, 28);
            sendDataToPort("$TIME" + "" + timeTex + ";");
        }

        private void sendCounter_Click_1(object sender, EventArgs e)
        {
            string counteText = fixedLengthString(counteTextBox.Text, 7);
            sendDataToPort("$CTID" + "" + counteText + ";");
        }

        private void sendCounterNo_Click_1(object sender, EventArgs e)
        {
            string totalCounterText = fixedLengthString(totalCounterTextBox.Text, 4);
            sendDataToPort("$CONTR" + "" + totalCounterText + ";");
        }

        private void closingTimeSend_Click_1(object sender, EventArgs e)
        {
            string closingTimeText = fixedLengthString(closingTimeTextBox.Text, 4);
            sendDataToPort("$CLTM" + "" + closingTimeText + ";");
        }

        private void sendToken1_Click_1(object sender, EventArgs e)
        {
            string tokenSlip1Text = fixedLengthString(tokenSlip1TextBox.Text, 28);
            sendDataToPort("$TSL9" + "" + tokenSlip1Text + ";");
        }

        private void sendToken2_Click_1(object sender, EventArgs e)
        {
            string tokenSlip2Text = fixedLengthString(tokenSlip2TextBox.Text, 28);
            sendDataToPort("$TSLA" + "" + tokenSlip2Text + ";");
        }

        private void sendToken3_Click_1(object sender, EventArgs e)
        {
            string tokenSlipBText = fixedLengthString(tokenSlipBTextBox.Text, 28);
            sendDataToPort("$TSLB" + "" + tokenSlipBText + ";");
        }

        private void tokenSlip1TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void sendCntLabel1_Click_1(object sender, EventArgs e)
        {
            string cntLabel1Text = fixedLengthString(cntLabel1TextBox.Text, 28);
            sendDataToPort("$LaC1" + "" + cntLabel1Text + ";");
        }

        private void sendCntLabel2_Click_1(object sender, EventArgs e)
        {
            string cntLabel2Text = fixedLengthString(cntLabel2TextBox.Text, 28);
            sendDataToPort("$LaC2" + "" + cntLabel2Text + ";");
        }

        private void sendCntLabel3_Click_1(object sender, EventArgs e)
        {
            string cntLabel3Text = fixedLengthString(cntLabel3TextBox.Text, 28);
            sendDataToPort("$LaC3" + "" + cntLabel3Text + ";");
        }

        private void sendCntLabel4_Click_1(object sender, EventArgs e)
        {
            string cntLabel4Text = fixedLengthString(cntLabel4TextBox.Text, 28);
            sendDataToPort("$LaC4" + "" + cntLabel4Text + ";");
        }

        private void sendCntLabel5_Click_1(object sender, EventArgs e)
        {
            string cntLabel5Text = fixedLengthString(cntLabel5TextBox.Text, 28);
            sendDataToPort("$LaC5" + "" + cntLabel5Text + ";");
        }

        private void sendCntLabel6_Click_1(object sender, EventArgs e)
        {
            string cntLabel6Text = fixedLengthString(cntLabel6TextBox.Text, 28);
            sendDataToPort("$LaC6" + "" + cntLabel6Text + ";");

        }

        private void sendCntLabel7_Click_1(object sender, EventArgs e)
        {
            string cntLabel7Text = fixedLengthString(cntLabel7TextBox.Text, 28);
            sendDataToPort("$LaC7" + "" + cntLabel7Text + ";");
        }

        private void sendCntLabel8_Click_1(object sender, EventArgs e)
        {
            string cntLabel8Text = fixedLengthString(cntLabel8TextBox.Text, 28);
            sendDataToPort("$LaC8" + "" + cntLabel8Text + ";");
        }

        private void sendCntLabel9_Click_1(object sender, EventArgs e)
        {
            string cntLabel9Text = fixedLengthString(cntLabel9TextBox.Text, 28);
            sendDataToPort("$LaC9" + "" + cntLabel9Text + ";");
        }

        private void sendCntLabel10_Click_1(object sender, EventArgs e)
        {
            string cntLabel10Text = fixedLengthString(cntLabel10TextBox.Text, 28);
            sendDataToPort("$LaCA" + "" + cntLabel10Text + ";");
        }

        private void sendCntLabel11_Click_1(object sender, EventArgs e)
        {
            string cntLabel11Text = fixedLengthString(cntLabel11TextBox.Text, 28);
            sendDataToPort("$LaCB" + "" + cntLabel11Text + ";");
        }

        private void sendCntLabel12_Click_1(object sender, EventArgs e)
        {
            string cntLabel12Text = fixedLengthString(cntLabel12TextBox.Text, 28);
            sendDataToPort("$LaCC" + "" + cntLabel12Text + ";");
        }

        private void sendCntLabel13_Click_1(object sender, EventArgs e)
        {
            string cntLabel13Text = fixedLengthString(cntLabel13TextBox.Text, 28);
            sendDataToPort("$LaCD" + "" + cntLabel13Text + ";");
        }

        private void sendCntLabel14_Click_1(object sender, EventArgs e)
        {
            string cntLabel14Text = fixedLengthString(cntLabel14TextBox.Text, 28);
            sendDataToPort("$LaCE" + "" + cntLabel14Text + ";");
        }

        private void sendCntLabel15_Click_1(object sender, EventArgs e)
        {
            string cntLabel15Text = fixedLengthString(cntLabel15TextBox.Text, 28);
            sendDataToPort("$LaCF" + "" + cntLabel15Text + ";");
        }

        private void sendCntLabel16_Click_1(object sender, EventArgs e)
        {
            string cntLabel16Text = fixedLengthString(cntLabel16TextBox.Text, 28);
            sendDataToPort("$LaCG" + "" + cntLabel16Text + ";");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendDataToPort("$RESET" +""+ ";");
        }

        private string fixedLengthString(string textData , int lenght)
        {
            
            Console.WriteLine(string.Format("|{0}|", centeredString(textData, lenght)));

            string stringData = centeredString(textData, lenght).PadRight(lenght, ' ').Substring(0, lenght);
            return stringData;
        }

      
    

        private void saveAll_Click(object sender, EventArgs e)
        {

            if (recordFileName.Text.Trim() == "")

            {

                MessageBox.Show("Record Name Field cannnot be blank");

                recordFileName.Focus();
                return;
            }

            model.instName = institutionTextBox.Text;
            model.bankId = bankIdTextBox.Text;
            model.timeDate = timeTextBox.Text;
            model.counterName = counteTextBox.Text;
            model.totCounter = totalCounterTextBox.Text;
            model.copiesNo = copiePrintingTextBox.Text;
            model.closingTime = closingTimeTextBox.Text;
            model.tokenSlip9 = tokenSlip1TextBox.Text;
            model.tokenSlipA = tokenSlip2TextBox.Text;
            model.tokenSlipB = tokenSlipBTextBox.Text;
            model.cla1 = cntLabel1TextBox.Text;
            model.cla2 = cntLabel2TextBox.Text;
            model.cla3 = cntLabel3TextBox.Text;
            model.cla4 = cntLabel4TextBox.Text;
            model.cla5 = cntLabel5TextBox.Text;
            model.cla6 = cntLabel6TextBox.Text;
            model.cla7 = cntLabel7TextBox.Text;
            model.cla8 = cntLabel8TextBox.Text;
            model.cla9 = cntLabel9TextBox.Text;
            model.cla10 = cntLabel10TextBox.Text;
            model.cla11 = cntLabel11TextBox.Text;
            model.cla12 = cntLabel12TextBox.Text;
            model.cla13 = cntLabel13TextBox.Text;
            model.cla14 = cntLabel14TextBox.Text;
            model.cla15 = cntLabel15TextBox.Text;
            model.cla16 = cntLabel16TextBox.Text;
            model.recordFileName = recordFileName.Text.Trim();

            if (qmsComboBox.SelectedIndex > 0)
            {
                updateDB(model);
                AutoClosingMessageBox.Show("Data Updated Successfully", "Update", 1000);
            }
            else
            {

                insertData(model);
                
            }


            Fillcombobox();

        }

        public void insertData(Model modelData)
        {
            m_dbConnection.Open();

            SQLiteCommand selectSQL = new SQLiteCommand("SELECT count(*) FROM qmsutility WHERE recordFileName='"+recordFileName.Text.Trim()+"'", m_dbConnection);

            int count = Convert.ToInt32(selectSQL.ExecuteScalar());

            if (count == 0)
            {
                SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO qmsutility (instName, bankId, timeDate, counterName, totCounter," +
                "copiesNo, closingTime, tokenSlip9, tokenSlipA, tokenSlipB, cla1, cla2, cla3, cla4, cla5, cla6,cla7,cla8,cla9,cla10," +
                "cla11, cla12,cla13,cla14,cla15,cla16,recordFileName) VALUES (@instName, @bankId, @timeDate, @counterName, @totCounter,@copiesNo, " +
                "@closingTime, @tokenSlip9, @tokenSlipA, @tokenSlipB, @cla1, @cla2, @cla3, @cla4," +
                " @cla5, @cla6, @cla7, @cla8, @cla9, @cla10, @cla11, @cla12, @cla13, @cla14, @cla15, @cla16, @recordFileName" +
                ")", m_dbConnection);



                insertSQL.Parameters.AddWithValue("@instName", modelData.instName);
                insertSQL.Parameters.AddWithValue("@bankId", modelData.bankId);
                insertSQL.Parameters.AddWithValue("@timeDate", modelData.timeDate);

                insertSQL.Parameters.AddWithValue("@counterName", modelData.counterName);
                insertSQL.Parameters.AddWithValue("@totCounter", modelData.totCounter);
                insertSQL.Parameters.AddWithValue("@copiesNo", modelData.copiesNo);

                insertSQL.Parameters.AddWithValue("@closingTime", modelData.closingTime);
                insertSQL.Parameters.AddWithValue("@tokenSlip9", modelData.tokenSlip9);
                insertSQL.Parameters.AddWithValue("@tokenSlipA", modelData.tokenSlipA);

                insertSQL.Parameters.AddWithValue("@tokenSlipB", modelData.tokenSlipB);


                insertSQL.Parameters.AddWithValue("@cla1", modelData.cla1);
                insertSQL.Parameters.AddWithValue("@cla2", modelData.cla2);

                insertSQL.Parameters.AddWithValue("@cla3", modelData.cla3);
                insertSQL.Parameters.AddWithValue("@cla4", modelData.cla4);
                insertSQL.Parameters.AddWithValue("@cla5", modelData.cla5);

                insertSQL.Parameters.AddWithValue("@cla6", modelData.cla6);
                insertSQL.Parameters.AddWithValue("@cla7", modelData.cla7);
                insertSQL.Parameters.AddWithValue("@cla8", modelData.cla8);

                insertSQL.Parameters.AddWithValue("@cla9", modelData.cla9);
                insertSQL.Parameters.AddWithValue("@cla10", modelData.cla10);
                insertSQL.Parameters.AddWithValue("@cla11", modelData.cla11);

                insertSQL.Parameters.AddWithValue("@cla12", modelData.cla12);
                insertSQL.Parameters.AddWithValue("@cla13", modelData.cla13);
                insertSQL.Parameters.AddWithValue("@cla14", modelData.cla14);

                insertSQL.Parameters.AddWithValue("@cla15", modelData.cla15);
                insertSQL.Parameters.AddWithValue("@cla16", modelData.cla16);
                insertSQL.Parameters.AddWithValue("@recordFileName", modelData.recordFileName);


                try
                {
                    insertSQL.ExecuteNonQuery();

                    AutoClosingMessageBox.Show("Data Inserted Successfully", "Insert", 1000);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else {
                AutoClosingMessageBox.Show("Record Name Already Exist", "Duplicate Data", 1000);
            }
            m_dbConnection.Close();
        }

        void Fillcombobox()
            {

                m_dbConnection.Open();
                SQLiteCommand cmd = new SQLiteCommand("select ID, recordFileName From qmsutility", m_dbConnection);
                SQLiteDataReader Sdr = cmd.ExecuteReader();
                while (Sdr.Read())
                {
                    /* userListDictionary.Add(Convert.ToInt32(Sdr["ID"].ToString()), Sdr["recordFileName"].ToString());
                    qmsComboBox.DataSource = new BindingSource(userListDictionary, null);
                    qmsComboBox.DisplayMember = "Value";
                    qmsComboBox.ValueMember = "Key"; */

                    if (!qmsComboBox.Items.Contains(Sdr["recordFileName"].ToString()))
                    {
                        qmsComboBox.Items.Add(Sdr["recordFileName"].ToString());
                    }
                }
                Sdr.Close();
                m_dbConnection.Close();
            
        }

        public void createDB()
        {

            if (!File.Exists("MyDatabase.sqlite"))
            {

                SQLiteConnection.CreateFile("MyDatabase.sqlite");

                m_dbConnection.Open();

                string sql = "create table qmsutility (ID INTEGER PRIMARY KEY   AUTOINCREMENT, instName varchar(20), bankId varchar(20), " +
                    "timeDate varchar(20), counterName varchar(20),totCounter varchar(20), copiesNo varchar(20),closingTime varchar(20), " +
                    "tokenSlip9 varchar(20),tokenSlipA varchar(20), tokenSlipB varchar(20)," +
                    "cla1 varchar(20), cla2 varchar(20),cla3 varchar(20), cla4 varchar(20)," +
                    "cla5 varchar(20), cla6 varchar(20),cla7 varchar(20), cla8 varchar(20)," +
                    "cla9 varchar(20), cla10 varchar(20),cla11 varchar(20), cla12 varchar(20)," +
                    "cla13 varchar(20), cla14 varchar(20),cla15 varchar(20), cla16 varchar(20), recordFileName varchar(60)  NOT NULL UNIQUE)";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                command.ExecuteNonQuery();

                m_dbConnection.Close();
            }


        }

        private void qmsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string value1 =  qmsComboBox.SelectedItem.ToString();
            //string[] tokens = value1.Split(',');
            //string[] keyValue = tokens[0].Split('[');
            //string[] lines = Regex.Split(value1, ",");
            
           // MessageBox.Show(lines[0].ToString());

            if (qmsComboBox.SelectedIndex > 0)
            {
                m_dbConnection.Open();
                SQLiteCommand cmd = new SQLiteCommand("select * From qmsutility where recordFileName ='" + qmsComboBox.SelectedItem.ToString() + "'", m_dbConnection);
                SQLiteDataReader Sdr = cmd.ExecuteReader();
                while (Sdr.Read())
                {
                    institutionTextBox.Text = Sdr["instName"].ToString();
                    bankIdTextBox.Text = Sdr["bankId"].ToString();
                    timeTextBox.Text = Sdr["timeDate"].ToString();
                    counteTextBox.Text = Sdr["counterName"].ToString();
                    totalCounterTextBox.Text = Sdr["totCounter"].ToString();
                    copiePrintingTextBox.Text = Sdr["copiesNo"].ToString();
                    closingTimeTextBox.Text = Sdr["closingTime"].ToString();
                    tokenSlip1TextBox.Text = Sdr["tokenSlip9"].ToString();
                    tokenSlip2TextBox.Text = Sdr["tokenSlipA"].ToString();
                    tokenSlipBTextBox.Text = Sdr["tokenSlipB"].ToString();

                    cntLabel1TextBox.Text = Sdr["cla1"].ToString();
                    cntLabel2TextBox.Text = Sdr["cla2"].ToString();
                    cntLabel3TextBox.Text = Sdr["cla3"].ToString();
                    cntLabel4TextBox.Text = Sdr["cla4"].ToString();
                    cntLabel5TextBox.Text = Sdr["cla5"].ToString();
                    cntLabel6TextBox.Text = Sdr["cla6"].ToString();
                    cntLabel7TextBox.Text = Sdr["cla7"].ToString();
                    cntLabel8TextBox.Text = Sdr["cla8"].ToString();
                    cntLabel9TextBox.Text = Sdr["cla9"].ToString();
                    cntLabel10TextBox.Text = Sdr["cla10"].ToString();
                    cntLabel11TextBox.Text = Sdr["cla11"].ToString();
                    cntLabel12TextBox.Text = Sdr["cla12"].ToString();
                    cntLabel13TextBox.Text = Sdr["cla13"].ToString();
                    cntLabel14TextBox.Text = Sdr["cla14"].ToString();
                    cntLabel15TextBox.Text = Sdr["cla15"].ToString();
                    cntLabel16TextBox.Text = Sdr["cla16"].ToString();
                    recordFileName.Text = Sdr["recordFileName"].ToString();

                }
                Sdr.Close();
                m_dbConnection.Close();
            }
            else
            {
                institutionTextBox.Text = "";
                bankIdTextBox.Text = "";
                timeTextBox.Text = "";
                counteTextBox.Text = "";
                totalCounterTextBox.Text = "";
                copiePrintingTextBox.Text = "";
                closingTimeTextBox.Text = "";
                tokenSlip1TextBox.Text = "";
                tokenSlip2TextBox.Text = "";
                tokenSlipBTextBox.Text = "";

                cntLabel1TextBox.Text = "";
                cntLabel2TextBox.Text = "";
                cntLabel3TextBox.Text = "";
                cntLabel4TextBox.Text = "";
                cntLabel5TextBox.Text = "";
                cntLabel6TextBox.Text = "";
                cntLabel7TextBox.Text = "";
                cntLabel8TextBox.Text = "";
                cntLabel9TextBox.Text = "";
                cntLabel10TextBox.Text = "";
                cntLabel11TextBox.Text = "";
                cntLabel12TextBox.Text = "";
                cntLabel13TextBox.Text = "";
                cntLabel14TextBox.Text = "";
                cntLabel15TextBox.Text = "";
                cntLabel16TextBox.Text ="";
                recordFileName.Text= "";
                


            }
        }

        public void updateDB(Model modelData)
        {
            m_dbConnection.Open();
          
            string sql_update = "UPDATE qmsutility SET instName = @instName, bankId = @bankId," +
                "timeDate = @timeDate," +
                "counterName = @counterName," +
                "totCounter = @totCounter, copiesNo = @copiesNo," +
                "closingTime = @closingTime, tokenSlip9 = @tokenSlip9," +
                "tokenSlipA = @tokenSlipA, tokenSlipB = @tokenSlipB," +
                "cla1 = @cla1, cla2 = @cla2," +
                "cla3 = @cla3, cla4 = @cla4," +
                "cla5 = @cla5, cla6 = @cla6," +
                "cla7 = @cla7, cla8 = @cla8," +
                "cla9 = @cla9, cla10 = @cla10," +
                "cla11 = @cla11, cla12 = @cla12," +
                "cla13 = @cla13, cla14 = @cla14," +
                "cla15 = @cla15, cla16 = @cla16, recordFileName=@recordFileName Where recordFileName = @ID";


            SQLiteCommand command = new SQLiteCommand(sql_update, m_dbConnection);

            

            command.Parameters.AddWithValue("@instName", modelData.instName);
            command.Parameters.AddWithValue("@bankId", modelData.bankId);
            command.Parameters.AddWithValue("@timeDate", modelData.timeDate);

            command.Parameters.AddWithValue("@counterName", modelData.counterName);
            command.Parameters.AddWithValue("@totCounter", modelData.totCounter);
            command.Parameters.AddWithValue("@copiesNo", modelData.copiesNo);

            command.Parameters.AddWithValue("@closingTime", modelData.closingTime);
            command.Parameters.AddWithValue("@tokenSlip9", modelData.tokenSlip9);
            command.Parameters.AddWithValue("@tokenSlipA", modelData.tokenSlipA);

            command.Parameters.AddWithValue("@tokenSlipB", modelData.tokenSlipB);


            command.Parameters.AddWithValue("@cla1", modelData.cla1);
            command.Parameters.AddWithValue("@cla2", modelData.cla2);

            command.Parameters.AddWithValue("@cla3", modelData.cla3);
            command.Parameters.AddWithValue("@cla4", modelData.cla4);
            command.Parameters.AddWithValue("@cla5", modelData.cla5);

            command.Parameters.AddWithValue("@cla6", modelData.cla6);
            command.Parameters.AddWithValue("@cla7", modelData.cla7);
            command.Parameters.AddWithValue("@cla8", modelData.cla8);

            command.Parameters.AddWithValue("@cla9", modelData.cla9);
            command.Parameters.AddWithValue("@cla10", modelData.cla10);
            command.Parameters.AddWithValue("@cla11", modelData.cla11);

            command.Parameters.AddWithValue("@cla12", modelData.cla12);
            command.Parameters.AddWithValue("@cla13", modelData.cla13);
            command.Parameters.AddWithValue("@cla14", modelData.cla14);

            command.Parameters.AddWithValue("@cla15", modelData.cla15);
            command.Parameters.AddWithValue("@cla16", modelData.cla16);
            command.Parameters.AddWithValue("@recordFileName", modelData.recordFileName);
            command.Parameters.AddWithValue("@ID", qmsComboBox.SelectedItem);

            try
            {
                command.ExecuteNonQuery();

                m_dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }


        public static Label SetPlaceholder(Control control, string text)
        {
            var placeholder = new Label
            {
                Text = text,
                Font = control.Font,
                ForeColor = Color.Gray,
                BackColor = Color.Transparent,
                Cursor = Cursors.IBeam,
                Margin = Padding.Empty,

                //get rid of the left margin that all labels have
                FlatStyle = FlatStyle.System,
                AutoSize = false,

                //Leave 1px on the left so we can see the blinking cursor
                Size = new Size(control.Size.Width - 1, control.Size.Height),
                Location = new Point(control.Location.X + 1, control.Location.Y)
            };

            //when clicking on the label, pass focus to the control
            placeholder.Click += (sender, args) => { control.Focus(); };

            //disappear when the user starts typing
            control.TextChanged += (sender, args) => {
                placeholder.Visible = string.IsNullOrEmpty(control.Text);
            };

            //stay the same size/location as the control
            EventHandler updateSize = (sender, args) => {
                placeholder.Location = new Point(control.Location.X + 1, control.Location.Y);
                placeholder.Size = new Size(control.Size.Width - 1, control.Size.Height);
            };

            control.SizeChanged += updateSize;
            control.LocationChanged += updateSize;

            control.Parent.Controls.Add(placeholder);
            placeholder.BringToFront();

            return placeholder;
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }


        public void PrintPage1(object sender, PrintPageEventArgs e)
        {
            String dateString = DateTime.Now.ToString("dd/MM/yy").Replace('-', '/');
            string drawString = "\n" + "Counter No.: " + cntLabel1TextBox.Text + "\n" + "****************************" + "\n" + institutionTextBox.Text +
               "\n" + "****************************" + "\n" + "TIME" + "               " + "DATE" + "\n" + DateTime.Now.ToString("HH:mm:ss") + "        " + dateString + "\n" + tokenSlip1TextBox.Text + "\n"
               + tokenSlip2TextBox.Text + "\n" + tokenSlipBTextBox.Text;
            // Create string to draw.
            string textTitle = "   TOKEN No 07";

            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;//center-align vertically
            drawFormat.Alignment = StringAlignment.Center; //center-align horizontally
            drawFormat.FormatFlags = StringFormatFlags.FitBlackBox;
            

           
            Font drawTitleFont = new Font("Arial Black", 60, FontStyle.Regular);

            // Create font and brush.
            Font drawFont = new Font("Arial Black", 40, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create rectangle for drawing.
            float x = 0.0F;
            float y = 0.0F;
            float width = 1170.0F;
            float height = 850.0F;
            RectangleF drawRect = new RectangleF(x, y, width, height);
            RectangleF drawRectq = new RectangleF(x, y, width, height);
            // Draw rectangle to screen.
            Pen blackPen = new Pen(Color.Black);

            e.Graphics.Clear(Color.White);
            e.Graphics.DrawRectangle(blackPen, x, y, width, height);

            e.Graphics.DrawString(textTitle, drawTitleFont, Brushes.Black, drawRectq);
            // Draw string to screen.
            // e.Graphics.DrawString(textTitle, drawTitleFont, drawBrush, drawRect, drawFormat);
            // Draw string to screen.
            e.Graphics.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat);
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;


            String dateString = DateTime.Now.ToString("dd/MM/yy").Replace('-', '/');

        

            string text =   cntLabel1TextBox.Text+"\n"+ counteTextBox.Text + " No.: " +"\n"+"****************************"+"\n" + institutionTextBox.Text +
                "\n"+ "****************************" + "\n"+"TIME"+"               "+"DATE"+"\n"+ DateTime.Now.ToString("HH:mm:ss") + "        "+dateString+"\n"+ tokenSlip1TextBox.Text+"\n"
                + tokenSlip2TextBox.Text+"\n"+ tokenSlipBTextBox.Text;
            /* e.Graphics.DrawString(text, new Font("Georgia", 30, FontStyle.Bold),
             Brushes.Black, 150, 150);*/
            string textTitle = "TOKEN 07";

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;//center-align vertically
            sf.Alignment = StringAlignment.Center; //center-align horizontally
            sf.FormatFlags = StringFormatFlags.FitBlackBox;

            Font drawFont = new Font("Arial Black", 30F);
            Font drawTitleFont = new Font("Arial Black", 70);
            
            e.Graphics.Clear(Color.White);
              
            // Sets the value of charactersOnPage to the number of characters 
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(text, drawFont,
                e.MarginBounds.Size, sf,
                out charactersOnPage, out linesPerPage);


           e.Graphics.DrawString(textTitle, drawTitleFont, Brushes.Black, 550, 65, sf);
           // e.Graphics.DrawString(text, drawFont, Brushes.Black, 570, 420, sf);
            // e.Graphics.DrawString(text, drawFont, Brushes.Maroon, 570, 360, sf);

            // Set up string. 

           // Font stringFont = new Font("Arial", 16);

            // Measure string.
          //  SizeF stringSize = new SizeF();
          //  stringSize = e.Graphics.MeasureString(text, drawFont);

            // Draw rectangle representing size of string.
            // e.Graphics.DrawRectangle(new Pen(Color.Red, 1), 570, 0, stringSize.Width, stringSize.Height);

            // Draw string to screen.
            e.Graphics.DrawString(text, drawFont, Brushes.Black, e.MarginBounds, sf);
           // e.Graphics.DrawString(text, drawFont, Brushes.Black, 570, 360, sf);

            //DateTime.Now.ToString("HH: mm: ss tt") #Example: 5:42:12 PM
        }

       


        private Font GetCorrectFont(Graphics graphic, String text, Size maxStringSize, Font labelFont)
        {
            //based on the Label string,we need to vary font size 
            //current width the text string
            SizeF sizeStr = graphic.MeasureString(text, labelFont);
            Font fontStr = new Font(labelFont.Name, labelFont.Size);
            while (sizeStr.Width > maxStringSize.Width)
            {
                //adjust the font size based on width ratio
                float wRatio = (maxStringSize.Width) / sizeStr.Width;
                //reduce the font size
                float newSize = (int)(fontStr.Size * wRatio);
                //this creates a new font with given fontfamily name
                fontStr = new Font(labelFont.Name, newSize);
                sizeStr = graphic.MeasureString(text, fontStr);
            }
            return fontStr;
        }


        private void printPreview_Click(object sender, EventArgs e)
        {
            CreatePrintPreviewControl();
        }


        private void CreatePrintPreviewControl()
        {
           // ppc = new PrintPreviewControl();
           // ppc.Name = "PrintPreviewControl1";
           // ppc.Dock = DockStyle.Fill;
           // ppc.Location = new Point(88, 80);

            printPreviewControl1.Document = docToPrint;
           // printPreviewControl1.AutoZoom = false;

           // PrintDialog printdlg = new PrintDialog();
           // PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
           // ppc.Zoom = .15;
           //ppc.Document.DocumentName = "c:\\";
           //   ppc.UseAntiAlias = true;

            // Add PrintPreviewControl to Form
            // Controls.Add(this.ppc);

            // Add PrintDocument PrintPage event handler
            // docToPrint.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("MyPaper", 0, 0);
            docToPrint.DefaultPageSettings.Landscape = true;
            docToPrint.DefaultPageSettings.PaperSize = new PaperSize("MyPaper", 900, 1100);

            docToPrint.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(PrintPage);

          //  printPrvDlg.Document = docToPrint;
           // printPrvDlg.ShowDialog(); // this shows the preview and then show the Printer Dlg below

         //   printdlg.Document = docToPrint;

          //  printdlg.ShowDialog();// Printing directly
        }

        

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void copiesSend_Click(object sender, EventArgs e)
        {
            string copiePrintingText = fixedLengthString(copiePrintingTextBox.Text, 2);
            sendDataToPort("$COPY" + "" + copiePrintingText + ";");
        }
      

        private void bankIdTextBox_TextChanged_1(object sender, EventArgs e)
        {
           
            if (bankIdTextBox.Text.ToString().Length != 0)
            {
                if (bankIdTextBox.Text.Length == 2)
                {
                    //   MessageBox.Show("The maximum amount in text box cant be more than 2");
                    sendBankID.Enabled = true;
              
                }
                else
                {
                    sendBankID.Enabled = false;
                }
            }
           

        }

        private void copiePrintingTextBox_TextChanged(object sender, EventArgs e)
        {
            if (copiePrintingTextBox.Text.ToString().Length != 0)
            {
                if (copiePrintingTextBox.Text.Length == 2)
                {
                    //   MessageBox.Show("The maximum amount in text box cant be more than 2");
                    copiesSend.Enabled = true;

                }
                else
                {
                    copiesSend.Enabled = false;
                }
            }

        }

        private void closingTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (closingTimeTextBox.Text.ToString().Length != 0)
            {
                if (closingTimeTextBox.Text.Length == 4)
                {
                    //   MessageBox.Show("The maximum amount in text box cant be more than 2");
                    closingTimeSend.Enabled = true;

                }
                else
                {
                    closingTimeSend.Enabled = false;
                }
            }
        }

        private void totalCounterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (totalCounterTextBox.Text.ToString().Length != 0)
            {
                if (totalCounterTextBox.Text.Length == 4)
                {
                    //   MessageBox.Show("The maximum amount in text box cant be more than 2");
                    sendCounterNo.Enabled = true;

                }
                else
                {
                    sendCounterNo.Enabled = false;
                }
            }
        }

        static string centeredString(string s, int width)
        {
            if (s.Length >= width)
            {
                return s;
            }

            int leftPadding = (width - s.Length) / 2;
            int rightPadding = width - s.Length - leftPadding;

            return new string(' ', leftPadding) + s + new string(' ', rightPadding);
        }
    }
}
