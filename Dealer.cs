using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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

        public Dealer(string name)
        {
            dealerName = name;

        }

        public void Shuffle(List<Card> deck)
        {
            Random random = new Random();
            int numCards = deck.Count;

            for (int i = numCards - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                Card tempCard = deck[i];
                deck[i] = deck[j];
                deck[j] = tempCard;
            }

        }


        public void dealPlayerCards(List<Card> deck, List<Player> players)
        {
            int numCards = 2;

            foreach (Player p in players)
            {
                p.playerCards.AddRange(deck.Take(numCards));
                deck.RemoveRange(0, numCards);
            }
        }

        public void dealFLop(List<Card> deck, List<Card> communityCards)
        {
            int numCards = 3;

            communityCards.AddRange(deck.Take(numCards));
            deck.RemoveRange(0, numCards);

        }

        public void dealTurnRiver(List<Card> deck, List<Card> communityCards)
        {
            int numCards = 1;

            communityCards.AddRange(deck.Take(numCards));
            deck.RemoveRange(0, numCards);

        }

        public void burnCard(List<Card> deck, List<Card> burnCards)
        {
            int numCards = 1;

            burnCards.AddRange(deck.Take(numCards));
            deck.RemoveRange(0, numCards);

        }


        public void resetTable(List<Player> players, List<Card> deck, List<Card> communityCards, List<Card> burnCards)
        {
            int holeCards = 2;
            int tableCards = 5;
            int throwAways = 3;

            foreach(Player p in players)
            {
                deck.AddRange(p.playerCards.Take(holeCards));
                p.playerCards.RemoveRange(0, holeCards);
            }

            deck.AddRange(communityCards.Take(tableCards));
            communityCards.RemoveRange(0, tableCards);

            deck.AddRange(burnCards.Take(throwAways));
            burnCards.RemoveRange(0, throwAways);

        }


    }
}
