using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents a Match
    /// </summary>
    [DataContract
        KnownType(typeof(IndividualMatch)), KnownType(typeof(TeamMatch))]
    public abstract class Match : Composite, INotifyPropertyChanged
    {
        #region Full properties
        [DataMember]
        private Participant p1;
        public Participant P1
        {
            get { return p1; }
            set { p1 = value; OnPropertyChanged(); }
        }

        [DataMember]
        private Participant p2;
        public Participant P2
        {
            get { return p2; }
            set { p2 = value; OnPropertyChanged(); }
        }

        // Get access to the score in order to bind it to the corresponding TextBox
        [DataMember]
        private Score p1Score;
        public int? Score1
        {
            get { return p1Score?.ScoreValue; }
            set { p1Score?.ChangeScore(value); OnPropertyChanged(); }
        }

        // Get access to the score in order to bind it to the corresponding TextBox
        [DataMember]
        private Score p2Score;
        public int? Score2
        {
            get { return p2Score?.ScoreValue; }
            set { p2Score?.ChangeScore(value); OnPropertyChanged(); }
        }
        #endregion

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Match constructor with 1 parameter
        /// </summary>
        /// <param name="p1">the first Participant</param>
        public Match(Participant p1) : base()
        {
            if (p1 != null)
            {
                this.p1 = p1;
                p1Score = new Score();
            }
        }

        /// <summary>
        /// Match constructor with 2 parameters
        /// </summary>
        /// <param name="p1">the first Participant</param>
        /// <param name="p2">the second Participant</param>
        public Match(Participant p1, Participant p2) : this(p1)
        {
            if (p2 != null)
            {
                if (!p1.Type.Equals(p2.Type))
                {
                    throw new ArgumentException("Ce ne sont pas des joueurs du même sport !");
                }
                this.p2 = p2;
                p2Score = new Score();
            }
        }

        public override void AddComponent(Component component)
        {
            if (!(component is Highlight))
            {
                throw new ArgumentException("Il n'est possible de rajouter que des temps-forts.");
            }
            base.AddComponent(component);
        }

        /// <summary>
        /// Adds the given participants (2 maximum) and initializes their score to 0
        /// </summary>
        /// <param name="participant">the given participants</param>
        public void AddParticipantToMatch(Participant participant)
        {
            if (P1 == null)
            {
                P1      = participant;
                p1Score = new Score();
                Score1  = 0;
            }
            else if (P2 == null)
            {
                P2      = participant;
                p2Score = new Score();
                Score2  = 0;
            }
        }

        public override string ToString()
        {
            string description = "Match :\n";

            description += $"\t\t{P1?.Name} ({Score1}) - ({Score2}) {P2?.Name}";
            description += "\n\t\tCommentaires :";
            foreach (Highlight h in ReadOnlyComponents)
            {
                description += $"\n\t\t{h.PostDate} - {h.Description}";
            }

            return description;
        }
    }
}