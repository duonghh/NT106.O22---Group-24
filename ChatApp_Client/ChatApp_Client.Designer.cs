namespace ChatApp_Client
{
    partial class ChatApp_Client
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
            ipAddressTextBox = new TextBox();
            label2 = new Label();
            portTextBox = new TextBox();
            label3 = new Label();
            usernameTextBox = new TextBox();
            connectButton = new Button();
            disconectButton = new Button();
            chatScreenRichTextBox = new RichTextBox();
            importTextBox = new TextBox();
            sendButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(221, 9);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "IP Address";
            // 
            // ipAddressTextBox
            // 
            ipAddressTextBox.Location = new Point(289, 6);
            ipAddressTextBox.Name = "ipAddressTextBox";
            ipAddressTextBox.Size = new Size(100, 23);
            ipAddressTextBox.TabIndex = 1;
            ipAddressTextBox.Text = "127.0.0.1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(395, 9);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 2;
            label2.Text = "Port";
            // 
            // portTextBox
            // 
            portTextBox.Location = new Point(430, 6);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(42, 23);
            portTextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 4;
            label3.Text = "Username";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(78, 6);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(137, 23);
            usernameTextBox.TabIndex = 5;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(138, 35);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(100, 23);
            connectButton.TabIndex = 6;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // disconectButton
            // 
            disconectButton.Enabled = false;
            disconectButton.Location = new Point(244, 35);
            disconectButton.Name = "disconectButton";
            disconectButton.Size = new Size(100, 23);
            disconectButton.TabIndex = 7;
            disconectButton.Text = "Disconect";
            disconectButton.UseVisualStyleBackColor = true;
            disconectButton.Click += disconectButton_Click;
            // 
            // chatScreenRichTextBox
            // 
            chatScreenRichTextBox.Location = new Point(12, 64);
            chatScreenRichTextBox.Name = "chatScreenRichTextBox";
            chatScreenRichTextBox.ReadOnly = true;
            chatScreenRichTextBox.Size = new Size(460, 406);
            chatScreenRichTextBox.TabIndex = 8;
            chatScreenRichTextBox.Text = "";
            // 
            // importTextBox
            // 
            importTextBox.Location = new Point(12, 476);
            importTextBox.MaxLength = 1024;
            importTextBox.Multiline = true;
            importTextBox.Name = "importTextBox";
            importTextBox.Size = new Size(377, 23);
            importTextBox.TabIndex = 9;
            // 
            // sendButton
            // 
            sendButton.Enabled = false;
            sendButton.Location = new Point(397, 476);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(75, 23);
            sendButton.TabIndex = 10;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // client
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 511);
            Controls.Add(sendButton);
            Controls.Add(importTextBox);
            Controls.Add(chatScreenRichTextBox);
            Controls.Add(disconectButton);
            Controls.Add(connectButton);
            Controls.Add(usernameTextBox);
            Controls.Add(label3);
            Controls.Add(portTextBox);
            Controls.Add(label2);
            Controls.Add(ipAddressTextBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "client";
            Text = "Client";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox ipAddressTextBox;
        private Label label2;
        private TextBox portTextBox;
        private Label label3;
        private TextBox usernameTextBox;
        private Button connectButton;
        private Button disconectButton;
        private RichTextBox chatScreenRichTextBox;
        private TextBox importTextBox;
        private Button sendButton;
    }
}
