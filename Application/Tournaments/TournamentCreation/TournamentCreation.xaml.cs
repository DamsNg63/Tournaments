using Tournaments.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tournaments
{
    /// <summary>
    /// Interaction logic for TournamentCreation.xaml
    /// </summary>
    public partial class TournamentCreation : Window, INotifyPropertyChanged
    {
        public object ParticipantsList
        {
            get { return mParticipantsList; }
            private set { mParticipantsList = value; OnPropertyChanged(); }
        }
        private object mParticipantsList;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// TournamentCreation constructor
        /// </summary>
        public TournamentCreation()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Updates the list of Participant when a Sport is selected
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void SelectionChanged_ComboBox(object sender, SelectionChangedEventArgs args)
        {
            ParticipantsList = (Application.Current as App).manager.ParticipantsList[TournamentSportToSelect.SelectedItem.ToString()];
        }

        /// <summary>
        /// Creates a Tournament
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void Validate_Click(object sender, RoutedEventArgs args)
        {
            string type = TournamentSportToSelect.SelectedItem?.ToString();
            if (!FieldChecker.FieldIsEmpty(type, TournamentName.Text, TournamentLocation.Text, TournamentDate.SelectedDate, ParticipantsSelection.SelectedItems.Count))
            {
                try
                {
                    (Application.Current as App).manager.CreateTournament
                    (
                        type,
                        TournamentName.Text,
                        TournamentLocation.Text,
                        TournamentDate.SelectedDate.Value,
                        TournamentImage.Source != null ? new Uri((TournamentImage.Source as BitmapImage).UriSource.AbsolutePath) : null,
                        ParticipantsSelection.SelectedItems
                    );
                    Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
