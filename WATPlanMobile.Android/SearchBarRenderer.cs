using Android.Content;
using Android.Widget;
using WATPlanMobile.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(WPSearchBarRenderer))]

namespace WATPlanMobile.Android
{
    public class WPSearchBarRenderer : SearchBarRenderer
    {
        public WPSearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Context?.Resources == null) return;
            var icon = Control?.FindViewById(Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null));
            (icon as ImageView)?.SetColorFilter(Color.LightGray.ToAndroid());
        }
    }
}