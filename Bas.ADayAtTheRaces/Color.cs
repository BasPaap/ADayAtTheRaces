using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces
{
    public sealed class Color
    {
        public float Red { get; set; }
        public float Green { get; set; }
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
