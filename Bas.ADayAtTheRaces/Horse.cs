using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces
{
    [DataContract]
    public sealed class Horse
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public float ReactionSpeed { get; set; }
        [DataMember]
        public float Reliability { get; set; }
        [DataMember]
        public Collection<RunningPhase> RunningPhases { get; private set; } = new Collection<RunningPhase>();

        public override string ToString() => $"{Name}";
    }
}
