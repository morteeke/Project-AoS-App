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

        OnLoad();
	}

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private void OnCampaignButtonClicked(object sender, EventArgs e)
    {

        Barrel.Current.EmptyAll();
    }

    private async void OnOpenPlayButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("debug", "open play has been clicked", "OK");
        OnLoad();
    }

    private async void OnLoad()
    {
        data = await DataHandler.Load();
        await DisplayAlert("debug", data, "OK");
    }
}