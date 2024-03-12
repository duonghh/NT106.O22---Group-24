namespace WindowExplorer
{
    partial class Form1
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
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            backButton = new Button();
            nextButton = new Button();
            pathTextBox = new TextBox();
            browseButton = new Button();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Location = new Point(12, 42);
            webView.Name = "webView";
            webView.Size = new Size(776, 396);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // backButton
            // 
            backButton.Location = new Point(12, 12);
            backButton.Name = "backButton";
            backButton.Size = new Size(49, 24);
            backButton.TabIndex = 1;
            backButton.Text = "<<";
            backButton.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            nextButton.Location = new Point(67, 12);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(49, 24);
            nextButton.TabIndex = 1;
            nextButton.Text = ">>";
            nextButton.UseVisualStyleBackColor = true;
            // 
            // pathTextBox
            // 
            pathTextBox.Location = new Point(122, 12);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.Size = new Size(598, 23);
            pathTextBox.TabIndex = 2;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(726, 12);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(62, 24);
            browseButton.TabIndex = 1;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pathTextBox);
            Controls.Add(browseButton);
            Controls.Add(nextButton);
            Controls.Add(backButton);
            Controls.Add(webView);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private Button backButton;
        private Button nextButton;
        private TextBox pathTextBox;
        private Button browseButton;
    }
}
