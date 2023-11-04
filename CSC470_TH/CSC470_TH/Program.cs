using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Player player1 = new Player("John", 500, 5);

            player1.Check();

            Player player2 = new Player("Sam", 450, 2);

            player2.Raise(25);

            Dealer dealer = new Dealer("Vegas Dealer");

            dealer.Shuffle();

            foreach (Card c in dealer.Deck)
            {
                Console.WriteLine(c.Value + " of " + c.Suit + " card id = " + c.ID);
            }


            

        }
    }
}


