using Microsoft.Data.SqlClient;
using System;

namespace _03._Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS01; Database=MinionsDB; Integrated Security=true";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            int id = int.Parse(Console.ReadLine());

            using SqlCommand command = new SqlCommand("SELECT Name FROM Villains WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("Id", id);
            var villain = command.ExecuteScalar();

            if (villain == null)
            {
                Console.WriteLine($"No villain with ID 10 exists in the database.");
            }
            else
            {
                Console.WriteLine($"Villain: {villain}");

                string query =
                    @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                          m.Name, 
                          m.Age
                      FROM MinionsVillains AS mv
                      JOIN Minions As m ON mv.MinionId = m.Id
                      WHERE mv.VillainId = @Id
                      ORDER BY m.Name";
                command.CommandText = query;

                using SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    Console.WriteLine("(no minions)");
                }

                while (reader.Read())
                {
                    var number = reader["RowNum"];
                    var name = reader["Name"];
                    var age = reader["Age"];

                    Console.WriteLine($"{number}. {name} {age}");
                }
            }
        }
    }
}