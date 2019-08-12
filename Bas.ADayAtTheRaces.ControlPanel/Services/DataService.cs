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
        private readonly FileSystemWatcher raceResultsFileSystemWatcher = new FileSystemWatcher();
        private readonly FileSystemWatcher configurationFileSystemWatcher = new FileSystemWatcher();

        public DataService()
        {
            InitializeFileWatcher(Properties.Settings.Default.RaceResultsFilePath, this.raceResultsFileSystemWatcher);
            InitializeFileWatcher(Properties.Settings.Default.ConfigurationFilePath, this.configurationFileSystemWatcher);
        }
        
        private void InitializeFileWatcher(string filePath, FileSystemWatcher watcher)
        {
            var expandedRaceResultsFilePath = Environment.ExpandEnvironmentVariables(filePath);

            if (!Directory.Exists(Path.GetDirectoryName(expandedRaceResultsFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(expandedRaceResultsFilePath));
            }

            watcher.Path = Path.GetDirectoryName(expandedRaceResultsFilePath);
            watcher.Filter = Path.GetFileName(expandedRaceResultsFilePath);
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            watcher.Changed += FileSystemWatcher_Changed;
            watcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Updated?.Invoke(this, EventArgs.Empty);            
        }

        public Collection<RaceResult> GetRaceResults()
        {
            var raceResultsFile = new RaceResultsFile();

            try
            {
                raceResultsFile.Load(Environment.ExpandEnvironmentVariables(Properties.Settings.Default.RaceResultsFilePath));
            }
            catch (FileNotFoundException)
            {
                // ignore this, the file will be loaded when the filesystemwatcher notices that it has been created.
            }
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

            try
            {
                configuration.Load(Environment.ExpandEnvironmentVariables(Properties.Settings.Default.ConfigurationFilePath));
            }
            catch (FileNotFoundException)
            {
                // ignore this, the file will be loaded when the filesystemwatcher notices that it has been created.
            }

            return configuration;
        }

        private void SaveConfiguration(ADayAtTheRacesConfiguration configuration)
        {
            configuration.Save(Environment.ExpandEnvironmentVariables(Properties.Settings.Default.ConfigurationFilePath));            
        }


        public void SaveRaces(IEnumerable<Race> races)
        {
            var configuration = LoadConfiguration();
            configuration.Races.Clear();

            foreach (var race in races)
            {
                configuration.Races.Add(race);
            }

            SaveConfiguration(configuration);
        }
    }
}
