using Bas.ADayAtTheRaces.ControlPanel.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Bas.ADayAtTheRaces.ControlPanel.ViewModel
{
    public abstract class UpdatableViewModel : ViewModelBase
    {
        protected IDataService DataService { get; set; }

        public UpdatableViewModel(IDataService dataService)
        {
            this.DataService = dataService;
            this.DataService.DataUpdated += DataService_Updated;
            Update();
        }

        private void DataService_Updated(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => Update()));
            
        }

        protected virtual void Update()
        {
        }
    }
}
