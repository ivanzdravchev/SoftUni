using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _07._Print_All_Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS01; Database=MinionsDB; Integrated Security=true";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SELECT Name FROM Minions", connection);
            using SqlDataReader reader = command.ExecuteReader();

            List<string> names = new List<string>();
            while (reader.Read())
            {
                names.Add((string)reader["Name"]);
            }

            for (int i = 0; i < names.Count / 2; i++)
            {
                Console.WriteLine(names[i]);
                Console.WriteLine(names[names.Count - i - 1]);
            }

            if (names.Count % 2 == 1)
            {
                Console.WriteLine(names[names.Count / 2]);
            }
        }
    }
}