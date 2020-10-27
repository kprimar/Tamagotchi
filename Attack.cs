using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    public class Attack
    {
        protected AbilityType attackType;
        protected string attackName;
        protected int minAttackStrength;
        protected int maxAttackStrength;

        public string GetAttackName()
        {
            return attackName;
        }

        public void GetDamage()
        {

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
            maxAttackStrength = 10;
        }
    }

    class FirePunch : Attack
    {
        public FirePunch()
        {
            attackType = AbilityType.Fire;
            minAttackStrength = 5;
            maxAttackStrength = 10;
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
            maxAttackStrength = 10;
        }
    }

    class FloatyFall : Attack
    {
        public FloatyFall()
        {
            attackType = AbilityType.Flying;
            minAttackStrength = 5;
            maxAttackStrength = 10;
        }
    }

    class Earthquake : Attack
    {
        public Earthquake()
        {
            attackType = AbilityType.Ground;
            minAttackStrength = 5;
            maxAttackStrength = 10;
        }
    }

    class LeafBlade : Attack
    {
        public LeafBlade()
        {
            attackType = AbilityType.Grass;
            minAttackStrength = 5;
            maxAttackStrength = 10;
        }
    }

    class DoubleHit : Attack
    {
        public DoubleHit()
        {
            attackType = AbilityType.Normal;
            minAttackStrength = 5;
            maxAttackStrength = 10;
            attackName = "DoubleHit";
        }
    }

    class DoubleSlap : Attack
    {
        public DoubleSlap()
        {
            attackType = AbilityType.Normal;
            minAttackStrength = 5;
            maxAttackStrength = 10;
            attackName = "DoubleSlap";
        }
    }
}
