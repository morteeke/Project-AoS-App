using Azure;
using System.Text.Json;

namespace MauiApp1;

public partial class GamemodePage : ContentPage
{
	public GamemodePage()
	{
		InitializeComponent();
	}

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void OnCampaignButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("debug", "campaign has been clicked", "OK");

        string result = await APIHandler.Request();

        // Parse the JSON response
        JsonDocument jsonDocument = JsonDocument.Parse(result);

        // Access the desired data in the JSON structure
        JsonElement armiesElement = jsonDocument.RootElement.GetProperty("armies");

        await DisplayAlert("debug", armiesElement[1].ToString(), "OK");
    }

    private void OnOpenPlayButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("debug", "open play has been clicked", "OK");
    }

    private async void GetData()
    {

    }
}