using Bas.ADayAtTheRaces.ControlPanel.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public abstract class ADayAtTheRacesViewModel : ViewModelBase
    {
        protected IDataService DataService { get; set; }

        public ADayAtTheRacesViewModel(IDataService dataService)
        {
            this.DataService = dataService;
            this.DataService.Updated += DataService_Updated;
            Update();
        }

        private void DataService_Updated(object sender, EventArgs e)
        {
            Update();
        }

        protected virtual void Update()
        {
        }
    }
}
