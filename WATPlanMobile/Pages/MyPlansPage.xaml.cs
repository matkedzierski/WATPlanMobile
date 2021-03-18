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
        public ObservableCollection<PlanModel> Plans { get; set; }
        public MyPlansPage()
        {
            GetSavedPlans();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            GetSavedPlans();
        }

        private async void GetSavedPlans()
        {
            var list = await App.DB.GetPlansAsync();
            foreach (var p in list)
            {
                Debug.WriteLine(p);
            }
            Plans = new ObservableCollection<PlanModel>(list);
            ListView.ItemsSource = Plans;
            if (Plans.Count != 0) return;
            Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync(true);
        }
        
        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UnitsPage());
        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var plan = (PlanModel) e.Item;
            await Navigation.PushAsync(new SavedPlanPage(plan));
        }
    }
}