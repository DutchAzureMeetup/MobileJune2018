using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Common.Contracts;

namespace AppFlix.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {
        public ObservableCollection<MovieSummary> Movies { get; set; }
        public Command LoadItemsCommand { get; set; }

        public MoviesViewModel()
        {
            Title = "Browse";
            Movies = new ObservableCollection<MovieSummary>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

			throw new NotImplementedException();
        }
    }
}