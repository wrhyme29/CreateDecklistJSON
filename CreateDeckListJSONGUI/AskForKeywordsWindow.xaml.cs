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
using static CreateDecklistJSONBackend.Utilities;


namespace CreateDeckListJSONGUI
{
    /// <summary>
    /// Interaction logic for AskForIdentifierWindow.xaml
    /// </summary>
    public partial class AskForKeywordsWindow : Window
    {
        public ObservableCollection<ObservedString> keywordsList;
        public AskForKeywordsWindow()
        {
            InitializeComponent();
            keywordsList = new ObservableCollection<ObservedString>();
            cmbKeyword.ItemsSource = keywordsList;
            List<string> keywordString = BuildKeywordList();
            foreach(string icon in keywordString)
            {
                keywordsList.Add(new ObservedString(icon));
            }
            cmbKeyword.SelectedIndex = 0;
        }

        public string ResponseText
        {
            get { return cmbKeyword.Text; }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private List<string> BuildKeywordList()
        {
            List<string> keywords = new List<string>()
            {
                "equipment",
                "one-shot",
                "ongoing",
                "limited"
            };

            keywords.Sort();
            return keywords;
        }

        public class ObservedString
        {
            public string Name { get; set; }
            public ObservedString(string name)
            {
                Name = name;
            }
        }
    }
}
