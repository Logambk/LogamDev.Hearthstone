using System.Collections.Generic;
using LogamDev.Hearthstone.Dto.HearthstoneJson;
using LogamDev.Hearthstone.Dto.Interface;
using Newtonsoft.Json.Linq;

namespace LogamDev.Hearthstone.Dto
{
    public class HearthstoneJsonCardParser : IHearthstoneJsonCardParser
    {
        public List<Card> Parse(JArray json)
        {
            if (json != null)
            {
                return json.ToObject<List<Card>>();
            }

            return null;
        }
    }
}
