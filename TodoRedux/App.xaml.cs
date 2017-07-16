using Redux;
using TodoRedux.States;
using TodoRedux.ViewModels;
using TodoRedux.Views;
using Xamarin.Forms;

namespace TodoRedux
{
    public partial class App : Application
    {
        public static IStore<ApplicationState> Store { get; private set; }

        public App()
        {
            InitializeComponent();

            Store = new Store<ApplicationState>(Reducers.Reducers.ReduceApplication, new ApplicationState());

            Resources = new ResourceDictionary();
			Resources.Add("primaryGreen", Color.FromHex("91CA47"));
			Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new TodoListPage());
			nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
			nav.BarTextColor = Color.White;

			MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
