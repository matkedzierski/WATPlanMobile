using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using WATPlanMobile.Controllers;
using Xamarin.Forms;

namespace WATPlanMobile.Models
{
    [Table("plans")]
    [DataContract]
    [Serializable]
    public class PlanModel : INotifyPropertyChanged
    {
        [PrimaryKey] [DataMember]
        public string ID { get; set; }

        [DataMember] 
        public string Name { get; set; }

        [DataMember] 
        public string Type { get; set; }

        [TextBlob("weeksBlobbed")] 
        public ObservableCollection<WeekModel> Weeks { get; set; }

        public string weeksBlobbed { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;


        public async Task<ObservableCollection<EventModel>> LoadFromAPI()
        {
            var events = await APIClient.GetEventsForPlan(ID);
            return events;
            //DependencyService.Get<Toast>().Show("Nie udało się wczytać planu!");
            
        }

        public void SetEvents(ObservableCollection<EventModel> events)
        {
            if (Weeks == null) Weeks = new ObservableCollection<WeekModel>();
            else Weeks.Clear();
            for (var i = 0; i < 50; i++)
            {
                var i1 = i;
                var wm = new WeekModel
                {
                    Offset = i, Events = new ObservableCollection<EventModel>(events.Where(e => e.Week == i1)),
                    Dates = GetDates(i1)
                };
                Weeks.Add(wm);
            }

            OnPropertyChanged(nameof(Weeks));
        }
        
        private string[] GetDates(int off)
        {
            var Dates = new string[7];
            // wyznacz ostatni pierwszy października
            var currDate = DateTime.Now.Date;
            var startSemestru = new DateTime(currDate.Year, 10, 1);
            if (currDate.Month < 10) startSemestru = startSemestru.AddYears(-1);

            // wyznacz pierwszy poniedziałek <= 1.10
            var dow = (int) startSemestru.DayOfWeek;
            if (dow == 0) dow = 7; // 0 to niedziela -> 7
            var odPoniedzialku = dow - 1; // ile dni od poniedzialku
            var zerowyPoniedzialek =
                startSemestru.AddDays(-1 * odPoniedzialku); // wyznacz datę pierwszego poniedziałku przed 1.10
            var iter = zerowyPoniedzialek.AddDays(7 * off);

            for (var i = 0; i < 7; i++)
            {
                Dates[i] = iter.ToString("dd.MM");
                iter = iter.AddDays(1);
            }

            return Dates;
        }

        public override string ToString()
        {
            return "> Plan:      \n" +
                   $"\tName:      {Name} \n" +
                   $"\tID:        {ID} \n" +
                   $"\tType:      {Type} \n" +
                   $"\tWeeks:     {Weeks.Count} \n";
        }

        public IEnumerable<EventModel> GetWeekEvents(int WeekOffset)
        {
            var week = Weeks.Single(w => w.Offset == WeekOffset);
            return week.Events;
        }

       
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}