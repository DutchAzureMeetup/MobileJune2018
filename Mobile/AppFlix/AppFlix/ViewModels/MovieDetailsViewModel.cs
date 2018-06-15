using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DutchAzureMeetup;
using Xamarin.Forms;

namespace AppFlix.ViewModels
{
    public class MovieDetailsViewModel : BaseViewModel
    {
        private MovieSummary _movieSummary;

        private MovieDetails _movie;
        public MovieDetails Movie
        {
            get => _movie;
            set => SetProperty(ref _movie, value);
        }

        public Command LoadMovieCommand { get; set; }

        public MovieDetailsViewModel(MovieSummary item = null)
        {
            _movieSummary = item;
            Title = item?.Title;
            LoadMovieCommand = new Command(async () => await ExecuteLoadMovieCommand());
        }

        async Task ExecuteLoadMovieCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Movie = await MovieService.GetMovieDetails(_movieSummary.Id);
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
