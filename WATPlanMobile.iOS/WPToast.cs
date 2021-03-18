using Foundation;
using UIKit;
using WATPlanMobile.Interfaces;
using WATPlanMobile.iOS;

[assembly:Xamarin.Forms.Dependency(typeof(WPToast))]  
  
namespace WATPlanMobile.iOS
{  
    public class WPToast : Toast  
    {
        private const double LONG_DELAY = 3.5;

        private NSTimer alertDelay;
        private UIAlertController alert;  
  
        public void Show(string message)  
        {  
            ShowAlert(message, LONG_DELAY);  
        }


        private void ShowAlert(string message, double seconds)  
        {  
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>  
            {  
                dismissMessage();  
            });  
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);  
            UIApplication.SharedApplication.KeyWindow.RootViewController?.PresentViewController(alert, true, null);  
        }

        private void dismissMessage()  
        {
            alert?.DismissViewController(true, null);
            alertDelay?.Dispose();
        }  
          
    }  
}