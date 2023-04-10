using System.Collections;
using System.Collections.Generic;
using System;
using static DungeonGame.Interfaces;

namespace DungeonGame
{
    public class Player
    {
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
            
            _currentRoom = room;
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
            IGameItems GItem = currentRoom.pickup(gameItem);
            BackPack bp = new BackPack();
            
        
            Hero hero = new Hero();
            
            if (inventory.contains(GItem) == true && GItem.IsUsable != false)
            { 
                hero.EquippedWeapon = GItem;
                GItem.Name = GItem.Name;
                hero.armorVal += GItem.ArmorValue;
                hero.health += GItem.Health;
                hero.damage += GItem.Damage;
            }
            else if(!inventory.contains(GItem) == false) 
            {
                hero.EquippedWeapon = null;
                informationMessage("weapon doesnt exist");
            }

           
        }
        public void showInventory()

        {
            Hero hero = new Hero();
            informationMessage("\nInventory.\n" + inventory.contents()+ "\nCoins\n" + hero.coins);
          
        }

        public void pickup(string gameItems)
        {
            
            IGameItems GItem = currentRoom.pickup(gameItems);
            if(GItem != null)
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
