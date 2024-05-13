namespace NetworkScanner
{
    partial class NetworkSniffer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            interfaceComboBox = new ComboBox();
            label2 = new Label();
            ipAddressTextBox = new TextBox();
            startButton = new Button();
            stopButton = new Button();
            listView = new ListView();
            NumberOfPacket = new ColumnHeader();
            Time = new ColumnHeader();
            Source = new ColumnHeader();
            SourcePort = new ColumnHeader();
            Destination = new ColumnHeader();
            DestinationPort = new ColumnHeader();
            Protocol = new ColumnHeader();
            PacketSize = new ColumnHeader();
            treeView = new TreeView();
            groupBox1 = new GroupBox();
            textEncodeButton = new Button();
            hexEncodeButton = new Button();
            richTextBox = new RichTextBox();
            deleteButton = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(132, 23);
            label1.TabIndex = 0;
            label1.Text = "Giao diện mạng";
            // 
            // interfaceComboBox
            // 
            interfaceComboBox.FormattingEnabled = true;
            interfaceComboBox.Location = new Point(150, 12);
            interfaceComboBox.Name = "interfaceComboBox";
            interfaceComboBox.Size = new Size(200, 31);
            interfaceComboBox.TabIndex = 1;
            interfaceComboBox.SelectedIndexChanged += interfaceComboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 52);
            label2.Name = "label2";
            label2.Size = new Size(82, 23);
            label2.TabIndex = 2;
            label2.Text = "Địa chỉ IP";
            // 
            // ipAddressTextBox
            // 
            ipAddressTextBox.Location = new Point(150, 49);
            ipAddressTextBox.Name = "ipAddressTextBox";
            ipAddressTextBox.ReadOnly = true;
            ipAddressTextBox.Size = new Size(150, 30);
            ipAddressTextBox.TabIndex = 3;
            // 
            // startButton
            // 
            startButton.Enabled = false;
            startButton.Location = new Point(757, 15);
            startButton.Name = "startButton";
            startButton.Size = new Size(81, 67);
            startButton.TabIndex = 4;
            startButton.Text = "Bắt đầu";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // stopButton
            // 
            stopButton.Enabled = false;
            stopButton.Location = new Point(844, 15);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(81, 67);
            stopButton.TabIndex = 4;
            stopButton.Text = "Dừng";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // listView
            // 
            listView.Columns.AddRange(new ColumnHeader[] { NumberOfPacket, Time, Source, SourcePort, Destination, DestinationPort, Protocol, PacketSize });
            listView.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listView.FullRowSelect = true;
            listView.Location = new Point(12, 96);
            listView.MultiSelect = false;
            listView.Name = "listView";
            listView.Size = new Size(1000, 300);
            listView.TabIndex = 5;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            // 
            // NumberOfPacket
            // 
            NumberOfPacket.Text = "No.";
            NumberOfPacket.Width = 75;
            // 
            // Time
            // 
            Time.Text = "Time";
            Time.Width = 125;
            // 
            // Source
            // 
            Source.Text = "Source";
            Source.Width = 125;
            // 
            // SourcePort
            // 
            SourcePort.Text = "Source Port";
            SourcePort.Width = 125;
            // 
            // Destination
            // 
            Destination.Text = "Destination";
            Destination.Width = 125;
            // 
            // DestinationPort
            // 
            DestinationPort.Text = "Destination Port";
            DestinationPort.Width = 125;
            // 
            // Protocol
            // 
            Protocol.Text = "Protocol";
            Protocol.Width = 125;
            // 
            // PacketSize
            // 
            PacketSize.Text = "Packet Size";
            PacketSize.Width = 100;
            // 
            // treeView
            // 
            treeView.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            treeView.Location = new Point(12, 402);
            treeView.Name = "treeView";
            treeView.Size = new Size(450, 250);
            treeView.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textEncodeButton);
            groupBox1.Controls.Add(hexEncodeButton);
            groupBox1.Controls.Add(richTextBox);
            groupBox1.Location = new Point(468, 402);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(544, 250);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Payload";
            // 
            // textEncodeButton
            // 
            textEncodeButton.Font = new Font("Segoe UI", 9F);
            textEncodeButton.Location = new Point(87, 221);
            textEncodeButton.Name = "textEncodeButton";
            textEncodeButton.Size = new Size(75, 23);
            textEncodeButton.TabIndex = 1;
            textEncodeButton.Text = "Text";
            textEncodeButton.UseVisualStyleBackColor = true;
            textEncodeButton.Click += asciiEncodeButton_Click;
            // 
            // hexEncodeButton
            // 
            hexEncodeButton.Font = new Font("Segoe UI", 9F);
            hexEncodeButton.Location = new Point(6, 221);
            hexEncodeButton.Name = "hexEncodeButton";
            hexEncodeButton.Size = new Size(75, 23);
            hexEncodeButton.TabIndex = 1;
            hexEncodeButton.Text = "Hex";
            hexEncodeButton.UseVisualStyleBackColor = true;
            hexEncodeButton.Click += hexEncodeButton_Click;
            // 
            // richTextBox
            // 
            richTextBox.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox.Location = new Point(6, 29);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(532, 186);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";
            // 
            // deleteButton
            // 
            deleteButton.Enabled = false;
            deleteButton.Location = new Point(931, 15);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(81, 67);
            deleteButton.TabIndex = 4;
            deleteButton.Text = "Xoá";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // NetworkSniffer
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 660);
            Controls.Add(groupBox1);
            Controls.Add(treeView);
            Controls.Add(listView);
            Controls.Add(stopButton);
            Controls.Add(deleteButton);
            Controls.Add(startButton);
            Controls.Add(ipAddressTextBox);
            Controls.Add(label2);
            Controls.Add(interfaceComboBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            Name = "NetworkSniffer";
            Text = "NetworkSniffer";
            Load += NetworkSniffer_Load;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox interfaceComboBox;
        private Label label2;
        private TextBox ipAddressTextBox;
        private Button startButton;
        private Button stopButton;
        private ListView listView;
        private ColumnHeader NumberOfPacket;
        private ColumnHeader Time;
        private ColumnHeader Source;
        private ColumnHeader SourcePort;
        private ColumnHeader Destination;
        private ColumnHeader Protocol;
        private ColumnHeader PacketSize;
        private TreeView treeView;
        private GroupBox groupBox1;
        private RichTextBox richTextBox;
        private Button deleteButton;
        private ColumnHeader DestinationPort;
        private Button textEncodeButton;
        private Button hexEncodeButton;
    }
}