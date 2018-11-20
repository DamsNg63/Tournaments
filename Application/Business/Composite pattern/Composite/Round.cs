using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents the on-progress state of a Tournament
    /// </summary>
    [DataContract]
    public class Round : Composite
    {
        private static Random random = new Random();

        [DataMember]
        public bool Terminated { get; set; }

        [DataMember]
        private int phase;
        public int Phase { get { return phase; } }

        [DataMember]
        private int nbMatches;

        /// <summary>
        /// Round constructor with 2 parameters
        /// </summary>
        /// <param name="nbMatches">the number of Match this Round possesses</param>
        /// <param name="phase">the phase in the Tournament</param>
        public Round(int nbMatches, int phase) : base()
        {
            Terminated     = false;
            this.nbMatches = nbMatches;
            this.phase     = phase;
        }

        public override void AddComponent(Component component)
        {
            if (!(component is Match))
            {
                throw new ArgumentException("Un tour ne possède que des matches.");
            }
            if (ReadOnlyComponents.Count >= nbMatches)
            {
                throw new ArgumentException("Le nombre maximum de matches pour ce tour est déjà atteint.");
            }
            base.AddComponent(component);
        }

        /// <summary>
        /// Generates the right amount of Match randomly within the Round
        /// </summary>
        /// <param name="type">the type that is used to get the right factory</param>
        /// <param name="participants">the list of participants</param>
        /// <param name="nbPreliminary">the number of preliminary matches</param>
        public void GenerateMatches(TeamType type, List<Participant> currentParticipants, int nbPreliminary)
        {
            Participant p1;
            Participant p2;

            for (int i = 0; i < nbMatches; i++)
            {
                p1 = currentParticipants.ElementAtOrDefault(random.Next(0, currentParticipants.Count));
                currentParticipants.Remove(p1);
                if (nbMatches <= nbPreliminary)
                {
                    p2 = null;
                }
                else
                {
                    p2 = currentParticipants.ElementAtOrDefault(random.Next(0, currentParticipants.Count));
                    currentParticipants.Remove(p2);
                }
                AddComponent(AbstractFactory.GetFactory(type).CreateMatch(p1, p2));
            }
        }

        #region Tournament progress
        /// <summary>
        /// Determines and returns the winners for this Round as a List
        /// </summary>
        /// <param name="tournament">the Tournament this Round belongs to</param>
        /// <returns>the List of winners</returns>
        public List<Participant> DetermineWinners(Tournament tournament)
        {
            List<Participant> winners = new List<Participant>();
            Terminated = true;

            foreach (Match match in ReadOnlyComponents)
            {
                while (match.Score1 == match.Score2)
                {
                    match.Score1 += random.Next(0, 2);
                    match.Score2 += random.Next(0, 2);
                }
                winners.Add(match.Score1 > match.Score2 ? match.P1 : match.P2);
            }

            return winners;
        }

        /// <summary>
        /// Move all the winners to the next Round
        /// </summary>
        /// <param name="tournament">the Tournament this Round belongs to</param>
        public void MoveToNextRound(Tournament tournament)
        {
            List<Participant> winners = DetermineWinners(tournament);

            // If it was the last round
            if (Phase == 1)
            {
                return;
            }

            foreach (Match match in (tournament.GetRounds().Where(n => (n as Round).Phase == Phase - 1).Single() as Round).ReadOnlyComponents)
            {
                Participant next;

                // Do that twice (adding participant to match) if the next match don't have any participant
                for (int i = 0; i < 2; i++)
                {
                    if (match.P1 == null || match.P2 == null)
                    {
                        next = winners.ElementAtOrDefault(0);
                        match.AddParticipantToMatch(next);
                        winners.Remove(next);
                    }
                }
            }
        }
        #endregion

        public override string ToString()
        {
            string description = $"Round n°{Phase} : le round est composé de {nbMatches} match" + (nbMatches < 2 ? "" : "es") + ".\n";

            foreach (Match m in ReadOnlyComponents)
            {
                description += $"\t{m.ToString()}\n";
            }

            return description;
        }
    }
}
