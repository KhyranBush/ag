using System.Collections;
using System.Collections.Generic;
using System;
using static DungeonGame.Interfaces;
using System.Numerics;

namespace DungeonGame
{
    public class Player
    {
        public int coins = 0;
        public int health = 50;
        public int Damage = 1;
        public int armorVal = 0;
        public int potion = 5;

        public float CarryCapacity = 150f;
        public int experience;
        public int expCap = 50;
        public int level = 1;
        Enemies enemy;
        public IGameItems EquippedWeapon { get; set; }


        List<Room> haveBeen = new List<Room>();
        List<Room> forBack = new List<Room>();

        private Room _currentRoom = null;
        public Room currentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }

        public string Name { set; get; }

        private IGameItemsContainer inventory;

        public Player(Room room)
        {


            Name = "";
            inventory = new BackPack();

        }



        public void give(IGameItems gameItems)
        {

            inventory.put(gameItems);

        }
        public IGameItems take(string gameItems)
        {
            return inventory.remove(gameItems);

        }

        public void EquipWeapon(string gameItem)
        {
            try
            {
                IGameItems GItem = inventory.getItems(gameItem);
                EquippedWeapon = GItem;
                armorVal += GItem.ArmorValue;
                health += GItem.Health;
                Damage += GItem.Damage;
                informationMessage(GItem + "Has Been Equiped!");
            }
            catch (ArgumentException ex)
            {

                EquippedWeapon = null;
                informationMessage(ex.Message + "\nThere are no weapons with the name entered please try again\n");
            }


        }
        public void UnequipWeapon(string gameItem)
        {
            try
            {
                IGameItems GItem = inventory.getItems(gameItem);
                EquippedWeapon = GItem;
                armorVal -= GItem.ArmorValue;
                health -= GItem.Health;
                Damage -= GItem.Damage;
                inventory.put(GItem);
                informationMessage(GItem + "Has Been unEquiped!");

            }
            catch (ArgumentException ex)
            {

                EquippedWeapon = null;
                informationMessage(ex.Message + "\nThere are no weapons with the name entered please try again\n");
            }


        }
        public void AttackAnEnemy(string Enemy)
        {

            Enemies CombatEnemy = this._currentRoom.GetEnemy(Enemy);
            bool defeat = CombatEnemy.IsTheEnemyAliveOrNot;

            warningMessage("Are you sure you want to attack? y or n");
            string attackPrompt = Console.ReadLine();
            Notification notification = new Notification();

            string n = "";
            int p = 0;
            int h = 0;
            int xp = 0;
            int currency = 0;



            n = CombatEnemy.Name;
            p = CombatEnemy.Power;
            h = CombatEnemy.Health;
            xp = CombatEnemy.Experience;
            currency = CombatEnemy.Coins;
            defeat = CombatEnemy.IsTheEnemyAliveOrNot;
            Random rand = new Random();



            while (defeat == false)
            {


                while (h > 0 && health > 0)
                {
                    bool run = false;

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
                    Console.WriteLine("Potions: " + potion + " Health: " + health + " Damage Numeber: " + Damage);
                    string input = Console.ReadLine();
                    if (input.ToLower() == "A" || input.ToLower() == "Attack" || input.ToLower() == "a")
                    {
                        //Attack
                        Console.WriteLine("Your enemy is struck by your awesome power! The " + n + " strikes you back.");
                        int damage = p - armorVal;
                        if (damage < 0)
                            damage = 0;
                        int attack = Damage - p;
                        Console.WriteLine("You lose " + damage + "  health and you dealt " + attack + " damage");
                        health -= damage;
                        h -= attack;
                        if (attack >= 0)
                            attack = 0;
                    }
                    else if (input.ToLower() == "D" || input.ToLower() == "Defend" || input.ToLower() == "d")
                    {
                        //Defend
                        Console.WriteLine("You raise your guard and hope to live. " + n + " strikes you and deals less damage.");
                        int damage = (p / 4) - armorVal;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        int attack = rand.Next(1, Damage) / 2;
                        Console.WriteLine("You lose " + damage + "  health and you dealt " + attack + " damage");
                        health -= damage;
                        h -= attack;
                    }

                    else if (input.ToLower() == "H" || input.ToLower() == "Heal" || input.ToLower() == "h")
                    {
                        //Heal
                        if (potion == 0)
                        {
                            Console.WriteLine("You reach for a healing rune but it seems you ran out.");
                            int damage = p - armorVal;
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
                            health += potVal;
                            Console.WriteLine("While healing " + n + " attacks you, dealing.");
                            int damage = (p / 2) - armorVal;
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
                        run = true;
                        int RunPenalty = 10;
                        health -= RunPenalty;
                        if (run == true)
                        {
                            defeat = true;

                            Console.WriteLine("Why would you think you can run away? You dont wanan run into the darkness do you? fine flee like a fool!");
                            informationMessage("You ran to the previous room, prepare for battle more thoroughly next time!");
                            warningMessage("You have lost " + RunPenalty + " Health...");
                            CombatEnemy.Health = 0;

                            back();

                        }


                        break;
                    }
                    Console.ReadKey();
                    if (h <= 0)
                    {
                        defeat = true;
                    }


                    while (health <= 0)
                    {

                        Game game = new Game();


                        Console.WriteLine("You have lost fair player,\n\n your soul will forever be lost in ether,\n\n Maybe there will be a shot at ascension next lifetime...\n");

                        game.end();


                        break;
                    }


                    while (attackPrompt.Equals("n"))
                    {
                        back();
                        warningMessage("\nYou have fled from an enemy SHAME ON you!\n");

                    }
                    while (defeat == true && run == false)
                    {




                        experience += xp;
                        defeat = true;
                        coins += currency;
                        informationMessage("You have defeated" + n + "And have gained" + coins + "Coins and " + experience + " Experience");
                        if (experience >= expCap)
                        {
                            LevelUp();
                        }




                        Console.WriteLine("\n\nYou have defeated " + n + " You recieved " + xp + " experience and " + currency + " Eliune");
                        break;



                    }

                    while (defeat == true && run == true)
                    {
                        h = 0;
                        defeat = true;
                        break;

                    }

                }
            }

        }


        public void LevelUp()
        {
            if (experience > expCap)
            {
                informationMessage("You have leveled up!");
                level += 1;
                expCap += 50;
                health += 1;
                Damage += 1;
                armorVal += 1;
                coins += 10;

            }
        }

        public void showStats()
        {
            //Hero hero = new Hero();


            informationMessage("\nPlayer Stats\n" + "\nHealth:\n " + health + "\nArmor Value (AV):\n " + armorVal + "\nDamage: \n" + Damage + "\nEquipped Weapon: \n" + EquippedWeapon);
        }

        public void showInventory()

        {
            //Hero hero = new Hero();
            informationMessage("\nInventory.\n" + inventory.contents() + "\nCoins\n" + coins);

        }
        public void ShowWares(string Merchant)
        {
            Merchant merchant = this._currentRoom.GetMerchant(Merchant);
            
            if (merchant == null)
            {
                warningMessage(Merchant + "is not in this room..");


            }
            else
            {
                merchant.showInventory(); 
            }

        }
       
       
        public void BuyWares(string Merchant, string GitemName)
        {
            Merchant merchant = this._currentRoom.GetMerchant(Merchant);
            if (merchant!= null && merchant.getItems(GitemName) != null)
            {
                inventory.put(merchant.getItems(GitemName));
                merchant.RemoveItemsWhenBought(GitemName);
                informationMessage("You have purchased " + GitemName + " From " + merchant.Name);
            }
        }
        public void pickup(string gameItems)
        {

            IGameItems GItem = currentRoom.pickup(gameItems);
            if (GItem != null)
            {
                if (GItem.CarryCapacity > GItem.Weight)
                {
                    warningMessage("\nItem " + gameItems + " is too big to carry.");
                    currentRoom.drop(GItem);
                }

                else
                {
                    give(GItem);
                    informationMessage("\nThe item " + gameItems + " is now in your inventory.");
                }

            }
            else
            {
                warningMessage("\nItem " + gameItems + " does not exist in this room.");
            }
        }

        public void drop(string gameItems)
        {
            IGameItems GItem = take(gameItems);
            if (GItem != null)
            {
                currentRoom.drop(GItem);
                informationMessage("\nThe item " + gameItems + " is now in the room.");
            }
            else
            {
                warningMessage("\nThe item " + gameItems + " is not in your inventory");
            }
        }


        public void waltTo(string direction)
        {
            Door door = this._currentRoom.getExit(direction);
            if (door != null)
            {
                haveBeen.Add(currentRoom);
                forBack.Add(currentRoom);
                if (door.State == OCState.Open)
                {
                    Room nextRoom = door.getRoom(_currentRoom);
                    NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterRoom", this));
                    this._currentRoom = nextRoom;
                    NotificationCenter.Instance.postNotification(new Notification("PlayerDidEnterRoom", this));
                    this.informationMessage("\n" + this._currentRoom.description());
                }
                else
                {
                    this.warningMessage("\nThe door on  " + direction + " is not open.");
                }

            }
            else
            {
                this.warningMessage("\nThe is no door on " + direction);
            }
        }


        public void say(string word)
        {
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillSayWord", this, userInfo));
            informationMessage("\n>>>" + word + "\n");
            NotificationCenter.Instance.postNotification(new Notification("PlayerDidSayWord", this, userInfo));
        }

        public void name(string newName)
        {
            Name = newName;
        }

        public void open(string direction)
        {
            Door door = this._currentRoom.getExit(direction);
            if (door != null)
            {
                if (door.State == OCState.Open)
                {
                    this.warningMessage("\nThe door on " + direction + " is already open.");
                }
                else
                {
                    if (door.open().State == OCState.Open)
                    {
                        this.informationMessage("\nThere door on  " + direction + " is now open.");
                    }
                    else
                    {
                        this.warningMessage("\nThe door on " + direction + " did NOT open.");
                    }

                }

            }
            else
            {
                this.warningMessage("\nThere is no door on " + direction);
            }
        }

        public void exit()
        {
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["state"] = new ParserNormalState();
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
            this.informationMessage("\n" + this._currentRoom.description());
        }
        public void start(string state)
        {
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            if (state.Equals("battle"))
            {
                userInfo["state"] = new ParserBattleState();
            }
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
        }

        public void outputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void coloredMessage(string message, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            outputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void debugMessage(string message)
        {
            coloredMessage(message, ConsoleColor.DarkRed);
        }

        public void warningMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Red);
        }

        public void informationMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Yellow);
        }

        //back command
        public void back()
        {
            if (forBack.Count > 0)
            {
                currentRoom = forBack[forBack.Count - 1];
                forBack.Remove(forBack[forBack.Count - 1]);
                this.informationMessage("\n" + this._currentRoom.description());
            }
            else
            {
                this.outputMessage("\n\nThere is no where to return to");
            }
        }

        public void quest()
        {
            informationMessage("Ascend to the top of the tower, and find the Truth of All Truths");
        }
    }



}


