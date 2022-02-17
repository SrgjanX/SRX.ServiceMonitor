//srgjanx

using SRX.ServiceMonitor.Models;
using SRX.ServiceMonitor.Properties;
using SRX.ServiceMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private TimeSpan refreshTime;
        private TimeSpan GetRefreshTimeSpan = TimeSpan.FromSeconds(Settings.Default.RefreshSeconds);

        public ServicesPanel()
        {
            InitializeComponent();
        }

        public async Task Load()
        {
            refreshTime = GetRefreshTimeSpan;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            await Refresh();
            KeyDown += WinMain_KeyDown;
        }

        private async void WinMain_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F5)
            {
                await Refresh();
            }
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (refreshTime.TotalSeconds <= 0)
                {
                    await Refresh();
                }
                else
                {
                    refreshTime = refreshTime.Add(TimeSpan.FromMilliseconds(0 - timer.Interval));
                }
                Dispatcher.Invoke(() =>
                {
                    lblNextRefresh.Content = $"Next refresh in: {$"{(int)refreshTime.TotalSeconds}s."}";
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            finally
            {
                Dispatcher.Invoke(() =>
                {
                    lblBlock.Visibility = panelProcesses.Children.Count > 0 ? Visibility.Hidden : Visibility.Visible;
                });
            }
        }

        private async Task Refresh()
        {
            refreshTime = GetRefreshTimeSpan;
            List<ProcessInfo> processes = await CheckProcesses();
            await UpdateProcessesUI(processes);
        }

        private async Task<List<ProcessInfo>> CheckProcesses()
        {
            List<ProcessInfo> processes = new List<ProcessInfo>();
            await Task.Run(() =>
            {
                if (File.Exists(Settings.Default.ProcessNamesFilepath))
                {
                    string[] lines = File.ReadAllLines(Settings.Default.ProcessNamesFilepath);
                    lines = lines.Where(x => !x.StartsWith(Settings.Default.CommentCharacter) && !string.IsNullOrEmpty(x)).ToArray();
                    if (lines.Any())
                    {
                        Process[] pname;
                        ProcessManager processesManager = new ProcessManager();
                        foreach (string process in lines)
                        {
                            pname = Process.GetProcessesByName(processesManager.GetProcessName(process));
                            bool isRunning = pname.Length != 0;
                            processes.Add(new ProcessInfo()
                            {
                                Name = processesManager.GetDisplayName(process),
                                Status = isRunning ? ProcessStatus.Running : ProcessStatus.Stopped
                            });
                        }
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
                    lblServiceCount.Content = "Running: 0, Stopped: 0";
                });
                Dispatcher.Invoke(() =>
                {
                    foreach (ProcessInfo processInfo in processes)
                    {
                        ProcessItem processItem = new ProcessItem()
                        {
                            Width = 128,
                            Height = 64,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Margin = new Thickness(5, 5, 5, 5),
                            DispalyName = processInfo.Name
                        };
                        processItem.SetStatus(processInfo.Status);
                        panelProcesses.Children.Add(processItem);
                    }
                    int running = processes?.Where(x => x.Status == ProcessStatus.Running)?.Count() ?? 0;
                    int stopped = processes?.Where(x => x.Status == ProcessStatus.Stopped)?.Count() ?? 0;
                    lblServiceCount.Content = $"Running: {running}, Stopped: {stopped}";
                });
            });
        }
    }
}