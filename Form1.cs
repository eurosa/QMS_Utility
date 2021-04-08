using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMS_Utility
{
    public partial class Form1 : Form
    {
        Model model;
        SQLiteConnection m_dbConnection;
        public Form1()
        {
            InitializeComponent();
            // form2 = new Form2(this);
            model = new Model();
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
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
                groupBox1.Enabled = false;
                

                }
             }
              // Call this function to close the port.
        private void disconnect()
            {
            ComPort.Close();
            btnConnect.Text = "Connect";
            //btnSend.Enabled = false;
            groupBox1.Enabled = true;
            
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

        private void sendName_Click(object sender, EventArgs e)
        {
            string institutionText = fixedLengthString(institutionTextBox.Text, 28);
            sendDataToPort("$BnkL"+ institutionText + ";");

            
        }

        private void sendBankID_Click(object sender, EventArgs e)
        {
            string bankIdText = fixedLengthString(bankIdTextBox.Text, 28);
            sendDataToPort("$BnkL" + "" + bankIdText + ";");
            
        }

        private void sendTime_Click(object sender, EventArgs e)
        {
            string timeTex = fixedLengthString(timeTextBox.Text, 28);
            sendDataToPort("$TIME" + "" + timeTex + ";");
        }

        private void sendCounter_Click(object sender, EventArgs e)
        {
            string counteText = fixedLengthString(counteTextBox.Text, 28);
            sendDataToPort("$CTID" + "" + counteText + ";");
        }

        private void sendCounterNo_Click(object sender, EventArgs e)
        {
            string totalCounterText = fixedLengthString(totalCounterTextBox.Text, 28);
            sendDataToPort("$CNTR" + "" + totalCounterText + ";");
        }

        private void copiesSend_Click(object sender, EventArgs e)
        {

        }

        private void closingTimeSend_Click(object sender, EventArgs e)
        {
            string closingTimeText = fixedLengthString(closingTimeTextBox.Text, 28);
            sendDataToPort("$CLTM" + "" + closingTimeText + ";");
        }

        private void sendToken1_Click(object sender, EventArgs e)
        {
            string tokenSlip1Text = fixedLengthString(tokenSlip1TextBox.Text, 28);
            sendDataToPort("$TSL9" + "" + tokenSlip1Text + ";");
        }

        private void sendToken2_Click(object sender, EventArgs e)
        {
            string tokenSlip2Text = fixedLengthString(tokenSlip2TextBox.Text, 28);
            sendDataToPort("$TSLA" + "" + tokenSlip2Text + ";");
        }

        private void sendToken3_Click(object sender, EventArgs e)
        {
            string tokenSlipBText = fixedLengthString(tokenSlipBTextBox.Text, 28);
            sendDataToPort("$TSLB" + "" + tokenSlipBText + ";");
        }

        private void tokenSlip1TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void sendCntLabel1_Click(object sender, EventArgs e)
        {
            string cntLabel1Text = fixedLengthString(cntLabel1TextBox.Text, 28);
            sendDataToPort("$LaC1" + "" + cntLabel1Text + ";");
        }

        private void sendCntLabel2_Click(object sender, EventArgs e)
        {
            string cntLabel2Text = fixedLengthString(cntLabel2TextBox.Text, 28);
            sendDataToPort("$LaC2" + "" + cntLabel2Text + ";");
        }

        private void sendCntLabel3_Click(object sender, EventArgs e)
        {
            string cntLabel3Text = fixedLengthString(cntLabel3TextBox.Text, 28);
            sendDataToPort("$LaC3" + "" + cntLabel3Text + ";");
        }

        private void sendCntLabel4_Click(object sender, EventArgs e)
        {
            string cntLabel4Text = fixedLengthString(cntLabel4TextBox.Text, 28);
            sendDataToPort("$LaC4" + "" + cntLabel4Text + ";");
        }

        private void sendCntLabel5_Click(object sender, EventArgs e)
        {
            string cntLabel5Text = fixedLengthString(cntLabel5TextBox.Text, 28);
            sendDataToPort("$LaC5" + "" + cntLabel5Text + ";");
        }

        private void sendCntLabel6_Click(object sender, EventArgs e)
        {
            string cntLabel6Text = fixedLengthString(cntLabel6TextBox.Text, 28);
            sendDataToPort("$LaC6" + "" + cntLabel6Text + ";");

        }

        private void sendCntLabel7_Click(object sender, EventArgs e)
        {
            string cntLabel7Text = fixedLengthString(cntLabel7TextBox.Text, 28);
            sendDataToPort("$LaC7" + "" + cntLabel7Text + ";");
        }

        private void sendCntLabel8_Click(object sender, EventArgs e)
        {
            string cntLabel8Text = fixedLengthString(cntLabel8TextBox.Text, 28);
            sendDataToPort("$LaC8" + "" + cntLabel8Text + ";");
        }

        private void sendCntLabel9_Click(object sender, EventArgs e)
        {
            string cntLabel9Text = fixedLengthString(cntLabel9TextBox.Text, 28);
            sendDataToPort("$LaC9" + "" + cntLabel9Text + ";");
        }

        private void sendCntLabel10_Click(object sender, EventArgs e)
        {
            string cntLabel10Text = fixedLengthString(cntLabel10TextBox.Text, 28);
            sendDataToPort("$LaCA" + "" + cntLabel10Text + ";");
        }

        private void sendCntLabel11_Click(object sender, EventArgs e)
        {
            string cntLabel11Text = fixedLengthString(cntLabel11TextBox.Text, 28);
            sendDataToPort("$LaCB" + "" + cntLabel11Text + ";");
        }

        private void sendCntLabel12_Click(object sender, EventArgs e)
        {
            string cntLabel12Text = fixedLengthString(cntLabel12TextBox.Text, 28);
            sendDataToPort("$LaCC" + "" + cntLabel12Text + ";");
        }

        private void sendCntLabel13_Click(object sender, EventArgs e)
        {
            string cntLabel13Text = fixedLengthString(cntLabel13TextBox.Text, 28);
            sendDataToPort("$LaCD" + "" + cntLabel13Text + ";");
        }

        private void sendCntLabel14_Click(object sender, EventArgs e)
        {
            string cntLabel14Text = fixedLengthString(cntLabel14TextBox.Text, 28);
            sendDataToPort("$LaCE" + "" + cntLabel14Text + ";");
        }

        private void sendCntLabel15_Click(object sender, EventArgs e)
        {
            string cntLabel15Text = fixedLengthString(cntLabel15TextBox.Text, 28);
            sendDataToPort("$LaCF" + "" + cntLabel15Text + ";");
        }

        private void sendCntLabel16_Click(object sender, EventArgs e)
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
            string stringData = textData.PadRight(lenght, ' ').Substring(0, lenght);
            return stringData;
        }

        private void bankIdTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void createDB()
        {

            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");

            m_dbConnection.Open();

            string sql = "create table highscores (name varchar(20), score int)";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('Me', 9001)";

            command = new SQLiteCommand(sql, m_dbConnection);

            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        private void saveAll_Click(object sender, EventArgs e)
        {
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
                    "cla13 varchar(20), cla14 varchar(20),cla15 varchar(20), cla16 varchar(20))";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

                command.ExecuteNonQuery();

                m_dbConnection.Close();
            }

            Create(model);
             /*   m_dbConnection.Open();

            string sql1 = "insert into qmsutility (name, score) values ('Ranj', 9001)";

            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            command1.ExecuteNonQuery();

            m_dbConnection.Close();*/

        }

        public void Create(Model modelData)
        {
            m_dbConnection.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO qmsutility (instName, bankId, timeDate, counterName, totCounter," +
                "copiesNo, closingTime, tokenSlip9, tokenSlipA, tokenSlipB, cla1, cla2, cla3, cla4, cla5, cla6,cla7,cla8,cla9,cla10," +
                "cla11, cla12,cla13,cla14,cla15,cla16) VALUES (@instName, @bankId, @timeDate, @counterName, @totCounter,@copiesNo, " +
                "@closingTime, @tokenSlip9, @tokenSlipA, @tokenSlipB, @cla1, @cla2, @cla3, @cla4," +
                " @cla5, @cla6, @cla7, @cla8, @cla9, @cla10, @cla11, @cla12, @cla13, @cla14, @cla15, @cla16" +
                ")", m_dbConnection);

            // insertSQL.Parameters.Add("@instName", SqlDbType.Int).Value = klantId;
            // insertSQL.Parameters.Add("@bankId", SqlDbType.VarChar, 20).Value = modelData.instName;
            // insertSQL.Parameters.Add("@bankId", SqlDbType.VarChar, 20).Value = klantVoornaam;

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
            

            try
            {
                insertSQL.ExecuteNonQuery();
                m_dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
