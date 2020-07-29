using System;


namespace Tamagochi
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameActive = false; 
            while (!gameActive)
            {
                GameMode myGame = new GameMode();
                myGame.Run();
                gameActive = false;
            }

        }
    }
}

 
