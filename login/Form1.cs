
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            string firstname = textBox1.Text;
            string password = textBox2.Text;

            // Connection string for MySQL database
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=root;database=inslap;";

            // Query to check if the user exists in the database
            string query = "SELECT COUNT(*) FROM users WHERE firstname = @firstname AND password = @password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstname", firstname);
                    command.Parameters.AddWithValue("@password", password);

                    try
                    {
                        connection.Open();

                        int result = Convert.ToInt32(command.ExecuteScalar());

                        if (result > 0)
                        {
                            // Login successful
                            MessageBox.Show("Login successful.");
                        }
                        else
                        {
                            // Login failed
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void username_Click(object sender, EventArgs e)
        {

        }
    }
}
        