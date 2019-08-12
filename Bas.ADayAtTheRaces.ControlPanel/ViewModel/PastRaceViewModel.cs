using Bas.ADayAtTheRaces.ControlPanel.Services;
using Bas.ADayAtTheRaces.RaceResults;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public sealed class PastRaceViewModel : ViewModelBase
    {
        private string time;

        public string Time
        {
            get { return time; }
            set { Set(ref this.time, value); }
        }
        
        public ObservableCollection<FinishedHorseViewModel> FinishedHorses { get; } = new ObservableCollection<FinishedHorseViewModel>();

        public PastRaceViewModel(RaceResult raceResult)
        {
            this.time = raceResult.RaceTime.ToString();

            foreach (var finish in raceResult.Finishes)
            {
                FinishedHorses.Add(new FinishedHorseViewModel(finish.HorseName, finish.JockeyColor, finish.Position, finish.TotalTime));
            }
        }
    }
}
