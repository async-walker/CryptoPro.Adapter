using System.Diagnostics;

namespace CryptoPro.Adapter.CryptCP.Extensions
{
    internal static class CmdExtensions
    {
        public static string RunProcessCMD(this string cmd)
        {
            var processInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                FileName = "cmd.exe",
                Arguments = "/c " + cmd,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            var process = new Process
            {
                StartInfo = processInfo
            };

            process.Start();

            var log = string.Empty;

            while (!process.StandardOutput.EndOfStream)
            {
                var output = process.StandardOutput.ReadToEnd();
                log += output;
            }

            process.WaitForExit();
            process.Kill();

            return log;
        }
    }
}
