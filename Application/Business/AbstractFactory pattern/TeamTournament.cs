using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents a Team Tournament
    /// </summary>
    [DataContract]
    public class TeamTournament : Tournament
    {
        /// <summary>
        /// TeamTournament constructor with 4 parameters
        /// </summary>
        /// <param name="teamType">the TeamType which represents the discipline</param>
        /// <param name="name">the name of the TeamTournament</param>
        /// <param name="location">the place where the TeamTournament takes place</param>
        /// <param name="date">the date when the TeamTournament</param>
        /// <param name="image">the path of the image that represents the TeamTournament</param>
        internal TeamTournament(TeamType teamType, string name, string location, DateTime date, Uri image) : base(teamType, name, location, date, image) { }

        public override void AddComponent(Component component)
        {
            if (!(component is Round) && !(component is Team))
            {
                throw new ArgumentException("Il n'est pas possible d'ajouter autre chose qu'un tour ou une équipe à la liste.");
            }
            base.AddComponent(component);
        }
    }
}
