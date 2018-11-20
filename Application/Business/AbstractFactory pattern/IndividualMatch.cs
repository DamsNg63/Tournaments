using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents of a Competitor Match
    /// </summary>
    [DataContract]
    public class IndividualMatch : Match
    {
        /// <summary>
        /// IndividualMatch constructor with 1 parameter
        /// </summary>
        /// <param name="c1">the first Competitor</param>
        internal IndividualMatch(Competitor c1) : base(c1) { }

        /// <summary>
        /// IndividualMatch constructor with 2 parameters
        /// </summary>
        /// <param name="c1">the first Competitor</param>
        /// <param name="c2">the second Competitor</param>
        internal IndividualMatch(Competitor c1, Competitor c2) : base(c1, c2) { }
    }
}
