using System.Linq;
using Redux;
using TodoRedux.Helpers;
using TodoRedux.Middleware;
using TodoRedux.State;
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

            var dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("todo.db");
            var liteDbMiddleware = new LiteDbMiddleware<ApplicationState>(dbPath);
            Store = new Store<ApplicationState>(
                Reducers.Reducers.ReduceApplication, 
                new ApplicationState(),
                liteDbMiddleware.CreateMiddleware());
            liteDbMiddleware.ReplayHistory();

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
