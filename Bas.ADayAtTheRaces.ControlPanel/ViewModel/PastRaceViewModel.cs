using Bas.ADayAtTheRaces.ControlPanel.Services;
using Bas.ADayAtTheRaces.RaceResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public sealed class PastRaceViewModel : RaceViewModel
    {
        public ObservableCollection<FinishedHorseViewModel> FinishedHorses { get; } = new ObservableCollection<FinishedHorseViewModel>();

        public PastRaceViewModel(RaceResult raceResult)
            : base(raceResult.RaceTime.TimeOfDay)
        {
            foreach (var finish in raceResult.Finishes)
            {
                FinishedHorses.Add(new FinishedHorseViewModel(finish.Position, finish.TotalTime, finish.HorseName, finish.JockeyColor));
            }
        }
    }
}
