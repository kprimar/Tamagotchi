using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    class Combat : Game
    {
        Pokemon enemyPokemon;
        List<Attack> enemyAttacks = new List<Attack>();

        Pokemon myPokemon;
        Random random = new Random();
        static int maxAttackOptions = 2;

        Dictionary<AbilityType, List<System.Type>> attackToTypeDictionary = new Dictionary<AbilityType, List<System.Type>>();
        Dictionary<AbilityType, Dictionary<AbilityType, float>> AttackToDictionary = new Dictionary<AbilityType, Dictionary<AbilityType, float>>();
        Dictionary<AbilityType, float> D_electricAffinities = new Dictionary<AbilityType, float>();
        Dictionary<AbilityType, float> D_fireAffinities = new Dictionary<AbilityType, float>();

        List<System.Type> electricAttackList = new List<System.Type>();
        List<System.Type> fireAttackList = new List<System.Type>();
        List<System.Type> waterAttackList = new List<System.Type>();
        List<System.Type> grassAttackList = new List<System.Type>();
        List<System.Type> fairyAttackList = new List<System.Type>();
        List<System.Type> flyingAttackList = new List<System.Type>();
        List<System.Type> groundAttackList = new List<System.Type>();
        List<System.Type> normalAttackList = new List<System.Type>();
        List<List<System.Type>> ListOfAttackLists = new List<List<Type>>();

        public override void StartGame()
        {
            Console.WriteLine("Welcome to the Pokemon Gym!\n");
            myPokemon = SetPokemon();
            CreateEnemyAttackLists();
            SpawnEnemy();
            SetEnemyAttacks();
            CreateAttackToTypeDictionary();
            CreateDictionaries();
        }

        private void CreateAttackToTypeDictionary()
        {
            attackToTypeDictionary.Add(AbilityType.Electric, electricAttackList);
            attackToTypeDictionary.Add(AbilityType.Fire, fireAttackList);
            attackToTypeDictionary.Add(AbilityType.Water, waterAttackList);
            attackToTypeDictionary.Add(AbilityType.Grass, grassAttackList);
            attackToTypeDictionary.Add(AbilityType.Fairy, fairyAttackList);
            attackToTypeDictionary.Add(AbilityType.Flying, flyingAttackList);
            attackToTypeDictionary.Add(AbilityType.Ground, groundAttackList);
            attackToTypeDictionary.Add(AbilityType.Normal, normalAttackList);
        }

        private void CreateDictionaries()
        {
            float superEffective = 2f;
            float neutral = 1f;
            float immune = 0f;
            float notVeryEffective = 2f;

            AttackToDictionary.Add(AbilityType.Electric, D_electricAffinities);
            AttackToDictionary.Add(AbilityType.Fire, D_fireAffinities);

            D_electricAffinities.Add(AbilityType.Normal, neutral);
            D_electricAffinities.Add(AbilityType.Flying, superEffective);
            D_electricAffinities.Add(AbilityType.Water, superEffective);
            D_electricAffinities.Add(AbilityType.Ground, immune);
            D_electricAffinities.Add(AbilityType.Grass, notVeryEffective);

            D_fireAffinities.Add(AbilityType.Normal, neutral);
            D_fireAffinities.Add(AbilityType.Fire, notVeryEffective);
            D_fireAffinities.Add(AbilityType.Water, notVeryEffective);
            D_fireAffinities.Add(AbilityType.Grass, superEffective);
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

        private void CreateEnemyList(List<System.Type> enemyList)
        {
            enemyList.Add(typeof(Pikachu));
            enemyList.Add(typeof(Charmander));
            enemyList.Add(typeof(Squirtle));
            enemyList.Add(typeof(Bulbasaur));
            enemyList.Add(typeof(Gyarados));
            enemyList.Add(typeof(Quagsire));
            enemyList.Add(typeof(Lanturn));
        }
        private void CreateEnemyAttackLists()
        {
            electricAttackList.Add(typeof(Thunderbolt));
            electricAttackList.Add(typeof(ThunderShock));

            fireAttackList.Add(typeof(FirePunch));
            fireAttackList.Add(typeof(FlameThrower));

            waterAttackList.Add(typeof(HydroCannon));
            waterAttackList.Add(typeof(Splash));

            grassAttackList.Add(typeof(LeafBlade));
            grassAttackList.Add(typeof(RazorLeaf));

            fairyAttackList.Add(typeof(SpiritBreak));
            fairyAttackList.Add(typeof(SoulDrain));

            flyingAttackList.Add(typeof(FloatyFall));
            flyingAttackList.Add(typeof(Flutter));

            groundAttackList.Add(typeof(Earthquake));
            groundAttackList.Add(typeof(Landslide));

            normalAttackList.Add(typeof(DoubleHit));
            normalAttackList.Add(typeof(DoubleSlap));

            ListOfAttackLists.Add(electricAttackList);
            ListOfAttackLists.Add(fireAttackList);
            ListOfAttackLists.Add(waterAttackList);
            ListOfAttackLists.Add(grassAttackList);
            ListOfAttackLists.Add(fairyAttackList);
            ListOfAttackLists.Add(flyingAttackList);
            ListOfAttackLists.Add(groundAttackList);
            ListOfAttackLists.Add(normalAttackList);

        }

        private void SetEnemyAttacks()
        {
            AbilityType enemyPType = enemyPokemon.PrimaryType;
            AbilityType enemySType = enemyPokemon.SecondaryType;

            Attack attack1 = null;
            Attack attack2 = null;
            Attack attack3 = null;
            Attack attack4 = null;

            foreach (KeyValuePair<AbilityType, List<System.Type>> attackList in attackToTypeDictionary)
            {
                if (attackList.Key == enemyPType)
                {
                    List<System.Type> possibleAttacks = attackList.Value;
                    int randomAttack = random.Next(0, possibleAttacks.Count);
                    attack1 = Activator.CreateInstance(possibleAttacks[randomAttack]) as Attack;
                }
            }

            if (enemySType != AbilityType.None)
            {
                foreach (KeyValuePair<AbilityType, List<System.Type>> attackList in attackToTypeDictionary)
                {
                    if (attackList.Key == enemySType)
                    {
                        List<System.Type> possibleAttacks = attackList.Value;
                        int randomAttack = random.Next(0, possibleAttacks.Count);
                        attack2 = Activator.CreateInstance(possibleAttacks[randomAttack]) as Attack;
                    }
                }
            }


            enemyPokemon.attacks.Add(attack1);
            enemyPokemon.attacks.Add(attack2);

        }


        private void SpawnEnemy()
        {
            System.Type enemyType = null;
            List<System.Type> enemyList = new List<System.Type>();
            CreateEnemyList(enemyList);

            do
            {             
                int randomEnemy = random.Next(0, enemyList.Count);
                enemyType = enemyList[randomEnemy];
            }
            while (enemyType.GetType() == myPokemon.GetType());

            enemyPokemon = Activator.CreateInstance(enemyType) as Pokemon;
            Console.WriteLine("A wild " + enemyPokemon.GetBreed() + " had appeared!");            
        }

    }
}
