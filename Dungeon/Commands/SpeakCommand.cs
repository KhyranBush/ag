using System;

namespace DungeonGame
{
    public class SpeakCommand : Command
	{
		public SpeakCommand() : base()
		{
			this.name = "Speak";
		}

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.say(this.secondWord);
            }
            else
            {
                player.warningMessage("\nSpeak What?");
            }
            return false;
        }
    }
}

