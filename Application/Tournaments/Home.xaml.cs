using Facade;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace Tournaments
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        #region Fields and properties
        public Manager Manager => (Application.Current as App).manager;
        private TournamentView tournamentView;

        public object SelectedTournament
        {
            get { return selectedTournament; }
            set { selectedTournament = value; ShowTree(); }
        }
        private object selectedTournament; 
        #endregion

        /// <summary>
        /// Home constructor
        /// </summary>
        public Home()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Display
        /// <summary>
        /// Shows the tree of the selected tournament
        /// </summary>
        private void ShowTree()
        {
            TournamentTreeGrid.Children.Clear();
            TournamentTreeGrid.ColumnDefinitions.Clear();
            TournamentTreeGrid.RowDefinitions.Clear();

            tournamentView = SelectedTournament != null ? new TournamentView(TournamentTreeGrid, SelectedTournament) : null;
        }

        /// <summary>
        /// Displays the tournament creation window
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void DisplayTournamentCreation_Click(object sender, RoutedEventArgs args)
            => new TournamentCreation().Show();

        /// <summary>
        /// When a tournament is selected
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void Tournaments_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            SelectedTournament = (sender as ListView).SelectedItem;
            TournamentBanner.DataContext = SelectedTournament;
            TournamentButtons.Children.Clear();

            if (SelectedTournament != null)
            {

                Thickness buttonMargin = new Thickness();
                buttonMargin.Left = 50;
                buttonMargin.Right = 50;
                buttonMargin.Top = 20;
                buttonMargin.Bottom = 20;

                ShowButton("PrintButton", "Imprimer", buttonMargin, PrintTournament_Click);
                ShowButton("DeleteTournamentButton", "Supprimer ce tournoi", buttonMargin, DeleteTournament_Click);
                ShowButton("NextRoundButton", "→ Tour suivant", buttonMargin, MoveToNextRound_Click);
            }
        }

        /// <summary>
        /// Shows the right Button with the given parameters
        /// </summary>
        /// <param name="name">the name of the Button</param>
        /// <param name="content">the content of the Button</param>
        /// <param name="buttonMargin">the margin of the Button</param>
        /// <param name="handler">the EventHandler of the Button</param>
        private void ShowButton(string name, string content, Thickness buttonMargin, RoutedEventHandler handler)
        {
            Button button = new Button()
            {
                Name    = name,
                Style   = Application.Current.Resources["DefaultButton"] as Style,
                Content = content
            };
            button.Margin = buttonMargin;
            button.Click += handler;
            TournamentButtons.Children.Add(button);
        } 
        #endregion

        #region Tournament related use cases
        /// <summary>
        /// Moves every winner of the last unplayed Round to the next Round
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void MoveToNextRound_Click(object sender, RoutedEventArgs args)
            => Manager.CarryOn(SelectedTournament);

        /// <summary>
        /// Delete the selected tournament
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void DeleteTournament_Click(object sender, RoutedEventArgs args)
        {
            // Ask the user if he reaalu wants to delete the tournament
            switch (MessageBox.Show($"Voulez-vous vraiment supprimer ce tournoi ?", "Attention !", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    // Find the location of the tournament in the list
                    Manager.DeleteTournament(SelectedTournament);
                    break;
                default:
                    break;
            }
        }
         
        /// <summary>
        /// Print the selected tournament
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void PrintTournament_Click(object sender, RoutedEventArgs args)
        {
            PrintDialog printDialog = new PrintDialog();
            string name;

            Manager.GetTournamentProperties(SelectedTournament, out name);

            bool? result = printDialog.ShowDialog();
            if (result.Value)
            {
                printDialog.PrintVisual(TournamentContentGrid, name);
            }
        }
        #endregion

        #region Save and load
        /// <summary>
        /// Gets the current directory when the client is trying to save or load the data
        /// </summary>
        /// <returns>the selected directory</returns>
        private string GetSelectedDirectory()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog() { RootFolder = Environment.SpecialFolder.MyComputer })
            {
                dialog.ShowDialog();
                return dialog.SelectedPath ?? null;
            }
        }

        /// <summary>
        /// Loads the data from the selected folder
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void LoadFromFolder_Click(object sender, RoutedEventArgs args)
        {
            string path = GetSelectedDirectory();

            if (File.Exists(Path.Combine(path, Manager.TournamentsXMLFile))
                && File.Exists(Path.Combine(path, Manager.ParticipantsXMLFile)))
            {
                Manager.LoadData(path);
            }
        }

        /// <summary>
        /// Saves the data to the selected folder
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void SaveToFolder_Click(object sender, RoutedEventArgs args)
            => Manager.SaveData(GetSelectedDirectory()); 
        #endregion

        #region Load and close
        /// <summary>
        /// Prompts whether the user wants to load from a folder at start-up
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            if (!Manager.BackupExists)
            {
                switch (MessageBox.Show("Voulez-vous charger depuis un dossier spécifique ?", "Premier lancement ou chargement des données", MessageBoxButton.YesNo, MessageBoxImage.Information))
                {
                    case MessageBoxResult.Yes:
                        LoadFromFolder_Click(sender, new RoutedEventArgs());
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts whether the user wants to save before closing
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void Window_Closing(object sender, CancelEventArgs args)
        {
            switch (MessageBox.Show("Voulez-vous sauvegarder ?", "Fermeture de l'application", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    SaveToFolder_Click(sender, new RoutedEventArgs());
                    goto case MessageBoxResult.No;
                case MessageBoxResult.No:
                    Environment.Exit(0);
                    break;
                default:
                    args.Cancel = true;
                    break;
            }
        } 
        #endregion
    }
}
