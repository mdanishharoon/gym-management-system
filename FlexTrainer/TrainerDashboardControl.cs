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
    public partial class TrainerDashboardControl : UserControl
    {
        public TrainerDashboardControl()
        {
            InitializeComponent();
            PopulateComboBox();

            populateTextBox();

        }


        private void populateTextBox() {
            int trainerID = GlobalVariables.ID;
            string query1 = "SELECT TrainerID, AVG(Rating) AS AverageRating FROM TrainerFeedback WHERE  TrainerID =" + GlobalVariables.ID + " GROUP BY TrainerID; ";
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
                            string  rating = reader["AverageRating"].ToString(); // Read the diet plan name
                            textBox1.Text = rating; // Add diet plan name to the textbox
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
            string query = "SELECT gyms.GymName FROM dbo.gyms";

            comboBox4.Items.Clear(); // Clear existing items in the combo box

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
                            string gymName = reader["GymName"].ToString(); // Read the gym name
                            comboBox4.Items.Add(gymName); // Add gym name to the combo box
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

        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT GymID FROM dbo.gyms WHERE GymName = '"+ comboBox4.Text +"'";
            string gymid = "";
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
                            gymid = reader["GymID"].ToString(); // Read the gym name
                             // Add gym name to the combo box
                        }

                        reader.Close(); // Close the reader
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display error message if exception occurs
            }

            string query2 = "SELECT AVG(tf.Rating) AS AverageRating FROM TrainerFeedback tf JOIN members m ON tf.ClientID = m.member_id JOIN trainers t ON tf.TrainerID = t.id WHERE m.GymId =" + gymid + " AND tf.TrainerID = " + GlobalVariables.ID;
            string gymrating = "0" ;
            
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
                            gymrating = reader["AverageRating"].ToString(); // Read the gym name
                            textBox1.Text = gymrating;
                                // Add gym name to the combo box
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
    }
}
