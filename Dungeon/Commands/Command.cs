using System.Collections;
using System.Collections.Generic;
using System;

namespace DungeonGame
{
    //Software design pattern: Commands
    public abstract class Command
    {
        private string _name;
        public string name { get { return _name; } set { _name = value; } }
        private string _secondWord;
        private string _thirdWord;
        public string thirdWord { get { return _thirdWord; } set { _thirdWord = value; } }      
        public string secondWord { get { return _secondWord; } set { _secondWord = value; } }

        public Command()
        {
            this.name = "";
            this.secondWord = null;
            this.thirdWord = null;
        }


        public bool hasSecondWord()
        {
            return this.secondWord != null;
        }
        public bool hasThirdWord()
        {
            return this.thirdWord != null;
        }

        override
        public string ToString()
        {
            return name + (hasSecondWord() ? " " + secondWord : hasThirdWord() ? " " + thirdWord: " ");
        }

        public abstract bool execute(Player player);
    }
}
