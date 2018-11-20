using Facade;
using Tournaments.Tools;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Tournaments
{
    /// <summary>
    /// Interaction logic for ParticipantCreation.xaml
    /// </summary>
    public partial class ParticipantCreation : Window
    {
        private MagicNameDetecter detecter;
        private Window source;
        private Page currentPage;
        public string Type { get; private set; }

        /// <summary>
        /// ParticipantCreation constructor
        /// </summary>
        public ParticipantCreation()
        {
            InitializeComponent();

            detecter = new MagicNameDetecter();
            detecter.MagicNameDetected += Detecter_MagicNameDetected;
        }

        /// <summary>
        /// ParticipantCreation constructor with 3 parameters
        /// </summary>
        /// <param name="type">the discipline assiociated with the Participant</param>
        /// <param name="target">the target Page that represents whether a Team or a Competitor is to be created</param>
        /// <param name="source">the Window that called the constructor</param>
        public ParticipantCreation(string type, Page target, Window source) : this()
        {
            this.source = source;
            Type        = type;
            currentPage = target;
            ParticipantCreationFrame.Navigate(target);
        }

        /// <summary>
        /// Creates the right Participant according to the current Page
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void Validate_Click(object sender, RoutedEventArgs args)
        {
            bool hasEmptyFields = currentPage is TeamCreation
                ? FieldChecker.FieldIsEmpty((currentPage as TeamCreation).TeamName.Text)
                : FieldChecker.FieldIsEmpty((currentPage as CompetitorCreation).LastName.Text, (currentPage as CompetitorCreation).FirstName.Text);

            if (!hasEmptyFields)
            {
                try
                {
                    if (currentPage is TeamCreation)
                    {
                        CreateTeam();
                    }
                    else
                    {
                        CreateCompetitor();
                    }
                    Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Attention !", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        /// <summary>
        /// Displays a welcome message
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void Detecter_MagicNameDetected(object sender, MagicNameDetectedEventArgs args)
        {
            MessageBox.Show(args.Message, "M. BOUHOURS détecté !");
        }

        /// <summary>
        /// Creates a Competitor
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void CreateCompetitor()
        {
            CompetitorCreation cc = currentPage as CompetitorCreation;
            string finalFirstName;
            detecter.Detect(cc.LastName.Text, out finalFirstName);
            (Application.Current as App).manager.CreateCompetitor
            (
                Type,
                cc.LastName.Text,
                finalFirstName ?? cc.FirstName.Text,
                cc.ParticipantImage.Source != null ? new Uri((cc.ParticipantImage.Source as BitmapImage).UriSource.AbsolutePath) : null,
                source is ParticipantCreation ? String.Concat(Type, "Participants") : Type
            );
        }

        /// <summary>
        /// Creates a Team
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void CreateTeam()
        {
            TeamCreation tc = currentPage as TeamCreation;
            (Application.Current as App).manager.CreateTeam
            (
                Type,
                tc.TeamName.Text,
                tc.ParticipantImage.Source != null ? new Uri((tc.ParticipantImage.Source as BitmapImage).UriSource.AbsolutePath) : null,
                tc.ParticipantsSelection.SelectedItems
            );
        }
    }
}
