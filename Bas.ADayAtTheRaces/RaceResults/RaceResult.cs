using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.RaceResults
{
    [DataContract]
    public sealed class RaceResult
    {
        [DataMember]
        public Collection<Finish> Finishes { get; private set; } = new Collection<Finish>();

        [DataMember]
        public DateTime RaceTime { get; set; } 
    }
}
