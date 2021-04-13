using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace WATPlanMobile.Models
{
    [Serializable]
    public class WeekModel
    {
        private ObservableCollection<EventModel> evs;
        
        public string[] Dates { get; set; }

        public int Offset { get; set; }

        public ObservableCollection<EventModel> Events
        {
            get => evs;
            set
            {
                evs = value;
                SetEventArray(evs);
            }
        }

        [Ignore]
        public EventModel[] EventArray { get; set; }


        public void SetEventArray(ObservableCollection<EventModel> eventList)
        {
            EventArray = new EventModel[49];
            foreach (var e in eventList) EventArray[(e.BlockNumber - 1) * (e.DayOfWeek - 1) + (e.DayOfWeek - 1)] = e;
        }
    }
}