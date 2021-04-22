using Microsoft.Data.SqlClient;
using System;

namespace _02._Villain_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS01; Database=MinionsDB; Integrated Security=true";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query =
                @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount 
                    FROM Villains AS v 
                    JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                GROUP BY v.Id, v.Name 
                  HAVING COUNT(mv.VillainId) > 3 
                ORDER BY COUNT(mv.VillainId)";

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var villain = reader["Name"];
                var minions = reader["MinionsCount"];
                Console.WriteLine($"{villain} - {minions}");
            }
        }
    }
}