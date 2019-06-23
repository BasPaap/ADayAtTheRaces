using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces
{
    [DataContract]
    public sealed class Color
    {
        [DataMember]
        public float Red { get; set; }
        [DataMember]
        public float Green { get; set; }
        [DataMember]
        public float Blue { get; set; }

        public Color(float red, float green, float blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public override string ToString() => $"({Red}, {Green}, {Blue})";
    }
}
