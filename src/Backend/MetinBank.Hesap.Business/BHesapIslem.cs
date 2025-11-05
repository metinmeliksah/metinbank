/*
 * MetinBank - Hesap Business Layer
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Hesap iş mantığı sınıfı
 * Standart: Business sınıfları B prefix'i ile başlar
 */

using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text;

namespace MetinBank.Hesap.Business
{
    /// <summary>
    /// Hesap Business Logic
    /// Birden fazla SP kullanarak anlamlı işlemler gerçekleştirir
    /// Prefix: B (BHesapIslem)
    /// </summary>
    public static class BHesapIslem
    {
        /// <summary>
        /// Yeni hesap açar ve ilk işlem kaydı oluşturur
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="musteriNo">Müşteri No (long tip - standart)</param>
        /// <param name="hesapTip">Hesap tipi (1:Vadesiz, 2:Vadeli, 3:Döviz, 4:KMH)</param>
        /// <param name="dovizKod">Döviz kodu</param>
        /// <param name="ilkYatirim">İlk yatırım tutarı</param>
        /// <param name="opAd">Operatör adı</param>
        /// <returns>Hesap No (string)</returns>
        public static string HesapAc(OracleConnection conn, OracleTransaction trans,
                                     long musteriNo, int hesapTip, int dovizKod,
                                     decimal ilkYatirim, string opAd)
        {
            string hesapNo = string.Empty;

            try
            {
                // 1. Validasyon kontrolleri
                if (musteriNo <= 0)
                {
                    throw new Exception("Müşteri No geçersiz");
                }

                if (hesapTip < 1 || hesapTip > 5)
                {
                    throw new Exception("Hesap tipi geçersiz");
                }

                if (ilkYatirim < 0)
                {
                    throw new Exception("İlk yatırım tutarı negatif olamaz");
                }

                // Vadesiz hesap için minimum tutar kontrolü
                if (hesapTip == 1 && ilkYatirim < 100)
                {
                    throw new Exception("Vadesiz hesap için minimum 100 TL gereklidir");
                }

                // 2. IBAN oluştur
                hesapNo = IbanOlustur(musteriNo);

                // 3. Hesap aç (SP çağrısı)
                // SpHesap.HesapAc(conn, trans, musteriNo, hesapNo, hesapTip, dovizKod, ilkYatirim);

                // 4. İlk yatırım varsa işlem kaydı oluştur
                if (ilkYatirim > 0)
                {
                    // SpIslem.IslemEkle(conn, trans, hesapNo, "YATIRIM", ilkYatirim, opAd);
                }

                // 5. Log ekle
                // SpLog.LogEkle(conn, trans, "HESAP_AC", hesapNo, opAd);

                // 6. StringBuilder ile mesaj oluştur (Standart)
                StringBuilder mesaj = new StringBuilder();
                mesaj.Append("Hesap başarıyla açıldı. ");
                mesaj.Append("Hesap No: ");
                mesaj.Append(hesapNo);
                mesaj.Append(", İlk Yatırım: ");
                mesaj.Append(ilkYatirim.ToString("N2"));
                mesaj.Append(" TL");

                Console.WriteLine(mesaj.ToString());

                return hesapNo;
            }
            catch (Exception ex)
            {
                throw new Exception("Hesap açılırken hata: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Para yatırma işlemi
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="hesapNo">Hesap No</param>
        /// <param name="tutar">Yatırılacak tutar</param>
        /// <param name="opAd">Operatör adı</param>
        /// <returns>Yeni bakiye</returns>
        public static decimal ParaYatir(OracleConnection conn, OracleTransaction trans,
                                       string hesapNo, decimal tutar, string opAd)
        {
            decimal yeniBakiye = 0;

            try
            {
                // 1. Validasyon
                if (string.IsNullOrWhiteSpace(hesapNo))
                {
                    throw new Exception("Hesap No boş olamaz");
                }

                if (tutar <= 0)
                {
                    throw new Exception("Yatırılacak tutar pozitif olmalıdır");
                }

                // 2. Mevcut bakiye al (SP çağrısı - database ile aynı isim)
                // decimal mevcutBakiye = SpHesap.get_bakiye(conn, hesapNo);

                // 3. Para yatır
                // SpHesap.ParaYatir(conn, trans, hesapNo, tutar);

                // 4. İşlem kaydı oluştur
                // SpIslem.IslemEkle(conn, trans, hesapNo, "YATIRIM", tutar, opAd);

                // 5. Yeni bakiye hesapla
                // yeniBakiye = mevcutBakiye + tutar;

                // 6. Log ekle
                // SpLog.LogEkle(conn, trans, "PARA_YATIR", hesapNo + " - " + tutar.ToString(), opAd);

                return yeniBakiye;
            }
            catch (Exception ex)
            {
                throw new Exception("Para yatırılırken hata: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Para çekme işlemi
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="hesapNo">Hesap No</param>
        /// <param name="tutar">Çekilecek tutar</param>
        /// <param name="opAd">Operatör adı</param>
        /// <returns>Yeni bakiye</returns>
        public static decimal ParaCek(OracleConnection conn, OracleTransaction trans,
                                     string hesapNo, decimal tutar, string opAd)
        {
            decimal yeniBakiye = 0;

            try
            {
                // 1. Validasyon
                if (string.IsNullOrWhiteSpace(hesapNo))
                {
                    throw new Exception("Hesap No boş olamaz");
                }

                if (tutar <= 0)
                {
                    throw new Exception("Çekilecek tutar pozitif olmalıdır");
                }

                // 2. Mevcut bakiye al
                // decimal mevcutBakiye = SpHesap.get_bakiye(conn, hesapNo);

                // 3. Bakiye kontrolü
                // if (mevcutBakiye < tutar)
                // {
                //     throw new Exception("Yetersiz bakiye. Mevcut: " + mevcutBakiye.ToString("N2"));
                // }

                // 4. Para çek
                // SpHesap.ParaCek(conn, trans, hesapNo, tutar);

                // 5. İşlem kaydı oluştur
                // SpIslem.IslemEkle(conn, trans, hesapNo, "CEKIM", tutar, opAd);

                // 6. Yeni bakiye hesapla
                // yeniBakiye = mevcutBakiye - tutar;

                // 7. Log ekle
                // SpLog.LogEkle(conn, trans, "PARA_CEK", hesapNo + " - " + tutar.ToString(), opAd);

                return yeniBakiye;
            }
            catch (Exception ex)
            {
                throw new Exception("Para çekilirken hata: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Hesaplar arası para transferi (Virman)
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="gonderenHesapNo">Gönderen hesap no</param>
        /// <param name="aliciHesapNo">Alıcı hesap no</param>
        /// <param name="tutar">Transfer tutarı</param>
        /// <param name="aciklama">Transfer açıklaması</param>
        /// <param name="opAd">Operatör adı</param>
        /// <returns>İşlem No</returns>
        public static long Virman(OracleConnection conn, OracleTransaction trans,
                                 string gonderenHesapNo, string aliciHesapNo,
                                 decimal tutar, string aciklama, string opAd)
        {
            long islemNo = 0;

            try
            {
                // 1. Validasyon
                if (string.IsNullOrWhiteSpace(gonderenHesapNo))
                {
                    throw new Exception("Gönderen hesap no boş olamaz");
                }

                if (string.IsNullOrWhiteSpace(aliciHesapNo))
                {
                    throw new Exception("Alıcı hesap no boş olamaz");
                }

                if (gonderenHesapNo == aliciHesapNo)
                {
                    throw new Exception("Aynı hesaba transfer yapılamaz");
                }

                if (tutar <= 0)
                {
                    throw new Exception("Transfer tutarı pozitif olmalıdır");
                }

                // 2. Gönderen hesap bakiye kontrolü
                // decimal gonderenBakiye = SpHesap.get_bakiye(conn, gonderenHesapNo);
                // if (gonderenBakiye < tutar)
                // {
                //     throw new Exception("Yetersiz bakiye");
                // }

                // 3. Alıcı hesap varlık kontrolü
                // bool aliciHesapVarMi = SpHesap.HesapVarMi(conn, aliciHesapNo);
                // if (!aliciHesapVarMi)
                // {
                //     throw new Exception("Alıcı hesap bulunamadı");
                // }

                // 4. Gönderen hesaptan para çek
                // SpHesap.ParaCek(conn, trans, gonderenHesapNo, tutar);

                // 5. Alıcı hesaba para yatır
                // SpHesap.ParaYatir(conn, trans, aliciHesapNo, tutar);

                // 6. Transfer işlem kaydı oluştur
                // islemNo = SpTransfer.VirmanKaydet(conn, trans, gonderenHesapNo, aliciHesapNo, tutar, aciklama);

                // 7. Log ekle
                // StringBuilder logMesaj = new StringBuilder();
                // logMesaj.Append("VIRMAN: ");
                // logMesaj.Append(gonderenHesapNo);
                // logMesaj.Append(" -> ");
                // logMesaj.Append(aliciHesapNo);
                // logMesaj.Append(" Tutar: ");
                // logMesaj.Append(tutar.ToString("N2"));
                // SpLog.LogEkle(conn, trans, "VIRMAN", logMesaj.ToString(), opAd);

                return islemNo;
            }
            catch (Exception ex)
            {
                throw new Exception("Virman işlemi sırasında hata: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// IBAN oluşturur
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="musteriNo">Müşteri No (long tip)</param>
        /// <returns>IBAN formatında hesap numarası</returns>
        private static string IbanOlustur(long musteriNo)
        {
            // TR + 2 hane kontrol + 5 hane banka kodu + 1 hane rezerv + 16 hane hesap no
            // Örnek: TR33 0006 2 0000000012345678
            
            string bankaKod = "00062"; // MetinBank kodu (örnek)
            string rezerv = "0";
            
            // Müşteri no'dan hesap no oluştur (16 hane)
            string hesapNo = musteriNo.ToString().PadLeft(16, '0');
            
            // IBAN kontrol haneleri hesapla (basitleştirilmiş)
            string kontrol = "33"; // Örnek kontrol hanesi
            
            // StringBuilder kullanarak IBAN oluştur (Standart)
            StringBuilder iban = new StringBuilder();
            iban.Append("TR");
            iban.Append(kontrol);
            iban.Append(bankaKod);
            iban.Append(rezerv);
            iban.Append(hesapNo);
            
            return iban.ToString();
        }

        /// <summary>
        /// Faiz hesaplar (Vadeli hesaplar için)
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="anapar">Anapara tutarı</param>
        /// <param name="faizOran">Yıllık faiz oranı (%)</param>
        /// <param name="gun">Vade gün sayısı</param>
        /// <returns>Faiz tutarı</returns>
        public static decimal FaizHesapla(decimal anapar, decimal faizOran, int gun)
        {
            // Basit faiz formülü: Faiz = Anapara * Oran * Gün / 36500
            decimal faiz = (anapar * faizOran * gun) / 36500;
            return Math.Round(faiz, 2);
        }
    }
}


