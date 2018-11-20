using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    /// <summary>
    /// Represents a single Participant
    /// </summary>
    [DataContract]
    public class Competitor : Participant, IEquatable<Competitor>
    {
        public new string FullName => $"{Name} {FirstName}";

        [DataMember]
        public string FirstName { get; private set; }

        /// <summary>
        /// Competitor constructor with 4 parameters
        /// </summary>
        /// <param name="teamType">the TeamType which represents the discipline</param>
        /// <param name="lastName">the last name</param>
        /// <param name="firstName">the first name</param>
        /// <param name="image">the path of the Competitor photo</param>
        public Competitor(TeamType teamType, string lastName, string firstName, Uri image) : base(teamType, lastName.ToUpper(), image)
        {
            FirstName = firstName?.Substring(0, 1).ToUpper() + firstName?.Substring(1);
        }

        public override void AddParticipant(Participant participant)
        {
            throw new NotImplementedException("Cette opération est impossible sur un compétiteur.");
        }

        public override void RemoveParticipant(Participant participant)
        {
            throw new NotImplementedException("Cette opération est impossible sur un compétiteur.");
        }

        public override bool Equals(object obj) => base.Equals(obj) && Equals(obj as Competitor);

        public bool Equals(Competitor other) => base.Equals(other) && FirstName == other.FirstName;

        public override int GetHashCode() => base.GetHashCode() * 17 + FirstName.GetHashCode();

        public override string ToString() => $"{Name} {FirstName} : joueur de {Type}";
    }
}
