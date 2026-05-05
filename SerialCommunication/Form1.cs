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
using System.Windows.Forms.VisualStyles;

namespace SerialCommunication
{
    public partial class Form1 : Form
    {
        private SerialPort serialPortArduino;
        private System.Windows.Forms.Timer timerOefening3;

        public Form1()
        {
            InitializeComponent();

            // initialize serial port object with timeouts
            serialPortArduino = new SerialPort();
            serialPortArduino.ReadTimeout = 1000;
            serialPortArduino.WriteTimeout = 1000;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();
                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;

                comboBoxBaudrate.SelectedIndex = comboBoxBaudrate.Items.IndexOf("115200");
                // attach checked handlers for digital2, digital3 and digital4
                this.checkBoxDigital2.CheckedChanged += new System.EventHandler(this.checkBoxDigital2_CheckedChanged);
                this.checkBoxDigital3.CheckedChanged += new System.EventHandler(this.checkBoxDigital3_CheckedChanged);
                this.checkBoxDigital4.CheckedChanged += new System.EventHandler(this.checkBoxDigital4_CheckedChanged);
                this.trackBarPWM9.Scroll += new System.EventHandler(this.trackBarPWM9_Scroll);
                // timer for oefening3
                timerOefening3 = new System.Windows.Forms.Timer();
                timerOefening3.Interval = 1000;
                timerOefening3.Tick += new System.EventHandler(this.timerOefening3_Tick);

                // tab control change to enable/disable timer
                this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
                this.trackBarPWM10.Scroll += new System.EventHandler(this.trackBarPWM10_Scroll);
                this.trackBarPWM11.Scroll += new System.EventHandler(this.trackBarPWM11_Scroll);
            }
            catch (Exception)
            { }
        }

        private void checkBoxDigital2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Geen open seriële verbinding.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cmd = checkBoxDigital2.Checked ? "set d2 high" : "set d2 low";
                serialPortArduino.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij verzenden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxDigital3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Geen open seriële verbinding.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cmd = checkBoxDigital3.Checked ? "set d3 high" : "set d3 low";
                serialPortArduino.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij verzenden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxDigital4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Geen open seriële verbinding.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cmd = checkBoxDigital4.Checked ? "set d4 high" : "set d4 low";
                serialPortArduino.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij verzenden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBarPWM9_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Geen open seriële verbinding.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cmd = $"set pwm9 {trackBarPWM9.Value}";
                serialPortArduino.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij verzenden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBarPWM10_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Geen open seriële verbinding.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cmd = $"set pwm10 {trackBarPWM10.Value}";
                serialPortArduino.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij verzenden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBarPWM11_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Geen open seriële verbinding.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string cmd = $"set pwm11 {trackBarPWM11.Value}";
                serialPortArduino.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij verzenden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxDigital10_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Geen open seriële verbinding.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cb = sender as CheckBox;
                if (cb == null) return;
                string cmd = cb.Checked ? "set d10 high" : "set d10 low";
                serialPortArduino.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij verzenden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxDigital11_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    MessageBox.Show("Geen open seriële verbinding.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cb = sender as CheckBox;
                if (cb == null) return;
                string cmd = cb.Checked ? "set d11 high" : "set d11 low";
                serialPortArduino.WriteLine(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij verzenden: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // enable timer only when oefening3 tab is selected
            try
            {
                if (tabControl.SelectedTab == tabPageOefening3)
                    timerOefening3.Enabled = true;
                else
                    timerOefening3.Enabled = false;
            }
            catch (Exception)
            { }
        }

        private void cboPoort_DropDown(object sender, EventArgs e)
        {
            try
            {
                string selected = (string)comboBoxPoort.SelectedItem;
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();

                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);

                comboBoxPoort.SelectedIndex = comboBoxPoort.Items.IndexOf(selected);
            }
            catch (Exception)
            {
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;
            }
        }

