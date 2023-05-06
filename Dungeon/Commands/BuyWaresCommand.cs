using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public class BuyWaresCommand : Command
    {

        public BuyWaresCommand()
        {
            this.name = "BuyWares";
        }
        override
   public bool execute(Player player)
        {
            if (this.hasSecondWord() != false && this.hasThirdWord()!= false)
            {
                player.BuyWares(this.secondWord,this.thirdWord);
            }
            else
            {
                player.BuyWares(this.secondWord, this.thirdWord);
            }
            return false;
        }
    }
        
}
