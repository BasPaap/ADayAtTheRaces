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
            set { Set(ref this.name, value);}
        }

        private SolidColorBrush color;

        public SolidColorBrush Color
        {
            get { return color; }
            set { Set(ref this.color, value); }
        }
        
        public HorseViewModel(string name, Color color)
        {
            Name = name;
            Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)color.Red, (byte)color.Green, (byte)color.Blue));
        }
    }

}
