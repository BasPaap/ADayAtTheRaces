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
        public Dictionary<string, Color> JockeyColors { get; private set; } = new Dictionary<string, Color>();

        [DataMember]
        public Dictionary<string, Speeds> HorseSpeeds { get; private set; } = new Dictionary<string, Speeds>();

        [DataMember]
        public DateTime Time { get; set; }

        public Race(DateTime time)
        {
            Time = time;
        }

        public Race(Race race)
            : this(race.Time)
        {
            foreach (var horse in race.Horses)
            {
                Horses.Add(horse);
            }

            foreach (var jockeyColorKey in race.JockeyColors.Keys)
            {
                JockeyColors.Add(jockeyColorKey, race.JockeyColors[jockeyColorKey]);
            }

            foreach (var horseSpeedsKey in race.HorseSpeeds.Keys)
            {
                HorseSpeeds.Add(horseSpeedsKey, race.HorseSpeeds[horseSpeedsKey]);
            }
        }

        public override string ToString() => $"Race at {Time.ToString()}";
    }
}
