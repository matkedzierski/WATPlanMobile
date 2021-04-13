using System;
using System.Diagnostics;
using System.Linq;
using WATPlanMobile.Models;
using WATPlanMobile.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WATPlanMobile.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanGridHeader
    {
        public static BindableProperty WeekProperty =
            BindableProperty.Create(
                nameof(Week),
                typeof(WeekModel),
                typeof(PlanGridHeader),
                propertyChanged: OnWeekChanged);

        public PlanGridHeader()
        {
            InitializeComponent();
            
        }

        public WeekModel Week
        {
            get => (WeekModel) GetValue(WeekProperty);
            set => SetValue(WeekProperty, value);
        }

        private static void OnWeekChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null || newvalue == oldvalue) return;
            var pg = (PlanGridHeader) bindable;
            pg.Children.Clear();
            pg.InitializeComponent();

            if (Preferences.Get("ukr_weekendy", false))
            {
                pg.ColumnDefinitions[6].Width = 0;
                pg.ColumnDefinitions[5].Width = 0;
            }
            else
            {
                pg.ColumnDefinitions[6].Width = GridLength.Star;
                pg.ColumnDefinitions[5].Width = GridLength.Star;
            }
            
            var week = pg.Week.Offset == PlanPage.CurrentWeekNumber();
            var podswietlone = false;
            
            var day = (int)DateTime.Now.DayOfWeek;
            if (day == 0) day = 7;
            var block = GetCurrentBlock();
            

            foreach (var e in pg.Week.Events)
            {
                if (week && e.DayOfWeek == day && block >= e.BlockNumber && block <= e.BlockNumber + e.BlockSpan - 1)
                {
                    pg.Children.Add(new Frame{
                        BackgroundColor = Color.Red,
                        Padding = new Thickness(1.5),
                        Content = new EventBox
                        {
                            Event = e, Line1 = e.Name, Line2 = e.Type, Line3 = e.Number,
                            Color = Color.FromHex(e.Color),
                            BorderColor = Color.Transparent
                        }}, e.DayOfWeek - 1, e.DayOfWeek, e.BlockNumber, e.BlockNumber + e.BlockSpan);
                    
                    podswietlone = true;
                }
                else
                {
                    pg.Children.Add(
                        new EventBox
                        {
                            Event = e, Line1 = e.Name, Line2 = e.Type, Line3 = e.Number,
                            Color = Color.FromHex(e.Color),
                            BorderColor = Color.Black
                        }, e.DayOfWeek - 1, e.DayOfWeek, e.BlockNumber, e.BlockNumber + e.BlockSpan);
                }
            }

            if(!podswietlone && week)
                pg.Children.Add(new Frame{BorderColor = Color.Red, BackgroundColor = Color.Transparent, CornerRadius = 5}, day-1, day, block, block + 1);
        }

        private static int GetCurrentBlock()
        {
            var now = DateTime.Now;
            var n = now.Hour*60 + now.Minute;
            if (n > 480 && n < 575) return 1;
            if (n > 590 && n < 685) return 2;
            if (n > 700 && n < 795) return 3;
            if (n > 810 && n < 905) return 4; 
            //długa przerwa
            if (n > 945 && n < 1040) return 5;
            if (n > 1055 && n < 1150) return 6;
            if (n > 1165 && n < 1260) return 7;
            return 0;
        }
    }
}