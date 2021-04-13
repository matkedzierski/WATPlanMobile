using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WATPlanMobile.Main;
using WATPlanMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WATPlanMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPlansPage
    {
        public MyPlansPage()
        {
            GetSavedPlans();
            InitializeComponent();
        }

        public ObservableCollection<PlanModel> Plans { get; set; }

        protected override void OnAppearing()
        {
            GetSavedPlans();
        }

        private async void GetSavedPlans()
        {
            var list = await App.DB.GetPlansAsync();
            Plans = new ObservableCollection<PlanModel>(list);
            ListView.ItemsSource = Plans;
            if (Plans.Count != 0) return;
            Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync(true);
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            if(((ToolbarItem) sender).Text=="Dodaj") await Navigation.PushAsync(new UnitsPage());
            if(((ToolbarItem) sender).Text=="Ustawienia") await Navigation.PushAsync(new SettingsPage());
        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var plan = (PlanModel) e.Item;
            await Navigation.PushAsync(new SavedPlanPage(plan));
        }
    }
}