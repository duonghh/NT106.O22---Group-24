namespace WindowExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void browseButton_Click(object sender, EventArgs e)
        {
            if (pathTextBox.Text == string.Empty)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowDialog();
                pathTextBox.Text = dialog.SelectedPath;
                webView.CoreWebView2.Navigate(pathTextBox.Text);
            }
            else
            {
                try
                {
                    webView.CoreWebView2.Navigate(pathTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đường dẫn lỗi\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
