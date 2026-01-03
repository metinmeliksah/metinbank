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

                // Onay durumu
                string onayDurumu = BelirleIlkOnayDurumu(tutar);
                
                string refNo = CommonFunctions.GenerateTransactionReference();
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
                         int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                // EFT ücreti
                decimal eftUcreti = 5.00m;
                decimal toplamTutar = tutar + eftUcreti;

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
                int kaynakHesapID = Convert.ToInt32(row["KaynakHesapID"]);
                int? hedefHesapID = row["HedefHesapID"] != DBNull.Value ? (int?)Convert.ToInt32(row["HedefHesapID"]) : null;
                decimal toplamTutar = tutar + Convert.ToDecimal(row["IslemUcreti"]);

                string yeniDurum = "";
                
                // Rol ve Tutar Kontrolü
                if (rol == "Mudur")
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
                else if (rol == "GenelMerkez")
                {
                    if (mevcutDurum != "OnayBekliyor_GenelMerkez") { _dataAccess.RollbackTransaction(); return "Bu işlem GM onayı beklemiyor."; }
                    yeniDurum = "Tamamlandı";
                }
                else
                {
                    _dataAccess.RollbackTransaction();
                    return "Geçersiz onay rolü.";
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

                // Log Güncelle (Önceki logu güncelle veya yeni log at - Basitlik için yeni log)
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
                    // 1. Kaynak hesaptan bloke ve bakiyeyi düş
                    // Not: Para zaten bakiyede duruyor ama BlokeBakiye'de de duruyor. 
                    // KullanilabilirBakiye = Bakiye - Bloke.
                    // İşlem yapılırsa: Bakiye düşer, Bloke çözülür.
                    
                    // Önce Blokeyi Kaldır ve Bakiyeyi Düş (Çünkü create anında bakiyeden düşmemiştik, sadece bloke koymuştuk değil mi?
                    // KONROL: Eski koda bakalım. "CreateOnayRec" ise bakiye düşmüyordu, SADECE Bloke artıyordu.
                    // string queryBloke = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye + @tutar WHERE HesapID = @hesapID";
                    
                    // Yani Bakiye hala eski Tutar. Bloke arttı.
                    // Şimdi işlem tamamlandı:
                    // BlokeBakiye azalmalı (-Tutar)
                    // Bakiye azalmalı (-Tutar)
                    
                    string queryBlokeCoz = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye - @tutar, Bakiye = Bakiye - @tutar WHERE HesapID = @hesapID";
                    MySqlParameter[] paramsBlokeCoz = new MySqlParameter[]
                    {
                        new MySqlParameter("@tutar", toplamTutar), // EFT ise ücret dahil
                        new MySqlParameter("@hesapID", kaynakHesapID)
                    };
                    hata = _dataAccess.ExecuteNonQuery(queryBlokeCoz, paramsBlokeCoz, out affected);
                    if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

                    // 2. Hedef hesaba ekle (Varsa)
                    if (hedefHesapID.HasValue)
                    {
                        hata = _bHesap.BakiyeGuncelle(hedefHesapID.Value, tutar); // Burada bloke yok, direkt ekle
                         if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }
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
                int kaynakHesapID = Convert.ToInt32(row["KaynakHesapID"]);
                decimal tutar = Convert.ToDecimal(row["Tutar"]);
                decimal ucret = Convert.ToDecimal(row["IslemUcreti"]);
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

                // Blokeyi Kaldır (Ama bakiyeden düşme! Para iade edilmiş gibi olur, yani blokeyi sıfırla)
                string queryBlokeCoz = "UPDATE Hesap SET BlokeBakiye = BlokeBakiye - @tutar WHERE HesapID = @hesapID";
                MySqlParameter[] paramsBlokeCoz = new MySqlParameter[]
                {
                    new MySqlParameter("@tutar", toplamTutar),
                    new MySqlParameter("@hesapID", kaynakHesapID)
                };
                hata = _dataAccess.ExecuteNonQuery(queryBlokeCoz, paramsBlokeCoz, out affected);
                 if (hata != null) { _dataAccess.RollbackTransaction(); return hata; }

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
            if (rol == "Mudur") durum = "OnayBekliyor_Mudur";
            else if (rol == "GenelMerkez") durum = "OnayBekliyor_GenelMerkez";
            else return "Geçersiz rol.";

            // Basit select
             string query = @"SELECT i.*, 
                            CASE 
                                WHEN i.IslemTipi = 'Havale' THEN CONCAT('Havale - ', i.AliciAdi)
                                WHEN i.IslemTipi = 'EFT' THEN CONCAT('EFT - ', i.AliciAdi)
                                ELSE i.IslemTipi 
                            END as IslemTanimi,
                            h.HesapNo, CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdSoyad
                            FROM Islem i
                            JOIN Hesap h ON i.KaynakHesapID = h.HesapID
                            JOIN Musteri m ON h.MusteriID = m.MusteriID
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

