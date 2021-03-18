using Android.App;
using Android.Widget;
using WATPlanMobile.Android;
using Toast = WATPlanMobile.Controllers.Toast;

[assembly: Xamarin.Forms.Dependency(typeof(WPToast))]  
  
namespace WATPlanMobile.Android  
{  
    public class WPToast : Controllers.Toast
    {  
        public void Show(string message)  
        {  
            global::Android.Widget.Toast.MakeText(Application.Context,message,ToastLength.Long)?.Show();  
        }  
    }  
}