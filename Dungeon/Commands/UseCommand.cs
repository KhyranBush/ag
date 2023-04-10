using System;

namespace DungeonGame
{
    public class UseCommand : Command
    {
        public UseCommand()
        {
            this.name = "Use";
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
                player.warningMessage("\nUse What?");
            }
            return false;
        }
    }

}

