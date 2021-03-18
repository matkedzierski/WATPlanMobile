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
        public EventBox()
        {
            InitializeComponent();
            var tap = new TapGestureRecognizer();
            tap.Tapped += PokazInfo;
            GestureRecognizers.Add(tap);
            Debug.WriteLine("Utworzono box!", "dupa");
        }

        public static readonly BindableProperty EventProperty = BindableProperty.Create(nameof(Event), typeof(EventModel), typeof(EventBox));
        public EventModel Event
        {
            get => (EventModel) GetValue(EventProperty);
            set
            {
                SetValue(EventProperty, value);
                Line1 = value.Name;
                Line2 = value.Type;
                Line3 = value.Week.ToString();
                Color = Color.FromHex(value.Color);
                Debug.WriteLine("Ustawiono box!", "dupa");
            }
        }
        
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public Color Color { get; set; }
        
        
        private async void PokazInfo(object sender, EventArgs e)
        {
            var parent = Parent;
            while (!(parent is PlanPage))
            {
                parent = parent.Parent;
            }

            var title = Event.Name + " (" + Event.Type + ")";
            var info =
                "Wykładowca: \n" + 
                Event.Lecturer + "\n\n" +
                "Sala: \n" + 
                Event.Room + "\n\n" +
                "Grupy: \n" + 
                Event.Groups;
            
            
            await ((PlanPage) parent).DisplayAlert(title, info, "Zamknij");
        }
        
    }
}