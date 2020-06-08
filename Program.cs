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

            Console.WriteLine("Congratulations! You chose a " + myPokemon.breed! + "\nWhat would you like to name it?");
            NamePokemon(myPokemon);
            Console.WriteLine(myPokemon.name + " is a great name for a " + myPokemon.breed);

            int currentTurn = 0;
            StartNextTurn(currentTurn, myPokemon);
            Console.WriteLine("What do you want to do with your pet?");
            ChooseAction();
            //Update stats
            //Start next turn

            Console.WriteLine("DEBUG: current hunger is: " + myPokemon.hunger);
            Console.WriteLine("DEBUG: current thirst is: " + myPokemon.thirst);
            Console.WriteLine("DEBUG: current happiness is: " + myPokemon.happiness);

            Console.ReadLine();
        }

        public static void ChooseAction()
        {
            Console.WriteLine("Press [F] - Give Food\nPress [W] - Give Water\nPress [P] - Play");
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

        public static void NamePokemon(Pokemon myPokemon)
        {
           string nameInput = Console.ReadLine();
           myPokemon.name = nameInput;
        }

        public static void StartNextTurn(int currentTurn, Pokemon myPokemon)
        {
            currentTurn++;
            myPokemon.ReduceStats();
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
