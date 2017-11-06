using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

public class Example
{
    public static void Main()
    {
        FileExtracter.FileExtracter feh = new FileExtracter.ShellScriptFileExtracter();

        feh.InitConnection();
        var devices = feh.Devices;

        string testDevice = "610510540122"; // 小米盒子
        // string testDevice = "127.0.0.1:26944"; // 模拟器
        string testDir = "/data/data";
        string testFile = "/init";

        var rd = feh.GetFileInformation(testDevice, testDir);
        var rf = feh.GetFileInformation(testDevice, testFile);
        var rl = feh.ListDirecotry(testDevice, testDir);
        var rs = feh.SearchFiles(testDevice, testDir, "*0*", FileExtracter.Type.alltype);
        var rlv = feh.ListDirecotryVerbose(testDevice, testDir);
        var rsv = feh.SearchFilesVerbose(testDevice, testDir, "*0*", FileExtracter.Type.alltype);
        feh.CopyFileFromDevice(testDevice, "/data/data/com.kanke.tv", "CopiedFiles");
    }
}