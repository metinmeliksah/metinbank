/*
 * MetinBank - Müşteri Service Layer
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Müşteri servis sınıfı - Client tarafından çağrılır
 * Standart: Service sınıfları S prefix'i ile başlar
 */

using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using MetinBank.Musteri.Interface;
using MetinBank.Musteri.Business;
using MetinBank.Musteri.SP;

namespace MetinBank.Musteri.Service
{
    /// <summary>
    /// Müşteri Service
    /// Client tarafından ilgili modülle alakalı istekleri karşılar
    /// Prefix: S (SMusteriService)
    /// Implements: IMusteriService
    /// </summary>
    public class SMusteriService : IMusteriService
    {
        // Private değişkenler - standart: _ ile başlar
        private string _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        public SMusteriService()
        {
            // Connection string'i configuration'dan al
            // _connectionString = ConfigurationManager.ConnectionStrings["OracleConn"].ConnectionString;
            _connectionString = "Data Source=localhost:1521/XE;User Id=metinbank;Password=metinbank123;";
        }

        /// <summary>
        /// Müşteri ekler
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// Service katmanında connection açılır ve transaction yönetimi yapılır
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <param name="ad">Ad</param>
        /// <param name="soyad">Soyad</param>
        /// <param name="eposta">E-posta</param>
        /// <param name="telefon">Telefon</param>
        /// <returns>Müşteri No (long tip - standart)</returns>
        public long MusteriEkle(string tcKimlikNo, string ad, string soyad, string eposta, string telefon)
        {
            // Oracle nesneleri - standart isimlendirme
            OracleConnection conn = null;
            OracleTransaction trans = null;
            long musteriNo = 0;

            try
            {
                // 1. TC Kimlik No validasyonu (Business katmanında)
                if (!BMusteriIslem.TcKimlikNoDogrula(tcKimlikNo))
                {
                    throw new Exception("TC Kimlik No geçersiz");
                }

                // 2. Connection aç (SADECE Service katmanında açılır)
                conn = new OracleConnection(_connectionString);
                conn.Open();

                // 3. Transaction başlat
                trans = conn.BeginTransaction();

                // 4. Business katmanını çağır
                string opAd = "SYSTEM"; // Gerçek uygulamada session'dan gelir
                musteriNo = BMusteriIslem.MusteriEkle(conn, trans, tcKimlikNo, ad, soyad, eposta, telefon, opAd);

                // 5. Transaction commit
                trans.Commit();

                return musteriNo;
            }
            catch (Exception ex)
            {
                // Hata durumunda rollback
                try
                {
                    trans?.Rollback();
                }
                catch (Exception ex1)
                {
                    // Rollback hatası - Exception standart: ex, ex1, ex2
                    throw new Exception("Rollback hatası: " + ex1.Message, ex1);
                }

                throw new Exception("Müşteri eklenirken hata: " + ex.Message, ex);
            }
            finally
            {
                // Connection kapat
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// TC Kimlik No ile müşteri bulur
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <returns>DataTable (Standart: dt)</returns>
        public DataTable MusteriBul(string tcKimlikNo)
        {
            OracleConnection conn = null;
            DataTable dt = null; // DataTable standart: dt

            try
            {
                conn = new OracleConnection(_connectionString);
                conn.Open();

                // SP katmanını doğrudan çağır (tek SP çağrısı için Business'a gerek yok)
                dt = SpMusteri.MusteriBul(conn, tcKimlikNo);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri bulunurken hata: " + ex.Message, ex);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Müşteri günceller
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="musteriNo">Müşteri No (long tip - standart)</param>
        /// <param name="ad">Ad</param>
        /// <param name="soyad">Soyad</param>
        /// <param name="eposta">E-posta</param>
        /// <returns>Başarılı ise null, hata ise hata mesajı</returns>
        public string MusteriGuncelle(long musteriNo, string ad, string soyad, string eposta)
        {
            OracleConnection conn = null;
            OracleTransaction trans = null;

            try
            {
                conn = new OracleConnection(_connectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                // Business katmanını çağır
                string opAd = "SYSTEM";
                string sonuc = BMusteriIslem.MusteriGuncelle(conn, trans, musteriNo, ad, soyad, eposta, "", opAd);

                if (sonuc == null)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }

                return sonuc;
            }
            catch (Exception ex)
            {
                try
                {
                    trans?.Rollback();
                }
                catch (Exception ex1)
                {
                    return "Rollback hatası: " + ex1.Message;
                }

                return "Müşteri güncellenirken hata: " + ex.Message;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Tüm müşterileri getirir
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// STANDART: Client'a gönderilecek DataTable'larda kayıt sayısı kontrolü (ROWNUM<100)
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable MusterileriGetir()
        {
            OracleConnection conn = null;
            DataTable dt = null;

            try
            {
                conn = new OracleConnection(_connectionString);
                conn.Open();

                // SP katmanını çağır (ROWNUM kontrolü SP'de yapılır)
                dt = SpMusteri.MusterileriGetir(conn);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteriler getirilirken hata: " + ex.Message, ex);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Müşteri bakiyesini getirir
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="musteriNo">Müşteri No (long tip)</param>
        /// <returns>Toplam bakiye</returns>
        public decimal ToplamBakiyeGetir(long musteriNo)
        {
            OracleConnection conn = null;

            try
            {
                conn = new OracleConnection(_connectionString);
                conn.Open();

                // Business katmanını çağır (birden fazla hesap bakiyesi toplar)
                decimal toplamBakiye = BMusteriIslem.ToplamBakiyeHesapla(conn, musteriNo);

                return toplamBakiye;
            }
            catch (Exception ex)
            {
                throw new Exception("Bakiye getirilirken hata: " + ex.Message, ex);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}


