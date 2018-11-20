using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents a discipline
    /// </summary>
    [DataContract]
    public class Sport : Composite, IEquatable<Sport>
    {
        [DataMember]
        private readonly TeamType mType;
        public TeamType Type => mType;

        [DataMember]
        public Uri Image { get; private set; }

        /// <summary>
        /// Sport constructor with 2 parameters
        /// </summary>
        /// <param name="type">the TeamType which represents the discipline</param>
        /// <param name="image">the path of the image that represents the Sport</param>
        public Sport(TeamType type, Uri image) : base()
        {
            mType = type;
            Image = image;
        }

        public override void AddComponent(Component component)
        {
            if (!(component is Tournament) && !Type.Equals((component as Tournament).Type))
            {
                throw new ArgumentException("Un sport ne contient que des tournois (pour ce sport).");
            }
            base.AddComponent(component);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals(obj as Sport);
        }

        public bool Equals(Sport other) => mType.Equals(other.mType);

        public override int GetHashCode() => mType.GetHashCode();

        public override string ToString()
        {
            string description = "Tennis tournament:\n";

            foreach (var item in ReadOnlyComponents)
            {
                description += $"\n{item}";
            }

            return description;
        }
    }
}
