using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    class Combat : Game
    {
        Pokemon enemyPokemon; //To Do: Set Pokemon Code

        static int maxAttackOptions = 4;

        public override void StartGame()
        {
            Console.WriteLine("Welcome to the Pokemon Gym!\n");
            Pokemon myPokemon = SetPokemon();
            SpawnEnemy();
            DisplayAttacks(myPokemon);
            DisplayEnemyStats();
            ChooseAttack(myPokemon);
            Console.ReadLine();
        }

        public void ChooseAttack(Pokemon myPokemon)
        {
            Attack attackThisTurn = null;
            while (attackThisTurn == null)
            {
                string playerSelection = Console.ReadLine();
                if (playerSelection == "1")
                {
                    attackThisTurn = myPokemon.attacks[0];
                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName()+"\n");
                }
                if (playerSelection == "2")
                {
                    attackThisTurn = myPokemon.attacks[1];
                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName()+"\n");
                }
                if (playerSelection == "3")
                {
                    attackThisTurn = myPokemon.attacks[2];
                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName()+"\n");
                }
                if (playerSelection == "4")
                {
                    attackThisTurn = myPokemon.attacks[3];
                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName()+"\n");
                }
            }
        }

        public void DisplayEnemyStats()
        {
            Console.WriteLine(enemyPokemon.GetBreed() + " HP: " + enemyPokemon.GetHP());
        }

        private void SpawnEnemy()
        {
            enemyPokemon = new Charmander(); //TO DO: Remove hardcoded enemy. Get random.
            Console.WriteLine("A wild " + enemyPokemon.GetBreed() + " has appeared"); ;
        }

        public void DisplayAttacks(Pokemon myPokemon)
        {
            Console.WriteLine("What do you want to do?\n");
            Console.WriteLine(myPokemon.GetName() + " HP: " + myPokemon.GetHP());
            GetAttacks(myPokemon);
        }

        private static void GetAttacks(Pokemon myPokemon)
        {
            int button = 1;
            List<Attack> myPokemonAttacks = myPokemon.attacks;
            foreach (Attack attack in myPokemonAttacks)
            {
                Console.WriteLine("Press " + button + ": " + attack.GetAttackName());
                button++;
            }
        }

        public override bool Update()
        {
            return false;
        }
    }
}
