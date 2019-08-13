using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces
{
    [DataContract]
    public sealed class RunningPhase
    {
        [DataMember]
        public float Speed { get; set; }
        [DataMember]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Constructs a running phase where the horse runs at <paramref name="speed"/> for <paramref name="durationInSeconds"/> seconds.
        /// </summary>
        /// <param name="durationInSeconds">Duration of the running phase, in seconds.</param>
        /// <param name="speed">Relative speed of the horse, a value between 0 and 1.</param>
        public RunningPhase(double durationInSeconds, float speed)
        {
            Speed = speed;
            Duration = TimeSpan.FromSeconds(durationInSeconds);
        }

        public override string ToString() => $"{Speed} for {Duration.TotalSeconds.ToString(CultureInfo.InvariantCulture)} seconds";
    }
}
