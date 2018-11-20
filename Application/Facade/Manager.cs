using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    /// <summary>
    /// Facade between the Business assembly and the Tournaments assembly
    /// </summary>
    public class Manager : INotifyPropertyChanged
    {
        private IDataManager dataManager;

        #region Full properties
        public ObservableCollection<Sport> SportsListData
        {
            get { return mSportsListData; }
            set { mSportsListData = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Sport> mSportsListData;

        public Dictionary<string, ObservableCollection<Participant>> ParticipantsList
        {
            get { return mParticipantsList; }
            set { mParticipantsList = value; OnPropertyChanged(); }
        }
        private Dictionary<string, ObservableCollection<Participant>> mParticipantsList;
        #endregion

        #region Calculated properties
        public string DefaultBackupFolder => (dataManager as XMLDataManager)?.DefaultBackupFolder;

        public string TournamentsXMLFile  => (dataManager as XMLDataManager)?.TournamentsXMLFile;
        public string ParticipantsXMLFile => (dataManager as XMLDataManager)?.ParticipantsXMLFile;

        public bool BackupExists
            => File.Exists(Path.Combine(DefaultBackupFolder, TournamentsXMLFile))
            && File.Exists(Path.Combine(DefaultBackupFolder, ParticipantsXMLFile));
        #endregion

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Manager constructor, checks whether the backup files are in the default folder and initializes the right IDataManager
        /// </summary>
        public Manager()
        {
            dataManager = new XMLDataManager();

            if (BackupExists)
            {
                LoadData(DefaultBackupFolder);
            }
            else
            {
                LoadStubData();
            }
        }

        #region Serialization
        /// <summary>
        /// Loads the basic data if the files are not in the default folder
        /// </summary>
        public void LoadStubData()
        {
            StubDataManager stub = new StubDataManager();

            SportsListData   = stub.LoadAppData();
            ParticipantsList = stub.LoadParticipants();
        }

        /// <summary>
        /// Loads the basic data if the files are not in the default folder
        /// </summary>
        public void LoadEmptyData()
        {
            IDataManager tmpDataManager = new EmptyDataManager();

            SportsListData   = tmpDataManager.LoadAppData();
            ParticipantsList = tmpDataManager.LoadParticipants();
        }

        /// <summary>
        /// Loads the data from the given path
        /// </summary>
        /// <param name="path">the given path</param>
        public void LoadData(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                Directory.SetCurrentDirectory(path);
                SportsListData   = dataManager.LoadAppData();
                ParticipantsList = dataManager.LoadParticipants();
            }
        }

        /// <summary>
        /// Saves the data to the given path
        /// </summary>
        /// <param name="path">the given path</param>
        public void SaveData(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Directory.SetCurrentDirectory(path);
                dataManager.SaveAppData(SportsListData);
                dataManager.SaveParticipants(ParticipantsList);
            }
        }
        #endregion

        #region Use cases
        /// <summary>
        /// Adds a Highlight to the given Match
        /// </summary>
        /// <param name="match">the given Match</param>
        /// <param name="description">the description of the Highlight</param>
        public void AddHighlight(object match, string description)
            => (match as Match).AddComponent(new Highlight(description));

        /// <summary>
        /// Moves the winners to the next Round or sets the Tournament Winner
        /// </summary>
        /// <param name="tournament"></param>
        public void CarryOn(object tournament)
            => (tournament as Tournament)?.CarryOn();

        /// <summary>
        /// Creates a Competitor and adds it to the ParticipantsList
        /// </summary>
        /// <param name="type">the discipline associated with the Competitor</param>
        /// <param name="lastName">the last name of the Competitor</param>
        /// <param name="firstName">the first name of the Competitor</param>
        /// <param name="image">the path of the image that represents the Competitor</param>
        /// <param name="dictionaryKey">the key of the ParticipantsList dictionary</param>
        public void CreateCompetitor(string type, string lastName, string firstName, Uri image, string dictionaryKey)
            => ParticipantsList[dictionaryKey].Add(new Competitor((TeamType)Enum.Parse(typeof(TeamType), type), lastName, firstName, image));

        /// <summary>
        /// Creates a Team and adds it to the ParticipantsList
        /// </summary>
        /// <param name="type">the discipline associated with the Team</param>
        /// <param name="name">the name of the Team</param>
        /// <param name="image">the path of the image that represents the Team</param>
        /// <param name="participants">the list of Competitor that is being added to the Team</param>
        public void CreateTeam(string type, string name, Uri image, System.Collections.IList participants)
        {
            Team team = new Team((TeamType)Enum.Parse(typeof(TeamType), type), name, image);
            foreach (var item in participants.Cast<Participant>().ToList())
            {
                team.AddParticipant(item);
            }
            ParticipantsList[type].Add(team);
        }

        /// <summary>
        /// Creates a Tournament
        /// </summary>
        /// <param name="type">the discipline associated with the Tournament</param>
        /// <param name="name">the name of the Tournament</param>
        /// <param name="location">the place where the Tournament takes place</param>
        /// <param name="date">the date when the Tournament takes place</param>
        /// <param name="image">the image of the Tournament</param>
        /// <param name="participants">the List of Participant that attend the Tournament</param>
        public void CreateTournament(string type, string name, string location, DateTime date, Uri image, System.Collections.IList participants)
        {
            TeamType teamType = (TeamType)Enum.Parse(typeof(TeamType), type);
            Tournament tournament = AbstractFactory.GetFactory(teamType).CreateTournament(teamType, name, location, date, image);
            Sport sport = SportsListData.Where(n => n.Type.Equals(teamType)).Single();

            foreach (Participant participant in participants)
            {
                tournament.AddComponent(participant);
            }

            tournament.GenerateRounds(participants.Cast<Participant>().ToList());

            sport.AddComponent(tournament);
        }

        /// <summary>
        /// Deletes the givenn Tournament
        /// </summary>
        /// <param name="tournament">the given Tournament</param>
        public void DeleteTournament(object tournament)
        {
            Sport sport = SportsListData.Where(n => n.Type == (tournament as Tournament).Type).Single();
            sport.RemoveComponent(tournament as Tournament);
        }

        /// <summary>
        /// Gets the collection of Component located in the given Round 
        /// </summary>
        /// <param name="round">`the given Round</param>
        /// <param name="components">the collection of Component</param>
        public void GetRoundProperties(object round, out IEnumerable<object> components)
        {
            components = (round as Round).ReadOnlyComponents;
        }

        /// <summary>
        /// Gets the name of the given Tournament
        /// </summary>
        /// <param name="tournament">the given Tournament</param>
        /// <param name="name">the name of the Tournament</param>
        public void GetTournamentProperties(object tournament, out string name)
        {
            name = (tournament as Tournament).Name;
        }

        /// <summary>
        /// Gets mainly the collection of Round located in the given Tournament as well as a few calculated properties
        /// </summary>
        /// <param name="tournament">the given Tournament</param>
        /// <param name="rounds">the collection of Round</param>
        /// <param name="nbRounds">the number of Rounds in the Tournament</param>
        /// <param name="log2N">the binary logarithm needed in TournamentView</param>
        public void GetTournamentProperties(object tournament, out IEnumerable<object> rounds, out int nbRounds, out double log2N)
        {
            Tournament t = tournament as Tournament;
            rounds = t.GetRounds();
            nbRounds = t.NbRounds;
            log2N = t.Log2N;
        }
        #endregion
    }
}
