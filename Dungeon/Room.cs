using System.Collections.Generic;
using static DungeonGame.Interfaces;

namespace DungeonGame
{


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
            return Container.getExits(this) + "\n*** You Win! ***"  + "\nYou can type 'Quit' to leave.";
        }

    }


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
            Hero hero = new Hero();
            Player player = (Player)notification.Object;
            if(player.currentRoom == Container)
            {
                string word = (string)notification.userInfo["word"];
                if(word != null)
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
                        hero.health -= 5;
                        player.warningMessage("\nHealth point" + hero.health);
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
    public class EntranceToFloorTwo: IRoomDelagate
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
            Hero hero = new Hero();
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
                        hero.health -= 5;
                        player.warningMessage("\nHealth point" + hero.health);
                    }
                }
            }
        }

    }


    public class Battle : IRoomDelagate
    {
        public Room Container { get; set; }
        public Door getExit(string exitName)
        {
            
            return Container.getExit(exitName, this);
        }
        public string getExits()
        {
            Encounters.FirstEncounter();

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

    public class battleTwo : IRoomDelagate
    {
        public Room Container { get; set; }
        public Door getExit(string exitName)
        {

            return Container.getExit(exitName, this);
        }
        public string getExits()
        {
            Encounters.SecondEncounter();

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

    public class battleThree : IRoomDelagate
    {
        public Room Container { get; set; }
        public Door getExit(string exitName)
        {

            return Container.getExit(exitName, this);
        }
        public string getExits()
        {
            Encounters.ThirdEncounter();

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

    public class finalBattle : IRoomDelagate
    {
        public Room Container { get; set; }
        public Door getExit(string exitName)
        {

            return Container.getExit(exitName, this);
        }
        public string getExits()
        {
            Encounters.FinalEncounter();

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
        public IRoomDelagate Delegate
        {
            get
            {
                return _delegate;
            }
            set
            {
                _delegate = value;
                if(_delegate != null)
                {
                    _delegate.Container = this;
                }
            }
        }

        private IGameItemsContainer GItemContainer;
       

        public Room() : this("No Tag")
        {

        }
        public Room(string tag)
        {
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
            if(_delegate != null)
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
            if(rDelegate == Delegate)
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
            if(_delegate != null)
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
                foreach(string exitName in keys)
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
            return "Your current location is " + this.tag + "\n" + this.getExits() + "\n" +this.getGItems();
        }
    }

}
