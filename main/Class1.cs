﻿
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Class1
{
    
    private string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=root;database=inslap;";

    private int Insert(string query)
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        MySqlCommand commandDatabase = new MySqlCommand(query, connection);

        try
        {
            connection.Open();
            int result = commandDatabase.ExecuteNonQuery();
            return (int)commandDatabase.LastInsertedId;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return -1;
    }
}




