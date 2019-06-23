using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces
{
    public sealed class Horse
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public float ReactionSpeed { get; set; }
        public float Reliability { get; set; }
        public Collection<RunningPhase> RunningPhases { get; private set; } = new Collection<RunningPhase>();

        public override string ToString() => $"{Name} ({Color})";
    }
}
