using System;
using System.Collections.Generic;
using System.Text;

namespace CreateDecklistJSONBackend
{
    public class Deck
    {
        public string name;
        public string kind;
        public string[] initialCardIdentifiers;
        public Card[] cards;
        public object[] promoCards;

        public Deck(string name, string kind, string[] initialCardIdentifiers, Card[] cards, object[] promoCards = null)
        {
            this.name = name;
            this.kind = kind;
            this.initialCardIdentifiers = initialCardIdentifiers;
            this.cards = cards;
            this.promoCards = promoCards;
        }

        public bool ShouldSerializepromoCards()
        {
            return promoCards != null;
        }
    }
}
