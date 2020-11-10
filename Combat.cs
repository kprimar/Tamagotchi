using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    class Combat : Game
    {
        Pokemon enemyPokemon; //To Do: Set Pokemon Code
        Pokemon myPokemon;
        Random random = new Random();

        static int maxAttackOptions = 4;

        public override void StartGame()
        {
            Console.WriteLine("Welcome to the Pokemon Gym!\n");
            myPokemon = SetPokemon();
            SpawnEnemy();
            Update(myPokemon);
        }

        private void Update(Pokemon myPokemon)
        {
            while (myPokemon.GetCurrentHP() > 0)
            {
                DisplayAttacks(myPokemon);
                ChooseAttack(myPokemon);
                DisplayStats(myPokemon);
                EnemyAttack();
                DisplayStats(myPokemon);
            }
        }

        private void EnemyAttack()
        {
            Attack enemyAttack = EnemyAttackSelection();
            int attackDamage = enemyAttack.GetDamage();
            myPokemon.ReduceHP(attackDamage);
            Console.WriteLine(enemyPokemon.GetBreed() + " does " + attackDamage + " damage!");
        }

        private Attack EnemyAttackSelection()
        {
            int randomAttack = random.Next(0, (maxAttackOptions-1));
            List <Attack> enemyAttacks = enemyPokemon.attacks;
            Attack enemyAttack = enemyAttacks[randomAttack];
            Console.WriteLine(enemyPokemon.GetBreed() + " uses " + enemyAttack.GetAttackName());
            return enemyAttack;

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
                    DealDamage(attackThisTurn, enemyPokemon);
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

        public void DealDamage(Attack thisAttack, Pokemon enemyPokemon)
        {
            int attackDamage = thisAttack.GetDamage();
            enemyPokemon.ReduceHP(attackDamage);
        }

        public void DisplayStats(Pokemon myPokemon)
        {
            Console.WriteLine("[" + myPokemon.GetName() + " HP: " + myPokemon.GetCurrentHP() + "/" + myPokemon.GetMaxHP() + "]");
            Console.WriteLine("vs.\n[" + enemyPokemon.GetBreed() + " HP: " + enemyPokemon.GetCurrentHP() + "/" + myPokemon.GetMaxHP() + "]\n");
        }

        private void SpawnEnemy()
        {
            enemyPokemon = new Charmander(); //TO DO: Remove hardcoded enemy. Get random.
            Console.WriteLine("A wild " + enemyPokemon.GetBreed() + " has appeared"); ;
        }

        public void DisplayAttacks(Pokemon myPokemon)
        {
            Console.WriteLine("What do you want to do?\n");
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
