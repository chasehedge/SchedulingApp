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
using System.Configuration;

namespace SchedulingApp

{
    public partial class MainForm : Form
    {
        private int selectedCustomerId;
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        private string currentUser;
        private int currentUserId;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string username, int userId)
        {
            InitializeComponent();
            currentUser = username;
            currentUserId = userId;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        // when the main form loads we call the LoadCustomers method to populate table
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadAppointments();
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
                        $"VALUES ('{country}', NOW(), '{currentUser}', NOW(), '{currentUser}')", conn);
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
                    LoadCustomerComboBox();

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
                    LoadCustomerComboBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }
        // populates text fields when user clicks on customer
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
                MessageBox.Show("Please select a customer to update", "Error");
                return;
            }

            // grabs the addressId and CustomerId from selected rows to apply updated info
            int customerId = Convert.ToInt32(customersTable.SelectedRows[0].Cells["customerId"].Value);
            int addressId = Convert.ToInt32(customersTable.SelectedRows[0].Cells["addressId"].Value);

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
                        $"WHERE addressId = {addressId}", conn);
                    addressCommand.ExecuteNonQuery();

                    // refresh table info after performing update
                    LoadCustomers();
                    LoadCustomerComboBox();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        // populates the appoitments DataGridVeiw (table) with the data, can be called again to refresh
        private void LoadAppointments()
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

                    // loops through each row and converts the values to local time to display correctly for the user in the table (or else it will show UTC time)
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DateTime utcStart = DateTime.SpecifyKind((DateTime)row["start"], DateTimeKind.Utc);
                        DateTime utcEnd = DateTime.SpecifyKind((DateTime)row["end"], DateTimeKind.Utc);
                        row["start"] = utcStart.ToLocalTime();
                        row["end"] = utcEnd.ToLocalTime();
                    }


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
                    appointmentCustomerComboBox.ValueMember = "customerId";
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
            // convert start time and end time to EST to check against the business hours

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

            // convert times back to UTC, you want to always store your times in UTC to keep it consistent
            DateTime utcStart = start.ToUniversalTime();
            DateTime utcEnd = end.ToUniversalTime();


            // validation for overlapping appointments

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var overlapCommand = new MySqlCommand(
                        $"SELECT COUNT(*) FROM appointment WHERE start < '{utcEnd:yyyy-MM-dd HH:mm:ss}' " +
                        $"AND end > '{utcStart:yyyy-MM-dd HH:mm:ss}'", conn);

                    long overlapCount = (long)overlapCommand.ExecuteScalar();

                    if (overlapCount > 0)
                    {
                        MessageBox.Show("This appointment overlaps with an existing appointment", "Validation Error");
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }


            // insert statement to add info to database after all validations have passed

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand(
                        $"INSERT INTO appointment (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                        $"VALUES ({selectedCustomerId}, {currentUserId}, '{title}', '', '', '', '{type}', '', " +
                        $"'{utcStart:yyyy-MM-dd HH:mm:ss}', '{utcEnd:yyyy-MM-dd HH:mm:ss}', NOW(), '{currentUser}', NOW(), '{currentUser}')", conn);
                    command.ExecuteNonQuery();

                    // refresh table to see updated info
                    LoadAppointments();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void appointmentDeleteButton_Click(object sender, EventArgs e)
        {
            //check if user has selected a row to delete
            if (appointmentsTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an apointment to delete", "Error");
                return;
            }

            //generate message asking if user wants to delete selected appointment
            var confirm = MessageBox.Show("Are you sure you want to delete the selected appointment?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            // get ID of selected appointment and delete it
            int appointmentId = Convert.ToInt32(appointmentsTable.SelectedRows[0].Cells["appointmentId"].Value);

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand($"DELETE FROM appointment WHERE appointmentId = {appointmentId}", conn);

                    command.ExecuteNonQuery();

                    //refresh appointment table
                    LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void appointmentsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // returns function if user clicks something else to prevent code break
            if (e.RowIndex < 0)
            {
                return;
            }

            appointmentTitleTextBox.Text = appointmentsTable.Rows[e.RowIndex].Cells["title"].Value.ToString();
            appointmentTypeTextBox.Text = appointmentsTable.Rows[e.RowIndex].Cells["type"].Value.ToString();
            appointmentCustomerComboBox.SelectedValue = appointmentsTable.Rows[e.RowIndex].Cells["customerId"].Value;
            appointmentStartDateTimePicker.Value = (DateTime)appointmentsTable.Rows[e.RowIndex].Cells["start"].Value;
            appointmentEndDateTimePicker.Value = (DateTime)appointmentsTable.Rows[e.RowIndex].Cells["end"].Value;
        }

        private void appointmentUpdateButton_Click(object sender, EventArgs e)
        {
            //check if user has selected a row to delete
            if (appointmentsTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an apointment to update", "Error");
                return;
            }

            // get ID of selected appointment
            int appointmentId = Convert.ToInt32(appointmentsTable.SelectedRows[0].Cells["appointmentId"].Value);

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
            // convert start time and end time to EST to check against the business hours

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

            // convert times back to UTC, you want to always store your times in UTC to keep it consistent
            DateTime utcStart = start.ToUniversalTime();
            DateTime utcEnd = end.ToUniversalTime();

            // validation for overlapping appointments

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var overlapCommand = new MySqlCommand(
                        $"SELECT COUNT(*) FROM appointment WHERE start < '{utcEnd:yyyy-MM-dd HH:mm:ss}' " +
                        $"AND end > '{utcStart:yyyy-MM-dd HH:mm:ss}' AND appointmentId != {appointmentId}", conn);

                    long overlapCount = (long)overlapCommand.ExecuteScalar();

                    if (overlapCount > 0)
                    {
                        MessageBox.Show("This appointment overlaps with an existing appointment", "Validation Error");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }


            // update statement to add info to database after all validations have passed

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand(
                        $"UPDATE appointment SET customerId = {selectedCustomerId}, title = '{title}', type = '{type}', " +
                        $"start = '{utcStart:yyyy-MM-dd HH:mm:ss}', end = '{utcEnd:yyyy-MM-dd HH:mm:ss}', " +
                        $"lastUpdate = NOW(), lastUpdateBy = '{currentUser}' " +
                        $"WHERE appointmentId = {appointmentId}", conn);


                    command.ExecuteNonQuery();

                    // refresh table to see updated info
                    LoadAppointments();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }


        // populates the calendarDataGridView with the appointments from the day selected on the calendar
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {

            // grabbing the selected date to know which date the user wants to see appointments from
            DateTime selectedDate = monthCalendar.SelectionStart;

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand($"SELECT customer.customerName, title, type, start, end FROM appointment " +
                        $"JOIN customer on appointment.customerId = customer.customerId " +
                        $"WHERE userId = {currentUserId} AND DATE(start) = '{selectedDate:yyyy-MM-dd}'", conn);

                    var dataTable = new DataTable();
                    var adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    // The DateTime has a property called kind, if we do not specify that the kind is UTC then it will confuse to translation method to local time
                    // loop through the rows in datagridview and convert them to local user time for display
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DateTime utcStart = DateTime.SpecifyKind((DateTime)row["start"], DateTimeKind.Utc);
                        DateTime utcEnd = DateTime.SpecifyKind((DateTime)row["end"], DateTimeKind.Utc);
                        row["start"] = utcStart.ToLocalTime();
                        row["end"] = utcEnd.ToLocalTime();
                    }

                    calendarDataGridView.DataSource = dataTable;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // gathering SQL data into a list to then apply LINQ and lambda expressions
        private List<Appointment> GetAllAppointments()
        {
            var list = new List<Appointment>();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new MySqlCommand(
                        "SELECT a.appointmentId, a.customerId, a.userId, c.customerName, a.title, a.type, a.start, a.end " +
                        "FROM appointment a JOIN customer c ON a.customerId = c.customerId", conn);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var appt = new Appointment();
                            appt.AppointmentId = Convert.ToInt32(reader["appointmentId"]);
                            appt.CustomerId = Convert.ToInt32(reader["customerId"]);
                            appt.UserId = Convert.ToInt32(reader["userId"]);
                            appt.CustomerName = reader["customerName"].ToString();
                            appt.Title = reader["title"].ToString();
                            appt.Type = reader["type"].ToString();
                            appt.Start = DateTime.SpecifyKind((DateTime)reader["start"], DateTimeKind.Utc).ToLocalTime();
                            appt.End = DateTime.SpecifyKind((DateTime)reader["end"], DateTimeKind.Utc).ToLocalTime();

                            list.Add(appt);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            return list;
        }

        // report button to generate type of appointments by month
        private void typesReportButton_Click(object sender, EventArgs e)
        {
            // call GetAllAppointments to get the list
            var appointments = GetAllAppointments();
            reportTextBox.Clear();

            // use LINQ with lambda to group year, month and type
            var grouped = appointments
                .GroupBy(a => new { a.Start.Year, a.Start.Month, a.Type })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .ThenBy(g => g.Key.Type);

            reportTextBox.AppendText("Type of appointments per month\n");
            reportTextBox.AppendText("==========================\n\n");

            // loop through groups and write to the RichTextBox
            foreach (var group in grouped)
            {
                reportTextBox.AppendText($"{group.Key.Year}-{group.Key.Month:D2} | {group.Key.Type}: {group.Count()}\n");
            }


        }
        // report button to generate schedule for each user
        private void scheduleReportButton_Click(object sender, EventArgs e)
        {
            // call GetAllAppointments to get the list
            var appointments = GetAllAppointments();
            reportTextBox.Clear();

            // use LINQ with lambda to group userId 
            var grouped = appointments
                .GroupBy(a => a.UserId)
                .OrderBy(g => g.Key);

            reportTextBox.AppendText("Schedule for each user\n");
            reportTextBox.AppendText("==========================\n\n");

            // nested foreach loop, list appointments under its user
            foreach (var group in grouped)
            {
                reportTextBox.AppendText($"User ID: {group.Key}\n");

                var userAppointments = group.OrderBy(a => a.Start);

                foreach (var appt in userAppointments)
                {

                    reportTextBox.AppendText($"  {appt.Start:g} - {appt.End:g} | {appt.Title} ({appt.Type}) for {appt.CustomerName}\n");
                }

                reportTextBox.AppendText("\n");
            }
        }
        // report button to generate ammount of appointments per customer
        private void appointmentsReportButton_Click(object sender, EventArgs e)
        {
            // call GetAllAppointments to get the list
            var appointments = GetAllAppointments();
            reportTextBox.Clear();

            // use LINQ with lambda to group CustomerName
            var grouped = appointments
                .GroupBy(a => a.CustomerName)
                .OrderByDescending(g => g.Key);

            reportTextBox.AppendText("Appointments per Customer\n");
            reportTextBox.AppendText("==========================\n\n");

            foreach (var group in grouped)
            {
                reportTextBox.AppendText($"{group.Key}: {group.Count()} appointment(s)\n");
            }
        }
    }
}



