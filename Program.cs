using System;


namespace Tamagochi
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GameMode myGame = new GameMode();
                myGame.Run();
            }

        }
    }
}

 
