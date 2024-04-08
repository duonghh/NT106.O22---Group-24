namespace NetworkScanner
{
    partial class NetworkScanner
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            interfacesComboBox = new ComboBox();
            listView = new ListView();
            IPAddress = new ColumnHeader();
            Status = new ColumnHeader();
            progressBar = new ProgressBar();
            label2 = new Label();
            statusLabel = new Label();
            stopButton = new Button();
            scanButton = new Button();
            label3 = new Label();
            ipAddressTextBox = new TextBox();
            label4 = new Label();
            subnetMaskTextBox = new TextBox();
            label5 = new Label();
            networkTextBox = new TextBox();
            featurePanel = new Panel();
            getInfoButton = new Button();
            scanPortButton = new Button();
            featurePanel.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(159, 21);
            label1.TabIndex = 0;
            label1.Text = "Chọn giao diện mạng";
            // 
            // interfacesComboBox
            // 
            interfacesComboBox.FormattingEnabled = true;
            interfacesComboBox.Location = new Point(177, 17);
            interfacesComboBox.Name = "interfacesComboBox";
            interfacesComboBox.Size = new Size(203, 29);
            interfacesComboBox.TabIndex = 1;
            interfacesComboBox.SelectedIndexChanged += interfacesComboBox_SelectedIndexChanged;
            // 
            // listView
            // 
            listView.Columns.AddRange(new ColumnHeader[] { IPAddress, Status });
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Location = new Point(12, 87);
            listView.MultiSelect = false;
            listView.Name = "listView";
            listView.Size = new Size(338, 292);
            listView.TabIndex = 2;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            listView.ItemSelectionChanged += listView_ItemSelectionChanged;
            // 
            // IPAddress
            // 
            IPAddress.Text = "Địa chỉ IP";
            IPAddress.Width = 150;
            // 
            // Status
            // 
            Status.Text = "Trạng thái";
            Status.Width = 100;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 406);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(680, 23);
            progressBar.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 382);
            label2.Name = "label2";
            label2.Size = new Size(82, 21);
            label2.TabIndex = 4;
            label2.Text = "Trạng thái:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(100, 382);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 21);
            statusLabel.TabIndex = 5;
            // 
            // stopButton
            // 
            stopButton.Enabled = false;
            stopButton.Location = new Point(542, 12);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(150, 35);
            stopButton.TabIndex = 6;
            stopButton.Text = "Dừng quét";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // scanButton
            // 
            scanButton.Location = new Point(386, 13);
            scanButton.Name = "scanButton";
            scanButton.Size = new Size(150, 35);
            scanButton.TabIndex = 6;
            scanButton.Text = "Bắt đầu quét";
            scanButton.UseVisualStyleBackColor = true;
            scanButton.Click += scanButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 55);
            label3.Name = "label3";
            label3.Size = new Size(74, 21);
            label3.TabIndex = 7;
            label3.Text = "Địa chỉ IP";
            // 
            // ipAddressTextBox
            // 
            ipAddressTextBox.Location = new Point(92, 52);
            ipAddressTextBox.Name = "ipAddressTextBox";
            ipAddressTextBox.ReadOnly = true;
            ipAddressTextBox.Size = new Size(125, 29);
            ipAddressTextBox.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(250, 56);
            label4.Name = "label4";
            label4.Size = new Size(100, 21);
            label4.TabIndex = 9;
            label4.Text = "Subnet Mask";
            // 
            // subnetMaskTextBox
            // 
            subnetMaskTextBox.Location = new Point(356, 52);
            subnetMaskTextBox.Name = "subnetMaskTextBox";
            subnetMaskTextBox.ReadOnly = true;
            subnetMaskTextBox.Size = new Size(125, 29);
            subnetMaskTextBox.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(511, 56);
            label5.Name = "label5";
            label5.Size = new Size(50, 21);
            label5.TabIndex = 11;
            label5.Text = "Mạng";
            // 
            // networkTextBox
            // 
            networkTextBox.Location = new Point(567, 53);
            networkTextBox.Name = "networkTextBox";
            networkTextBox.ReadOnly = true;
            networkTextBox.Size = new Size(125, 29);
            networkTextBox.TabIndex = 10;
            // 
            // featurePanel
            // 
            featurePanel.Controls.Add(getInfoButton);
            featurePanel.Controls.Add(scanPortButton);
            featurePanel.Enabled = false;
            featurePanel.Location = new Point(356, 88);
            featurePanel.Name = "featurePanel";
            featurePanel.Size = new Size(336, 291);
            featurePanel.TabIndex = 12;
            // 
            // getInfoButton
            // 
            getInfoButton.Location = new Point(16, 3);
            getInfoButton.Name = "getInfoButton";
            getInfoButton.Size = new Size(150, 35);
            getInfoButton.TabIndex = 6;
            getInfoButton.Text = "Lấy thông tin";
            getInfoButton.UseVisualStyleBackColor = true;
            getInfoButton.Click += getInfoButton_Click;
            // 
            // scanPortButton
            // 
            scanPortButton.Location = new Point(172, 3);
            scanPortButton.Name = "scanPortButton";
            scanPortButton.Size = new Size(150, 35);
            scanPortButton.TabIndex = 6;
            scanPortButton.Text = "Quét cổng";
            scanPortButton.UseVisualStyleBackColor = true;
            scanPortButton.Click += scanPortButton_Click;
            // 
            // NetworkScanner
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(704, 441);
            Controls.Add(featurePanel);
            Controls.Add(label5);
            Controls.Add(networkTextBox);
            Controls.Add(subnetMaskTextBox);
            Controls.Add(label4);
            Controls.Add(ipAddressTextBox);
            Controls.Add(label3);
            Controls.Add(scanButton);
            Controls.Add(stopButton);
            Controls.Add(statusLabel);
            Controls.Add(label2);
            Controls.Add(progressBar);
            Controls.Add(listView);
            Controls.Add(interfacesComboBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "NetworkScanner";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Network Scanner";
            Load += NetworkScanner_Load;
            featurePanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox interfacesComboBox;
        private ListView listView;
        private ProgressBar progressBar;
        private Label label2;
        private Label statusLabel;
        private Button stopButton;
        private Button scanButton;
        private Label label3;
        private TextBox ipAddressTextBox;
        private Label label4;
        private TextBox subnetMaskTextBox;
        private Label label5;
        private TextBox networkTextBox;
        private ColumnHeader IPAddress;
        private ColumnHeader MACAddress;
        private Panel featurePanel;
        private Button scanPortButton;
        private ColumnHeader Status;
        private Button getInfoButton;
    }
}
