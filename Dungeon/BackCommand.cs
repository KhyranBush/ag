using System;
//This command is used to backtrack through the rooms the user
//has been through
namespace DungeonGame
{
	public class BackCommand : Command
	{

		public BackCommand() : base()
		{
			this.name = "Back";
		}

		override
		public bool execute(Player player)
		{
			if (this.hasSecondWord())
			{
				player.outputMessage("\nI cannot back" + this.secondWord + "");
			}
			else
			{
				player.back();
			}
			return false;
		}
	}
}

