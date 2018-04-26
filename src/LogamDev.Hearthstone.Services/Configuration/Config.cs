using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LogamDev.Hearthstone.Services.Configuration
{
    public static class Config
    {
        private static Dictionary<string, string> conf = new Dictionary<string, string>();

        public static bool AllowLogging => GetBoolean(ConfigKeys.AllowLogging, true);

        private static bool GetBoolean(string key, bool defaultValue)
        {
            var strValue = GetStr(key);
            if (bool.TryParse(strValue, out var parsedValue))
            {
                return parsedValue;
            }

            return defaultValue;
        }

        private static string GetStr(string key)
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
                    conf[key] = string.Empty;
                }
            }

            return conf[key];
        }
    }
}
