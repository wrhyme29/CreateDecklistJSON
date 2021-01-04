using System;

namespace CreateDecklistJSONBackend
{
    public class Card
    {
        public string identifier;
        public string title;
        public int count;
        public string[] body;
        public string[] icons;
        public string[] keywords;
        public string[] powers;
        public int hitPoints;
        public string flavorText;
        public string flavorReference;
        public FlavorQuote[] flavorQuotes;
        public bool character;
        public string[] incapicitatedAbilities;
        public string[] flippedIcons;

        public Card(string identifier, string title, int count, string[] icons, string[] keywords, string[] body = null, string[] powers = null, int hitPoints = 0,
            string flavorText = "", string flavorReference = "", FlavorQuote[] flavorQuotes = null, bool character = false, string[] incapicitatedAbilities = null, string[] flippedIcons = null)
        {
            this.identifier = identifier;
            this.title = title;
            this.count = count;
            this.icons = icons;
            this.keywords = keywords;
            this.body = body;
            this.powers = powers;
            this.hitPoints = hitPoints;
            this.flavorText = flavorText;
            this.flavorReference = flavorReference;
            this.flavorQuotes = flavorQuotes;
            this.character = character;
            this.incapicitatedAbilities = incapicitatedAbilities;
            this.flippedIcons = flippedIcons;
        }

        public bool ShouldSerializebody()
        {
            return body != null;
        }

        public bool ShouldSerializekeywords()
        {
            return keywords != null && keywords.Length > 0;
        }

        public bool ShouldSerializepowers()
        {
            return powers != null;
        }

        public bool ShouldSerializehitPoints()
        {
            return hitPoints != 0;
        }

        public bool ShouldSerializeflavorText()
        {
            return flavorText != "";
        }

        public bool ShouldSerializeflavorReference()
        {
            return flavorReference != "";
        }

        public bool ShouldSerializeflavorQuotes()
        {
            return flavorQuotes != null;
        }

        public bool ShouldSerializecharacter()
        {
            return character != false;
        }

        public bool ShouldSerializeincapicitatedAbilities()
        {
            return incapicitatedAbilities != null;
        }

        public bool ShouldSerializeflippedIcons()
        {
            return flippedIcons != null;
        }


        public class FlavorQuote
        {
            public string identifier;
            public string text;

            public FlavorQuote(string identifier, string text)
            {
                this.identifier = identifier;
                this.text = text;
            }
        }
    }
}
