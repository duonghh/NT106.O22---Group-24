namespace NetworkScanner
{
    partial class Account
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
            usernameTextBox = new TextBox();
            label1 = new Label();
            passwordTextBox = new TextBox();
            label2 = new Label();
            OKButton = new Button();
            SuspendLayout();
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(105, 12);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(200, 30);
            usernameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(87, 23);
            label1.TabIndex = 1;
            label1.Text = "Username";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(105, 48);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(200, 30);
            passwordTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 51);
            label2.Name = "label2";
            label2.Size = new Size(80, 23);
            label2.TabIndex = 1;
            label2.Text = "Password";
            // 
            // OKButton
            // 
            OKButton.Location = new Point(12, 84);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(293, 35);
            OKButton.TabIndex = 2;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // Account
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(321, 130);
            Controls.Add(OKButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(passwordTextBox);
            Controls.Add(usernameTextBox);
            Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Account";
            Text = "Account";
            Load += Account_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox usernameTextBox;
        private Label label1;
        private TextBox passwordTextBox;
        private Label label2;
        private Button OKButton;
    }
}