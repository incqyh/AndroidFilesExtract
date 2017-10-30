using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdbHelper;

namespace AndroidFilesExtract
{
    public partial class FormFilesHandle : Form
    {
        public FormFilesHandle()
        {
            InitializeComponent();
            InitializeFileTree();
            InitializeDevideList();
        }

        private void InitializeFileTree()
        {
            AdbHelper.AdbHelper.deviceNo = AdbHelper.AdbHelper.GetSerialNo();
            TreeNode n = new TreeNode();
            n.Text = "/";
            fileTree.Nodes.Add(n);
        }

        private void InitializeDevideList()
        {
            AdbHelper.AdbHelper.StartServer();
            string[] devices =  AdbHelper.AdbHelper.GetDevices();
            foreach (string device in devices)
                deviceList.Items.Add(device);
        }

        private void StartSearch_Click(object sender, EventArgs e)
        {
            string pattern = searchPattern.Text;
            var files = AdbHelper.AdbHelper.SearchFiles(pattern, currentPath.Text);
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
                string[] path = item.ToString().Split(new char[]{ ' '});
                AdbHelper.AdbHelper.CopyFromDevice(path[0], "CopiedFiles");
            }
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
            string fullPath = "/";
            while (perDir.Count != 0)
            {
                if (perDir.Peek() != "/")
                    fullPath += perDir.Peek() + "/";
                perDir.Pop();
            }
            currentPath.Text = fullPath;

            var test = AdbHelper.AdbHelper.GetProperty(fullPath);
            if (selNode.Nodes.Count == 0)
            {
                var dir = AdbHelper.AdbHelper.ListDataFolder(fullPath);
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

        private void connectDevice_Click(object sender, EventArgs e)
        {
            AdbHelper.AdbHelper.deviceNo = deviceList.Text;
        }
    }
}