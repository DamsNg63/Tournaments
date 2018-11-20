using Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Creates empty collections
    /// </summary>
    public class EmptyDataManager : IDataManager
    {
        public ObservableCollection<Sport> LoadAppData()
        {
            ObservableCollection<Sport> data = new ObservableCollection<Sport>();

            Sport basketball   = new Sport(TeamType.Basketball, new Uri("Resources;Component/Sports/basketball_icon.png", UriKind.Relative));
            Sport football     = new Sport(TeamType.Football, new Uri("Resources;Component/Sports/football_icon.png", UriKind.Relative));
            Sport handball     = new Sport(TeamType.Handball, new Uri("Resources;Component/Sports/handball_icon.png", UriKind.Relative));
            Sport tennisDouble = new Sport(TeamType.TennisDouble, new Uri("Resources;Component/Sports/tennis_icon.png", UriKind.Relative));
            Sport tennisSimple = new Sport(TeamType.TennisSimple, new Uri("Resources;Component/Sports/tennis_icon.png", UriKind.Relative));

            data.Add(basketball);
            data.Add(football);
            data.Add(handball);
            data.Add(tennisDouble);
            data.Add(tennisSimple);

            return data;
        }

        public Dictionary<string, ObservableCollection<Participant>> LoadParticipants()
        {
            Dictionary<string, ObservableCollection<Participant>> allParticipants = new Dictionary<string, ObservableCollection<Participant>>();

            ObservableCollection<Participant> tennisCompetitors = new ObservableCollection<Participant>();

            // Teams
            allParticipants.Add(TeamType.Basketball.ToString(), new ObservableCollection<Participant>());
            allParticipants.Add(TeamType.Football.ToString(), new ObservableCollection<Participant>());
            allParticipants.Add(TeamType.Handball.ToString(), new ObservableCollection<Participant>());
            allParticipants.Add(TeamType.TennisDouble.ToString(), new ObservableCollection<Participant>());
            allParticipants.Add(TeamType.TennisSimple.ToString(), tennisCompetitors);

            // Possible team members
            allParticipants.Add(String.Concat(TeamType.Basketball.ToString(), "Participants"), new ObservableCollection<Participant>());
            allParticipants.Add(String.Concat(TeamType.Football.ToString(), "Participants"), new ObservableCollection<Participant>());
            allParticipants.Add(String.Concat(TeamType.Handball.ToString(), "Participants"), new ObservableCollection<Participant>());
            allParticipants.Add(String.Concat(TeamType.TennisDouble.ToString(), "Participants"), tennisCompetitors);

            return allParticipants;
        }

        public void SaveAppData(ObservableCollection<Sport> appData)
        {
            throw new NotImplementedException();
        }

        public void SaveParticipants(Dictionary<string, ObservableCollection<Participant>> participants)
        {
            throw new NotImplementedException();
        }
    }
}
