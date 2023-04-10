using System;
using static DungeonGame.Interfaces;

namespace DungeonGame
{
    //The stats of the users characters
    class Hero
    {
        public int coins = 0;
        public int health = 50;
        public int damage = 1;
        public int armorVal = 0;
        public int potion = 5;
        public int weaponVal = 1;
        public float CarryCapacity = 150f;
        public int experience;
        public int expCap = 50; 
        public int level = 1;
        public IGameItems EquippedWeapon { get; set; }

    }
}

