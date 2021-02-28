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
        static int maxAttackOptions = 4;

        Dictionary<AbilityType, Dictionary<AbilityType, float>> MultiplyerDictionary = new Dictionary<AbilityType, Dictionary<AbilityType, float>>();
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
        Dictionary<AbilityType, List<System.Type>> attackToTypeDictionary = new Dictionary<AbilityType, List<System.Type>>();



        public override void StartGame()
        {
            if (!isReplay)
            {
                CreateEnemyAttackLists();
                CreateAttackToTypeDictionary();
                CreateDictionaries();
            }
            Console.WriteLine("Welcome to the Pokemon Gym!\n");
            myPokemon = SetPokemon();
            SetRandomAttacks(myPokemon);
            SpawnEnemy();
            SetRandomAttacks(enemyPokemon);
            foreach (Attack attack in enemyPokemon.attacks)
            {
                Console.WriteLine("DEBUG ENEMY ATTACKS \n" + attack.GetAttackName().ToString());
            }
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

            MultiplyerDictionary.Add(AbilityType.Electric, D_electricAffinities);
            MultiplyerDictionary.Add(AbilityType.Fire, D_fireAffinities);

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
            foreach (KeyValuePair<AbilityType, Dictionary<AbilityType, float>> dictionary in MultiplyerDictionary)
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
        }

        private void SetRandomAttacks(Pokemon targetPokemon)
        {
            AbilityType PType = targetPokemon.PrimaryType;
            AbilityType SType = targetPokemon.SecondaryType;

            Attack attack1 = GetRandomAttackForType(PType);
            targetPokemon.attacks.Add(attack1);

            if (SType != AbilityType.None)
            {
                Attack attack2 = GetRandomAttackForType(SType);
                targetPokemon.attacks.Add(attack2);
            }
            while (targetPokemon.attacks.Count < maxAttackOptions)
            {
                Attack rndAttack = GetRandomAttack(targetPokemon.attacks);
                targetPokemon.attacks.Add(rndAttack);
            }
        }

        private Attack GetRandomAttack(List<Attack> blacklist)
        {
            Attack rndAttack = null;
            bool pickAttack = false;
            do
            {
                pickAttack = false;
                Array abilityTypeArray = Enum.GetValues(typeof(AbilityType));
                AbilityType rndAbilityType;
                do
                {
                    rndAbilityType = (AbilityType)abilityTypeArray.GetValue(random.Next(0, abilityTypeArray.Length));
                }
                while (rndAbilityType == AbilityType.None);
                rndAttack = GetRandomAttackForType(rndAbilityType);
                foreach (Attack blacklistedAttack in blacklist)
                {
                    if (blacklistedAttack.GetType() == rndAttack.GetType())
                    {
                        pickAttack = true;
                        break;
                    }
   
                }
            } while (pickAttack);

            return rndAttack;
        }

        private Attack GetRandomAttackForType(AbilityType abilityType)
        {
            Attack rndAttack = null;
            List<System.Type> typeList;
            if (attackToTypeDictionary.TryGetValue(abilityType, out typeList))
            {
                int randomAttack = random.Next(0, typeList.Count);
                rndAttack = Activator.CreateInstance(typeList[randomAttack]) as Attack;
            }
            else { Console.WriteLine("not found"); }
            return rndAttack; 
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
            while (enemyType == myPokemon.GetType());

            enemyPokemon = Activator.CreateInstance(enemyType) as Pokemon;
            Console.WriteLine("A wild " + enemyPokemon.GetBreed() + " had appeared!");            
        }

    }
}
