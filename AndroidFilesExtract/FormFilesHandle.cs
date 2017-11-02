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
using System.Text.RegularExpressions;

namespace AndroidFilesExtract
{
    public partial class FormFilesHandle : Form
    {
        public FormFilesHandle()
        {
            InitializeComponent();
        }

        private void test_Click(object sender, EventArgs e)
        {
            FileExtracter.FileExtracter feh = new FileExtracter.AdbFileExtracter();
            feh.InitConnection();
            var devices = feh.Devices;
            foreach (string d in devices)
                testView.Items.Add(d);

            string testDevice = "610510540122";
            // string testDevice = "127.0.0.1:26944";
            string testDir = "/data/data";
            string testFile = "/init";

            var rd = feh.GetFileInformation(testDevice, testDir);
            // var rf = feh.GetFileInformation(testDevice, testFile);
            var rl = feh.ListDirecotry(testDevice, testDir);
            var rs = feh.SearchFiles(testDevice, testDir, "*0*", FileExtracter.Type.alltype);
            // feh.CopyFileFromDevice(testDevice, "/data/data/com.kanke.tv", "CopiedFiles");
        }
    }
}