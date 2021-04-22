using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _05._Change_Town_Names_Casing
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS01; Database=MinionsDB; Integrated Security=true";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string countryName = Console.ReadLine();

            string updateQuery =
                @"UPDATE Towns
                  SET Name = UPPER(Name)
                  WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @CountryName)";
            using SqlCommand command = new SqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("CountryName", countryName);
            int rowsChanged = command.ExecuteNonQuery();

            string selectQuery =
                @"SELECT t.Name 
                  FROM Towns as t
                  JOIN Countries AS c ON c.Id = t.CountryCode
                  WHERE c.Name = @CountryName";
            command.CommandText = selectQuery;

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("No town names were affected.");
                return;
            }

            Console.WriteLine($"{rowsChanged} town names were affected.");

            List<string> towns = new List<string>();
            while (reader.Read())
            {
                towns.Add((string)reader["Name"]);
            }
            Console.WriteLine($"[{string.Join(", ", towns)}]");
        }
    }
}