using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkScanner
{
    public partial class Account : Form
    {
        public string username;
        public string password;
        public bool IsOK;
        public Account()
        {
            InitializeComponent();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            username = string.Empty;
            password = string.Empty;
            IsOK = false;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            username = usernameTextBox.Text;
            password = passwordTextBox.Text;
            IsOK = true;
            this.Close();
        }

        
    }
}
