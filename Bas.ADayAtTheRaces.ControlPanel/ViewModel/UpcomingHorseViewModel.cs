using Bas.ADayAtTheRaces.ControlPanel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public class UpcomingHorseViewModel : HorseViewModel
    {
        private float firstLapSpeed;

        public const float MinSpeed = 0.5f;
        public const float MaxSpeed = 1.5f;
        
        public float FirstLapSpeed
        {
            get { return firstLapSpeed; }
            set { Set(ref this.firstLapSpeed, Clamp(value, MinSpeed, MaxSpeed)); }
        }

        private float secondLapSpeed;

        public float SecondLapSpeed
        {
            get { return secondLapSpeed; }
            set { Set(ref this.secondLapSpeed, Clamp(value, MinSpeed, MaxSpeed)); }
        }

        private float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }

        public UpcomingHorseViewModel(Horse horse, (float firstLapSpeed, float secondLapSpeed) speeds)
            : base(horse)
        {
            FirstLapSpeed = speeds.firstLapSpeed;
            SecondLapSpeed = speeds.secondLapSpeed;
        }
    }
}
