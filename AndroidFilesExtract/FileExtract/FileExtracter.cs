using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace FileExtracter
{
    /// <summary>
    /// linux下的7种文件类型
    /// 不过在data目录中应该只有部分类型会存在
    /// </summary>
    public enum Type
    {
        alltype = ' ',
        fne = 'w', // file not exist

        directory = 'd',
        file = 'f',
        link = 'l',
        block = 'b',
        character = 'c', 
        socket = 's',
        pipe = 'p'
    }

    /// <summary>
    /// 存放文件详细信息的数据结构
    /// </summary>
    class FileProperty
    {
        public Type type;
        public string path;
        public string size;
        public string accessTime;
        public string modifyTime;
    }

    /// <summary>
    /// 函数调用之后统一返回的结果类型
    /// </summary>
    enum State { noConnection, copyFail, invalidInput, unexpectedOutput, fileNotExist};
    class Result
    {
        public bool success;
        public string errorMessage;
        public State state;
        public List<FileProperty> filesProperty;
        public Result()
        {
            filesProperty = new List<FileProperty>();
        }
    }

    /// <summary>
    /// 文件操作的接口
    /// </summary>
    interface FileExtracter
    {
        Result InitConnection();
        Result GetFileInformation(string device, string path);
        Result ListDirecotry(string device, string path);
        Result SearchFiles(string device, string path, string pattern, Type fileType);
        Result CopyFileFromDevice(string device, string devivePath, string pcPath);
        string[] Devices { get; }
    }

    class AdbFileExtracter:FileExtracter
    {
        string[] devices;

        public string[] Devices
        {
            get { return devices; }
        }

        FileProperty GetProperty(string device, string path)
        {
            FileProperty property = new FileProperty();

            var items = AdbHelper.AdbHelper.ListDataFolder(device, path);
            string errorPattern = "No such file or directory";
            if (items.Length != 0 && items[0].Contains(errorPattern))
                property.type = Type.fne;
            else
            {
                var rawData = AdbHelper.AdbHelper.GetProperty(device, path);
                property.path = path;
                property.modifyTime = rawData[5].Substring(8, rawData[5].Length - 8);
                property.accessTime = rawData[4].Substring(8, rawData[4].Length - 8);

                if (rawData[1].Contains("directory")) property.type = Type.directory;
                if (rawData[1].Contains("regular")) property.type = Type.file;
                if (rawData[1].Contains("symbol")) property.type = Type.link;

                string sizePattern = @"(?<=Size: )\d*\b";
                property.size = Regex.Matches(rawData[1], sizePattern)[0].ToString();
            }

            return property;
        }

        public Result InitConnection()
        {
            Result result = new Result();
            try
            {
                devices = AdbHelper.AdbHelper.GetDevices();
                if (devices.Length == 0)
                {
                    result.success = false;
                    result.state = State.noConnection;
                    result.errorMessage = "No device detected!";
                }
                else
                {
                    result.success = true;
                    devices = AdbHelper.AdbHelper.GetDevices();
                }

            }
            catch (Exception ex)
            {
                result.success = false;
                result.state = State.noConnection;
                result.errorMessage = ex.ToString();
            }
            return result;
        }

        public Result GetFileInformation (string device, string path)
        {
            Result result = new Result();
            try
            {
                result.filesProperty.Add(GetProperty(device, path));
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.ToString();
            }
            return result;
        }


        public Result ListDirecotry(string device, string path)
        {
            Result result = new Result();
            try
            {
                FileProperty property = GetProperty(device, path);
                if (property.type == Type.directory)
                {
                    var items = AdbHelper.AdbHelper.ListDataFolder(device, path);
                    foreach(var item in items)
                    {
                        property = GetProperty(device, path + "/" + item);
                        result.filesProperty.Add(property);
                    }
                    result.success = true;
                }
                else
                {
                    result.success = false;
                    result.state = State.invalidInput;
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.ToString();
            }
            return result;

        }

        public Result SearchFiles(string device, string path, string pattern, Type fileType)
        {
            Result result = new Result();
            try
            {
                var items = AdbHelper.AdbHelper.SearchFiles(device, pattern, path, (char)fileType);
                FileProperty property = new FileProperty();
                foreach(var item in items)
                {
                    property = GetProperty(device, item);
                    result.filesProperty.Add(property);
                }
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.ToString();
            }
            return result;

        }

        public Result CopyFileFromDevice(string device, string devicePath, string pcPath)
        {
            Result result = new Result();
            try
            {
                AdbHelper.AdbHelper.CopyFromDevice(device, devicePath, pcPath);
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.ToString();
            }
            return result;
        }
    }
}