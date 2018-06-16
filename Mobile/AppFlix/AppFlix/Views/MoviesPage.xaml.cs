using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppFlix.ViewModels;
using DutchAzureMeetup;
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

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as MovieSummary;
            if (item == null)
                return;

            await Navigation.PushAsync(new MovieDetailsPage(new MovieDetailsViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Movies.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}