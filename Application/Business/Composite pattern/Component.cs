using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Root of the Composite design pattern
    /// </summary>
    [DataContract,
        KnownType(typeof(Highlight)),
        KnownType(typeof(Sport)),
        KnownType(typeof(Round)),
        KnownType(typeof(Tournament)), KnownType(typeof(IndividualTournament)), KnownType(typeof(TeamTournament)),
        KnownType(typeof(Match)), KnownType(typeof(IndividualMatch)), KnownType(typeof(TeamMatch)),
        KnownType(typeof(Participant)), KnownType(typeof(Team)), KnownType(typeof(Competitor))]
    public abstract class Component
    {
        /// <summary>
        /// Adds the given Component to the Components list (located in Composite)
        /// </summary>
        /// <param name="component">the given Component</param>
        public abstract void AddComponent(Component component);

        /// <summary>
        /// Removes the given Component from the Components list (located in Composite)
        /// </summary>
        /// <param name="component">the given Component</param>
        public abstract void RemoveComponent(Component component);
    }
}
