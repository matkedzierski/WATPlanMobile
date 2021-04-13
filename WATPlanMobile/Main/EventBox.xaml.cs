using System;
using System.Diagnostics;
using WATPlanMobile.Models;
using WATPlanMobile.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WATPlanMobile.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventBox
    {
        public static readonly BindableProperty EventProperty = BindableProperty.Create(
            nameof(Event),
            typeof(EventModel),
            typeof(EventBox));

        public static readonly BindableProperty Line1Prop = BindableProperty.Create(
            nameof(Line1),
            typeof(string),
            typeof(EventBox));

        public static readonly BindableProperty Line2Prop = BindableProperty.Create(
            nameof(Line2),
            typeof(string),
            typeof(EventBox));

        public static readonly BindableProperty Line3Prop = BindableProperty.Create(
            nameof(Line3),
            typeof(string),
            typeof(EventBox));

        public static readonly BindableProperty ColorProp = BindableProperty.Create(
            nameof(Color),
            typeof(Color),
            typeof(EventBox));

        public EventBox()
        {
            BindingContext = this;
            InitializeComponent();
            var tap = new TapGestureRecognizer();
            tap.Tapped += PokazInfo;
            GestureRecognizers.Add(tap);
        }
        //propertyChanged: EventChanged);

        /* private static void EventChanged(BindableObject bindable, object oldvalue, object newvalue)
         {
             if (newvalue == null || newvalue == oldvalue || !(newvalue is EventModel)) return;
             var obj = (EventBox) bindable;
             var ev = (EventModel) newvalue;
             obj.InitializeComponent();
             Debug.WriteLine("Ustawiono box!", "dupa");
         }*/

        public EventModel Event
        {
            get => (EventModel) GetValue(EventProperty);
            set => SetValue(EventProperty, value);
        }

        public string Line1
        {
            get => (string) GetValue(Line1Prop);
            set => SetValue(Line1Prop, value);
        }

        public string Line2
        {
            get => (string) GetValue(Line2Prop);
            set => SetValue(Line2Prop, value);
        }

        public string Line3
        {
            get => (string) GetValue(Line3Prop);
            set => SetValue(Line3Prop, value);
        }

        public Color Color
        {
            get => (Color) GetValue(ColorProp);
            set => SetValue(ColorProp, value);
        }


        private async void PokazInfo(object sender, EventArgs e)
        {
            var parent = Parent;
            while (!(parent is PlanPage || parent is SavedPlanPage)) parent = parent.Parent;

            var title = Event.Name + " (" + Event.Type + ")";
            var info =
                //"Wykładowca: \n" +
                //Event.Lecturer + "\n\n" +
                "Sala: \n" +
                Event.Room + "\n\n" +
                "Grupy: \n" +
                Event.Groups;
            await ((Page) parent).DisplayAlert(title, info, "Zamknij");
        }
    }
}