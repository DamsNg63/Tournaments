using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents a Team which is made of Competitor
    /// </summary>
    [DataContract]
    public class Team : Participant
    {
        [DataMember]
        private List<Participant> TeamComposition { get; set; }
        public ReadOnlyCollection<Participant> ReadOnlyTeamComposition => TeamComposition.AsReadOnly();

        /// <summary>
        /// Team constructor with 3 parameters
        /// </summary>
        /// <param name="teamType">the TeamType which represents the discipline</param>
        /// <param name="name">the name of the Team</param>
        /// <param name="image">the path of the image that represents the Team</param>
        public Team(TeamType teamType, string name, Uri image) : base(teamType, name, image)
        {
            TeamComposition = new List<Participant>();
        }

        public override void AddParticipant(Participant participant)
        {
            if (TeamComposition.Count() >= (int)Type)
            {
                throw new IndexOutOfRangeException($"Vous ne pouvez pas ajouter {participant.FullName}, l'équipe est au complet !");
            }
            if (TeamComposition.Contains(participant))
            {
                throw new ArgumentException($"{participant.FullName} est déjà dans l'équipe.");
            }
            TeamComposition.Add(participant);
        }

        public override void RemoveParticipant(Participant participant)
        {
            TeamComposition.Remove(participant);
        }

        public override string ToString()
        {
            string description = $"{Name} est une équipe de {Type} composée de {ReadOnlyTeamComposition.Count} joueurs :\n";

            foreach (Competitor c in ReadOnlyTeamComposition)
            {
                description += c + "\n";
            }

            return description;
        }
    }
}
