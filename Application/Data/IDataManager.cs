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
    /// Loads and saves the data of the Application
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Gets all the saved or default data for the application
        /// </summary>
        /// <returns>an ObservableCollection with the needed data</returns>
        ObservableCollection<Sport> LoadAppData();

        /// <summary>
        /// Gets all the saved or default Participant data for the application
        /// </summary>
        /// <returns>an ObservableCollection with the needed Participant data</returns>
        Dictionary<string, ObservableCollection<Participant>> LoadParticipants();

        /// <summary>
        /// Saves the main data of the application
        /// </summary>
        /// <param name="appData">the main data</param>
        /// <returns>true if the data was succesfully saved, false otherwise</returns>
        void SaveAppData(ObservableCollection<Sport> appData);

        /// <summary>
        /// Saves the participants data
        /// </summary>
        /// <param name="participants">the participants data</param>
        /// <returns>true if the data was succesfully saved, false otherwise</returns>
        void SaveParticipants(Dictionary<string, ObservableCollection<Participant>> participants);
    }
}
