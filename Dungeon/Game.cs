using System.Collections;
using System.Collections.Generic;
using System;
using System.Data;
using System.Net.Http.Headers;

namespace DungeonGame
{
	public class Game
	{
		Player player;
		Parser parser;
		public bool playing;
		public bool finished;

		//This is command design
		Queue<Command> commandQueue;


		public Game()
		{
          
            playing = false;
			parser = new Parser(new CommandWords());
			player = new Player(GameWorld.Instance.Lobby);
			Dictionary<string, object> userInfo = new Dictionary<string, object>();
			userInfo["state"] = new ParserCharacterState();
			NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", player, userInfo));
			commandQueue = new Queue<Command>();
		}


		//This is the main loop that keeps going until the win condition is met whichh is when the player enters the final room  
		public void play()
		{

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.

			bool finished = false;
			while (!finished && playing)
			{
				Console.Write("\n" + player.Name + ">");
				Command command = parser.parseCommand(Console.ReadLine());
				if (command == null)
				{
					player.warningMessage("Command could not be understood.");
				}
				else
				{
					finished = command.execute(player);
				}
			}
			while(finished && !playing)
			{
				end();
			
			}
		}

		public void start()
		{
			playing = true;
			player.currentRoom = GameWorld.Instance.Lobby;	
			player.informationMessage(welcome());
			processCommandQueue();
		}

		public void end()
		{
			playing = false;
			

                player.informationMessage(goodbye());
            
			
		}

		public string welcome()
		{
			return "Welcome to the Tree Of Life: The Age of Ascension\n\n\nAscend to glory young hero!\n\n" + "You are currently in mental stasis, a transition between worlds, a space both in and within- exsistent yet none existent in time\n\nI Ask you only one simple question dear player..\n\nWhat is your name?" + "\n\nYou may. type 'Help' if you that is truly what you desire..\n\n";
			
		}

		public string goodbye()
		{
			return "\nThanks for playing!\n";
		}



		public void processCommandQueue()
		{
			while(commandQueue.Count > 0)
			{
				Command command = commandQueue.Dequeue();
				player.outputMessage(">" + command);
				command.execute(player);
				

            }
		}

		public  void FirstEncounter()
		{
			player.informationMessage("A terrible foe appears! ");
		}
		
		

	}
}
 