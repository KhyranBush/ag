using System;

//This was originally going to be used to open doors or chests. However, we discarded the idea
namespace DungeonGame
{
    public class OpenCommand : Command
	{
		public OpenCommand()
		{
			this.name = "Open";
		}
        override
        public bool execute(Player player, object gameItem)
        {
            if (this.hasSecondWord())
            {
                player.open(this.secondWord);
            }
            else
            {
                player.warningMessage("\nOpen What?");
            }
            return false;
        }
    }
}


