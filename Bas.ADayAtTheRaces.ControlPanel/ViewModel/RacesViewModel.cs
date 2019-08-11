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
    public sealed class RacesViewModel : ADayAtTheRacesViewModel
    {
        public RacesViewModel(IDataService dataService)
            : base(dataService)
        {
        }

        public ObservableCollection<PastRaceViewModel> PastRaces { get; } = new ObservableCollection<PastRaceViewModel>();

        private RaceViewModel currentRace;

        public RaceViewModel CurrentRace
        {
            get { return currentRace; }
            set { Set(ref this.currentRace, value);}
        }

        public ObservableCollection<UpcomingRaceViewModel> UpcomingRaces { get; } = new ObservableCollection<UpcomingRaceViewModel>();

        protected override void Update()
        {
            PastRaces.Clear();
            foreach (var raceResult in this.DataService.GetRaceResults())
            {
                PastRaces.Add(new PastRaceViewModel(raceResult));
            }

            UpcomingRaces.Clear();
            foreach (var race in this.DataService.GetRaces())
            {
                if (race.Time > DateTime.Now.TimeOfDay)
                {
                    UpcomingRaces.Add(new UpcomingRaceViewModel(race));
                }
            }
        }
    }
}
