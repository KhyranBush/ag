using System.Collections.Generic;
using static DungeonGame.Interfaces;
using static DungeonGame.Enemies;
using System;

namespace DungeonGame
{


    public class Room
    {
        private Dictionary<string, Door> exits;
        private string _tag;
        private Dictionary<string, Enemies> _enemies;

        private IRoomDelagate _delegate;

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
                    _delegate.Container = this;
                }
            }
        }
        public Room() : this("No Tag")
        {
        }

        public Room(string tag = "No Tag")
        {
            _enemies = new Dictionary<string, Enemies>();

            exits = new Dictionary<string, Door>();
            this.tag = tag;
            _delegate = null;
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

        //End of Delegate section
        //beginning of Items interfaces

        private IGameItemsContainer GItemContainer;



        //public string Tag { get; set; }


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
            public Room Container { get; set; }

            public string MagicWord { set; get; }
            public Door getExit(string exitName)
            {
                return null;
            }

            public TrapRoom()
            {
                MagicWord = "Mario";
                //MagicWord = "mario";
                NotificationCenter.Instance.addObserver("PlayerWillSayWord", PlayerWillSayWord);
            }


            public string getExits()
            {
                return Container.getExits(this) + "\nYou cant leave until you solve this.\nWho am I?\nI jump high but I am not Michael Jordan.\nI have gloves but I am not Mickey Mouse\nI have a mustache but I am not Dr Robotnik";

            }


            public void PlayerWillSayWord(Notification notification)
            {

                Player player = (Player)notification.Object;
                if (player.currentRoom == Container)
                {
                    string word = (string)notification.userInfo["word"];
                    if (word != null)
                    {
                        if (word.Equals(MagicWord))
                        {
                            player.informationMessage("~ You did it! ~");
                            Container.Delegate = null;
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
            public Room Container { get; set; }

            public string MagicWord { set; get; }
            public Door getExit(string exitName)
            {
                return null;
            }

            public TrapRoomTwo()
            {
                MagicWord = "Gummy bear";
                NotificationCenter.Instance.addObserver("PlayerWillSayWord", PlayerWillSayWord);
            }


            public string getExits()
            {
                return Container.getExits(this) + "\nYou cant leave until you solve this.\nWhat do you call a bear with no teeth?";

            }


            public void PlayerWillSayWord(Notification notification)
            {
                Player player = (Player)notification.Object;
                if (player.currentRoom == Container)
                {
                    string word = (string)notification.userInfo["word"];
                    if (word != null)
                    {
                        if (word.Equals(MagicWord))
                        {
                            player.informationMessage("~ You did it! ~");
                            Container.Delegate = null;
                        }
                    }
                }
            }

        }
        public class EntranceToFloorTwo : IRoomDelagate
        {
            public Room Container { get; set; }

            public string MagicWord { set; get; }
            public Door getExit(string exitName)
            {
                return null;
            }

            public EntranceToFloorTwo()
            {
                MagicWord = "Cain";
                //MagicWord = "mario";
                NotificationCenter.Instance.addObserver("PlayerWillSayWord", PlayerWillSayWord);
            }


            public string getExits()
            {
                return Container.getExits(this) + "\nJealously twists the righteous,\n\n a Shrine of understanding, forget me not,\n\n The creed and bonds of Brotherhood.. broken, an unspeakable misfortune, Killer I have been dubbed, though innocent I am...\n\n who am i?";

            }


            public void PlayerWillSayWord(Notification notification)
            {

                Player player = (Player)notification.Object;
                if (player.currentRoom == Container)
                {
                    string word = (string)notification.userInfo["word"];
                    if (word != null)
                    {
                        if (word.Equals(MagicWord))
                        {
                            player.informationMessage("~ You did it! ~");
                            Container.Delegate = null;
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
        //------------------------------------------------Enemies and Battles--------------------------------
        private Enemies Enemy;
        public void setEnemy(Enemies _enemy)
        {
            this.Enemy = _enemy;
        }
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
        public void AddEnemy(string enemyName, Enemies enemy)//Add a room assignment?
        {
            _enemies[enemyName] = enemy;
        }

        public Enemies GetEnemy(string enemyName)
        {
            if (_Enemydelegate != null)
            {
                return _Enemydelegate.getEnemy(enemyName);
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

        public class Battle : Room, IRoomDelagate
        {

            //Having problems with everything being null
            public Room Container { get; set; }
            public R EnemyContainer { get; set; }
            public Enemies getEnemy(string enemyName)
            {
                return EnemyContainer.GetEnemy(enemyName);
            }

            public Door getExit(string exitName)
            {

                return Container.getExit(exitName, this);
            }



            public void PlayerDidSayWord(Notification notification)
            {
                Player player = (Player)notification.Object;
                if (player.currentRoom == Container)
                {
                }
            }
        }




        public class battleTwo : Room, IRoomDelagate
        {

            public Room Container { get; set; }
            public Door getExit(string exitName)
            {

                return Container.getExit(exitName, this);
            }
            public string getExits()
            {
                Console.WriteLine("\nAs you walk through the dungeon You stumble upon a heap of armor");
                Console.WriteLine("\nIt assembles as it looks in your direction and mystic black mist flows from its helm ....");
                Console.WriteLine("\nIt rusheds toward you filled with the intent to destroy, Now is the time for action, fight young hero!");
                Console.ReadKey();




                return Container.getExits(this) + "\n~ Battle complete. The exits are shown now~\n";

            }
            public void PlayerDidSayWord(Notification notification)
            {
                Player player = (Player)notification.Object;
                if (player.currentRoom == Container)
                {
                }
            }
        }

        public class battleThree : Room, IRoomDelagate
        {


            public Room Container { get; set; }
            public Door getExit(string exitName)
            {

                return Container.getExit(exitName, this);
            }
            public string getExits()
            {
                Console.WriteLine("\nLooks like you've stumbled upon another enemy...");
                Console.WriteLine("\nThis is a brute known only for destruction and violence!");
                Console.WriteLine("\nVictory is slim and death is almost certain as you fight the...");
                Console.ReadKey();




                return Container.getExits(this) + "\n~ Battle complete. The exits are shown now~\n";

            }
            public void PlayerDidSayWord(Notification notification)
            {
                Player player = (Player)notification.Object;
                if (player.currentRoom == Container)
                {
                }
            }
        }

        public class finalBattle : Room, IRoomDelagate
        {


            public Room Container { get; set; }
            public Door getExit(string exitName)
            {

                return Container.getExit(exitName, this);
            }
            public string getExits()
            {
                Console.WriteLine("\nLooks like you've stumbled upon another enemy...");
                Console.WriteLine("\nAkuma has been looking for a worthy challenge.");
                Console.WriteLine("\nDeath is almost certainly assured");
                Console.ReadKey();



                return Container.getExits(this) + "\n~ Battle complete. The exits are shown now~\n";

            }
            public void PlayerDidSayWord(Notification notification)
            {
                Player player = (Player)notification.Object;
                if (player.currentRoom == Container)
                {
                }
            }
        }
        public class EndRoom : IRoomDelagate
        {

            public Room Container { get; set; }

            public Door getExit(string exitName)
            {
                return null;
            }

            public EndRoom()
            {

            }

            public string getExits()
            {
                return Container.getExits(this) + "\n*** You Win! ***" + "\nYou can type 'Quit' to leave.";
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
}
