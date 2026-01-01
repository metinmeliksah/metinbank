using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Util;

namespace MetinBank.Business
{
    public class BOnay
    {
        private readonly DataAccess _dataAccess;
        private readonly BHesap _bHesap;

        public BOnay()
        {
            _dataAccess = new DataAccess();
            _bHesap = new BHesap();
        }

        public string OnayTalebiOlustur(long islemID, string islemTipi, int talepEdenID, string beklenenRol, out int onayLogID)
        {
            onayLogID = 0;

            try
            {
                string query = @"INSERT INTO OnayLog (IslemID, IslemTipi, TalepEdenID, OnayDurumu, BeklenenOnaylayanRol)
                                VALUES (@islemID, @islemTipi, @talepEdenID, 'Beklemede', @beklenenRol)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@islemID", islemID),
                    new MySqlParameter("@islemTipi", islemTipi),
                    new MySqlParameter("@talepEdenID", talepEdenID),
                    new MySqlParameter("@beklenenRol", beklenenRol)
                };

                int affectedRows;
                string hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) return hata;

                onayLogID = (int)_dataAccess.GetLastInsertId();

                return null;
            }
            catch (Exception ex)
            {
                return $"Onay talebi oluşturma hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string IslemOnayla(int onayLogID, int onaylayanID)
        {
            try
            {
                string hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Onay log güncelle
                string queryOnay = @"UPDATE OnayLog SET OnayDurumu = 'Onaylandı', OnaylayanID = @onaylayanID, 
                                    OnayTarihi = NOW() WHERE OnayLogID = @onayLogID";

                MySqlParameter[] paramsOnay = new MySqlParameter[]
                {
                    new MySqlParameter("@onaylayanID", onaylayanID),
                    new MySqlParameter("@onayLogID", onayLogID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(queryOnay, paramsOnay, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                if (affectedRows == 0) { _dataAccess.RollbackTransaction(); return "Onay kaydı bulunamadı."; }

                // İşlem ID'sini al
                string queryIslemID = "SELECT IslemID FROM OnayLog WHERE OnayLogID = @onayLogID";
                MySqlParameter[] paramsIslemID = new MySqlParameter[] { new MySqlParameter("@onayLogID", onayLogID) };

                object resultIslemID;
                hata = _dataAccess.ExecuteScalar(queryIslemID, paramsIslemID, out resultIslemID);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                long islemID = Convert.ToInt64(resultIslemID);

                // İşlemi güncelle
                string queryIslem = @"UPDATE Islem SET OnayDurumu = 'Tamamlandı', OnaylayanID = @onaylayanID, 
                                     OnayTarihi = NOW() WHERE IslemID = @islemID";

                MySqlParameter[] paramsIslem = new MySqlParameter[]
                {
                    new MySqlParameter("@onaylayanID", onaylayanID),
                    new MySqlParameter("@islemID", islemID)
                };

                hata = _dataAccess.ExecuteNonQuery(queryIslem, paramsIslem, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // İşlem detaylarını al
                string queryDetay = "SELECT KaynakHesapID, HedefHesapID, Tutar, IslemTipi FROM Islem WHERE IslemID = @islemID";
                MySqlParameter[] paramsDetay = new MySqlParameter[] { new MySqlParameter("@islemID", islemID) };

                DataTable dtDetay;
                hata = _dataAccess.ExecuteQuery(queryDetay, paramsDetay, out dtDetay);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                if (dtDetay.Rows.Count > 0)
                {
                    DataRow row = dtDetay.Rows[0];
                    int? kaynakHesapID = row["KaynakHesapID"] != DBNull.Value ? (int?)Convert.ToInt32(row["KaynakHesapID"]) : null;
                    int? hedefHesapID = row["HedefHesapID"] != DBNull.Value ? (int?)Convert.ToInt32(row["HedefHesapID"]) : null;
                    decimal tutar = Convert.ToDecimal(row["Tutar"]);
                    string islemTipi = row["IslemTipi"].ToString();

                    // Bloke bakiyeyi kaldır ve gerçek işlemi yap
                    if (kaynakHesapID.HasValue)
                    {
                        string queryBlokeKaldir = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye - @tutar WHERE HesapID = @hesapID";
                        MySqlParameter[] paramsBlokeKaldir = new MySqlParameter[]
                        {
                            new MySqlParameter("@tutar", tutar),
                            new MySqlParameter("@hesapID", kaynakHesapID.Value)
                        };
                        hata = _dataAccess.ExecuteNonQuery(queryBlokeKaldir, paramsBlokeKaldir, out affectedRows);
                        if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                        hata = _bHesap.BakiyeGuncelle(kaynakHesapID.Value, -tutar);
                        if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                    }

                    if (hedefHesapID.HasValue)
                    {
                        hata = _bHesap.BakiyeGuncelle(hedefHesapID.Value, tutar);
                        if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                    }
                }

                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"İşlem onaylama hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string IslemReddet(int onayLogID, int onaylayanID, string redNedeni)
        {
            try
            {
                string hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Onay log güncelle
                string queryOnay = @"UPDATE OnayLog SET OnayDurumu = 'Reddedildi', OnaylayanID = @onaylayanID, 
                                    OnayTarihi = NOW(), RedNedeni = @redNedeni WHERE OnayLogID = @onayLogID";

                MySqlParameter[] paramsOnay = new MySqlParameter[]
                {
                    new MySqlParameter("@onaylayanID", onaylayanID),
                    new MySqlParameter("@redNedeni", redNedeni),
                    new MySqlParameter("@onayLogID", onayLogID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(queryOnay, paramsOnay, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // İşlem ID'sini al
                string queryIslemID = "SELECT IslemID FROM OnayLog WHERE OnayLogID = @onayLogID";
                MySqlParameter[] paramsIslemID = new MySqlParameter[] { new MySqlParameter("@onayLogID", onayLogID) };

                object resultIslemID;
                hata = _dataAccess.ExecuteScalar(queryIslemID, paramsIslemID, out resultIslemID);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                long islemID = Convert.ToInt64(resultIslemID);

                // İşlemi güncelle
                string queryIslem = @"UPDATE Islem SET OnayDurumu = 'Reddedildi', OnaylayanID = @onaylayanID, 
                                     OnayTarihi = NOW(), RedNedeni = @redNedeni WHERE IslemID = @islemID";

                MySqlParameter[] paramsIslem = new MySqlParameter[]
                {
                    new MySqlParameter("@onaylayanID", onaylayanID),
                    new MySqlParameter("@redNedeni", redNedeni),
                    new MySqlParameter("@islemID", islemID)
                };

                hata = _dataAccess.ExecuteNonQuery(queryIslem, paramsIslem, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // Bloke bakiyeyi iade et
                string queryDetay = "SELECT KaynakHesapID, Tutar FROM Islem WHERE IslemID = @islemID";
                MySqlParameter[] paramsDetay = new MySqlParameter[] { new MySqlParameter("@islemID", islemID) };

                DataTable dtDetay;
                hata = _dataAccess.ExecuteQuery(queryDetay, paramsDetay, out dtDetay);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                if (dtDetay.Rows.Count > 0)
                {
                    DataRow row = dtDetay.Rows[0];
                    int? kaynakHesapID = row["KaynakHesapID"] != DBNull.Value ? (int?)Convert.ToInt32(row["KaynakHesapID"]) : null;
                    decimal tutar = Convert.ToDecimal(row["Tutar"]);

                    if (kaynakHesapID.HasValue)
                    {
                        string queryBlokeKaldir = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye - @tutar WHERE HesapID = @hesapID";
                        MySqlParameter[] paramsBlokeKaldir = new MySqlParameter[]
                        {
                            new MySqlParameter("@tutar", tutar),
                            new MySqlParameter("@hesapID", kaynakHesapID.Value)
                        };
                        hata = _dataAccess.ExecuteNonQuery(queryBlokeKaldir, paramsBlokeKaldir, out affectedRows);
                        if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                    }
                }

                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"İşlem reddetme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string OnayBekleyenler(string rolAdi, out DataTable onaylar)
        {
            onaylar = null;

            try
            {
                string query = @"SELECT ol.OnayLogID, ol.IslemID, ol.IslemTipi, i.IslemReferansNo, i.Tutar, i.ParaBirimi,
                                i.KaynakHesapID, i.HedefIBAN, i.Aciklama, ol.TalepTarihi,
                                k.Ad, k.Soyad, ol.BeklenenOnaylayanRol
                                FROM OnayLog ol
                                INNER JOIN Islem i ON ol.IslemID = i.IslemID
                                INNER JOIN Kullanici k ON ol.TalepEdenID = k.KullaniciID
                                WHERE ol.OnayDurumu = 'Beklemede' AND ol.BeklenenOnaylayanRol = @rolAdi
                                ORDER BY ol.TalepTarihi DESC";

                MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@rolAdi", rolAdi) };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out onaylar);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Onay bekleyen listesi hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}

