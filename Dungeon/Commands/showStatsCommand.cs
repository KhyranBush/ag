using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public class showStatsCommand : Command
    {
        public showStatsCommand() : base()
        {
            this.name = "showStats";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.warningMessage("\nI cannot Display " + secondWord);

            }
            else
            {
                player.showStats();
            }
            return false;
        }
    }
}
