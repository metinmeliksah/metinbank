/*
 * MetinBank - Müşteri Business Layer
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Müşteri iş mantığı sınıfı
 * Standart: Business sınıfları B prefix'i ile başlar
 */

using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text;
using MetinBank.Musteri.SP;

namespace MetinBank.Musteri.Business
{
    /// <summary>
    /// Müşteri Business Logic
    /// Birden fazla SP kullanarak anlamlı işlemler gerçekleştirir
    /// Prefix: B (BMusteriIslem)
    /// </summary>
    public static class BMusteriIslem
    {
        /// <summary>
        /// Yeni müşteri ekler ve log kaydı oluşturur
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// Birden fazla SP çağırır (Müşteri ekleme + Log ekleme)
        /// </summary>
        /// <param name="conn">Oracle Connection (Service katmanından gelir)</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <param name="ad">Ad</param>
        /// <param name="soyad">Soyad</param>
        /// <param name="eposta">E-posta</param>
        /// <param name="telefon">Telefon</param>
        /// <param name="opAd">Operatör adı (işlemi yapan)</param>
        /// <returns>Müşteri No (long tip - standart)</returns>
        public static long MusteriEkle(OracleConnection conn, OracleTransaction trans,
                                       string tcKimlikNo, string ad, string soyad,
                                       string eposta, string telefon, string opAd)
        {
            long musteriNo = 0;

            try
            {
                // 1. Validasyon kontrolleri
                if (string.IsNullOrWhiteSpace(tcKimlikNo))
                {
                    throw new Exception("TC Kimlik No boş olamaz");
                }

                if (tcKimlikNo.Length != 11)
                {
                    throw new Exception("TC Kimlik No 11 karakter olmalıdır");
                }

                if (string.IsNullOrWhiteSpace(ad))
                {
                    throw new Exception("Ad boş olamaz");
                }

                if (string.IsNullOrWhiteSpace(soyad))
                {
                    throw new Exception("Soyad boş olamaz");
                }

                // 2. Aynı TC Kimlik No ile kayıt var mı kontrol et
                DataTable dtKontrol = SpMusteri.MusteriBul(conn, tcKimlikNo);
                if (dtKontrol != null && dtKontrol.Rows.Count > 0)
                {
                    throw new Exception("Bu TC Kimlik No ile kayıtlı müşteri zaten mevcut");
                }

                // 3. Müşteri ekle
                musteriNo = SpMusteri.MusteriEkle(conn, trans, tcKimlikNo, ad, soyad, eposta, telefon);

                // 4. Log ekle (Başka bir SP - örnek amaçlı)
                // SpLog.LogEkle(conn, trans, "MUSTERI_EKLE", musteriNo.ToString(), opAd);

                // 5. StringBuilder kullanarak mesaj oluştur (Standart)
                StringBuilder mesaj = new StringBuilder();
                mesaj.Append("Müşteri başarıyla eklendi. ");
                mesaj.Append("Müşteri No: ");
                mesaj.Append(musteriNo.ToString());
                mesaj.Append(", Ad Soyad: ");
                mesaj.Append(ad);
                mesaj.Append(" ");
                mesaj.Append(soyad);

                // Log için mesaj hazır
                Console.WriteLine(mesaj.ToString());

                return musteriNo;
            }
            catch (Exception ex)
            {
                // Exception standart: ex, ex1, ex2
                throw new Exception("Müşteri eklenirken hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Müşteri günceller ve log kaydı oluşturur
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="musteriNo">Müşteri No (long tip - standart)</param>
        /// <param name="ad">Ad</param>
        /// <param name="soyad">Soyad</param>
        /// <param name="eposta">E-posta</param>
        /// <param name="telefon">Telefon</param>
        /// <param name="opAd">Operatör adı</param>
        /// <returns>Başarılı ise null, hata ise hata mesajı</returns>
        public static string MusteriGuncelle(OracleConnection conn, OracleTransaction trans,
                                            long musteriNo, string ad, string soyad,
                                            string eposta, string telefon, string opAd)
        {
            try
            {
                // 1. Validasyon
                if (musteriNo <= 0)
                {
                    return "Müşteri No geçersiz";
                }

                if (string.IsNullOrWhiteSpace(ad))
                {
                    return "Ad boş olamaz";
                }

                if (string.IsNullOrWhiteSpace(soyad))
                {
                    return "Soyad boş olamaz";
                }

                // 2. Müşteri güncelle
                // string sonuc = SpMusteri.MusteriGuncelle(conn, trans, musteriNo, ad, soyad, eposta, telefon);

                // 3. Log ekle
                // SpLog.LogEkle(conn, trans, "MUSTERI_GUNCELLE", musteriNo.ToString(), opAd);

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return "Müşteri güncellenirken hata: " + ex.Message;
            }
        }

        /// <summary>
        /// Müşteri siler (soft delete) ve log kaydı oluşturur
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="musteriNo">Müşteri No (long tip)</param>
        /// <param name="opAd">Operatör adı</param>
        /// <returns>Başarılı ise null, hata ise mesaj</returns>
        public static string MusteriSil(OracleConnection conn, OracleTransaction trans,
                                       long musteriNo, string opAd)
        {
            try
            {
                // 1. Müşterinin aktif hesabı var mı kontrol et
                // DataTable dtHesaplar = SpHesap.HesaplariGetir(conn, musteriNo);
                // if (dtHesaplar != null && dtHesaplar.Rows.Count > 0)
                // {
                //     return "Müşterinin aktif hesapları var. Önce hesapları kapatınız.";
                // }

                // 2. Müşteri sil (soft delete - aktif=0)
                // SpMusteri.MusteriSil(conn, trans, musteriNo);

                // 3. Log ekle
                // SpLog.LogEkle(conn, trans, "MUSTERI_SIL", musteriNo.ToString(), opAd);

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return "Müşteri silinirken hata: " + ex.Message;
            }
        }

        /// <summary>
        /// Müşteri bakiyesini hesaplar
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// Birden fazla hesabın bakiyesini toplar
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="musteriNo">Müşteri No (long tip)</param>
        /// <returns>Toplam bakiye (decimal)</returns>
        public static decimal ToplamBakiyeHesapla(OracleConnection conn, long musteriNo)
        {
            decimal toplamBakiye = 0;

            try
            {
                // 1. Müşterinin tüm hesaplarını getir
                // DataTable dtHesaplar = SpHesap.HesaplariGetir(conn, musteriNo);

                // 2. For döngüsü ile bakiyeleri topla (Standart: counter i, j, k)
                // for (int i = 0; i < dtHesaplar.Rows.Count; i++)
                // {
                //     DataRow drow = dtHesaplar.Rows[i]; // DataRow standart: drow
                //     decimal bakiye = Convert.ToDecimal(drow["bakiye"]);
                //     toplamBakiye += bakiye;
                // }

                return toplamBakiye;
            }
            catch (Exception ex)
            {
                throw new Exception("Toplam bakiye hesaplanırken hata: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// TC Kimlik No doğrulama algoritması
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <returns>Geçerli ise true, değilse false</returns>
        public static bool TcKimlikNoDogrula(string tcKimlikNo)
        {
            // TC Kimlik No algoritması
            if (string.IsNullOrWhiteSpace(tcKimlikNo) || tcKimlikNo.Length != 11)
            {
                return false;
            }

            // İlk hane 0 olamaz
            if (tcKimlikNo[0] == '0')
            {
                return false;
            }

            // Tüm karakterler rakam olmalı
            for (int i = 0; i < tcKimlikNo.Length; i++) // Standart: counter i
            {
                if (!char.IsDigit(tcKimlikNo[i]))
                {
                    return false;
                }
            }

            // Algoritma doğrulaması
            int[] rakamlar = new int[11];
            for (int i = 0; i < 11; i++)
            {
                rakamlar[i] = int.Parse(tcKimlikNo[i].ToString());
            }

            // 10. hane kontrolü
            int toplam1 = (rakamlar[0] + rakamlar[2] + rakamlar[4] + rakamlar[6] + rakamlar[8]) * 7;
            int toplam2 = rakamlar[1] + rakamlar[3] + rakamlar[5] + rakamlar[7];
            int onuncuHane = (toplam1 - toplam2) % 10;

            if (onuncuHane != rakamlar[9])
            {
                return false;
            }

            // 11. hane kontrolü
            int toplam3 = 0;
            for (int i = 0; i < 10; i++)
            {
                toplam3 += rakamlar[i];
            }

            int onbirinciHane = toplam3 % 10;

            return onbirinciHane == rakamlar[10];
        }
    }
}


