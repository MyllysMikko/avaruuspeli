using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaruuspeli
{
    internal class LogsManager
    {
        string textFile = "";

        public LogsManager()
        {
            string date = DateTime.Now.ToString();

            textFile = $"Logs/{date}.txt";
        }


        public void Log(string text, LogLevel logLevel)
        {
#if DEBUG
            string log = $"{DateTime.Now:T} {logLevel}: {text}";

            if (!File.Exists(textFile))
            {
                using (StreamWriter sw = File.CreateText(textFile))
                {
                    sw.WriteLine(log);
                    sw.Close();
                }

            }
            else
            {
                using (StreamWriter sw = File.AppendText(textFile))
                {
                    sw.WriteLine(log);
                    sw.Close();
                }
            }
#endif
        }

        public enum LogLevel
        {
            SUCCESS,
            ERROR,
            INFO
        }

    }
}
