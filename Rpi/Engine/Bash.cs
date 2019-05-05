using System;
using System.Diagnostics;

namespace Rpi.Engine
{
    public static class Bash
    {
        public static bool Run(string cmd, out string exception)
        {
            try
            {
                var processInfo = new ProcessStartInfo();
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.FileName = "/bin/bash";
                processInfo.Arguments = $" -c \"{cmd}\"";
                var process = Process.Start(processInfo);
                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex.ToString();
                return false;
            }
        }


        public static string Invoke(string comand)
        {
            try
            {
                var processInfo = new ProcessStartInfo();
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardOutput = true;
                processInfo.FileName = "/bin/bash";
                processInfo.Arguments = $" -c \"{comand}\"";

                var process = Process.Start(processInfo);
                var outPut = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return outPut;
            }
            catch
            {
                return "";
            }
        }


        public static void Signal()
        {
            Run($"amixer set 'PCM' 100% unmute; aplay signal.wav", out _);
        }
    }
}
