using System;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class Player
    {
        public Player(string playerName, CardClass playerClass, int playerStartingHealth)
        {
            Armor = 0;
            Card = null;
            Class = playerClass;
            EquipedWeapon = null;
            Health = playerStartingHealth;
            Id = Guid.NewGuid();
            Name = playerName;
        }

        public int Armor { get; set; }
        public CardHero Card { get; set; }
        public CardClass Class { get; set; }
        public Weapon EquipedWeapon { get; set; }
        public int Health { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
