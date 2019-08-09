using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bas.ADayAtTheRaces.RaceResults;

namespace Bas.ADayAtTheRaces.ControlPanel.Services
{
    public sealed class DataService : IDataService
    {
        public event EventHandler Updated;

        public Collection<RaceResult> GetRaceResults()
        {
            var raceResultsFile = new RaceResultsFile();
            raceResultsFile.Load(Environment.ExpandEnvironmentVariables(Properties.Settings.Default.RaceResultsFilePath));

            return raceResultsFile.RaceResults;
        }

        public Collection<Horse> GetHorses()
        {
            var configuration = LoadConfiguration();
            return configuration.Horses;            
        }

        public Collection<Race> GetRaces()
        {
            var configuration = LoadConfiguration();
            return configuration.Races;
        }

        private ADayAtTheRacesConfiguration LoadConfiguration()
        {
            var configuration = new ADayAtTheRacesConfiguration();
            configuration.Load(Environment.ExpandEnvironmentVariables(Properties.Settings.Default.ConfigurationFilePath));
            return configuration;
        }
    }
}
