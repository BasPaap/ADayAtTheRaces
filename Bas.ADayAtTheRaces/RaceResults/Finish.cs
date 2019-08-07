using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.RaceResults
{
    [DataContract]
    public sealed class Finish
    {
        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public TimeSpan TotalTime { get; set; }

        [DataMember]
        public string HorseName { get; set; }
    }
}
