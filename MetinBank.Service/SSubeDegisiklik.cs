using System;
using System.Data;
using MetinBank.Business;
using MetinBank.Util;

namespace MetinBank.Service
{
    /// <summary>
    /// Şube değişikliği servis sınıfı
    /// </summary>
    public class SSubeDegisiklik
    {
        private readonly BSubeDegisiklik _bSubeDegisiklik;
        private readonly BLog _bLog;

        public SSubeDegisiklik()
        {
            _bSubeDegisiklik = new BSubeDegisiklik();
            _bLog = new BLog();
        }

        /// <summary>
        /// Yeni şube değişikliği talebi oluşturur
        /// </summary>
        public string TalepOlustur(int kullaniciID, int mevcutSubeID, int yeniSubeID, string talepNedeni, out int talepID)
        {
            talepID = 0;

            try
            {
                // Validasyon
                if (kullaniciID <= 0)
                    return "Geçersiz kullanıcı.";

                if (mevcutSubeID <= 0 || yeniSubeID <= 0)
                    return "Geçersiz şube bilgisi.";

                if (string.IsNullOrWhiteSpace(talepNedeni))
                    return "Talep nedeni boş olamaz.";

                // Business logic çağır
                string hata = _bSubeDegisiklik.TalepOlustur(kullaniciID, mevcutSubeID, yeniSubeID, talepNedeni, out talepID);

                return hata;
            }
            catch (Exception ex)
            {
                _bLog.GuvenlikLoguKaydet(
                    "SubeDegisiklikTalepHata",
                    kullaniciID,
                    CommonFunctions.GetLocalIPAddress(),
                    $"Şube değişikliği talebi oluşturma hatası: {ex.Message}",
                    "Orta"
                );
                return $"Talep oluşturma hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Şube değişikliği talebini onaylar
        /// </summary>
        public string TalepOnayla(int talepID, int onaylayanID)
        {
            try
            {
                // Validasyon
                if (talepID <= 0)
                    return "Geçersiz talep.";

                if (onaylayanID <= 0)
                    return "Geçersiz onaylayan kullanıcı.";

                // Business logic çağır
                string hata = _bSubeDegisiklik.TalepOnayla(talepID, onaylayanID);

                if (hata == null)
                {
                    _bLog.GuvenlikLoguKaydet(
                        "SubeDegisiklikOnayi",
                        onaylayanID,
                        CommonFunctions.GetLocalIPAddress(),
                        $"Şube değişikliği talebi onaylandı. Talep ID: {talepID}",
                        "Dusuk"
                    );
                }

                return hata;
            }
            catch (Exception ex)
            {
                _bLog.GuvenlikLoguKaydet(
                    "SubeDegisiklikOnayHata",
                    onaylayanID,
                    CommonFunctions.GetLocalIPAddress(),
                    $"Şube değişikliği onaylama hatası: {ex.Message}",
                    "Yuksek"
                );
                return $"Talep onaylama hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Şube değişikliği talebini reddeder
        /// </summary>
        public string TalepReddet(int talepID, int onaylayanID, string redNedeni)
        {
            try
            {
                // Validasyon
                if (talepID <= 0)
                    return "Geçersiz talep.";

                if (onaylayanID <= 0)
                    return "Geçersiz onaylayan kullanıcı.";

                if (string.IsNullOrWhiteSpace(redNedeni))
                    return "Red nedeni boş olamaz.";

                if (redNedeni.Length < 10)
                    return "Red nedeni en az 10 karakter olmalıdır.";

                // Business logic çağır
                string hata = _bSubeDegisiklik.TalepReddet(talepID, onaylayanID, redNedeni);

                if (hata == null)
                {
                    _bLog.GuvenlikLoguKaydet(
                        "SubeDegisiklikRed",
                        onaylayanID,
                        CommonFunctions.GetLocalIPAddress(),
                        $"Şube değişikliği talebi reddedildi. Talep ID: {talepID}",
                        "Dusuk"
                    );
                }

                return hata;
            }
            catch (Exception ex)
            {
                _bLog.GuvenlikLoguKaydet(
                    "SubeDegisiklikRedHata",
                    onaylayanID,
                    CommonFunctions.GetLocalIPAddress(),
                    $"Şube değişikliği reddetme hatası: {ex.Message}",
                    "Orta"
                );
                return $"Talep reddetme hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Bekleyen şube değişikliği taleplerini getirir
        /// </summary>
        public string BekleyenTalepleriGetir(out DataTable talepler)
        {
            talepler = null;

            try
            {
                return _bSubeDegisiklik.BekleyenTalepleriGetir(out talepler);
            }
            catch (Exception ex)
            {
                return $"Bekleyen talepler getirme hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Kullanıcının şube değişikliği talep durumunu getirir
        /// </summary>
        public string KullaniciTalepDurumuGetir(int kullaniciID, out DataTable talepler)
        {
            talepler = null;

            try
            {
                if (kullaniciID <= 0)
                {
                    return "Geçersiz kullanıcı.";
                }

                return _bSubeDegisiklik.KullaniciTalepDurumuGetir(kullaniciID, out talepler);
            }
            catch (Exception ex)
            {
                return $"Kullanıcı talep durumu getirme hatası: {ex.Message}";
            }
        }

        /// <summary>
        /// Tüm şube değişikliği taleplerini getirir
        /// </summary>
        public string TumTalepleriGetir(out DataTable talepler)
        {
            talepler = null;

            try
            {
                return _bSubeDegisiklik.TumTalepleriGetir(out talepler);
            }
            catch (Exception ex)
            {
                return $"Tüm talepler getirme hatası: {ex.Message}";
            }
        }
    }
}
