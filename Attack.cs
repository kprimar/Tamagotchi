using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    class Attack
    {
        protected PokemonType attackType;
        protected int minAttackStrength;
        protected int maxAttackStrength;
    }

    class Thunderbolt : Attack
    {
        public Thunderbolt()
        {
            attackType = PokemonType.Electric;
            minAttackStrength = 5;
            maxAttackStrength = 10;
        }

    }
}
