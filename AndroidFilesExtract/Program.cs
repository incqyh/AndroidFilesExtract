using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Text.RegularExpressions;
using AAF.Library.Extracter;
using System.Text.RegularExpressions;

namespace Ch13Ex01
{
    class Program
    {
        static void Main(string[] args)
        {
            FileExtracter feh = new ShellFileExtracter();

            feh.InitConnection();
            var devices = feh.Devices;

            // string testDevice = "610510540122"; // 小米盒子
            string testDevice = "127.0.0.1:26944"; // 模拟器
            string testDir = "/data/data";
            string testFile = "/init";

            var rl = feh.ListDirecotry(testDevice, testDir);
            var rs = feh.SearchFiles(testDevice, testDir, "*0*");
            feh.CopyFileFromDevice(testDevice, "/data/data/com.kanke.tv", "CopiedFiles");
        }
    }
}