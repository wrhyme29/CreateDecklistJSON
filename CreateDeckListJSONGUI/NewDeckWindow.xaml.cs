using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using CreateDecklistJSONBackend;
using Newtonsoft.Json;
using System.IO;

namespace CreateDeckListJSONGUI
{
    /// <summary>
    /// Interaction logic for NewDeckWindow.xaml
    /// </summary>
    public partial class NewDeckWindow : Window
    {
        ObservableCollection<ObservedString> deckIdentifiers;
        ObservableCollection<CardContainer> cardList;
        ObservableCollection<ObservedString> kindList;

        public CardContainer cardToReturn;
        public MainWindow MW;
        
        public NewDeckWindow(MainWindow mw, Deck deck = null)
        {
            InitializeComponent();
            MW = mw;
            deckIdentifiers = new ObservableCollection<ObservedString>();
            cardList = new ObservableCollection<CardContainer>();
            kindList = new ObservableCollection<ObservedString>();
            lbInitialCardIdentifierList.ItemsSource = deckIdentifiers;
            lbCardList.ItemsSource = cardList;
            cmbKind.ItemsSource = kindList;
            cardToReturn = null;
            foreach(string kind in BuildKindOptions())
            {
                kindList.Add(new ObservedString(kind));
            }

            cmbKind.SelectedIndex = 0;

            if(deck != null)
            {
                LoadDeck(deck);
            }
        }

        private void LoadDeck(Deck deck)
        {
            tb_DeckName.Text = deck.name;
            cmbKind.SelectedIndex = kindList.IndexOf(kindList.Where(os => os.Title == deck.kind).FirstOrDefault());
            foreach(string id in deck.initialCardIdentifiers)
            {
                deckIdentifiers.Add(new ObservedString(id));
            }

            foreach(Card card in deck.cards)
            {
                cardList.Add(new CardContainer(card.title, card));
            }
        }

        private void AddIdentifierButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AskForResponseWindow("Initial Card Identifier:");
            if (dialog.ShowDialog() == true)
            {
                deckIdentifiers.Add(new ObservedString(dialog.ResponseText));
                lbInitialCardIdentifierList.SelectedIndex = deckIdentifiers.Count - 1;
            }
        }

        private void RemoveIdentifierButton_Click(object sender, RoutedEventArgs e)
        {
            if(deckIdentifiers.Count > 0)
            {
                deckIdentifiers.RemoveAt(lbInitialCardIdentifierList.SelectedIndex);
            }
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            var cardView = new CardViewWindow(this);
            if(cardView.ShowDialog() == true)
            {
                if(cardToReturn != null)
                {
                    cardList.Add(cardToReturn);
                    lbCardList.SelectedIndex = cardList.Count - 1;
                }
            }
        }

        private void RemoveCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (cardList.Count > 0)
            {
                cardList.RemoveAt(lbCardList.SelectedIndex);
            }
        }

        private void EditCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (cardList.Count > 0)
            {
                CardContainer selectedCard = cardList.Where(cc => cardList.IndexOf(cc) == lbCardList.SelectedIndex).FirstOrDefault();
                var cardView = new CardViewWindow(this, selectedCard.CardObject);
                if (cardView.ShowDialog() == true)
                {
                    if (cardToReturn != null)
                    {
                        cardList.Insert(lbCardList.SelectedIndex, cardToReturn);
                        cardList.Remove(selectedCard);

                    }
                }
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Deck deck = BuildDeck();
            SerializeDeck(deck);
            DialogResult = true;
        }

        private Deck BuildDeck()
        {
            string name = tb_DeckName.Text;
            string kind = kindList.Where(os => kindList.IndexOf(os) == cmbKind.SelectedIndex).FirstOrDefault().Title;
            string[] initialCardIdentifiers = ConvertObservableCollectionToStringArray(deckIdentifiers);
            Card[] cards = ConvertObservableCollectionOfCardsToStringArray(cardList);

            return new Deck(name, kind, initialCardIdentifiers, cards);
        }

        private void SerializeDeck(Deck deck)
        {
            string JSONresult = JsonConvert.SerializeObject(deck);
            string path = $"{MW.FilePath.Text}\\{deck.name}.json";
            if (File.Exists(path))
            {
                File.Delete(path);

            }

            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(JSONresult.ToString());
                tw.Close();
            }
        }

        private string[] ConvertObservableCollectionToStringArray(ObservableCollection<ObservedString> observedStrings)
        {
            List<string> stringsList = new List<string>();
            foreach (ObservedString observedString in observedStrings)
            {
                stringsList.Add(observedString.Title);
            }
            return stringsList.ToArray();
        }

        private Card[] ConvertObservableCollectionOfCardsToStringArray(ObservableCollection<CardContainer> cardCollection)
        {
            List<Card> cardList = new List<Card>();
            foreach (CardContainer cc in cardCollection)
            {
                cardList.Add(cc.CardObject);
            }
            return cardList.ToArray();
        }

        private List<string> BuildKindOptions()
        {
            return new List<string> { "Environment", "Hero", "Villain", "VillainTeam", "Other" };

        }
    }

    public class ObservedString
    {
        public string Title { get; set; }
        
        public ObservedString(string title)
        {
            Title = title;
        }
    }

    public class CardContainer
    {
        public string Title { get; set; }
        public Card CardObject { get; set; }

        public CardContainer(string title, Card cardObject)
        {
            Title = title;
            CardObject = cardObject;
        }
    }
}
