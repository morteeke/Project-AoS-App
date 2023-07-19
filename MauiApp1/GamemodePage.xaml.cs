using Azure;
using System.Text.Json;
using System.Runtime.Caching;

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

        OnLoad();
    }

    private async void OnOpenPlayButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("debug", "open play has been clicked", "OK");

        FileOperations fileOperations = new FileOperations();
        var result = await fileOperations.ReadFile();
        await DisplayAlert("debug", result.ToString(), "OK");
    }

    private async Task<JsonElement> GetData()
    {

        string result = await APIHandler.Request();

        // Parse the JSON response
        JsonDocument jsonDocument = JsonDocument.Parse(result);

        // Access the desired data in the JSON structure
        JsonElement rootElement = jsonDocument.RootElement;

        return rootElement;
    }
    private async void OnLoad()
    {
        string cacheKey = "my_api_data";
        ObjectCache cache = MemoryCache.Default;

        var cachedData = cache[cacheKey] as string;

        string output = "1";

        //If there is no data in the cache.
        if (cachedData == null)
        {
            //Fetch from api

            output = "0";

            var apiData = await GetData();

            var countJsonElement = apiData.GetProperty("count");
            var dataJsonElement = apiData.GetProperty("data");

            await DisplayAlert("debug", "blablabla= " + countJsonElement, "OK");
            await DisplayAlert("Debug", "Data: " + dataJsonElement[0].GetProperty("army"), "OK");

            var cachePolicy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) };
            cache.Add(cacheKey, apiData, cachePolicy);

            cachedData = apiData.ToString();
        }

        await DisplayAlert("debug", output + cachedData, "OK");
    }
}