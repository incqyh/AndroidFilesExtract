﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileExtracter
{
    /// <summary>
    /// linux下的7种文件类型
    /// 不过在data目录中应该只有部分类型会存在
    /// </summary>
    public enum Type
    {
        alltype = 'a',
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
    // enum State { noConnection, copyFail, invalidInput, unexpectedOutput, fileNotExist};
    class Result
    {
        public bool success;
        public string errorMessage;
        // public State state;
        public List<FileProperty> filesProperty;
        public List<string> filesName;
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
        Result ListDirecotryVerbose(string device, string path);
        Result SearchFilesVerbose(string device, string path, string pattern, Type fileType);
        Result CopyFileFromDevice(string device, string devivePath, string pcPath);
        string[] Devices { get; }
    }

    class ShellScriptFileExtracter : FileExtracter
    {
        string[] devices;

        public string[] Devices
        {
            get { return devices; }
        }

        List<FileProperty> ParaseProperties(string[] rawData)
        {
            List<FileProperty> result = new List<FileProperty>();

            for (int i = 0; i < rawData.Length; i += 7)
            {
                FileProperty property = new FileProperty();
                property.modifyTime = rawData[i + 5].Substring(8, rawData[5].Length - 8);
                property.accessTime = rawData[i + 4].Substring(8, rawData[4].Length - 8);

                if (rawData[i + 1].Contains("directory")) property.type = Type.directory;
                if (rawData[i + 1].Contains("regular")) property.type = Type.file;
                if (rawData[i + 1].Contains("symbol")) property.type = Type.link;

                string sizePattern = @"(?<=Size: )\d*\b";
                property.size = Regex.Matches(rawData[i + 1], sizePattern)[0].ToString();
                string pathPattern = @"(?<=File: ).*";
                property.path = Regex.Matches(rawData[i], pathPattern)[0].ToString();

                result.Add(property);
            }
            return result;
        }

        FileProperty GetProperty(string device, string path)
        {
            FileProperty property = new FileProperty();

            var items = AdbHelper.AdbHelper.ListDataFolder(device, path);
            string errorPattern = "No such file or directory";
            if (items.Length != 0 && items[0].Contains(errorPattern))
                throw new Exception("Wrong path!");
            else
            {
                var rawData = AdbHelper.AdbHelper.GetProperty(device, path);
                property =  ParaseProperties(rawData)[0];
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
                    throw new Exception("No device detected!");
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
                result.errorMessage = ex.ToString();
            }
            return result;
        }

        public Result GetFileInformation(string device, string path)
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
                    result.filesName = new List<string>();
                    foreach (string file in AdbHelper.AdbHelper.ListDataFolder(device, path))
                        result.filesName.Add(file);
                    result.success = true;
                }
                else
                {
                    throw new Exception("Wrong path!");
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
                result.filesName = new List<string>();
                foreach (string file in AdbHelper.AdbHelper.SearchFiles(device, pattern, path, (char)fileType))
                    result.filesName.Add(file);
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.ToString();
            }
            return result;

        }

        public Result ListDirecotryVerbose(string device, string path)
        {
            string listScript;
            if (path == "/")
            {
                listScript = System.String.Format(
                                                  "ls {0}|while read i;do " +
                                                  "echo \\\"$(stat \\\"/$i\\\")\\\";" +
                                                  "done", path);
            }
            else
            {
                listScript = System.String.Format(
                                                  "ls {0}|while read i;do " +
                                                  "echo \\\"$(stat \\\"{0}/$i\\\")\\\";" +
                                                  "done", path);
            }

            // string scriptName = "/sdcard/utils/list" + path.Replace('/', '_');
            Result result = new Result();
            try
            {
                FileProperty property = GetProperty(device, path);
                if (property.type == Type.directory)
                {
                    var properities = AdbHelper.AdbHelper.RunShell(device, listScript);
                    result.filesProperty = ParaseProperties(properities);
                    result.success = true;
                }
                else
                {
                    throw new Exception("Wrong path!");
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.ToString();
            }
            return result;

        }

        public Result SearchFilesVerbose(string device, string path, string pattern, Type fileType)
        {
            string searchScript;
            if (fileType == Type.alltype)
            {
                searchScript = System.String.Format(
                                                   "find {0} -name \\\"{1}\\\"|" +
                                                   "while read i;do " +
                                                   "echo \\\"$(stat \\\"$i\\\")\\\"; " +
                                                   "done", path, pattern);
            }
            else
            {
                searchScript = System.String.Format(
                                                   "find {0} -name \\\"{1}\\\" -type {2}|" +
                                                   "while read i;do " +
                                                   "echo \\\"$(stat \\\"$i\\\")\\\"; " +
                                                   "done", path, pattern, (char)fileType);
            }
            // string scriptName = "/sdcard/utils/search" + path.Replace('/', '_');

            Result result = new Result();
            try
            {
                var properities = AdbHelper.AdbHelper.RunShell(device, searchScript);
                result.filesProperty = ParaseProperties(properities);
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
            if (!System.IO.Directory.Exists(pcPath))
                System.IO.Directory.CreateDirectory(pcPath);
            Result result = new Result();
            try
            {
                FileProperty property = GetProperty(device, devicePath);
                if (property.type == Type.fne)
                    throw new Exception("Wrong path");
                if (property.type == Type.directory)
                {
                    string fullPath = pcPath + '/' + property.path.Replace('/', '_');
                    if (!System.IO.Directory.Exists(fullPath))
                        System.IO.Directory.CreateDirectory(fullPath);
                    AdbHelper.AdbHelper.CopyFromDevice(device, devicePath, fullPath);
                }
                else
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

    /*
    /// <summary>
    /// 先将所有的文件拷到本地，再进行分析
    /// </summary>
    class CopyFirstFileExtracter:FileExtracter
    {
        string[] devices;

        public string[] Devices
        {
            get { return devices; }
        }

        FileProperty GetProperty(string device, string path)
        {
            FileProperty property = new FileProperty();
            return property;
        }

        List<FileProperty> GetProperties(string device, string[] paths)
        {
            List<FileProperty> properties = new List<FileProperty>();
            return properties;
        }

        public Result InitConnection()
        {
            Result result = new Result();
            try
            {
                devices = AdbHelper.AdbHelper.GetDevices();
                if (devices.Length == 0)
                {
                    throw new Exception("No device detected!");
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
                result.errorMessage = ex.ToString();
            }
            return result;
        }

        public Result GetFileInformation (string device, string path)
        {
            Result result = new Result();
            return result;
        }


        public Result ListDirecotry(string device, string path)
        {
            Result result = new Result();
            return result;

        }

        public Result SearchFiles(string device, string path, string pattern, Type fileType)
        {
            Result result = new Result();
            return result;
        }

        public Result CopyFileFromDevice(string device, string devicePath, string pcPath)
        {
            Result result = new Result();
            try
            {
                GetProperty(device, devicePath);
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
    */
}