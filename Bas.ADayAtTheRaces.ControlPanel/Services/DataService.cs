using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bas.ADayAtTheRaces.RaceResults;

namespace Bas.ADayAtTheRaces.ControlPanel.Services
{
    public sealed class DataService : IDataService
    {
        public event EventHandler Updated;
        private readonly FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

        public DataService()
        {
            this.fileSystemWatcher.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "A Day At The Races");
            this.fileSystemWatcher.Filter = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.RaceResultsFilePath);
            this.fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
            this.fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            this.fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Updated?.Invoke(this, EventArgs.Empty);            
        }

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
