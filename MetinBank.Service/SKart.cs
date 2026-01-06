using System;
using System.Data;
using MetinBank.Business;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Service
{
    /// <summary>
    /// Kart işlemleri servis sınıfı
    /// </summary>
    public class SKart
    {
        private readonly BKart _bKart;
        private readonly BLog _bLog;

        public SKart()
        {
            _bKart = new BKart();
            _bLog = new BLog();
        }

        /// <summary>
        /// Yeni kart oluşturur
        /// </summary>
        public string CreateCard(int hesapID, string kartMarkasi, string kartSahibiAdi, int kullaniciID, out int kartID, out long createdCardNo)
        {
            kartID = 0;
            createdCardNo = 0;
            try
            {
                string hata = _bKart.CreateCard(hesapID, kartMarkasi, kartSahibiAdi, kullaniciID, out kartID, out createdCardNo);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "KartOlustur",
                    "BankaKarti",
                    kartID,
                    null,
                    $"HesapID: {hesapID}, Marka: {kartMarkasi}",
                    $"Yeni kart oluşturuldu: {kartMarkasi}",
                    CommonFunctions.GetLocalIPAddress(),
                    hata == null,
                    hata
                );

                return hata;
            }
            catch (Exception ex)
            {
                return $"Servis hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşterinin tüm kartlarını getirir
        /// </summary>
        public string GetMusteriKartlari(int musteriID, out DataTable kartlar)
        {
            return _bKart.GetMusteriKartlari(musteriID, out kartlar);
        }

        /// <summary>
        /// Hesaba ait kartları getirir
        /// </summary>
        public string GetHesapKartlari(int hesapID, out DataTable kartlar)
        {
            return _bKart.GetHesapKartlari(hesapID, out kartlar);
        }

        /// <summary>
        /// Kartı iptal eder
        /// </summary>
        public string CancelCard(int kartID, int kullaniciID)
        {
            try
            {
                string hata = _bKart.CancelCard(kartID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "KartIptal",
                    "BankaKarti",
                    kartID,
                    "Aktif",
                    "Iptal",
                    $"Kart iptal edildi: KartID {kartID}",
                    CommonFunctions.GetLocalIPAddress(),
                    hata == null,
                    hata
                );

                return hata;
            }
            catch (Exception ex)
            {
                return $"Servis hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Kartı bloke eder
        /// </summary>
        public string BlockCard(int kartID, int kullaniciID)
        {
            try
            {
                string hata = _bKart.BlockCard(kartID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "KartBloke",
                    "BankaKarti",
                    kartID,
                    "Aktif",
                    "Blokeli",
                    $"Kart bloke edildi: KartID {kartID}",
                    CommonFunctions.GetLocalIPAddress(),
                    hata == null,
                    hata
                );

                return hata;
            }
            catch (Exception ex)
            {
                return $"Servis hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Blokeli kartı aktifleştirir
        /// </summary>
        public string ActivateCard(int kartID, int kullaniciID)
        {
            try
            {
                string hata = _bKart.ActivateCard(kartID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "KartAktivasyon",
                    "BankaKarti",
                    kartID,
                    "Blokeli",
                    "Aktif",
                    $"Kart aktifleştirildi: KartID {kartID}",
                    CommonFunctions.GetLocalIPAddress(),
                    hata == null,
                    hata
                );

                return hata;
            }
            catch (Exception ex)
            {
                return $"Servis hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Tek bir kartın detaylarını getirir
        /// </summary>
        public string GetKartDetay(int kartID, out BankaKartiModel kart)
        {
            return _bKart.GetKartDetay(kartID, out kart);
        }

        /// <summary>
        /// Kart limitlerini günceller
        /// </summary>
        public string UpdateCardLimits(int kartID, decimal gunlukHarcama, decimal aylikHarcama, decimal gunlukCekim)
        {
            return _bKart.UpdateCardLimits(kartID, gunlukHarcama, aylikHarcama, gunlukCekim);
        }

        /// <summary>
        /// Müşterinin hesaplarını getirir (kart başvurusu için)
        /// </summary>
        public string GetMusteriHesaplari(int musteriID, out DataTable hesaplar)
        {
            return _bKart.GetMusteriHesaplari(musteriID, out hesaplar);
        }
    }
}

