using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Creates objects related to the Competitor
    /// </summary>
    class IndividualFactory : AbstractFactory
    {
        public override Match CreateMatch(Participant p1, Participant p2)
            => new IndividualMatch(p1 as Competitor, p2 as Competitor);

        public override Tournament CreateTournament(TeamType teamType, string name, string location, DateTime date, Uri image)
            => new IndividualTournament(teamType, name, location, date, image);
    }
}
