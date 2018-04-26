using System.Collections.Generic;
using LogamDev.Hearthstone.Services.Log;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface ILogger
    {
        void Log(LogType type, LogSeverity severity, string message, Dictionary<string, string> data = null);
    }
}
