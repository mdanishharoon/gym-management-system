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
    public partial class ControlDietPlan : UserControl
    {
        private string dietplanID;

        public ControlDietPlan()
        {
            InitializeComponent();
            populate();
            PopulateComboBox();
        }
        private void PopulateComboBox()
        {
            comboBox2.DropDownHeight = comboBox1.Font.Height * 7;

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT MealName FROM Meals";

            comboBox2.Items.Clear(); // Clear existing items in the combo box

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
                            string gymName = reader["MealName"].ToString(); // Read the gym name
                            comboBox2.Items.Add(gymName); // Add gym name to the combo box
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
        private void populate() {

            dataGridView1.CellClick += DataGridView1_CellClick;

            // Subscribe to the SelectionChanged event to handle row selection changes

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT DietPlanID as ID,PlanName as Name,PlanObjective as Objective, CreatorType as Creator FROM dbo.dietPlans";
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

             
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
                dietplanID = value.ToString();
                MessageBox.Show("Diet Plan ID: " + dietplanID);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string usertype;
            if (comboBox1.Text == "User") {
                usertype = "User";
            }
            else if (comboBox1.Text == "Trainer")
            {
                usertype = "Trainer";
            }
            else {
                return;
            }
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT DietPlanID as ID,PlanName as Name,PlanObjective as Objective, CreatorType as Creator FROM dbo.dietPlans where CreatorType = '"+ usertype +"'";
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
            string objective;
            if (comboBox5.Text == "Bulking")
            {
                objective = "Bulking";
            }
            else if (comboBox5.Text == "Cutting")
            {
                objective = "Cutting";
            }
            else if (comboBox5.Text == "Maintenance")
            {
                objective = "Maintenance";
            }
            else
            {
                return;
            }
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT DietPlanID as ID,PlanName as Name,PlanObjective as Objective, CreatorType as Creator FROM dbo.dietPlans where PlanObjective = '" + objective + "'";
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

        private void button1_Click(object sender, EventArgs e)
        {
            //insert the dietplanID into the current user
            int user = GlobalVariables.ID;
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "UPDATE members SET DietPlanID = " + dietplanID + " WHERE member_id = " + user.ToString();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            
            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                MessageBox.Show("Diet Plan Assigned Successfully");

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

            string mealname = comboBox2.Text;
            string MealID="";string PlanID="";
            string subquery = "Select MealID FROM Meals WHERE MealName = '" + mealname + "'";
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
                    MealID= reader["MealID"].ToString();

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
                sqlConnection2.Close();
            }

            //get dietplanID
            string query3 = "Select DietPlanID FROM Dietplans where creatorId = " + GlobalVariables.ID.ToString();
            SqlConnection sqlConnection3 = new SqlConnection(connectionString);
            sqlConnection3.Open();
            try
            {
                // SQL query to retrieve data

                // Create SQL command and execute query
                SqlCommand sqlCommand = new SqlCommand(query3, sqlConnection3);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                { 
                    PlanID = reader["DietPlanID"].ToString();

                }
                else
                {
                    MessageBox.Show("Plan not found in database.");
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
            
            string query = "INSERT INTO DietMeals Values ( "+ PlanID +", " + MealID + " )";
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
                MessageBox.Show("Meal Added Successfully");

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string meal = comboBox2.Text;
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT Protein, Carbs, Fat,Allergen FROM Meals WHERE MealName = '" + meal + "'";
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
                    textBox2.Text = reader["Protein"].ToString();
                    textBox3.Text = reader["Carbs"].ToString();
                    textBox4.Text = reader["Fat"].ToString();
                    textBox5.Text = reader["Allergen"].ToString();
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

        private void label2_Click(object sender, EventArgs e)
        {


        }

        private void ControlDietPlan_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string newname = textBox1.Text;
            string userid = GlobalVariables.ID.ToString();
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "Update Dietplans set PlanName = '" + newname+ "' WHERE creatorId = " + userid;
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
                MessageBox.Show("Diet Plan Assigned Successfully");

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

        private void button6_Click(object sender, EventArgs e)
        {
            string newname = comboBox3.Text;
            string userid = GlobalVariables.ID.ToString();
            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "Update Dietplans set PlanObjective = '" + newname + "' WHERE CreatorID = " + userid;
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
                MessageBox.Show("Diet Plan Assigned Successfully");

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
