using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents a Participant in a Tournament
    /// </summary>
    [DataContract
        KnownType(typeof(Competitor)), KnownType(typeof(Team))]
    public abstract class Participant : Component, IEquatable<Participant>
    {
        public string FullName => Name;

        [DataMember]
        private readonly TeamType mType;
        public TeamType Type => mType;

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Uri Image { get; private set; }

        //public int NbWonTournaments => ReadOnlyComponents.Count;

        /// <summary>
        /// Participant constructor with 3 parameters
        /// </summary>
        /// <param name="teamType">the TeamType which represents the discipline</param>
        /// <param name="name">the name of the Participant</param>
        /// <param name="image">the path of the image that represents the Participant</param>
        public Participant(TeamType teamType, string name, Uri image)
        {
            mType = teamType;
            Name  = name.Substring(0, 1).ToUpper() + name.Substring(1);
            Image = image == null ? new Uri("Resources;Component/Default/head.png", UriKind.Relative) : image;
        }

        public override void AddComponent(Component component)
        {
            throw new NotImplementedException("Cette opération est impossible sur un temps fort.");
        }

        public override void RemoveComponent(Component component)
        {
            throw new NotImplementedException("Cette opération est impossible sur un temps fort.");
        }

        /// <summary>
        /// Adds the given Participant to the TeamComposition (located in Team)
        /// </summary>
        /// <param name="participant">the given Participant</param>
        public abstract void AddParticipant(Participant participant);

        /// <summary>
        /// Removes the given Participant from the TeamComposition (located in Team)
        /// </summary>
        /// <param name="participant">the given Participant</param>
        public abstract void RemoveParticipant(Participant participant);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Participant)) return false;
            return Equals(obj as Participant);
        }

        public bool Equals(Participant other) => Type.Equals(other.Type) && Name == other.Name && (Image.Equals(other.Image));

        public override int GetHashCode()
        {
            int hash = 31;
            return hash;
        }
    }
}
