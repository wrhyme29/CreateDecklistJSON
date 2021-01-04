using System;
using System.Collections.Generic;
using System.IO;
using CreateDecklistJSONBackend;
using Newtonsoft.Json;

namespace CreateDecklistJSONConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Deck Name: ");
            string deckName = Console.ReadLine();

            Deck deck = GetDeckFromConsole();
            string JSONresult = JsonConvert.SerializeObject(deck);
            string path = $"D:\\json\\{deckName}.json";
            if(File.Exists(path))
            {
                File.Delete(path);
                
            }

            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(JSONresult.ToString());
                tw.Close();
            }

        }

        private static Deck GetDeckFromConsole()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Kind: ");
            string kind = Console.ReadLine();
            Console.WriteLine("Initial Identifiers: ");
            bool ask = true;
            string initial = "";
            int initialCount = 1;
            List<string> initialList = new List<string>();
            while (ask)
            {
                Console.Write($"Initial Identifer {initialCount}: ");
                initial = Console.ReadLine();
                if (initial == "")
                {
                    ask = false;
                }
                else
                {
                    initialList.Add(initial);
                    initialCount++;
                }
            }
            string[] initialCardIdentifiers = initialList.ToArray();

            List<Card> cardList = new List<Card>();
            Console.WriteLine("Cards: ");
            ask = true;
            while(ask)
            {
                Card card = GetCardFromConsole();
                cardList.Add(card);
                Console.Write("More Cards: Y/N: ");
                string more = Console.ReadLine();
                if(more == "N" || more == "n")
                {
                    ask = false;
                }
            }

            Card[] cards = cardList.ToArray();

            return new Deck(name, kind, initialCardIdentifiers, cards);
        }

        private static Card GetCardFromConsole()
        {
            Console.Write("Identifier: ");
            string identifier = Console.ReadLine();
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Count: ");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Icons:");
            bool ask = true;
            string icon = "";
            int iconCount = 1;
            List<string> iconList = new List<string>();
            while(ask)
            {
                Console.Write($"Icon{iconCount}: ");
                icon = Console.ReadLine();
                if(icon == "")
                {
                    ask = false;
                } else
                {
                    iconList.Add(icon);
                    iconCount++;
                }
            }
            string[] icons = iconList.ToArray();

            Console.WriteLine("Keywords: ");
            ask = true;
            string keyword = "";
            int keywordCount = 1;
            List<string> keywordList = new List<string>();
            while (ask)
            {
                Console.Write($"Keyword{keywordCount}: ");
                keyword = Console.ReadLine();
                if (keyword == "")
                {
                    ask = false;
                }
                else
                {
                    keywordList.Add(keyword);
                    keywordCount++;
                }
            }
            string[] keywords = keywordList.ToArray();

            Console.WriteLine("Body: ");
            ask = true;
            string bodyValue = "";
            int bodyCount = 1;
            List<string> bodyList = new List<string>();
            while (ask)
            {
                Console.Write($"Body{bodyCount}: ");
                bodyValue = Console.ReadLine();
                if (bodyValue == "")
                {
                    ask = false;
                }
                else
                {
                    bodyList.Add(bodyValue);
                    bodyCount++;
                }
            }
            string[] body;
            if (bodyList.Count == 0)
            {
                body = null;
            }
            else
            {
                body = bodyList.ToArray();
            }

            Console.WriteLine("Powers: ");
            ask = true;
            string powerValue = "";
            int powerCount = 1;
            List<string> powerList = new List<string>();
            while (ask)
            {
                Console.Write($"Power{powerCount}: ");
                powerValue = Console.ReadLine();
                if (powerValue == "")
                {
                    ask = false;
                }
                else
                {
                    powerList.Add(powerValue);
                    powerCount++;
                }
            }
            string[] powers;
            if (powerList.Count == 0)
            {
                powers = null;
            }
            else
            {
                powers = powerList.ToArray();
            }

            Console.WriteLine("Hitpoints: ");
            string hitPointsValue = Console.ReadLine();
            int hitPoints;
            if(hitPointsValue == "")
            {
                hitPoints = 0;
            } else
            {
                hitPoints = Convert.ToInt32(hitPointsValue);
            }

            Console.WriteLine("Character: ");
            string characterValue = Console.ReadLine();
            bool character;
            if(characterValue == "true")
            {
                character = true;
            } else
            {
                character = false;
            }
            string[] incapAbilities = null;
            string[] flippedIcons = null;
            string flavorText = "";
            string flavorReference = "";
            Card.FlavorQuote[] flavorQuotes = null;
            if(character)
            {
                Console.WriteLine("Incapicitated Abilities: ");
                ask = true;
                string incapValue = "";
                int incapCount = 1;
                List<string> incapList = new List<string>();
                while (ask)
                {
                    Console.Write($"Incap{incapCount}: ");
                    incapValue = Console.ReadLine();
                    if (incapValue == "")
                    {
                        ask = false;
                    }
                    else
                    {
                        incapList.Add(incapValue);
                        incapCount++;
                    }
                }
                if (incapList.Count == 0)
                {
                    incapAbilities = null;
                }
                else
                {
                    incapAbilities = incapList.ToArray();
                }

                Console.WriteLine("Flipped Icons:");
                ask = true;
                string flippedIcon = "";
                int flippedIconCount = 1;
                List<string> flippedIconList = new List<string>();
                while (ask)
                {
                    Console.Write($"FlippedIcon{flippedIconCount}: ");
                    flippedIcon = Console.ReadLine();
                    if (flippedIcon == "")
                    {
                        ask = false;
                    }
                    else
                    {
                        flippedIconList.Add(flippedIcon);
                        flippedIconCount++;
                    }
                }
                if (flippedIconList.Count == 0)
                {
                    flippedIcons = null;
                }
                else
                {
                    flippedIcons = flippedIconList.ToArray();
                }

            }
            else
            {
                Console.Write("FlavorText: ");
                flavorText = Console.ReadLine();
                Console.Write("FlavorReference: ");
                flavorReference = Console.ReadLine();

                Console.WriteLine("FlavorQuotes: ");
                ask = true;
                string flavorQId = "";
                string flavorQText = "";
                int quoteCount = 1;
                List<Card.FlavorQuote> quoteList = new List<Card.FlavorQuote>();
                while (ask)
                {
                    Console.Write($"Quote{quoteCount} Identifier: ");
                    flavorQId = Console.ReadLine();
                    Console.Write($"Quote{quoteCount} Text: ");
                    flavorQText = Console.ReadLine();
                    if (flavorQId == "" || flavorQText == "")
                    {
                        ask = false;
                    }
                    else
                    {
                        quoteList.Add(new Card.FlavorQuote(flavorQId, flavorQText));
                        quoteCount++;
                    }
                }
                if (quoteList.Count == 0)
                {
                    flavorQuotes = null;
                }
                else
                {
                    flavorQuotes = quoteList.ToArray();
                }

            }

            return new Card(identifier, title, count, icons, keywords, body, powers, hitPoints, flavorText, flavorReference, flavorQuotes, character, incapAbilities, flippedIcons);
        }
    }
}
