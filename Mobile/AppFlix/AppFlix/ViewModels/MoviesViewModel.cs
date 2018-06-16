using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DutchAzureMeetup;

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

            IsBusy = true;

            try
            {
                Movies.Clear();
                var movies = await MovieService.GetMovies();
                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}