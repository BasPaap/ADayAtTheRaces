using Bas.ADayAtTheRaces.RaceResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.Services
{
    /// <summary>
    /// Design-time data for use in Visual Studio and Blender designers.
    /// </summary>
    public sealed class DesignDataService : IDataService
    {
        private readonly ADayAtTheRacesConfiguration configurationFile = new ADayAtTheRacesConfiguration();
        private readonly RaceResultsFile raceResultsFile = new RaceResultsFile();

        public DesignDataService()
        {            
            configurationFile.Populate();
            raceResultsFile.Populate();
        }

        // This event is not used at design time, so we'll make it explicit to avoid compiler warnings, 
        // https://blogs.msdn.microsoft.com/trevor/2008/08/14/c-warning-cs0067-the-event-event-is-never-used/
        public event EventHandler DataUpdated
        {
            add { }
            remove { }
        }

        public Collection<RaceResult> GetRaceResults()
        {
            return this.raceResultsFile.RaceResults;
        }

        public Collection<Horse> GetHorses()
        {
            return this.configurationFile.Horses;
        }

        public Collection<Race> GetRaces()
        {
            return this.configurationFile.Races;
        }

        public void SaveRaces(IEnumerable<Race> races)
        {
            throw new NotImplementedException();
        }
    }
}
