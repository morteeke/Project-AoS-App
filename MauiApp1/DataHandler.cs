using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1
{
    public static class DataHandler
    {
        public static async Task<string> Load()
        {
            //This has to be declared to use the caching. (so it doesn't conflict)
            Barrel.ApplicationId = "unique_app_id";


            string cacheKey = "my_api_data";

            //Here we delete the contents of all expired caches.
            Barrel.Current.EmptyExpired();

            //We get the data that has already been cached.
            string cachedData = Barrel.Current.Get<string>(cacheKey);

            //We check if the cache is empty or not. Do some stuff wheter it is empty or not.
            if (string.IsNullOrEmpty(cachedData))
            {
                var apiData = await FetchData();

                //Let's get the data from the api and save it to the cache.
                Barrel.Current.Add(cacheKey, apiData, expireIn: TimeSpan.FromHours(1));
                return "NEW = " + apiData.ToString();
            }
            else
            {
                return "OLD = " + cachedData;

            }
        }

        private static async Task<JsonElement> FetchData()
        {

            string result = await APIHandler.Request();

            // Parse the JSON response
            JsonDocument jsonDocument = JsonDocument.Parse(result);

            // Access the desired data in the JSON structure
            JsonElement rootElement = jsonDocument.RootElement;

            return rootElement;
        }
    }
}
