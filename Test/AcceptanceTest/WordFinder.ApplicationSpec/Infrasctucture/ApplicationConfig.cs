using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WordFinder.ApplicationSpec.Infrasctucture
{
    public class ApplicationConfig
    {
        private static string Path
        {
            get
            {
                return string.Format("{0}\\WordFinder.Application.exe", AppDomain.CurrentDomain.BaseDirectory);
            }
        }

        private static int ExecutionTimeout
        {
            get
            {
                return 5000;
            }
        }

        public static string ExecuteWithWord(string word)
        {
            var resultMessage = string.Empty;
            var wordFinderProccessStartInfo = new ProcessStartInfo
            {
                Arguments = word,
                FileName = Path,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };

            using (var wordFinderProccess = Process.Start(wordFinderProccessStartInfo))
            {
                wordFinderProccess.WaitForExit(ExecutionTimeout);
                wordFinderProccess.StandardInput.WriteLine();
                resultMessage = wordFinderProccess.StandardOutput.ReadToEnd();
            }
            return resultMessage;
        }
    }
}
