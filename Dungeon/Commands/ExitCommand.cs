using System;

//This allows the user to exit the start up screen
namespace DungeonGame
{
    public class ExitCommand : Command
	{
		public ExitCommand()
		{
			name = "Done";
		}
        override
        public bool execute(Player player)  
        {
            if (!this.hasSecondWord())
            {
                player.exit();
            }
            else
            {
                player.warningMessage("\nExit?" +secondWord + "???");
            }
            return false;
        }
    }
}

