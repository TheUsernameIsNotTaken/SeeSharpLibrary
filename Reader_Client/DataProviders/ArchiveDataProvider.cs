using System;
using System.Collections.Generic;
using System.Net.Http;
using Library_Models;
using Newtonsoft.Json;

namespace Reader_Client.DataProviders
{
    public static class ArchiveDataProvider
    {
        private static string _url = LibraryDataProvider.archiveUrl;

        //Get a specific existing data's Borrowed DateTime in the database on the server by it's ids.
        public static DateTime GetSpecificBorrowDateTime(long bookId, long borrowerId)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_url + "/specific/" + bookId + "+" + borrowerId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawData = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ArchiveData>(rawData);
                    return data.BorrowedAt.Value;
                }
                else
                {
                    throw new InvalidOperationException(response.StatusCode.ToString());
                }
            }
        }
    }
}
