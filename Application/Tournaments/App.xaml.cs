using Facade;
using Tournaments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Tournaments
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal Manager manager = new Manager();

        #region Image related
        /// <summary>
        /// Browses the local files to find an image for the right component
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        void BrowseImages_Click(object sender, RoutedEventArgs args)
        {
            Button button = args.Source as Button;
            Image image   = null;
            Window window = Window.GetWindow(button);

            string imageType = window is TournamentCreation ? "TournamentImage" : (window is ParticipantCreation ? "ParticipantImage" : null);

            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                FileName = "Images",
                DefaultExt = ".jpg | .png | .gif",
                Filter = "All images files (.jpg, .png, .gif) | *.jpg; *.png; *.gif | JPG files (.jpg) | *.jpg | PNG files (.png) | *.png | GIF files (.gif) | *.gif"
            };

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results 
            if (result.Value)
            {
                if (window is TournamentCreation)
                {
                    image = window.FindName(imageType) as Image;
                }
                else if (window is ParticipantCreation)
                {
                    Frame frame = window.FindName("ParticipantCreationFrame") as Frame;
                    image = (frame.Content as Page).FindName(imageType) as Image;
                }
                image.Source = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
            }
        }

        /// <summary>
        /// Removes the set image
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        void RemoveImage_RightClick(object sender, RoutedEventArgs args)
        {
            Button button = null;
            Image image   = args.Source as Image;
            Window window = Window.GetWindow(image);
            image.Source = null;

            if (window is ParticipantCreation)
            {
                Frame frame = window.FindName("ParticipantCreationFrame") as Frame;
                button = (frame.Content as Page).FindName("ParticipantImageButton") as Button;
            }
            else if (window is TournamentCreation)
            {
                button = window.FindName("TournamentImageButton") as Button;
            }
        }
        #endregion

        #region State bar related
        /// <summary>
        /// Allows the user to drag and drop the Window as well as change the Window's state using the top Grid
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        void DragAndDrop(object sender, MouseButtonEventArgs args)
        {
            Window window = Window.GetWindow(args.Source as DependencyObject);
            if (args.ClickCount == 2)
            {
                MaximizationState_Click(sender, args as RoutedEventArgs);
            }
            window.DragMove();
        }

        /// <summary>
        /// Closes the Window on click
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        void Close_Click(object sender, RoutedEventArgs args)
            => Window.GetWindow(args.Source as Button).Close();

        /// <summary>
        /// Changes the maximization button on click
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        void MaximizationState_Click(object sender, RoutedEventArgs args)
        {
            DependencyObject source = args.Source as DependencyObject;
            Window window = Window.GetWindow(source);

            window.WindowState = (window.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// Minimizes the Window on click
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        void MinimizationState_Click(object sender, RoutedEventArgs args)
            => Window.GetWindow(args.Source as DependencyObject).WindowState = WindowState.Minimized;
        #endregion

        #region Participant creation
        /// <summary>
        /// Displays the Participant creation Window
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        void DisplayParticipantCreation_Click(object sender, RoutedEventArgs args)
        {
            Window window = Window.GetWindow(args.Source as Button);
            if (window is TournamentCreation)
            {
                ComboBox comboBox = window.FindName("TournamentSportToSelect") as ComboBox;
                string type = comboBox.SelectedItem?.ToString();
                if (type == null)
                {
                    MessageBox.Show("Veuillez choisir un sport avant cela.", "Attention", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    new ParticipantCreation
                    (
                        type,
                        type.Equals("TennisSimple")
                            ? new CompetitorCreation() as Page
                            : new TeamCreation(type) as Page,
                        window
                    ).Show();
                }
            }
            else
            {
                new ParticipantCreation((window as ParticipantCreation).Type, new CompetitorCreation() as Page, window).Show();
            }
        }
        #endregion
    }
}

