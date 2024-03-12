using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleWindowExplorer
{
    public partial class SimpleWindowExplorer : Form
    {
        private string filePathString;
        private string selectedFolder;
        private string[] visitedPaths;
        private int visitedPathIndex;
        public SimpleWindowExplorer()
        {
            InitializeComponent();
        }

        private void SimpleWindowExplorer_Load(object sender, EventArgs e)
        {
            filePathString = "C:";
            selectedFolder = "";
            pathTextBox.Text = "C:";
            visitedPaths = ["C:"];
            visitedPathIndex = 0;
            previousButton.Enabled = false;
            nextButton.Enabled = false;
            loadFilesAndDirectory();
        }

        public void loadFilesAndDirectory() // Load files và folders trong thư mục
        {
            try
            {
                DirectoryInfo fileList = new DirectoryInfo(filePathString + "\\" + selectedFolder);
                if (selectedFolder != "") filePathString += "\\" + selectedFolder;
                pathTextBox.Text = filePathString;
                fileListView.Items.Clear();
                FileInfo[] files = fileList.GetFiles();
                DirectoryInfo[] dirs = fileList.GetDirectories();

                foreach (FileInfo file in files)
                {
                    fileListView.Items.Add(file.Name, 1); // Load files
                }
                foreach (DirectoryInfo dir in dirs)
                {
                    fileListView.Items.Add(dir.Name, 0);   // Load folders
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đọc đường dẫn này!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fileListView.Items.Clear();
                filePathString = visitedPaths[visitedPathIndex];
                pathTextBox.Text = filePathString;
                selectedFolder = "";
                if (visitedPathIndex == 0) previousButton.Enabled = false;
                loadFilesAndDirectory();
            }
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            visitedPathIndex--;
            if (visitedPathIndex == 0) previousButton.Enabled = false;
            filePathString = visitedPaths[visitedPathIndex];
            selectedFolder = "";
            nextButton.Enabled = true;
            loadFilesAndDirectory();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            visitedPathIndex++;
            if (visitedPathIndex == visitedPaths.Length - 1) nextButton.Enabled = false;
            filePathString = visitedPaths[visitedPathIndex];
            selectedFolder = "";
            previousButton.Enabled = true;
            loadFilesAndDirectory();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            filePathString = pathTextBox.Text;
            previousButton.Enabled = true;
            loadFilesAndDirectory();
            expandVistedPathArray();
        }

        private void fileListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)  // Kiểm tra item được chọn 
        {
            string selectedItem = e.Item.Text;

            FileAttributes fileAttrs = File.GetAttributes(filePathString + "\\" + selectedItem);
            if ((fileAttrs & FileAttributes.Directory) == FileAttributes.Directory) // Nếu item được chọn là 1 folder
            {
                selectedFolder = selectedItem;
            }
            else
            {
                selectedFolder = "";
            }
        }

        private void fileListView_MouseDoubleClick(object sender, MouseEventArgs e) // Double click folder
        {
            previousButton.Enabled = true;
            loadFilesAndDirectory();
            expandVistedPathArray();
        }

        private void pathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filePathString = pathTextBox.Text;
                previousButton.Enabled = true;
                loadFilesAndDirectory();
                expandVistedPathArray();
            }
        }

        private void pathTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            folderBrowserDialog1.ShowDialog(this);
            pathTextBox.Text = folderBrowserDialog1.SelectedPath;
            filePathString = folderBrowserDialog1.SelectedPath;
            selectedFolder = "";
            previousButton.Enabled = true;
            loadFilesAndDirectory();
            expandVistedPathArray();
        }

        private void expandVistedPathArray()
        {
            if (visitedPaths[visitedPathIndex] != filePathString)
            {
                visitedPathIndex++;
                Array.Resize(ref visitedPaths, visitedPathIndex + 1);
                visitedPaths[visitedPathIndex] = filePathString;
                nextButton.Enabled = false;
            }
        }

    }
}
