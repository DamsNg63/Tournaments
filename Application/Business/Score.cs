using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents a Score for one Participant
    /// </summary>
    [DataContract]
    public class Score : IEquatable<Score>
    {
        [DataMember]
        public int? ScoreValue { get; private set; }

        /// <summary>
        /// Score default constructor that initializes ScoreValue to 0
        /// </summary>
        public Score() : this(0) { }

        /// <summary>
        /// Score constructor with 1 parameter
        /// </summary>
        /// <param name="value">the score value</param>
        public Score(int? value)
        {
            ScoreValue = value;
        }

        /// <summary>
        /// Changes ScoreValue to the given value
        /// </summary>
        /// <param name="newValue">the given value</param>
        public void ChangeScore(int? newValue)
        {
            ScoreValue = newValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;

            Score other = obj as Score;

            return Equals(obj as Score);
        }

        public bool Equals(Score other) => ScoreValue == other.ScoreValue;

        public override int GetHashCode() => ScoreValue.Value % 31;
    }
}
