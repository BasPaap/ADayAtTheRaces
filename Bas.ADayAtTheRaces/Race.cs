using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces
{
    [DataContract]
    public sealed class Race
    {
        [DataMember]
        public Collection<Horse> Horses { get; private set; } = new Collection<Horse>();

        [DataMember]
        public Dictionary<Horse, (float firstLapSpeed, float secondLapSpeed)> HorseSpeeds { get; private set; } = new Dictionary<Horse, (float firstLapSpeed, float secondLapSpeed)>();

        [DataMember]
        public TimeSpan Time { get; set; }

        public Race(int hours, int minutes, int seconds)
        {
            Time = new TimeSpan(hours, minutes, seconds);
        }

        public override string ToString() => $"Race at {Time.ToString()}";
    }
}
