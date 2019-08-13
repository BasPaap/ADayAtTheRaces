using Bas.ADayAtTheRaces.RaceResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.Services
{
    public interface IDataService
    {
        /// <summary>
        /// Returns results from earlier races.
        /// </summary>
        /// <returns>Results from earlier races.</returns>
        Collection<RaceResult> GetRaceResults();

        /// <summary>
        /// Returns all planned races.
        /// </summary>
        /// <returns>All planned races.</returns>
        Collection<Race> GetRaces();

        /// <summary>
        /// Returns all horses.
        /// </summary>
        /// <returns>All horses.</returns>
        Collection<Horse> GetHorses();

        /// <summary>
        /// Saves modified races to the configuration.
        /// </summary>
        /// <param name="races">All races that should be saved.</param>
        void SaveRaces(IEnumerable<Race> races);

        /// <summary>
        /// Notifies subscribers when any relevant data has been updated.
        /// </summary>
        event EventHandler DataUpdated;
    }
}
