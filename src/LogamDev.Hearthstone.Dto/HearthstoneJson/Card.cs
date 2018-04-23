using System.Collections.Generic;
using Newtonsoft.Json;

namespace LogamDev.Hearthstone.Dto.HearthstoneJson
{
    public class Card
    {
        [JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

        [JsonProperty(PropertyName = "armor")]
        public int? Armor { get; set; }

        [JsonProperty(PropertyName = "attack")]
        public int? Attack { get; set; }

        [JsonProperty(PropertyName = "cardClass")]
        public CardClass? Class { get; set; }

        [JsonProperty(PropertyName = "classes")]
        public List<CardClass> Classes { get; set; }

        [JsonProperty(PropertyName = "collectible")]
        public bool? IsCollectible { get; set; }

        [JsonProperty(PropertyName = "collectionText")]
        public string CollectionText { get; set; }

        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; set; }

        [JsonProperty(PropertyName = "dbfId")]
        public int? DbfId { get; set; }

        [JsonProperty(PropertyName = "durability")]
        public int? Durability { get; set; }

        [JsonProperty(PropertyName = "elite")]
        public bool? IsElite { get; set; }

        [JsonProperty(PropertyName = "entourage")]
        public List<string> Entourage { get; set; }

        [JsonProperty(PropertyName = "faction")]
        public Faction? Faction { get; set; }

        [JsonProperty(PropertyName = "flavor")]
        public string Flavor { get; set; }

        [JsonProperty(PropertyName = "health")]
        public int? Health { get; set; }

        [JsonProperty(PropertyName = "hideStats")]
        public bool? IsHideStats { get; set; }

        [JsonProperty(PropertyName = "howToEarn")]
        public string HowToEarn { get; set; }

        [JsonProperty(PropertyName = "howToEarnGolden")]
        public string HowToEarnGolden { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "mechanics")]
        public List<GameTag> Mechanics { get; set; }

        [JsonProperty(PropertyName = "multiClassGroup")]
        public MultiClassGroup? MultiClassGroup { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "overload")]
        public int? Overload { get; set; }

        [JsonProperty(PropertyName = "playRequirements")]
        public Dictionary<PlayReq, int> Requirements { get; set; }

        [JsonProperty(PropertyName = "questReward")]
        public string QuestReward { get; set; }

        [JsonProperty(PropertyName = "race")]
        public Race? Race { get; set; }

        [JsonProperty(PropertyName = "rarity")]
        public Rarity? Rarity { get; set; }

        [JsonProperty(PropertyName = "referencedTags")]
        public List<GameTag> ReferencedTags { get; set; }

        [JsonProperty(PropertyName = "set")]
        public CardSet? Set { get; set; }

        [JsonProperty(PropertyName = "spellDamage")]
        public int? SpellDamage { get; set; }

        [JsonProperty(PropertyName = "targetingArrowText")]
        public string TargetingArrowText { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "type")]
        public CardType? Type { get; set; }
    }
}
