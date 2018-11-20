using Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Loads default data to test the application
    /// </summary>
    public class StubDataManager : IDataManager
    {
        public ObservableCollection<Sport> LoadAppData()
        {
            ObservableCollection<Sport> data = new ObservableCollection<Sport>();

            Sport basketball   = new Sport(TeamType.Basketball, new Uri("Resources;Component/Sports/basketball_icon.png", UriKind.Relative));
            Sport football     = new Sport(TeamType.Football, new Uri("Resources;Component/Sports/football_icon.png", UriKind.Relative));
            Sport handball     = new Sport(TeamType.Handball, new Uri("Resources;Component/Sports/handball_icon.png", UriKind.Relative));
            Sport tennisDouble = new Sport(TeamType.TennisDouble, new Uri("Resources;Component/Sports/tennis_icon.png", UriKind.Relative));
            Sport tennisSimple = new Sport(TeamType.TennisSimple, new Uri("Resources;Component/Sports/tennis_icon.png", UriKind.Relative));

            Tournament basketallTournament    = BasketballTournament();
            Tournament footballTournament     = FootballTournament();
            Tournament handballTournament     = HandballTournament();
            Tournament tennisDoubleTournament = TennisDoubleTournament();
            Tournament tennisSimpleTournament = TennisSimpleTournament();

            basketball.AddComponent(basketallTournament);
            football.AddComponent(footballTournament);
            handball.AddComponent(handballTournament);
            tennisDouble.AddComponent(tennisDoubleTournament);
            tennisSimple.AddComponent(tennisSimpleTournament);

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
            tennisCompetitors.Add(new Competitor(TeamType.TennisSimple, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            tennisCompetitors.Add(new Competitor(TeamType.TennisSimple, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            tennisCompetitors.Add(new Competitor(TeamType.TennisSimple, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            tennisCompetitors.Add(new Competitor(TeamType.TennisSimple, "PARKER", "Tony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            tennisCompetitors.Add(new Competitor(TeamType.TennisSimple, "BRYANT", "Kobe", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            // Teams
            allParticipants.Add(TeamType.Basketball.ToString(), new ObservableCollection<Participant>());
            allParticipants.Add(TeamType.Football.ToString(), new ObservableCollection<Participant>());
            allParticipants.Add(TeamType.Handball.ToString(), new ObservableCollection<Participant>());
            allParticipants.Add(TeamType.TennisDouble.ToString(), new ObservableCollection<Participant>());
            allParticipants.Add(TeamType.TennisSimple.ToString(), tennisCompetitors);

            // Possible team members
            string teamType = String.Concat(TeamType.Basketball.ToString(), "Participants");
            ObservableCollection<Participant> basketballCompetitors = new ObservableCollection<Participant>();
            basketballCompetitors.Add(new Competitor(TeamType.Basketball, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            basketballCompetitors.Add(new Competitor(TeamType.Basketball, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            basketballCompetitors.Add(new Competitor(TeamType.Basketball, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            basketballCompetitors.Add(new Competitor(TeamType.Basketball, "PARKER", "Tony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            basketballCompetitors.Add(new Competitor(TeamType.Basketball, "BRYANT", "Kobe", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            allParticipants.Add(teamType, basketballCompetitors);

            teamType = String.Concat(TeamType.Football.ToString(), "Participants");
            ObservableCollection<Participant> footballCompetitors = new ObservableCollection<Participant>();
            footballCompetitors.Add(new Competitor(TeamType.Football, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            footballCompetitors.Add(new Competitor(TeamType.Football, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            footballCompetitors.Add(new Competitor(TeamType.Football, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            footballCompetitors.Add(new Competitor(TeamType.Football, "PARKER", "Tony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            footballCompetitors.Add(new Competitor(TeamType.Football, "BRYANT", "Kobe", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            allParticipants.Add(teamType, footballCompetitors);

            teamType = String.Concat(TeamType.Handball.ToString(), "Participants");
            ObservableCollection<Participant> handballCompetitors = new ObservableCollection<Participant>();
            handballCompetitors.Add(new Competitor(TeamType.Football, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            handballCompetitors.Add(new Competitor(TeamType.Football, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            handballCompetitors.Add(new Competitor(TeamType.Football, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            handballCompetitors.Add(new Competitor(TeamType.Football, "PARKER", "Tony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            handballCompetitors.Add(new Competitor(TeamType.Football, "BRYANT", "Kobe", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            allParticipants.Add(teamType, handballCompetitors);

            allParticipants.Add(String.Concat(TeamType.TennisDouble.ToString(), "Participants"), tennisCompetitors);



            return allParticipants;
        }

        #region Creation of various tournaments
        /// <summary>
        /// Creates a Basketball Tournament
        /// </summary>
        /// <returns>a Basketball Tournament</returns>
        public Tournament BasketballTournament()
        {
            TeamType teamType   = TeamType.Basketball;
            string name         = "NBA";
            string location     = "Los Angeles";
            DateTime date       = new DateTime(2017, 4, 10);

            List<Participant> list = new List<Participant>();

            Team team1 = new Team(TeamType.Basketball, "Lakers", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team1.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "PARKER", "Tony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "BRYANT", "Kobe", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team2 = new Team(TeamType.Basketball, "Chicago Bulls", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team2.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "PARKER", "Tony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "BRYANT", "Kobe", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team3 = new Team(TeamType.Basketball, "AS Toronto", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team3.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "PARKER", "Tony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "BRYANT", "Kobe", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            list.Add(team1);
            list.Add(team2);
            list.Add(team3);

            Tournament tournament = AbstractFactory.GetFactory(teamType).CreateTournament(teamType, name, location, date, new Uri("Resources;Component/Default/banner.png", UriKind.Relative));
            tournament.GenerateRounds(list);
            foreach (var item in list)
            {
                tournament.AddComponent(item);
            }

            return tournament;
        }

        /// <summary>
        /// Creates a Football Tournament
        /// </summary>
        /// <returns>a Football Tournament</returns>
        public Tournament FootballTournament()
        {
            TeamType teamType   = TeamType.Football;
            string name         = "Champions League";
            string location     = "Clermont-Ferrand";
            DateTime date       = new DateTime(2017, 5, 10);

            List<Participant> list = new List<Participant>();

            Team team1 = new Team(teamType, "G1", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team1.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "BLAY", "Adrien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "COSTA", "Anthony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team2 = new Team(teamType, "G2", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team2.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "BLAY", "Adrien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "COSTA", "Anthony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team3 = new Team(teamType, "G3", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team3.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "BLAY", "Adrien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "COSTA", "Anthony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team4 = new Team(teamType, "G4", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team4.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "BLAY", "Adrien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "COSTA", "Anthony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            list.Add(team1);
            list.Add(team2);
            list.Add(team3);
            list.Add(team4);

            Tournament tournament = AbstractFactory.GetFactory(teamType).CreateTournament(teamType, name, location, date, new Uri("Resources;Component/Default/banner.png", UriKind.Relative));
            tournament.GenerateRounds(list);
            foreach (var item in list)
            {
                tournament.AddComponent(item);
            }

            return tournament;
        }

        /// <summary>
        /// Creates a Handball Tournament
        /// </summary>
        /// <returns>a Handball Tournament</returns>
        public Tournament HandballTournament()
        {
            TeamType teamType   = TeamType.Handball;
            string name         = "European Championships";
            string location     = "Berlin";
            DateTime date       = new DateTime(2018, 3, 22);

            List<Participant> list = new List<Participant>();

            Team team1 = new Team(teamType, "G1", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team1.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team2 = new Team(teamType, "G2", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team2.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team3 = new Team(teamType, "G3", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team3.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team4 = new Team(teamType, "G4", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team4.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team5 = new Team(teamType, "G5", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team5.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team5.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team5.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team5.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team5.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team5.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team5.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team6 = new Team(teamType, "G6", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team6.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team6.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team6.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team6.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team6.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team6.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team6.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team7 = new Team(teamType, "G7", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team7.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team7.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team7.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team7.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team7.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team7.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team7.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team8 = new Team(teamType, "G8", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team8.AddParticipant(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team8.AddParticipant(new Competitor(teamType, "TORTI", "Clément", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team8.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team8.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team8.AddParticipant(new Competitor(teamType, "GOUNON", "Thomas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team8.AddParticipant(new Competitor(teamType, "BAVOUZET", "Maxime", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team8.AddParticipant(new Competitor(teamType, "BONHOMME", "Valentin", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            list.Add(team1);
            list.Add(team2);
            list.Add(team3);
            list.Add(team4);
            list.Add(team5);
            list.Add(team6);
            list.Add(team7);
            list.Add(team8);

            Tournament tournament = AbstractFactory.GetFactory(teamType).CreateTournament(teamType, name, location, date, new Uri("Resources;Component/Default/banner.png", UriKind.Relative));
            tournament.GenerateRounds(list);
            foreach (var item in list)
            {
                tournament.AddComponent(item);
            }

            return tournament;
        }

        /// <summary>
        /// Creates a Tennis (double) Tournament
        /// </summary>
        /// <returns>a Tennis (double) Tournament</returns>
        public Tournament TennisDoubleTournament()
        {
            TeamType teamType = TeamType.TennisDouble;
            string name       = "Roland-Garros";
            string location   = "France";
            DateTime date     = new DateTime(2018, 6 , 2);

            List<Participant> list = new List<Participant>();

            Team team1 = new Team(teamType, "Name1", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team1.AddParticipant(new Competitor(teamType, "NADAL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team1.AddParticipant(new Competitor(teamType, "FEDERER", "Roger", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team2 = new Team(teamType, "Name2", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team2.AddParticipant(new Competitor(teamType, "MURRAY", "Andy", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team2.AddParticipant(new Competitor(teamType, "RODDICK", "Andy", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team3 = new Team(teamType, "Name3", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team3.AddParticipant(new Competitor(teamType, "SAMPRAS", "Pete", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team3.AddParticipant(new Competitor(teamType, "AGASSI", "André", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team4 = new Team(teamType, "Name4", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team4.AddParticipant(new Competitor(teamType, "NISHIKORI", "Key", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team4.AddParticipant(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team5 = new Team(teamType, "Name5", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team5.AddParticipant(new Competitor(teamType, "KURKLU", "Fikret", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team5.AddParticipant(new Competitor(teamType, "DAVYDENKO", "Nikolay", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team6 = new Team(teamType, "Name6", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team6.AddParticipant(new Competitor(teamType, "THIEM", "Dominic", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team6.AddParticipant(new Competitor(teamType, "COSTA", "Anthony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team7 = new Team(teamType, "Name7", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team7.AddParticipant(new Competitor(teamType, "TSONGA", "Jo-Wilfried", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team7.AddParticipant(new Competitor(teamType, "MANNARINO", "Adrian", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team8 = new Team(teamType, "Name8", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team8.AddParticipant(new Competitor(teamType, "LENDL", "Ivan", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team8.AddParticipant(new Competitor(teamType, "GASQUET", "Richard", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team9 = new Team(teamType, "Name9", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team9.AddParticipant(new Competitor(teamType, "NAOUZ", "Mahir", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team9.AddParticipant(new Competitor(teamType, "BENAYOUN", "Alexis", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Team team10 = new Team(teamType, "Name10", new Uri("Resources;Component/Default/head.png", UriKind.Relative));
            team10.AddParticipant(new Competitor(teamType, "NAOUZ", "Dominique", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            team10.AddParticipant(new Competitor(teamType, "ESSADIK", "Mohammed", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            list.Add(team1);
            list.Add(team2);
            list.Add(team3);
            list.Add(team4);
            list.Add(team5);
            list.Add(team6);
            list.Add(team7);
            list.Add(team8);
            list.Add(team9);
            list.Add(team10);

            Tournament tournament = AbstractFactory.GetFactory(teamType).CreateTournament(teamType, name, location, date, new Uri("Resources;Component/Default/banner.png", UriKind.Relative));
            tournament.GenerateRounds(list);
            foreach (var item in list)
            {
                tournament.AddComponent(item);
            }

            return tournament;
        }

        /// <summary>
        /// Creates a Tennis (simple) Tournament
        /// </summary>
        /// <returns>a Tennis (simple) Tournament</returns>
        public Tournament TennisSimpleTournament()
        {
            TeamType teamType = TeamType.TennisSimple;
            string name       = "Wimbledon";
            string location   = "Angleterre";
            DateTime date     = new DateTime(2018, 8 , 8);

            List<Participant> list = new List<Participant>();

            list.Add(new Competitor(teamType, "NADAL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "FEDERER", "Roger", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "MURRAY", "Andy", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "RODDICK", "Andy", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "SAMPRAS", "Pete", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "AGASSI", "André", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "NISHIKORI", "Key", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "NGUYEN", "Damien", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "ROUSEYROL", "Raphaël", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "DAVYDENKO", "Nikolay", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "THIEM", "Dominic", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "COSTA", "Anthony", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "DJOKOVIC", "Novak", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "SEPPI", "Andreas", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "ZVEREV", "Alexander", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));
            list.Add(new Competitor(teamType, "PIOLINE", "Cédric", new Uri("Resources;Component/Default/head.png", UriKind.Relative)));

            Tournament tournament = AbstractFactory.GetFactory(teamType).CreateTournament(teamType, name, location, date, new Uri("Resources;Component/Default/banner.png", UriKind.Relative));
            tournament.GenerateRounds(list);
            foreach (var item in list)
            {
                tournament.AddComponent(item);
            }

            return tournament;
        }
        #endregion

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
