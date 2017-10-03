using System;
using System.Diagnostics;
using LocalDatabaseSample.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LocalDatabaseSample
{
    public partial class App : Application
    {
        static ExtendedDatabase database3;

        public App()
        {
            InitializeComponent();
            Resources = new ResourceDictionary
            {
                { "primaryGreen", Color.FromHex("91CA47") },
                { "primaryDarkGreen", Color.FromHex("6FA22E") }
            };

            var nav = new NavigationPage(new AssetListPage())
            {
                BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"],
                BarTextColor = Color.White
            };


            MainPage = nav;
        }

		/* Create Database Object. This Database is created if it does not exist.
		 * Otherwise return the database connection.
		 * 
		 */ 
		public static ExtendedDatabase Database3
		{
			get
			{
				if (database3 == null)
				{
					database3 = new ExtendedDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("ExtendedSQLite.db3"));
				}
				return database3;
			}
		}

        public int ResumeAtTodoId { get; set; }

        public int ResumeAt { get; set; }

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
