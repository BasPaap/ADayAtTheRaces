using Bas.ADayAtTheRaces.ControlPanel.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public class HorseViewModel : ViewModelBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { Set(ref this.name, value); }
        }

        public SolidColorBrush BackgroundColor { get; set; }
        public SolidColorBrush ForegroundColor { get; private set; }


        public HorseViewModel(string name, Color jockeyColor)
        {
            Name = name;
            var color = System.Windows.Media.Color.FromRgb((byte)jockeyColor.Red, (byte)jockeyColor.Green, (byte)jockeyColor.Blue);
            BackgroundColor = new SolidColorBrush(color);

            var perceivedBrightness = (int)Math.Sqrt(
                color.R * color.R * .241 +
                color.G * color.G * .691 +
                color.B * color.B * .068);

            const int brightnessCutoffValue = 130;
            ForegroundColor = new SolidColorBrush(perceivedBrightness > brightnessCutoffValue ? Colors.Black : Colors.White);
        }
    }

}
