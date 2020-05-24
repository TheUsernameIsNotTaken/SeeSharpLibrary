using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Library_Models;
using Newtonsoft.Json;

namespace Reader_Client.DataProviders
{
    public static class PersonDataProvider
    {
        private static string _url = LibraryDataProvider.personUrl;
    }
}
