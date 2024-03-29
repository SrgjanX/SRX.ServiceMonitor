﻿//srgjanx

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SRX.ServiceMonitor.Utils
{
    internal static class ProcessExtensions
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            StringBuilder fileNameBuilder = new StringBuilder(buffer);
            uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
            return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength)
                ? fileNameBuilder.ToString()
                : null;
        }

        public static string TryGetMainModuleFileName(this Process process, int buffer = 1024)
        {
            try
            {
                return process.GetMainModuleFileName(buffer);
            }
            catch { }
            return null;
        }
    }
}