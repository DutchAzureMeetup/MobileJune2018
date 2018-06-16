
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppFlix.ViewModels;

namespace AppFlix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetailsPage : ContentPage
	{
        MovieDetailsViewModel viewModel;

        public MovieDetailsPage(MovieDetailsViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadMovieCommand.Execute(null);
        }
    }
}