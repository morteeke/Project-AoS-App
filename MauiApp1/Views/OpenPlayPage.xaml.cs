using System.Text.Json;

namespace MauiApp1;

public partial class OpenPlayPage : ContentPage
{
	public OpenPlayPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        List<Army> armies = new List<Army>();

		Army army;
		Unit unit;

		JsonElement data = DataHandler.LoadArmy("Sylvanth").GetProperty("data");
		for(int c = 0; c < data.GetArrayLength(); c++)
		{
			unit = new Unit();
			army = new Army(data[c].GetProperty("army").ToString(), unit);

			armies.Add(army);
		}

		string output = "";
		foreach(Army arm in armies)
		{
			output += arm.Name + "\n";
		}

		DisplayAlert("Debug", output, "OK");
    }
}