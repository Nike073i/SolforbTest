using System;
using SolforbTest.WebClient.Config.Consts;

namespace SolforbTest.WebClient.Config
{
    public static class DbConfig
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            string dbConnectionString =
                configuration[ConfigurationKeys.DatabaseConnectionKey]
                ?? throw new InvalidOperationException("Database connection string not set");
            return dbConnectionString;
        }
    }
}
