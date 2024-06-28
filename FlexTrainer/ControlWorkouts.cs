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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBproj
{
    public partial class ControlWorkouts : UserControl
    {
        private string workoutplanID;

        public ControlWorkouts()
        {
            InitializeComponent();
            populate();
            PopulateComboBox();
        }
        private void populate()
        {
            // Subscribe to the SelectionChanged event to handle row selection changes
            dataGridView1.CellClick += DataGridView1_CellClick;

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT PlanID as ID,PlanName as Name, PlanObjective as Objective, CreatorType as Creator FROM dbo.WorkoutPlans";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            //
            try
            {
                // SQL query to retrieve data
                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                // Create DataTable to hold query results
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);  // Load data into DataTable
                // Close SqlDataReader and SqlCommand
                reader.Close();
                sqlCommand.Dispose();

                dataGridView1.RowHeadersVisible = false;

                // Bind DataTable to DataGridView
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["ID"].Width = 20;
                dataGridView1.Columns["Name"].Width = 100;
                dataGridView1.Columns["Objective"].Width = 75;
                dataGridView1.Columns["Creator"].Width = 50;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection.Close();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PopulateComboBox()
        {
            comboBox1.DropDownHeight = comboBox1.Font.Height * 7;

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT ExerciseName FROM Exercises";

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
                            string gymName = reader["ExerciseName"].ToString(); // Read the gym name
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


        private void button5_Click(object sender, EventArgs e)
        {
            string newname = textBox1.Text;
            string userid = GlobalVariables.ID.ToString();
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "Update WorkoutPlans set PlanName = '" + newname + "' WHERE creatorId = " + userid;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            //
            try
            {
                // SQL query to retrieve data
                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                MessageBox.Show("Workout PLan Name changed  ");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string usertype;
            if (comboBox1.Text == "User")
            {
                usertype = "User";
            }
            else if (comboBox1.Text == "Trainer")
            {
                usertype = "Trainer";
            }
            else
            {
                return;
            }
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT PlanID as ID,PlanName as Name, PlanObjective as Objective, CreatorType as Creator FROM dbo.WorkoutPlans where CreatorType = '" + usertype + "'";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            //
            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                // Create DataTable to hold query results
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);  // Load data into DataTable
                // Close SqlDataReader and SqlCommand
                reader.Close();
                sqlCommand.Dispose();

                dataGridView1.RowHeadersVisible = false;

                // Bind DataTable to DataGridView
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["ID"].Width = 20;
                dataGridView1.Columns["Name"].Width = 100;
                dataGridView1.Columns["Objective"].Width = 75;
                dataGridView1.Columns["Creator"].Width = 50;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string usertype;
            string objective;
            if (comboBox5.Text == "Strength")
            {
                objective = "Strength";
            }
            else if (comboBox5.Text == "Bodybuilding")
            {
                objective = "Bodybuilding";
            }
            else if (comboBox5.Text == "Calisthenics")
            {
                objective = "Calisthenics";
            }
            else
            {
                return;
            }
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT PlanID as ID,PlanName as Name, PlanObjective as Objective, CreatorType as Creator FROM dbo.WorkoutPlans where PlanObjective = '" + objective + "'";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            //
            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                // Create DataTable to hold query results
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);  // Load data into DataTable
                // Close SqlDataReader and SqlCommand
                reader.Close();
                sqlCommand.Dispose();

                dataGridView1.RowHeadersVisible = false;

                // Bind DataTable to DataGridView
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["ID"].Width = 20;
                dataGridView1.Columns["Name"].Width = 100;
                dataGridView1.Columns["Objective"].Width = 75;
                dataGridView1.Columns["Creator"].Width = 50;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection.Close();
            }

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the DataGridView instance
                DataGridView dataGridView = sender as DataGridView;

                // Get the value from the first column (index 0) of the clicked row
                object value = dataGridView.Rows[e.RowIndex].Cells[0].Value;

                // Check if the value is not null
                workoutplanID = value.ToString();
                MessageBox.Show("Diet Plan ID: " + workoutplanID);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //insert the dietplanID into the current user
            int user = GlobalVariables.ID;
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "UPDATE members SET WorkoutPlanID = " + workoutplanID + " WHERE member_id = " + user.ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                MessageBox.Show("Workout Assigned Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection.Close();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string exname = comboBox4.Text;
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT MuscleGroup, Machine FROM Exercises WHERE ExerciseName = '" + exname + "'";
            
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            //
            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    // Retrieve values from the reader and display in textboxes
                    // Assuming textbox2, textbox3, and textbox4 are TextBox controls on your form
                    textBox2.Text = reader["MuscleGroup"].ToString();
                    textBox3.Text = reader["Machine"].ToString();

                }
                else
                {
                    // Handle case where meal name is not found
                    // For example, display an error message
                    MessageBox.Show("Meal not found in database.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";

            string ExName = comboBox4.Text;
            string ExID = ""; string PlanID = "";
            string subquery = "Select ExerciseID FROM Exercises WHERE ExerciseName = '" + ExName + "'";
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
                    ExID = reader["ExerciseID"].ToString();

                }
                else
                {
                    // Handle case where meal name is not found
                    // For example, display an error message
                    MessageBox.Show("Workout not found in database.");
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

            string subquery2 = "Select PlanID FROM WorkoutPlans WHERE CreatorID = '" + GlobalVariables.ID.ToString() + "'";
            SqlConnection sqlConnection3 = new SqlConnection(connectionString);
            sqlConnection3.Open();
            //get meal id
            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(subquery2, sqlConnection3);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    // Retrieve values from the reader and display in textboxes
                    // Assuming textbox2, textbox3, and textbox4 are TextBox controls on your form
                    PlanID = reader["PlanID"].ToString();

                }
                else
                {
                    // Handle case where meal name is not found
                    // For example, display an error message
                    MessageBox.Show("Workout not found in database.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection3.Close();
            }


            string query = "INSERT INTO WorkoutExercises Values ( " + PlanID + ", " + ExID + ", " + comboBox6.Text +" , " + comboBox3.Text+")";
            MessageBox.Show(query);
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            //
            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                MessageBox.Show("Exercise Added Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close SQL connection
                sqlConnection.Close();
            }
        }
    }
}
