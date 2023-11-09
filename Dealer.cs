using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH
{
    public class Dealer
    {
        public string dealerName { get; }
        public List<Card> Deck { get; private set; } = default!;

        public Dealer(string name)
        {
            dealerName = name;
            InitializeDeck();
        }

        public void Shuffle()
        {
            Console.WriteLine(this.dealerName + " shuffles the deck");
            Random random = new Random();
            this.Deck = this.Deck.OrderBy(card => random.Next()).ToList();

           /* List<Card> duplicates = new List<Card>();
            HashSet<int> foundCards = new HashSet<int>();

            foreach (Card c in this.Deck)
            {
                if (!foundCards.Add(c.ID))
                {
                    this.Shuffle();
                }
            } */
        }

        public List<Card> dealHoleCards()
        {
            List<Card> holeCards = new List<Card>();
            Card holeCard1 = new Card(1, "Heart", "Jack");
            Card holeCard2 = new Card(2, "Diamond", "Seven");
            holeCards.Add(holeCard1);
            holeCards.Add(holeCard2);
            return holeCards;
            //TODO: implement an algorithm to deal a card
        }

        public List<Card> dealFlop()
        {
            List<Card> flopCards = new List<Card>();
            Card flopCard1 = new Card(3, "Hearts", "Two");
            Card flopCard2 = new Card(4, "Diamonds", "Eight");
            Card flopCard3 = new Card(5, "Clubs", "Five");
            flopCards.Add(flopCard1);
            flopCards.Add(flopCard2);
            flopCards.Add(flopCard3);
            return flopCards;
            //TODO: implement an algorithm to deal the flop
        }


        public Card dealTurn()
        {
            Card turnCard = new Card(6, "Spades", "Ace");
            return turnCard;
            //TODO: implement an algorithim to deal the turn
        }

        public Card dealRiver()
        {
            Card riverCard = new Card(7, "Spades", "King");
            return riverCard;
            //TODO: implement an algorithim to deal the turn
        }


        public void resetTable(List<Player> activePlayers)
        {
            Console.WriteLine("Reset all active player's hole cards");
            //TODO: implement a method to remove all the player's hole cards

        }

        private void InitializeDeck()
        {
            string[] Suits = { "Clubs", "Spades", "Hearts", "Diamonds" };
            string[] Values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            Deck = new List<Card>();
            int id = 0;
            
            foreach(string s in Suits)
            {
                foreach(string v in Values)
                {
                    id++;
                    Card card = new Card(id, s, v);
                    Deck.Add(card);
                }
            }
        }






    }
}
