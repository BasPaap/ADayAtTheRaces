using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.RaceResults
{
    /// <summary>
    /// Describes the moment a horse finishes, including the name of the horse, its position, the horse's total time, etc.
    /// </summary>
    [DataContract]
    public sealed class Finish
    {
        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public TimeSpan TotalTime { get; set; }

        [DataMember]
        public string HorseName { get; set; }

        [DataMember]
        public Color JockeyColor { get; set; }
    }
}
