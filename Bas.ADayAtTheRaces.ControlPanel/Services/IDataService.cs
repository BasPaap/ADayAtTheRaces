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
        Collection<RaceResult> GetRaceResults();
        Collection<Race> GetRaces();
        Collection<Horse> GetHorses();
        void SaveRaces(IEnumerable<Race> races);

        event EventHandler Updated;
    }
}
