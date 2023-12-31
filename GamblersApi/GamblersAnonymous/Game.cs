﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GamblersAnonymous
{
    // The game class serves and the primary driver for the game engine
    // proceding through hands, rounds, and betting is primariy preformed here

    public class Game
    {
        public List<Player> players { get; set; } = default!;
        public List<Card> deck { get; set; } = default!;
        public List<Card> communityCards { get; set; } = default!;
        public List<Card> burnCards { get; set; } = default!;
        public Dealer dealer { get; set; }
        public List<Player> roundWinners { get; set; } = default!;
        public int buyInAmount { get; set; }
        public int smallBlind { get; set; }
        public int bigBlind { get; set; }
        public int potAmount { get; set; }
        public int totalPotAmount { get; set; }
        public Player bigBlindPlayer { get; set; } = default!;
        public Player smallBlindPlayer { get; set; } = default!;
        public int currentSBPlayerIndex { get; set; }
        public int currentBBPlayerIndex { get; set; }
        public int twoPlayerSwitch {  get; set; }
        public int startPlayerIndex { get; set; }
        public int numRounds {  get; set; }
        public int currentBet {  get; set; }


        public Game(int buyIn, int BB, int SB)
        {
            this.currentSBPlayerIndex = 0;
            this.currentBBPlayerIndex = 1;
            this.twoPlayerSwitch = 0;
            this.startPlayerIndex = 0;
            this.buyInAmount = buyIn;
            this.smallBlind = SB;
            this.bigBlind = BB;
            this.players = InitializePlayers();
            this.deck = InitializeDeck();
            this.communityCards = new List<Card>();
            this.burnCards = new List<Card>();
            this.dealer = new Dealer("Vegas Dealer");
            this.roundWinners = new List<Player>();
            this.potAmount = 0;
            this.totalPotAmount = 0;
            this.numRounds = 0;
        }

        private List<Player> InitializePlayers()
        {
            // player initialization will eventually be completed through online
            // player UI
            List<Player> initialPlayers = new List<Player>();

            initialPlayers.Add(new Player("John", this.buyInAmount));
            initialPlayers.Add(new Player("Steve", this.buyInAmount));
            initialPlayers.Add(new Player("Matt", this.buyInAmount));
            //initialPlayers.Add(new Player("Tom"));
            //initialPlayers.Add(new Player("Jason"));

            return initialPlayers;

        }

        private List<Card> InitializeDeck()
        {
            string[] Suits = { "Clubs", "Spades", "Hearts", "Diamonds" };
            int[] Values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            List<Card> initialDeck = new List<Card>();
            int id = 0;

            foreach (string s in Suits)
            {
                foreach (int v in Values)
                {
                    id++;
                    Card card = new Card(id, s, v);
                    initialDeck.Add(card);
                }
            }
            return initialDeck;
        }

      
        public async void startGame()
        {
            while (this.players.Count > 1)
            {
                
                StartRound();
                EvaluateRound();
                ResetRound();

            }
            Console.WriteLine(this.players[0].playerName + " wins the game with a total winnings of " + this.totalPotAmount);

        }

        private void StartRound()
        {
            this.numRounds++;
            this.startPlayerIndex = this.currentSBPlayerIndex;
            Console.WriteLine("Number of players for round " + this.numRounds + ": ");
            int numPlayers = 1;
            Console.WriteLine("***********************************************************************************");
            for (int i = 1; i <= this.players.Count; i++)
            {
                Console.WriteLine("Player " + i + " = " + this.players[i - 1].playerName + " with " + this.players[i - 1].playerChips + " chips.");
                numPlayers++;
            }
            Console.WriteLine("***********************************************************************************\n");

            if (this.players.Count == 2)
            {
                if(twoPlayerSwitch == 0)
                {
                    this.smallBlindPlayer = this.players[0];
                    this.bigBlindPlayer = this.players[1];
                    currentSBPlayerIndex = 0;
                    twoPlayerSwitch++;
                    Console.WriteLine("***********************************************************************************");
                    Console.WriteLine("Round " + this.numRounds + " Small Blind Player = " + this.smallBlindPlayer.playerName + ".");
                    Console.WriteLine("Round " + this.numRounds + " Big Blind Player = " + this.bigBlindPlayer.playerName + ".");
                    Console.WriteLine("***********************************************************************************\n");
                }
                else
                {
                    this.smallBlindPlayer = this.players[1];
                    this.bigBlindPlayer = this.players[0];
                    currentSBPlayerIndex = 1;
                    twoPlayerSwitch--;
                    Console.WriteLine("***********************************************************************************");
                    Console.WriteLine("Round " + this.numRounds + " Small Blind Player = " + this.smallBlindPlayer.playerName + ".");
                    Console.WriteLine("Round " + this.numRounds + " Big Blind Player = " + this.bigBlindPlayer.playerName + ".");
                    Console.WriteLine("***********************************************************************************\n");

                }
            }
            else
            {
                this.smallBlindPlayer = GetNextSBPlayer();
                this.bigBlindPlayer = GetNextBBPlayer();
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("Round " + this.numRounds + " Small Blind Player = " + this.smallBlindPlayer.playerName + ".");
                Console.WriteLine("Round " + this.numRounds + " Big Blind Player = " + this.bigBlindPlayer.playerName + ".");
                Console.WriteLine("***********************************************************************************\n");

            }

            Console.WriteLine("***********************************************************************************");
            this.smallBlindPlayer.PaySmallBlind(smallBlind);
            this.bigBlindPlayer.PayBigBlind(bigBlind);
            Console.WriteLine("***********************************************************************************\n");

            this.potAmount = 0;
            this.potAmount += (this.bigBlind + this.smallBlind);
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Round " + numRounds + " starts with " + this.potAmount + " chips in the pot.");
            Console.WriteLine("***********************************************************************************\n");

            this.currentBet = this.bigBlind;

            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("The Dealer is shuffling deck.");
            Console.WriteLine("***********************************************************************************\n");
            dealer.Shuffle(this.deck);

            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("The Dealer is dealing out player cards.");
            Console.WriteLine("***********************************************************************************\n");
            dealer.dealPlayerCards(this.deck, this.players);


            // betting round before the flop
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Betting round before the flop begins.");
            Console.WriteLine("***********************************************************************************\n");
            BettingRound();

            // dealer burns a card and then deals the flop
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("The Dealer is dealing out the flop.");
            Console.WriteLine("***********************************************************************************\n");
            dealer.burnCard(deck, burnCards);
            dealer.dealFLop(deck, communityCards);
            this.printCommunityCards();



            // betting round after the flop
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Betting round after the flop begins.");
            Console.WriteLine("***********************************************************************************\n");
            BettingRound();


            // dealer burns a card and then deals out the turn
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Dealer is dealing out the turn card.");
            Console.WriteLine("***********************************************************************************\n");
            dealer.burnCard(deck, burnCards);
            dealer.dealTurnRiver(deck, communityCards);
            this.printCommunityCards();

            // betting round after the turn
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Betting round after the turn begins.");
            Console.WriteLine("***********************************************************************************\n");
            BettingRound();

            // dealer burns a card and then deals out the river
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Dealer is dealing out the river card.");
            Console.WriteLine("***********************************************************************************\n");
            dealer.burnCard(deck, burnCards);
            dealer.dealTurnRiver(deck, communityCards);
            this.printCommunityCards();


            // final round of betting
            Console.WriteLine("***********************************************************************************");
            Console.WriteLine("Betting round after the river begins.");
            Console.WriteLine("***********************************************************************************\n");
            BettingRound();

        }

        public Player GetNextSBPlayer()
        {
            Player nextSBPlayer = players[currentSBPlayerIndex];
            nextSBPlayer.SetBlinds(isSB: true, isBB: false);
            currentSBPlayerIndex = (currentSBPlayerIndex + 1) % players.Count;
            return nextSBPlayer;
        }

        public Player GetNextBBPlayer()
        {
       
            Player nextBBPlayer = players[currentBBPlayerIndex];
            nextBBPlayer.SetBlinds(isSB: false, isBB: true);
            currentBBPlayerIndex = (currentBBPlayerIndex + 1) % players.Count;
            return nextBBPlayer;
        }

        private void EvaluateRound()
        {
            // reset the winner(s)
            this.roundWinners.Clear();

            // DeterminWinner return the winning player, or a list of players if there is a tie
            roundWinners = DetermineWinner(this.players, this.communityCards);
            printWinners();
        }

        private void ResetRound()
        {
            rewardPot();
            Console.WriteLine("Dealer resets table...");
            dealer.resetTable(this.players, this.deck, this.communityCards, this.burnCards);
            for(int i = 0; i < this.players.Count - 1; i++)
            {
                this.players[i].ResetTotalBet();
                this.players[i].Folded = false;
                this.players[i].allIn = false;

                if (this.players[i].playerChips <= 0)
                {
                    Console.WriteLine(this.players[i].playerName + " has 0 chips left and leaves the table");
                    this.players.RemoveAt(i);
                }
            }
        }

        private void BettingRound()
        {

            // Keep track of players who have not folded
            List<Player> activePlayers = new List<Player>();
            if (this.players.Count == 2)
            {

                activePlayers = new List<Player>(this.players.Skip(this.currentSBPlayerIndex).Concat(players.Take(currentSBPlayerIndex)));
            }
            else
            {
                activePlayers = new List<Player>(this.players.Skip(this.startPlayerIndex).Concat(players.Take(startPlayerIndex)));
            }

            // Keep track of the player who made the last raise
            Player lastRaiser = null;
            bool initPass = true;
            int buyIn = this.currentBet;

            // Betting continues until all active players have made the same bet or have folded
            while ((activePlayers.Count > 1 && !AllPlayersHaveSameBet(activePlayers, this.currentBet)) || (activePlayers.Count > 1 &&  initPass))
            {
                initPass = false;
                
                foreach (Player player in activePlayers.ToList()) 
                {
                    if(player == lastRaiser)
                    {
                        break;
                    }


                    if (player.Folded != true && player.allIn != true)
                    {
                        Console.WriteLine("***********************************************************************************");
                        Console.WriteLine("It's " + player.playerName + "'s turn.");
                        Console.WriteLine(player.playerName + "'s chips = " + player.playerChips);
                        Console.WriteLine("Current buy in = " + buyIn + "\n\n");
                        // Display options and get user input
                        Console.WriteLine("||| What do you want to do? |||");
                        Console.WriteLine("1. Check");
                        Console.WriteLine("2. Call");
                        Console.WriteLine("3. Raise");
                        Console.WriteLine("4. Fold");
                        Console.WriteLine("***********************************************************************************\n");

                        int userChoice;
                        bool validChoice = false;

                        do
                        {
                            Console.Write("Choose your action (1-4): ");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out userChoice) && userChoice >= 1 && userChoice <= 4)
                            {
                                
                                if(userChoice == 1 && player.playerCurrentBet < this.currentBet)
                                {
                                    Console.WriteLine(player.playerName + " needs to buy in before checking.");
                                }
                                else if (userChoice == 2 && player.playerCurrentBet == this.currentBet)
                                {
                                    Console.WriteLine(player.playerName + " is already at the current bet, please check or raise");
                                }
                                else
                                {
                                    validChoice = true;
                                }
                              
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                            }
                        } while (!validChoice);

                        int amountToBet = 0;
                        bool isBlind = false;

                        // Process user choice
                        switch (userChoice)
                        {
                            case 1: // Check
                                Console.WriteLine("***********************************************************************************");
                                player.Check();
                                player.UpdateCurrentBet(0, isBlind);
                                Console.WriteLine("***********************************************************************************\n");
                                break;

                            case 2: // Call
                                Console.WriteLine("***********************************************************************************");
                                amountToBet = player.Call(this.currentBet);
                                player.UpdateCurrentBet(amountToBet, isBlind);
                                Console.WriteLine("***********************************************************************************\n");
                                break;

                            case 3: // Raise
                                do
                                {
                                    validChoice = false;  // reset valid choice to ensure player gets another chance if an invalid input is entered
                                    Console.Write("Enter the raise amount: ");
                                    string raiseInput = Console.ReadLine();

                                    if (int.TryParse(raiseInput, out int raiseAmount))
                                    {
                                        Console.WriteLine("***********************************************************************************");
                                        amountToBet = player.Raise(raiseAmount, this.currentBet);
                                        player.UpdateCurrentBet(amountToBet, isBlind);
                                        Console.WriteLine("***********************************************************************************\n");
                                        buyIn = this.currentBet + raiseAmount;
                                        if(amountToBet > 0)
                                        {
                                            lastRaiser = player; // assign lastRaiser
                                            validChoice = true; // Exit the loop as the input is valid and players has enough for the raise
                                        }
                                      
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid integer for the raise amount.");
                                    }
                                } while (!validChoice);
                                break;

                            case 4: // Fold
                                player.Fold();
                                break;

                            default:
                                break;
                        }

                        // Update the game state based on the player's action
                        this.potAmount += amountToBet;

                        // Update the current bet if the player's bet is higher
                        if (buyIn > currentBet)
                        {
                            this.currentBet = buyIn;
                        }

                        // If the player is all-in or has folded, remove them from active players
                        if (player.allIn || player.Folded)
                        {
                            activePlayers.Remove(player);
                        }

                    }

                }


            }
            Console.WriteLine("Chips currently in the pot = " + this.potAmount + ".");
            Console.WriteLine("[*******************************]");
            Console.WriteLine("[**********Betting ends*********]");
            Console.WriteLine("[*******************************]\n");
            this.totalPotAmount += this.potAmount;
            this.currentBet = 0;
            foreach(Player player in this.players)
            {
                player.ResetCurrentBet();
            }

        }

       
        private bool AllPlayersHaveSameBet(List<Player> players, int currentBet)
        {
            // used to determine if betting should continue, checks to see if all players have either folded, gone all on, or if players bet equal to current bet
            return players.All(player => player.Folded || player.allIn || player.playerCurrentBet == this.currentBet);
        }

        private bool AllPlayersChecked(List<Player> players)
        {
            return players.All(player => player.playerCurrentBet == 0);
        }

        private void rewardPot()
        {
            if (this.roundWinners.Count() == 1)
            {
                Console.WriteLine("Rewarding " + this.roundWinners[0].playerName + " with pot amount $" + this.potAmount);
                roundWinners[0].addToChips(this.potAmount);
            }
            else
            {
                Console.WriteLine("Pot will be split " + this.roundWinners.Count() + " ways. Pot winners are...");
                foreach (Player p in roundWinners)
                {
                    Console.WriteLine(p.playerName);
                    p.addToChips(this.potAmount / roundWinners.Count);

                }

            }

        }



        public void addPlayer(Player player)
        {

            if (this.players.Count < 8)
            {
                this.players.Add(player);
            }
            else
            {
                Console.WriteLine("Table is full");
            }

        }

        public void printDeck()
        {
            foreach (Card c in this.deck)
            {
                Console.WriteLine(c.Value + " of " + c.Suit + " card id = " + c.ID);
            }
        }

        public int numCards()
        {
            return this.deck.Count;
        }

        public void printCommunityCards()
        {
            int numCards = 1;
            foreach (Card c in this.communityCards)
            {
                Console.WriteLine("Community Card " + numCards + " = " + c.Value + " of " + c.Suit + "'s.");
                numCards++;
            }

        }

        public void printBurnCards()
        {
            foreach (Card c in this.burnCards)
            {
                Console.WriteLine(c.Value + " of " + c.Suit + " card id = " + c.ID);
            }

        }

        public void printPlayers()
        {

            if (this.players.Count > 0)
            {
                int numplayers = 1;
                foreach (Player p in this.players)
                {
                    Console.WriteLine("Player " + numplayers + " : " + p.playerName);
                    numplayers++;
                }

            }
            else
            {
                Console.WriteLine("Table empty.");
            }

        }

        public void printWinners()
        {
            if (this.roundWinners.Count == 1)
            {
                Console.WriteLine(this.roundWinners[0].playerName + " wins the pot with a " + this.roundWinners[0].playersHand.handTitle);
            }
            else
            {
                foreach (Player p in roundWinners)
                {
                    Console.Write(p.playerName + ", ");
                }
                Console.WriteLine("splits the pot with a " + this.roundWinners[0].playersHand.handTitle);

            }
            foreach (Player p in roundWinners)
            {
                Console.WriteLine(p.playerName + " wins the round with a " + p.playersHand.handTitle);
            }
        }


        public void printPlayerCards(Player player)
        {
            Console.WriteLine(player.playerName + " cards are " + player.playerCards[0].Value + " of " +
                              player.playerCards[0].Suit + " and " + player.playerCards[1].Value + " of " +
                              player.playerCards[1].Suit);


        }

        public List<Player> DetermineWinner(List<Player> players, List<Card> communityCards)
        {
            List<Player> winners = new List<Player>();

            foreach (Player player in players)
            {
                Hand hand = new Hand();
                hand.EvaluateHand(player.playerCards, this.communityCards, player);
                player.playersHand = hand; // Set the player's hand for reference later

                if (winners.Count == 0 || hand.HasBetterHand(winners[0].playersHand) == 1)
                {
                    winners.Clear();
                    winners.Add(player);
                }
                else if (hand.HasBetterHand(winners[0].playersHand) == 3)
                {
                    // It's a tie, add the player to the list of winners
                    winners.Add(player);
                }
                // If hand.HasBetterHand(player.Hand) is 2, the player does not win this round
            }

            return winners;
        }



    }
}
