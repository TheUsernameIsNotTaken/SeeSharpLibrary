using System;
using System.Collections.Generic;
using System.Net.Http;
using Library_Models;
using Newtonsoft.Json;

namespace Admin_Client.DataProviders
{
    public static class ArchiveDataProvider
    {
        private static string _url = LibraryDataProvider.archiveUrl;

        //Get a specific existing data in the database on the server by it's ids.
        public static ArchiveData GetSpecificData(long bookId, long borrowerId)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url + "/specific/" + bookId + "+" + borrowerId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ArchiveData>(rawData);
                    return data;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }

        //Get a specific existing data in the database on the server by a specific id.
        public static IList<ArchiveData> GetManyBySingleId(bool isBookId, long id)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url + (isBookId ? "/book/" : "/person/") + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var manyData = JsonConvert.DeserializeObject<IList<ArchiveData>>(rawData);
                    return manyData;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }
    }
}
