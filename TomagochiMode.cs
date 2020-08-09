using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    class Tomagochi : Game
    {
        public override void StartGame()
        {
            Console.WriteLine("Welcome to the Pokemon Playground!\n");
            Pokemon myPokemon = SetPokemon();
            myPokemon.ReduceStats();
            for (int currentTurn = 1; currentTurn < 10; currentTurn++)
            {
                bool isAlive = myPokemon.CheckHealth();
                if (isAlive)
                {
                    Console.WriteLine(myPokemon.GiveHint());
                    Console.WriteLine("What do you want to do with " + myPokemon.GetName() + " the " + myPokemon.GetType() + "?\n");
                    ChooseAction(myPokemon);
                    myPokemon.ReduceStats();
                }
                else
                {
                    GameOver(myPokemon);
                    return;
                }

            }

            Console.WriteLine(myPokemon.GetName() + " the " + myPokemon.GetType() + " had a great day! YOU WIN! \n");

        }

        public static void ChooseAction(Pokemon myPokemon)
        {
            Console.WriteLine("Press [F] - Give Food\nPress [W] - Give Water\nPress [P] - Play\n");
            var actionSelect = Console.ReadLine();

            switch (actionSelect)
            {
                case "F":
                case "f":
                    myPokemon.HungerUp();
                    Console.WriteLine("You fed " + myPokemon.GetName() + " the " + myPokemon.GetType() + ". They look satisfied.");
                    break;
                case "W":
                case "w":
                    if (!myPokemon.GetsThirsty())
                    {
                        Console.WriteLine(myPokemon.GetType() + " type Pokemon don't get thirsty...");
                        ChooseAction(myPokemon);
                        return;
                    }
                    myPokemon.ThirstUp();
                    Console.WriteLine(myPokemon.GetName() + " the " + myPokemon.GetType() + " took a drink. They look refreshed.");
                    break;
                case "P":
                case "p":
                    myPokemon.HappinessUp();
                    Console.WriteLine(myPokemon.GetName() + " the " + myPokemon.GetType() + " looks excited!");
                    break;
                default:
                    Console.WriteLine("That isn't a valid selection");
                    ChooseAction(myPokemon);
                    break;
            }
        }

        private static void GameOver(Pokemon myPokemon)
        {
            Console.WriteLine("Oh no! You exhausted " + myPokemon.GetName() + " the " + myPokemon.GetType() + ". GAME OVER");
            Console.WriteLine("Press any key to play again");
            Console.ReadLine();
        }



        //FOR DEBUGGING
        public static void ShowDebugsStats(Pokemon myPokemon)
        {
            Console.WriteLine("DEBUG: current hunger is: " + myPokemon.GetHunger());
            Console.WriteLine("DEBUG: current thirst is: " + myPokemon.GetThirst());
            Console.WriteLine("DEBUG: current happiness is: " + myPokemon.GetHappiness());
        }

    }

}
