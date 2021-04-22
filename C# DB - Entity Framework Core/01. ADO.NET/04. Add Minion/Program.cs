using Microsoft.Data.SqlClient;
using System;

namespace _04._Add_Minion
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS01; Database=MinionsDB; Integrated Security=true";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string[] minionData = Console.ReadLine().Split(' ');

            string minionName = minionData[1];
            int minionAge = int.Parse(minionData[2]);
            string townName = minionData[3];
            string villainName = Console.ReadLine().Split(' ')[1];

            // Transaction to make sure all operations pass
            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using SqlCommand command = new SqlCommand("SELECT Id FROM Towns WHERE Name = @TownName", connection);
                command.Transaction = transaction;
                command.Parameters.AddWithValue("TownName", townName);
                var town = command.ExecuteScalar();

                if (town == null)
                {
                    command.CommandText = "INSERT INTO Towns (Name) VALUES (@TownName)";
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Town {townName} was added to the database.");

                    // Select the newly added town ID
                    command.Parameters.Clear();
                    command.CommandText = "SELECT Id FROM Towns WHERE Name = @TownName";
                    command.Parameters.AddWithValue("TownName", townName);
                    town = command.ExecuteScalar();
                }

                command.Parameters.Clear();
                command.CommandText = "SELECT Id FROM Villains WHERE Name = @Name";
                command.Parameters.AddWithValue("Name", villainName);
                var villain = command.ExecuteScalar();

                if (villain == null)
                {
                    command.CommandText = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@Name, 4)";
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villainName} was added to the database.");

                    // Select the newly added villain ID
                    command.Parameters.Clear();
                    command.CommandText = "SELECT Id FROM Villains WHERE Name = @Name";
                    command.Parameters.AddWithValue("Name", villainName);
                    villain = command.ExecuteScalar();
                }

                // Add the new minion
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO Minions (Name, Age, TownId) VALUES (@Name, @Age, @TownId)";
                command.Parameters.AddWithValue("Name", minionName);
                command.Parameters.AddWithValue("Age", minionAge);
                command.Parameters.AddWithValue("TownId", town);
                command.ExecuteNonQuery();

                // Select the new minion
                command.Parameters.Clear();
                command.CommandText = "SELECT Id FROM Minions WHERE Name = @Name";
                command.Parameters.AddWithValue("Name", minionName);
                var minion = command.ExecuteScalar();

                // Add the villain-minion relationship
                command.Parameters.Clear();
                command.CommandText = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@MinionId, @VillainId)";
                command.Parameters.AddWithValue("MinionId", minion);
                command.Parameters.AddWithValue("VillainId", villain);
                command.ExecuteNonQuery();

                // Commit the changes
                transaction.Commit();

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
            catch(Exception e)
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