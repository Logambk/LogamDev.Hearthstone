using System;
using System.Collections.Generic;
using System.IO;
using LogamDev.Hearthstone.Services.Configuration;
using LogamDev.Hearthstone.Services.Interface;
using Newtonsoft.Json;

namespace LogamDev.Hearthstone.Services.Log
{
    public class TextFileLogger : ILogger
    {
        private object logFileLock = new object();

        public TextFileLogger()
        {
            lock (logFileLock)
            {
                if (File.Exists(Config.LoggingFileName))
                {
                    File.Delete(Config.LoggingFileName);
                }
            }
        }

        public void Log(LogType type, LogSeverity severity, string message, Dictionary<string, string> data = null)
        {
            if (Config.LoggingIsEnabled)
            {
                var dataStr = data != null ? JsonConvert.SerializeObject(data) : string.Empty;
                lock (logFileLock)
                {
                    using (var streamWriter = File.AppendText(Config.LoggingFileName))
                    {
                        streamWriter.WriteLine($"{DateTime.Now} {type} {severity} {message} {dataStr}");
                    }
                }
            }
        }
    }
}
