
using MySql.Data.MySqlClient;
using Microsoft.Maui.Controls;
using System.Data;

namespace MauiApp1;

public partial class MainPage : ContentPage
{

    //Everything we need to connect to the database.
	string server = "sql7.freesqldatabase.com";
	string database_name = "sql7633110";
	string db_username = "sql7633110";
	string db_password = "MT7D39jLxq";
    string your_port = "3306";


    public MainPage()
	{
		InitializeComponent();
	}

	private void OnLoginButtonClicked(object sender, EventArgs e)
	{
        

        string connectionString = "server="+server+";port="+your_port+";database="+database_name+";uid="+db_username+";password="+db_password+";";

        string getData = CheckForUserAccount(connectionString, UsernameField.Text, PasswordField.Text);
        MyConsole.Text = getData;

        if (getData != "")
        {

            //open de eerste page met AppShell class.
            //App.Current.MainPage = new AppShell();

        }
        else
        {
            string output = "invalid username or password";
            //DisplayAlert("User", output, "OK");
        }

        App.Current.MainPage = new AppShell();

        //voor mensen die problemen hebben met zicht. Zo kan de text worden voorgelezen.
        SemanticScreenReader.Announce(LoginBtn.Text);
    }

    //We will start an sql query looking for the given username and passsword.
	private string CheckForUserAccount(string connectionString, string username, string password)
	{
        string output = "";

        //For debugging purpose.
        MyConsole.Text = "Make Connection... (" + connectionString + ")";

        MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            // Connection established successfully, perform database operations here
            MyConsole.Text += "\n\nConnection established";

            //Execute a query
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

