using System.Collections;
using System.Collections.Generic;

namespace DungeonGame
{
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            this.name = "Quit";
        }

        override
        public bool execute(Player player)
        {
            bool answer = true;
            if (this.hasSecondWord())
            {
                player.warningMessage("\nI cannot quit " + this.secondWord);
                answer = false;
            }
            return answer;
        }
    }
}
