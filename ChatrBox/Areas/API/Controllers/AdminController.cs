#nullable disable
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Policy;

namespace ChatrBox.Areas.API.Controllers
{
    [Authorize]
    [Area("API")]
    public class AdminController : ControllerBase
    {
        private class LogRequest
        {
            public static List<string> ActiveLogs { get; set; } = new List<string>();
            public string LogFilepath
            {
                get => LogFilepath;
                set
                {
                    if (!ActiveLogs.Contains(value))
                        ActiveLogs.Add(value);
                    LogFilepath = value;
                }
            }
            public string LogMsg;

        }

        private static List<LogRequest> _LogQueue = new List<LogRequest>();
        public static string HomePath { get; set; }

        [HttpPost]
        public void Log(string logMsg, string path,
            string file = "default.log", int retries = 0)
        {
            if (logMsg == null) return;

            var logDir = Path.Combine(HomePath, "logs");

            if (!path.IsNullOrEmpty())
                logDir = Path.Combine(logDir, path);

            var logFile = Path.Combine(logDir, file);

            //makes the directory if it does not already exist
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);


            var rand = new Random();
            Thread.Sleep(rand.Next(1000, 3000));
            try
            {
                //create the file if it does not already exist
                if (!System.IO.File.Exists(logFile))
                    System.IO.File.Create(logFile);

                System.IO.File.AppendAllText(logFile, $"{logMsg}\n");
            }
            catch //delay than reattempt 
            {
                if (retries < 5)
                {
                    Thread.Sleep(rand.Next(10000, 40000));
                    Log(logMsg, path, file, ++retries);
                }
                else if (retries < 10)
                {
                    Thread.Sleep(rand.Next(10000, 120000));
                    Log(logMsg, path, file, ++retries);
                }

                return;
            }
        }
    }
}
