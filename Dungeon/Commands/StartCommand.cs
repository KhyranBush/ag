using System;

namespace DungeonGame
{
    public class StartCommand : Command
	{
		public StartCommand()
		{
            this.name = "Start";
		}
        override
   public bool execute(Player player, object gameItem)
        {
            if (this.hasSecondWord())
            {
                player.start(this.secondWord);
            }
            else
            {
                player.warningMessage("\nStart What?");
            }
            return false;
        }
    }
}

