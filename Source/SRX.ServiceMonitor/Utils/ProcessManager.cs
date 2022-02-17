//srgjanx

using SRX.ServiceMonitor.Properties;
using System.IO;

namespace SRX.ServiceMonitor.Utils
{
    public class ProcessManager
    {
        private string Filepath => Settings.Default.ProcessNamesFilepath;

        //public void Add(string line)
        //{
        //    List<string> lines = File.ReadAllLines(Filepath).ToList();
        //    lines.Add(line);
        //    FormatAndSave(lines.ToArray());
        //}

        //public void Edit(string OldLine, string NewLine)
        //{
        //    List<string> lines = File.ReadAllLines(Filepath).ToList();
        //    for (int i = 0; i < lines.Count; i++)
        //    {
        //        if (lines[i] == OldLine)
        //            lines[i] = NewLine;
        //    }
        //    FormatAndSave(lines.ToArray());
        //}

        //public void Remove(string lineToRemove)
        //{
        //    List<string> lines = File.ReadAllLines(Filepath).ToList();
        //    lines.Remove(lineToRemove);
        //    FormatAndSave(lines.ToArray());
        //}

        //private void FormatAndSave(string[] lines)
        //{
        //    try
        //    {
        //        lines = lines.Where(x => !string.IsNullOrEmpty(x) && !x.StartsWith("#")).ToArray();
        //        List<string> newLines = new List<string>()
        //        {
        //            $"{Settings.Default.CommentCharacter}All lines starting with {Settings.Default.CommentCharacter} are comments.",
        //            string.Empty
        //        };
        //        foreach (string line in lines)
        //            newLines.Add(line);
        //        File.WriteAllLines(Settings.Default.ProcessNamesFilepath, newLines);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //public bool IsValidName(string Name)
        //{
        //    return Name != null && Name.Replace(" ", "").Length != 0;
        //}

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
    }
}