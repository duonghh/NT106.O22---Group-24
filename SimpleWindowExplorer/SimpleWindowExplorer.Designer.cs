namespace SimpleWindowExplorer
{
    partial class SimpleWindowExplorer
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleWindowExplorer));
            label1 = new Label();
            previousButton = new Button();
            nextButton = new Button();
            pathTextBox = new TextBox();
            openButton = new Button();
            fileListView = new ListView();
            menu = new ContextMenuStrip(components);
            copy = new ToolStripMenuItem();
            cut = new ToolStripMenuItem();
            paste = new ToolStripMenuItem();
            delete = new ToolStripMenuItem();
            iconList = new ImageList(components);
            folderBrowserDialog1 = new FolderBrowserDialog();
            menu.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(122, 16);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 0;
            label1.Text = "Path:";
            // 
            // previousButton
            // 
            previousButton.Location = new Point(12, 12);
            previousButton.Name = "previousButton";
            previousButton.Size = new Size(49, 23);
            previousButton.TabIndex = 1;
            previousButton.Text = "<<";
            previousButton.UseVisualStyleBackColor = true;
            previousButton.Click += previousButton_Click;
            // 
            // nextButton
            // 
            nextButton.Location = new Point(67, 12);
            nextButton.Name = "nextButton";
            nextButton.Size = new Size(49, 23);
            nextButton.TabIndex = 1;
            nextButton.Text = ">>";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += nextButton_Click;
            // 
            // pathTextBox
            // 
            pathTextBox.Location = new Point(162, 12);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.Size = new Size(556, 23);
            pathTextBox.TabIndex = 2;
            pathTextBox.MouseClick += pathTextBox_MouseClick;
            pathTextBox.KeyDown += pathTextBox_KeyDown;
            // 
            // openButton
            // 
            openButton.Location = new Point(724, 11);
            openButton.Name = "openButton";
            openButton.Size = new Size(64, 23);
            openButton.TabIndex = 1;
            openButton.Text = "Open";
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += openButton_Click;
            // 
            // fileListView
            // 
            fileListView.ContextMenuStrip = menu;
            fileListView.LargeImageList = iconList;
            fileListView.Location = new Point(12, 41);
            fileListView.Name = "fileListView";
            fileListView.Size = new Size(776, 397);
            fileListView.SmallImageList = iconList;
            fileListView.TabIndex = 3;
            fileListView.UseCompatibleStateImageBehavior = false;
            fileListView.ItemSelectionChanged += fileListView_ItemSelectionChanged;
            fileListView.MouseDoubleClick += fileListView_MouseDoubleClick;
            // 
            // menu
            // 
            menu.Items.AddRange(new ToolStripItem[] { copy, cut, paste, delete });
            menu.Name = "contextMenuStrip1";
            menu.Size = new Size(108, 92);
            // 
            // copy
            // 
            copy.Name = "copy";
            copy.Size = new Size(107, 22);
            copy.Text = "Copy";
            // 
            // cut
            // 
            cut.Name = "cut";
            cut.Size = new Size(107, 22);
            cut.Text = "Cut";
            // 
            // paste
            // 
            paste.Name = "paste";
            paste.Size = new Size(107, 22);
            paste.Text = "Paste";
            // 
            // delete
            // 
            delete.Name = "delete";
            delete.Size = new Size(107, 22);
            delete.Text = "Delete";
            // 
            // iconList
            // 
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            iconList.ImageStream = (ImageListStreamer)resources.GetObject("iconList.ImageStream");
            iconList.TransparentColor = Color.Transparent;
            iconList.Images.SetKeyName(0, "3767084.png");
            iconList.Images.SetKeyName(1, "643663-200.png");
            // 
            // SimpleWindowExplorer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(fileListView);
            Controls.Add(pathTextBox);
            Controls.Add(openButton);
            Controls.Add(nextButton);
            Controls.Add(previousButton);
            Controls.Add(label1);
            Name = "SimpleWindowExplorer";
            Text = "Simple Window Explorer";
            Load += SimpleWindowExplorer_Load;
            menu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button previousButton;
        private Button nextButton;
        private TextBox pathTextBox;
        private Button openButton;
        private ListView fileListView;
        private FolderBrowserDialog folderBrowserDialog1;
        private ImageList iconList;
        private ContextMenuStrip menu;
        private ToolStripMenuItem copy;
        private ToolStripMenuItem cut;
        private ToolStripMenuItem paste;
        private ToolStripMenuItem delete;
    }
}