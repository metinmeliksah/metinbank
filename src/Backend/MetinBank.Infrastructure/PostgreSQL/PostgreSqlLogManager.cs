/*
 * MetinBank - PostgreSQL Log Manager
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * PostgreSQL log veritabanı yönetimi
 * Standart: Class ismi büyük harfle başlar (PostgreSqlLogManager)
 */

using Npgsql;
using System;
using System.Data;
using System.Text;

namespace MetinBank.Infrastructure.PostgreSQL
{
    /// <summary>
    /// PostgreSQL Log Manager
    /// Log kayıtlarını PostgreSQL'e yazmak için kullanılır
    /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
    /// </summary>
    public class PostgreSqlLogManager : IDisposable
    {
        // Private değişkenler - class başında tanımlı
        private string _connectionString;
        private NpgsqlConnection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">PostgreSQL connection string</param>
        public PostgreSqlLogManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Sistem log kaydı ekler
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="islemTip">İşlem tipi (MUSTERI_EKLE, HESAP_AC, vb.)</param>
        /// <param name="islemDetay">İşlem detayı</param>
        /// <param name="musteriNo">Müşteri numarası</param>
        /// <param name="hesapNo">Hesap numarası</param>
        /// <param name="tutar">İşlem tutarı</param>
        /// <param name="opAd">Operatör adı</param>
        /// <param name="ipAdres">IP adresi</param>
        /// <param name="sessionId">Session ID</param>
        /// <returns>Hata mesajı varsa string, yoksa null</returns>
        public string SistemLogEkle(string islemTip, string islemDetay,
                                    long? musteriNo = null, string hesapNo = null,
                                    decimal? tutar = null, string opAd = null,
                                    string ipAdres = null, string sessionId = null)
        {
            string hata = null; // Method içinde tanımlama - standart

            try
            {
                // Connection aç
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    // SQL oluştur
                    StringBuilder sql = new StringBuilder(); // StringBuilder kullan - standart
                    sql.Append("INSERT INTO log.sistem_log (");
                    sql.Append("islem_tip, islem_detay, musteri_no, hesap_no, ");
                    sql.Append("tutar, op_ad, ip_adres, session_id) ");
                    sql.Append("VALUES (@islemTip, @islemDetay, @musteriNo, @hesapNo, ");
                    sql.Append("@tutar, @opAd, @ipAdres, @sessionId)");

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), conn))
                    {
                        // Parametreler - standart: camelCase
                        cmd.Parameters.AddWithValue("@islemTip", islemTip);
                        cmd.Parameters.AddWithValue("@islemDetay", (object)islemDetay ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@musteriNo", (object)musteriNo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@hesapNo", (object)hesapNo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tutar", (object)tutar ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@opAd", (object)opAd ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ipAdres", (object)ipAdres ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@sessionId", (object)sessionId ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) // Exception standart: ex
            {
                hata = ex.Message;
            }

            return hata; // String döndür - standart
        }

