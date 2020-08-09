using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagochi
{
    class Combat : Game
    {
        public override void StartGame()
        {
            Console.WriteLine("Welcome to the Pokemon Gym!\n");
            Pokemon myPokemon = SetPokemon();
            Console.WriteLine("This mode is under construction. Come back soon!\n");
            Console.ReadLine();

        }
    }
}
