using Azure;
using MonkeyCache.FileStore;
using System.Text.Json;

namespace MauiApp1;


// 19/07/2023: Tryed to cache the data fetched from api. Does not seem to work, might try with a txt file.

public partial class GamemodePage : ContentPage
{


    public GamemodePage()
	{
		InitializeComponent();

        //OnLoad();
	}

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private void OnCampaignButtonClicked(object sender, EventArgs e)
    {

        LoadData();
    }

    private async void OnOpenPlayButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("debug", "open play has been clicked", "OK");

        Barrel.Current.EmptyAll();
    }

    //This method gets all the data from the REST api.
    private async Task<JsonElement> GetData()
    {

        string result = await APIHandler.Request();

        // Parse the JSON response
        JsonDocument jsonDocument = JsonDocument.Parse(result);

        // Access the desired data in the JSON structure
        JsonElement rootElement = jsonDocument.RootElement;

        return rootElement;
    }

    
    //This method loads the data from the cache and checks if there is a cache.
    private async void LoadData()
    {
        //This has to be declared to use the caching. (so it doesn't conflict)
        Barrel.ApplicationId = "unique_app_id";


        string cacheKey = "my_api_data";

        //Here we delete the contents of all expired caches.
        Barrel.Current.EmptyExpired();

        //We get the data that has already been cached.
        string cachedData = Barrel.Current.Get<string>(cacheKey);

        //We check if the cache is empty or not. Do some stuff wheter it is empty or not.
        if(string.IsNullOrEmpty(cachedData))
        {
            var apiData = await GetData();

            //Let's get the data from the api and save it to the cache.
            CacheData(cacheKey, apiData);

            await DisplayAlert("debug", "New cache = " + apiData, "OK");
        }
        else
        {
            await DisplayAlert("debug", "Existing cache = "+cachedData, "OK");

        }


    }

    //Here we put all the api data inside of the cache.
    private void CacheData(string cacheKey, JsonElement apiData)
    {
        Barrel.Current.Add(cacheKey, apiData, expireIn:TimeSpan.FromSeconds(5));
    }
}