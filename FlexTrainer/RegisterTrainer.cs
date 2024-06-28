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
using System.Data.Sql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBproj
{
    public partial class RegisterTrainer : Form
    {
        public RegisterTrainer()
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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter a username");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter a password");
                return;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("Please enter an Name");
                return;
            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please Select a gym");
                return;
            }

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            string un = textBox1.Text;
            string pw = textBox2.Text;
            string name = textBox3.Text;
            string gymName = comboBox1.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery1 = "INSERT INTO trainers (username, password, name) " +
                                         "VALUES (@Username, @Password, @Name)";
                    using (SqlCommand command = new SqlCommand(insertQuery1, connection))
                    {
                        command.Parameters.AddWithValue("@Username", un);
                        command.Parameters.AddWithValue("@Password", pw);
                        command.Parameters.AddWithValue("@Name", name);
                        //command.Parameters.AddWithValue("@GymName", gymName);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Trainer record inserted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert trainer record.");
                        }



                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            using (Login login = new Login()) {
                this.Hide();
                login.ShowDialog();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
