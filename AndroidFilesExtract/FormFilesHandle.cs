using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidFilesExtract
{
    public partial class FormFilesHandle : Form
    {
        public FormFilesHandle()
        {
            InitializeComponent();
            InitializaFileTree();
        }

        private void InitializaFileTree()
        {
            TreeNode n = new TreeNode();
            n.Text = "/";
            fileTree.Nodes.Add(n);
        }

        private void StartSearch_Click(object sender, EventArgs e)
        {
            string pattern = searchPattern.Text;
            List<string> files = AdbHelper.SearchFiles(AdbHelper.GetSerialNo(), pattern, currentPath.Text);
            searchedFiles.Items.Clear();
            foreach (string file in files)
            {
                searchedFiles.Items.Add(file);
            }
        }

        private void CopyFiles_Click(object sender, EventArgs e)
        {
            foreach (var item in searchedFiles.SelectedItems)
            {
                string path = item.ToString();
                AdbHelper.CopyFromDevice(AdbHelper.GetSerialNo(), path, "CopiedFiles");
            }
        }

        private void GoToAnalyze_Click(object sender, EventArgs e)
        {

        }

        private void FileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selNode = e.Node;
            TreeNode itNode = e.Node;

            Stack<string> perDir = new Stack<string>();
            while (itNode.Parent != null)
            {
                perDir.Push(itNode.Text);
                itNode = itNode.Parent;
            }
            perDir.Push(itNode.Text);
            string fullPath = "";
            while (perDir.Count != 0)
            {
                if (perDir.Peek() != "/")
                    fullPath += "/" + perDir.Peek();
                perDir.Pop();
            }
            currentPath.Text = fullPath;

            if (AdbHelper.StartServer() && selNode.Nodes.Count == 0)
            {
                List<string> dir = AdbHelper.ListDataFolder(AdbHelper.GetSerialNo(), fullPath);
                if (dir[0][0] != '/')
                    foreach (var it in dir)
                    {
                        TreeNode n = new TreeNode();
                        n.Text = it;
                        selNode.Nodes.Add(n);
                    }
            }
            selNode.Expand();
        }
    }
}