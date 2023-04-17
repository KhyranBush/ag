using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public class AttackCommand : Command
    {
        public AttackCommand()
        {
            this.name = "Attack";
        }
        override
   public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.start(this.secondWord);
            }
            else
            {
                player.warningMessage("\nAttack Who?");
            }
            return false;
        }
    }
}