        private void timerOefening3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino == null || !serialPortArduino.IsOpen)
                {
                    // nothing to do if not connected
                    return;
                }

                // clear any pending data
                try { var _ = serialPortArduino.ReadExisting(); } catch { }

                // digital5
                serialPortArduino.WriteLine("get d5");
                string resp5 = serialPortArduino.ReadLine().Trim();
                string val5 = resp5;
                if (resp5.Contains(":")) val5 = resp5.Split(':').Last().Trim();
                else if (resp5.Contains(" ")) val5 = resp5.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                radioButtonDigital5.Checked = (val5 == "1");

                // digital6
                serialPortArduino.WriteLine("get d6");
                string resp6 = serialPortArduino.ReadLine().Trim();
                string val6 = resp6;
                if (resp6.Contains(":")) val6 = resp6.Split(':').Last().Trim();
                else if (resp6.Contains(" ")) val6 = resp6.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                radioButtonDigital6.Checked = (val6 == "1");

                // digital7
                serialPortArduino.WriteLine("get d7");
                string resp7 = serialPortArduino.ReadLine().Trim();
                string val7 = resp7;
                if (resp7.Contains(":")) val7 = resp7.Split(':').Last().Trim();
                else if (resp7.Contains(" ")) val7 = resp7.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
                radioButtonDigital7.Checked = (val7 == "1");
            }
            catch (TimeoutException)
            {
                // ignore timeouts silently
            }
            catch (Exception ex)
            {
                // show other errors
                MessageBox.Show("Fout tijdens statuspolling: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino != null && serialPortArduino.IsOpen)
                {
                    // close existing connection
                    serialPortArduino.Close();
                    buttonConnect.Text = "Connect";
                    radioButtonVerbonden.Checked = false;
                    labelStatus.Text = "Niet verbonden";
                }
                else
                {
                    // set port and baud from UI (other properties set in code later)
                    if (comboBoxPoort.SelectedItem != null)
                        serialPortArduino.PortName = comboBoxPoort.SelectedItem.ToString();

                    if (comboBoxBaudrate.SelectedItem != null)
                    {
                        int baud;
                        if (int.TryParse(comboBoxBaudrate.SelectedItem.ToString(), out baud))
                            serialPortArduino.BaudRate = baud;
                    }

                    // data bits from numeric control
                    serialPortArduino.DataBits = (int)numericUpDownDatabits.Value;

                    // parity from radio buttons
                    if (radioButtonParityNone.Checked)
                        serialPortArduino.Parity = Parity.None;
                    else if (radioButtonParityEven.Checked)
                        serialPortArduino.Parity = Parity.Even;
                    else if (radioButtonParityOdd.Checked)
                        serialPortArduino.Parity = Parity.Odd;
                    else if (radioButtonParityMark.Checked)
                        serialPortArduino.Parity = Parity.Mark;
                    else if (radioButtonParitySpace.Checked)
                        serialPortArduino.Parity = Parity.Space;

                    // stop bits from radio buttons
                    if (radioButtonStopbitsNone.Checked)
                        serialPortArduino.StopBits = StopBits.None;
                    else if (radioButtonStopbitsOne.Checked)
                        serialPortArduino.StopBits = StopBits.One;
                    else if (radioButtonStopbitsOnePointFive.Checked)
                        serialPortArduino.StopBits = StopBits.OnePointFive;
                    else if (radioButtonStopbitsTwo.Checked)
                        serialPortArduino.StopBits = StopBits.Two;

                    // handshake from radio buttons
                    if (radioButtonHandshakeNone.Checked)
                        serialPortArduino.Handshake = Handshake.None;
                    else if (radioButtonHandshakeRTS.Checked)
                        serialPortArduino.Handshake = Handshake.RequestToSend;
                    else if (radioButtonHandshakeRTSXonXoff.Checked)
                        serialPortArduino.Handshake = Handshake.RequestToSendXOnXOff;
                    else if (radioButtonHandshakeXonXoff.Checked)
                        serialPortArduino.Handshake = Handshake.XOnXOff;

                    // RTS/DTR from checkboxes
                    serialPortArduino.RtsEnable = checkBoxRtsEnable.Checked;
                    serialPortArduino.DtrEnable = checkBoxDtrEnable.Checked;

                    serialPortArduino.Open();

                    // send ping and expect pong
                    try
                    {
                        serialPortArduino.WriteLine("ping");
                        string resp = serialPortArduino.ReadLine().Trim();
                        if (string.Equals(resp, "pong", StringComparison.InvariantCultureIgnoreCase))
                        {
                            radioButtonVerbonden.Checked = true;
                            buttonConnect.Text = "Disconnect";
                            labelStatus.Text = "Verbonden met " + serialPortArduino.PortName;
                        }
                        else
                        {
                            serialPortArduino.Close();
                            MessageBox.Show("Onverwacht antwoord: " + resp, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            buttonConnect.Text = "Connect";
                            labelStatus.Text = "Niet verbonden";
                        }
                    }
                    catch (TimeoutException)
                    {
                        serialPortArduino.Close();
                        MessageBox.Show("Geen antwoord van Arduino (timeout).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        buttonConnect.Text = "Connect";
                        labelStatus.Text = "Niet verbonden";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serial port error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
