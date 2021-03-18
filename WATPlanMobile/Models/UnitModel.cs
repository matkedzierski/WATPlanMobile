using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace WATPlanMobile.Models
{
    [DataContract]
    [Serializable]
    public class UnitModel
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Count { get; set; }
        public ObservableCollection<PlanModel> Plans { get; set; }
    }
}