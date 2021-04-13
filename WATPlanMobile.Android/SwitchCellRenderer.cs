using Android.Content;
using Android.Views;
using Android.Widget;
using WATPlanMobile.Android;
using WATPlanMobile.Main;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof (WPSwitchCell), typeof (CustomSwitchCellRenderer))]
namespace WATPlanMobile.Android
{
    
    public class CustomSwitchCellRenderer : SwitchCellRenderer
    {
        protected override global::Android.Views.View GetCellCore(Cell item, global::Android.Views.View convertView, ViewGroup parent,
            Context context)
        {

            var cell =  base.GetCellCore(item, convertView, parent, context);

            var child1 = ((LinearLayout)cell).GetChildAt(1);

            var label = (TextView)((LinearLayout)child1)?.GetChildAt(0);
            var c = AppInfo.RequestedTheme switch
            {
                AppTheme.Light => "L_NormalTextColor",
                AppTheme.Dark => "D_NormalTextColor",
                _ => "L_NormalTextColor"
            };
            var color = Application.Current.Resources[c] is Color
                ? (Color) Application.Current.Resources[c]
                : default;
            label?.SetTextColor(color.ToAndroid());

            return cell;
        }
    }
    
}