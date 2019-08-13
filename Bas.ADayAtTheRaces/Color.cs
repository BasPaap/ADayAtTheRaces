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
        public byte Red { get; set; }
        [DataMember]
        public byte Green { get; set; }
        [DataMember]
        public byte Blue { get; set; }

        /// <summary>
        /// Constructs the color with the provided <paramref name="red"/>, <paramref name="green"/> and <paramref name="blue"/> values.
        /// </summary>
        /// <param name="red">The value of the red component.</param>
        /// <param name="green">The value of the green component.</param>
        /// <param name="blue">The value of the blue component</param>
        public Color(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public override string ToString() => $"({Red}, {Green}, {Blue})";
    }
}
