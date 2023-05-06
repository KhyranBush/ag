using System.Collections.Generic;
using static DungeonGame.Interfaces;
using static DungeonGame.Enemies;
using System;
using System.ComponentModel;

namespace DungeonGame
{


    public class Room :IRoomDelagate
    {
        private Dictionary<string, Door> exits;
        private string _tag;
        private Dictionary<string, Enemies> _enemies;
        private Dictionary<string, Merchant> _merchant;
        private Enemies Enemy;
        private Merchant Merchant;
        public void setEnemy(Enemies _enemy)
        {
            this.Enemy = _enemy;
        }
        public void setMerchant(Merchant _merchant)
        {
            this.Merchant = _merchant;
        }

        public Room RoomContainer { get; set; }
        
        private  IRoomDelagate _delegate;

        public IRoomDelagate Delegate
        {
            get
            {
                return _delegate;
            }
            set
            {
                _delegate = value;
                if (_delegate != null)
                {
                    _delegate.RoomContainer = this;
                }
            }
        }
        public Room() : this("No Tag")
        {
        }

        public Room(string tag = "No Tag")
        {
            _enemies = new Dictionary<string, Enemies>();
            _merchant = new Dictionary<string, Merchant>(); 
            exits = new Dictionary<string, Door>();
            this.tag = tag;
            _delegate = null;
            _Enemydelegate = null;
            _merchantDelagate = null;
            GItemContainer = new BackPack();
        }



        public string tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

       

        private IGameItemsContainer GItemContainer;



        

        public void drop(IGameItems GItem)
        {
            GItemContainer.put(GItem);

        }


        public IGameItems pickup(string GItemName)
        {
            return GItemContainer.remove(GItemName);
        }




        public string getGItems()
        {
            return GItemContainer.contents();

        }


        //------------------exits-------------------------
        public void setExit(string exitName, Door door)
        {
            exits[exitName] = door;
        }
        public Door getExit(string exitName)
        {
            if (_delegate != null)
            {
                return _delegate.getExit(exitName);
            }
            else
            {
                return getExit(exitName, null);
            }
        }

        public Door getExit(string exitName, IRoomDelagate rDelegate)
        {
            if (rDelegate == Delegate)
            {
                Door door = null;
                exits.TryGetValue(exitName, out door);
                return door;
            }
            else
            {
                return null;
            }
        }

        public string getExits()
        {
            if (_delegate != null)
            {
                return _delegate.getExits();
            }
            else
            {
                return getExits(null);
            }
        }

        public string getExits(IRoomDelagate rDelegate)
        {
            if (rDelegate == Delegate)
            {
                string exitNames = "Exits directions are:";
                Dictionary<string, Door>.KeyCollection keys = exits.Keys;
                foreach (string exitName in keys)
                {
                    exitNames += " " + exitName;
                }
                return exitNames;
            }
            else
            {
                return "???";
            }
        }


        public string description()
        {
            return "Your current location is " + this.tag + "\n" + this.getExits() + "\n" + this.getGItems();
        }




        //Traprooms


        public class TrapRoom : IRoomDelagate
        {
            public Room RoomContainer { get; set; }

            public string MagicWord { set; get; }
            public Door getExit(string exitName)
            {
                return null;
            }

            public TrapRoom()
            {
                MagicWord = "GoldCoin";
                //MagicWord = "mario";
                NotificationCenter.Instance.addObserver("PlayerWillSayWord", PlayerWillSayWord);
            }


            public string getExits()
            {
              Room room = this.RoomContainer;
                room.warningMessage("\nSinister evil first beGins with an Old, antiquated Love of finances\n Dont fall into despair, Consider a first sighting of finance\n Your high Ordered Intuiton knows best...\n knowledge lasts forever Notice the lapses and similarities ascender!\n");
                return RoomContainer.getExits(this);
            }


            public void PlayerWillSayWord(Notification notification)
            {

                Player player = (Player)notification.Object;
                if (player.currentRoom == RoomContainer)
                {
                    string word = (string)notification.userInfo["word"];
                    if (word != null)
                    {
                        if (word.Equals(MagicWord))
                        {
                            player.informationMessage("~ You did it! ~");
                            RoomContainer.getExits(this);
                        }
                        else
                        {
                            player.informationMessage("You said the wrong word!");
                            player.warningMessage("\n\nYou took" + 5 + "damage");
                            player.health -= 5;
                            player.warningMessage("\nHealth point" + player.health);
                        }
                    }
                }
            }

        }

        public class TrapRoomTwo : IRoomDelagate
        {
            public Room RoomContainer { get; set; }

            public string MagicWord { set; get; }
            public Door getExit(string exitName)
            {
                return null;
            }

            public TrapRoomTwo()
            {
                MagicWord = "Raven";
                NotificationCenter.Instance.addObserver("PlayerWillSayWord", PlayerWillSayWord);
            }


            public string getExits()
            {
                return RoomContainer.getExits(this) + "\nWhere does the light strike from?";

            }


