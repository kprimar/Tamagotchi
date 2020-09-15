using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    class Tomagochi : Game
    {
        private int lastTurn = 10;
        private int currentTurn = 0;
        Pokemon myPokemon;

        public override void StartGame()
        {
            Console.WriteLine("Welcome to the Pokemon Playground!\n");
            myPokemon = SetPokemon();
            currentTurn = 0;
        }

        public override bool Update()
        {
            currentTurn++;
            if(currentTurn >= lastTurn)
            {
                Console.WriteLine(myPokemon.GetName() + " the " + myPokemon.GetBreed() + " had a great day! YOU WIN! \n");
                return false;
            }
            myPokemon.ReduceStats();
            bool isAlive = myPokemon.CheckHealth();
            if (isAlive)
            {
                Console.WriteLine(myPokemon.GiveHint());
                Console.WriteLine("What do you want to do with " + myPokemon.GetName() + " the " + myPokemon.GetBreed() + "?\n");
                ChooseAction(myPokemon);
                return true;
            }
            else
            {
                GameOver(myPokemon);
                return false;
            }

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
                    Console.WriteLine("You fed " + myPokemon.GetName() + " the " + myPokemon.GetBreed() + ". They look satisfied.");
                    break;
                case "W":
                case "w":
                    if (!myPokemon.GetsThirsty())
                    {
                        Console.WriteLine(myPokemon.GetBreed() + " type Pokemon don't get thirsty...");
                        ChooseAction(myPokemon);
                        return;
                    }
                    myPokemon.ThirstUp();
                    Console.WriteLine(myPokemon.GetName() + " the " + myPokemon.GetBreed() + " took a drink. They look refreshed.");
                    break;
                case "P":
                case "p":
                    myPokemon.HappinessUp();
                    Console.WriteLine(myPokemon.GetName() + " the " + myPokemon.GetBreed() + " looks excited!");
                    break;
                default:
                    Console.WriteLine("That isn't a valid selection");
                    ChooseAction(myPokemon);
                    break;
            }
        }

        private static void GameOver(Pokemon myPokemon)
        {
            Console.WriteLine("Oh no! You exhausted " + myPokemon.GetName() + " the " + myPokemon.GetBreed() + ". GAME OVER");
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
