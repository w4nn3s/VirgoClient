using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirgoClient
{
    public partial class VirgoClient : Form
    {
        public Socket _clientSocket;

        public VirgoClient()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] _buffer = Encoding.ASCII.GetBytes(txtInput.Text);
                _clientSocket.BeginSend(_buffer, 0,_buffer.Length,SocketFlags.None, new AsyncCallback(SendBufferCallback), null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.BeginConnect(new IPEndPoint(IPAddress.Any, 4444), new AsyncCallback(ConnectedCallback), null);

        }

        private void ConnectedCallback(IAsyncResult AR)
        {
            try
            {

                _clientSocket.EndConnect(AR);
                btnSend.Invoke(new Action(() => btnSend.Enabled=true));
                lblInfo.Invoke(new Action(() => lblInfo.Visible = true));
                lblInfo.Invoke(new Action(() => lblInfo.Text = _clientSocket.LocalEndPoint.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SendBufferCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket.EndSend(AR);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
