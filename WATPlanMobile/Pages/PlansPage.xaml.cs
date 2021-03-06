using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WATPlanMobile.Controllers;
using WATPlanMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WATPlanMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlansPage
    {
        public PlansPage(UnitModel wydzial)
        {
            Wydzial = wydzial;
            BindingContext = Wydzial;
            InitializeComponent();
            ListView.SendRefreshing();
        }

        public UnitModel Wydzial { get; set; }
        public ObservableCollection<PlanModel> Wszystkie { get; set; }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            Wszystkie = await Task.Run(() => APIClient.GetPlansForUnit(Wydzial.ID));
            if (Wszystkie == null)
            {
                //TODO wypisz że się nie udało
                ((ListView) sender).IsRefreshing = false;
                return;
            }

            Wydzial.Plans = Wszystkie;
            Device.BeginInvokeOnMainThread(() =>
            {
                ((ListView) sender).ItemsSource = Wydzial.Plans;
                ((ListView) sender).IsRefreshing = false;
            });

            bar.IsEnabled = true;
        }

        private void Filtruj(object sender, TextChangedEventArgs e)
        {
            ListView.ItemsSource = Wydzial.Plans.Where(w => w.Name.ToLower().Contains(e.NewTextValue.ToLower()));
        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new PlanPage((PlanModel) e.Item));
        }
    }
}