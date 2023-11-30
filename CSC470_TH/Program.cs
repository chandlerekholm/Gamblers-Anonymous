using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH // Note: actual namespace depends on the project name.
{
    internal class Program
    {

      

        static void Main(string[] args)
        {

            Game game = new Game(200, 10, 5); // buy in amount, big blind, small blind
            Console.WriteLine("Starting game...");
            game.startGame();


        }
    }
}


