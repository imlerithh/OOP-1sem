using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Lab_13
{
    class Program
    {
        static void Main(string[] args)
        {
            //FSIDiskInfo.DiskTotalFreeSpace(@"D:\");
            //FSIDiskInfo.DiskDriveFormat(@"D:\");
            //FSIDiskInfo.DisksGeneralInfo();

            //string path = "logfile.txt";
            //FSIFileInfo.FullPath(new FileInfo(path));
            //FSIFileInfo.LenExtName(new FileInfo(path));
            //FSIFileInfo.CreationAndChangeTime(new FileInfo(path));

            //path = @"D:\Education\course_2\Practice\OOP\Lab_13\Lab_13";
            //FSIDirInfo.NumberOfFiles(path);
            //FSIDirInfo.CreationTime(path);
            //FSIDirInfo.Directories(path);

            //FSIFileManager.A();
            //FSIFileManager.B(@"D:\Education\course_2\Practice\OOP\Lab_13\From", "txt");
            Console.WriteLine("Number of Logs: " + FSILog.NumberOfLogs());
            Console.Write("\nLogs by Day: "); string date = Console.ReadLine();
            Console.WriteLine(FSILog.LogsByDate(date));
            FSILog.SaveLogsForThisHour();
        }
    }
    public static class FSILog
    {
        public static void WriteLog(string action, DateTime dateTime, bool success)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("logfile.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("*LOG INFO*");
                    sw.WriteLine("Time: " + dateTime);
                    sw.WriteLine("Action: " + action);
                    sw.WriteLine("Success: " + success);
                    sw.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static int NumberOfLogs()
        {
            int count = 0;
            using (StreamReader sr = new StreamReader("logfile.txt", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith('*'))
                        count++;
                }
            }
            return count;
        }
        public static string LogsByDate(string date)
        {
            string output = "";
            using (StreamReader sr = new StreamReader("logfile.txt", System.Text.Encoding.Default))
            {
                string line;
                string[] words;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("Time:"))
                    {
                        words = line.Split(' ');
                        if (words[1].Equals(date))
                        {
                            output += line + '\n';
                            line = sr.ReadLine();
                            output += line + '\n';
                            line = sr.ReadLine();
                            output += line + "\n\n";
                        }
                    }
                }
            }
            return output;
        }
        public static void SaveLogsForThisHour()
        {
            int hour = DateTime.Now.Hour;
            string[] readText = File.ReadAllLines("logfile.txt");
            string[] words;
            using (StreamWriter sw = new StreamWriter("logfile.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < readText.Length; i++)
                {
                    if (readText[i].StartsWith("Time:"))
                    {
                        words = readText[i].Split(' ');
                        words = words[2].Split(':');
                        if (words[0].Equals(hour.ToString()))
                        {
                            sw.WriteLine("*LOG INFO*");
                            sw.WriteLine(readText[i]);
                            sw.WriteLine(readText[++i]);
                            sw.WriteLine(readText[++i] + '\n');
                        }
                    }
                }
            }
        } 
    }
    public static class FSIDiskInfo
    {
        private static DriveInfo[] drives = DriveInfo.GetDrives();
        public static void DiskTotalFreeSpace(string diskName)
        {
            bool success = false;
            for (int i = 0; i < drives.Length; i++)
            {
                if (drives[i].Name.Equals(diskName))
                {
                    Console.WriteLine($"Total Free Space on disk {diskName}  {drives[i].TotalFreeSpace}\n");
                    success = true;
                    break;
                }
                else if (i + 1 == drives.Length) Console.WriteLine($"There is no disk with this name - {diskName}" + "\n");
            }
            FSILog.WriteLog($"DiskTotalFreeSpace - {diskName}", DateTime.Now, success);
        }
        public static void DiskDriveFormat(string diskName)
        {
            bool success = false;
            for (int i = 0; i < drives.Length; i++)
            {
                if (drives[i].Name.Equals(diskName))
                {
                    Console.WriteLine($"Drive Format on disk {diskName}  {drives[i].DriveFormat}\n");
                    success = true;
                    break;
                }
                else if (i + 1 == drives.Length) Console.WriteLine($"There is no disk with this name - {diskName}" + "\n");
            }
            FSILog.WriteLog($"DiskDriveFormat - {diskName}", DateTime.Now, success);
        }
        public static void DisksGeneralInfo()
        {
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Name: {drive.Name}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Total Size: {drive.TotalSize}");
                    Console.WriteLine($"Total Free Space: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Volume Label: {drive.VolumeLabel}\n");
                }
            }
            FSILog.WriteLog($"DiskGeneralInfo", DateTime.Now, true);
        }
    }
        public static class FSIFileInfo
        {
            public static void FullPath(FileInfo fileInf)
            {
                bool success = false;
            if (fileInf.Exists) 
            {
                Console.WriteLine($"Full Path of {fileInf.Name} : {fileInf.DirectoryName}\n");
                success = true;
            }
            else Console.WriteLine($"File \"{fileInf.Name}\" doesn't exist" + "\n");

            FSILog.WriteLog($"FullPath - {fileInf.Name}", DateTime.Now, success);
            }
            public static void LenExtName(FileInfo fileInf)
            {
                bool success= false;
                if (fileInf.Exists)
                {
                    Console.WriteLine($"File Name : {fileInf.Name}");
                    Console.WriteLine($"File Length: {fileInf.Length}");
                    Console.WriteLine($"Full Extension: {fileInf.Extension}\n");
                    success = true;
                }
                else Console.WriteLine($"File \"{fileInf.Name}\" doesn't exist" + "\n");

                FSILog.WriteLog($"LenExtName - {fileInf.Name}", DateTime.Now, success);
            }
            public static void CreationAndChangeTime(FileInfo fileInf)
            {
                bool success = false;
                if (fileInf.Exists)
                {
                    Console.WriteLine($"Creation Time of {fileInf.Name} : {fileInf.CreationTime}");
                    Console.WriteLine($"Last Change Time of {fileInf.Name} : {fileInf.LastWriteTime}\n");
                    success = true;
                }
                else Console.WriteLine($"File \"{fileInf.Name}\" doesn't exist" + "\n");

                FSILog.WriteLog($"CreationAndChangeTime - {fileInf.Name}", DateTime.Now, success);
            }
        }
    public static class FSIDirInfo
    {
        public static void NumberOfFiles(string dirName)
        {
            bool success = false;
            if (Directory.Exists(dirName))
            {
                Console.WriteLine($"Number of Files in {dirName} : {Directory.GetFiles(dirName).Count()}\n");
                success = true;
            }
            else Console.WriteLine($"Directory \"{dirName}\" doesn't exist" + "\n");

            FSILog.WriteLog($"CreationAndChangeTime - {dirName}", DateTime.Now, success);
        }
        public static void CreationTime(string dirName)
        {
            bool success = false;
            if (Directory.Exists(dirName))
            {
                Console.WriteLine($"Creation Time of {dirName} : {new DirectoryInfo(dirName).CreationTime}\n");
                success = true;
            }
            else Console.WriteLine($"Directory \"{dirName}\" doesn't exist" + "\n");

            FSILog.WriteLog($"CreationTime - {dirName}", DateTime.Now, success);
        }
        public static void Directories(string dirName)
        {
            bool success = false;
            if (Directory.Exists(dirName))
            {
                success = true;
                Console.WriteLine("Directories:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                    Console.WriteLine(s);
                Console.WriteLine();
            }
            else Console.WriteLine($"Directory \"{dirName}\" doesn't exist" + "\n");

            FSILog.WriteLog($"Directories - {dirName}", DateTime.Now, success);
        }
    }
        public static class FSIFileManager
        {
            public static void A()
            {
            try
            {
                string path = @"D:\Education\course_2\Practice\OOP\Lab_13\Inspect";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                FileInfo fileInf = new FileInfo(@"D:\Education\course_2\Practice\OOP\Lab_13\Inspect\fdirinfo.txt");
                fileInf.Create().Dispose();
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(@"D:\Education\course_2\Practice\OOP\Lab_13\Inspect\dirinfo.txt", true);
                    fileInf.Delete();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            }
        public static void B(string fromPath, string extension)
        {
            try
            {
                string path = @"D:\Education\course_2\Practice\OOP\Lab_13\Files";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                if (Directory.Exists(fromPath))
                {
                    string[] files = Directory.GetFiles(fromPath);
                    foreach (string s in files)
                    {
                        if (s.EndsWith(extension))
                        {
                            FileInfo fileInf = new FileInfo(s);
                            string p = path + "\\" + Path.GetFileName(s);
                            if (fileInf.Exists)
                                fileInf.CopyTo(p, true);
                        }
                    }
                }
                dirInfo = new DirectoryInfo(@"D:\Education\course_2\Practice\OOP\Lab_13\Files");
                if (dirInfo.Exists)
                    dirInfo.MoveTo(@"D:\Education\course_2\Practice\OOP\Lab_13\Inspect\Files");
                ZipFile.CreateFromDirectory(@"D:\Education\course_2\Practice\OOP\Lab_13\Inspect\Files", @"D:\Education\course_2\Practice\OOP\Lab_13\Inspect\files.zip");
                ZipFile.ExtractToDirectory(@"D:\Education\course_2\Practice\OOP\Lab_13\Inspect\files.zip", @"D:\Education\course_2\Practice\OOP\Lab_13\ZipOut");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        }
}    
