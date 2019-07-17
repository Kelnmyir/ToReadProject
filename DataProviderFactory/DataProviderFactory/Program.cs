using System;
using static System.Console;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace DataProviderFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Fun with Data Provider Factories");
            string dataProvider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection==null)
                {
                    MessageBox.Show("Connection");
                    return;
                }
                WriteLine($"Connection is a: {connection.GetType().Name}");
                connection.ConnectionString = connectionString;
                connection.Open();

                DbCommand command = factory.CreateCommand();
                if(command==null)
                {
                    MessageBox.Show("Command");
                    return;
                }
                WriteLine($"Command is: {command.GetType().Name}");
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Inventory";

                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    WriteLine($"Data reader is a: {dataReader.GetType().Name}");
                    WriteLine("\ncurrent output:");
                    while (dataReader.Read())
                    {
                        WriteLine($"Car number {dataReader["CarID"]} named {dataReader["PetName"]}");
                    }
                }
                ReadKey();
            }
        }
    }
}
