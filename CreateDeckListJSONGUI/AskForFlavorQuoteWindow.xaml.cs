using System;
using System.Collections.Generic;
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

namespace CreateDeckListJSONGUI
{
    /// <summary>
    /// Interaction logic for AskForIdentifierWindow.xaml
    /// </summary>
    public partial class AskForFlavorQuoteWindow : Window
    {
        CardViewWindow CW;
        public AskForFlavorQuoteWindow(CardViewWindow cw)
        {
            InitializeComponent();
            CW = cw;
        }

        

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CW.flavorQuoteReturn = new Card.FlavorQuote(IdentifierBox.Text, TextBox.Text);
            DialogResult = true;
        }
    }
}
