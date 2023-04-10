using System;

namespace DungeonGame
{
    public class QuestCommand : Command
	{
		public QuestCommand() 
		{
			this.name = "Quest";
		}
        override
       public bool execute(Player player, object gameItem)
        {
            if (this.hasSecondWord())
            {
                player.warningMessage(this.secondWord + " isn't your quest");
            }
            else
            {
                player.quest();
            }
            return false;
        }
    }

}
