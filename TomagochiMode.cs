﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    class Tomagochi : GameMode
    {
        public static void StartGame()
        {
            Pokemon myPokemon = null;

            //Choose Starter Code
            while (myPokemon == null)
            {
                Console.WriteLine("Welcome to the Pokemon Center. Choose your starter\n");
                myPokemon = ChooseStarter();
            }

            //Choose Name Code
            while (myPokemon.GetName() == null)
            {
                Console.WriteLine("Congratulations! You chose a " + myPokemon.GetType() + "\nWhat would you like to name it?\n");
                string nameInput = Console.ReadLine();
                myPokemon.SetName(nameInput);
            }
            Console.WriteLine(myPokemon.GetName() + " is a great name for a " + myPokemon.GetType());



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

            Console.WriteLine(myPokemon.GetName() + " the " + myPokemon.GetType() + " had a great day! YOU WIN! \nWould you like to switch game modes?");

        }

        public static Pokemon ChooseStarter()
        {
            Console.WriteLine("Press 'P' for Pikachu\nPress 'C' for Charmander\nPress 'S' for Squirtle\n");
            var starterInput = Console.ReadLine();

            while (string.IsNullOrEmpty(starterInput))
            {
                Console.WriteLine("I know it's hard but you have to choose one!");
                starterInput = Console.ReadLine();
            }

            switch (starterInput)
            {
                case "P":
                case "p":
                    return new Pikachu();
                case "C":
                case "c":
                    return new Charmander();
                case "S":
                case "s":
                    return new Squirtle();

                default:
                    Console.WriteLine("That isn't a valid answer");
                    return null;
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
            StartGame();
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