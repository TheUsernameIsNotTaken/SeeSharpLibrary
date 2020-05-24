using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Library_Models;
using Newtonsoft.Json;

namespace Reader_Client.DataProviders
{
    public static class LibraryDataProvider
    {
        public static string bookUrl = "http://localhost:5000/api/book";
        public static string personUrl = "http://localhost:5000/api/person";
        public static string archiveUrl = "http://localhost:5000/api/archive";

        //Get a single data inside a database from the server by it's ID.
        public static T GetSingleData<T>(string url, long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url + "/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var singleData = JsonConvert.DeserializeObject<T>(rawData);
                    return singleData;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        //Get a all data inside a database from the server
        public static IList<T> GetAllData<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                    
                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var allData = JsonConvert.DeserializeObject<IList<T>>(rawData);
                    return allData;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        //Search multiple existing entries in the database on the server by a part of their data.
        public static IList<T> SearchByStringData<T>(string searchUrl, string dataPart)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(searchUrl + dataPart).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<IList<T>>(rawData);
                    return data;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }
    }
}
