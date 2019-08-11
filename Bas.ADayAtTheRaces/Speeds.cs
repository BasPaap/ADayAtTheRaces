using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces
{
    [DataContract]
    public sealed class Speeds
    {
        [DataMember]
        public float FirstLapSpeed { get; set; }

        [DataMember]
        public float SecondLapSpeed { get; set; }
    }
}
