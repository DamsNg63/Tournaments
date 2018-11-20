using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class IndividualFactory : Factory
    {
        public override Tournament CreateTournament()
        {
            TeamType tournamentType = TeamType.Tennis_Simple;
            string name = "US Championship";
            string location = "IDKWhat Stadium";
            DateTime date = new DateTime(2018, 4, 22);
            List<Participant> list = new List<Participant>();

            list.Add(new Competitor(tournamentType, "ROUSEYROL", "Raphaël", new Uri("http://test")));
            list.Add(new Competitor(tournamentType, "TORTI", "Clément", new Uri("http://test")));
            list.Add(new Competitor(tournamentType, "NGUYEN", "Damien", new Uri("http://test")));
            list.Add(new Competitor(tournamentType, "PARKER", "Tony", new Uri("http://test")));
            list.Add(new Competitor(tournamentType, "BRYANT", "Kobe", new Uri("http://test")));
            list.Add(new Competitor(tournamentType, "KURKLU", "Fikret", new Uri("http://test")));

            return new TeamTournament(tournamentType, name, location, date, list);
        }
    }
}

