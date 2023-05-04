using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using MySqlConnector;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace account
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog to allow the user to select an image file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png, *.bmp)|*.jpg;*.png;*.bmp";

            // Show the OpenFileDialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image file into a new Image object
                Image newImage = Image.FromFile(openFileDialog.FileName);

                // Display the new image in a PictureBox or other control
                pictureBox1.Image = newImage;

                // Dispose of the old image if there was one
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Get the old and new passwords from the text boxes
            string oldpassword = textBox1.Text;
            string newPassword = textBox2.Text;

            // Create a connection to the database
            string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = inslap; ";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // Check if the old password is correct
            string query = "SELECT COUNT(*) FROM users WHERE  password = @password";
            MySqlCommand command = new MySqlCommand(query, connection);
           
            command.Parameters.AddWithValue("@password", oldpassword);
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count == 0)
            {
                MessageBox.Show("Old password is incorrect.");
                return;
            }

            // Update the password in the database
            query = "UPDATE users SET password = @password";
            command = new MySqlCommand(query, connection);
           
            command.Parameters.AddWithValue("@password", newPassword);
            command.ExecuteNonQuery();

            // Close the connection
            connection.Close();

            // Display a success message
            MessageBox.Show("Password changed successfully!");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a MySQL connection string
                string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = inslap; ";

                // Create a MySqlConnection object
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Create a SQL query to delete the user
                    string query = "DELETE FROM users WHERE firstname = @firstname  ";

                    // Create a MySqlCommand object
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        
                        // Open the database connection
                        connection.Open();

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Close the database connection
                        connection.Close();

                        // Display a message to the user
                        MessageBox.Show(rowsAffected.ToString() + " user deleted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }
    }
}