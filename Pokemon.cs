using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Tamagochi
{    public class Pokemon
    {
        protected string pType;
        protected string name;
        protected bool isAlive = true;
        protected bool hasThirst;
        protected int hunger = 10;
        protected int thirst = 10;
        protected int happiness = 10;


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
                string hungerHint = "\n" + name + " the " + pType + " looks hungry.\n";
                return hungerHint;
            }
            else if (thirst <= hunger && thirst <= happiness)
            {
                string thirstHint = "\n" + name + " the " + pType + " looks thirsty.\n";
                return thirstHint;
            }
            else
            {
                string happinessHint = "\n" + name + " the " + pType + " looks bored.\n";
                return happinessHint;
            }

        }

        public void SetName(string nameInput)
        {
            name = nameInput;
        }

        public string GetName()
        {
            return name;
        }


        //GET PROTECTED DATA
        public string GetType()
        {
            return pType;
        }

        public int GetHunger()
        {
            return hunger;
        }

        public bool GetsThirsty()
        {
            return hasThirst;
        }

        public int GetThirst()
        {
            return thirst;
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

    }

    public class Pikachu : Pokemon
    {
        public Pikachu()
        {
            pType = "Pikachu";
            hasThirst = true;
    }

    }
    public class Charmander : Pokemon
    {
        public Charmander()
        {
            pType = "Charmander";
            hasThirst = true;
        }
    }

    public class Squirtle : Pokemon
    {
        public Squirtle()
        {
            pType = "Squirtle";
            hasThirst = false;
        }
    }
}
