using System;

namespace DungeonGame
{
    class Program
    {
        public static Hero currentPlayer = new Hero();
        
        static void Main(string[] args)
        {
            Game game = new Game();
            game.start();
            game.play();
            game.end();
        }
    }
}
