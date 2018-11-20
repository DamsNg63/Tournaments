using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Creates objects related to the Team
    /// </summary>
    class TeamFactory : AbstractFactory
    {
        public override Match CreateMatch(Participant p1, Participant p2)
            => new TeamMatch(p1 as Team, p2 as Team);

        public override Tournament CreateTournament(TeamType teamType, string name, string location, DateTime date, Uri image)
            => new TeamTournament(teamType, name, location, date, image);
    }
}
