using System;

//The users inventory
namespace DungeonGame
{
    public class InventoryCommand : Command
	{
		public InventoryCommand() : base()
		{
            this.name = "Inventory";
		}
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.warningMessage("\nI cannot inventory " + secondWord);
                
            }
            else
            {
                player.showInventory();
            }
            return false;
        }
    }


}
