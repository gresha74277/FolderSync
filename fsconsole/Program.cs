using FolderSyncLib;
using FolderSyncLib.Models;
using Syroot.Windows.IO;

namespace fsconsole
{
    internal class Program
    {
        static string wspace = string.Empty;
        static bool isPrint = false;
        static int copyFiles = 0;
        static int overwriteFiles = 0;
        static void Main(string[] args)
        {
            Loging.LogEvent += Loging_LogEvent;
            WiteSpaceInit();
            Console.CursorVisible = false;
            var startTime = DateTime.Now;
            var list = SyncFolderModel.LoadList();
            if (list == null)
            {
                PrintCenterInfo($"Отсутствует список файлов: [{SyncFolderModel.DefaultListFileName}]", ConsoleColor.Red);
                Thread.Sleep(3000);
                return;
            }
            else if (list.Count == 0)
            {
                PrintCenterInfo("Список файлов пуст", ConsoleColor.Red);
                Thread.Sleep(3000);
                return;
            }
            var updateList = new List<SyncFolderModel>();
            foreach (var folder in list)
            {
                if (folder.Settings.IsSkip) continue;
                Console.Title = $"Синхронизация папок: [{folder}]";
                StartCopy(folder);
                folder.Settings.LastUpdate = DateTime.Now;
                updateList.Add(folder);
            }
            SyncFolderModel.SaveList(updateList);
            Thread.Sleep(2000);
            Console.ResetColor();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Синхронезация завершена.\n\tСкопированно: [{copyFiles}]\n\tОбновленно: [{overwriteFiles}]\n\tВсего: [{copyFiles + overwriteFiles}]\n\tЗатраченое время: [{(DateTime.Now - startTime).ToString(@"dd\д\ hh\ч\ mm\м\ ss\с")}]");
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private static void Loging_LogEvent(Exception ex)
        {
            PrintException(ex.ToString(), ConsoleColor.DarkRed);
            File.AppendAllText(Path.Combine(KnownFolders.LocalAppData.Path, Path.ChangeExtension(SyncFolderModel.DefaultListFileName, ".log")), $"[{DateTime.Now}]{ex}\n");
        }

        private static void StartCopy(SyncFolderModel folder)
        {
            var work = true;
            Task.Factory.StartNew(() =>
            {
                var p = 0;
                while (work)
                {

                    for (int i = 0; i < Console.WindowWidth; i++)
                    {
                        if (!isPrint)
                        {
                            if (p == i)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            if (i > Console.WindowWidth) break;
                            else
                            {
                                Console.SetCursorPosition(i, 0);
                                Console.Write(">");
                                Console.SetCursorPosition(Console.WindowWidth - 1 - i, Console.WindowHeight - 1);
                                Console.Write("<");
                            }
                            Console.ResetColor();
                        }
                        else Console.ResetColor();
                    }
                    Thread.Sleep(10);
                    if (p++ > Console.WindowWidth) p = 0;
                }
            });
            CopyFolder(folder.SourceFolder, folder.DestinationFolder);
            work = false;
        }

        private static void CopyFolder(string source, string dest)
        {
            try
            {
                if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
                CopyFiles(Directory.GetFiles(source), dest);
                var innerFolders = Directory.GetDirectories(source);
                foreach (var innerFolder in innerFolders)
                {
                    var dirName = innerFolder.Substring(innerFolder.LastIndexOf('\\') + 1);
                    dest = Path.Combine(dest, dirName);
                    CopyFolder(innerFolder, dest);
                }
            }
            catch (Exception ex)
            {
                Loging.AddLog(ex);
            }

        }

        private static void CopyFiles(string[] fileList, string destPath)
        {
            try
            {
                if (fileList.Length == 0) return;
                foreach (var sourceFile in fileList)
                {
                    var destFile = Path.Combine(destPath, Path.GetFileName(sourceFile));
                    if (File.Exists(destFile))
                    {
                        if (new FileInfo(sourceFile).Length != new FileInfo(destFile).Length)
                        {
                            PrintInfo($"Обновление файла:[{Path.GetFileName(sourceFile)}] -> [{destFile}]", ConsoleColor.Yellow);
                            File.Delete(destFile);
                            File.Copy(sourceFile, destFile);
                            overwriteFiles++;
                        }
                        else continue;
                    }
                    else
                    {
                        PrintInfo($"Копирование файла:[{Path.GetFileName(sourceFile)}] -> [{destFile}]", ConsoleColor.Green);
                        File.Copy(sourceFile, destFile);
                        copyFiles++;
                    }
                }
            }
            catch (Exception ex)
            {
                Loging.AddLog(ex);
            }
        }

        private static void WiteSpaceInit()
        {
            var w = Console.WindowWidth;
            while (w-- > 0) wspace += " ";
        }

        private static void PrintCenterInfo(string text, ConsoleColor color)
        {
            isPrint = true;
            var midH = Console.WindowHeight / 2;
            Console.SetCursorPosition(0, midH - 1);
            Console.BackgroundColor = color;
            Console.WriteLine(wspace);
            Console.WriteLine(wspace);
            Console.WriteLine(wspace);
            Console.ResetColor();
            Console.ForegroundColor = color;
            var left = (Console.WindowWidth / 2) - (text.Length + 2) / 2;
            Console.SetCursorPosition(left, midH);
            Console.Write($" {text} ");

            Console.SetCursorPosition(0, 0);
            isPrint = false;
        }
        private static void PrintInfo(string text, ConsoleColor color)
        {
            isPrint = true;
            Console.ResetColor();
            Console.SetCursorPosition(0, 1);
            for(int i = Console.WindowHeight - 2; i > 1; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(wspace);
            }
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            isPrint = false;
        }
        private static void PrintException(string text, ConsoleColor color)
        {
            isPrint = true;
            Console.ResetColor();
            Console.SetCursorPosition(0, Console.WindowHeight / 2 + 1);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            isPrint = false;
        }
    }
}