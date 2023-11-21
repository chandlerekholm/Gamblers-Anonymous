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
        public string Value { get; }

        public Card(int id, string suit, string value)
        {
            ID = id;
            Suit = suit;
            Value = value;
        }

    }
}
