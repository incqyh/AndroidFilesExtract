using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;

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
    }

    /// <summary>
    /// 文件操作的接口
    /// </summary>
    interface FileExtracter
    {
        Result InitConnection();
        Result GetFileInformation(string path);
        Result ListDirecotry(string path);
        Result SearchFiles(string path, string pattern, Type fileType);
        Result CopyFileFromDevice(string devPath, string pcPath);
    }

    class AdbFileExtracter:FileExtracter
    {
        string[] devices;

        FileProperty GetProperty(string path)
        {
            FileProperty property = new FileProperty();

            var items = AdbHelper.AdbHelper.ListDataFolder(path);
            string pattern = "No such file or directory";
            if (items[0].Contains(pattern))
                property.type = Type.fne;
            else
            {
                var rawData = AdbHelper.AdbHelper.GetProperty(path);
                property.path = path;
                property.modifyTime = rawData[5].Substring(8, rawData[5].Length - 8);
                property.accessTime = rawData[4].Substring(8, rawData[4].Length - 8);
                string[] tmp = rawData[1].Split(' ');
                property.size = tmp[1];
                switch (tmp[7])
                {
                    case "symbolic":break;
                }
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
                }
                else
                {
                    result.success = true;
                }

            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.ToString();
            }
            return result;
        }

        public Result GetFileInformation (string path)
        {

            Result result = new Result();
            try
            {
                result.filesProperty.Add(GetProperty(path));
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.errorMessage = ex.ToString();
            }
            return result;
        }


        public Result ListDirecotry(string path)
        {
            Result result = new Result();
            try
            {
                FileProperty property = GetProperty(path);
                if (property.type == Type.directory)
                {
                    var items = AdbHelper.AdbHelper.ListDataFolder(path);
                    foreach(var item in items)
                    {
                        property = GetProperty(path + "/" + item);
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

        public Result SearchFiles(string path, string pattern, Type fileType)
        {
            Result result = new Result();
            try
            {
                var items = AdbHelper.AdbHelper.SearchFiles(pattern, path, (char)fileType);
                FileProperty property = new FileProperty();
                foreach(var item in items)
                {
                    property = GetProperty(path + "/" + item);
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

        public Result CopyFileFromDevice(string devPath, string pcPath)
        {
            Result result = new Result();
            try
            {
                AdbHelper.AdbHelper.CopyFromDevice(devPath, pcPath);
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