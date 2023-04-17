using System;using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.XPath;namespace DungeonGame{
    //This is where we have the combat encounters in the game. Their are a total of 4 combat encounters (2 on each floor)
    //Different encounters offer different challenges and they each have their own set of health and damage they can deal.
    public class Encounters    {
        private Player player;
        private Enemies enemy;
        public Encounters(Player player, Enemies enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }
        public bool IsDefeated { get; set; }     
            public  void PlayerCombat(Player player, Enemies enemy)
            {
            Game game = new Game();
           

                Notification notification = new Notification();

                string n = "";
                int p = 0;
                int h = 0;
                int xp = 0;
                int currency = 0;
                bool defeat = false;


                n = enemy.Name;
                p = enemy.Power;
                h = enemy.Health;
                xp = enemy.Experience;
                currency = enemy.Coins;
                defeat = enemy.IsTheEnemyAliveOrNot;
            Random rand = new Random();

                
                while (defeat == false)
                {

                    while (h > 0 && this.player.health > 0)
                    {

                        defeat = false;
                        //This is where we were able to make the user input for what they can do in these encounters
                        //The plaer has 4 options: Attack, Defend, Heal,and Run
                        Console.WriteLine("" + n);
                        Console.WriteLine("Power level" + p + " Health level: " + h);
                        Console.WriteLine("=====================");
                        Console.WriteLine("|     ⚔ Attack ⚔     |");
                        Console.WriteLine("|     ✧ Defend ✧     |");
                        Console.WriteLine("|      ♥ Heal ♥      |");
                        Console.WriteLine("|      ? Run ?       |");
                        Console.WriteLine("=====================");
                        Console.WriteLine("Potions: " + this.player.potion + " Health: " + this.player.health + " Health: " + this.player.damage + this.player.EquippedWeapon.Damage);
                        string input = Console.ReadLine();
                        if (input.ToLower() == "A" || input.ToLower() == "Attack" || input.ToLower() == "a")
                        {
                            //Attack
                            Console.WriteLine("Your enemy is struck by your awesome power! The " + n + " strikes you back.");
                            int damage = p - this.player.armorVal;
                            if (damage < 0)
                                damage = 0;
                            int attack = this.player.damage - p;
                            Console.WriteLine("You lose " + damage + "  health and you dealt " + attack + " damage");
                            player.health -= damage;
                            h -= attack;
                        }
                        else if (input.ToLower() == "D" || input.ToLower() == "Defend" || input.ToLower() == "d")
                        {
                            //Defend
                            Console.WriteLine("You raise your guard and hope to live. " + n + " strikes you and deals less damage.");
                            int damage = (p / 4) - this.player.armorVal;
                            if (damage < 0)
                            {
                                damage = 0;
                            }
                            int attack = rand.Next(1, this.player.damage) / 2;
                            Console.WriteLine("You lose " + damage + "  health and you dealt " + attack + " damage");
                            this.player.health -= damage;
                            h -= attack;
                        }

                        else if (input.ToLower() == "H" || input.ToLower() == "Heal" || input.ToLower() == "h")
                        {
                            //Heal
                            if (this.player.potion == 0)
                            {
                                Console.WriteLine("You reach for a healing rune but it seems you ran out.");
                                int damage = p - player.armorVal;
                                if (damage < 0)
                                {
                                    damage = 0;
                                }
                                Console.WriteLine("You took " + damage + " damage from " + n);
                            }
                            else
                            {
                                int potVal = 5;
                                Console.WriteLine("You pulled out a healing rune and crush it in your hand.. You gained " + potVal + "health");
                                this.player.health += potVal;
                                Console.WriteLine("While healing " + n + " attacks you, dealing.");
                                int damage = (p / 2) - this.player.armorVal;
                                if (damage < 0)
                                {
                                    damage = 0;
                                }
                                Console.WriteLine("You took " + damage + " damage.");
                            }
                            Console.ReadKey();
                        }//Run
                        else if (input.ToLower() == "R" || input.ToLower() == "Run" || input.ToLower() == "r")
                        {
                            Console.WriteLine("Why would you think you can run away? You dont wanan run into the darkness.");
                        }
                        Console.ReadKey();
                        if (h <= 0)
                        {
                            defeat = true;
                        }

                    }
                    while (this.player.health <= 0)
                    {




                        Console.WriteLine("You have lost fair player,\n\n your soul will forever be lost in ether,\n\n Maybe there will be a shot at ascension next lifetime...\n");

                        game.end();


                    break;
                    }
              
                }
                while (defeat == true)
                {



                    this.player.coins = currency;
                    this.player.experience += xp;
                    defeat = true;
                    this.player.coins += currency;
                    if (player.experience >= player.expCap)
                    {
                        //player = (Player)notification.Object;
                        this.player.level += 1;
                        this.player.expCap += 50;
                        this.player.coins += currency;
                        Console.WriteLine("\n\nYou have now leveled up, you are now level:" + player.level + "\nyour maxiumum exp has went up to " + player.expCap);
                    }




                    Console.WriteLine("\n\nYou have defeated " + n + " You recieved " + xp + " experience and " + currency + " Eliune");
                    break;



                }



            }
        // Enemies 
        
            
    }
}