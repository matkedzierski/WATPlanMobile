using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using WATPlanMobile.Main;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Platform = Xamarin.Essentials.Platform;

namespace WATPlanMobile.Android
{
    [Activity(Label = "WATPlan", Icon = "@mipmap/icon", Theme = "@style/AppTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            var statuBarColor = Xamarin.Forms.Application.Current.Resources["StatusBarColor"] is Color
                ? (Color) Xamarin.Forms.Application.Current.Resources["StatusBarColor"]
                : default;
            Window?.SetStatusBarColor(statuBarColor.ToAndroid());
        }
        
        
    }
}