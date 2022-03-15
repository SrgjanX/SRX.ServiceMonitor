//srgjanx

using SRX.ServiceMonitor.Models;
using SRX.ServiceMonitor.Properties;
using SRX.ServiceMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace SRX.ServiceMonitor.Controls
{
    public partial class ServicesPanel : UserControl
    {
        private Timer timer;

        public ServicesPanel()
        {
            InitializeComponent();
        }

        public void Load()
        {
            timer = new Timer(Settings.Default.RefreshSeconds * 1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    lblLoading.Visibility = Visibility.Hidden;
                });
                await Refresh();
                Dispatcher.Invoke(() =>
                {
                    lblBlock.Visibility = panelProcesses.Children.Count > 0 ? Visibility.Hidden : Visibility.Visible;
                });
            }
            catch (Exception ex)
            {
                timer.Dispose();
                MessageBox.Show(ex.Message + "\r\n" + ex?.InnerException?.Message, "Fatal error occurred", MessageBoxButton.OK, MessageBoxImage.Error);
                Dispatcher.Invoke(() =>
                {
                    Application.Current.Shutdown();
                });
            }
        }

        private async Task Refresh()
        {
            List<ProcessInfo> processes = await CheckProcesses();
            await UpdateProcessesUI(processes);
        }

        private async Task<List<ProcessInfo>> CheckProcesses()
        {
            List<ProcessInfo> processes = new List<ProcessInfo>();
            await Task.Run(() =>
            {
                ProcessManager processesManager = new ProcessManager();
                string[] lines = processesManager.GetProcesses();
                if (lines?.Any() == true)
                {
                    Process[] pname;
                    foreach (string process in lines)
                    {
                        ProcessInfo processInfo = new ProcessInfo()
                        {
                            ProcessName = processesManager.GetProcessName(process)
                        };
                        pname = Process.GetProcessesByName(processInfo.ProcessName);
                        processInfo.DisplayName = processesManager.GetDisplayName(process);
                        processInfo.FilePath = GetProcessFilePath(processInfo.ProcessName);
                        processInfo.Status = pname.Length != 0 ? ProcessStatus.Running : ProcessStatus.Stopped;
                        processes.Add(processInfo);
                    }
                }
            });
            return processes;
        }

        private async Task UpdateProcessesUI(List<ProcessInfo> processes)
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    panelProcesses.Children.Clear();
                    lblRunning.Content = processes?.Where(x => x.Status == ProcessStatus.Running)?.Count() ?? 0;
                    lblStopped.Content = processes?.Where(x => x.Status == ProcessStatus.Stopped)?.Count() ?? 0;
                    foreach (ProcessInfo processInfo in processes)
                    {
                        panelProcesses.Children.Add(InitializeProcessItem(processInfo));
                    }
                });
            });
        }

        private ProcessItem InitializeProcessItem(ProcessInfo processInfo)
        {
            return new ProcessItem(processInfo)
            {
                Width = 128,
                Height = 64,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(5)
            };
        }

        private string GetProcessFilePath(string processName)
        {
            Process[] process = Process.GetProcessesByName(processName);
            return process?.Length > 0
                ? process.First().TryGetMainModuleFileName()
                : null;
        }
    }
}