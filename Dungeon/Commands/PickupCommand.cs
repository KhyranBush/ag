using System;

namespace DungeonGame
{
    public class PickupCommand : Command
    {
        public PickupCommand() : base()
        {
            string name = "Pickup";
            
            this.name = name.ToLower();
        }
        override
        public bool execute(Player player, object gameItem)
        {
            if (this.hasSecondWord())
            {
                player.pickup(this.secondWord);
               
            }
            else
            {
                player.warningMessage("\nPick up what?");
            }
            return false;
        }
       
    }
}

