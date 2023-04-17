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
        private IRoomDelagate _delegate;
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
                    _Enemydelegate.EnemyContainer = this;
                }
            }
        }
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
        //End of Delegate section
       

        private IGameItemsContainer GItemContainer;


       public Room() : this("No Tag")
       {

       }
        
        public Room(string tag = "No Tag" ) 
        {
            enemies = new Dictionary<string, Enemies>();
            exits = new Dictionary<string, Door>();
            this.tag = tag;
            _delegate = null;
            GItemContainer = new BackPack();
            
        }

        

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
            if (rDelegate == EnemyDelegate)
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
        private Dictionary<string, Enemies> enemies;
       
        public void AddEnemy(string enemyName, Enemies enemy)
        {
            enemies[enemyName] = enemy;
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
                enemies.TryGetValue(enemyName, out enemy);
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
                return getEnemies(null,null);
            }                                        
        }
        public string getEnemies(IEnemyDelagate eDelegate,string currentRoom)
        {
            string enemyNames = " ";
           
            if (eDelegate == EnemyDelegate)
            {

                if (enemies.ContainsKey(currentRoom))
                {
                    Enemies enemiesInRoom = enemies[currentRoom];
                    Dictionary<string, Enemies>.KeyCollection keys = enemiesInRoom.Keys;
                    foreach (string enemyName in enemies.Keys)
                    {
                        enemyNames += " " + enemyName;
                    }
                    return enemyNames;
                }
                else
                {
                    return "No enemies in this room.";
                }
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
       


        public class Battle :Room, IRoomDelagate
        {
            

            public Room Container { get; set; }
            public Enemies EnemyContainer { get; set; }
            public Door getExit(string exitName)
            {

                return Container.getExit(exitName, this);
            }
            public string getExits()
            {
                Console.WriteLine("\nIn far corner of the room the light effigies collide forming a spirit irradiating the same luminescent glow present on the walls");
                Console.WriteLine("\nAs it animates, it notices your prescence and transforms in color from a blueish hue to a deep burning crimson");
                Console.WriteLine("\nThe spirit speaks with its metallic and disturbing voice that slowly drones in and out\n Your death marks the end of another foolish ascension\n" +
                                    "prepare to meet the Loa once more impudent Ascender!");
                Console.WriteLine("\nThis is your first trial young hero, Destroy or be Destroyed.. KIll or be Killed, If you wish to proceed, attack the enemy!");
                
                Enemies enemy1 = new Enemies(Container, "Ethereal Spirit Soldier: Rohin the first Ascender", 200, 5, 10, 4, 10, false);

                enemies.Add("Rohin", enemy1);


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
                Enemies enemy2 = new Enemies(Container, "Skeleton man 2.0", 7, 10, 11, 100, 10, false);
                enemies.Add("Skeleton", enemy2);



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

        public class battleThree :Room,  IRoomDelagate
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
                Enemies enemy3 = new Enemies(Container,"Rancor", 10, 10, 10, 200, 10, false);
                enemies.Add("Rancor", enemy3);
                
           

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

        public class finalBattle :Room, IRoomDelagate
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

                Enemies enemy4 = new Enemies(Container, "Akuma, The Raging Demon!", 20, 30, 20, 200, 20, false);
                enemies.Add("Akuma", enemy4);

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
        
    }
    

    
    
}
