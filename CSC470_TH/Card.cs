using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC470_TH
{
    public class Card
    {
        public int ID { get; }
        public string Suit { get; }
        public int Value { get; }

        // simple data structure for a card
        public Card(int id, string suit, int value)
        {
            ID = id;
            Suit = suit;
            Value = value;
        }

    }
}
