using System;
using System.IO;
using System.Text;

namespace Dash
{
    public class Logger
    {
        public static void Log(string message)
        {
            var logFile = Environment.CurrentDirectory + "\\crash_log.txt";
            StringBuilder sb = new StringBuilder();

            sb.Append(message);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.StackTrace);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            File.AppendAllText(logFile, sb.ToString());
        }
    }
}
