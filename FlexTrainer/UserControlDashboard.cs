using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBproj
{
    public partial class UserControlDashboard : UserControl
    {
        public UserControlDashboard()
        {
            InitializeComponent();
            populateTextBoxes();
            PopulateComboBox();
        }

        public void populateTextBoxes() {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;            

            //write a query to get the current Diet Plan for the user
            int userID = GlobalVariables.ID;
            string query1 = "Select d.PlanName FROM members m Join DietPlans d on m.DietPlanID = d.DietPlanID where m.member_id = " + userID;
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            //display the diet plan name in the textbox
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        connection.Open(); // Open the connection

                        SqlDataReader reader = command.ExecuteReader(); // Execute the query

                        while (reader.Read()) // Loop through the results
                        {
                            string dietPlanName = reader["PlanName"].ToString(); // Read the diet plan name
                            textBox1.Text = dietPlanName; // Add diet plan name to the textbox
                        }

                        reader.Close(); // Close the reader
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display error message if exception occurs
            }
            

            string query2 = "Select t.Name FROM members m Join trainers t on m.TrainerID = t.id where m.member_id = " + userID;
            //display the trainer name in the textbox
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        connection.Open(); // Open the connection

                        SqlDataReader reader = command.ExecuteReader(); // Execute the query

                        while (reader.Read()) // Loop through the results
                        {
                            string trainerName = reader["name"].ToString(); // Read the trainer name
                            textBox2.Text = trainerName; // Add trainer name to the textbox
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

        private void button1_Click(object sender, EventArgs e)
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


            string rating = comboBox2.Text;
            string clientid = GlobalVariables.ID.ToString();
            //Enter into ClientTrainerFeedback table
            string query = "INSERT INTO TrainerFeedback  VALUES ('" + clientid + "', '" + trainerid + "', '" + rating + "')";
            sqlConnection2 = new SqlConnection(connectionString);
            sqlConnection2.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection2);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Feedback Submitted");
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
        }
    }
}
