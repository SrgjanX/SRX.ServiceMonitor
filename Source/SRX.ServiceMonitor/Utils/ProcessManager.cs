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
                lines = lines.Where(x => !x.StartsWith(Settings.Default.CommentCharacter.ToString()) && !string.IsNullOrEmpty(x)).ToArray();
            }
            return lines;
        }

        public string GetProcessName(string processName)
        {
            return processName?.Split(Settings.Default.SeperatorCharacter)[0];
        }

        public string GetDisplayName(string processName)
        {
            if (processName != null)
            {
                string[] split = processName.Split(Settings.Default.SeperatorCharacter);
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
                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\nFile path: {filePath}", "Could not open process", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}