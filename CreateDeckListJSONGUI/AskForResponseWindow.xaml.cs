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

namespace CreateDeckListJSONGUI
{
    /// <summary>
    /// Interaction logic for AskForIdentifierWindow.xaml
    /// </summary>
    public partial class AskForResponseWindow : Window
    {
        public AskForResponseWindow(string prompt = "")
        {
            InitializeComponent();
            tb_PromptText.Text = prompt;
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
