using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBproj
{
    public partial class ControlBooking : UserControl
    {
        public ControlBooking()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            comboBox1.DropDownHeight = comboBox1.Font.Height * 7;

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT Name FROM Trainers";

            comboBox1.Items.Clear(); // Clear existing items in the combo box

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open(); // Open the connection

                        SqlDataReader reader = command.ExecuteReader(); // Execute the query

                        while (reader.Read()) // Loop through the results
                        {
                            string gymName = reader["Name"].ToString(); // Read the gym name
                            comboBox1.Items.Add(gymName); // Add gym name to the combo box
                        }

                        reader.Close(); // Close the reader
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display error message if exception occurs
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string trainername = comboBox1.Text;

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";

            string trainerid = "";
            string subquery = "Select id FROM trainers WHERE Name = '" + trainername + "'";
            SqlConnection sqlConnection2 = new SqlConnection(connectionString);
            sqlConnection2.Open();
            //get meal id
            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(subquery, sqlConnection2);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    // Retrieve values from the reader and display in textboxes
                    // Assuming textbox2, textbox3, and textbox4 are TextBox controls on your form
                    trainerid = reader["id"].ToString();

                }
                else
                {
                    // Handle case where meal name is not found
                    // For example, display an error message
                    MessageBox.Show("Trainer Not found");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection2.Close();
            }
            DateTime selectedDate = monthCalendar1.SelectionStart;



            string query = "INSERT INTO TrainerBookings VALUES (" + trainerid +", "+GlobalVariables.ID +", '" + selectedDate + "')";


            string checkQuery = "SELECT COUNT(*) FROM TrainerBookings " +
                    "WHERE TrainerID = @TrainerID AND ClientID = @ClientID AND AppointmentDate = @BookingDate";

            int existingCount = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the connection
                conn.Open();

                // Create and execute the command to check for existing bookings
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@TrainerID", trainerid);
                    checkCommand.Parameters.AddWithValue("@ClientID", GlobalVariables.ID);
                    checkCommand.Parameters.AddWithValue("@BookingDate", selectedDate);

                    // Execute the scalar query to get the count of existing bookings
                    existingCount = (int)checkCommand.ExecuteScalar();
                }

                // Check the result of the query
                if (existingCount > 0)
                {
                    // Booking already exists, show a message box
                    MessageBox.Show("This date is already booked for the selected trainer.", "Booking Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Booking does not exist, proceed with the insert query
                    string insertQuery = "INSERT INTO TrainerBookings (TrainerID, ClientID, AppointmentDate) " +
                                         "VALUES (@TrainerID, @ClientID, @BookingDate)";

                    // Create and execute the command for inserting the booking
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, conn))
                    {
                        insertCommand.Parameters.AddWithValue("@TrainerID", trainerid);
                        insertCommand.Parameters.AddWithValue("@ClientID", GlobalVariables.ID);
                        insertCommand.Parameters.AddWithValue("@BookingDate", selectedDate);

                        // Execute the insert query
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Insert successful, show success message if needed
                            MessageBox.Show("Booking successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Handle insert failure if needed
                            MessageBox.Show("Failed to add booking.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }

        private void ControlBooking_Load(object sender, EventArgs e)
        {

        }
    }
}
