using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    public enum AbilityType { None = -1, Electric, Fire, Water, Fairy, Flying, Ground, Grass, Normal};
    public abstract class Game
    {
        public Pokemon SetPokemon()
        {
            Pokemon myPokemon = null;

            //Choose Starter Code
            while (myPokemon == null)
            {
                Console.WriteLine("Choose your starter\n");
                myPokemon = ChooseStarter();
            }

            //Choose Name Code
            while (myPokemon.GetName() == null)
            {
                Console.WriteLine("Congratulations! You chose a " + myPokemon.GetBreed() + "\nWhat would you like to name it?\n");
                string nameInput = Console.ReadLine();
                myPokemon.SetName(nameInput);
            }
            Console.WriteLine(myPokemon.GetName() + " is a great name for a " + myPokemon.GetBreed());
            return myPokemon;
        }   

        public Pokemon ChooseStarter()
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

        public abstract void StartGame();

        public abstract bool Update();


        }


}
