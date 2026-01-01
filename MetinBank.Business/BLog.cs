using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Util;

namespace MetinBank.Business
{
    public class BLog
    {
        private readonly DataAccess _dataAccess;

        public BLog()
        {
            _dataAccess = new DataAccess();
        }

        public string IslemLoguKaydet(int? kullaniciID, string islemTipi, string tabloAdi, long? kayitID, 
                                     string oncekiDeger, string yeniDeger, string islemDetay, string ipAdresi, 
                                     bool basariliMi, string hataMesaji)
        {
            try
            {
                string query = @"INSERT INTO IslemLog (KullaniciID, LogTipi, IslemTipi, TabloAdi, KayitID, 
                                OncekiDeger, YeniDeger, IslemDetay, IPAdresi, MacAdresi, SessionID, BasariliMi, HataMesaji)
                                VALUES (@kullaniciID, 'Islem', @islemTipi, @tabloAdi, @kayitID, @oncekiDeger, 
                                @yeniDeger, @islemDetay, @ipAdresi, @macAdresi, @sessionID, @basariliMi, @hataMesaji)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kullaniciID", (object)kullaniciID ?? DBNull.Value),
                    new MySqlParameter("@islemTipi", islemTipi ?? ""),
                    new MySqlParameter("@tabloAdi", tabloAdi ?? ""),
                    new MySqlParameter("@kayitID", (object)kayitID ?? DBNull.Value),
                    new MySqlParameter("@oncekiDeger", oncekiDeger ?? ""),
                    new MySqlParameter("@yeniDeger", yeniDeger ?? ""),
                    new MySqlParameter("@islemDetay", islemDetay ?? ""),
                    new MySqlParameter("@ipAdresi", ipAdresi ?? ""),
                    new MySqlParameter("@macAdresi", CommonFunctions.GetMacAddress()),
                    new MySqlParameter("@sessionID", CommonFunctions.GenerateSessionId()),
                    new MySqlParameter("@basariliMi", basariliMi),
                    new MySqlParameter("@hataMesaji", hataMesaji ?? "")
                };

                int affectedRows;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Log kaydetme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string LoginLoguKaydet(int? kullaniciID, string kullaniciAdi, string islemTipi, bool basariliMi, 
                                     string ipAdresi, string macAdresi, string tarayici, string cihaz, 
                                     string isletimSistemi, string hataMesaji)
        {
            try
            {
                string query = @"INSERT INTO LoginLog (KullaniciID, KullaniciAdi, IslemTipi, BasariliMi, IPAdresi, 
                                MacAdresi, Tarayici, Cihaz, IsletimSistemi, HataMesaji)
                                VALUES (@kullaniciID, @kullaniciAdi, @islemTipi, @basariliMi, @ipAdresi, @macAdresi, 
                                @tarayici, @cihaz, @isletimSistemi, @hataMesaji)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@kullaniciID", (object)kullaniciID ?? DBNull.Value),
                    new MySqlParameter("@kullaniciAdi", kullaniciAdi ?? ""),
                    new MySqlParameter("@islemTipi", islemTipi ?? ""),
                    new MySqlParameter("@basariliMi", basariliMi),
                    new MySqlParameter("@ipAdresi", ipAdresi ?? ""),
                    new MySqlParameter("@macAdresi", macAdresi ?? ""),
                    new MySqlParameter("@tarayici", tarayici ?? ""),
                    new MySqlParameter("@cihaz", cihaz ?? ""),
                    new MySqlParameter("@isletimSistemi", isletimSistemi ?? ""),
                    new MySqlParameter("@hataMesaji", hataMesaji ?? "")
                };

                int affectedRows;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Login log kaydetme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string GuvenlikLoguKaydet(string olayTipi, int? kullaniciID, string ipAdresi, string olayDetay, string riskSeviyesi)
        {
            try
            {
                string query = @"INSERT INTO GuvenlikLog (OlayTipi, KullaniciID, IPAdresi, OlayDetay, RiskSeviyesi)
                                VALUES (@olayTipi, @kullaniciID, @ipAdresi, @olayDetay, @riskSeviyesi)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@olayTipi", olayTipi ?? ""),
                    new MySqlParameter("@kullaniciID", (object)kullaniciID ?? DBNull.Value),
                    new MySqlParameter("@ipAdresi", ipAdresi ?? ""),
                    new MySqlParameter("@olayDetay", olayDetay ?? ""),
                    new MySqlParameter("@riskSeviyesi", riskSeviyesi ?? "Dusuk")
                };

                int affectedRows;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Güvenlik log kaydetme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}

