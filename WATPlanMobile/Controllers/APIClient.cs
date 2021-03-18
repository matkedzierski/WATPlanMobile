using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using WATPlanMobile.Models;
using Xamarin.Essentials;

namespace WATPlanMobile.Controllers
{
    public class APIClient 
    {
        public static async Task<ObservableCollection<UnitModel>> GetAllUnits()
        {
            if (!IsConnected()) return null;
            var uri = new Uri ("http://watplan.ml/DomenaTestowa/api/units");
            var client = new HttpClient ();
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<UnitModel>>(content);
            return list;
        }

        public static async Task<ObservableCollection<PlanModel>> GetPlansForUnit(string unitId)
        {
            if (!IsConnected()) return null;
            var uri = new Uri ("http://watplan.ml/DomenaTestowa/api/plans/" + unitId);
            var client = new HttpClient ();
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<PlanModel>>(content);
            return list;
        }

        public static async Task<ObservableCollection<EventModel>> GetEventsForPlan(string planId)
        {
            if (!IsConnected()) return null;
            var uri = new Uri ("http://watplan.ml/DomenaTestowa/api/events/" + planId);
            var client = new HttpClient ();
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode) return null;
            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<ObservableCollection<EventModel>>(content);
            return list;
        }

        public static bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet || current == NetworkAccess.Local;
        }
    }
}