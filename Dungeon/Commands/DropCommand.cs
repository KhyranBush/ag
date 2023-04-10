using System;

//This is the drop command, allowing the user to drop their item of choice
//If they did not say what to drop, then it will show them their current inventory
//to remind the player what they can drop.
namespace DungeonGame

{
    public class DropCommand : Command
	{
		public DropCommand()
		{
			this.name = "Drop";
		}
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {

                player.drop(this.secondWord);
            }
            else
            {
                player.showInventory();
                player.warningMessage("\nDrop what?");
            }
            return false;
        }
    }

}
