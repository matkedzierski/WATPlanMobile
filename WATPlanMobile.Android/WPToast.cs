using System.Threading;
using Android.Widget;
using WATPlanMobile.Android;
using Xamarin.Forms;
using Application = Android.App.Application;
using Toast = WATPlanMobile.Controllers.Toast;

[assembly: Dependency(typeof(WPToast))]

namespace WATPlanMobile.Android
{
    public class WPToast : Toast
    {
        public void Show(string message)
        {
            global::Android.Widget.Toast.MakeText(Application.Context, message, ToastLength.Long)?.Show();
            
        }
    }
}