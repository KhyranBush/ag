using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using static DungeonGame.Interfaces;

namespace DungeonGame
{
    public class Enemies
    {
        private Dictionary<string, Enemies> enemies;
        public string Name = " ";
        public int Coins = 0;
        public int Health = 50;
        public int Damage = 1;
        public int Power = 1;
        public int Experience;
        public bool IsTheEnemyAliveOrNot;
        public Player Player;
        //private Room RoomEnemyIsLocated;

        public Dictionary<string, Enemies>.KeyCollection Keys { get; internal set; }

        public Enemies( string name, int power, int damage, int hlth, int exp, int coins, bool IsDefeated)
        {
            enemies = new Dictionary<string, Enemies>();
            name = Name;
            coins = Coins;
            power = Power;
            damage = Damage;
            hlth = Health;
            exp = Experience;
            IsDefeated = IsTheEnemyAliveOrNot;
      
        }
        
       

        
    }
}
