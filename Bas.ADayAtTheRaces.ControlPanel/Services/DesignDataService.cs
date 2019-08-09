using Bas.ADayAtTheRaces.RaceResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.Services
{
    public sealed class DesignDataService : IDataService
    {
        private readonly ADayAtTheRacesConfiguration configurationFile = new ADayAtTheRacesConfiguration();
        private readonly RaceResultsFile raceResultsFile = new RaceResultsFile();

        public DesignDataService()
        {            
            configurationFile.Populate();
            raceResultsFile.Populate();
        }

        public event EventHandler Updated;

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
    }
}
