using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH
{
     public class Hand
    {
        public int handRank {  get; set; }
        public string handTitle { get; set; }
        public List<Card> bestHand { get; set; } = default!;


        public Hand()
        {
            this.handRank = 0;
            this.handTitle = "";
            this.bestHand = new List<Card>();
        }

        // return an integer value that represents the best hand
        // hand ranks are as follows...
        // HighCard = 1
        // OnePair = 2
        // TwoPair = 3
        // ThreeOfAKind = 4
        // Straight = 5
        // Flush = 6
        // FullHouse = 7
        // FourOfAKind = 8
        // StraightFlush = 9
        // RoyalFlush = 10


        public void EvaluateHand(List<Card> holeCards, List<Card> communityCards, Player p)
        {
            List<Card> allCards = holeCards.Concat(communityCards).ToList();
            allCards.Sort((x, y) => x.Value.CompareTo(y.Value));

            Console.WriteLine(p.playerName + "'s cards are...");
            foreach (Card card in allCards)
            {
                Console.WriteLine(card.Value + " of " + card.Suit);
            }

            if (IsRoyalFlush(allCards))
            {
                this.handRank = 10;
                this.handTitle = "Royal Flush";
                return;
            }
            else if (IsStraightFlush(allCards))
            {
                this.handRank = 9;
                this.handTitle = "Straight Flush";
                return;
            }
            else if (IsFourOfAKind(allCards))
            {
                this.handRank = 8;
                this.handTitle = "4 of a Kind";
                return;
            }
            else if (IsFullHouse(allCards))
            {
                this.handRank = 7;
                this.handTitle = "Full House";
                return;
            }
            else if (IsFlush(allCards))
            {
                this.handRank = 6;
                this.handTitle = "Flush";
                return;
            }
            else if (IsStraight(allCards))
            {
                this.handRank = 5;
                this.handTitle = "Straight";
                return;
            }
            else if (IsThreeOfAKind(allCards))
            {
                this.handRank = 4;
                this.handTitle = "3 of a Kind";
                return;
            }
            else if (IsTwoPair(allCards))
            {
                this.handRank = 3;
                this.handTitle = "2 Pair";
                return;
            }
            else if (IsOnePair(allCards))
            {
                this.handRank = 2;
                this.handTitle = "Pair";
               
            }
            else
            {
                this.handRank = 1;
                this.handTitle = "High Card";
                Card highCard = getHighCard(allCards);
                this.bestHand.Add(highCard);
                return;
            }
        }

        public Card getHighCard(List<Card> cards)
        {
            this.bestHand.Clear();
            var orderedCards = cards.OrderByDescending(card => card.Value);
            return orderedCards.First();
        }
        public bool IsOnePair(List<Card> cards)
        {
            this.bestHand.Clear();
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].Value == cards[i + 1].Value)
                {
                    this.bestHand.Add(cards[i]);
                    this.bestHand.Add(cards[i + 1]);
                    return true;
                }
            }
            return false;
        }

        public bool IsTwoPair(List<Card> cards)
        {
            this.bestHand.Clear();
            int pairCount = 0;
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].Value == cards[i + 1].Value)
                {
                    pairCount++;
                    this.bestHand.Add(cards[i]);
                    this.bestHand.Add(cards[i + 1]);
                    // Skip the next card of the pair
                    i++;
                }
            }

            return pairCount == 2;
        }

        public bool IsThreeOfAKind(List<Card> cards)
        {
            this.bestHand.Clear();
            var orderedCards = cards.OrderByDescending(card => card.Value);
            var groupByValue = orderedCards.GroupBy(card => card.Value);

            foreach (var valueGroup in groupByValue)
            {
                if (valueGroup.Count() == 3)
                {
                    // found a group of 3
                    this.bestHand.AddRange(valueGroup);
                    return true;
                }

            }
            
            return false;
        }

        public bool IsStraight(List<Card> cards)
        {
            this.bestHand.Clear();

            // sort cards by value
            var sortedCards = cards.OrderBy(card => card.Value).ToList();

            //check for a straight treating the ace as a value of 14
            if(CheckStraight(sortedCards))
            {
                return true;
            }

            // Check for a straight with Ace as 1
            var aceAsOneCards = cards.Select(card => card.Value == 14 ? new Card(1, card.Suit, 1) : card).ToList();
            var sortedAceAsOneCards = aceAsOneCards.OrderBy(card => (int)card.Value).ToList();

            if (CheckStraight(sortedAceAsOneCards))
            {
                return true;
            }

            return false;

        }

        public bool CheckStraight(List<Card> cards)
        {

            var allStraights = new List<List<Card>>();

            // Extract unique ranks while ignoring duplicates
            var uniqueValues = cards.Select(card => card.Value).Distinct().OrderBy(value => (int)value).ToList();

            for (int i = 0; i <= uniqueValues.Count - 5; i++)
            {
                bool isStraight = true;

                //Check for consecutive values
                for (int j = 0; j < 4; j++)
                {
                    if ((int)uniqueValues[i + j + 1] != (int)uniqueValues[i + j] + 1)
                    {
                        isStraight = false;
                        break;
                    }

                }

                if (isStraight)
                {
                    // Find the cards corresponding to the unique ranks in the straight
                    var straightCards = cards.Where(card => uniqueValues.Skip(i).Take(5).Contains(card.Value)).ToList();
                    allStraights.Add(straightCards);
                }

            }

            if (allStraights.Any())
            {
                // Find and set the highest straight
                var highestStraight = allStraights.OrderByDescending(straight => straight.Max(card => card.Value)).First();
                this.bestHand.AddRange(highestStraight);
                return true;
            }

            return false;
        }

        //method for removing duplicates from the card list, not currently used keeping in case needed later
        public void RemoveDuplicates(List<Card> cards)
        {
            var uniqueValues = new HashSet<int>();
            var duplicates = new List<Card>();

            foreach (var card in cards)
            {
                if (!uniqueValues.Add(card.Value))
                {
                    // If the rank is already in the HashSet, add it to duplicates
                    duplicates.Add(card);
                }
            }

            foreach (var duplicate in duplicates)
            {
                // Find the index of the first occurrence of each duplicate rank
                var index = cards.FindIndex(card => card.Value == duplicate.Value);

                if (index != -1)
                {
                    // Remove all but the first occurrence of each duplicate rank
                    cards.RemoveAll(card => card.Value == duplicate.Value && card != cards[index]);
                }
            }
        }

        public bool IsFlush(List<Card> cards)
        {
            this.bestHand.Clear();
            // Use Linq to group cards by suit
            foreach (var suit in cards.Select(card => card.Suit).Distinct())
            {
                var suitCards = cards.Where(card => card.Suit == suit).ToList();

                if(suitCards.Count() >= 5)
                {
                    this.bestHand.AddRange(suitCards.Take(5));
                    return true;
                }

            }

            return false;
        }

        public bool IsFullHouse(List<Card> cards)
        {
            this.bestHand.Clear();
            // put the deck is descending order because a full house tie will be decided by which player has highest 3 of a kind.
            var orderedCards = cards.OrderByDescending(card => card.Value);
            var groupByValue = orderedCards.GroupBy(card => card.Value);

            foreach (var valueGroup in groupByValue)
            {

                if (valueGroup.Count() == 3)
                {
                    // found a group of 3
                    this.bestHand.AddRange(valueGroup);


                    var secondValueGroup = groupByValue.FirstOrDefault(g => g.Key != valueGroup.Key && g.Count() >= 2);

                    if (secondValueGroup != null)
                    {
                        this.bestHand.AddRange(secondValueGroup.Take(2));
                        return true;
                    }

                }

            }
            return false;
        }

        public bool IsFourOfAKind(List<Card> cards)
        {
            this.bestHand.Clear();
            var orderedCards = cards.OrderByDescending(card => card.Value);
            var groupByValue = orderedCards.GroupBy(card => card.Value);

            foreach (var valueGroup in groupByValue)
            {
                if (valueGroup.Count() == 4)
                {
                    // found a group of 4
                    this.bestHand.AddRange(valueGroup);
                    return true;
                }

            }

            return false;
        }

        public bool IsStraightFlush(List<Card> cards)
        {
            this.bestHand.Clear();

            // Sort cards by value
            var sortedCards = cards.OrderBy(card => card.Value).ToList();

            // Check for a straight treating the ace as a value of 14
            if (CheckStraight(sortedCards))
            {
                var highestStraight = new List<Card>();
                highestStraight = this.bestHand.ToList();
                // Check if the straight is also a flush
                if (IsFlush(highestStraight))
                {
                    return true;
                }
            }

            // Check for a straight with Ace as 1
            var aceAsOneCards = cards.Select(card => card.Value == 14 ? new Card(1, card.Suit, 1) : card).ToList();
            var sortedAceAsOneCards = aceAsOneCards.OrderBy(card => (int)card.Value).ToList();
            int l = 0;
            
            // Check for a straight treating the ace as 1
            if (CheckStraight(sortedAceAsOneCards))
            {
                // Adjust the Ace value to 1 in the bestHand list
                //this.bestHand = this.bestHand.Select(card => card.Value == 14 ? new Card(1, card.Suit, 1) : card).ToList();

                // Check if the straight is also a flush
                if (IsFlush(sortedAceAsOneCards))
                {
                   return true;
                }
            }

            return false;
        }

        public bool IsRoyalFlush(List<Card> cards)
        {
            this.bestHand.Clear();
            // Check for both straight and flush
            if (IsStraightFlush(cards))
            {
                if (this.bestHand[0].Value == 10 && this.bestHand[4].Value == 14)
                {
                    return true;
                }
            }

            return false;
        }

        public int HasBetterHand(Hand opponentHand)
        {
            if (opponentHand == null)
            {
                throw new ArgumentNullException(nameof(opponentHand));
            }

            if(this.handRank > opponentHand.handRank)
            {
                return 1;
            }
            else if(this.handRank < opponentHand.handRank)
            {
                return 2;
            }
            else
            {
                // hand ranks are equal so compare cards to determine winner
                return HasBetterHandByCardValue(opponentHand);

            }

        }

        public int HasBetterHandByCardValue(Hand opponentHand)
        {
            // high card, pair, three of a kind, full house, and four of a kind can all be evaluated based on the value of the first card in hand
            if (this.handRank == 1 || this.handRank == 2 || this.handRank == 4 || this.handRank == 7 || this.handRank == 8)
            {
                if (this.bestHand[0].Value > opponentHand.bestHand[0].Value)
                {
                    return 1;
                }
                else if (this.bestHand[0].Value < opponentHand.bestHand[0].Value)
                {
                    return 2;
                }
                else
                {
                    return 3; // split the pot
                }
            }
            else if(this.handRank == 3) // two pair evaluated based on the value of the last card in the hand (highest pair)
            {
                if (this.bestHand[3].Value > opponentHand.bestHand[3].Value)
                {
                    return 1;
                }
                else if (this.bestHand[3].Value < opponentHand.bestHand[3].Value)
                {
                    return 2;
                }
                else
                {
                    return 3; // split the pot
                }

            }
            else if (this.handRank == 5 || this.handRank == 6 || this.handRank == 9) // straight, flush, and straight flush evaluated based on the value of the last card in the hand (highest card)
            {
                if (this.bestHand[4].Value > opponentHand.bestHand[4].Value)
                {
                    return 1;
                }
                else if (this.bestHand[4].Value < opponentHand.bestHand[4].Value)
                {
                    return 2;
                }
                else
                {
                    return 3; // split the pot
                }

            }
            else // we are evaluating a royal flush, which is decided based on suit in the following order: Spades, Hearts, Diamonds, Clubs
            {
                int card1SuitValue = ValueBySuit(this.bestHand[4].Suit);
                int card2SuitValue = ValueBySuit(opponentHand.bestHand[4].Suit);

                if(card1SuitValue > card2SuitValue)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }

        public int ValueBySuit(String suit)
        {
            switch (suit)
            {
                case "Spades":
                    return 1;
                case "Hearts":
                    return 2;
                case "Diamonds":
                    return 3;
                case "Clubs":
                    return 4;
                default:
                    // Handle the case where the suit is not recognized
                    throw new ArgumentException("Invalid card suit");
            }
        }

    }
}
