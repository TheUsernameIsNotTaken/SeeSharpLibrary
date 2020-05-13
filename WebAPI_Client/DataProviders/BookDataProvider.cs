using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Library_Models;
using Newtonsoft.Json;

namespace WebAPI_Client.DataProviders
{
    public static class BookDataProvider
    {
        private static string _url = "http://localhost:5000/api/book";

        public static IList<Book> GetBook()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url).Result;
                    
                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var book = JsonConvert.DeserializeObject<IList<Book>>(rawData);
                    return book;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        public static void CreateBook(Book book)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(book);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PostAsync(_url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        public static void UpdateBook(Book book)
        {
            using (var client = new HttpClient())
            {
                var rawData = JsonConvert.SerializeObject(book);
                var content = new StringContent(rawData, Encoding.UTF8, "application/json");

                var response = client.PutAsync(_url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        public static void DeleteBook(long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync(_url + "/" + id).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }
    }
}
