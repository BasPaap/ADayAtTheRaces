using Bas.ADayAtTheRaces.ControlPanel.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public sealed class FinishedHorseViewModel : HorseViewModel
    {
        private int position;

        public int Position
        {
            get { return position; }
            set { Set(ref this.position, value); }
        }

        private string time;

        public string Time
        {
            get { return time; }
            set { Set(ref this.time, value); }
        }

        public FinishedHorseViewModel(Horse horse, Color color)
            : base(horse, color)
        {
        }

        public FinishedHorseViewModel(int position, TimeSpan time, string name, Color color)
            : base(name, color)
        {
            Position = position;
            Time = time.ToString();
        }
    }
}
