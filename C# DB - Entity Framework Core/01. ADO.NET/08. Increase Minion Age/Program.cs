using Microsoft.Data.SqlClient;
using System;
using System.Text.RegularExpressions;

namespace _08._Increase_Minion_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS01; Database=MinionsDB; Integrated Security=true";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // Avoid SQL injection since i wanted to use WHERE IN
            Regex regex = new Regex(@"\d+");
            MatchCollection ids = regex.Matches(Console.ReadLine());
            
            string query =
                @$"UPDATE Minions
                  SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                  WHERE Id IN ({string.Join(", ", ids)})";
            using SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            command.CommandText = "SELECT Name, Age FROM Minions";
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
            }
        }
    }
}