using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp

{
    public partial class MainForm : Form
    {
        private int selectedCustomerId;
        private string connectionString = "server=localhost;user=root;password=root;database=scheduling_db";
        private string currentUser;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string username)
        {
            InitializeComponent();
            currentUser = username;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        // when the main form loads we call the LoadCustomers method to populate table
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            Loadappointments();
            LoadCustomerComboBox();
        }
        // add button for customers tab , includes validation for non empty fields, trimmed fields, and phone number only allows digits and -.
        private void addButton_Click(object sender, EventArgs e)
        {

            // Gather text fields as variables and trims them.
            var customerName = customerNameTextBox.Text.Trim();
            var address = addressTextBox.Text.Trim();
            var phoneNumber = phoneNumberTextBox.Text.Trim();

            // Checks if all fields are filled and if not displays error message
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(address) || 
            string.IsNullOrEmpty(phoneNumber))

            {
                MessageBox.Show("All fields are required", "Validation Error");
                return;
            }

            // Checks if the phone number only contains digits and dashes. If it contains other chars then returns an error message.
            if (!Regex.IsMatch(phoneNumber, @"^[0-9\-]+$"))
            {
                MessageBox.Show("Phone number can only contain digits and dashes", "Validation Error");
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // placeholder just to fill database
                    string city = "N/A";
                    string postalCode = "0000";
                    string country = "United States";

                    // INSERT statements into database after validations have been ran

                    var countryCommand = new MySqlCommand($"INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                        $"VALUES ('{country}', NOW(), '{currentUser}', NOW(), '{currentUser}')" , conn);
                    countryCommand.ExecuteNonQuery();
                    long countryId = countryCommand.LastInsertedId;

                    var cityCommand = new MySqlCommand($"INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                        $"VALUES ('{city}', {countryId},  NOW(), '{currentUser}', NOW(), '{currentUser}')", conn);
                    cityCommand.ExecuteNonQuery();
                    long cityId = cityCommand.LastInsertedId;

                    var addressCommand = new MySqlCommand($"INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                        $"VALUES ('{address}', ' ' , {cityId}, '{postalCode}', '{phoneNumber}', NOW(), '{currentUser}', NOW(), '{currentUser}')", conn);
                    addressCommand.ExecuteNonQuery();
                    long addressId = addressCommand.LastInsertedId;

                    var customerCommand = new MySqlCommand($"INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                        $"VALUES ('{customerName}', {addressId}, 1, NOW(), '{currentUser}', NOW(), '{currentUser}')", conn);
                    customerCommand.ExecuteNonQuery();
                    long customerId = customerCommand.LastInsertedId;

                    // calling LoadCustomers again to refresh the table after the new data has been added
                    LoadCustomers();

                    // refreshing text fields to allow more data to be added smoothly
                    customerNameTextBox.Clear();
                    addressTextBox.Clear();
                    phoneNumberTextBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // populates the customers DataGridVeiw (table) with the data, can be called again to refresh
        private void LoadCustomers()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand("SELECT customer.addressId, customer.customerId, customer.customerName, address.address, address.phone FROM customer " +
                        "JOIN address on customer.addressId = address.addressId", conn);

                    var dataTable = new DataTable();
                    var adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    customersTable.DataSource = dataTable;

                    // Hides the customerId column. Need the ID for deletion and update but dont want it to show in the grid.
                    // Also hiding the address column same reason
                    customersTable.Columns["customerId"].Visible = false;
                    customersTable.Columns["addressId"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // check if user has selected a row to delete.
            if (customersTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Error");
                return;
            }
            // pop up message asking if user wants to delete selected row.
            var confirm = MessageBox.Show("Are you sure you want to delete selected customer?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            // get ID of selected row and delete it.
            int customerId = Convert.ToInt32(customersTable.SelectedRows[0].Cells["customerId"].Value);

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand($"DELETE FROM customer WHERE customerId = {customerId}", conn);
                    command.ExecuteNonQuery();

                    // refresh data after deletion
                    LoadCustomers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void customersTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // returns the method if user clicks something unexpected to brevent code break
            if (e.RowIndex < 0)
            {
                return;
            }

            customerNameTextBox.Text = customersTable.Rows[e.RowIndex].Cells["customerName"].Value.ToString();
            phoneNumberTextBox.Text = customersTable.Rows[e.RowIndex].Cells["phone"].Value.ToString();
            addressTextBox.Text = customersTable.Rows[e.RowIndex].Cells["address"].Value.ToString();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
         
            // make sure user has selected a customer to update - display error if not
            if (customersTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customerr to update", "Error");
                return;
            }

            // grabs the addressId and CustomerId from selected rows to apply updated info
            int customerId = Convert.ToInt32(customersTable.SelectedRows[0].Cells["customerId"].Value);
            int addressId = Convert.ToInt32(customersTable.SelectedRows [0].Cells["addressId"].Value);    

            // Gather text fields as variables and trims them.
            var customerName = customerNameTextBox.Text.Trim();
            var address = addressTextBox.Text.Trim();
            var phoneNumber = phoneNumberTextBox.Text.Trim();

            // Checks if all fields are filled and if not displays error message
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(address) ||
            string.IsNullOrEmpty(phoneNumber))

            {
                MessageBox.Show("All fields are required", "Validation Error");
                return;
            }

            // Checks if the phone number only contains digits and dashes. If it contains other chars then returns an error message.
            if (!Regex.IsMatch(phoneNumber, @"^[0-9\-]+$"))
            {
                MessageBox.Show("Phone number can only contain digits and dashes", "Validation Error");
                return;
            }

            // perform the updating of the info in the database
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var customerIdCommand = new MySqlCommand($"UPDATE customer SET customerName = '{customerName}', lastUpdate = NOW(), " +
                        $"lastUpdateBy = '{currentUser}' WHERE customerId = {customerId}", conn);
                    customerIdCommand.ExecuteNonQuery();

                    var addressCommand = new MySqlCommand($"UPDATE address SET address = '{address}', lastUpdate = NOW(), lastUpdateBy = '{currentUser}', phone = '{phoneNumber}' " +
                        $"WHERE addressId = {addressId}" , conn);
                    addressCommand.ExecuteNonQuery();

                    // refresh table info after performing update
                    LoadCustomers();

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        // populates the appoitments DataGridVeiw (table) with the data, can be called again to refresh
        private void Loadappointments()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand("SELECT appointment.appointmentId, appointment.customerId, customer.customerName, appointment.title, " +
                        "appointment.type, appointment.start, appointment.end FROM appointment JOIN customer on appointment.customerId = customer.customerId", conn);

                    var dataTable = new DataTable();
                    var adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    appointmentsTable.DataSource = dataTable;

                    // hiding appointmentId and customerId only used for linking dont need to show it
                    appointmentsTable.Columns["customerId"].Visible = false;
                    appointmentsTable.Columns["appointmentId"].Visible = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }
        // used to fill in the customer combo box on the appoinments tab
        private void LoadCustomerComboBox()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand("SELECT customerId, customerName FROM customer", conn);
                    var dataTable = new DataTable();
                    var adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    appointmentCustomerComboBox.DataSource = dataTable;
                    appointmentCustomerComboBox.DisplayMember = "customerName";
                    appointmentCustomerComboBox.ValueMember = "customerName";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void appointmentAddButton_Click(object sender, EventArgs e)
        {
            // grabbing text field values and trimming them

            var title = appointmentTitleTextBox.Text.Trim();
            var type = appointmentTypeTextBox.Text.Trim();
            int selectedCustomerId = Convert.ToInt32(appointmentCustomerComboBox.SelectedValue);
            DateTime start = appointmentStartDateTimePicker.Value;
            DateTime end = appointmentEndDateTimePicker.Value;

            // validation to check if title / type are empty

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Title and Type fields are required.", "Validation Error");
                return;
            }

            // validation to check if start time is before end time to prevent errors

            if (end <= start)
            {
                MessageBox.Show("End time must be after start time", "Validation Error");
                return;
            }

            // get EST time to convert from local user time to EST
            // convert start time and end time to EST

            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime estStart = TimeZoneInfo.ConvertTime(start, TimeZoneInfo.Local, estZone);
            DateTime estEnd = TimeZoneInfo.ConvertTime(end, TimeZoneInfo.Local, estZone);
            
            // validation for monday - friday
            if (estStart.DayOfWeek == DayOfWeek.Saturday || estStart.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Appointments must be schedules Monday through Friday.", "Validation Error");
                return;
            }

            //validation for business hours only 9am - 5pm
            if (estStart.Hour < 9 || estEnd.Hour > 17 || (estEnd.Hour == 17 && estEnd.Minute > 0))
            {
                MessageBox.Show("Appointments must be between 9:00 am and 5:00 pm EST.", "Validation Error");
                return;
            }
            
        }
    }
}


