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
        public Dictionary<string, LapSpeedModifier> HorseSpeedModifiers { get; private set; } = new Dictionary<string, LapSpeedModifier>();

        [DataMember]
        public DateTime Time { get; set; }

        /// <summary>
        /// Constructs a race without horses, speed modifiers or jockeycolors that starts at the set <paramref name="time"/>.
        /// </summary>
        /// <param name="time">The time at which the race starts.</param>
        public Race(DateTime time)
        {
            Time = time;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="race"></param>
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

            foreach (var horseSpeedModifiersKey in race.HorseSpeedModifiers.Keys)
            {
                HorseSpeedModifiers.Add(horseSpeedModifiersKey, race.HorseSpeedModifiers[horseSpeedModifiersKey]);
            }
        }

        public override string ToString() => $"Race at {Time.ToString()}";
    }
}
