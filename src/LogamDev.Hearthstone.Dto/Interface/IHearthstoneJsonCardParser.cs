using System.Collections.Generic;
using LogamDev.Hearthstone.Dto.HearthstoneJson;
using Newtonsoft.Json.Linq;

namespace LogamDev.Hearthstone.Dto.Interface
{
    public interface IHearthstoneJsonCardParser
    {
        List<Card> Parse(JArray json);
    }
}
