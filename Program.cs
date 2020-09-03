using System;


namespace Tamagochi
{
    class Program
    {
        static void Main(string[] args)
        {
            GameMode myGame = new GameMode();
            myGame.Run();

            while (myGame.isGameActive)
            {
                myGame.Update();
            }

        }

    }
}


 
