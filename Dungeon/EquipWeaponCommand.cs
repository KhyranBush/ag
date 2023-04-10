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
                player.EquipWeapon(string gameItem);

            }
            else
            {
              
                player.EquipWeapon(string gameItem);
            }
            return false;
        }

       
    }

}
