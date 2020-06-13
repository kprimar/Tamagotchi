using System;


namespace Tamagochi
{
    class Program
    {
        public static void Main(string[] args)
        {
            StartGame();
        }

        public static void StartGame()
        {
            Pokemon myPokemon = null;

            while (myPokemon == null)
            {
                Console.WriteLine("Welcome to the Pokemon Center. Choose your starter\n");
                myPokemon = ChooseStarter();
            }

            while (myPokemon.name == null)
            {
                Console.WriteLine("Congratulations! You chose a " + myPokemon.GetType() + "\nWhat would you like to name it?\n");
                myPokemon.name = Console.ReadLine();
            }

            Console.WriteLine(myPokemon.name + " is a great name for a " + myPokemon.GetType());


            for (int currentTurn = 1; currentTurn < 10; currentTurn++)
            {
                PlayerTurn(myPokemon);
                Console.WriteLine(myPokemon.GiveHint());
            }

            Console.WriteLine(myPokemon.name + " the " + myPokemon.GetType() + " had a great day! YOU WIN!");
        }

        private static void GiveHint(Pokemon myPokemon)
        {
            int currentHunger = myPokemon.hunger;
            int currentThirst = myPokemon.thirst;
            int currentHappiness = myPokemon.happiness;

            if (currentHunger < currentThirst && currentHunger < currentHappiness)
            {
                Console.WriteLine(myPokemon.name + " the " + myPokemon.GetType() + " looks hungry.\n");
            }
            else if (currentThirst < currentHunger && currentThirst < currentHappiness)
            {
                Console.WriteLine(myPokemon.name + " the " + myPokemon.GetType() + " looks thirsty.\n");
            }
            else 
            {
                Console.WriteLine(myPokemon.name + " the " + myPokemon.GetType() + " looks bored.\n");
                return;
            }


        }

        public static void ShowDebugsStats(Pokemon myPokemon)
        {
            Console.WriteLine("DEBUG: current hunger is: " + myPokemon.hunger);
            Console.WriteLine("DEBUG: current thirst is: " + myPokemon.thirst);
            Console.WriteLine("DEBUG: current happiness is: " + myPokemon.happiness);
        }

        public static void ChooseAction(Pokemon myPokemon)
        {
            Console.WriteLine("Press [F] - Give Food\nPress [W] - Give Water\nPress [P] - Play\n");
            var actionSelect = Console.ReadLine();

            switch (actionSelect)
            {
                case "F":
                case "f":
                    Console.WriteLine("You fed " + myPokemon.name + " the " + myPokemon.GetType() + ". They look satisfied.\n");
                    myPokemon.hunger += 5;
                    if (myPokemon.hunger > 10)
                    {
                        myPokemon.hunger = 10;
                    }
                    break;
                case "W":
                case "w":
                    myPokemon.thirst += 5;
                    Console.WriteLine(myPokemon.name + " the " + myPokemon.GetType() + " took a drink. They look refreshed.\n");
                    if (myPokemon.thirst > 10)
                    {
                        myPokemon.thirst = 10;
                    }
                    break;
                case "P":
                case "p":
                    myPokemon.happiness += 5;
                    if (myPokemon.happiness > 10)
                    {
                        Console.WriteLine(myPokemon.name + " the " + myPokemon.GetType() + " looks excited!\n");
                        myPokemon.happiness = 10;
                    }
                    break;
                default:
                    Console.WriteLine("That isn't a valid selection\n");
                    ChooseAction(myPokemon);
                    break;
            }


            ShowDebugsStats(myPokemon); //DEBUG LINES
        }


        public static void PlayerTurn(Pokemon myPokemon)
        {

            myPokemon.ReduceStats();
            if (myPokemon.hunger <=0 || myPokemon.thirst <= 0 || myPokemon.happiness <= 0)
            {
                myPokemon.isAlive = false;
                GameOver(myPokemon);
            }
                Console.WriteLine("What do you want to do with " + myPokemon.name + " the " + myPokemon.GetType() + "?\n");
            ChooseAction(myPokemon);
        }

        private static void GameOver(Pokemon myPokemon)
        {
            Console.WriteLine("Oh no! You exhausted " + myPokemon.name + " the " + myPokemon.GetType() + ". GAME OVER");
            Console.WriteLine("Press any key to play again");
            Console.ReadLine();
            StartGame();
        }

        public static void NamePokemon(Pokemon myPokemon)
        {
           string nameInput = Console.ReadLine();
           myPokemon.name = nameInput;
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
    }



}
