using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    public class GameMode
    {
        public bool gameActive = false;
        public void Run()
        {
            Game gameMode = SelectGameMode();
            gameMode.StartGame();
        }

        public Game SelectGameMode()
        {
            Console.WriteLine("Welcome to the Pokemon Center. What would you like to do?\n");
            Game currentMode = null;
            int modeSelected = GameMode.ProcessPlayerSelection();
            if (modeSelected == 1)
            {
                currentMode = new Tomagochi();
                return currentMode;
            }
            if (modeSelected == 2)
            {
                currentMode = new Combat();
                return currentMode;
            }
            else
            {
                return currentMode;
            }

        }

        public static int ProcessPlayerSelection()
        {
            Console.WriteLine("Please select an activity.\nP = Pokemon Playground \nG = Pokemon Gym");
            string playerInput = Console.ReadLine();
            int modeSelected = 0;
            if (playerInput == "P" || playerInput == "p")
            {
                modeSelected = 1;
            }
            else if (playerInput == "G" || playerInput == "g")
            {
                modeSelected = 2;
            }
            else
            {
                Console.WriteLine("That isn't a valid selection\n");
                ProcessPlayerSelection();
            }
            return modeSelected;
        }
    }
}
