using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkScanner
{
    public partial class SelectInterfaceForm : Form
    {
        public IPAddress selectedIPAddress;
        public SelectInterfaceForm()
        {
            InitializeComponent();
            selectedIPAddress = null;
        }

        private void SelectInterface_Load(object sender, EventArgs e)
        {
            #region Lấy ra tất cả Network Interface khả dụng
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    comboBox1.Items.Add(networkInterface.Name);
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Lấy ra IP của Interface khi được chọn
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.Name.Contains(comboBox1.Text))    // Lấy ra Interface được chọn
                {
                    IPInterfaceProperties properties = networkInterface.GetIPProperties();
                    foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            selectedIPAddress = ip.Address;
                        }
                    }
                    break;
                }
            }
            #endregion
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != string.Empty)
            {
                button1.Enabled = true;
            }
        }
    }
}
