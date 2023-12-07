using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GamblersAnonymous;

public class Dealer
{
    public string dealerName { get; }

    public Dealer(string name)
    {
        dealerName = name;

    }

    public void Shuffle(List<Card> deck)
    {
        Random random = new Random(Guid.NewGuid().GetHashCode());
        int numCards = deck.Count;

        this.cutDeck((deck.Count / 2), deck);
        int numberOfShuffles = 5;

        while(numberOfShuffles > 0)
        {
            while (numCards > 1)
            {
                numCards--;
                int j = random.Next(numCards + 1);
                Card tempCard = deck[j];
                deck[j] = deck[numCards];
                deck[numCards] = tempCard;
            }

            numberOfShuffles--;
        }
    }

    public void cutDeck(int cut, List<Card> deck)
    {
        List<Card> topHalf = deck.GetRange(0, cut);
        List<Card> bottomHalf = deck.GetRange(cut, deck.Count - cut);

        deck.Clear();
        deck.AddRange(bottomHalf);
        deck.AddRange(topHalf);

    }

    public void dealPlayerCards(List<Card> deck, List<Player> players)
    {
        int numCards = 2;

        while (numCards > 0)
        {
            foreach (Player p in players)
            {
                p.playerCards.AddRange(deck.Take(1));
                deck.RemoveRange(0, 1);
            }
            numCards--;

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
