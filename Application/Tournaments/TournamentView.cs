using Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Tournaments
{
    /// <summary>
    /// Represents the Tournament Tree
    /// </summary>
    public class TournamentView
    {
        private Grid grid;

        /// <summary>
        /// TournamentView constructor
        /// </summary>
        /// <param name="grid">the Grid that contains the Tournament tree</param>
        /// <param name="nbParticipants">the number of Participants</param>
        public TournamentView(Grid grid, object tournament)
        {
            this.grid = grid;
            GenerateTree(tournament);
        }

        /// <summary>
        /// Generates a tree from the tournament member
        /// </summary>
        private void GenerateTree(object tournament)
        {
            Manager manager = (Application.Current as App).manager;
            int roundIndex = 0;
            // Define the maximum number of row needed
            int maxNbRows = 0;

            IEnumerable<object> rounds;
            int nbRounds;
            double log2N;
            manager.GetTournamentProperties(tournament, out rounds, out nbRounds, out log2N);

            foreach (object round in rounds)
            {
                IEnumerable<object> components;
                manager.GetRoundProperties(round, out components);
                CreateRound(nbRounds, log2N, components, roundIndex);
                roundIndex++;
                // Find out which round has the greater number of match
                maxNbRows = components.Count() > maxNbRows ? components.Count() : maxNbRows;
            }

            // Define the number of row of the grid
            for (int i = 0; i < maxNbRows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { MinHeight = 70 });
            }
        }

        /// <summary>
        /// Generates the given Round at the specified Round index
        /// </summary>
        /// <param name="round">the given Round</param>
        /// <param name="roundIndex">the Round index</param>
        private void CreateRound(int nbRounds, double log2N, IEnumerable<object> roundComponents, int roundIndex)
        {
            int matchIndex = 0;

            // Add a column to the grid
            grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 350 });

            foreach (object match in roundComponents)
            {
                CreateMatch(nbRounds, log2N, match, roundIndex, matchIndex++);
            }
        }

        /// <summary>
        /// Inserts a StackPanel that contains 2 TextBlock with the following names (example with the very first cell):
        ///     - StackPanel    -> Cell_0_0
        ///     - Participant 1 -> Cell_0_0_p1
        ///     - Participant 2 -> Cell_0_0_p2
        /// </summary>
        /// <param name="match">the Match that is associated with the StackPanel</param>
        /// <param name="roundIndex">the Round index</param>
        /// <param name="matchIndex">the Match index within the Round</param>
        private void CreateMatch(int nbRounds, double log2N, object match, int roundIndex, int matchIndex)
        {
            // Associate the stack panel with a click event
            StackPanel stackPanel = new StackPanel()
            {
                Name = $"Cell_{roundIndex}_{matchIndex}",
                Style = Application.Current.Resources["TreeStackPanel"] as Style,
            };

            // Put the stack panel in the right position of the grid based on the tournament architecture
            stackPanel.SetValue(Grid.ColumnProperty, roundIndex);
            // Add the event to show the associated match when clicked
            stackPanel.MouseUp += Match_Click;

            // Set the Data contexte of the stackpanel in order to retrieve the associated match when clicked
            stackPanel.DataContext = match;

            // Define the rowspan of the match to determine the grid's row
            int rowSpan = 1;
            if (roundIndex != 0)
            {
                rowSpan = (int)Math.Pow(2, nbRounds != log2N ? roundIndex - 1 : roundIndex);
                Grid.SetRowSpan(stackPanel, rowSpan);
            }
            stackPanel.SetValue(Grid.RowProperty, matchIndex * rowSpan);

            // The stack panel representing the FIRST participant of the match
            // It contain a textblock with the name of the participant
            // and a textbox for it score during the match
            StackPanel row1 = new StackPanel()
            {
                Style       = Application.Current.Resources["TreeRowStackPanel"] as Style,
                DataContext = match
            };

            TextBlock ntb1 = new TextBlock()
            {
                Background = Brushes.LightGray,
                Name       = $"Cell_{roundIndex}_{matchIndex}_p1",
                Style      = Application.Current.Resources["TreeNameTextBlock"] as Style
            };
            TextBox stb1 = new TextBox()
            {
                Background = Brushes.LightSkyBlue,
                Name       = $"Cell_{roundIndex}_{matchIndex}_s1",
                Style      = Application.Current.Resources["TreeScoreTextBox"] as Style
            };

            // Bind the textbox with the score 
            Binding binding             = new Binding("Score1");
            binding.Source              = match;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Mode                = BindingMode.TwoWay;
            stb1.SetBinding(TextBox.TextProperty, binding);

            // Bind the name with the textblock
            Binding bindingName             = new Binding("P1.Name");
            bindingName.Source              = match;
            bindingName.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindingName.Mode                = BindingMode.TwoWay;
            ntb1.SetBinding(TextBlock.TextProperty, bindingName);

            row1.Children.Add(ntb1);
            row1.Children.Add(stb1);

            // Same thing for the SECOND participant
            StackPanel row2 = new StackPanel()
            {
                Style = Application.Current.Resources["TreeRowStackPanel"] as Style,
            };
            TextBlock ntb2 = new TextBlock()
            {
                Background = Brushes.LightGray,
                Name       = $"Cell_{roundIndex}_{matchIndex}_p2",
                Style      = Application.Current.Resources["TreeNameTextBlock"] as Style
            };
            TextBox stb2 = new TextBox()
            {
                Background = Brushes.LightSkyBlue,
                Style      = Application.Current.Resources["TreeScoreTextBox"] as Style
            };

            Binding binding2             = new Binding("Score2");
            binding2.Source              = match;
            binding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding2.Mode                = BindingMode.TwoWay;
            stb2.SetBinding(TextBox.TextProperty, binding2);

            Binding bindingName2             = new Binding("P2.Name");
            bindingName2.Source              = match;
            bindingName2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindingName2.Mode                = BindingMode.TwoWay;
            ntb2.SetBinding(TextBlock.TextProperty, bindingName2);

            row2.Children.Add(ntb2);
            row2.Children.Add(stb2);
            
            // Add the elment to the main stackPanel
            stackPanel.Children.Add(row1);
            stackPanel.Children.Add(row2);

            // Add the main StackPanel to the grid
            grid.Children.Add(stackPanel);
        }

        /// <summary>
        /// Shows the detail of the Match
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void Match_Click(object sender, MouseButtonEventArgs args)
        {
            // Retrieve the associated match
            object match = ((sender as StackPanel).DataContext);

            MatchDetailsView matchDetails = new MatchDetailsView();
            matchDetails.DataContext = match;
            matchDetails.Show();
        }
    }
}
