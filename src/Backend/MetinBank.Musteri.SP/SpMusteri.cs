/*
 * MetinBank - SpMusteri (SP Object Katmanı)
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Müşteri Stored Procedure'lerini çağıran sınıf
 * 
 * STANDARTLAR:
 * - Bu katmanda OracleConnection kurulmaz
 * - Connection bilgisi parametre olarak gönderilir
 * - SP isimleri database'deki ile birebir aynı olmalı
 * - Oracle Connection nesneleri: conn, cmd, trans, prm, da, dr, cb
 */

using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace MetinBank.Musteri.SP
{
    /// <summary>
    /// Müşteri Stored Procedure'leri
    /// Package: PKG_MUSTERI (Database'deki package adı)
    /// </summary>
    public static class SpMusteri
    {
        // Package ve Procedure isimleri - standart: T_MUSTERI, P_MUSTERI
        public const string T_MUSTERI = "PKG_MUSTERI";
        public const string P_MUSTERI_EKLE = "P_MUSTERI_EKLE";
        public const string P_MUSTERI_GUNCELLE = "P_MUSTERI_GUNCELLE";
        public const string P_MUSTERI_SIL = "P_MUSTERI_SIL";

        /// <summary>
        /// Müşteri ekler
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">Oracle Connection (Çağıran katmandan gelir)</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <param name="ad">Ad</param>
        /// <param name="soyad">Soyad</param>
        /// <param name="eposta">E-posta</param>
        /// <param name="telefon">Telefon</param>
        /// <returns>Müşteri No (long tip - standart)</returns>
        public static long MusteriEkle(OracleConnection conn, OracleTransaction trans,
                                       string tcKimlikNo, string ad, string soyad,
                                       string eposta, string telefon)
        {
            // Oracle nesneleri - standart isimlendirme
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = T_MUSTERI + "." + P_MUSTERI_EKLE;

            // Parametreler - standart: prm
            OracleParameter prmTcNo = new OracleParameter("p_tc_kimlik_no", OracleDbType.Varchar2);
            prmTcNo.Value = tcKimlikNo;
            cmd.Parameters.Add(prmTcNo);

            OracleParameter prmAd = new OracleParameter("p_ad", OracleDbType.Varchar2);
            prmAd.Value = ad;
            cmd.Parameters.Add(prmAd);

            OracleParameter prmSoyad = new OracleParameter("p_soyad", OracleDbType.Varchar2);
            prmSoyad.Value = soyad;
            cmd.Parameters.Add(prmSoyad);

            OracleParameter prmEposta = new OracleParameter("p_eposta", OracleDbType.Varchar2);
            prmEposta.Value = eposta;
            cmd.Parameters.Add(prmEposta);

            OracleParameter prmTelefon = new OracleParameter("p_telefon", OracleDbType.Varchar2);
            prmTelefon.Value = telefon;
            cmd.Parameters.Add(prmTelefon);

            // Output parameter - Müşteri No (long tip standart)
            OracleParameter prmMusteriNo = new OracleParameter("p_musteri_no", OracleDbType.Int64);
            prmMusteriNo.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(prmMusteriNo);

            cmd.ExecuteNonQuery();

            return Convert.ToInt64(prmMusteriNo.Value.ToString());
        }

        /// <summary>
        /// Müşteri bulur (TC Kimlik No ile)
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <returns>DataTable</returns>
        public static DataTable MusteriBul(OracleConnection conn, string tcKimlikNo)
        {
            /*
             * .NET-Oracle tür uyuşmazlığı nedeniyle
             * Rowtype return eden SP'ler için SELECT kısmı
             * .NET tarafında yazılır. İsimler birebir aynı olmalı.
             */
            string sql = @"SELECT musteri_no,
                                 tc_kimlik_no,
                                 ad,
                                 soyad,
                                 eposta,
                                 telefon,
                                 durum,
                                 kayit_tarih
                          FROM musteriler
                          WHERE tc_kimlik_no = :tc_kimlik_no
                            AND aktif = 1";

            // Oracle nesneleri - standart isimlendirme
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleParameter prm = new OracleParameter("tc_kimlik_no", tcKimlikNo);
            cmd.Parameters.Add(prm);

            // DataAdapter ve DataTable - standart isimlendirme
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        /// <summary>
        /// Bakiye getirir (Database'deki SP ismiyle birebir aynı)
        /// SP ismi: get_bakiye
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="hesapNo">Hesap No</param>
        /// <returns>Bakiye (decimal)</returns>
        public static decimal get_bakiye(OracleConnection conn, string hesapNo)
        {
            /*
             * Metod ismi database'deki SP ismiyle aynı: get_bakiye
             * Bu standarda uyulmalı
             */
            string sql = @"SELECT bakiye 
                          FROM hesaplar 
                          WHERE hesap_no = :hesap_no
                            AND aktif = 1";

            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleParameter prm = new OracleParameter("hesap_no", hesapNo);
            cmd.Parameters.Add(prm);

            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToDecimal(result) : 0;
        }

        /// <summary>
        /// Tüm müşterileri getirir (kayıt sayısı sınırlı - standart)
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <returns>DataTable (max 100 kayıt)</returns>
        public static DataTable MusterileriGetir(OracleConnection conn)
        {
            /*
             * STANDART: Client'a gönderilecek DataTable'larda
             * mümkünse kayıt sayısı kontrolü konmalı (ROWNUM<100)
             */
            string sql = @"SELECT * FROM 
                          (SELECT musteri_no,
                                  tc_kimlik_no,
                                  ad,
                                  soyad,
                                  eposta,
                                  telefon
                           FROM musteriler
                           WHERE aktif = 1
                           ORDER BY musteri_no DESC)
                          WHERE ROWNUM < 100";

            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}

