namespace ChatApp_Server
{
    partial class ChatApp_Server
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
            portTextBox = new TextBox();
            startButton = new Button();
            stopButton = new Button();
            chatScreenRichTextBox = new RichTextBox();
            importTextBox = new TextBox();
            sendButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 0;
            label1.Text = "Port";
            // 
            // portTextBox
            // 
            portTextBox.Location = new Point(47, 6);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(50, 23);
            portTextBox.TabIndex = 1;
            // 
            // startButton
            // 
            startButton.Location = new Point(103, 5);
            startButton.Name = "startButton";
            startButton.Size = new Size(180, 23);
            startButton.TabIndex = 2;
            startButton.Text = "Start Server";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // stopButton
            // 
            stopButton.Enabled = false;
            stopButton.Location = new Point(292, 5);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(180, 23);
            stopButton.TabIndex = 2;
            stopButton.Text = "Stop Server";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // chatScreenRichTextBox
            // 
            chatScreenRichTextBox.Location = new Point(12, 35);
            chatScreenRichTextBox.Name = "chatScreenRichTextBox";
            chatScreenRichTextBox.ReadOnly = true;
            chatScreenRichTextBox.Size = new Size(460, 435);
            chatScreenRichTextBox.TabIndex = 3;
            chatScreenRichTextBox.Text = "";
            // 
            // importTextBox
            // 
            importTextBox.Location = new Point(12, 476);
            importTextBox.Multiline = true;
            importTextBox.Name = "importTextBox";
            importTextBox.Size = new Size(379, 23);
            importTextBox.TabIndex = 4;
            // 
            // sendButton
            // 
            sendButton.Enabled = false;
            sendButton.Location = new Point(397, 476);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(75, 23);
            sendButton.TabIndex = 5;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // ChatApp_Server
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 511);
            Controls.Add(sendButton);
            Controls.Add(importTextBox);
            Controls.Add(chatScreenRichTextBox);
            Controls.Add(stopButton);
            Controls.Add(startButton);
            Controls.Add(portTextBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ChatApp_Server";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox portTextBox;
        private Button startButton;
        private Button stopButton;
        private RichTextBox chatScreenRichTextBox;
        private TextBox importTextBox;
        private Button sendButton;
    }
}
