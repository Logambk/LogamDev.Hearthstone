using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using LogamDev.Hearthstone.Services.Log;

namespace LogamDev.Hearthstone.Services.Configuration
{
    public static class Config
    {
        private static Dictionary<string, string> conf = new Dictionary<string, string>();
        private const string DefaultLoggingFileName = "Log.txt";

        public static bool LoggingIsEnabled => GetBoolean(ConfigKeys.LoggingIsEnabled, true);
        public static LogSeverity LoggingMinimumSeverityLevel => LogSeverity.Error;
        public static string LoggingFileName => GetStr(ConfigKeys.LoggingFileName, DefaultLoggingFileName);

        private static bool GetBoolean(string key, bool defaultValue)
        {
            var strValue = GetStr(key);
            if (bool.TryParse(strValue, out var parsedValue))
            {
                return parsedValue;
            }

            return defaultValue;
        }

        private static string GetStr(string key, string defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            if (!conf.ContainsKey(key))
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                {
                    conf[key] = ConfigurationManager.AppSettings[key];
                }
                else
                {
                    conf[key] = defaultValue ?? string.Empty;
                }
            }

            return conf[key];
        }
    }
}
