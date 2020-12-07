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

        Dictionary<AbilityType, float> D_electric = new Dictionary<AbilityType, float>();

        public override void StartGame()
        {
            Console.WriteLine("Welcome to the Pokemon Gym!\n");
            myPokemon = SetPokemon();
            SpawnEnemy();
            CreateDictionaries();

        }

        private void CreateDictionaries()
        {
            D_electric.Add(AbilityType.Flying, 2f);
            D_electric.Add(AbilityType.Water, 2f);
            D_electric.Add(AbilityType.Ground, 1f);
            D_electric.Add(AbilityType.Grass, 0.5f);
        }

        public override bool Update()
        {
            DisplayAttacks();
            ChooseAttack();
   
            if(enemyPokemon.GetCurrentHP() <=0)
            {
                Console.WriteLine("The opposing Pokmemon fainted! YOU WIN!");
                return false;
            }

            DisplayStats();
            EnemyAttack();
            DisplayStats();

            if (myPokemon.GetCurrentHP() <= 0)
            {
                Console.WriteLine("Your Pokmemon fainted!");
                return false;
            }

            return true;
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

        public void ChooseAttack()
        {
            Attack attackThisTurn = null;
            while (attackThisTurn == null)
            {
                string playerSelection = Console.ReadLine();
                if (playerSelection == "1")
                {
                    attackThisTurn = myPokemon.attacks[0];
                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName()+"\n");
                    float attackMultiplier = CheckMultiplier(attackThisTurn);
                    DealDamage(attackThisTurn, enemyPokemon, attackMultiplier);
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

        private float CheckMultiplier(Attack attackThisTurn)
        {
            float firstMultiplier = 0;
            float secondMultiplier = 0;
            float overallMultiplier = 0;
            AbilityType attackType = attackThisTurn.GetAttackType();
            if (attackType == AbilityType.Electric)
            {
                foreach (KeyValuePair<AbilityType, float> element in D_electric)
                {
                    if (element.Key == enemyPokemon.PrimaryType)
                    {
                        firstMultiplier = element.Value;
                    }
                    if (element.Key == enemyPokemon.SecondaryType)
                    {
                        secondMultiplier = element.Value;
                    }
                }
            }
            overallMultiplier = firstMultiplier + secondMultiplier;
            Console.WriteLine("Multiplier was " + overallMultiplier);
            return overallMultiplier;

        }

        public void DealDamage(Attack thisAttack, Pokemon enemyPokemon, float multiplier)
        {
            int attackDamage = thisAttack.GetDamage();
            Console.WriteLine("Attack damage without multiplier :" + attackDamage);
            int attackMultiplier = (int)Math.Round(multiplier);
            attackDamage *= attackMultiplier;
            enemyPokemon.ReduceHP(attackDamage);
            Console.WriteLine("Attack damage with multiplier :" + attackDamage);
        }

        public void DisplayStats()
        {
            Console.WriteLine("[" + myPokemon.GetName() + " HP: " + myPokemon.GetCurrentHP() + "/" + myPokemon.GetMaxHP() + "]");
            Console.WriteLine("vs.\n[" + enemyPokemon.GetBreed() + " HP: " + enemyPokemon.GetCurrentHP() + "/" + myPokemon.GetMaxHP() + "]\n");
        }

        private void SpawnEnemy()
        {
            enemyPokemon = new Charmander(); //TO DO: Remove hardcoded enemy. Get random.
            Console.WriteLine("A wild " + enemyPokemon.GetBreed() + " has appeared"); ;
        }

        public void DisplayAttacks()
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



    }
}
