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
using CreateDecklistJSONBackend;
using static CreateDecklistJSONBackend.Utilities;

namespace CreateDeckListJSONGUI
{
    /// <summary>
    /// Interaction logic for CardViewWindow.xaml
    /// </summary>
    public partial class CardViewWindow : Window
    {
        NewDeckWindow NewDeckWindowCallback;
        ObservableCollection<ObservedString> iconsList;
        ObservableCollection<ObservedString> keywordsList;
        ObservableCollection<ObservedString> bodyList;
        ObservableCollection<ObservedString> powersList;
        ObservableCollection<FlavorQuoteContainer> fqList;
        ObservableCollection<ObservedString> incapList;
        ObservableCollection<ObservedString> flippedIconsList;
        public Card.FlavorQuote flavorQuoteReturn;




        public CardViewWindow(NewDeckWindow newDeckWindow, Card cardToView = null)
        {
            InitializeComponent();
            NewDeckWindowCallback = newDeckWindow;
            iconsList = new ObservableCollection<ObservedString>();
            keywordsList = new ObservableCollection<ObservedString>();
            bodyList = new ObservableCollection<ObservedString>();
            powersList = new ObservableCollection<ObservedString>();
            fqList = new ObservableCollection<FlavorQuoteContainer>();
            incapList = new ObservableCollection<ObservedString>();
            flippedIconsList = new ObservableCollection<ObservedString>();

            lbIconList.ItemsSource = iconsList;
            lbKeywordsList.ItemsSource = keywordsList;
            lbBodyList.ItemsSource = bodyList;
            lbPowerList.ItemsSource = powersList;
            lbFlavorQuoteList.ItemsSource = fqList;
            lbIncapsList.ItemsSource = incapList;
            lbFlippedIconsList.ItemsSource = flippedIconsList;

            if(cardToView != null)
            {
                BuildCardToView(cardToView);
            }
        }

        private void BuildCardToView(Card cardToView)
        {
            tb_CardIdentifier.Text = cardToView.identifier;
            tb_CardTitle.Text = cardToView.title;
            tb_Count.Text = cardToView.count.ToString();
            foreach(string icon in cardToView.icons)
            {
                iconsList.Add(new ObservedString(icon));
            }

            foreach (string keyword in cardToView.keywords)
            {
                keywordsList.Add(new ObservedString(keyword));
            }
            foreach (string body in cardToView.body)
            {
                bodyList.Add(new ObservedString(body));
            }

            foreach (string power in cardToView.powers)
            {
                powersList.Add(new ObservedString(power));
            }
            tb_Hitpoints.Text = cardToView.hitPoints == 0 ? "" : cardToView.hitPoints.ToString();
            cbCharacter.IsChecked = cardToView.character;
            tb_CardFlavorText.Text = cardToView.flavorText;
            tb_CardFlavorReference.Text = cardToView.flavorReference;
            foreach (Card.FlavorQuote fq in cardToView.flavorQuotes)
            {
                fqList.Add(new FlavorQuoteContainer(fq.text, fq));
            }

            foreach (string incap in cardToView.incapicitatedAbilities)
            {
                incapList.Add(new ObservedString(incap));
            }

            foreach (string flippedIcon in cardToView.flippedIcons)
            {
                flippedIconsList.Add(new ObservedString(flippedIcon));
            }

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string identifier = tb_CardIdentifier.Text;
            string title = tb_CardTitle.Text;
            string[] icons = ConvertObservableCollectionToStringArray(iconsList);
            string[] keywords = ConvertObservableCollectionToStringArray(keywordsList);
            int count = Convert.ToInt32(tb_Count.Text);
            string[] body = ConvertObservableCollectionToStringArray(bodyList);
            string[] power = ConvertObservableCollectionToStringArray(powersList);
            int hitpoints = tb_Hitpoints.Text == "" ? 0 : Convert.ToInt32(tb_Hitpoints.Text);
            bool character = cbCharacter.IsChecked.Value;
            string flavorText = tb_CardFlavorText.Text;
            string flavorReference = tb_CardFlavorReference.Text;
            Card.FlavorQuote[] flavorQuotes = ConvertObservableCollectionOfFlavorQuotesToArray(fqList);
            string[] incaps = ConvertObservableCollectionToStringArray(incapList);
            string[] flippedIcons = ConvertObservableCollectionToStringArray(flippedIconsList);
            Card newCard = new Card(identifier, title, count, icons, keywords, body, power, hitpoints, flavorText, flavorReference, flavorQuotes, character, incaps, flippedIcons);
            CardContainer cardContainerToReturn = new CardContainer(newCard.title, newCard);
            NewDeckWindowCallback.cardToReturn = cardContainerToReturn;
            DialogResult = true;

        }

