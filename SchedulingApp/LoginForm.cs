using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace SchedulingApp
{
    public partial class LoginForm : Form
    {
        // connection string used to tell MySqlConnection the database login info.
        private string connectionString = "server=localhost;user=root;password=root;database=scheduling_db";
        private bool isSpanish = false;

        public LoginForm()
        {
            InitializeComponent();
        }
        // when the form loads it conats the api and determines the users location.
        private async void Form1_Load(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync("http://ip-api.com/json");
                var serializer = new JavaScriptSerializer();
                var location = serializer.Deserialize<Dictionary<string, string>>(response);

                var city = location["city"];
                var regionName = location["regionName"];
                var country = location["country"];

                locationLabel.Text = $"{city}, {regionName}, {country}";
            }
          

        }
        // When login is clicked this determines if the username and password are in the database. If so main form loads if not error message is displayed.
        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand($"SELECT * FROM user WHERE userName = '{username}' AND password = '{password}'", conn);
                    string status = "";

                    using (var reader = command.ExecuteReader())
                    {

                        if (reader.Read() == true)
                        {
                            status = "Success!";
                            int userId = Convert.ToInt32(reader["userId"]);
                            CheckUpcomingAppoitments(userId);
                            var mainForm = new MainForm(username);
                            mainForm.Show();
                            this.Hide();

                        }
                        else
                        {
                            status = "Failed Login!";
                            errorLabel.Visible = true;
                            if (isSpanish)
                            {
                                errorLabel.Text = "¡El nombre de usuario y la contraseña no coinciden!";
                            }
                            else
                            {
                                errorLabel.Text = "The username and password combination is incorrect!";
                            }
                        }
                    }
                    // writes each login attempt into the Login_History file.
                    using (var writer = new StreamWriter("Login_History.txt", true))
                    {
                        writer.WriteLine($"{DateTime.Now} | {username} | {status}");


                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }
        // checks if the user who logged in has an appoitment within 15 minutes and displays a message if so.
        private void CheckUpcomingAppoitments(int userId)
        {

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand($"SELECT * FROM appointment WHERE userId = {userId} AND start BETWEEN NOW() and DATE_ADD(NOW(), INTERVAL 15 MINUTE)", conn);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            MessageBox.Show($"You have an appoitment in the next 15 minutes!");
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        // language button for switching to Spanish and English.
        private void languageButton_Click(object sender, EventArgs e)
        {
            isSpanish = !isSpanish;

            if (isSpanish)
            {
                usernameLabel.Text = "Usuario:";
                passwordLabel.Text = "Contraseña:";
                loginButton.Text = "Iniciar Sesión";
                languageButton.Text = "English";
            }
            else
            {
                usernameLabel.Text = "Username:";
                passwordLabel.Text = "Password";
                loginButton.Text = "Login";
                languageButton.Text = "Español";
            }

            if (errorLabel.Visible == true)
            {
                if (isSpanish)
                {
                    errorLabel.Text = "¡El nombre de usuario y la contraseña no coinciden!";
                }
                else
                {
                    errorLabel.Text = "The username and password combination is incorrect!";
                }
            }
        }
    }
}
