using System.Collections.Generic;
using System.IO;

namespace LogamDev.Hearthstone.Dto.UnitTest.TestData
{
    public static class TestDataProvider
    {
        private static Dictionary<string, string> jsons = new Dictionary<string, string>();

        public static string GetStringContent(string path)
        {
            if (!jsons.ContainsKey(path))
            {
                jsons[path] = File.ReadAllText(path);
            }

            return jsons[path];
        }
    }
}
