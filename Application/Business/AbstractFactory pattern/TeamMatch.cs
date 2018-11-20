using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents a Team Match
    /// </summary>
    [DataContract]
    public class TeamMatch : Match
    {
        /// <summary>
        /// TeamMatch constructor with 1 parameter
        /// </summary>
        /// <param name="t1">the first Team</param>
        internal TeamMatch(Team t1) : base(t1) { }

        /// <summary>
        /// TeamMatchh constructor with 2 parameters
        /// </summary>
        /// <param name="t1">the first Team</param>
        /// <param name="t2">the second Team</param>
        internal TeamMatch(Team t1, Team t2) : base(t1, t2) { }
    }
}
