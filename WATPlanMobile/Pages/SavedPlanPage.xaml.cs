using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WATPlanMobile.Controllers;
using WATPlanMobile.Main;
using WATPlanMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WATPlanMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedPlanPage
    {
        public SavedPlanPage(PlanModel plan)
        {
            Plan = plan;
            BindingContext = Plan;
            InitializeComponent();
            Load();
            CheckUpdates();
            Weekendy();
        }

        
        public PlanModel Plan { get; set; }

        private async void Usun(object sender, EventArgs e)
        {
            await App.DB.DeletePlanAsync(Plan);
            DependencyService.Get<Toast>().Show("Usunięto Plan " + Plan.Name);
            await Navigation.PopToRootAsync(false);
        }

        private void Weekendy()
        {
            if (Preferences.Get("ukr_wyklady", false))
            {
                var newWeeks = Plan.Weeks.ToList();
                foreach (var wm in newWeeks)
                {
                    wm.Events = new ObservableCollection<EventModel>(wm.Events.Where(ev => ev.Type != "Wykład"));
                }

                carousel.ItemsSource = newWeeks;
            }
            else
            {
                carousel.ItemsSource = Plan.Weeks;
            }
        }

        private void Load()
        {
            //carousel.ScrollTo(PlanPage.CurrentWeekNumber(), animate: false);
            if (carousel.ItemsSource == null) return;
            carousel.IsScrollAnimated = false;
            var obj = carousel.ItemsSource.Cast<object>().ToList()[PlanPage.CurrentWeekNumber()];
            if(obj!=null)
                carousel.CurrentItem = obj;
            carousel.IsScrollAnimated = true;
            //DependencyService.Get<Toast>().Show("Załadowano plan z bazy");
        }
        
        private async void CheckUpdates()
        {
            //TODO: Preferences
            Indicator.IsRunning = true;
            
            var events = new ObservableCollection<EventModel>();
            await Task.Run(async () =>
            {
                events = await Plan.LoadFromAPI();
            });
            if (events == null || events.Count==0) DependencyService.Get<Toast>()
                .Show("Nie udało się zaktualizować planu! \nWidoczna wersja może nie być aktualna!");
            else if (events.Count > 0)
            {
                Plan.SetEvents(events);
            }

            Indicator.IsRunning = false;
        }
    }
}