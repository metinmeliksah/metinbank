using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Data.Interfaces;

namespace MetinBank.Data
{
    /// <summary>
    /// Repository implementation for database operations
    /// </summary>
    public sealed class Repository : IRepository
    {
        private static readonly Lazy<Repository> _instance = 
            new Lazy<Repository>(() => new Repository());

        private Repository()
        {
        }

        public static Repository Instance => _instance.Value;

        /// <summary>
        /// Executes a non-query SQL command (INSERT, UPDATE, DELETE)
        /// </summary>
        public int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            try
            {
                using (var connection = DbConnectionManager.Instance.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executes a query and returns a single value
        /// </summary>
        public object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            try
            {
                using (var connection = DbConnectionManager.Instance.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executes a query and returns a MySqlDataReader
        /// Note: The connection remains open for the reader. Caller must dispose the reader.
        /// </summary>
        public MySqlDataReader ExecuteReader(string query, MySqlParameter[] parameters = null)
        {
            try
            {
                var connection = DbConnectionManager.Instance.GetConnection();
                var command = new MySqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                // CommandBehavior.CloseConnection ensures connection is closed when reader is disposed
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executes a query and returns a DataTable
        /// </summary>
        public DataTable GetDataTable(string query, MySqlParameter[] parameters = null)
        {
            try
            {
                using (var connection = DbConnectionManager.Instance.GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Begins a database transaction
        /// Note: Caller is responsible for committing/rolling back and disposing the transaction
        /// </summary>
        public MySqlTransaction BeginTransaction()
        {
            var connection = DbConnectionManager.Instance.GetConnection();
            connection.Open();
            return connection.BeginTransaction();
        }
    }
}
