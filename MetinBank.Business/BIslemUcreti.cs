using System;
using System.Data;
using MetinBank.Util;
using MySql.Data.MySqlClient;

namespace MetinBank.Business
{
    public class BIslemUcreti
    {
        private readonly DataAccess _dataAccess;

        public BIslemUcreti()
        {
            _dataAccess = new DataAccess();
        }

        /// <summary>
        /// İşlem ücretini hesaplar
        /// </summary>
        /// <param name="islemTipi">Havale, EFT, Virman, ParaYatirma, ParaCekme</param>
        /// <param name="islemKanali">Internet, Mobil, Sube</param>
        /// <param name="tutar">İşlem tutarı</param>
        /// <returns>İşlem ücreti</returns>
        public decimal IslemUcretiHesapla(string islemTipi, string islemKanali, decimal tutar)
        {
            try
            {
                string query = @"
                    SELECT Ucret 
                    FROM VW_IslemUcretleri 
                    WHERE IslemTipi = @IslemTipi 
                      AND IslemKanali = @IslemKanali 
                      AND @Tutar >= MinTutar 
                      AND @Tutar < MaxTutar 
                    LIMIT 1";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@IslemTipi", islemTipi),
                    new MySqlParameter("@IslemKanali", islemKanali),
                    new MySqlParameter("@Tutar", tutar)
                };

                object result;
                string hata = _dataAccess.ExecuteScalar(query, parameters, out result);
                
                if (hata != null)
                {
                    throw new Exception(hata);
                }
                
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }

                return 0; // Ücret bulunamazsa 0 döndür
            }
            catch (Exception ex)
            {
                throw new Exception($"İşlem ücreti hesaplanırken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Tüm işlem ücretlerini getirir
        /// </summary>
        public DataTable TumUcretleriGetir()
        {
            try
            {
                string query = @"
                    SELECT 
                        IslemTipi,
                        IslemKanali,
                        MinTutar,
                        CASE WHEN MaxTutar = 999999999 THEN NULL ELSE MaxTutar END AS MaxTutar,
                        Ucret,
                        Aktif
                    FROM VW_IslemUcretleri
                    ORDER BY IslemTipi, IslemKanali, MinTutar";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);
                
                if (hata != null)
                {
                    throw new Exception(hata);
                }
                
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ücretler getirilirken hata oluştu: {ex.Message}");
            }
        }

        /// <summary>
        /// Belirli bir işlem tipi için ücretleri getirir
        /// </summary>
        public DataTable IslemTipiUcretleriGetir(string islemTipi, string islemKanali)
        {
            try
            {
                string query = @"
                    SELECT 
                        MinTutar,
                        CASE WHEN MaxTutar = 999999999 THEN NULL ELSE MaxTutar END AS MaxTutar,
                        Ucret
                    FROM VW_IslemUcretleri
                    WHERE IslemTipi = @IslemTipi 
                      AND IslemKanali = @IslemKanali
                    ORDER BY MinTutar";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@IslemTipi", islemTipi),
                    new MySqlParameter("@IslemKanali", islemKanali)
                };

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
                
                if (hata != null)
                {
                    throw new Exception(hata);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"İşlem tipi ücretleri getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
