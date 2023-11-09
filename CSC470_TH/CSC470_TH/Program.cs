using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH // Note: actual namespace depends on the project name.
{
    class Program
    {

        static void Main(string[] args)
        {
            //bool canCheck = new bool();
            bool canCheck = true;

            Console.WriteLine("Game Started");

            Player player1 = new Player("John", 500, 5);
            Player player2 = new Player("Sam", 450, 2);

            Dealer dealer = new Dealer("Vegas Dealer");

            dealer.Shuffle();

            player1.Raise(25);
            canCheck = false;
            player2.Call();
            Console.WriteLine(player1.playerName + " now has " + player1.playerChips + " chips");
            Console.WriteLine(player2.playerName + " now has " + player2.playerChips + " chips");
            Console.WriteLine("New turn");
            canCheck = true;
            player1.Check();
            player2.Check();
            Console.WriteLine("New turn");




            // foreach (Card c in dealer.Deck)
            //  {
            //      Console.WriteLine(c.Value + " of " + c.Suit + " card id = " + c.ID);
            // }




        }
    }
}


