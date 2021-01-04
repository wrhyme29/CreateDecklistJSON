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
    public partial class AskForIconsWindow : Window
    {
        public ObservableCollection<ObservedString> iconList;
        public AskForIconsWindow()
        {
            InitializeComponent();
            iconList = new ObservableCollection<ObservedString>();
            cmbIcons.ItemsSource = iconList;
            List<string> iconStrings = BuildIconList();
            foreach(string icon in iconStrings)
            {
                iconList.Add(new ObservedString(icon));
            }
            cmbIcons.SelectedIndex = 0;
        }

        public string ResponseText
        {
            get { return iconList.Where(os => iconList.IndexOf(os) == cmbIcons.SelectedIndex).FirstOrDefault().Name; }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private List<string> BuildIconList()
        {
            List<string> smallIcons = new List<string>()
            {
                "CannotDealDamage",
                "CannotDrawCards",
                "CannotPlayCards",
                "CannotUsePowers",
                "CounterDamage",
                "Destroy",
                "DestroyEnvironment",
                "DestroyEquipment",
                "DestroyOngoing",
                "DestroySelf",
                "DestroyTarget",
                "DestroyHero",
                "Discard",
                "DrawCardExtra",
                "DrawCardNow",
                "EndOfTurnAction",
                "GainHP",
                "HasPower",
                "IncreaseGainHP",
                "ReduceGainHP",
                "MakeDamageIrreducible",
                "MakeDecision",
                "Manipulate",
                "PlayCardExtra",
                "PlayCardNow",
                "Search",
                "SkipTurn",
                "StartAndEndOfTurnAction",
                "StartOfTurnAction",
                "UsePowerExtra",
                "UsePowerNow",
                "Indestructible",
                "Perform",
                "PerformMelody",
                "PerformHarmony",
                "PerformRhythm",
                "Accompany",
                "AccompanyHarmony",
                "AccompanyRhythm",
                "CancelledDamageGreen",
                "CancelledDamageRed",
                "DealUnknownDamage",
                "MakeDamageUnredirectable",
                "FlipFaceUp",
                "FlipFaceDown",
                "FlipFaceUpAndDown",
                "LoseTheGame",
                "WinTheGame",
                "CannotWinTheGame",
                "AddTokens",
                "RemoveTokens",
                "AddOrRemoveTokens",
                "AllForms",
                "CrocodileForm",
                "GazelleForm",
                "RhinocerosForm",
                "CannotGainHP",
                "RemoveFromGame",
                "MakeDamageUnincreasable",
                "Filter",
                "TakeExtraTurn",
                "FlipArcanaControlToken",
                "FlipAvianControlToken",
                "FlipControlToken",
                "SwitchBattleZone",
                "MoveCountdownTokenDown",
                "MoveCountdownTokenUp",
                "RemoveDevastation",
                "AddDevastation",
            };

            var damageTypes = Enum.GetNames(typeof(DamageType));
            var damageStrings = new[]
            {
                "DealDamage",
                "ImmuneToDamage",
                "IncreaseDamageDealt",
                "IncreaseDamageTaken",
                "RedirectDamage",
                "ReduceDamageDealt",
                "ReduceDamageTaken"
            };
            foreach (var str in damageStrings)
            {
                smallIcons.Add(str);
            }

            foreach (var dt in damageTypes)
            {
                foreach (var str in damageStrings)
                {
                    smallIcons.Add(str + dt);
                }
            }

            smallIcons.Sort();
            return smallIcons;
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
