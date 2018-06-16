using Xamarin.Forms;
using AppFlix.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppFlix
{
    public partial class App : Application
	{
		
		public App ()
		{
			InitializeComponent();


            MainPage = new NavigationPage(new MoviesPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