            public void PlayerWillSayWord(Notification notification)
            {
                Player player = (Player)notification.Object;
                if (player.currentRoom == RoomContainer)
                {
                    string word = (string)notification.userInfo["word"];
                    if (word != null)
                    {
                        if (word.Equals(MagicWord))
                        {
                            
                            player.informationMessage("~ You did it! ~");
                            RoomContainer.Delegate = RoomContainer;
                        }
                    }
                }
            }

        }

        //------------------------------------------------Enemies and Battles--------------------------------
        
        //private Dictionary<string, Enemies> enemies;
        private IEnemyDelagate _Enemydelegate;
        public IEnemyDelagate EnemyDelegate
        {
            get
            {
                return _Enemydelegate;
            }
            set
            {
                _Enemydelegate = value;
                if (_Enemydelegate != null)
                {
                    _Enemydelegate.EnemyContainer = Enemy;
                }
            }
        }
       

        public void AddEnemy(string enemyName,int Health, int power,int coins, int exp, bool AliveorNOt)//Add a room assignment?
        {
            Enemy = new Enemies(" ", 0, 0, 0, 0, 0, false);

            _enemies[enemyName] = Enemy;
            Enemy.Name += enemyName;
            Enemy.Power += power;    
            Enemy.Coins += coins;
             Enemy.Experience += exp;
            Enemy.Health += Health;
            Enemy.IsTheEnemyAliveOrNot = AliveorNOt;
            

            
        }

        public Enemies GetEnemy(string enemyName)
        {
            if (_Enemydelegate != null)
            {
                return _Enemydelegate.GetEnemy(enemyName);
            }
            else
            {
                return GetEnemy(enemyName, null);
            }
        }

        public Enemies GetEnemy(string enemyName, IEnemyDelagate eDelegate)
        {
            if (eDelegate == EnemyDelegate)
            {
                Enemies enemy = null;
                _enemies.TryGetValue(enemyName, out enemy);
                return enemy;
            }
            else
            {
                return null;
            }
        }

        public string GetEnemies()
        {
            if (_Enemydelegate != null)
            {
                return _Enemydelegate.getEnemies();
            }
            else
            {
                return getEnemies(null);
            }
        }
        public string getEnemies(IEnemyDelagate eDelegate)
        {
            if (eDelegate == EnemyDelegate)
            {
                string enemyNames = "Enemies in room:";
                Dictionary<string, Enemies>.KeyCollection keys = _enemies.Keys;
                foreach (string enemyName in keys)
                {
                    enemyNames += " " + enemyName;
                }
                return enemyNames;
            }
            else
            {
                return "???";
            }
        }

        private IMerchantDelagate _merchantDelagate;
        public IMerchantDelagate MerchantDelagate
        {
            get
            {
              
                return _merchantDelagate;
            }
            set
            {
                _merchantDelagate = value;
                if (_merchantDelagate != null)
                {
                    _merchantDelagate.MerchantContainer = Merchant;
                }
            }
        }
        public void AddMerchant(string Name)
        {
            
            Merchant = new Merchant(Name);
            _merchant[Name]= Merchant;
            Merchant.Name = Name;
           
            if(Merchant == null)
            {
                warningMessage("critical error!");
            }
           
            
        }
        public Merchant GetMerchant(string merchantName)
        {
            if (_merchantDelagate != null)
            {
                return _merchantDelagate.GetMerchant(merchantName);
            }
            else
            {
                return GetMerchant(merchantName, null);
            }
        }

        public Merchant GetMerchant(string merchantName, IMerchantDelagate mDelegate)
        {
            if (mDelegate == MerchantDelagate)
            {
                Merchant merchant = Merchant;
                _merchant.TryGetValue(merchantName, out Merchant);
                if(_merchant == null)
                {
                    warningMessage("Please tell me who you're speaking of");
                }
                
                return merchant;
            }
            else
            {
                return Merchant;
            }
        }

        public string GetMerchants()
        {
            if (_merchantDelagate != null)
            {
                return _merchantDelagate.getMerchants();
            }
            else
            {
                return getMerchants(null);
            }
        }
        public string getMerchants(IMerchantDelagate mDelegate)
        {
            if (mDelegate == _merchantDelagate)
            {
                string merchantNames = "Merchants in Room: ";
                Dictionary<string, Merchant>.KeyCollection keys = _merchant.Keys;
                foreach (string merchantName in keys)
                {
                    merchantNames += " " + merchantName;
                }
                return merchantNames;
            }
            else
            {
                return "???";
            }
        }





        public class EndRoom : IRoomDelagate
        {

            public Room RoomContainer { get; set; }

            public Door getExit(string exitName)
            {
                return null;
            }

            public EndRoom()
            {

            }

            public string getExits()
            {
                return RoomContainer.getExits(this) + "\n*** You Win! ***" + "\nYou can type 'Quit' to leave.";
            }
          
        }
        //----------------------------Room Message Types----------------------------------------------
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

    }




}
