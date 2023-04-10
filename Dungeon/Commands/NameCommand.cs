using System;

//This is just the command for the users name in the starting area.
namespace DungeonGame
{
    public class NameCommand : Command
	{
		public NameCommand()
		{
            this.name = "Name";
		}

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.name(this.secondWord);
            }
            else
            {
                player.warningMessage("\nWhat name: ");
            }
            return false;
        }
    }

}

