using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace MetinBank.Data
{
    /// <summary>
    /// Singleton pattern for database connection management
    /// </summary>
    public sealed class DbConnectionManager
    {
        private static readonly Lazy<DbConnectionManager> _instance = 
            new Lazy<DbConnectionManager>(() => new DbConnectionManager());

        private readonly string _connectionString;

        private DbConnectionManager()
        {
            // Read connection string from app.config
            _connectionString = ConfigurationManager.ConnectionStrings["MetinBankDB"]?.ConnectionString;
            
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'MetinBankDB' not found in configuration file.");
            }
        }

        public static DbConnectionManager Instance => _instance.Value;

        /// <summary>
        /// Creates and returns a new MySQL connection
        /// </summary>
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Gets the connection string
        /// </summary>
        public string ConnectionString => _connectionString;
    }
}
