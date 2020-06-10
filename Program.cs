using System;
using System.Security.Cryptography.X509Certificates;

namespace Tamagochi
{
    class Program
    {
        public static void Main(string[] args)
        {
            Pokemon myPokemon = new Pokemon();
            Console.WriteLine("Welcome to the Pokemon Center. Choose your starter\n");
            ChooseStarter(myPokemon);

            Console.WriteLine("Congratulations! You chose a " + myPokemon.breed! + "\nWhat would you like to name it?\n");
            NamePokemon(myPokemon);
            Console.WriteLine(myPokemon.name + " is a great name for a " + myPokemon.breed);

            int currentTurn = 0;
            while(currentTurn < 10)
            {
                StartNextTurn(currentTurn, myPokemon);
            }
            Console.WriteLine(myPokemon.name + " the " + myPokemon.breed + " had a great day! YOU WIN!");




        }


        private static void EndCurrentTurn(Pokemon myPokemon)
        {
            int currentHunger = myPokemon.hunger;
            int currentThirst = myPokemon.thirst;
            int currentHappiness = myPokemon.happiness;

            if (currentHunger < currentThirst && currentHunger < currentHappiness)
            {
                Console.WriteLine(myPokemon.name + " the " + myPokemon.breed + " looks hungry.\n");
            }
            else if (currentThirst < currentHunger && currentThirst < currentHappiness)
            {
                Console.WriteLine(myPokemon.name + " the " + myPokemon.breed + " looks thirsty.\n");
            }
            else 
            {
                Console.WriteLine(myPokemon.name + " the " + myPokemon.breed + " looks bored.\n");
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
                    Console.WriteLine("You fed " + myPokemon.name + " the " + myPokemon.breed + ". They look satisfied.\n");
                    myPokemon.hunger += 5;
                    if (myPokemon.hunger > 10)
                    {
                        myPokemon.hunger = 10;
                    }
                    break;
                case "W":
                case "w":
                    myPokemon.thirst += 5;
                    Console.WriteLine(myPokemon.name + " the " + myPokemon.breed + " took a drink. They look refreshed.\n");
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
                        Console.WriteLine(myPokemon.name + " the " + myPokemon.breed + " looks excited!\n");
                        myPokemon.happiness = 10;
                    }
                    break;
                default:
                    Console.WriteLine("That isn't a valid selection\n");
                    ChooseAction(myPokemon);
                    break;
            }

            EndCurrentTurn(myPokemon);
            ShowDebugsStats(myPokemon); //DEBUG LINES
        }


        public static void StartNextTurn(int currentTurn, Pokemon myPokemon)
        {
            currentTurn++;
            Console.WriteLine(currentTurn);
            myPokemon.ReduceStats();
            if (myPokemon.hunger <=0 || myPokemon.thirst <= 0 || myPokemon.happiness <= 0)
            {
                myPokemon.isAlive = false;
                GameOver(myPokemon);
            }
                Console.WriteLine("What do you want to do with " + myPokemon.name + " the " + myPokemon.breed + "?\n");
            ChooseAction(myPokemon);
        }

        private static void GameOver(Pokemon myPokemon)
        {
            Console.WriteLine("Oh no! You exhausted " + myPokemon.name + " the " + myPokemon.breed + ". GAME OVER");
        }

        public static void NamePokemon(Pokemon myPokemon)
        {
           string nameInput = Console.ReadLine();
           myPokemon.name = nameInput;
        }

        public static void ChooseStarter(Pokemon myPokemon)
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
                    myPokemon.breed = "Pikachu";
                    break;
                case "C":
                case "c":
                    myPokemon.breed = "Charmander";
                    break;
                case "S":
                case "s":
                    myPokemon.breed = "Squirtle";
                    break;
                default:
                    Console.WriteLine("That isn't a valid answer");
                    ChooseStarter(myPokemon);
                    break;
            }
        }
    }


    public class Pokemon
    {
        public string name;
        public string breed;
        public bool isAlive = true;

        public int hunger = 10;
        public int thirst = 10;
        public int happiness = 10;

        public int StatDecreaseThisTurn()
        {
            Random random = new Random();
            return random.Next(0, 5);
        }

        public void ReduceStats()
        {
            int hungerChange = StatDecreaseThisTurn();
            hunger -= hungerChange;

            int thirstChange = StatDecreaseThisTurn();
            thirst -= thirstChange;
            
            int happinessChange = StatDecreaseThisTurn();
            happiness -= happinessChange;

        }

        public void HealthUp()
        {
            hunger += 5;
        }
        public void ThirstUp()
        {
            thirst += 5;
        }
        public void HappinessUp()
        {
            happiness += 5;
        }

    }

    public class Pikachu : Pokemon
    {

    }
    public class Charmander : Pokemon
    {

    }

    public class Squirtle : Pokemon
    {

    }
}
