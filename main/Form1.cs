using MySqlConnector;
using System.Xml.Linq;
using System;
using System.Windows.Forms;


namespace main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstname = textBox1.Text;
            string lastname = textBox2.Text;
            string email = textBox3.Text;
            string password = textBox4.Text;

            if (firstname != "" && password != "" && email != "")
            {
                // Code to add user to database or file goes here
                MessageBox.Show("Sign-up successful!");
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
            // Get the values from the text boxes
            

            // Create a connection to the database
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=root;database=inslap;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // Insert the data into the database
            string query = "INSERT INTO inslap.users (firstname, email,lastname,password) VALUES (@firstname, @email,@lastname,@password)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstname", firstname);
            command.Parameters.AddWithValue("@lastname", lastname);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            command.ExecuteNonQuery();

            // Close the connection
            connection.Close();

            // Display a success message
            MessageBox.Show("Data sent to the database.");

        }
        
    }
}