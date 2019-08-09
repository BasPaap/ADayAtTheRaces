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
            throw new NotImplementedException();
        }

        public Collection<Horse> GetHorses()
        {
            throw new NotImplementedException();
        }

        public Collection<Race> GetRaces()
        {
            throw new NotImplementedException();
        }
    }
}
