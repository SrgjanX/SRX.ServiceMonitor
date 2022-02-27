//srgjanx

using SRX.ServiceMonitor.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace SRX.ServiceMonitor.Utils
{
    public class ProcessManager
    {
        private string Filepath => Settings.Default.ProcessNamesFilepath;

        public string[] GetProcesses()
        {
            string[] lines = null;
            if (File.Exists(Filepath))
            {
                lines = File.ReadAllLines(Filepath);
                lines = lines.Where(x => !x.StartsWith(Settings.Default.CommentCharacter) && !string.IsNullOrEmpty(x)).ToArray();
            }
            return lines;
        }

        public string GetProcessName(string processName)
        {
            if(processName != null)
            {
                processName = processName.Split(';')[0];
            }
            return processName;
        }

        public string GetDisplayName(string processName)
        {
            if (processName != null)
            {
                string[] split = processName.Split(';');
                if(split.Length > 1)
                    processName = split[1].Trim(' ');
            }
            return processName;
        }

        public string GetText()
        {
            return File.ReadAllText(Filepath);
        }

        public void SetText(string text)
        {
            File.WriteAllText(Filepath, text);
        }

        public void OpenProcess(string filePath)
        {
            try
            {
                Process p1 = new Process();
                p1.StartInfo.FileName = filePath;
                p1.StartInfo.CreateNoWindow = true;
                p1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\nFile path: {filePath}", "Could not open process", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}