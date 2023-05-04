using MySqlConnector;
using System;
namespace home
    
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

            try
            {
                // Create a MySQL connection string
                string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = inslap; "; 

                // Create a MySqlConnection object
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Create a SQL query to retrieve the user's balance
                    string query = "SELECT amountofmoney FROM users WHERE firstname = @firstname";

                    // Create a MySqlCommand object
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        

                        // Open the database connection
                        connection.Open();

                        // Execute the query and get the result
                        object result = command.ExecuteScalar();

                        // Close the database connection
                        connection.Close();

                        // Display the user's balance in a label
                        balance.Text = "Balance: " + result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                // Create a MySQL connection string
                string connectionString = "datasource = 127.0.0.1; port = 3306; username = root; password = root; database = inslap; ";

                // Create a MySqlConnection object
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Start a transaction to ensure atomicity
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Retrieve the sender's account information
                        string selectSenderQuery = "SELECT * FROM users WHERE firstname = @firstname AND lastname = @lastname FOR UPDATE";
                        MySqlCommand selectSenderCommand = new MySqlCommand(selectSenderQuery, connection);
                        selectSenderCommand.Parameters.AddWithValue("@firstName", textBox4.Text);
                        selectSenderCommand.Parameters.AddWithValue("@lastName", textBox5.Text);
                        MySqlDataReader senderReader = selectSenderCommand.ExecuteReader();
                        
                        // Check if the sender exists and has enough balance
                        if (!senderReader.HasRows)
                        {
                            MessageBox.Show("Sender does not exist.");
                            senderReader.Close();
                            return;
                        }

                        senderReader.Read();
                        decimal senderBalance = senderReader.GetDecimal("balance");
                        if (senderBalance < numericUpDown1.Value) 
                        {
                            MessageBox.Show("Sender does not have enough balance.");
                            senderReader.Close();
                            return;
                        }

                        int senderAccountId = senderReader.GetInt32("id");
                        senderReader.Close();

                        // Retrieve the recipient's account information
                        string selectRecipientQuery = "SELECT * FROM users WHERE firstname = @firstname AND lastname = @lastname FOR UPDATE";
                        MySqlCommand selectRecipientCommand = new MySqlCommand(selectRecipientQuery, connection);
                        selectRecipientCommand.Parameters.AddWithValue("@firstname", textBox1.Text);
                        selectRecipientCommand.Parameters.AddWithValue("@lastname", textBox2.Text);
                        MySqlDataReader recipientReader = selectRecipientCommand.ExecuteReader();

                        // Check if the recipient exists
                        if (!recipientReader.HasRows)
                        {
                            MessageBox.Show("Recipient does not exist.");
                            recipientReader.Close();
                            return;
                        }

                        recipientReader.Read();
                        int recipientAccountId = recipientReader.GetInt32("id");
                        recipientReader.Close();

                        // Update the sender's balance
                        string updateSenderQuery = "UPDATE users SET amountofmoney = amountofmoney - @amount WHERE firstname = @firstame";
                        MySqlCommand updateSenderCommand = new MySqlCommand(updateSenderQuery, connection);
                        updateSenderCommand.Parameters.AddWithValue("@amount", numericUpDown1.Value);
                        updateSenderCommand.Parameters.AddWithValue("@firstname", senderAccountId);
                        updateSenderCommand.ExecuteNonQuery();

                        // Update the recipient's balance
                        string updateRecipientQuery = "UPDATE users SET amountofmoney = amountofmoney + @amount WHERE firstname = @firstname";
                        MySqlCommand updateRecipientCommand = new MySqlCommand(updateRecipientQuery, connection);
                        updateRecipientCommand.Parameters.AddWithValue("@amount", numericUpDown1.Value);
                        updateRecipientCommand.Parameters.AddWithValue("@firstname", recipientAccountId);
                        updateRecipientCommand.ExecuteNonQuery();

                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Transfer successful.");
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction if an exception occurs
                        transaction.Rollback();
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        // Close the database connection
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the database connection process
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}