using System;
using System.Diagnostics;
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
        public PlanModel Plan { get; set; }

        public PlanPage(PlanModel wybrany)
        {
            Plan = wybrany;
            BindingContext = Plan;
            InitializeComponent();
            RefreshView.IsRefreshing = true;
            Debug.WriteLine("Wybrano Plan: " + Plan.Name + "!", "dupa");
        }

        public PlanPage(){}

        private async void Zapisz(object sender, EventArgs e)
        {
            var saved = await App.DB.GetPlanAsync(Plan.ID);
            if (saved != null)
            {
                DependencyService.Get<Toast>().Show("Ten plan jest już zapisany!");
                return;
            }
            await App.DB.SavePlanAsync(Plan);
            //TODO: krzyknij że zapisano
            Debug.WriteLine("Zapisano Plan: " + Plan.Name, "dupa");
            DependencyService.Get<Toast>().Show("Zapisano Plan " + Plan.Name);
            Navigation.InsertPageBefore(new MyPlansPage(), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync(true);
        }

        private async void RefreshView_OnRefreshing(object sender, EventArgs e)
        {
            await Plan.Load();
            RefreshView.IsRefreshing = false;
        }
    }
}
