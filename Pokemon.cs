using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Tamagochi
{    public class Pokemon
    {
        protected string pType;
        public string name;
        public bool isAlive = true;

        public int hunger = 10;
        public int thirst = 10;
        public int happiness = 10;



        public string GetType()
        {
            return pType;
        }

        //REDUCE STATS
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

        public string GiveHint()
        {
            if (hunger < thirst && hunger < happiness)
            {
                string hungerHint = name + " the " + pType + " looks hungry.\n";
                return hungerHint;
            }
            else if (thirst < hunger && thirst < happiness)
            {
                string thirstHint = name + " the " + pType + " looks thirsty.\n";
                return thirstHint;
            }
            else
            {
                string happinessHint = name + " the " + pType + " looks bored.\n";
                return happinessHint;
            }

        }


        //INCREASE STATS
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
        public Pikachu()
        {
            pType = "Pikachu";
        }

    }
    public class Charmander : Pokemon
    {
        public Charmander()
        {
            pType = "Charmander";
        }
    }

    public class Squirtle : Pokemon
    {
        public Squirtle()
        {
            pType = "Squirtle";
        }
    }
}
