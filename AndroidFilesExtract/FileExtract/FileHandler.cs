using System;
using System.Collections.Generic;

namespace FileExtracter
{
    /// <summary>
    /// linux下的7种文件类型
    /// 不过在data目录中应该只有部分类型会存在
    /// </summary>
    public enum Type
    {
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
    struct FileProperty
    {
        Type type;
        string path;
        string size;
        string createTime;
        string modifyTime;
    }

    /// <summary>
    /// 文件操作的接口
    /// </summary>
    interface FileExtracter
    {
        bool IsExist(string path);
        FileProperty GetFileInformation(string path);
        List<FileProperty> ListDirecotry(string path);
        List<FileProperty> SearchFiles(string path, string pattern, Type fileType);
        void CopyFileFromDevice(string path);
        void CreateFileTree(string path);
    }

     class AdbFileExtracter:FileExtracter
     {

         public bool IsExist(string path)
         {
             var info = AdbHelper.AdbHelper.ListDataFolder(path);
             string pattern = "No such file or directory";
             if (info[0].Contains(pattern))
                 return false;
             return true;
         }

         public FileProperty GetFileInformation (string path)
         {
            FileProperty property = new FileProperty();
            var items = AdbHelper.AdbHelper.GetProperty(path);

            return property;
         }


         public string[] Search(string path, int pattern, int type)
         {

         }
     }
}