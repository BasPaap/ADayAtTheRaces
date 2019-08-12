using Bas.ADayAtTheRaces.ControlPanel.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public sealed class UpcomingRaceViewModel : ViewModelBase
    {
        private readonly Race originalRace;

        private string time;

        public string Time
        {
            get { return time; }
            set { Set(ref this.time, value); }
        }

        public ObservableCollection<UpcomingHorseViewModel> UpcomingHorses { get; } = new ObservableCollection<UpcomingHorseViewModel>();

        public UpcomingRaceViewModel(Race race)
        {
            this.originalRace = race;

            Time = race.Time.ToString();

            foreach (var horse in race.Horses)
            {
                UpcomingHorses.Add(new UpcomingHorseViewModel(horse.Name, race.JockeyColors[horse.Name], race.HorseSpeeds[horse.Name]));
            }
        }

        public Race GetRace()
        {
            var modifiedRace = new Race(originalRace);

            foreach (var upcomingHorse in UpcomingHorses)
            {
                modifiedRace.HorseSpeeds[upcomingHorse.Name] = new Speeds()
                {
                    FirstLapSpeed = upcomingHorse.FirstLapSpeed,
                    SecondLapSpeed = upcomingHorse.SecondLapSpeed
                };
            }

            return modifiedRace;
        }
    }
}
