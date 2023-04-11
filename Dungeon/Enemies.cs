using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public class Enemies
    {
        public string Name = " ";
        public int Coins = 0;
        public int Health = 50;
        public int Damage = 1;
        public int Power = 1;
        public int Experience;
        public bool IsTheEnemyAliveOrNot;
   


        public Enemies(string name,int power,  int damage,  int hlth,  int exp,  int coins, bool IsDefeated)
        {
            name = Name;
            coins = Coins;
            power = Power;
            damage = Damage;
            hlth = Health;
            exp = Experience;
            IsDefeated = IsTheEnemyAliveOrNot;
        }
        public static void FirstEncounter()
        {
            Console.WriteLine("\nIn far corner of the room the light effigies collide forming a spirit irradiating the same luminescent glow present on the walls");
            Console.WriteLine("\nAs it animates, it notices your prescence and transforms in color from a blueish hue to a deep burning crimson");
            Console.WriteLine("\nThe spirit speaks with its metallic and disturbing voice that slowly drones in and out\n Your death marks the end of another foolish ascension\n" +
                                "prepare to meet the Loa once more impudent Ascender!");
            Console.WriteLine("\nThis is your first trial young hero, Destroy or be Destroyed.. KIll or be Killed!");
            Console.ReadKey();
            Enemies enemy1 = new Enemies("Ethereal Spirit Soldier: Rohin the first Ascender", 200, 5, 10, 4, 10, false);
            
        }

        public static void SecondEncounter()
        {
            Console.WriteLine("\nAs you walk through the dungeon You stumble upon a heap of armor");
            Console.WriteLine("\nIt assembles as it looks in your direction and mystic black mist flows from its helm ....");
            Console.WriteLine("\nIt rusheds toward you filled with the intent to destroy, Now is the time for action, fight young hero!");
            Console.ReadKey();
            Enemies enemy2 = new Enemies("Skeleton man 2.0", 7, 10, 11, 100,10, false);
        }

        public static void ThirdEncounter()
        {
            Console.WriteLine("\nLooks like you've stumbled upon another enemy...");
            Console.WriteLine("\nThis is a brute known only for destruction and violence!");
            Console.WriteLine("\nVictory is slim and death is almost certain as you fight the...");
            Console.ReadKey();
            Enemies enemy3 = new Enemies("Rancor", 10, 10, 10, 200,10, false);
        }

        public static void FinalEncounter()
        {
            Console.WriteLine("\nLooks like you've stumbled upon another enemy...");
            Console.WriteLine("\nAkuma has been looking for a worthy challenge.");
            Console.WriteLine("\nDeath is almost certainly assured");
            Console.ReadKey();
            Enemies enemy4 = new Enemies("Akuma, The Raging Demon!", 20, 30, 20, 200,20, false)
;
        }
    }

}
