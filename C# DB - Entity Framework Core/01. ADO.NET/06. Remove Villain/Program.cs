using Microsoft.Data.SqlClient;
using System;

namespace _06._Remove_Villain
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS01; Database=MinionsDB; Integrated Security=true";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            int villainId = int.Parse(Console.ReadLine());

            string selectQuery = "SELECT Name FROM Villains WHERE Id = @VillainId";
            using SqlCommand command = new SqlCommand(selectQuery, connection);
            command.Parameters.AddWithValue("VillainId", villainId);
            var villainName = command.ExecuteScalar();

            if (villainName == null)
            {
                Console.WriteLine("No such villain was found.");
                return;
            }

            using SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                // Release minions first due FK constraints
                command.CommandText = "DELETE FROM MinionsVillains WHERE VillainId = @VillainId";
                var rowsAffected = command.ExecuteNonQuery();
                
                // Delete villain
                command.CommandText = "DELETE FROM Villains WHERE Id = @VillainId";
                command.ExecuteNonQuery();

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{rowsAffected} minions were released.");

                transaction.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    transaction.Rollback();
                    Console.WriteLine(e.Message);
                }
                catch
                {
                    Console.WriteLine("Server Error");
                }
            }
        }
    }
}