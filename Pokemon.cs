using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Tamagochi
{    public class Pokemon
    {
        //GENERAL POKEMON DATA
        protected string breed;
        protected string name;
        protected int maxHP = 20;
        protected int currentHP;
        public AbilityType PrimaryType;
        public AbilityType SecondaryType;

        public Pokemon()
        {
            currentHP = maxHP;
            attacks = new List<Attack>();
            {
                new Empty();
            }
        }

        //ATTACKS
        public List<Attack> attacks = new List<Attack>();

        //POKEMON HEALTH DATA
        protected bool isAlive = true;
        protected bool hasThirst;
        protected int hunger = 10;
        protected int thirst = 10;
        protected int happiness = 10;
        protected int StartingHP = 50;

        //MODIFY STATS
        public int StatDecreaseThisTurn()
        {
            Random random = new Random();
            return random.Next(0, 5);
        }
        public void ReduceStats()
        {
            int hungerChange = StatDecreaseThisTurn();
            hunger -= hungerChange;

            if(hasThirst)
            {
                int thirstChange = StatDecreaseThisTurn();
                thirst -= thirstChange;
            }

            int happinessChange = StatDecreaseThisTurn();
            happiness -= happinessChange;
        }
        public void HungerUp()
        {
            hunger += 5;
            if (hunger > 10)
            {
                hunger = 10;
            }
        }
        public void ThirstUp()
        {
            thirst += 5;
            if (thirst > 10)
            {
                thirst = 10;
            }
        }
        public void HappinessUp()
        {
            happiness += 5;
            if (happiness > 10)
            {
                happiness = 10;
            }
        }
        public string GiveHint()
        {
            if (hunger <= thirst && hunger <= happiness)
            {
                string hungerHint = "\n" + name + " the " + breed + " looks hungry.\n";
                return hungerHint;
            }
            else if (thirst <= hunger && thirst <= happiness)
            {
                string thirstHint = "\n" + name + " the " + breed + " looks thirsty.\n";
                return thirstHint;
            }
            else
            {
                string happinessHint = "\n" + name + " the " + breed + " looks bored.\n";
                return happinessHint;
            }

        }


        //GET PROTECTED DATA
        public void SetName(string nameInput)
        {
            name = nameInput;
        }
        public string GetName()
        {
            return name;
        }
        public string GetBreed()
        {
            return breed;
        }
        public int GetHappiness()
        {
            return happiness;
        }
        public bool CheckHealth()
        {
            if (hunger <= 0 || thirst <= 0 || happiness <= 0)
            {
                isAlive = false;
            }
            return isAlive;
        }
        public int GetCurrentHP()
        {
            return currentHP;
        }
        public int GetMaxHP()
        {
            return maxHP;
        }
        public int ReduceHP(int damage)
        {
            currentHP = currentHP -= damage;
            return currentHP;
        }
        public int GetHunger()
        {
            return hunger;
        }
        public int GetThirst()
        {
            return thirst;
        }
        public bool GetsThirsty()
        {
            return hasThirst;
        }

    }

    public class Pikachu : Pokemon
    {
        public Pikachu()
        {
            breed = "Pikachu";
            hasThirst = true;
            PrimaryType = AbilityType.Electric;
            SecondaryType = AbilityType.None;
            attacks = new List<Attack>()
            {
            new Thunderbolt(),
            new DoubleHit(),
            new DoubleSlap(),
            new Empty(),
            };

        }
    }
    public class Charmander : Pokemon
    {
        public Charmander()
        {
            breed = "Charmander";
            hasThirst = true;
            PrimaryType = AbilityType.Fire;
            SecondaryType = AbilityType.Flying;
        }
    }
    public class Squirtle : Pokemon
    {
        public Squirtle()
        {
            breed = "Squirtle";
            hasThirst = false;
            PrimaryType = AbilityType.Water;
            SecondaryType = AbilityType.None;
        }
    }
    public class Bulbasaur : Pokemon
    {
        public Bulbasaur()
        {
            breed = "Bulbasaur";
            hasThirst = false;
            PrimaryType = AbilityType.Grass;
            SecondaryType = AbilityType.None;
        }
    }
    public class Gyarados : Pokemon
    {
        public Gyarados()
        {
            breed = "Gyarados";
            hasThirst = false;
            PrimaryType = AbilityType.Water;
            SecondaryType = AbilityType.Flying;
        }
    }
    public class Lanturn : Pokemon
    {
        public Lanturn()
        {
            breed = "Lanturn";
            hasThirst = false;
            PrimaryType = AbilityType.Water;
            SecondaryType = AbilityType.Electric;
        }
    }
    public class Quagsire : Pokemon
    {
        public Quagsire()
        {
            breed = "Lanturn";
            hasThirst = false;
            PrimaryType = AbilityType.Water;
            SecondaryType = AbilityType.Ground;
        }
    }
}
