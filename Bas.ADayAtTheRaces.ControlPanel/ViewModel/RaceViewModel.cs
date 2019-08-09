using Bas.ADayAtTheRaces.ControlPanel.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public class RaceViewModel : ViewModelBase
    {
        private string time;

        public string Time
        {
            get { return time; }
            set { Set(ref this.time, value); }
        }

        public ObservableCollection<HorseViewModel> Horses { get; } = new ObservableCollection<HorseViewModel>();

        public RaceViewModel(Race race)
        {
            Time = race.Time.ToString();

            foreach (var horse in race.Horses)
            {
                Horses.Add(new HorseViewModel(horse, race.HorseColors[horse]));
            }
        }

        public RaceViewModel(TimeSpan time)
        {
            Time = time.ToString();
        }
    }
}
