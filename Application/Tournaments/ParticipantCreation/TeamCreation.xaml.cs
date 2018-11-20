using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tournaments
{
    /// <summary>
    /// Interaction logic for TeamCreation.xaml
    /// </summary>
    public partial class TeamCreation : Page, INotifyPropertyChanged
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
        /// TeamCreation constructor
        /// </summary>
        public TeamCreation()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// TeamCreation constructor with 1 parameter
        /// </summary>
        /// <param name="type">the discipline associated with the Team</param>
        public TeamCreation(string type) : this()
        {
            ParticipantsList = (Application.Current as App).manager.ParticipantsList[String.Concat(type, "Participants")];
        }
    }
}
