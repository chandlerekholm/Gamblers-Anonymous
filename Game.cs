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



        public Game()
        {
            this.players = new List<Player>();
            /* InitializeDeck(); */
            this.communityCards = new List<Card>();
            this.burnCards = new List<Card>();
            this.dealer = new Dealer("Vegas Dealer");
        }

        public void startGame()
        {
            InitializePlayers();
            InitializeDeck();

            Console.WriteLine("Number of players = " + this.numPlayers());

            this.printPlayers();

            Console.WriteLine("Dealer shuffled deck...");
            dealer.Shuffle(this.deck);



            Console.WriteLine("Dealer is dealing out player cards...");
            dealer.dealPlayerCards(this.deck, this.players);
            
            foreach(Player p in this.players)
            {
                this.printPlayerCards(p);
            }

            dealer.burnCard(deck, burnCards);
            Console.WriteLine("Dealer is dealing out the flop...");
            dealer.dealFLop(deck, communityCards);
            this.printCommunityCards();

            dealer.burnCard(deck, burnCards);
            Console.WriteLine("Dealer is dealing out the turn...");
            dealer.dealTurnRiver(deck, communityCards);
            this.printCommunityCards();

            dealer.burnCard(deck, burnCards);
            Console.WriteLine("Dealer is dealing out the river...");
            dealer.dealTurnRiver(deck, communityCards);
            this.printCommunityCards();

            Console.WriteLine("Dealer burned 3 cards...");
            this.printBurnCards();

            Console.WriteLine("Dealer has " + this.numCards() + " cards remaining.");

            Console.WriteLine("Dealer resets table...");
            dealer.resetTable(players, deck, communityCards, burnCards);

            Console.WriteLine("Dealer has " + this.numCards() + " after reseting table.");

            dealer.Shuffle(deck);
            this.printDeck();


            Console.WriteLine("Printing out community cards after reset...");
            this.printCommunityCards();


            Console.WriteLine("Printing out burn cards after reset...");
            this.printBurnCards();




            Console.WriteLine("Dealer shuffled deck...");
            dealer.Shuffle(this.deck);

            Console.WriteLine("***************NEXT ROUND BEGINS******************");

            Console.WriteLine("Dealer is dealing out player cards...");
            dealer.dealPlayerCards(this.deck, this.players);

            foreach (Player p in this.players)
            {
                this.printPlayerCards(p);
            } 

            dealer.burnCard(deck, burnCards);
            Console.WriteLine("Dealer is dealing out the flop...");
            dealer.dealFLop(deck, communityCards);
            this.printCommunityCards();

            dealer.burnCard(deck, burnCards);
            Console.WriteLine("Dealer is dealing out the turn...");
            dealer.dealTurnRiver(deck, communityCards);
            this.printCommunityCards();

            dealer.burnCard(deck, burnCards);
            Console.WriteLine("Dealer is dealing out the river...");
            dealer.dealTurnRiver(deck, communityCards);
            this.printCommunityCards();

            Console.WriteLine("Dealer burned 3 cards...");
            this.printBurnCards();

            Console.WriteLine("Dealer has " + this.numCards() + " cards remaining.");

            Console.WriteLine("Dealer resets table...");
            dealer.resetTable(players, deck, communityCards, burnCards);

            Console.WriteLine("Dealer has " + this.numCards() + " after reseting table.");

            dealer.Shuffle(deck);
            this.printDeck();


            Console.WriteLine("Printing out community cards after reset...");
            this.printCommunityCards();


            Console.WriteLine("Printing out burn cards after reset...");
            this.printBurnCards();

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


        private void InitializeDeck()
        {
            string[] Suits = { "Clubs", "Spades", "Hearts", "Diamonds" };
            string[] Values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            this.deck = new List<Card>();
            int id = 0;

            foreach (string s in Suits)
            {
                foreach (string v in Values)
                {
                    id++;
                    Card card = new Card(id, s, v);
                    this.deck.Add(card);
                }

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



        public void InitializePlayers()
        {
            this.players.Add(new Player("John"));
            this.players.Add(new Player("Sam"));
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

        public void printPlayerCards(Player player)
        {
            Console.WriteLine(player.playerName + " cards are " + player.playerCards[0].Value + " of " +
                              player.playerCards[0].Suit + " and " + player.playerCards[1].Value + " of " + 
                              player.playerCards[1].Suit);


        }

        public int numPlayers()
        {
            return this.players.Count;
        }



    }
}
