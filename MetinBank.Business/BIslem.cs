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

        /// <summary>
        /// Tutara göre ilk onay durumunu belirler
        /// </summary>
        private string BelirleIlkOnayDurumu(decimal tutar)
        {
            if (tutar <= 50000) return "Tamamlandı"; 
            return "OnayBekliyor_Mudur"; 
        }

        private string CreateOnayRec(long islemID, string islemTipi, int kullaniciID, string rol, string durum)
        {
             string query = @"INSERT INTO OnayLog (IslemID, IslemTipi, TalepEdenID, OnayDurumu, BeklenenOnaylayanRol)
                            VALUES (@islemID, @islemTipi, @talepEdenID, @durum, @beklenenRol)";
            
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@islemID", islemID),
                new MySqlParameter("@islemTipi", islemTipi),
                new MySqlParameter("@talepEdenID", kullaniciID),
                new MySqlParameter("@durum", durum),
                new MySqlParameter("@beklenenRol", rol)
            };

            int affectedRows;
            return _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
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

                // Onay durumu belirle
                string onayDurumu = BelirleIlkOnayDurumu(tutar);

                // İşlem kaydı
                string refNo = CommonFunctions.GenerateTransactionReference();
                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefHesapID, IslemTipi, IslemAltTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, KullaniciID, SubeID, OnayDurumu, KanalTipi, BasariliMi)
                                VALUES (@refNo, NULL, @hesapID, 'Yatırma', 'Nakit', @tutar, 'TL', 0, @aciklama, @kullaniciID, 
                                @subeID, @onayDurumu, 'Sube', 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@hesapID", hesapID),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@aciklama", aciklama ?? "Para yatırma"),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID),
                    new MySqlParameter("@onayDurumu", onayDurumu)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = _dataAccess.GetLastInsertId();

                if (onayDurumu == "Tamamlandı")
                {
                    // Normal bakiye güncelleme
                    hata = _bHesap.BakiyeGuncelle(hesapID, tutar);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }
                else
                {
                    // Onay bekliyor: Hem Bakiyeye Ekle HEM de Blokeye Ekle (Kullanılabilir artmasın)
                    string queryBakiyeBloke = "UPDATE Hesap SET Bakiye = Bakiye + @tutar, BlokeBakiye = BlokeBakiye + @tutar WHERE HesapID = @hesapID";
                    MySqlParameter[] paramsBloke = new MySqlParameter[]
                    {
                        new MySqlParameter("@tutar", tutar),
                        new MySqlParameter("@hesapID", hesapID)
                    };
                    hata = _dataAccess.ExecuteNonQuery(queryBakiyeBloke, paramsBloke, out affectedRows);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                    // Log oluştur
                    hata = CreateOnayRec(islemID, "Yatırma", kullaniciID, "Mudur", "Beklemede");
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }

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

                // Onay durumu
                string onayDurumu = BelirleIlkOnayDurumu(tutar);

                // İşlem kaydı
                string refNo = CommonFunctions.GenerateTransactionReference();
                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefHesapID, IslemTipi, IslemAltTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, KullaniciID, SubeID, OnayDurumu, KanalTipi, BasariliMi)
                                VALUES (@refNo, @hesapID, NULL, 'Çekme', 'Nakit', @tutar, 'TL', 0, @aciklama, @kullaniciID, 
                                @subeID, @onayDurumu, 'Sube', 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@hesapID", hesapID),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@aciklama", aciklama ?? "Para çekme"),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID),
                    new MySqlParameter("@onayDurumu", onayDurumu)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = _dataAccess.GetLastInsertId();

                if (onayDurumu == "Tamamlandı")
                {
                    // Bakiye güncelle
                    hata = _bHesap.BakiyeGuncelle(hesapID, -tutar);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }
                else
                {
                    // Onay bekliyor: Tutarı bloke et (Kullanılabilir bakiyeden düşer)
                    string queryBloke = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye + @tutar WHERE HesapID = @hesapID";
                    MySqlParameter[] paramsBloke = new MySqlParameter[]
                    {
                        new MySqlParameter("@tutar", tutar),
                        new MySqlParameter("@hesapID", hesapID)
                    };
                    hata = _dataAccess.ExecuteNonQuery(queryBloke, paramsBloke, out affectedRows);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                    // Log
                    hata = CreateOnayRec(islemID, "Çekme", kullaniciID, "Mudur", "Beklemede");
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }

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
                            int kullaniciID, int subeID, decimal islemUcreti, out long islemID)
        {
            islemID = 0;

            try
            {
                decimal toplamTutar = tutar + islemUcreti;

                string hata = ValidationHelper.ValidateTutar(tutar, 1, 1000000);
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

                hata = ValidationHelper.ValidateBakiye(kaynakHesap.KullanilabilirBakiye, toplamTutar);
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

                // Onay durumu
                string onayDurumu = BelirleIlkOnayDurumu(tutar);
                
                string refNo = CommonFunctions.GenerateTransactionReference();
                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefHesapID, HedefIBAN, IslemTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, AliciAdi, KullaniciID, SubeID, OnayDurumu, 
                                KanalTipi, IPAdresi, BasariliMi)
                                VALUES (@refNo, @kaynakID, @hedefID, @hedefIBAN, 'Havale', @tutar, 'TL', @islemUcreti, @aciklama, 
                                @aliciAdi, @kullaniciID, @subeID, @onayDurumu, 'Internet', @ip, 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@kaynakID", kaynakHesapID),
                    new MySqlParameter("@hedefID", (object)hedefHesapID ?? DBNull.Value),
                    new MySqlParameter("@hedefIBAN", hedefIBAN),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@islemUcreti", islemUcreti),
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

                if (onayDurumu == "Tamamlandı")
                {
                    hata = _bHesap.BakiyeGuncelle(kaynakHesapID, -toplamTutar);
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
                        new MySqlParameter("@tutar", toplamTutar),
                        new MySqlParameter("@hesapID", kaynakHesapID)
                    };
                    hata = _dataAccess.ExecuteNonQuery(queryBloke, paramsBloke, out affectedRows);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                    // Log
                    hata = CreateOnayRec(islemID, "Havale", kullaniciID, "Mudur", "Beklemede");
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }

                hata = _dataAccess.CommitTransaction();
                return hata;
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
                         int kullaniciID, int subeID, decimal islemUcreti, out long islemID)
        {
            islemID = 0;

            try
            {
                decimal toplamTutar = tutar + islemUcreti;

                string hata = ValidationHelper.ValidateTutar(tutar, 1, 1000000);
                if (hata != null) return hata;

                hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // Bakiye kontrolü (ücret dahil)
                HesapModel kaynakHesap;
                hata = _bHesap.HesapGetir(kaynakHesapID, out kaynakHesap);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                hata = ValidationHelper.ValidateBakiye(kaynakHesap.KullanilabilirBakiye, toplamTutar);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                string refNo = CommonFunctions.GenerateTransactionReference();
                string onayDurumu = BelirleIlkOnayDurumu(tutar);

                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefIBAN, IslemTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, AliciAdi, KullaniciID, SubeID, OnayDurumu, 
                                KanalTipi, IPAdresi, BasariliMi)
                                VALUES (@refNo, @kaynakID, @hedefIBAN, 'EFT', @tutar, 'TL', @ucret, @aciklama, 
                                @aliciAdi, @kullaniciID, @subeID, @onayDurumu, 'Internet', @ip, 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@kaynakID", kaynakHesapID),
                    new MySqlParameter("@hedefIBAN", hedefIBAN),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@ucret", islemUcreti),
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
                else
                {
                    // Bloke bakiye ekle
                    string queryBloke = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye + @tutar WHERE HesapID = @hesapID";
                    MySqlParameter[] paramsBloke = new MySqlParameter[]
                    {
                        new MySqlParameter("@tutar", toplamTutar),
                        new MySqlParameter("@hesapID", kaynakHesapID)
                    };
                    hata = _dataAccess.ExecuteNonQuery(queryBloke, paramsBloke, out affectedRows);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                    // Onay kaydı oluştur
                    hata = CreateOnayRec(islemID, "EFT", kullaniciID, "Mudur", "Beklemede");
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }

                hata = _dataAccess.CommitTransaction();
                return hata;
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
                string hata = ValidationHelper.ValidateTutar(tutar, 1, 1000000);
                if (hata != null) return hata;

                hata = _dataAccess.BeginTransaction();
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

                // Onay durumu
                string onayDurumu = BelirleIlkOnayDurumu(tutar);

                string refNo = CommonFunctions.GenerateTransactionReference();
                string query = @"INSERT INTO Islem (IslemReferansNo, KaynakHesapID, HedefHesapID, IslemTipi, 
                                Tutar, ParaBirimi, IslemUcreti, Aciklama, KullaniciID, SubeID, OnayDurumu, 
                                KanalTipi, BasariliMi)
                                VALUES (@refNo, @kaynakID, @hedefID, 'Virman', @tutar, 'TL', 0, @aciklama, 
                                @kullaniciID, @subeID, @onayDurumu, 'Sube', 1)";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@refNo", refNo),
                    new MySqlParameter("@kaynakID", kaynakHesapID),
                    new MySqlParameter("@hedefID", hedefHesapID),
                    new MySqlParameter("@tutar", tutar),
                    new MySqlParameter("@aciklama", aciklama ?? "Virman"),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@subeID", subeID),
                    new MySqlParameter("@onayDurumu", onayDurumu)
                };

                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(query, parameters, out affectedRows);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                islemID = _dataAccess.GetLastInsertId();

                if (onayDurumu == "Tamamlandı")
                {
                    hata = _bHesap.BakiyeGuncelle(kaynakHesapID, -tutar);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                    hata = _bHesap.BakiyeGuncelle(hedefHesapID, tutar);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
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

                    // Onay kaydı oluştur
                    hata = CreateOnayRec(islemID, "Virman", kullaniciID, "Mudur", "Beklemede");
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                }

                hata = _dataAccess.CommitTransaction();
                return hata;
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

        // YENİ: İşlem Onaylama Metodu
        public string IslemOnayla(long islemID, int kullaniciID, string rol)
        {
            try
            {
                string hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // İşlem detaylarını çek
                string queryIslem = "SELECT * FROM Islem WHERE IslemID = @islemID FOR UPDATE";
                MySqlParameter[] paramIslem = new MySqlParameter[] { new MySqlParameter("@islemID", islemID) };
                DataTable dtIslem;
                hata = _dataAccess.ExecuteQuery(queryIslem, paramIslem, out dtIslem);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                if (dtIslem.Rows.Count == 0) { _dataAccess.RollbackTransaction(); return "İşlem bulunamadı."; }

                DataRow row = dtIslem.Rows[0];
                string mevcutDurum = row["OnayDurumu"].ToString();
                decimal tutar = Convert.ToDecimal(row["Tutar"]);
                string islemTipi = row["IslemTipi"].ToString();
                
                // ID'leri güvenli al
                int? kaynakHesapID = row["KaynakHesapID"] != DBNull.Value ? (int?)Convert.ToInt32(row["KaynakHesapID"]) : null;
                int? hedefHesapID = row["HedefHesapID"] != DBNull.Value ? (int?)Convert.ToInt32(row["HedefHesapID"]) : null;
                
                decimal toplamTutar = tutar + Convert.ToDecimal(row["IslemUcreti"]);

                string yeniDurum = "";

                // Rol Normalizasyonu
                string normalizedRol = "";
                if (rol.IndexOf("Mudur", StringComparison.OrdinalIgnoreCase) >= 0 || 
                    rol.IndexOf("Müdür", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    normalizedRol = "Mudur";
                }
                else if (rol.IndexOf("Genel", StringComparison.OrdinalIgnoreCase) >= 0 || 
                         rol.IndexOf("Merkez", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    normalizedRol = "GenelMerkez";
                }
                else
                {
                     _dataAccess.RollbackTransaction();
                     return "Geçersiz onay rolü.";
                }
                
                // Rol ve Tutar Kontrolü
                if (normalizedRol == "Mudur")
                {
                    if (mevcutDurum != "OnayBekliyor_Mudur") { _dataAccess.RollbackTransaction(); return "Bu işlem müdür onayı beklemiyor."; }
                    
                    if (tutar > 250000)
                    {
                        yeniDurum = "OnayBekliyor_GenelMerkez"; // 250k üstü GM'ye gider
                    }
                    else
                    {
                        yeniDurum = "Tamamlandı"; // Altı ise biter
                    }
                }
                else if (normalizedRol == "GenelMerkez")
                {
                    if (mevcutDurum != "OnayBekliyor_GenelMerkez") { _dataAccess.RollbackTransaction(); return "Bu işlem GM onayı beklemiyor."; }
                    yeniDurum = "Tamamlandı";
                }

                // Durum Güncelle
                string queryUpdate = "UPDATE Islem SET OnayDurumu = @yeniDurum WHERE IslemID = @islemID";
                MySqlParameter[] paramsUpdate = new MySqlParameter[] 
                { 
                    new MySqlParameter("@yeniDurum", yeniDurum),
                    new MySqlParameter("@islemID", islemID)
                };
                hata = _dataAccess.ExecuteNonQuery(queryUpdate, paramsUpdate, out int affected);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                // Log Güncelle
                string queryLog = @"INSERT INTO OnayLog (IslemID, IslemTipi, OnaylayanID, OnayDurumu, OnayTarihi)
                                  VALUES (@islemID, @islemTipi, @kullaniciID, @durum, NOW())";
                MySqlParameter[] paramsLog = new MySqlParameter[]
                {
                    new MySqlParameter("@islemID", islemID),
                    new MySqlParameter("@islemTipi", islemTipi),
                    new MySqlParameter("@kullaniciID", kullaniciID),
                    new MySqlParameter("@durum", yeniDurum == "Tamamlandı" ? "Onaylandı" : "Aktarıldı")
                };
                _dataAccess.ExecuteNonQuery(queryLog, paramsLog, out affected);


                // Eğer TAMAMLANDI ise para transferini gerçekleştir
                if (yeniDurum == "Tamamlandı")
                {
                    if (islemTipi == "Yatırma")
                    {
                        // Yatırma: Sadece blokeyi (ve bakiyeyi güncellemeye gerek yok, zaten eklenmişti) çözmeliyiz AMA
                        // ParaYatir fonksiyonunda hem Bakiye hem BlokeBakiye artırılmıştı.
                        // Yani şu an para hesapta VAR ama BLOKE.
                        // BlokeBakiye düşürülmeli.
                        if (hedefHesapID.HasValue)
                        {
                            string queryBlokeCoz = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye - @tutar WHERE HesapID = @hesapID";
                            MySqlParameter[] paramsBlokeCoz = new MySqlParameter[]
                            {
                                new MySqlParameter("@tutar", tutar), 
                                new MySqlParameter("@hesapID", hedefHesapID.Value)
                            };
                            hata = _dataAccess.ExecuteNonQuery(queryBlokeCoz, paramsBlokeCoz, out affected);
                            if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                        }
                    }
                    else
                    {
                        // Havale, EFT, Virman, Çekme:
                        // Kaynak hesaptan hem Bloke hem Bakiye düşülmeli.
                        // Çünkü işlem oluşturulurken Bakiye düşülmemiş, SADECE Bloke eklenmişti (ParaCek'te bu şekilde planladım).
                        // Havale/EFT/Virman'da da böyle: 'OnayBekliyor' durumunda bakiye düşülmedi, bloke kondu.
                        
                        if (kaynakHesapID.HasValue)
                        {
                            string queryBlokeCoz = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye - @tutar, Bakiye = Bakiye - @tutar WHERE HesapID = @hesapID";
                            MySqlParameter[] paramsBlokeCoz = new MySqlParameter[]
                            {
                                new MySqlParameter("@tutar", toplamTutar), // EFT ise ücret dahil
                                new MySqlParameter("@hesapID", kaynakHesapID.Value)
                            };
                            hata = _dataAccess.ExecuteNonQuery(queryBlokeCoz, paramsBlokeCoz, out affected);
                            if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                        }

                        // Hedef hesaba ekle (Sadece Havale ve Virman için, Çekme/EFT için hedef hesap bizde değil veya NULL)
                        if (hedefHesapID.HasValue && (islemTipi == "Havale" || islemTipi == "Virman"))
                        {
                            hata = _bHesap.BakiyeGuncelle(hedefHesapID.Value, tutar);
                             if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                        }
                    }
                }
                
                hata = _dataAccess.CommitTransaction();
                return hata;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Onay hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string IslemReddet(long islemID, int kullaniciID, string aciklama)
        {
             try
            {
                string hata = _dataAccess.BeginTransaction();
                if (hata != null) return hata;

                // İşlem detaylarını çek
                string queryIslem = "SELECT * FROM Islem WHERE IslemID = @islemID FOR UPDATE";
                MySqlParameter[] paramIslem = new MySqlParameter[] { new MySqlParameter("@islemID", islemID) };
                DataTable dtIslem;
                hata = _dataAccess.ExecuteQuery(queryIslem, paramIslem, out dtIslem);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                
                DataRow row = dtIslem.Rows[0];
                int? kaynakHesapID = row["KaynakHesapID"] != DBNull.Value ? (int?)Convert.ToInt32(row["KaynakHesapID"]) : null;
                int? hedefHesapID = row["HedefHesapID"] != DBNull.Value ? (int?)Convert.ToInt32(row["HedefHesapID"]) : null;
                
                decimal tutar = Convert.ToDecimal(row["Tutar"]);
                decimal ucret = Convert.ToDecimal(row["IslemUcreti"]);
                string islemTipi = row["IslemTipi"].ToString();
                
                decimal toplamTutar = tutar + ucret;

                // Durum Güncelle
                string queryUpdate = "UPDATE Islem SET OnayDurumu = 'Reddedildi', Aciklama = CONCAT(Aciklama, ' | Red: ', @aciklama) WHERE IslemID = @islemID";
                MySqlParameter[] paramsUpdate = new MySqlParameter[] 
                { 
                    new MySqlParameter("@aciklama", aciklama),
                    new MySqlParameter("@islemID", islemID)
                };
                hata = _dataAccess.ExecuteNonQuery(queryUpdate, paramsUpdate, out int affected);
                if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                if (islemTipi == "Yatırma")
                {
                    // Yatırma reddedildi: Parayı hesaptan geri almalıyız.
                    // Hem Bakiye hem Bloke düşülecek.
                    if (hedefHesapID.HasValue)
                    {
                        string queryTersIslem = "UPDATE Hesap SET Bakiye = Bakiye - @tutar, BlokeBakiye = BlokeBakiye - @tutar WHERE HesapID = @hesapID";
                        MySqlParameter[] paramsTers = new MySqlParameter[]
                        {
                            new MySqlParameter("@tutar", tutar),
                            new MySqlParameter("@hesapID", hedefHesapID.Value)
                        };
                        hata = _dataAccess.ExecuteNonQuery(queryTersIslem, paramsTers, out affected);
                        if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                    }
                }
                else
                {
                    // Çekme, Havale, EFT, Virman reddedildi:
                    // Sadece blokeyi kaldırmalıyız (Bakiye zaten düşülmemişti).
                    if (kaynakHesapID.HasValue)
                    {
                        string queryBlokeCoz = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye - @tutar WHERE HesapID = @hesapID";
                        MySqlParameter[] paramsBlokeCoz = new MySqlParameter[]
                        {
                            new MySqlParameter("@tutar", toplamTutar),
                            new MySqlParameter("@hesapID", kaynakHesapID.Value)
                        };
                        hata = _dataAccess.ExecuteNonQuery(queryBlokeCoz, paramsBlokeCoz, out affected);
                         if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
                    }
                }

                hata = _dataAccess.CommitTransaction();
                return null;
            }
            catch (Exception ex)
            {
                _dataAccess.RollbackTransaction();
                return $"Red hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Müşterinin işlem geçmişini getirir
        /// </summary>
        public string MusterininIslemleri(int musteriID, out DataTable islemler)
        {
            islemler = null;

            try
            {
                string query = @"SELECT i.IslemID, i.IslemReferansNo, i.IslemTipi, i.Tutar, i.ParaBirimi,
                                i.IslemTarihi, i.OnayDurumu, i.Aciklama, i.AliciAdi,
                                h.IBAN as KaynakIBAN, i.HedefIBAN
                                FROM Islem i
                                LEFT JOIN Hesap h ON i.KaynakHesapID = h.HesapID
                                WHERE h.MusteriID = @musteriID OR i.HedefHesapID IN 
                                    (SELECT HesapID FROM Hesap WHERE MusteriID = @musteriID)
                                ORDER BY i.IslemTarihi DESC
                                LIMIT 500";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@musteriID", musteriID)
                };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out islemler);
                return hata;
            }
            catch (Exception ex)
            {
                return $"İşlem geçmişi hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        /// <summary>
        /// Hesabın işlem geçmişini getirir
        /// </summary>
        public string HesabinIslemleri(int hesapID, out DataTable islemler)
        {
            islemler = null;

            try
            {
                string query = @"SELECT i.IslemID, i.IslemReferansNo, i.IslemTipi, i.Tutar, i.ParaBirimi,
                                i.IslemTarihi, i.OnayDurumu, i.Aciklama, i.AliciAdi,
                                h.IBAN as KaynakIBAN, i.HedefIBAN
                                FROM Islem i
                                LEFT JOIN Hesap h ON i.KaynakHesapID = h.HesapID
                                WHERE i.KaynakHesapID = @hesapID OR i.HedefHesapID = @hesapID
                                ORDER BY i.IslemTarihi DESC
                                LIMIT 500";

                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@hesapID", hesapID)
                };

                string hata = _dataAccess.ExecuteQuery(query, parameters, out islemler);
                return hata;
            }
            catch (Exception ex)
            {
                return $"İşlem geçmişi hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public string OnayBekleyenIslemleriGetir(string rol, out DataTable islemler)
        {
            islemler = null;
            string durum = "";
            
            // Rol kontrolü
            if (rol.IndexOf("Mudur", StringComparison.OrdinalIgnoreCase) >= 0 || 
                rol.IndexOf("Müdür", StringComparison.OrdinalIgnoreCase) >= 0) 
            {
                durum = "OnayBekliyor_Mudur";
            }
            else if (rol.IndexOf("Genel", StringComparison.OrdinalIgnoreCase) >= 0 || 
                     rol.IndexOf("Merkez", StringComparison.OrdinalIgnoreCase) >= 0) 
            {
                durum = "OnayBekliyor_GenelMerkez";
            }
            else 
            {
                return "Bu işlem için yetkiniz bulunmamaktadır.";
            }

            // Gelişmiş sorgu - Çift kayıt sorununu çözer ve detayları getirir
            string query = @"SELECT 
                                i.IslemID,
                                i.IslemReferansNo,
                                i.IslemTipi,
                                i.Tutar,
                                i.ParaBirimi,
                                i.IslemTarihi,
                                i.OnayDurumu,
                                i.Aciklama,
                                i.IslemUcreti,
                                
                                -- Gönderen Bilgileri
                                h_kaynak.IBAN as GonderenIBAN,
                                CONCAT(m_kaynak.Ad, ' ', m_kaynak.Soyad) as GonderenAdSoyad,

                                -- Alıcı Bilgileri (Eğer iç transfer ise HedefHesap'tan, dış ise Islem tablosundan)
                                CASE 
                                    WHEN i.HedefIBAN IS NOT NULL AND i.HedefIBAN != '' THEN i.HedefIBAN 
                                    ELSE h_hedef.IBAN 
                                END as AliciIBAN,
                                
                                CASE 
                                    WHEN i.AliciAdi IS NOT NULL AND i.AliciAdi != '' THEN i.AliciAdi
                                    ELSE CONCAT(m_hedef.Ad, ' ', m_hedef.Soyad)
                                END as AliciAdSoyad,

                                -- Oluşturan Personel
                                CONCAT(k.Ad, ' ', k.Soyad) as OlusturanPersonel

                            FROM Islem i
                            LEFT JOIN Hesap h_kaynak ON i.KaynakHesapID = h_kaynak.HesapID
                            LEFT JOIN Musteri m_kaynak ON h_kaynak.MusteriID = m_kaynak.MusteriID
                            LEFT JOIN Hesap h_hedef ON i.HedefHesapID = h_hedef.HesapID
                            LEFT JOIN Musteri m_hedef ON h_hedef.MusteriID = m_hedef.MusteriID
                            LEFT JOIN Kullanici k ON i.KullaniciID = k.KullaniciID
                            WHERE i.OnayDurumu = @durum
                            ORDER BY i.IslemTarihi ASC";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@durum", durum)
            };

            return _dataAccess.ExecuteQuery(query, parameters, out islemler);
        }
    }
}

