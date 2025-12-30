using System;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Business
{
    public class BIslem
    {
        private readonly DataAccess _dataAccess;
        private readonly BHesap _bHesap;

        public BIslem()
        {
            _dataAccess = new DataAccess();
            _bHesap = new BHesap();
        }

        public string ParaYatir(int hesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = ValidationHelper.ValidateTutar(tutar, 1, 500000);
                if (hata != null) return hata;

                hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Hesap kontrolü
                HesapModel hesap;
                hata = _bHesap.HesapGetir(hesapID, out hesap);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                if (hesap.Durum != "Aktif") { _dataAccess.RollbackTransaction(); return "Hesap aktif değil."; }

                // Bakiye güncelle
                hata = _bHesap.BakiyeGuncelle(hesapID, tutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // İşlem kaydı
                string refNo = CommonFunctions.GenerateTransactionReference();
                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefHesapID, IslemTipi, IslemAltTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, KullaniciID, SubeID, OnayDurumu, KanalTipi, BasariliMi)
                                VALUES (@refNo, NULL, @hesapID, 'Yatırma', 'Nakit', @tutar, 'TL', 0, @aciklama, @kullaniciID, 
                                @subeID, 'Tamamlandı', 'Sube', 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@hesapID", hesapID),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@aciklama", aciklama ?? "Para yatırma"),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = _dataAccess.GetLastInsertId();

                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Para yatırma hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string ParaCek(int hesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = ValidationHelper.ValidateTutar(tutar, 1, 50000);
                if (hata != null) return hata;

                hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Hesap kontrolü ve bakiye kontrolü
                HesapModel hesap;
                hata = _bHesap.HesapGetir(hesapID, out hesap);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                if (hesap.Durum != "Aktif") { _dataAccess.RollbackTransaction(); return "Hesap aktif değil."; }

                hata = ValidationHelper.ValidateBakiye(hesap.KullanilabilirBakiye, tutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // Bakiye güncelle
                hata = _bHesap.BakiyeGuncelle(hesapID, -tutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // İşlem kaydı
                string refNo = CommonFunctions.GenerateTransactionReference();
                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefHesapID, IslemTipi, IslemAltTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, KullaniciID, SubeID, OnayDurumu, KanalTipi, BasariliMi)
                                VALUES (@refNo, @hesapID, NULL, 'Çekme', 'Nakit', @tutar, 'TL', 0, @aciklama, @kullaniciID, 
                                @subeID, 'Tamamlandı', 'Sube', 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@hesapID", hesapID),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@aciklama", aciklama ?? "Para çekme"),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = _dataAccess.GetLastInsertId();

                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Para çekme hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string Havale(int kaynakHesapID, string hedefIBAN, decimal tutar, string aciklama, string aliciAdi, 
                            int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = ValidationHelper.ValidateTutar(tutar, 1, 100000);
                if (hata != null) return hata;

                hata = IbanHelper.ValidateIban(hedefIBAN);
                if (hata != null) return hata;

                hedefIBAN = IbanHelper.RemoveIbanSpaces(hedefIBAN);

                hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Kaynak hesap kontrolü
                HesapModel kaynakHesap;
                hata = _bHesap.HesapGetir(kaynakHesapID, out kaynakHesap);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                hata = ValidationHelper.ValidateBakiye(kaynakHesap.KullanilabilirBakiye, tutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // Hedef hesap kontrolü (Metin Bank içi ise)
                HesapModel hedefHesap = null;
                int? hedefHesapID = null;

                if (IbanHelper.IsMetinBankIban(hedefIBAN))
                {
                    hata = _bHesap.HesapGetirIBAN(hedefIBAN, out hedefHesap);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                    hedefHesapID = hedefHesap.HesapID;
                }

                // Onay durumu belirle
                string onayDurumu = tutar > 5000 ? (tutar > 10000 ? "Beklemede" : "Beklemede") : "Tamamlandı";

                // İşlem referans numarası
                string refNo = CommonFunctions.GenerateTransactionReference();

                // İşlem kaydı
                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefHesapID, HedefIBAN, IslemTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, AliciAdi, KullaniciID, SubeID, OnayDurumu, 
                                KanalTipi, IPAdresi, BasariliMi)
                                VALUES (@refNo, @kaynakID, @hedefID, @hedefIBAN, 'Havale', @tutar, 'TL', 0, @aciklama, 
                                @aliciAdi, @kullaniciID, @subeID, @onayDurumu, 'Sube', @ip, 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@kaynakID", kaynakHesapID),
                    new MySqlParameter("@hedefID", (object)hedefHesapID ?? DBNull.Value),
                    new MySqlParameter("@hedefIBAN", hedefIBAN),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@aciklama", aciklama ?? "Havale"),
                    new MySqlParameter("@aliciAdi", aliciAdi ?? ""),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID),
                    new MySqlParameter("@onayDurumu", onayDurumu),
                    new MySqlParameter("@ip", CommonFunctions.GetLocalIPAddress())
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = _dataAccess.GetLastInsertId();

                // Onay gerekmiyorsa bakiyeleri güncelle
                if (onayDurumu == "Tamamlandı")
                {
                    hata = _bHesap.BakiyeGuncelle(kaynakHesapID, -tutar);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                    if (hedefHesapID.HasValue)
                    {
                        hata = _bHesap.BakiyeGuncelle(hedefHesapID.Value, tutar);
                        if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                    }
                }
                else
                {
                    // Bloke bakiye ekle
                    string queryBloke = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye + @tutar WHERE HesapID = @hesapID";
                    MySqlParameter[] paramsBloke = new MySqlParameter[]
                    {
                        new MySqlParameter("@tutar", tutar),
                        new MySqlParameter("@hesapID", kaynakHesapID)
                    };
                    hata = _dataAccess.ExecuteNonQuery(queryBloke, paramsBloke, out affectedRows);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }

                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Havale hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string EFT(int kaynakHesapID, string hedefIBAN, decimal tutar, string aciklama, string aliciAdi, 
                         int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                // EFT ücreti
                decimal eftUcreti = 5.00m;
                decimal toplamTutar = tutar + eftUcreti;

                string hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Bakiye kontrolü (ücret dahil)
                HesapModel kaynakHesap;
                hata = _bHesap.HesapGetir(kaynakHesapID, out kaynakHesap);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                hata = ValidationHelper.ValidateBakiye(kaynakHesap.KullanilabilirBakiye, toplamTutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                string refNo = CommonFunctions.GenerateTransactionReference();
                string onayDurumu = tutar > 5000 ? "Beklemede" : "Tamamlandı";

                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefIBAN, IslemTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, AliciAdi, KullaniciID, SubeID, OnayDurumu, 
                                KanalTipi, IPAdresi, BasariliMi)
                                VALUES (@refNo, @kaynakID, @hedefIBAN, 'EFT', @tutar, 'TL', @ucret, @aciklama, 
                                @aliciAdi, @kullaniciID, @subeID, @onayDurumu, 'Sube', @ip, 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@kaynakID", kaynakHesapID),
                    new MySqlParameter("@hedefIBAN", hedefIBAN),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@ucret", eftUcreti),
                    new MySqlParameter("@aciklama", aciklama ?? "EFT"),
                    new MySqlParameter("@aliciAdi", aliciAdi ?? ""),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID),
                    new MySqlParameter("@onayDurumu", onayDurumu),
                    new MySqlParameter("@ip", CommonFunctions.GetLocalIPAddress())
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = _dataAccess.GetLastInsertId();

                if (onayDurumu == "Tamamlandı")
                {
                    hata = _bHesap.BakiyeGuncelle(kaynakHesapID, -toplamTutar);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }

                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"EFT hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string Virman(int kaynakHesapID, int hedefHesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                HesapModel kaynakHesap;
                hata = _bHesap.HesapGetir(kaynakHesapID, out kaynakHesap);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                HesapModel hedefHesap;
                hata = _bHesap.HesapGetir(hedefHesapID, out hedefHesap);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                if (kaynakHesap.MusteriID != hedefHesap.MusteriID)
                {
                    _dataAccess.RollbackTransaction();
                    return "Virman sadece aynı müşterinin hesapları arasında yapılabilir.";
                }

                hata = ValidationHelper.ValidateBakiye(kaynakHesap.KullanilabilirBakiye, tutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                hata = _bHesap.BakiyeGuncelle(kaynakHesapID, -tutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                hata = _bHesap.BakiyeGuncelle(hedefHesapID, tutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                string refNo = CommonFunctions.GenerateTransactionReference();
                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefHesapID, IslemTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, KullaniciID, SubeID, OnayDurumu, 
                                KanalTipi, BasariliMi)
                                VALUES (@refNo, @kaynakID, @hedefID, 'Virman', @tutar, 'TL', 0, @aciklama, 
                                @kullaniciID, @subeID, 'Tamamlandı', 'Sube', 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@kaynakID", kaynakHesapID),
                    new MySqlParameter("@hedefID", hedefHesapID),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@aciklama", aciklama ?? "Virman"),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = _dataAccess.GetLastInsertId();

                hata = _dataAccess.CommitTransaction();
                if (hata != null) return hata;

                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Virman hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}

