using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTournaments
{
    /// <summary>
    /// Facade between the Business assembly and the GraphicalTournaments assembly
    /// </summary>
    public class Manager
    {
        public ObservableCollection<Sport> SportsListData { get; private set; }
        public Dictionary<string, ObservableCollection<Participant>> ParticipantsList { get; private set; }

        /// <summary>
        /// Manager constructor , uses the StubDataManager
        /// </summary>
        public Manager() : this(new StubDataManager()) { }

        /// <summary>
        /// Manager constructor with 1 parameter
        /// </summary>
        /// <param name="dataManager">the source of the application data, whether it be the stub or serialized objects</param>
        public Manager(IDataManager dataManager)
        {
            SportsListData   = dataManager.LoadAppData();
            ParticipantsList = dataManager.LoadParticipants();
        }

        /// <summary>
        /// Creates a Tournament
        /// </summary>
        /// <param name="teamType">the discipline associated with the Tournament</param>
        /// <param name="name">the name of the Tournament</param>
        /// <param name="location">the place where the Tournament takes place</param>
        /// <param name="date">the date when the Tournament takes place</param>
        /// <param name="image">the image of the Tournament</param>
        /// <param name="participants">the List of Participant that attend the Tournament</param>
        public void CreateTournament(TeamType teamType, string name, string location, DateTime date, Uri image, List<Participant> participants)
        {
            Tournament tournament = AbstractFactory.GetFactory(teamType).CreateTournament(teamType, name, location, date, image);
            Sport sport = SportsListData.Where(n => n.Type.Equals(teamType)).Single();

            foreach (Participant participant in participants)
            {
                tournament.AddComponent(participant);
            }

            tournament.GenerateRounds(participants);

            sport.AddComponent(tournament);
        }
    }
}
