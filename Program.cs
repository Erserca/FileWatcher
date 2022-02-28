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
            string programYolu = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase );
            
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Console.WriteLine($"Değiştirilen: {e.FullPath}");
           
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Oluşturulan: {e.FullPath}";
            Console.WriteLine(value);
            
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e){
            Console.WriteLine($"Silinen: {e.FullPath}");
            
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Yeniden Adlandırma Eski: {e.OldFullPath} " + $" Yeni: {e.FullPath}");
            
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
