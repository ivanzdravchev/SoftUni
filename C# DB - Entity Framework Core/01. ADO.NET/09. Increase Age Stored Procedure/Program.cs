using Microsoft.Data.SqlClient;
using System;

namespace _09._Increase_Age_Stored_Procedure
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS01; Database=MinionsDB; Integrated Security=true";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            int inputId = int.Parse(Console.ReadLine());

            string query = "EXEC usp_GetOlder @Id";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("Id", inputId);
            command.ExecuteNonQuery();

            command.CommandText = "SELECT Name, Age FROM Minions WHERE Id = @Id";

            using SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Console.WriteLine($"{reader["Name"]} - {reader["Age"]} years old");
        }
    }
}