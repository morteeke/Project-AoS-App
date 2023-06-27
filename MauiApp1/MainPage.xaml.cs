
using MySql.Data.MySqlClient;
using Microsoft.Maui.Controls;
using System.Data;

namespace MauiApp1;

public partial class MainPage : ContentPage
{

	string server = "sql7.freesqldatabase.com";
	string database_name = "sql7628906";
	string db_username = "sql7628906";
	string db_password = "8vYlewVtKE";
    string your_port = "3306";


    public MainPage()
	{
		InitializeComponent();
	}

	private async void OnLogginButtonClicked(object sender, EventArgs e)
	{
        string connectionString = "server="+server+";port="+your_port+";database="+database_name+";uid="+db_username+";password="+db_password+";";

        string getData = CheckForUserAccount(connectionString, UsernameField.Text, PasswordField.Text);
        MyConsole.Text = getData;

        string output;
        if (getData != "")
        {
            output = "You logged in as " + getData;
        }
        else
        {
            output = "invalid username or password";
        }
        await DisplayAlert("User", output, "OK");


        //voor mensen die problemen hebben met zicht. Zo kan de text worden voorgelezen.
        SemanticScreenReader.Announce(CounterBtn.Text);

        //open de eerste page na in te loggen.
        await Navigation.PushModalAsync(new GamemodePage());
	}

	private string CheckForUserAccount(string connectionString, string username, string password)
	{
        string output = "";
        MyConsole.Text = "Make Connection... (" + connectionString + ")";

        MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            // Connection established successfully, perform database operations here
            MyConsole.Text += "\n\nConnection established";

            // Example: execute a query
            string query = "SELECT username FROM User WHERE username = '"+ username + "' and password = '"+password+"'";

            MyConsole.Text += "\n\nStart a query...";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // Read data from the reader
                string column2Value = reader.GetString("username");
                output = column2Value;
                // ...
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            // Handle any potential exceptions or errors
            MyConsole.Text = "Error: " + ex.Message;
        }
        finally
        {
            connection.Close();
        }
        return output;
        
    }
}