        private void AddIconButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AskForIconsWindow();
            if (dialog.ShowDialog() == true)
            {
                iconsList.Add(new ObservedString(dialog.ResponseText));
                lbIconList.SelectedIndex = iconsList.Count - 1;
            }
        }

        private void RemoveIconButton_Click(object sender, RoutedEventArgs e)
        {
            if (iconsList.Count > 0)
            {
                iconsList.RemoveAt(lbIconList.SelectedIndex);
            }
        }

        private void AddKeywordButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AskForKeywordsWindow();
            if (dialog.ShowDialog() == true)
            {
                keywordsList.Add(new ObservedString(dialog.ResponseText.ToLower()));
                lbKeywordsList.SelectedIndex = keywordsList.Count - 1;
            }
        }

        private void RemoveKeywordButton_Click(object sender, RoutedEventArgs e)
        {
            if (keywordsList.Count > 0)
            {
                keywordsList.RemoveAt(lbKeywordsList.SelectedIndex);
            }
        }

        private string[] ConvertObservableCollectionToStringArray(ObservableCollection<ObservedString> observedStrings)
        {
            List<string> stringsList = new List<string>();
            foreach(ObservedString observedString in observedStrings)
            {
                stringsList.Add(observedString.Title);
            }
            return stringsList.ToArray();
        }

        private Card.FlavorQuote[] ConvertObservableCollectionOfFlavorQuotesToArray(ObservableCollection<FlavorQuoteContainer> fqCollection)
        {
            List<Card.FlavorQuote> quoteList = new List<Card.FlavorQuote>();
            foreach (FlavorQuoteContainer cc in fqCollection)
            {
                quoteList.Add(cc.FlavorQuoteObject);
            }
            return quoteList.ToArray();
        }

        public class ObservedString
        {
            public string Title { get; set; }
            public ObservedString(string title)
            {
                Title = title;
            }
        }

        public class FlavorQuoteContainer
        {
            public string Title { get; set; }
            public Card.FlavorQuote FlavorQuoteObject { get; set; }

            public FlavorQuoteContainer(string title, Card.FlavorQuote flavorQuoteObject)
            {
                Title = title;
                FlavorQuoteObject = flavorQuoteObject;
            }
        }

        private void cbCharacter_Changed(object sender, RoutedEventArgs e)
        {

        }

        private void AddFQButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AskForFlavorQuoteWindow(this);
            if (dialog.ShowDialog() == true)
            {
                fqList.Add(new FlavorQuoteContainer(flavorQuoteReturn.text, flavorQuoteReturn));
                lbFlavorQuoteList.SelectedIndex = fqList.Count - 1;
            }
        }

        private void RemoveFQButton_Click(object sender, RoutedEventArgs e)
        {
            if (fqList.Count > 0)
            {
                fqList.RemoveAt(lbFlavorQuoteList.SelectedIndex);
            }
        }

        private void AddBodyButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AskForResponseWindow("Body:");
            if (dialog.ShowDialog() == true)
            {
                bodyList.Add(new ObservedString(dialog.ResponseText));
                lbBodyList.SelectedIndex = bodyList.Count - 1;
            }
        }

        private void RemoveBodyButton_Click(object sender, RoutedEventArgs e)
        {
            if (bodyList.Count > 0)
            {
                bodyList.RemoveAt(lbBodyList.SelectedIndex);
            }
        }

        private void AddPowerButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AskForResponseWindow("Power:");
            if (dialog.ShowDialog() == true)
            {
                powersList.Add(new ObservedString(dialog.ResponseText));
                lbPowerList.SelectedIndex = powersList.Count - 1;
            }
        }

        private void RemovePowerButton_Click(object sender, RoutedEventArgs e)
        {
            if (powersList.Count > 0)
            {
                powersList.RemoveAt(lbPowerList.SelectedIndex);
            }
        }

        private void AddIncapButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AskForResponseWindow("Incap:");
            if (dialog.ShowDialog() == true)
            {
                incapList.Add(new ObservedString(dialog.ResponseText));
                lbIncapsList.SelectedIndex = incapList.Count - 1;
            }
        }

        private void RemoveIncapButton_Click(object sender, RoutedEventArgs e)
        {
            if (incapList.Count > 0)
            {
                incapList.RemoveAt(lbIncapsList.SelectedIndex);
            }
        }

        private void AddFlippedIconsButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AskForIconsWindow();
            if (dialog.ShowDialog() == true)
            {
                flippedIconsList.Add(new ObservedString(dialog.ResponseText));
                lbFlippedIconsList.SelectedIndex = flippedIconsList.Count - 1;
            }
        }

        private void RemoveFlippedIconsButton_Click(object sender, RoutedEventArgs e)
        {
            if (flippedIconsList.Count > 0)
            {
                flippedIconsList.RemoveAt(lbFlippedIconsList.SelectedIndex);
            }
        }
    }
}
