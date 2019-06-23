using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces
{
    public sealed class RunningPhase
    {
        public float Speed { get; set; }
        public TimeSpan Duration { get; set; }

        public RunningPhase(double durationInSeconds, float speed)
        {
            Speed = speed;
            Duration = TimeSpan.FromSeconds(durationInSeconds);
        }

        public override string ToString() => $"{Speed} for {Duration.TotalSeconds.ToString(CultureInfo.InvariantCulture)} seconds";
    }
}
