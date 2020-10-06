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
            ChooseAction(myPokemon);

        }

        public void ChooseAction(Pokemon myPokemon)
        {
            Console.Write("What do you want to do?\n");
            Attack slot1 = myPokemon.attackSlot1;
            Attack slot2 = myPokemon.attackSlot2;
            Attack slot3 = myPokemon.attackSlot3;
            Attack slot4 = myPokemon.attackSlot4;
            Console.Write(slot1.GetAttackName() + "\n");
            Console.Write(slot2.GetAttackName() + "\n");
            Console.Write(slot3.GetAttackName() + "\n");
            Console.Write(slot4.GetAttackName() + "\n");


        }


        public override bool Update()
        {
            return false;
        }
    }
}
