using System;
using System.Data;
using System.Data.SqlClient;

namespace MetinBank.Modul.SPObject
{
    /// <summary>
    /// Stored Procedure çağrıları için temel sınıf
    /// </summary>
    public abstract class BaseSP
    {
        protected string ConnectionString { get; set; }

        public BaseSP()
        {
            // Connection string - appsettings'den okunacak
            ConnectionString = "Data Source=.;Initial Catalog=MetinBankDB;Integrated Security=True";
        }

        /// <summary>
        /// Stored Procedure çalıştırır ve etkilenen satır sayısını döner
        /// </summary>
        protected int ExecuteNonQuery(string spName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Stored Procedure çalıştırır ve DataTable döner
        /// </summary>
        protected DataTable ExecuteReader(string spName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// Stored Procedure çalıştırır ve tek değer döner
        /// </summary>
        protected object? ExecuteScalar(string spName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
