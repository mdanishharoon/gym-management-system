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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (RegisterMember register = new RegisterMember())
            {
                this.Hide();
                register.ShowDialog();
            }
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

            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                using (Admin admin = new Admin())
                {
                    this.Hide();
                    admin.ShowDialog();
                }
            }


            SqlConnection conn = new SqlConnection("Data Source=DANISH\\SQLEXPRESS;Initial Catalog=FlexTrainer;Integrated Security=True;Encrypt=False;");
            conn.Open();
            string un = textBox1.Text;
            string pw = textBox2.Text;
            string table;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    table = "members";
                      break;
                case 1:
                    table = "trainers";
                      break;
                case 2: 
                    table = "owners";
                      break;
                default :
                    table = "admin";
                     break;

            }


            string query = "select * from " + table + " where username = '" + un + "' and password = '" + pw + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            object res = cmd.ExecuteScalar();
            
            //getID of user
            
            if (res != null)
            {
                int userID;
                string query2 = "";
                //save id of the logged in user.
                if (table == "trainers") {
                    query2 = "select id from " + table + " where username = '" + un + "' and password = '" + pw + "'";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    reader.Read();
                    GlobalVariables.ID = int.Parse(reader["ID"].ToString()); //Trainer ID TO BE USED LATER IN THE PROGRAM
                    reader.Close();
                }

                if (table == "members") {
                    query2 = "select member_id  from " + table + " where username = '" + un + "' and password = '" + pw + "'";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    reader.Read();
                    GlobalVariables.ID = int.Parse(reader["member_id"].ToString()); //USER ID TO BE USED LATER IN THE PROGRAM
                    reader.Close();
                }




                MessageBox.Show("Login Successful");
                if (table == "members")
                {
                    using (User member = new User())
                    {
                        this.Hide();
                        member.ShowDialog();
                    }
                }
                else if (table == "trainers")
                {
                    using (Trainer trainer = new Trainer())
                    {
                        this.Hide();
                        trainer.ShowDialog();
                    }
                }
                else if (table == "owners")
                {
                    using (Owner owner = new Owner())
                    {
                        this.Hide();
                        owner.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (RegisterTrainer trainer = new RegisterTrainer())
            {
                this.Hide();
                trainer.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using(RegisterOwner owner = new RegisterOwner())
            {
                this.Hide();
                owner.ShowDialog();
            }
        }
    }
}