        /// <summary>
        /// Hata log kaydı ekler
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="hataMesaj">Hata mesajı</param>
        /// <param name="hataDetay">Hata detayı (stack trace)</param>
        /// <param name="spAd">Stored procedure adı</param>
        /// <param name="methodAd">Method adı</param>
        /// <param name="opAd">Operatör adı</param>
        /// <param name="ipAdres">IP adresi</param>
        /// <returns>Hata mesajı varsa string, yoksa null</returns>
        public string HataLogEkle(string hataMesaj, string hataDetay = null,
                                 string spAd = null, string methodAd = null,
                                 string opAd = null, string ipAdres = null)
        {
            string hata = null;

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO log.hata_log (");
                    sql.Append("hata_mesaj, hata_detay, sp_ad, method_ad, op_ad, ip_adres) ");
                    sql.Append("VALUES (@hataMesaj, @hataDetay, @spAd, @methodAd, @opAd, @ipAdres)");

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), conn))
                    {
                        cmd.Parameters.AddWithValue("@hataMesaj", hataMesaj);
                        cmd.Parameters.AddWithValue("@hataDetay", (object)hataDetay ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@spAd", (object)spAd ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@methodAd", (object)methodAd ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@opAd", (object)opAd ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ipAdres", (object)ipAdres ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message;
                // Hata log yazarken hata olursa, console'a yaz
                Console.WriteLine("PostgreSQL Hata Log Hatası: " + ex.Message);
            }

            return hata;
        }

        /// <summary>
        /// API log kaydı ekler
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="endpoint">API endpoint</param>
        /// <param name="httpMethod">HTTP method (GET, POST, PUT, DELETE)</param>
        /// <param name="requestBody">Request body</param>
        /// <param name="responseBody">Response body</param>
        /// <param name="statusCode">HTTP status code</param>
        /// <param name="responseTimeMs">Response time (milliseconds)</param>
        /// <param name="ipAdres">IP adresi</param>
        /// <param name="userAgent">User agent</param>
        /// <param name="musteriNo">Müşteri no</param>
        /// <returns>Hata mesajı varsa string, yoksa null</returns>
        public string ApiLogEkle(string endpoint, string httpMethod,
                                string requestBody = null, string responseBody = null,
                                int? statusCode = null, int? responseTimeMs = null,
                                string ipAdres = null, string userAgent = null,
                                long? musteriNo = null)
        {
            string hata = null;

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO log.api_log (");
                    sql.Append("endpoint, http_method, request_body, response_body, ");
                    sql.Append("status_code, response_time_ms, ip_adres, user_agent, musteri_no) ");
                    sql.Append("VALUES (@endpoint, @httpMethod, @requestBody, @responseBody, ");
                    sql.Append("@statusCode, @responseTimeMs, @ipAdres, @userAgent, @musteriNo)");

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), conn))
                    {
                        cmd.Parameters.AddWithValue("@endpoint", endpoint);
                        cmd.Parameters.AddWithValue("@httpMethod", httpMethod);
                        cmd.Parameters.AddWithValue("@requestBody", (object)requestBody ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@responseBody", (object)responseBody ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@statusCode", (object)statusCode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@responseTimeMs", (object)responseTimeMs ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ipAdres", (object)ipAdres ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@userAgent", (object)userAgent ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@musteriNo", (object)musteriNo ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }

            return hata;
        }

        /// <summary>
        /// Giriş log kaydı ekler
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="musteriNo">Müşteri numarası</param>
        /// <param name="girisTip">Giriş tipi (WEB, MOBILE, ATM, SUBE)</param>
        /// <param name="girisBasarili">Başarılı mı?</param>
        /// <param name="hataMesaj">Hata mesajı (başarısız ise)</param>
        /// <param name="ipAdres">IP adresi</param>
        /// <param name="deviceInfo">Cihaz bilgisi</param>
        /// <param name="location">Konum bilgisi</param>
        /// <returns>Hata mesajı varsa string, yoksa null</returns>
        public string GirisLogEkle(long musteriNo, string girisTip, bool girisBasarili,
                                   string hataMesaj = null, string ipAdres = null,
                                   string deviceInfo = null, string location = null)
        {
            string hata = null;

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();
                    sql.Append("INSERT INTO log.giris_log (");
                    sql.Append("musteri_no, giris_tip, giris_basarili, hata_mesaj, ");
                    sql.Append("ip_adres, device_info, location) ");
                    sql.Append("VALUES (@musteriNo, @girisTip, @girisBasarili, @hataMesaj, ");
                    sql.Append("@ipAdres, @deviceInfo, @location)");

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql.ToString(), conn))
                    {
                        cmd.Parameters.AddWithValue("@musteriNo", musteriNo);
                        cmd.Parameters.AddWithValue("@girisTip", girisTip);
                        cmd.Parameters.AddWithValue("@girisBasarili", girisBasarili);
                        cmd.Parameters.AddWithValue("@hataMesaj", (object)hataMesaj ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ipAdres", (object)ipAdres ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@deviceInfo", (object)deviceInfo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@location", (object)location ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                hata = ex.Message;
            }

            return hata;
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}


