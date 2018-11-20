using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Used to create environments of objects depending on their TeamType
    /// </summary>
    public abstract class AbstractFactory
    {
        /// <summary>
        /// Get the right concrete factory using the given TeamType
        /// </summary>
        /// <param name="type">the given TeamType</param>
        /// <returns>the right concrete factory</returns>
        public static AbstractFactory GetFactory(TeamType type)
        {
            if ((int)type == 1)
            {
                return new IndividualFactory();
            }
            return new TeamFactory();
        }

        /// <summary>
        /// Creates a Match
        /// </summary>
        /// <param name="p1">the first contestant</param>
        /// <param name="p2">the second contestant</param>
        /// <returns>the proper concret Match according to the TeamType</returns>
        public abstract Match CreateMatch(Participant p1, Participant p2);

        /// <summary>
        /// Creates a Tournament
        /// </summary>
        /// <param name="teamType">the discipline of the Tournament</param>
        /// <param name="name">the name of the Tournament</param>
        /// <param name="location">the location where the Tournament takes place</param>
        /// <param name="date">the date when the Tournament takes place</param>
        /// <param name="image">the path of the image that represents the Tournament</param>
        /// <returns>the proper concret Tournament according to the TeamType</returns>
        public abstract Tournament CreateTournament(TeamType teamType, string name, string location, DateTime date, Uri image);
    }
}
