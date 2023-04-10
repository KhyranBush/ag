using System.Collections;
using System.Collections.Generic;

//A command that allows the user to go to different room
namespace DungeonGame
{
    public class GoCommand : Command
    {

        public GoCommand() : base()
        {
            this.name = "Go";
        }

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.waltTo(this.secondWord);
            }
            else
            {
                player.warningMessage("\nGo Where?");
            }
            return false;
        }
    }
}
