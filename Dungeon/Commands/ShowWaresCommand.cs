using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public class ShowWaresCommand:Command
    {

            public ShowWaresCommand() : base()
            {
                this.name = "ShowWares";
            }
            override
            public bool execute(Player player)
            {
                if (this.hasSecondWord())
                {
                    player.ShowWares(this.secondWord);
                }
                else
                {
                    player.ShowWares(this.secondWord);
                }
                return false;
            }

        
    }
}
