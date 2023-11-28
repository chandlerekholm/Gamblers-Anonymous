using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH
{
    public class Game
    {

        public List<Player> players { get; set; } = default!;
        public List<Card> deck { get; set; } = default!;
        public List<Card> communityCards { get; set; } = default!;
        public List<Card> burnCards { get; set; } = default!;
        public Dealer dealer { get; set; }
        public List<Player> roundWinners { get; set; } = default!;
        public int potAmount { get; set; }



        public Game()
        {
            this.players = InitializePlayers();
            this.deck = InitializeDeck();
            this.communityCards = new List<Card>();
            this.burnCards = new List<Card>();
            this.dealer = new Dealer("Vegas Dealer");
            this.roundWinners = new List<Player>();
            this.potAmount = inializePot();
        }

        private List<Player> InitializePlayers()
        {
            List<Player> initialPlayers = new List<Player>();

            initialPlayers.Add(new Player("John"));
            initialPlayers.Add(new Player("Steve"));
            initialPlayers.Add(new Player("Matt"));
            initialPlayers.Add(new Player("Tom"));
            initialPlayers.Add(new Player("Jason"));

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

        private int inializePot()
        {
            //determin the pot amount
            return 100;
        }

        public void startGame()
        {
            int roundCount = 0;
            while(roundCount < 5)
            {
                StartRound();
                EvaluateRound();
                ResetRound();
                roundCount++;
            }
   


        }

        private void StartRound()
        {
            Console.WriteLine("Dealer shuffled deck...");
            dealer.Shuffle(this.deck);

            Console.WriteLine("Dealer is dealing out player cards...");
            dealer.dealPlayerCards(this.deck, this.players);

            // betting round before the flop
            BettingRound();

            // dealer burns a card and then deals the flop
            Console.WriteLine("Dealer deals out the flop...");
            dealer.burnCard(deck, burnCards);
            dealer.dealFLop(deck, communityCards);

            // print out cards on the table
            Console.WriteLine("Community Cards after flop...");
            this.printCommunityCards();

            // betting round after the flop
            BettingRound();

            // dealer burns a card and then deals out the turn
            Console.WriteLine("Dealer is dealing out the turn...");
            dealer.burnCard(deck, burnCards);
            dealer.dealTurnRiver(deck, communityCards);

            // print out cards on the table
            Console.WriteLine("Community Cards after turn...");
            this.printCommunityCards();

            // betting round after the turn
            BettingRound();

            // dealer burns a card and then deals out the river
            Console.WriteLine("Dealer is dealing out the river...");
            dealer.burnCard(deck, burnCards);
            dealer.dealTurnRiver(deck, communityCards);

            // print out cards on the table
            Console.WriteLine("Community Cards after river...");
            this.printCommunityCards();

            // final round of betting
            BettingRound();



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

        }

        private void BettingRound()
        {
            Console.WriteLine("Players are betting");
        }

        private void rewardPot()
        {
            if(this.roundWinners.Count() == 1)
            {
                Console.WriteLine("Rewarding " + this.roundWinners[0].playerName + " with pot amount $" + this.potAmount);
            }
            else
            {
                Console.WriteLine("potAmount will be split " + this.roundWinners.Count() + " ways. Pot winners are...");
                foreach (Player p in roundWinners)
                {
                    Console.WriteLine(p.playerName);
                }

            }

        }



        public void addPlayer(Player player)
        {
            
            if(this.players.Count < 8)
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
            foreach (Card c in this.communityCards)
            {
                Console.WriteLine(c.Value + " of " + c.Suit + " card id = " + c.ID);
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
            
            if(this.players.Count > 0)
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
            if(this.roundWinners.Count == 1)
            {
                Console.WriteLine(this.roundWinners[0].playerName + " wins the pot with a " + this.roundWinners[0].playersHand.handTitle);
            }
            else
            {
                foreach (Player p in roundWinners)
                {
                    Console.Write(p.playerName + ", ");
                }
                Console.WriteLine("split the pot with a " + this.roundWinners[0].playersHand.handTitle);

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
