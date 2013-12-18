using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace com.lexnetcrm.common
{
    public class UProcess
    {
        public static void executeCmdLineProcess(string exeToRun, string arguments, string workingDirectory)
        {
            Process proc = new Process();

            proc.StartInfo.FileName = exeToRun;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;

            if (String.IsNullOrEmpty(workingDirectory) == false)
            {
                proc.StartInfo.WorkingDirectory = workingDirectory;
            }

            proc.Start();

            string strOutput = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();
        }
    }
}
