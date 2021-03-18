using System;
using System.Diagnostics;
using System.IO;
using WATPlanMobile.Controllers;
using WATPlanMobile.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace WATPlanMobile.Main
{
    public partial class App
    {
        public App()
        {
            Debug.WriteLine("Uruchomiono aplikację!", "dupa");
            InitializeComponent();
            Device.SetFlags(new[] {
                "CarouselView_Experimental",
            });
            var list = DB.GetPlansAsync().Result;
            MainPage = list.Count > 0 ? new NavigationPage(new MyPlansPage()) : new NavigationPage(new MainPage());
        }


        public static PlanyDB database;
        
        public static PlanyDB DB =>
            database ?? (database =
                new PlanyDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Plany.db")));


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