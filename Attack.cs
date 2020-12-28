using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    public class Attack
    {
        Random damage = new Random();
        protected AbilityType attackType;
        protected string attackName;
        protected int minAttackStrength;
        protected int maxAttackStrength;
        protected int damageMultiplier;



        public AbilityType GetAttackType()
        {
            return attackType;
        }

        public string GetAttackName()
        {
            return attackName;
        }

        public int GetDamage()
        {
            int damageThisTurn = damage.Next(minAttackStrength, maxAttackStrength);
            return damageThisTurn;
        }

        public int GetMaxDmg()
        {
            return maxAttackStrength;
        }
        public int GetMinDmg()
        {
            return minAttackStrength;
        }


    }

    class Empty : Attack
    {
        public Empty()
        {
            attackType = AbilityType.None;
            attackName = "Empty";
            minAttackStrength = 0;
            maxAttackStrength = 0;
        }
    }

    class Thunderbolt : Attack
    {
        public Thunderbolt()
        {
            attackType = AbilityType.Electric;
            attackName = "Thunderbolt";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class FirePunch : Attack
    {
        public FirePunch()
        {
            attackType = AbilityType.Fire;
            attackName = "FirePunch";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class HydroCannon : Attack
    {
        public HydroCannon()
        {
            attackType = AbilityType.Water;
            minAttackStrength = 5;
            maxAttackStrength = 10;
        }
    }

    class SpiritBreak : Attack
    {
       public SpiritBreak()
       {
           attackType = AbilityType.Fairy;
           minAttackStrength = 5;
           maxAttackStrength = 7;
       }
    }

    class FloatyFall : Attack
    {
        public FloatyFall()
        {
            attackType = AbilityType.Flying;
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class Earthquake : Attack
    {
        public Earthquake()
        {
            attackType = AbilityType.Ground;
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class LeafBlade : Attack
    {
        public LeafBlade()
        {
            attackType = AbilityType.Grass;
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class DoubleHit : Attack
    {
        public DoubleHit()
        {
            attackType = AbilityType.Normal;
            minAttackStrength = 5;
            maxAttackStrength = 7;
            attackName = "DoubleHit";
        }
    }

    class DoubleSlap : Attack
    {
        public DoubleSlap()
        {
            attackType = AbilityType.Normal;
            minAttackStrength = 5;
            maxAttackStrength = 7;
            attackName = "DoubleSlap";
        }
    }
}