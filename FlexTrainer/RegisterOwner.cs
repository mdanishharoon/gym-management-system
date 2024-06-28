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

namespace DBproj
{
    public partial class RegisterOwner : Form
    {
        public RegisterOwner()
        {
            InitializeComponent();
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
                MessageBox.Show("Please enter an Gym Name");
                return;
            }


            string username = textBox1.Text;
            string password = textBox2.Text;
            string gymName = textBox3.Text;

            string connectionString = "Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery1 = "INSERT INTO owners (username, password, gymName) " +
                                         "VALUES (@Username, @Password, @GymName)";
                    using (SqlCommand command = new SqlCommand(insertQuery1, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@GymName", gymName);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Owner record inserted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Owner record insertion failed.");
                        }
                    }
                    string insertQuery2 = "INSERT INTO gyms (gymName) " +
                                         "VALUES (@GymName)";

                    using (SqlCommand command = new SqlCommand(insertQuery2, connection))
                    {
                        command.Parameters.AddWithValue("@GymName", gymName);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Gym record inserted successfully.");
                            using(Login login = new Login())
                            {
                                this.Hide();
                                login.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gym record insertion failed.");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
