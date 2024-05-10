﻿namespace NetworkScanner
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
            pingTaskProgressBar = new ProgressBar();
            findButton = new Button();
            label3 = new Label();
            ipAddressTextBox = new TextBox();
            label4 = new Label();
            subnetMaskTextBox = new TextBox();
            label5 = new Label();
            networkTextBox = new TextBox();
            seletedIpAddressTextBox = new TextBox();
            label2 = new Label();
            stopFindingButton = new Button();
            progessPercent = new Label();
            groupBox1 = new GroupBox();
            networkSnifferButton = new Button();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            logRichTextBox = new RichTextBox();
            stopButton = new Button();
            startButton = new Button();
            label6 = new Label();
            featureComboBox = new ComboBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 25);
            label1.Name = "label1";
            label1.Size = new Size(200, 28);
            label1.TabIndex = 0;
            label1.Text = "Chọn giao diện mạng";
            // 
            // interfacesComboBox
            // 
            interfacesComboBox.FormattingEnabled = true;
            interfacesComboBox.Location = new Point(8, 49);
            interfacesComboBox.Margin = new Padding(3, 2, 3, 2);
            interfacesComboBox.Name = "interfacesComboBox";
            interfacesComboBox.Size = new Size(229, 36);
            interfacesComboBox.TabIndex = 1;
            interfacesComboBox.SelectedIndexChanged += interfacesComboBox_SelectedIndexChanged;
            // 
            // listView
            // 
            listView.Columns.AddRange(new ColumnHeader[] { IPAddress });
            listView.Font = new Font("Segoe UI", 12F);
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Location = new Point(6, 298);
            listView.Margin = new Padding(3, 2, 3, 2);
            listView.MultiSelect = false;
            listView.Name = "listView";
            listView.Size = new Size(232, 246);
            listView.TabIndex = 2;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            listView.ItemSelectionChanged += listView_ItemSelectionChanged;
            // 
            // IPAddress
            // 
            IPAddress.Text = "Địa chỉ IP";
            IPAddress.Width = 225;
            // 
            // pingTaskProgressBar
            // 
            pingTaskProgressBar.Location = new Point(6, 263);
            pingTaskProgressBar.Margin = new Padding(3, 2, 3, 2);
            pingTaskProgressBar.Name = "pingTaskProgressBar";
            pingTaskProgressBar.Size = new Size(231, 28);
            pingTaskProgressBar.TabIndex = 3;
            // 
            // findButton
            // 
            findButton.Enabled = false;
            findButton.Location = new Point(6, 193);
            findButton.Margin = new Padding(3, 2, 3, 2);
            findButton.Name = "findButton";
            findButton.Size = new Size(111, 35);
            findButton.TabIndex = 6;
            findButton.Text = "Bắt đầu tìm";
            findButton.UseVisualStyleBackColor = true;
            findButton.Click += findButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 86);
            label3.Name = "label3";
            label3.Size = new Size(92, 28);
            label3.TabIndex = 7;
            label3.Text = "Địa chỉ IP";
            // 
            // ipAddressTextBox
            // 
            ipAddressTextBox.Location = new Point(114, 84);
            ipAddressTextBox.Margin = new Padding(3, 2, 3, 2);
            ipAddressTextBox.Name = "ipAddressTextBox";
            ipAddressTextBox.ReadOnly = true;
            ipAddressTextBox.Size = new Size(124, 34);
            ipAddressTextBox.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 122);
            label4.Name = "label4";
            label4.Size = new Size(125, 28);
            label4.TabIndex = 9;
            label4.Text = "Subnet Mask";
            // 
            // subnetMaskTextBox
            // 
            subnetMaskTextBox.Location = new Point(114, 119);
            subnetMaskTextBox.Margin = new Padding(3, 2, 3, 2);
            subnetMaskTextBox.Name = "subnetMaskTextBox";
            subnetMaskTextBox.ReadOnly = true;
            subnetMaskTextBox.Size = new Size(124, 34);
            subnetMaskTextBox.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 157);
            label5.Name = "label5";
            label5.Size = new Size(126, 28);
            label5.TabIndex = 11;
            label5.Text = "Địa chỉ mạng";
            // 
            // networkTextBox
            // 
            networkTextBox.Location = new Point(114, 154);
            networkTextBox.Margin = new Padding(3, 2, 3, 2);
            networkTextBox.Name = "networkTextBox";
            networkTextBox.ReadOnly = true;
            networkTextBox.Size = new Size(124, 34);
            networkTextBox.TabIndex = 10;
            // 
            // seletedIpAddressTextBox
            // 
            seletedIpAddressTextBox.Location = new Point(149, 22);
            seletedIpAddressTextBox.Margin = new Padding(3, 2, 3, 2);
            seletedIpAddressTextBox.Name = "seletedIpAddressTextBox";
            seletedIpAddressTextBox.Size = new Size(151, 34);
            seletedIpAddressTextBox.TabIndex = 8;
            seletedIpAddressTextBox.TextChanged += seletedIpAddressTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 25);
            label2.Name = "label2";
            label2.Size = new Size(172, 28);
            label2.TabIndex = 7;
            label2.Text = "Địa chỉ IP cần quét";
            // 
            // stopFindingButton
            // 
            stopFindingButton.Enabled = false;
            stopFindingButton.Location = new Point(127, 193);
            stopFindingButton.Margin = new Padding(3, 2, 3, 2);
            stopFindingButton.Name = "stopFindingButton";
            stopFindingButton.Size = new Size(111, 35);
            stopFindingButton.TabIndex = 6;
            stopFindingButton.Text = "Dừng tìm";
            stopFindingButton.UseVisualStyleBackColor = true;
            stopFindingButton.Click += stopFindingButton_Click;
            // 
            // progessPercent
            // 
            progessPercent.BackColor = Color.Transparent;
            progessPercent.Location = new Point(6, 231);
            progessPercent.Name = "progessPercent";
            progessPercent.Size = new Size(231, 28);
            progessPercent.TabIndex = 13;
            progessPercent.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(networkSnifferButton);
            groupBox1.Controls.Add(listView);
            groupBox1.Controls.Add(progessPercent);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(interfacesComboBox);
            groupBox1.Controls.Add(pingTaskProgressBar);
            groupBox1.Controls.Add(findButton);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(stopFindingButton);
            groupBox1.Controls.Add(networkTextBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(subnetMaskTextBox);
            groupBox1.Controls.Add(ipAddressTextBox);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(243, 587);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm địa chỉ cục bộ";
            // 
            // networkSnifferButton
            // 
            networkSnifferButton.Location = new Point(6, 548);
            networkSnifferButton.Margin = new Padding(3, 2, 3, 2);
            networkSnifferButton.Name = "networkSnifferButton";
            networkSnifferButton.Size = new Size(231, 35);
            networkSnifferButton.TabIndex = 14;
            networkSnifferButton.Text = "Network sniffer...";
            networkSnifferButton.UseVisualStyleBackColor = true;
            networkSnifferButton.Click += networkSnifferButton_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(stopButton);
            groupBox2.Controls.Add(startButton);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(featureComboBox);
            groupBox2.Controls.Add(seletedIpAddressTextBox);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(273, 12);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(519, 587);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Các công cụ";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(logRichTextBox);
            groupBox3.Location = new Point(6, 91);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(508, 495);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Bản ghi";
            // 
            // logRichTextBox
            // 
            logRichTextBox.Font = new Font("Segoe UI", 10F);
            logRichTextBox.Location = new Point(6, 27);
            logRichTextBox.Margin = new Padding(3, 2, 3, 2);
            logRichTextBox.Name = "logRichTextBox";
            logRichTextBox.ReadOnly = true;
            logRichTextBox.Size = new Size(496, 462);
            logRichTextBox.TabIndex = 0;
            logRichTextBox.Text = "";
            // 
            // stopButton
            // 
            stopButton.Enabled = false;
            stopButton.Location = new Point(411, 22);
            stopButton.Margin = new Padding(3, 2, 3, 2);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(100, 64);
            stopButton.TabIndex = 11;
            stopButton.Text = "Dừng";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // startButton
            // 
            startButton.Enabled = false;
            startButton.Location = new Point(305, 22);
            startButton.Margin = new Padding(3, 2, 3, 2);
            startButton.Name = "startButton";
            startButton.Size = new Size(100, 64);
            startButton.TabIndex = 11;
            startButton.Text = "Bắt đầu";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 61);
            label6.Name = "label6";
            label6.Size = new Size(98, 28);
            label6.TabIndex = 10;
            label6.Text = "Tính năng";
            // 
            // featureComboBox
            // 
            featureComboBox.Enabled = false;
            featureComboBox.FormattingEnabled = true;
            featureComboBox.Items.AddRange(new object[] { "Quét cổng TCP", "Kiểm tra tốc độ mạng", "Lắng nghe thông điệp", "Mở thư mực File Sharing" });
            featureComboBox.Location = new Point(91, 57);
            featureComboBox.Margin = new Padding(3, 2, 3, 2);
            featureComboBox.Name = "featureComboBox";
            featureComboBox.Size = new Size(208, 36);
            featureComboBox.TabIndex = 9;
            featureComboBox.SelectedIndexChanged += featureComboBox_SelectedIndexChanged;
            // 
            // NetworkScanner
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 611);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "NetworkScanner";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Network Scanner";
            Load += NetworkScanner_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private ComboBox interfacesComboBox;
        private ListView listView;
        private ProgressBar pingTaskProgressBar;
        private Button findButton;
        private Label label3;
        private TextBox ipAddressTextBox;
        private Label label4;
        private TextBox subnetMaskTextBox;
        private Label label5;
        private TextBox networkTextBox;
        private ColumnHeader IPAddress;
        private ColumnHeader MACAddress;
        private TextBox seletedIpAddressTextBox;
        private Label label2;
        private Button stopFindingButton;
        private Label progessPercent;
        private GroupBox groupBox1;
        private Button networkSnifferButton;
        private GroupBox groupBox2;
        private Label label6;
        private ComboBox featureComboBox;
        private GroupBox groupBox3;
        private RichTextBox logRichTextBox;
        private Button stopButton;
        private Button startButton;
    }
}
