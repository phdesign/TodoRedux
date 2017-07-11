using TodoRedux.Repositories;
using TodoRedux.Views;
using Xamarin.Forms;

namespace TodoRedux
{
    public partial class App : Application
    {
		static TodoItemRepository database;

        public App()
        {
            InitializeComponent();

            Resources = new ResourceDictionary();
			Resources.Add("primaryGreen", Color.FromHex("91CA47"));
			Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

			var nav = new NavigationPage(new TodoListPage());
			nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
			nav.BarTextColor = Color.White;

			MainPage = nav;
        }
		
		public static TodoItemRepository Database
		{
			get
			{
				if (database == null)
				{
					database = new TodoItemRepository();
				}
				return database;
			}
		}

		public int ResumeAtTodoId { get; set; }
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
