namespace NetworkScanner
{
    partial class SelectInterfaceForm
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
            comboBox1 = new ComboBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(253, 21);
            label1.TabIndex = 0;
            label1.Text = "Chọn giao diện mạng để lắng nghe";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(271, 6);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(200, 29);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(178, 50);
            button1.Name = "button1";
            button1.Size = new Size(147, 30);
            button1.TabIndex = 2;
            button1.Text = "Chọn";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // SelectInterfaceForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 97);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 12F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            Name = "SelectInterfaceForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Select Interface";
            Load += SelectInterface_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Button button1;
    }
}