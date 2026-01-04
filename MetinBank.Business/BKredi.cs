using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Business
{
    public class BKredi
    {
        private readonly DataAccess _dataAccess;

        public BKredi()
        {
            _dataAccess = new DataAccess();
        }

        // 2026 Kredi Vergileri
        private const decimal KKDF_ORANI = 0.15m;
        private const decimal BSMV_ORANI = 0.15m;

        /// <summary>
        /// Aktif kredi oranlarını getirir
        /// </summary>
        public List<KrediOranModel> GetKrediOranlari()
        {
            List<KrediOranModel> list = new List<KrediOranModel>();
            try
            {
                string query = "SELECT * FROM KrediOranlari WHERE AktifMi = 1";
                DataTable dt;
                string err = _dataAccess.ExecuteQuery(query, null, out dt);
                
                if (err == null && dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new KrediOranModel
                        {
                            OranID = Convert.ToInt32(row["OranID"]),
                            KrediTipi = row["KrediTipi"].ToString(),
                            BaslangicTutar = Convert.ToDecimal(row["BaslangicTutar"]),
                            BitisTutar = Convert.ToDecimal(row["BitisTutar"]),
                            MinVade = Convert.ToInt32(row["MinVade"]),
                            MaxVade = Convert.ToInt32(row["MaxVade"]),
                            FaizOrani = Convert.ToDecimal(row["FaizOrani"]),
                            AktifMi = Convert.ToBoolean(row["AktifMi"])
                        });
                    }
                }
            }
            catch { /* Log error */ }
            finally { _dataAccess.CloseConnection(); }
            
            return list;
        }

        /// <summary>
        /// Tutara göre uygun faiz oranını ve vade limitlerini bulur
        /// </summary>
        public KrediOranModel GetUygunOran(decimal tutar)
        {
            KrediOranModel uygunOran = null;
            try
            {
                string query = "SELECT * FROM KrediOranlari WHERE AktifMi = 1 AND @tutar BETWEEN BaslangicTutar AND BitisTutar LIMIT 1";
                MySqlParameter[] p = { new MySqlParameter("@tutar", tutar) };
                DataTable dt;
                _dataAccess.ExecuteQuery(query, p, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    uygunOran = new KrediOranModel
                    {
                        OranID = Convert.ToInt32(row["OranID"]),
                        KrediTipi = row["KrediTipi"].ToString(),
                        MinVade = Convert.ToInt32(row["MinVade"]),
                        MaxVade = Convert.ToInt32(row["MaxVade"]),
                        FaizOrani = Convert.ToDecimal(row["FaizOrani"])
                    };
                }
            }
            finally { _dataAccess.CloseConnection(); }
            return uygunOran;
        }

        /// <summary>
        /// Kredi hesaplama ve ödeme planı oluşturma
        /// </summary>
        public Dictionary<string, object> Hesapla(decimal tutar, int vade)
        {
            var oranModel = GetUygunOran(tutar);
            if (oranModel == null) return new Dictionary<string, object> { { "Hata", "Bu tutar için uygun oran bulunamadı." } };

            if (vade > oranModel.MaxVade) return new Dictionary<string, object> { { "Hata", $"Bu tutar için maksimum vade {oranModel.MaxVade} aydır." } };

            decimal aylikFaizOrani = oranModel.FaizOrani / 100;
            decimal brutFaizOrani = aylikFaizOrani * (1 + KKDF_ORANI + BSMV_ORANI);

            // Taksit Formülü: A = P * [r(1+r)^n] / [(1+r)^n - 1]
            // P: Ana Para, r: Brüt Faiz, n: Vade
            double faizCarpan = Math.Pow((double)(1 + brutFaizOrani), vade);
            decimal aylikTaksit = tutar * (brutFaizOrani * (decimal)faizCarpan) / ((decimal)faizCarpan - 1);
            decimal toplamOdeme = aylikTaksit * vade;

            return new Dictionary<string, object>
            {
                { "KrediTutari", tutar },
                { "Vade", vade },
                { "FaizOrani", oranModel.FaizOrani },
                { "AylikTaksit", Math.Round(aylikTaksit, 2) },
                { "ToplamOdeme", Math.Round(toplamOdeme, 2) },
                { "KKDF", KKDF_ORANI * 100 },
                { "BSMV", BSMV_ORANI * 100 }
            };
        }

        /// <summary>
        /// Kredi Başvurusu Kaydeder (Otomatik Onay Mekanizması Dahil)
        /// </summary>
        public string BasvuruYap(KrediBasvuruModel model)
        {
            try
            {
                // 1. Faiz ve Vade Kontrolü
                var hesapSonuc = Hesapla(model.TalepEdilenTutar, model.TalepEdilenVade);
                if (hesapSonuc.ContainsKey("Hata")) return hesapSonuc["Hata"].ToString();

                decimal aylikTaksit = (decimal)hesapSonuc["AylikTaksit"];
                decimal faizOrani = (decimal)hesapSonuc["FaizOrani"];

                // 2. Onay Mekanizması
                string durum = "Beklemede";
                string redNedeni = null;
                int? onaylayanID = null;

                // Gelir Oranı (DSR - Debt Service Ratio)
                decimal gelirOrani = 0;
                if (model.AylikGelir > 0)
                    gelirOrani = aylikTaksit / model.AylikGelir;
                else
                    return "Aylık gelir sıfır olamaz.";

                if (model.Kanal == "Web" || model.Kanal == "Mobil")
                {
                    // İnternet Şube Kuralı: DSR <= %45 ise ONAY, üstü RET
                    if (gelirOrani <= 0.45m) 
                    {
                        durum = "ONAYLANDI";
                        onaylayanID = 1; // Sistem
                    }
                    else
                    {
                        durum = "REDDEDILDI";
                        redNedeni = "Gelir/Taksit dengesi uygun değil (Otomatik Değerlendirme).";
                    }
                }
                else
                {
                    // Şube Kuralı: Limitlere göre Müdür Onayı
                    if (gelirOrani > 0.60m)
                    {
                        durum = "REDDEDILDI"; 
                        redNedeni = "Yasal limitler gereği gelirinizin %60'ından fazlası taksit olamaz.";
                    }
                    else
                    {
                        durum = "BEKLEMEDE"; // Müdür onayına düşer
                    }
                }

                // 3. Kayıt
                string query = @"INSERT INTO KrediBasvuru 
                                (MusteriID, TCKN, AdSoyad, CepTelefon, AylikGelir, TalepEdilenTutar, TalepEdilenVade, 
                                 FaizOrani, Kanal, Durum, RedNedeni, BasvuruTarihi)
                                VALUES 
                                (@musteriID, @tckn, @adSoyad, @cep, @gelir, @tutar, @vade, 
                                 @faiz, @kanal, @durum, @neden, NOW())";

                MySqlParameter[] p = {
                    new MySqlParameter("@musteriID", model.MusteriID.HasValue ? (object)model.MusteriID : DBNull.Value),
                    new MySqlParameter("@tckn", model.TCKN),
                    new MySqlParameter("@adSoyad", model.AdSoyad ?? ""),
                    new MySqlParameter("@cep", model.CepTelefon ?? ""),
                    new MySqlParameter("@gelir", model.AylikGelir),
                    new MySqlParameter("@tutar", model.TalepEdilenTutar),
                    new MySqlParameter("@vade", model.TalepEdilenVade),
                    new MySqlParameter("@faiz", faizOrani),
                    new MySqlParameter("@kanal", model.Kanal),
                    new MySqlParameter("@durum", durum),
                    new MySqlParameter("@neden", redNedeni ?? (object)DBNull.Value)
                };

                int affected;
                string err = _dataAccess.ExecuteNonQuery(query, p, out affected);
                if (err != null) return err;

                return durum; // Sonucu döndür (ONAYLANDI/REDDEDILDI/BEKLEMEDE)
            }
            catch (Exception ex)
            {
                return "Başvuru hatası: " + ex.Message;
            }
            finally 
            { 
               _dataAccess.CloseConnection();
            }
        }

        // Onay bekleyen listesi (Şube Müdürü için)
        public DataTable GetBekleyenBasvurular()
        {
             DataTable dt = null;
             string query = "SELECT * FROM KrediBasvuru WHERE Durum = 'BEKLEMEDE' ORDER BY BasvuruTarihi DESC";
             _dataAccess.ExecuteQuery(query, null, out dt);
             _dataAccess.CloseConnection();
             return dt;
        }

        // Müdür Onayı
        public string BasvuruOnaylaReddet(int basvuruID, bool onayla, int mudurID, string aciklama = "")
        {
            string yeniDurum = onayla ? "ONAYLANDI" : "REDDEDILDI";
            string query = @"UPDATE KrediBasvuru 
                           SET Durum = @durum, 
                               OnaylayanKullaniciID = @mudurID, 
                               OnayTarihi = NOW(),
                               RedNedeni = @aciklama
                           WHERE BasvuruID = @id";
            
            MySqlParameter[] p = {
                new MySqlParameter("@durum", yeniDurum),
                new MySqlParameter("@mudurID", mudurID),
                new MySqlParameter("@aciklama", aciklama ?? ""),
                new MySqlParameter("@id", basvuruID)
            };

            int aff;
            string res = _dataAccess.ExecuteNonQuery(query, p, out aff);
            _dataAccess.CloseConnection();
            return res;
        }
        public void KrediKullandir(int basvuruID)
        {
            // 1. Başvuru Bilgilerini Çek
            DataTable dtBasvuru;
            MySqlParameter[] p1 = { new MySqlParameter("@id", basvuruID) };
            _dataAccess.ExecuteQuery("SELECT * FROM KrediBasvuru WHERE BasvuruID = @id", p1, out dtBasvuru);
            
            if (dtBasvuru.Rows.Count == 0) throw new Exception("Başvuru bulunamadı.");

            DataRow r = dtBasvuru.Rows[0];
            int musteriID = Convert.ToInt32(r["MusteriID"]);
            decimal tutar = Convert.ToDecimal(r["TalepEdilenTutar"]);
            int vade = Convert.ToInt32(r["TalepEdilenVade"]);
            decimal faizOrani = Convert.ToDecimal(r["FaizOrani"]);
            
            // Hesaplama Yap
            var plan = Hesapla(tutar, vade);
            decimal aylikTaksit = (decimal)plan["AylikTaksit"];
            decimal toplamOdeme = (decimal)plan["ToplamOdeme"];
            
            // 2. Krediler Tablosuna Ekle
            object krediIDObj;
            string insertKredi = @"
                INSERT INTO Krediler (MusteriID, BasvuruID, KrediTutari, Vade, FaizOrani, ToplamGeriOdeme, BaslangicTarihi, BitisTarihi, KalanAnaPara, Durum)
                VALUES (@mID, @bID, @tutar, @vade, @faiz, @toplam, NOW(), DATE_ADD(NOW(), INTERVAL @vade MONTH), @tutar, 'Aktif');
                SELECT LAST_INSERT_ID();";
            
            MySqlParameter[] p2 = {
                new MySqlParameter("@mID", musteriID),
                new MySqlParameter("@bID", basvuruID),
                new MySqlParameter("@tutar", tutar),
                new MySqlParameter("@vade", vade),
                new MySqlParameter("@faiz", faizOrani),
                new MySqlParameter("@toplam", toplamOdeme)
            };

            _dataAccess.ExecuteScalar(insertKredi, p2, out krediIDObj);
            int krediID = Convert.ToInt32(krediIDObj);

            // 3. Ödeme Planı Oluştur
            decimal kalanAnaPara = tutar;
            DateTime vadeTarihi = DateTime.Now;

            // KKDF ve BSMV oranları
            decimal kkdf = 0.15m;
            decimal bsmv = 0.15m;
            decimal vergiCarpani = 1 + kkdf + bsmv;

            for (int i = 1; i <= vade; i++)
            {
                vadeTarihi = vadeTarihi.AddMonths(1);
                
                // Aylık Faiz: Kalan Ana Para * (Brüt Faiz)
                // Dikkat: Veritabanındaki FaizOrani müşteri faizidir (Net). Vergiler eklenir.
                decimal brutAylikOran = (faizOrani / 100m) * vergiCarpani;
                
                decimal aylikToplamFaiz = Math.Round(kalanAnaPara * brutAylikOran, 2);
                
                // DB'ye kaydederken Faiz ve Vergi'yi ayırabiliriz
                // ToplamFaiz = HamFaiz + Vergi
                // HamFaiz = ToplamFaiz / 1.30
                decimal hamFaiz = Math.Round(aylikToplamFaiz / vergiCarpani, 2);
                decimal vergiTutari = aylikToplamFaiz - hamFaiz;

                decimal anaParaBileseni = Math.Round(aylikTaksit - aylikToplamFaiz, 2);
                
                if (i == vade || anaParaBileseni > kalanAnaPara)
                {
                    anaParaBileseni = kalanAnaPara;
                    aylikTaksit = aylikToplamFaiz + anaParaBileseni;
                }

                kalanAnaPara -= anaParaBileseni;
                if(kalanAnaPara < 0) kalanAnaPara = 0;

                string insertPlan = @"
                    INSERT INTO KrediOdemePlani (KrediID, TaksitNo, VadeTarihi, TaksitTutari, AnaParaTutari, FaizTutari, VergiTutari, KalanAnaPara)
                    VALUES (@kID, @no, @tarih, @taksit, @anaPara, @faiz, @vergi, @kalan)";
                
                MySqlParameter[] pPlan = {
                    new MySqlParameter("@kID", krediID),
                    new MySqlParameter("@no", i),
                    new MySqlParameter("@tarih", vadeTarihi),
                    new MySqlParameter("@taksit", aylikTaksit),
                    new MySqlParameter("@anaPara", anaParaBileseni),
                    new MySqlParameter("@faiz", hamFaiz),
                    new MySqlParameter("@vergi", vergiTutari),
                    new MySqlParameter("@kalan", kalanAnaPara)
                };

                int aff;
                _dataAccess.ExecuteNonQuery(insertPlan, pPlan, out aff);
            }

            // 4. Parayı Hesaba Yatır
            object hesapIDObj;
            string qHesap = "SELECT HesapID FROM Hesaplar WHERE MusteriID = @mID AND HesapTurID = 1 LIMIT 1";
            MySqlParameter[] pHesap = { new MySqlParameter("@mID", musteriID) };
            _dataAccess.ExecuteScalar(qHesap, pHesap, out hesapIDObj);
            
            if (hesapIDObj != null)
            {
                int hesapID = Convert.ToInt32(hesapIDObj);
                
                string updateHesap = "UPDATE Hesaplar SET Bakiye = Bakiye + @tutar WHERE HesapID=@hID";
                MySqlParameter[] pUpdate = { 
                    new MySqlParameter("@tutar", tutar), 
                    new MySqlParameter("@hID", hesapID) 
                };
                int aff2;
                _dataAccess.ExecuteNonQuery(updateHesap, pUpdate, out aff2);
                
                string insertIslem = @"INSERT INTO Islem (HesapID, IslemTuru, Tutar, Aciklama, Tarih) 
                                      VALUES (@hID, 'KrediKullandirim', @tutar, 'Kredi Kullandırımı', NOW())";
                MySqlParameter[] pIslem = { 
                    new MySqlParameter("@hID", hesapID),
                    new MySqlParameter("@tutar", tutar)
                };
                int aff3;
                _dataAccess.ExecuteNonQuery(insertIslem, pIslem, out aff3);
            }

            // Başvuru durumunu güncelle
            MySqlParameter[] pFinal = { new MySqlParameter("@id", basvuruID) };
            int affFinal;
            _dataAccess.ExecuteNonQuery("UPDATE KrediBasvuru SET Durum='Kullandirildi' WHERE BasvuruID=@id", pFinal, out affFinal);
        }
    }
}
