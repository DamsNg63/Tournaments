using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Represents a Competitor Tournament
    /// </summary>
    [DataContract]
    public class IndividualTournament : Tournament
    {
        /// <summary>
        /// IndividualTournament constructor with 4 parameters
        /// </summary>
        /// <param name="teamType">the TeamType which represents the discipline</param>
        /// <param name="name">the name of the IndividualTournament</param>
        /// <param name="location">the place where the IndividualTournament takes place</param>
        /// <param name="date">the date when the IndividualTournament takes place</param>
        /// <param name="image">the path of the image that represents the IndividualTournament</param>
        internal IndividualTournament(TeamType teamType, string name, string location, DateTime date, Uri image) : base(teamType, name, location, date, image) { }

        public override void AddComponent(Component component)
        {
            if (!(component is Round) && !(component is Competitor))
            {
                throw new ArgumentException("Il n'est pas possible d'ajouter autre chose qu'un tour ou compétiteur à la liste.");
            }
            base.AddComponent(component);
        }
    }
}
