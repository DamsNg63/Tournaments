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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tournaments
{
    /// <summary>
    /// Interaction logic for MatchDetailsDetails.xaml
    /// </summary>
    public partial class MatchDetailsView : Window
    {
        /// <summary>
        /// MatchDetailsView constructor
        /// </summary>
        public MatchDetailsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds a Highlight to the selected Match
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private void CommentButton_Click(object sender, RoutedEventArgs args)
        {
            string description = CommentTextBox.Text;

            if (!string.IsNullOrEmpty(description))
            {
                CommentTextBox.Clear();
                (Application.Current as App).manager.AddHighlight(DataContext, description);
            }
        }
    }
}
