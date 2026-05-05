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
