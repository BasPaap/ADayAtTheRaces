using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class HorseExtensions
    {
        public static Color ToUnityColor(this Bas.ADayAtTheRaces.Color color)
        {
            return new Color(color.Red / 255.0f, color.Green / 255.0f, color.Blue / 255.0f);
        }
    }
}
