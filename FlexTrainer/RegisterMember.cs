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
    public partial class RegisterMember : Form
    {
        public RegisterMember()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            comboBox1.DropDownHeight = comboBox1.Font.Height * 7;

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string query = "SELECT gyms.GymName FROM dbo.gyms";

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
                            string gymName = reader["GymName"].ToString(); // Read the gym name
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


        private void RegisterMember_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Please enter a username");
                return;
            }
            if(textBox2.Text == "")
            {
                MessageBox.Show("Please enter a password");
                return;
            }
            if(textBox3.Text == "")
            {
                MessageBox.Show("Please enter an email");
                return;
            }
            if(textBox4.Text == "")
            {
                MessageBox.Show("Please enter an age");
                return;
            }
            if(textBox5.Text == "")
            {
                MessageBox.Show("Please enter a duration");
                return;
            }




            SqlConnection conn = new SqlConnection("Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;");
            conn.Open();
            string un = textBox1.Text;
            string pw = textBox2.Text;
            string email = textBox3.Text;
            string age = textBox4.Text;
            int duration = int.Parse(textBox5.Text);
            string gymName = comboBox1.Text;

            //GET GYM ID OF SELECTED GYM
            string gymIdquery = "Select GymID from dbo.gyms where GymName = '" + gymName + "'";
            SqlCommand cmd = new SqlCommand(gymIdquery, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int gymId = int.Parse(reader["GymID"].ToString());
            reader.Close();




            string query = "Insert into dbo.members (username, email, password, age,membership, GymId) values('" + un + "','" + email + "','" + pw + "'," + age + "," + duration + "," + gymId + ")";
            SqlCommand cm = new SqlCommand(query, conn);
            if(cm.ExecuteNonQuery()>=1)
            {

                string query2 = "select member_id from members where username = '" + un + "' and password = '" + pw + "'";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                GlobalVariables.ID = int.Parse(reader2["member_id"].ToString());
                reader2.Close();

                string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
                query2 = "INSERT INTO DietPlans (CreatorID, CreatorType) VALUES (@CreatorID, @CreatorType)";
                ////////////CREATE NEW DIET PLAN FOR THE NEWLY REGISTERED MEMBER
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand with the query and connection
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        // Add parameters to the command to avoid SQL injection
                        command.Parameters.AddWithValue("@CreatorID", GlobalVariables.ID.ToString());
                        command.Parameters.AddWithValue("@CreatorType", "User");

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Insert successful");
                        }
                        else
                        {
                            MessageBox.Show("Insert failed");
                        }
                    }
                }

                //////////////CREATE NEW workout PLAN FOR THE NEWLY REGISTERED MEMBER
                string query3 = "INSERT INTO WORKOUTPLANS (PlanName,PlanObjective,CreatorID,CreatorType) VALUES ('User " + GlobalVariables.ID.ToString()+" Plan','Default',@CreatorID, @CreatorType)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand with the query and connection
                    using (SqlCommand command = new SqlCommand(query3, connection))
                    {
                        // Add parameters to the command to avoid SQL injection
                        command.Parameters.AddWithValue("@CreatorID", GlobalVariables.ID.ToString());
                        command.Parameters.AddWithValue("@CreatorType", "User");

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Insert successful");
                        }
                        else
                        {
                            MessageBox.Show("Insert failed");
                        }
                    }
                }

                MessageBox.Show("Member Registered");
                using(Login login = new Login())
                {
                    this.Hide();
                    login.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Error Please Try Again!");
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
