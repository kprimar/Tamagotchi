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
        Dictionary<AbilityType, float> D_fire = new Dictionary<AbilityType, float>();

        public override void StartGame()
        {
            Console.WriteLine("Welcome to the Pokemon Gym!\n");
            myPokemon = SetPokemon();
            SpawnEnemy();
            CreateDictionaries();

        }

        private void CreateDictionaries()
        {
            D_electric.Add(AbilityType.Normal, 1f);
            D_electric.Add(AbilityType.Flying, 2f);
            D_electric.Add(AbilityType.Water, 2f);
            D_electric.Add(AbilityType.Ground, 0f);
            D_electric.Add(AbilityType.Grass, 0.5f);

            D_fire.Add(AbilityType.Normal, 1f);
            D_fire.Add(AbilityType.Fire, 0.5f);
            D_fire.Add(AbilityType.Water, 0.5f);
            D_fire.Add(AbilityType.Grass, 2f);
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
            float enemyAtkWithMultiplier = CalculateMultiplier(enemyAttack, myPokemon);
            DealDamage(myPokemon, enemyAtkWithMultiplier);
            ShowAttackMessage(enemyPokemon, enemyAttack, enemyAtkWithMultiplier);
            Console.WriteLine(enemyPokemon.GetBreed() + " does " + enemyAtkWithMultiplier + " damage!");
        }

        private void ShowAttackMessage(Pokemon attacker, Attack thisAttack, float damageWithMultiplier)
        {
            float startingDamage = thisAttack.GetDamage();
            if (damageWithMultiplier < startingDamage)
            {
                Console.WriteLine(attacker.GetBreed() + " uses " + thisAttack.GetAttackName() + ". It's not very effective.");
            }
            else if (damageWithMultiplier >= (startingDamage * 2))
            {
                Console.WriteLine(attacker.GetBreed() + " uses " + thisAttack.GetAttackName() + ". It's super effective!");
            }
            else
            {
                Console.WriteLine(attacker.GetBreed() + " uses " + thisAttack.GetAttackName());
            }
        }

        private Attack EnemyAttackSelection()
        {
            int randomAttack = random.Next(0, (maxAttackOptions-1));
            List <Attack> enemyAttacks = enemyPokemon.attacks;
            Attack enemyAttack = enemyAttacks[randomAttack];
            return enemyAttack;
        }

        public void ChooseAttack()
        {
            Attack attackThisTurn = null;
            while (attackThisTurn == null)
            {
                string playerSelection = Console.ReadLine();
                bool inputIsNumber = false;
                int input;
                if (int.TryParse(playerSelection, out input))
                {
                    inputIsNumber = true;
                }
                else
                {
                    Console.WriteLine("Please choose a number between 1-4");
                }

                if (inputIsNumber)
                {
                    if (input >= 1 && input <= 4)
                    {
                        input -= 1;
                        attackThisTurn = myPokemon.attacks[input];
                        Console.WriteLine("You choose " + attackThisTurn.GetAttackName() + "\n");
                        float attackWithMultiplier = CalculateMultiplier(attackThisTurn, enemyPokemon);
                        DealDamage(enemyPokemon, attackWithMultiplier);
                        ShowAttackMessage(myPokemon, attackThisTurn, attackWithMultiplier);
                    }
                    else
                    {
                        Console.WriteLine("Please choose a number between 1-4");
                    }
                }
            }
        }
                /*
                                if (playerSelection == "1")
                                {
                                    attackThisTurn = myPokemon.attacks[0];
                                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName()+"\n");
                                    float attackWithMultiplier = CalculateMultiplier(attackThisTurn, enemyPokemon);
                                    DealDamage(enemyPokemon, attackWithMultiplier);
                                    ShowAttackMessage(myPokemon, attackThisTurn, attackWithMultiplier);
                                }
                                if (playerSelection == "2")
                                {
                                    attackThisTurn = myPokemon.attacks[1];
                                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName() + "\n");
                                    float attackWithMultiplier = CalculateMultiplier(attackThisTurn, enemyPokemon);
                                    DealDamage(enemyPokemon, attackWithMultiplier);
                                    ShowAttackMessage(myPokemon, attackThisTurn, attackWithMultiplier);
                                }
                                if (playerSelection == "3")
                                {
                                    attackThisTurn = myPokemon.attacks[2];
                                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName() + "\n");
                                    float attackWithMultiplier = CalculateMultiplier(attackThisTurn, enemyPokemon);
                                    DealDamage(enemyPokemon, attackWithMultiplier);
                                    ShowAttackMessage(myPokemon, attackThisTurn, attackWithMultiplier);
                                }
                                if (playerSelection == "4")
                                {
                                    attackThisTurn = myPokemon.attacks[3];
                                    Console.WriteLine("You choose " + attackThisTurn.GetAttackName() + "\n");
                                    float attackWithMultiplier = CalculateMultiplier(attackThisTurn, enemyPokemon);
                                    DealDamage(enemyPokemon, attackWithMultiplier);
                                    ShowAttackMessage(myPokemon, attackThisTurn, attackWithMultiplier);
                                }
                */


        private float CalculateMultiplier(Attack attackThisTurn, Pokemon targetPokemon)
        {
            float startingDamage = attackThisTurn.GetDamage();
            float damageThisTurn = startingDamage;
            AbilityType attackType = attackThisTurn.GetAttackType();
            if (attackType == AbilityType.Electric)
            {
                foreach (KeyValuePair<AbilityType, float> element in D_electric)
                {
                    if (element.Key == targetPokemon.PrimaryType || element.Key == targetPokemon.SecondaryType)
                    {
                        Console.WriteLine("Multiplier was " + element.Value);
                        damageThisTurn *= element.Value;
                    }
                }
            }

            else if (attackType == AbilityType.Fire)
            {
                foreach (KeyValuePair<AbilityType, float> element in D_fire)
                {
                    if (element.Key == targetPokemon.PrimaryType || element.Key == targetPokemon.SecondaryType)
                    {
                        Console.WriteLine("Multiplier was " + element.Value);
                        damageThisTurn *= element.Value;
                    }
                }
            }

            return damageThisTurn;
        }

        public void DealDamage(Pokemon targetPokemon, float damageThisAttack)
        {           
            targetPokemon.ReduceHP((int)damageThisAttack);
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
