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
        public TimeSpan Time { get; set; }

        public Race(int hours, int minutes)
        {
            Time = new TimeSpan(hours, minutes, 0);
        }

        public override string ToString() => $"Race at {Time.ToString()}";
    }
}
