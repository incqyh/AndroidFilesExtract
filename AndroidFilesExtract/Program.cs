using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;

public class Example
{
    public static void Main()
    {
        FileExtracter.FileExtracter feh = new FileExtracter.AdbFileExtracter();
        feh.InitConnection();
        var devices = feh.Devices;

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