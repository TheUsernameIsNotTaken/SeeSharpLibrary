using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Library_Models;
using Newtonsoft.Json;

namespace Admin_Client.DataProviders
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
        public static IList<T> SearchData<T>(string url, string dataPart)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url + "/search/" + dataPart).Result;

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

        //Send a new data to a database on the server to save it.
        public static void CreateData<T>(string url, T entity)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(entity);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        //Update an already existing data in a database on the server.
        public static void UpdateData<T>(string url, T entity, long id)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(entity);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PutAsync(url + "/" + id, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        //Delete an existing data in a database on the server.
        public static void DeleteData<T>(string url, long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync(url + "/" + id).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }
    }
}
