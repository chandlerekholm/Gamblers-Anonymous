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
        public int playerCurrentBet { get; set; }         // the player's current bet amount
        public int playerTotalBet { get; set; }     // total bet amound for the round
        public List<Card> playerCards { get; set; } = default!;  // player's hole card
        public Hand playersHand { get; set; } = default!;       // The players current best hand
        public bool isWaiting { get; set; }         // Every player will initially be waiting
        public bool Folded { get; set; }            // Indication for when a player folds
        public bool allIn { get; set; }
        public bool IsBigBlind { get; set; } = false;
        public bool IsSmallBlind { get; set; } = false;

        public Player(string name, int chips)
        {
            this.playerName = name;
            this.playerChips = chips;
            this.playerCurrentBet = 0;
            this.playerTotalBet = 0;
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
            this.IsBigBlind = false;
            this.IsSmallBlind = false;

            IsBigBlind = isBB;
            IsSmallBlind = isSB;

            if(this.IsBigBlind == true)
            {
                Console.WriteLine(this.playerName + " has the Big Blind token.");
            }
            else if (this.IsSmallBlind == true)
            {
                Console.WriteLine(this.playerName + " has the Small Blind token.");
            }
            else
            {
                return;
            }
        }

        public void PayBigBlind(int chipAmount)
        {
            bool isBlind = true;
            this.playerChips -= chipAmount;
            this.UpdateCurrentBet(chipAmount, isBlind);
            Console.WriteLine(this.playerName + " pays " + chipAmount + " chips into the pot for the Big Blind and has " + this.playerChips + " chips left.");
        }

        public void PaySmallBlind(int chipAmount)
        {
            bool isBlind = true;
            this.playerChips -= chipAmount;
            this.UpdateCurrentBet(chipAmount, isBlind);
            Console.WriteLine(this.playerName + " pays " + chipAmount + " chips into the pot for the Small Blind and has " + this.playerChips + " chips left.");
        }

        public void UpdateCurrentBet(int chipAmount, bool isBlind)
        {
            
            this.playerCurrentBet += chipAmount;
            this.playerTotalBet += chipAmount;
            if (!isBlind)
            {
                Console.WriteLine(this.playerName + " has " + this.playerTotalBet + " chips in the pot.");
            }
        }

        public void ResetCurrentBet()
        {
            this.playerCurrentBet = 0;
        }

        public void ResetTotalBet()
        {
            this.playerTotalBet = 0;
        }

        public void Check()
        {
            // players stays
            Console.WriteLine(this.playerName + " checks.");
        }

        public int Call(int currentBet)
        {

            if (this.playerChips > (currentBet - this.playerCurrentBet))
            {
                // player has enough to call the current bet
                int additionalChips = currentBet - this.playerCurrentBet;
                Console.WriteLine(this.playerName + " calls and throws " + additionalChips + " chips into the pot.");
                this.playerChips -= additionalChips;
                return additionalChips;
            }
            else
            {
                // player is all in
                int allInAmount = this.playerChips;
                Console.WriteLine(this.playerName + " goes all in and throws " + allInAmount + " chips into the pot." );
                this.playerChips = 0;
                this.allIn = true;
                return allInAmount;
            }

        }

        public int Raise(int raiseAmount, int currentBet) // should return an int
        {

            if (this.playerChips > (raiseAmount + (currentBet - this.playerCurrentBet)))
            {
                // player has enough for the raise
                int totalRaise = (raiseAmount + (currentBet - this.playerCurrentBet));
                Console.WriteLine(this.playerName + " raises the pot by " + raiseAmount +  " and throws " + totalRaise + " chips into the pot");
                this.playerChips -= (raiseAmount + (currentBet - this.playerCurrentBet));
                return (raiseAmount + (currentBet - this.playerCurrentBet));
            }
            else if (this.playerChips == (raiseAmount + (currentBet - this.playerCurrentBet)))
            {
                // player is all in
                int allInAmount = this.playerChips;
                Console.WriteLine(this.playerName + " goes all in and throws " + allInAmount + " chips into the pot.");
                this.playerChips = 0;
                this.allIn = true;
                return allInAmount;
            }
            else
            {
                // player does not have enough chips for the raise, returning 0 to indicate unsuccessful raise
                int maxRaise = this.playerChips - (currentBet - this.playerCurrentBet);
                Console.WriteLine(this.playerName + " does not have enough chips for this raise. Maximum raise allowed would be " + maxRaise + " chips." );
                return 0; //this.playerChips - currentBet - this.playerCurrentBet
            }

        }

        public void Fold()
        {
            Console.WriteLine(this.playerName + " folds.");
            this.Folded = true;
        }

        public void printCards()
        {
            Console.WriteLine(this.playerName + " has a " + this.playerCards[0].Value + " of " + this.playerCards[0].Suit +
                              " and a " + this.playerCards[1].Value + " of " + this.playerCards[1].Suit + ".");
        }


        /*public void calcRank(int chips, bool wonHand)
        {
            Console.WriteLine("Player " + this.playerName + " rank changed.");
            // TODO: Implement this method
        }*/

    }
}


