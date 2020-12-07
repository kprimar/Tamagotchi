using System;


namespace Tamagochi
{
    class Program
    {
        static void Main(string[] args)
        {
            GameMode myGame = new GameMode();
            myGame.Start();

            while (myGame.isGameActive)
            {
                myGame.Update();
            }

        }

    }
}


 
