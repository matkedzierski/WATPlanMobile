using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WATPlanMobile.Main;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WATPlanMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            ukr_wyklady.On = Preferences.Get("ukr_wyklady", false);
            ukr_weekendy.On = Preferences.Get("ukr_weekendy", false);
        }


        private async void Change(object sender, ToggledEventArgs e)
        {
            if(sender == ukr_wyklady) Preferences.Set("ukr_wyklady", ukr_wyklady.On);
            if(sender == ukr_weekendy) Preferences.Set("ukr_weekendy", ukr_weekendy.On);

            await Application.Current.SavePropertiesAsync();
        }
    }
}