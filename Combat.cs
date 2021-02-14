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

        Dictionary<AbilityType, Dictionary<AbilityType, float>> AttackToDictionary = new Dictionary<AbilityType, Dictionary<AbilityType, float>>();
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

        //MAKE THESE VALUES VARIABLES

            AttackToDictionary.Add(AbilityType.Electric, D_electric);
            AttackToDictionary.Add(AbilityType.Fire, D_fire);

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


        //SHARED FUNCTIONS
        private void ShowAttackMessage(Pokemon attacker, Attack thisAttack, float damageWithMultiplier)
        {
            int dmgWithMultiplierInt = (int)damageWithMultiplier;

            if (dmgWithMultiplierInt >= thisAttack.GetMaxDmg())
            {
                Console.WriteLine(attacker.GetBreed() + " uses " + thisAttack.GetAttackName() + ". It's super effective!");
            }
            else if (dmgWithMultiplierInt < thisAttack.GetMinDmg())
            {
                Console.WriteLine(attacker.GetBreed() + " uses " + thisAttack.GetAttackName() + ". It's not very effective.");
            }
            else
            {
                Console.WriteLine(attacker.GetBreed() + " uses " + thisAttack.GetAttackName());
            }
        }

        private float CalculateMultiplier(Attack attackThisTurn, Pokemon targetPokemon)
        {
            float startingDamage = attackThisTurn.GetDamage();
            float damageThisTurn = startingDamage;
            AbilityType attackType = attackThisTurn.GetAttackType();
            Dictionary<AbilityType, float> attackDictionary;

            Console.WriteLine(startingDamage);
            foreach (KeyValuePair<AbilityType, Dictionary<AbilityType, float>> dictionary in AttackToDictionary)
            {
                if (attackType == dictionary.Key)
                {
                    attackDictionary = dictionary.Value;
                    foreach (KeyValuePair<AbilityType, float> element in attackDictionary)
                    {
                        if (element.Key == targetPokemon.PrimaryType || element.Key == targetPokemon.SecondaryType)
                        {
                            damageThisTurn *= element.Value;
                        }
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


        //ENEMY FUNCTIONS
        private Attack EnemyAttackSelection()
        {
            int randomAttack = random.Next(0, (maxAttackOptions - 1));
            List<Attack> enemyAttacks = enemyPokemon.attacks;
            Attack enemyAttack = enemyAttacks[randomAttack];
            return enemyAttack;
        }

        private void EnemyAttack()
        {
            Attack enemyAttack = EnemyAttackSelection();
            float enemyAtkWithMultiplier = CalculateMultiplier(enemyAttack, myPokemon);
            DealDamage(myPokemon, enemyAtkWithMultiplier);
            ShowAttackMessage(enemyPokemon, enemyAttack, enemyAtkWithMultiplier);
        }


        //PLAYER FUNCTIONS
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
                    if (input >= 1 && input <= maxAttackOptions)
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

        public void DisplayAttacks()
        {
            Console.WriteLine("What do you want to do?\n");
            GetAttacks(myPokemon);
        }

        private void CreateEnemyList(List<Pokemon> enemyList)
        {
            enemyList.Add(new Pikachu());
            enemyList.Add(new Charmander());
            enemyList.Add(new Squirtle());
            enemyList.Add(new Bulbasaur());
            enemyList.Add(new Gyarados());
            enemyList.Add(new Quagsire());
            enemyList.Add(new Lanturn());
        }

        private void SpawnEnemy()
        {
            do
            {
                List<Pokemon> enemyList = new List<Pokemon>();
                CreateEnemyList(enemyList);
                Random random = new Random();
                int randomEnemy = random.Next(0, enemyList.Count);
                enemyPokemon = enemyList[randomEnemy];
            }
            while (enemyPokemon.GetBreed() == myPokemon.GetBreed());

            Console.WriteLine("A wild " + enemyPokemon.GetBreed() + " had appeared!");            
        }

    }
}
