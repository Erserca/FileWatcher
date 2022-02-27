using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;

namespace Watcher
{
    class MyClassCS
    {
        static void Main(){
            
            using var watcher = new FileSystemWatcher(@"C:\");

            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Çıkış için ENTER'a tıklayınız...");
            Console.ReadLine();
        }

        
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed || e.FullPath == @"C:\Users\Erdo\source\repos\FileWatcher\hareket.txt")
            {
                return;
            }
            Console.WriteLine($"Değiştirilen: {e.FullPath}");
            File.AppendAllText(@"hareket.txt","Değiştirilme, " + e.FullPath + ", " + DateTime.Now + "\n");
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Oluşturulan: {e.FullPath}";
            Console.WriteLine(value);
            File.AppendAllText(@"hareket.txt","Oluşturulma, " + e.FullPath + ", " + DateTime.Now + "\n");
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e){
            Console.WriteLine($"Silinen: {e.FullPath}");
            File.AppendAllText(@"hareket.txt","Silinme, " + e.FullPath + ", " + DateTime.Now + "\n");
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Yeniden Adlandırma Eski: {e.OldFullPath} " + $" Yeni: {e.FullPath}");
            File.AppendAllText(@"hareket.txt","İsim Değiştirme, Eski: " + e.OldFullPath + ", Yeni: " + e.FullPath+ ", "+ DateTime.Now + "\n");
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}

/*


*/
