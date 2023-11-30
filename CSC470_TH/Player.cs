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
        public bool allIn {  get; set; }
        public bool IsBigBlind { get; set; } = false;
        public bool IsSmallBlind { get; set; } = false;

        public Player(string name)
        {
            this.playerName = name;
            this.playerChips = 0;
            this.playerRank = 0;
            this.playerCards = new List<Card>();
            this.playersHand = new Hand();
            this.isWaiting = true;
            this.Folded = false;
            this.allIn = false;
            IsBigBlind = false;
            IsSmallBlind = false;
        }

        public void SetBlinds(bool isBB, bool isSB)
        {
            IsBigBlind = isBB;
            IsSmallBlind = isSB;
        }

        public void PayBlind(int chipAmount)
        {
            this.playerChips -= chipAmount;
        }

        public void Check()
        {
            // players stays
            Console.WriteLine(this.playerName + " checks.");
        }

        public int Call(int currentBet)
        {
            Console.WriteLine(this.playerName + " calls.");


            if (this.playerChips > this.playerChips)
            {
                // player has enough to call the current bet
                Console.WriteLine(this.playerName + " calls.");
                this.playerChips -= currentBet;
                return currentBet;
            }
            else
            {
                // player is all in
                Console.WriteLine(this.playerName + " goes all in to call.");
                int allInAmount = this.playerChips;
                this.playerChips = 0;
                this.allIn = true;
                return allInAmount;
            }

        }

        public int Raise(int raiseAmount, int currentBet) // should return an int
        {
            if(this.playerChips > (raiseAmount + currentBet))
            {
                // player has enough for the raise
                Console.WriteLine(this.playerName + " raises.");
                this.playerChips -= (raiseAmount + currentBet);
                return (raiseAmount + currentBet);
            }
            else if(this.playerChips == (raiseAmount + currentBet))
            {
                // player is all in
                Console.WriteLine(this.playerName + " goes all in.");
                int allInAmount = this.playerChips;
                this.playerChips = 0;
                this.allIn = true;
                return allInAmount;
            }
            else
            {
                // player does not have enough chips for the raise, returning 0 to indicate unsuccessful raise
                Console.WriteLine(this.playerName + " does not have enough chips for this raise");
                return 0;
            }
           
        }

        public void Fold()
        {
            Console.WriteLine(this.playerName + " folds.");
            this.Folded = true;
        }


        /*public void calcRank(int chips, bool wonHand)
        {
            Console.WriteLine("Player " + this.playerName + " rank changed.");
            // TODO: Implement this method
        }*/

    }
}


