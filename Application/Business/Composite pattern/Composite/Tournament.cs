using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Business
{
    /// <summary>
    /// Represents a Tournament
    /// </summary>
    [DataContract
        KnownType(typeof(IndividualTournament)), KnownType(typeof(TeamTournament))]
    public abstract class Tournament : Composite, INotifyPropertyChanged
    {
        #region Properties
        public TeamType Type => mType;
        [DataMember]
        private readonly TeamType mType;

        [DataMember]
        public Uri Image { get; private set; }

        [DataMember]
        public string Name
        {
            get { return mName; }
            set { mName = value; OnPropertyChanged(); }
        }
        private string mName;

        [DataMember]
        public DateTime Date { get; private set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        private Participant mWinner;
        public Participant Winner
        {
            get { return mWinner; }
            set { mWinner = value; OnPropertyChanged(); }
        }
        #endregion

        #region Calculated properties needed in the TournamentView
        public int NbParticipants => GetParticipants().Count();
        public double Log2N => Math.Log(NbParticipants) / Math.Log(2);
        public int NbRounds => (int)Math.Ceiling(Log2N);
        public int NearestPow2 { get; private set; }
        public int NbFirstRoundMatches { get; private set; }
        #endregion

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Tournament constructor with 5 parameters
        /// </summary>
        /// <param name="teamType">the TeamType which represents the discipline</param>
        /// <param name="name">the name of the Tournament</param>
        /// <param name="location">the place where the Tournament takes place</param>
        /// <param name="date">the date when the Tournament takes place</param>
        /// <param name="image">the path of the image that represents the Tournament</param>
        public Tournament(TeamType teamType, string name, string location, DateTime date, Uri image) : base()
        {
            Image    = image == null ? new Uri("Resources;Component/Default/banner.png", UriKind.Relative) : image;
            mType    = teamType;
            Name     = name;
            Location = location;
            Date     = date;
        }

        public override void AddComponent(Component component)
        {
            if (!(component is Participant) && !(component is Round))
            {
                throw new ArgumentException("Il n'est possible de rajouter que des tours ou participants à un tournoi.");
            }
            base.AddComponent(component);
        }

        #region Filters
        /// <summary>
        /// Returns a collection of Participant from the Components list
        /// </summary>
        /// <returns>a collection of Participant</returns>
        public IEnumerable<Component> GetParticipants() => ReadOnlyComponents.Where(n => n is Participant);

        /// <summary>
        /// Returns a collection of Round from the Components list
        /// </summary>
        /// <returns>collection of Round</returns>
        public IEnumerable<Component> GetRounds() => ReadOnlyComponents.Where(n => n is Round);
        #endregion

        #region Tree generation
        /// <summary>
        /// Generate the right amount of Round with the given list of Participants
        /// </summary>
        /// <param name="participants">the list of Participant</param>
        public void GenerateRounds(List<Participant> participants)
        {
            // Used in the following two assignments
            double nbParticipantsBase = Math.Log(participants.Count) / Math.Log(2);
            // The number of Round including the "preliminary" phase
            int nbRounds = (int)Math.Ceiling(nbParticipantsBase);
            // Indicates whether the number of Participant will lead to a preliminary phase
            bool hasPreliminary = nbRounds != nbParticipantsBase;
            // The highest power of 2 below the number of Participant
            NearestPow2 = (int)Math.Pow(2, hasPreliminary ? nbRounds - 1 : nbRounds);
            // The number of preliminary Match
            NbFirstRoundMatches = participants.Count - NearestPow2;

            List<Participant> currentParticipants = new List<Participant>(participants);

            if (hasPreliminary)
            {
                Round preliminary = new Round(NbFirstRoundMatches, nbRounds--);
                preliminary.GenerateMatches(Type, currentParticipants, 0);
                AddComponent(preliminary);
            }
            Round firstOrSecond = new Round((int)Math.Pow(2, nbRounds - 1), nbRounds);
            firstOrSecond.GenerateMatches(Type, currentParticipants, hasPreliminary ? NbFirstRoundMatches : 0);
            AddComponent(firstOrSecond);

            Round round;
            for (int i = nbRounds - 2; i >= 0; i--)
            {
                round = new Round((int)Math.Pow(2, i), i + 1);
                round.GenerateMatches(Type, currentParticipants, 0);
                AddComponent(round);
            }
        }
        #endregion

        #region Tournament progress
        /// <summary>
        /// Moves the winners to the next Round or sets the Tournament Winner
        /// </summary>
        public void CarryOn()
        {
            // If the number of Round to be played is equals 1, that means it's the last round
            bool isLastPlayedRound = GetRounds().Where(n => !(n as Round).Terminated).Count() == 1;
            if (!isLastPlayedRound)
            {
                // If there are still rounds left to play, tell the last played round (the first round among those that are still to be played) to put its winner in the next round matches
                var lastPlayedRound = GetRounds().Where(n => !(n as Round).Terminated).FirstOrDefault();
                (lastPlayedRound as Round)?.MoveToNextRound(this);
            }
            else
            {
                // The tournament is finished, get the winner of the last round (finale) and return it
                var lastRound = GetRounds().Where(n => (n as Round).Phase == 1).Single();
                Winner = (lastRound as Round).DetermineWinners(this).Single();
            }
        }
        #endregion

        public override string ToString()
        {
            string description = $"Tournoi de {mType} : {Name}\nLe {Date} à {Location}";

            foreach (Round r in ReadOnlyComponents.Where(n => n is Round))
            {
                description += $"\n---------------------------------------\n{r}";
            }

            description += "\nListe de participants :\n";
            foreach (Participant p in ReadOnlyComponents.Where(n => n is Participant))
            {
                description += $"{p}\n";
            }

            return description;
        }
    }
}
