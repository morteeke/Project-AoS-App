using Azure;
using MonkeyCache.FileStore;
using System.Text.Json;

namespace MauiApp1;


// 22/07/2023: added caching.

public partial class GamemodePage : ContentPage
{
    string data;

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

        await DisplayAlert("Campaign", "You clicked campaign", "OK");
    }

    private async void OnOpenPlayButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Open Play", "Open play has been clicked", "OK");

        await Navigation.PushAsync(new OpenPlayPage());
    }

    private async void OnLoad()
    {
        data = await DataHandler.LoadCache();
        await DisplayAlert("API call", data, "OK");
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        OnLoad();
    }
}