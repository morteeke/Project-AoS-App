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

    private void OnCampaignButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("debug", "campaign has been clicked", "OK");
    }

    private void OnOpenPlayButtonClicked(object sender, EventArgs e)
    {
        DisplayAlert("debug", "open play has been clicked", "OK");
    }
}