using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WATPlanMobile.Controllers;
using WATPlanMobile.Main;
using WATPlanMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WATPlanMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanPage
    {
        public PlanPage(PlanModel wybrany)
        {
            Plan = wybrany;
            BindingContext = Plan;
            InitializeComponent();
            Load();
        }


        public PlanPage()
        {
        }

        public PlanModel Plan { get; set; }

        private async void Zapisz(object sender, EventArgs e)
        {
            var saved = await App.DB.GetPlanAsync(Plan.ID);
            if (saved != null)
            {
                DependencyService.Get<Toast>().Show("Ten plan jest już zapisany!");
                return;
            }

            await App.DB.SavePlanAsync(Plan);
            DependencyService.Get<Toast>().Show("Zapisano Plan " + Plan.Name);
            Navigation.InsertPageBefore(new MyPlansPage(), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync(true);
        }

        private async void Load()
        {
            Indicator.IsRunning = true;
            
            var events = new ObservableCollection<EventModel>();
            await Task.Run(async () =>
            {
                events = await Plan.LoadFromAPI();
            });
            if (events == null) DependencyService.Get<Toast>().Show("Nie udało się załadować planu!");
            else if (events.Count > 0)
            {
                Plan.SetEvents(events);
                carousel.ScrollTo(CurrentWeekNumber(), animate: false);
            }
            else DependencyService.Get<Toast>().Show("Plan jest pusty!");

            Indicator.IsRunning = false;
        }
        
        public static int CurrentWeekNumber()
        {
            // wyznacz ostatni pierwszy października
            var currDate = DateTime.Now.Date;
            var startSemestru = new DateTime(currDate.Year, 10, 1);
            if (currDate.Month < 10) startSemestru = startSemestru.AddYears(-1);
            
            // wyznacz pierwszy poniedziałek <= 1.10
            var dow = (int)startSemestru.DayOfWeek;
            if (dow == 0) dow = 7; // 0 to niedziela -> 7
            var odPoniedzialku = dow - 1; // ile dni od poniedzialku
            var zerowyPoniedzialek = startSemestru.AddDays(-1 * odPoniedzialku); // wyznacz datę pierwszego poniedziałku przed 1.10
            
            // wyznacz numer tygodnia względem zerowego poniedziałku
            var timespan = DateTime.Now.Date.Subtract(zerowyPoniedzialek); // odleglosc od poniedzialku zerowego
            var tydzien = (int)timespan.TotalDays / 7; // na tygodnie

            return tydzien;
        }

        private void Carousel_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "ItemsSource") return;
            if (carousel.ItemsSource == null) return;
            carousel.IsScrollAnimated = false;
            carousel.CurrentItem = carousel.ItemsSource.Cast<object>().ToList()[PlanPage.CurrentWeekNumber()];
            carousel.IsScrollAnimated = true;
        }
    }
}