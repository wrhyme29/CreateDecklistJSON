using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using CreateDecklistJSONBackend;
using Newtonsoft.Json;


namespace CreateDeckListJSONGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewDeckButton_Click(object sender, RoutedEventArgs e)
        {
            NewDeckWindow newDeckWindow = new NewDeckWindow(this);
            newDeckWindow.ShowDialog();
        }
        private void ChangePathButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "D:\\json";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FilePath.Text = dialog.FileName;
            }
        }

        private void LoadDeckButton_Click(object sender, RoutedEventArgs e)
        {
            Deck deck = DeserializeDeck();
            NewDeckWindow newDeckWindow = new NewDeckWindow(this, deck);
            newDeckWindow.ShowDialog();
        }

        private Deck DeserializeDeck()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FilePath.Text;
            string path = "";
            if (openFileDialog.ShowDialog() == true)
            {
                path = File.ReadAllText(openFileDialog.FileName);
                Deck deck = JsonConvert.DeserializeObject<Deck>(path);
                return deck;
            }

            return null;
            
        }
    }
}
