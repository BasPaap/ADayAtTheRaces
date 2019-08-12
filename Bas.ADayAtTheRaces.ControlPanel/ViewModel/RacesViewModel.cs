using Bas.ADayAtTheRaces.ControlPanel.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
            SaveCommand = new RelayCommand(() =>
            {
                var races = from u in UpcomingRaces
                            select u.GetRace();

                this.DataService.SaveRaces(races);
            });
        }

        public ObservableCollection<PastRaceViewModel> PastRaces { get; } = new ObservableCollection<PastRaceViewModel>();
        
        public ObservableCollection<UpcomingRaceViewModel> UpcomingRaces { get; } = new ObservableCollection<UpcomingRaceViewModel>();

        public RelayCommand SaveCommand { get; private set; } 

        protected override void Update()
        {
            PastRaces.Clear();
            foreach (var raceResult in this.DataService.GetRaceResults())
            {
                PastRaces.Add(new PastRaceViewModel(raceResult));
            }

            if (PastRaces.Count > 0)
            {
                PastRaces.Last().IsExpanded = true;
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
