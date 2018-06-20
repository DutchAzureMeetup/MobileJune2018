using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppFlix.ViewModels;
using Common.Contracts;
using AppFlix.Views;

namespace AppFlix.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoviesPage : ContentPage
	{
        MoviesViewModel viewModel;

        public MoviesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MoviesViewModel();
        }

		// TODO: Bind to OnItemSelected

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Movies.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}