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
        public int damage = 1;
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
                damage += GItem.Damage;
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
                damage -= GItem.Damage;
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
            


            if (CombatEnemy != null && CombatEnemy.IsTheEnemyAliveOrNot == true)
            {
                int power = CombatEnemy.Power;
                int Hlth = CombatEnemy.Health;
                string name = Enemy;
               
                warningMessage(name + "is struck by your awesome power! The " + name + " strikes you back.");
                int damage = power - armorVal;
                if (damage < 0)
                    damage = 0;
                int attack = damage - power;
                warningMessage("You lose " + damage + "  health and you dealt " + attack + " damage");
                health -= damage;
                Hlth -= attack;
                if(CombatEnemy.Health == 0)
                {
                     CombatEnemy.IsTheEnemyAliveOrNot = false;
                }
            }
            else if(CombatEnemy!=null && CombatEnemy.IsTheEnemyAliveOrNot == false)
            {
             
                informationMessage("You have defeated" + Enemy + "You have gained\n" + CombatEnemy.Experience + "Experience\n" + CombatEnemy.Coins + "Coins\n");
                coins += CombatEnemy.Coins;
                experience += CombatEnemy.Experience;
                if(experience > expCap)
                {
                    LevelUp();

                }
            }
            else if(CombatEnemy == null)
            {
                informationMessage(CombatEnemy + "Does not exist!");
            }
            
            
        }
        public void LevelUp()
        {
            if (experience > expCap)
            {
                informationMessage("You have leveled up!");
                expCap += 50;
                health += 1;
                damage += 1;
                armorVal += 1;
                coins += 10;

            }
        }
       
        public void showStats()
        {
            //Hero hero = new Hero();


            informationMessage("\nPlayer Stats\n" + "\nHealth:\n " + health + "\nArmor Value (AV):\n " + armorVal + "\nDamage: \n" + damage + "\nEquipped Weapon: \n" + EquippedWeapon);
        }

        public void showInventory()

        {
            //Hero hero = new Hero();
            informationMessage("\nInventory.\n" + inventory.contents() + "\nCoins\n" + coins);

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
            informationMessage("Find a way to escape this dungeon before who knows what finds you!");
        }
    }



}
