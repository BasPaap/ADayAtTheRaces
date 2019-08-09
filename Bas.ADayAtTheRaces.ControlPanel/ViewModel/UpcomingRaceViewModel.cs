using Bas.ADayAtTheRaces.ControlPanel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public sealed class UpcomingRaceViewModel : RaceViewModel
    {
        public ObservableCollection<UpcomingHorseViewModel> UpcomingHorses { get; } = new ObservableCollection<UpcomingHorseViewModel>();

        public UpcomingRaceViewModel(Race race)
            : base(race)
        {
            foreach (var horse in race.Horses)
            {
                UpcomingHorses.Add(new UpcomingHorseViewModel(horse, race.HorseSpeeds[horse]));
            }
        }
    }
}
