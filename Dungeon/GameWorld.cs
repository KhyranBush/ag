using System.Dynamic;
using System.Numerics;
using System.Runtime.CompilerServices;
using static DungeonGame.Interfaces;



namespace DungeonGame
{
	public class GameWorld
	{
		static private GameWorld _instance = null;
		private Player Player;
		private Enemies Enemies;
		static public GameWorld Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameWorld();
				}
				return _instance;
			}
		}
	
		private Room _entrance;
		private Room _lobby;
		private Room _victorytrigger;

		public Room Entrance
		{
			get
			{
				return _entrance;
			}
		}
		public Room Lobby
		{
			get
			{
				return _lobby;
			}
		}
		private GameWorld()
		{
				
			createWorld();
			NotificationCenter.Instance.addObserver("EndGame", EndGame);
		}
		public void SetPlayer(Player player)
		{
			this.Player = player;
		}
		
		
		public void EndGame(Notification notification)
		{
			Player player = (Player)notification.Object;
			Room room = player.currentRoom;
			if (room == _victorytrigger)
			{
				player.informationMessage("~ You Won! ~");
			}

		}
		

		private void createWorld()
		{
			Room room1 = new Room("A bright room with strange inscriptions depicting gruesome monsters slaughtering seemingly innocent people...\n\n The air feels uncomfortably heavy");                               //Room 1
			Room room2 = new Room("\nA corridor with a heavy stench of metal and blood as many weapons lay unclaimed and rusted on the blood stained floor\n Something is off about this place..."); //Room 2
			Room room3 = new Room("\nA wide open area with an eye catching luminescent glow irradiating the walls\n You eyes immediately are drawn to the light source in the distance..." + "\n Hark thy Adventurer, tis I Rohin, Here to stop your foolish ascension!");		 //Room 3
			Room room4 = new Room("\n A room with Armor strewn around the floor and depictions of magical combat on the walls.\n Maybe you should look at them closer\n standing as still as night is ama man\n He is Short, has broad shoulders and decently muscular, You suspect Him to be a Dwarf!\n Hi my Name is Bazaar, How may I be of service?!");						                                         //Room 4
			Room room5 = new Room("\nHellfire and brimstone, a match made in heaven wouldnt you agree?\n Fight for your life, as you lose your mortal seed\n the wind blows and the horn calls but none shall hear\nAnguish and cry out loud as you're encumbered by fear\n for destiny strikes as King Solomon Appears! ");                                                     //Room 5
			Room room51 = new Room("Consider this a break young traveler take the time to think about your path...");
            //----------------------------------Floor 2 ----------------------
            Room room7 = new Room("\nFirst room on the second floor of this place\n\n You notice the change in scenery as the walls are covered in vines\n\nThe luscious greenery gives a very leafy aroma that is harsh on the nose\n\n Maybe you should explore?\n");                                                       //Room 6
			Room room6 = new Room("\"For passage into the second floor, Riddle me this,\n I see all for I am all knowing\n I fly High but shudder at the thought of the ground\n I can be everywhere yet nowhere at the same time\n I am contracted by the one above all, who am i?\n\n This Room Contains A creature encased in stone,\nHellach the nameplate says,\n maybe you should attack it?");						                 //Room 7
			Room room8 = new Room("\nstaircase Area.\n A long staircase is in front of you which seems to lead to a higher level");//Room 8
			Room room9 = new Room("\nfirst room of the second level");											     //Room 9
			Room room10 = new Room("\nmain room of the second level");											     //Room 10
			Room room11 = new Room("\ndining hall of the death dungeon");											 //Room 11
			Room room12 = new Room("\nkitchen of the death dungeon. Maybe snacks are here.");                     //Room 12
			Room room13 = new Room("\na strange desert?");
			Room room14 = new Room("\nthe iron blood tournament");
			Room room15 = new Room("\nlaundary room.");
			Room room16 = new Room("\nbalchony of the death dungeon.\nMaybe you could get away by jumping over it.");
			
			Room room0 = new Room("You are currently in the realm of discord, you both exist and do not exist until you say your name\n\nEnter 'Name(your name)' and when you are done enter 'Done'\n"); //Room 13

			
			
		

            //Room routes from Room 1
            Door door = Door.MakeDoor(room1, room2, "north", "east");
            door.open();
            door = Door.MakeDoor(room2, room3, "east", "north");
            door = Door.MakeDoor(room3, room4, "north", "north");
            door = Door.MakeDoor(room3, room5, "west", "west");
            door = Door.MakeDoor(room4, room6, "east", "north");
            door = Door.MakeDoor(room6, room7, "north", "north"); //Room7 is Floor 2 Rooom6,  is the final Boss Room
			door=Door.MakeDoor(room5, room51, "west", "west");

           // door = Door.CreateLockedDoor(room6, room7, "east", "north", "BloodKey");

			//Room routes from Room 2

			room4.AddMerchant("Bazaar");
			
		
			//room4.MerchantDelagate.MerchantContainer.AddItems("Obadachi", 1, 15, 10, 10, "Legend has it the ancient Ascenders used this to defeat unbreakable foes", true);
			

		


			

			_entrance = room1;
			_lobby = room0;
			//---------------------floor doors and their prerequisites-----------



			//Trap/Puzzle rooms
			

			
			Room.TrapRoomTwo tRoomTwo = new Room.TrapRoomTwo();

			room5.Delegate = tRoomTwo;

			//This is the room you need to go to end the game and win
			Room.EndRoom eRoom = new Room.EndRoom();
			room16.Delegate = eRoom;

            //These are rooms where battle encounters would start.
            //Not all monsters are created
			//First battle is in room 3
            //Room battleRoom = new Room.Battle();
         
           // room3.Delegate = (IRoomDelagate)battleRoom;
			
			
			room3.AddEnemy("Rohin",10,4,100, 60, false);
			room6.AddEnemy("Hellach",20, 10, 500, 100, false);
			room5.AddEnemy("Solomon", 15,11,200,110,false);
			room6.Delegate = tRoomTwo;
            
            //Room 4 Trap Room
            Room.TrapRoom tr1 = new Room.TrapRoom();


            IGameItems gSwordmod = new IGameItem("Ultimate", 0, 10, 2, 3, "", true);
            IGameItems gSword = new IGameItem("GreatSword", 10f,10,2,3,"A massive blade with rust laden on its shaft, shows sign of frequent use", true);
			gSword.AddDecorator(gSwordmod);
			room3.drop(gSword);
            IGameItems gBow = new IGameItem("HuntingBow", 3f, 8, 100, 1, "A basic hunting bow, with a green luminescence irradiating from its string", true);
            room2.drop(gBow);
            IGameItems gStaff = new IGameItem("ApprenticeStaff", 1f, 15, 1, 0, "A Staff that irradiates an eerie Gold luminescence, mana splurges from its mysically cracked orb", true);
            room4.drop(gSword);
			room5.drop(gStaff);



            IGameItems Gold = new IGameItem("GoldCoin", 0.0f,0,0,0,"A unique instance of a coin with a woman on the front who has a tail???!",false);
			room1.drop(Gold);
			IGameItems Floor2Key = new IGameItem("BloodKey", 0.0f, 0, 0, 0,"A key stained with blood that is ever flowing", false);
			room5.drop(Floor2Key);

    }


	}
}
