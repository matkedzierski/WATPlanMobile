using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
using WATPlanMobile.Annotations;
using WATPlanMobile.Controllers;

namespace WATPlanMobile.Models
{
    [Table("plans")]
    [DataContract]
    [Serializable]
    public class PlanModel : INotifyPropertyChanged
    {
        [PrimaryKey]
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Type { get; set; }

        [Ignore]
        public ObservableCollection<EventModel> Events
        {
            get => EventsBlobbed ==null ? new ObservableCollection<EventModel>() : JsonConvert.DeserializeObject<ObservableCollection<EventModel>>(EventsBlobbed);
            set => EventsBlobbed = JsonConvert.SerializeObject(value);
        }
        [DataMember]
        public string EventsBlobbed { get; set; }

        public async Task Load()
        {
            Debug.WriteLine("Ładuje...", "dupa");
            Events = await APIClient.GetEventsForPlan(ID);
            Debug.WriteLine(Events.Count + " już", "dupa");
            OnPropertyChanged(nameof(Events));
        }
        
        public override string ToString()
        {
            return $"> Plan: \n" + 
                   $"\tName:      {Name} \n" +
                   $"\tID:        {ID} \n" +
                   $"\tType:      {Type} \n" +
                   $"\tEvents:    {Events.Count} \n";
        }

        public IEnumerable<EventModel> GetWeekEvents(int WeekOffset)
        {
            var cest = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
            //wyznacz ktory tydzien
            var now = TimeZoneInfo.Local.Equals(cest) ? DateTime.Now : TimeZoneInfo.ConvertTime(DateTime.Now, cest);
            var currentWeek = GetWeekNumber(now);
            var week = currentWeek + WeekOffset;
            //zbierz wydarzenia z tego tygodnia
            return Events.Where(e => e.Week == week).ToList().OrderByDescending(model => model.Week*7 + model.DayOfWeek);
        }

        public static int GetWeekNumber(DateTime start)
        {
            while (true)
            {
                var startOfYear = start.AddDays(-start.Day + 1).AddMonths(-start.Month + 1);
                var endOfYear = startOfYear.AddYears(1).AddDays(-1);
                int[] iso8601Correction = {6, 7, 8, 9, 10, 4, 5};
                var nds = start.Subtract(startOfYear).Days + iso8601Correction[(int) startOfYear.DayOfWeek];
                var wk = nds / 7;
                switch (wk)
                {
                    case 0:
                        start = startOfYear.AddDays(-1);
                        continue;
                    case 53:
                        return endOfYear.DayOfWeek < DayOfWeek.Thursday ? 1 : wk;
                    default:
                        return wk;
                }
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}