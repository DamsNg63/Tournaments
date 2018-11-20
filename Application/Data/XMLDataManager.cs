using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Windows;

namespace Data
{
    /// <summary>
    /// Loads data from an XML backup
    /// </summary>
    public class XMLDataManager : IDataManager
    {
        #region Serializers
        private DataContractSerializer tournamentsSerializer  = new DataContractSerializer(typeof(ObservableCollection<Sport>));
        private DataContractSerializer participantsSerializer = new DataContractSerializer(typeof(Dictionary<string, ObservableCollection<Participant>>));
        #endregion

        #region Default backup folder and file names
        public string DefaultBackupFolder { get; private set; }
            = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "tournaments_backups");

        public string TournamentsXMLFile { get; private set; } = "tournaments_backup.xml";
        public string ParticipantsXMLFile { get; private set; } = "participants_backup.xml"; 
        #endregion

        private XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, IndentChars = "\t" };

        #region Loaders
        public ObservableCollection<Sport> LoadAppData()
        {
            ObservableCollection<Sport> sportsListData = null;
            using (Stream reader = File.OpenRead(TournamentsXMLFile))
            {
                sportsListData = tournamentsSerializer.ReadObject(reader) as ObservableCollection<Sport>;
            }

            return sportsListData;
        }

        public Dictionary<string, ObservableCollection<Participant>> LoadParticipants()
        {
            Dictionary<string, ObservableCollection<Participant>> participantsList = null;
            using (Stream reader = File.OpenRead(ParticipantsXMLFile))
            {
                participantsList = participantsSerializer.ReadObject(reader) as Dictionary<string, ObservableCollection<Participant>>;
            }

            return participantsList;
        }
        #endregion

        #region Savers
        public void SaveAppData(ObservableCollection<Sport> appData)
        {
            using (XmlWriter writer = XmlWriter.Create(TournamentsXMLFile, settings))
            {
                tournamentsSerializer.WriteObject(writer, appData);
            }
        }

        public void SaveParticipants(Dictionary<string, ObservableCollection<Participant>> participants)
        {
            using (XmlWriter writer = XmlWriter.Create(ParticipantsXMLFile, settings))
            {
                participantsSerializer.WriteObject(writer, participants);
            }
        } 
        #endregion
    }
}
