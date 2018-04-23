using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LogamDev.Hearthstone.Dto.UnitTest.TestData
{
    public class TestDataVersions : IEnumerable<object[]>
    {
        private List<string> versions = new List<string>()
        {
            "23966-us"
        };

        private string GetCollectibleContent(string version) => TestData.GetStringContent($@"TestData\{version}\cards.collectible.json");
        private string GetNonCollectibleContent(string version) => TestData.GetStringContent($@"TestData\{version}\cards.json");

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var version in versions)
            {
                yield return new object[]
                {
                    JArray.Parse(GetCollectibleContent(version)),
                    JArray.Parse(GetNonCollectibleContent(version))
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
