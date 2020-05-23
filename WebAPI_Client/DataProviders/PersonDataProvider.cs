using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Library_Models;
using Newtonsoft.Json;

namespace Admin_Client.DataProviders
{
    public static class PersonDataProvider
    {
        private static string _url = LibraryDataProvider.personUrl;

        ////Search multiple existing persons in the database on the server by part of their name.
        //public static IList<Person> SearchBooks(string name)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = client.GetAsync(_url + "/search/" + name).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var rawData = response.Content.ReadAsStringAsync().Result;
        //            var books = JsonConvert.DeserializeObject<IList<Person>>(rawData);
        //            return books;
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException(response.StatusCode.ToString());
        //        }
        //    }
        //}
    }
}
