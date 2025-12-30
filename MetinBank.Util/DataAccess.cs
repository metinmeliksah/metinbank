using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MetinBank.Util
{
    /// <summary>
    /// MySQL veritabanı bağlantı yönetimi ve temel CRUD işlemleri
    /// </summary>
    public class DataAccess : IDisposable
    {
        private MySqlConnection _connection;
        private MySqlTransaction _transaction;
        private static string _connectionString;

        /// <summary>
        /// Connection string'i yapılandırma dosyasından okur
        /// </summary>
        static DataAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MetinBankDB"]?.ConnectionString
                ?? "Server=localhost;Database=MetinBankDB;Uid=root;Pwd=;CharSet=utf8mb4;";
        }

        /// <summary>
        /// Yeni bir DataAccess instance oluşturur
        /// </summary>
        public DataAccess()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Veritabanı bağlantısını açar
        /// </summary>
        public string OpenConnection()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return null;
            }
            catch (Exception ex)
            {
                return $"Veritabanı bağlantı hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Veritabanı bağlantısını kapatır
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Bağlantı kapatma hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Transaction başlatır
        /// </summary>
        public string BeginTransaction()
        {
            try
            {
                string hata = OpenConnection();
                if (hata != null) return hata;

                _transaction = _connection.BeginTransaction();
                return null;
            }
            catch (Exception ex)
            {
                return $"Transaction başlatma hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Transaction'ı commit eder
        /// </summary>
        public string CommitTransaction()
        {
            try
            {
                _transaction?.Commit();
                return null;
            }
            catch (Exception ex)
            {
                return $"Transaction commit hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Transaction'ı rollback eder
        /// </summary>
        public string RollbackTransaction()
        {
            try
            {
                _transaction?.Rollback();
                return null;
            }
            catch (Exception ex)
            {
                return $"Transaction rollback hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// SELECT sorgusu çalıştırır ve DataTable döndürür
        /// </summary>
        /// <param name="query">SQL sorgusu</param>
        /// <param name="parameters">Parametreler</param>
        /// <param name="result">Sonuç DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        public string ExecuteQuery(string query, MySqlParameter[] parameters, out DataTable result)
        {
            result = new DataTable();
            try
            {
                string hata = OpenConnection();
                if (hata != null) return hata;

                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(result);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return $"Sorgu çalıştırma hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// INSERT, UPDATE, DELETE sorgusu çalıştırır
        /// </summary>
        /// <param name="query">SQL sorgusu</param>
        /// <param name="parameters">Parametreler</param>
        /// <param name="affectedRows">Etkilenen satır sayısı</param>
        /// <returns>Hata mesajı veya null</returns>
        public string ExecuteNonQuery(string query, MySqlParameter[] parameters, out int affectedRows)
        {
            affectedRows = 0;
            try
            {
                string hata = OpenConnection();
                if (hata != null) return hata;

                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    affectedRows = cmd.ExecuteNonQuery();
                }

                return null;
            }
            catch (Exception ex)
            {
                return $"Sorgu çalıştırma hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Tek bir değer döndüren sorgu çalıştırır (COUNT, MAX, vb.)
        /// </summary>
        /// <param name="query">SQL sorgusu</param>
        /// <param name="parameters">Parametreler</param>
        /// <param name="result">Sonuç değer</param>
        /// <returns>Hata mesajı veya null</returns>
        public string ExecuteScalar(string query, MySqlParameter[] parameters, out object result)
        {
            result = null;
            try
            {
                string hata = OpenConnection();
                if (hata != null) return hata;

                using (MySqlCommand cmd = new MySqlCommand(query, _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    result = cmd.ExecuteScalar();
                }

                return null;
            }
            catch (Exception ex)
            {
                return $"Sorgu çalıştırma hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Son eklenen kaydın ID'sini döndürür
        /// </summary>
        /// <returns>Son eklenen ID</returns>
        public long GetLastInsertId()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", _connection))
                {
                    if (_transaction != null)
                        cmd.Transaction = _transaction;

                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt64(result);
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Stored procedure çalıştırır
        /// </summary>
        /// <param name="procedureName">Stored procedure adı</param>
        /// <param name="parameters">Parametreler</param>
        /// <param name="result">Sonuç DataTable</param>
        /// <returns>Hata mesajı veya null</returns>
        public string ExecuteStoredProcedure(string procedureName, MySqlParameter[] parameters, out DataTable result)
        {
            result = new DataTable();
            try
            {
                string hata = OpenConnection();
                if (hata != null) return hata;

                using (MySqlCommand cmd = new MySqlCommand(procedureName, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (_transaction != null)
                        cmd.Transaction = _transaction;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(result);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return $"Stored procedure hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Bağlantı durumunu kontrol eder
        /// </summary>
        public bool IsConnected()
        {
            return _connection != null && _connection.State == ConnectionState.Open;
        }

        /// <summary>
        /// Dispose pattern implementasyonu
        /// </summary>
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}

