class FileHandler
{
    const char directory = 'd';
    const char file = 'f';
    const char link = 'l';
    const char block = 'b';
    const char ch = 'c';
    const char sock = 's';

    struct FileProperty
    {
        int type;
        string path;
        string modifiedTime;
        string size;
    }

    public static bool IsExist(string path)
    {
        var info = AdbHelper.AdbHelper.ListDataFolder(path);
        string pattern = "No such file or directory";
        if (info[0].Contains(pattern))
            return false;
        return true;
    }
    
//     public static FileProperty(string path)
//     {
//         var info = AdbHelper.AdbHelper.ListDataFolder(path);
//         if (info.Length != 0)
//         {
//             switch (info[0])
//             {
//                 case 'd': return Type.directory;
//                 case '-': return Type.file;
//                 case 'l': return Type.linkfile;
//             }
//         }
//     }


     public static string[] Search(string path, int pattern, int type)
     {
         
     }
}