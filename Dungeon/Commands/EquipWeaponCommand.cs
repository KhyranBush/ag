using System;
using System.Collections.Generic;
using System.Text;
using static DungeonGame.Interfaces;

namespace DungeonGame
{
    public class EquipWeaponCommand : Command
    {
        public EquipWeaponCommand() : base()
        {
            this.name = "Equip";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.EquipWeapon(this.secondWord);
            }
            else
            {
                player.EquipWeapon(this.secondWord);
            }
            return false;
        }
     
    }
}

