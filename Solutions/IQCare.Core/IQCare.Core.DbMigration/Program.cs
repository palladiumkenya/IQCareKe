using DbUp;
using DbUp.Engine;
using IQCare.SharedKernel.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Reflection;
using static IQCare.SharedKernel.Infrastructure.Helpers.ConnectionStringBuilder;


namespace IQCare.Core.DbMigration
{
    class Program
    {
        static IConfigurationRoot Config { get; } = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json")
         .Build();
      
        static int Main(string[] args)
        {
            var connectionString = BuildConnectionString(Config, new ConnectionString());

            if (string.IsNullOrEmpty(connectionString))
                return PrintErrorMessage("IQ connection string value not found");

            var upgrader =
                DeployChanges.To.SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogScriptOutput()
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
              return PrintErrorMessage(result.Error);

            return PrintSucessMessage("IQCare Dbmigration completed succesfully!");
        }

        private static int PrintErrorMessage(object message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadLine();
            return -1;
        }

        private static int PrintSucessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey();
            return 0;
        }
    }
}
