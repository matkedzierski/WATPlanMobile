using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using WATPlanMobile.Main;
using Xamarin.Forms.Platform.Android;

namespace WATPlanMobile.Android
{
    [Activity(Label = "WATPlanMobile", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            var statuBarColor = Xamarin.Forms.Application.Current.Resources["StatusBarColor"] is Xamarin.Forms.Color ? (Xamarin.Forms.Color) Xamarin.Forms.Application.Current.Resources["StatusBarColor"] : default;
            Window?.SetStatusBarColor(statuBarColor.ToAndroid());
        }
    }
}