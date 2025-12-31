using System.Data;
using MySql.Data.MySqlClient;

namespace MetinBank.Data.Interfaces
{
    /// <summary>
    /// Repository interface for database operations
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Executes a non-query SQL command (INSERT, UPDATE, DELETE)
        /// </summary>
        int ExecuteNonQuery(string query, MySqlParameter[] parameters = null);

        /// <summary>
        /// Executes a query and returns a single value
        /// </summary>
        object ExecuteScalar(string query, MySqlParameter[] parameters = null);

        /// <summary>
        /// Executes a query and returns a MySqlDataReader
        /// </summary>
        MySqlDataReader ExecuteReader(string query, MySqlParameter[] parameters = null);

        /// <summary>
        /// Executes a query and returns a DataTable
        /// </summary>
        DataTable GetDataTable(string query, MySqlParameter[] parameters = null);

        /// <summary>
        /// Begins a database transaction
        /// </summary>
        MySqlTransaction BeginTransaction();
    }
}
