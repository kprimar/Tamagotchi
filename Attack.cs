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
    class ThunderShock : Attack
    {
        public ThunderShock()
        {
            attackType = AbilityType.Electric;
            attackName = "Thundershock";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class FirePunch : Attack
    {
        public FirePunch()
        {
            attackType = AbilityType.Fire;
            attackName = "Fire Punch";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }
    class FlameThrower : Attack
    {
        public FlameThrower()
        {
            attackType = AbilityType.Fire;
            attackName = "Flame Thrower";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class HydroCannon : Attack
    {
        public HydroCannon()
        {
            attackType = AbilityType.Water;
            attackName = "Hydro Cannon";
            minAttackStrength = 5;
            maxAttackStrength = 10;
        }
    }
    class Splash : Attack
    {
        public Splash()
        {
            attackType = AbilityType.Water;
            attackName = "Splash";
            minAttackStrength = 5;
            maxAttackStrength = 10;
        }
    }

    class SpiritBreak : Attack
    {
       public SpiritBreak()
       {
           attackType = AbilityType.Fairy;
           attackName = "Spirit Break";
           minAttackStrength = 5;
           maxAttackStrength = 7;
       }
    }
    class SoulDrain : Attack
    {
        public SoulDrain()
        {
            attackType = AbilityType.Fairy;
            attackName = "Soul Drain";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class FloatyFall : Attack
    {
        public FloatyFall()
        {
            attackType = AbilityType.Flying;
            attackName = "Floaty Fall";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }
    class Flutter : Attack
    {
        public Flutter()
        {
            attackType = AbilityType.Flying;
            attackName = "Flutter";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class Earthquake : Attack
    {
        public Earthquake()
        {
            attackType = AbilityType.Ground;
            attackName = "Earthquake";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }
    class Landslide : Attack
    {
        public Landslide()
        {
            attackType = AbilityType.Ground;
            attackName = "Landslide";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }

    class LeafBlade : Attack
    {
        public LeafBlade()
        {
            attackType = AbilityType.Grass;
            attackName = "Leaf Blade";
            minAttackStrength = 5;
            maxAttackStrength = 7;
        }
    }
    class RazorLeaf : Attack
    {
        public RazorLeaf()
        {
            attackType = AbilityType.Grass;
            attackName = "Razor Leaf";
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
            attackName = "Double Hit";
        }
    }

    class DoubleSlap : Attack
    {
        public DoubleSlap()
        {
            attackType = AbilityType.Normal;
            minAttackStrength = 5;
            maxAttackStrength = 7;
            attackName = "Double Slap";
        }
    }
}