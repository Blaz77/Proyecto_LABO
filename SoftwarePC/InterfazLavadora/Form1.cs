using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace InterfazLavadora
{
    public enum DataMode { Text, Hex }

    public enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error };

    public partial class Form1 : Form
    {
        private SerialPort comport = new SerialPort();
        private Color[] LogMsgTypeColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };


        private DataMode CurrentDataMode
        {
            get
            {
                return DataMode.Hex;
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitializeControlValues();

            EnableControls();


            comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }

        private void InitializeControlValues()
        {

            RefreshComPortList();
            
            if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = cmbPortName.Items.Count - 1;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void EnableControls()
        {
            txtSendData.Enabled = btnSend.Enabled = comport.IsOpen;

            if (comport.IsOpen) btnOpenPort.Text = "&Close Port";
            else btnOpenPort.Text = "&Open Port";
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            bool error = false;

            if (comport.IsOpen) comport.Close();
            else
            {
                comport.BaudRate = int.Parse(cmbBaudRate.Text);
                comport.DataBits = 8;
                comport.StopBits = StopBits.One;
                comport.Parity = Parity.None;
                comport.PortName = cmbPortName.Text;

                try
                {
                    comport.Open();
                }
                catch (UnauthorizedAccessException) { error = true; }
                catch (IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }

            EnableControls();

            if (comport.IsOpen)
            {
                txtSendData.Focus();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        { SendData(); }

        private void SendData()
        {
            if (CurrentDataMode == DataMode.Text)
            {
                comport.Write(txtSendData.Text);

                Log(LogMsgType.Outgoing, txtSendData.Text + "\n");
            }
            else
            {
                try
                {
                    byte[] data = HexStringToByteArray(txtSendData.Text);

                    comport.Write(data, 0, data.Length);

                    Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                }
                catch (FormatException)
                {
                    Log(LogMsgType.Error, "Not properly formatted hex string: " + txtSendData.Text + "\n");
                }
            }
            txtSendData.SelectAll();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!comport.IsOpen) return;


            if (CurrentDataMode == DataMode.Text)
            {
                string data = comport.ReadExisting();

                Log(LogMsgType.Incoming, data);
            }
            else
            {
                int bytes = comport.BytesToRead;

                byte[] buffer = new byte[bytes];

                comport.Read(buffer, 0, bytes);

                Log(LogMsgType.Incoming, ByteArrayToHexString(buffer));
            }
        }

        private void tmrCheckComPorts_Tick(object sender, EventArgs e)
        {
            RefreshComPortList();
        }

        private void Log(LogMsgType msgtype, string msg)
        {
            rtfTerminal.Invoke(new EventHandler(delegate
            {
                rtfTerminal.SelectedText = string.Empty;
                rtfTerminal.SelectionFont = new Font(rtfTerminal.SelectionFont, FontStyle.Bold);
                rtfTerminal.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtfTerminal.AppendText(msg);
                rtfTerminal.ScrollToCaret();
            }));
        }

        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        private void RefreshComPortList()
        {
            string selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, comport.IsOpen);

            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(OrderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }

        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            string selected = null;

            string[] ports = SerialPort.GetPortNames();

            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            if (updated)
            {
                ports = OrderedPortNames();

                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            return selected;
        }

        private string[] OrderedPortNames()
        {
            int num;

            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }
    }
}
