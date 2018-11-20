using Business;
using Data;
using Facade;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

using static System.Console;

namespace BusinessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();

            #region Get the first football Tournament and display it as generated
            WriteLine("===== BEGINNING THE FIRST FOOTBALL TOURNAMENT =====");
            Tournament footTourney
                = manager.SportsListData.Where(n => n.Type.Equals(TeamType.Football)).FirstOrDefault().ReadOnlyComponents.FirstOrDefault() as Tournament;
            WriteLine(footTourney);
            WriteLine($"The winner: {footTourney?.Winner}");
            ReadKey();
            Clear();
            #endregion

            #region Get the first Match and add a Highlight to it
            WriteLine("===== ADDING A HIGHLIGHT FOR THE FIRST MATCH =====");
            Match match = (footTourney.ReadOnlyComponents.FirstOrDefault() as Round).ReadOnlyComponents.FirstOrDefault() as Match;
            match.AddComponent(new Highlight("Superbe match en perspective !"));
            WriteLine(footTourney);
            WriteLine($"The winner: {footTourney.Winner}");
            ReadKey();
            Clear();
            #endregion

            #region Move the winners to the next Round (uses the AddScoreToParticipant() method)
            WriteLine("===== MOVING WINNERS TO THE NEXT ROUND =====");
            footTourney.CarryOn();
            WriteLine(footTourney);
            WriteLine($"The winner: {footTourney.Winner}");
            ReadKey();
            Clear();
            #endregion

            #region Move the winners to the next Round until there is a winner
            WriteLine("===== MOVING WINNERS TO THE NEXT ROUND UNTIL THERE IS A WINNER =====");
            while (footTourney.Winner == null)
            {
                footTourney.CarryOn();
            }
            WriteLine(footTourney);
            WriteLine($"The winner: {footTourney.Winner}");
            ReadKey();
            Clear();
            #endregion

            #region Create a new Tournament using the manager
            WriteLine("===== CREATING A NEW TOURNAMENT USING THE MANAGER =====");
            TeamType type = TeamType.TennisSimple;
            Uri defaultPic = new Uri("Data;Component/Images/tete.jpg", UriKind.Relative);

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    manager.CreateCompetitor(type.ToString(), $"NOM{i}", $"Prenom{i}", defaultPic, type.ToString());
                }

                manager.CreateTournament(type.ToString(), "Tourney", "There", DateTime.Now, defaultPic, manager.ParticipantsList[type.ToString()].Where(n => n.Type.Equals(type)).Cast<Participant>().ToList());
            }
            catch (Exception)
            {
                WriteLine($"Déjà chargé depuis la sauvegarde XML.");
            }
            Tournament tennisTourney
                = manager.SportsListData.Where(n => n.Type.Equals(type)).Single().ReadOnlyComponents
                    .Where(n => (n as Tournament).Name.Equals("Tourney")).Single() as Tournament;
            WriteLine(tennisTourney);
            WriteLine($"The winner: {tennisTourney.Winner}");
            ReadKey();
            Clear();
            #endregion

            #region Save the data to and load them from the default backup folder (~/MyDocuments/tournaments_backups)
            WriteLine("===== SAVING ALL THE DATA =====");
            manager.SaveData(manager.DefaultBackupFolder);
            ReadKey();

            WriteLine("===== LOADING THE DATA =====");
            manager.LoadData(manager.DefaultBackupFolder);
            tennisTourney = manager.SportsListData.Where(n => n.Type.Equals(type)).Single().ReadOnlyComponents
                    .Where(n => (n as Tournament).Name.Equals("Tourney")).Single() as Tournament;
            WriteLine(tennisTourney);
            WriteLine($"The winner: {tennisTourney.Winner}");
            ReadKey();
            Clear();
            #endregion

            #region Test our personal event and Easter Egg
            MagicNameDetecter detecter = new MagicNameDetecter();
            detecter.MagicNameDetected += Detecter_MagicNameDetected;

            WriteLine("===== TESTING OUR PERSONAL EVENT =====");
            Write("Input last name: ");
            string lastName = ReadLine();
            string firstName;

            detecter.Detect(lastName, out firstName);
            manager.CreateCompetitor("TennisSimple", lastName, firstName ?? "Default", null, "TennisSimple");

            Competitor createdCompetitor = manager.ParticipantsList["TennisSimple"].Where(n => n.Name.Equals(lastName.ToUpper())).Single() as Competitor;
            WriteLine($"Created user: {createdCompetitor.Name} {createdCompetitor.FirstName}");
            ReadKey();
            Clear();
            #endregion

            #region Delete the first Tennis Simple Tournament
            WriteLine("===== DELETING THE FIRST TENNIS SIMPLE TOURNAMENT =====");
            var tennisSimple = manager.SportsListData.Where(n => n.Type.Equals(TeamType.TennisSimple)).Single();
            WriteLine($"Number of tennis tournaments now: {tennisSimple.ReadOnlyComponents.Count}");
            ReadKey();
            tennisSimple.RemoveComponent((tennisSimple as Sport).ReadOnlyComponents.First());
            WriteLine($"\nNumber of tennis tournaments after deletion: {tennisSimple.ReadOnlyComponents.Count}"); 
            #endregion


            // À ne pas effacer
            ReadKey();
        }

        /// <summary>
        /// Writes a message in the console when the magic name is detected
        /// </summary>
        /// <param name="sender">the object that raised the event</param>
        /// <param name="args">the event args</param>
        private static void Detecter_MagicNameDetected(object sender, MagicNameDetectedEventArgs args)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine(args.Message);
            ForegroundColor = ConsoleColor.White;
        }
    }
}
