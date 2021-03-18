using System;
using System.Diagnostics;
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
        public PlanModel Plan { get; set; }
        public SavedPlanPage(PlanModel plan)
        {
            Plan = plan;
            BindingContext = Plan;
            InitializeComponent();
        }
        
        private async void Usun(object sender, EventArgs e)
        {
            await App.DB.DeletePlanAsync(Plan);
            //TODO: krzyknij że zapisano
            Debug.WriteLine("Usunięto Plan: " + Plan.Name, "dupa");
            DependencyService.Get<Toast>().Show("Usunięto Plan " + Plan.Name);
            await Navigation.PopToRootAsync(false);
        }
        
        private void Weekendy(object sender, EventArgs e)
        {
            if (Preferences.Get("wyk", false))
            {
                Preferences.Set("wyk", true);
                ((ToolbarItem) sender).Text = "Pokaż weekendy";
            }
            else
            {
                Preferences.Set("wyk", false);
                ((ToolbarItem) sender).Text = "Ukryj weekendy"; 
            }
        }

        private void RefreshView_OnRefreshing(object sender, EventArgs e)
        {
            //TODO update wydarzeń
        }
    }
}