using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGame
{
    public class UnequipWeaponCommand : Command
    {
        public UnequipWeaponCommand() : base()
        {
            this.name = "Unequip";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.UnequipWeapon(this.secondWord);
            }
            else
            {
                player.UnequipWeapon(this.secondWord);
            }
            return false;
        }

    }
}


