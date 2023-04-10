using System;
//This command is used to backtrack through the rooms the user
//has been through
namespace DungeonGame
{
    public class BackCommand : Command
    {

        public BackCommand() : base()
        {
            name = "Back";
        }

        override
        public bool execute(Player player)
        {
            if (hasSecondWord())
            {
                player.outputMessage("\nI cannot back" + secondWord + "");
            }
            else
            {
                player.back();
            }
            return false;
        }
    }
}

