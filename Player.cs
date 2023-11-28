// How are we going to handle userChips? Do we just have an ammount given to the player
// depending on the table with unlimited buy ins if the player losses all chips? Or are
// we going to keep each players chip amount in the db? 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH
{
    public class Player
    {
        public string playerName { get; set; }      // playerName stored in db, part of profile setup 
        public int playerRank { get; set; }         // playerRank determined by number of hands won, stored in db
        public int playerChips { get; set; }        // Amount of chips to be distributed to the player based on the table 
        public List<Card> playerCards { get; set; } = default!;  // player's hole card
        public Hand playersHand { get; set; } = default!;       // The players current best hand
        public bool isWaiting { get; set; }         // Every player will initially be waiting
        public bool Folded { get; set; }            // Indication for when a player folds

        
        public Player(string name)
        {
            this.playerName = name;
            this.playerChips = 0;
            this.playerRank = 0;
            this.playerCards = new List<Card>();
            this.playersHand = new Hand();
            this.isWaiting = true;
            this.Folded = false;

        }

        public void Check()
        {
            Console.WriteLine("Player " + this.playerName + " checked.");
            // TODO: Implement this method
        }

        public void Call() //should return an int
        {
            Console.WriteLine("Player " + this.playerName + " called.");
            // TODO: Implement this method
        }

        public void Raise(int chipAmount) // should return an int
        {
            Console.WriteLine("Player " + this.playerName + " raised by " + chipAmount);
            // TODO: Implement this method
        }

        public void Fold()
        {
            Console.WriteLine("Player " + this.playerName + " folded.");
            // TODO: Implement this method
            Folded = true;
        }


        public void calcRank(int chips, bool wonHand)
        {
            Console.WriteLine("Player " + this.playerName + " rank changed.");
            // TODO: Implement this method
        }

    }
}


