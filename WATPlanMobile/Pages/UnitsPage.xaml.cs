using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WATPlanMobile.Controllers;
using WATPlanMobile.Models;
using Xamarin.Forms;

namespace WATPlanMobile.Pages
{
    public partial class UnitsPage
    {
        public ObservableCollection<UnitModel> Units{ get; set; }
        public UnitsPage()
        {
            InitializeComponent();
            ListView.SendRefreshing();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            Units = await Task.Run(APIClient.GetAllUnits);
            Device.BeginInvokeOnMainThread( ()=>
            {
                ((ListView) sender).ItemsSource = Units;
                ((ListView) sender).IsRefreshing = false;
            });
        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new PlansPage((UnitModel)e.Item));
        }
    }
}