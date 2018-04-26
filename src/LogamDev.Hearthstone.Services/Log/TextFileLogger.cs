using System;
using System.Collections.Generic;
using System.IO;
using LogamDev.Hearthstone.Services.Interface;
using Newtonsoft.Json;

namespace LogamDev.Hearthstone.Services.Log
{
    public class TextFileLogger : ILogger
    {
        private const string DefaultFileName = "Log.txt";

        public TextFileLogger()
        {
            if (!File.Exists(DefaultFileName))
            {
                File.Create(DefaultFileName);
            }
        }

        public void Log(LogType type, LogSeverity severity, string message, Dictionary<string, string> data = null)
        {
            if (File.Exists(DefaultFileName))
            {
                var dataStr = data != null ? JsonConvert.SerializeObject(data) : string.Empty;
                using (var streamWriter = File.AppendText(DefaultFileName))
                {
                    streamWriter.WriteLine($"{DateTime.Now}, {type}, {severity}, {message}, {dataStr}");
                }
            }
        }
    }
}
