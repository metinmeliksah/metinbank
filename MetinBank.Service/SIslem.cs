using System;
using System.Data;
using MetinBank.Business;
using MetinBank.Util;

namespace MetinBank.Service
{
    public class SIslem
    {
        private readonly BIslem _bIslem;
        private readonly BLog _bLog;

        public SIslem()
        {
            _bIslem = new BIslem();
            _bLog = new BLog();
        }

        public string ParaYatir(int hesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = _bIslem.ParaYatir(hesapID, tutar, aciklama, kullaniciID, subeID, out islemID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "ParaYatir",
                    "Islem",
                    islemID,
                    null,
                    $"HesapID: {hesapID}, Tutar: {tutar}",
                    $"Para yatırma: {CommonFunctions.FormatCurrency(tutar)}",
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

        public string ParaCek(int hesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = _bIslem.ParaCek(hesapID, tutar, aciklama, kullaniciID, subeID, out islemID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "ParaCek",
                    "Islem",
                    islemID,
                    null,
                    $"HesapID: {hesapID}, Tutar: {tutar}",
                    $"Para çekme: {CommonFunctions.FormatCurrency(tutar)}",
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

        public string Havale(int kaynakHesapID, string hedefIBAN, decimal tutar, string aciklama, string aliciAdi, 
                            int kullaniciID, int subeID, decimal islemUcreti, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = _bIslem.Havale(kaynakHesapID, hedefIBAN, tutar, aciklama, aliciAdi, kullaniciID, subeID, islemUcreti, out islemID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "Havale",
                    "Islem",
                    islemID,
                    null,
                    $"Kaynak: {kaynakHesapID}, Hedef: {hedefIBAN}, Tutar: {tutar}",
                    $"Havale: {CommonFunctions.FormatCurrency(tutar)} - {aliciAdi}",
                    CommonFunctions.GetLocalIPAddress(),
                    hata == null,
                    hata
                );

                if (hata == null && tutar > 5000)
                {
                    _bLog.GuvenlikLoguKaydet(
                        "YuksekTutarliHavale",
                        kullaniciID,
                        CommonFunctions.GetLocalIPAddress(),
                        $"Yüksek tutarlı havale: {CommonFunctions.FormatCurrency(tutar)}",
                        tutar > 10000 ? "Yuksek" : "Orta"
                    );
                }

                return hata;
            }
            catch (Exception ex)
            {
                return $"Servis hatası: {ex.Message}";
            }
        }

        public string EFT(int kaynakHesapID, string hedefIBAN, decimal tutar, string aciklama, string aliciAdi, 
                         int kullaniciID, int subeID, decimal islemUcreti, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = _bIslem.EFT(kaynakHesapID, hedefIBAN, tutar, aciklama, aliciAdi, kullaniciID, subeID, islemUcreti, out islemID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "EFT",
                    "Islem",
                    islemID,
                    null,
                    $"Kaynak: {kaynakHesapID}, Hedef: {hedefIBAN}, Tutar: {tutar}",
                    $"EFT: {CommonFunctions.FormatCurrency(tutar)} - {aliciAdi}",
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

        public string Virman(int kaynakHesapID, int hedefHesapID, decimal tutar, string aciklama, int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;

            try
            {
                string hata = _bIslem.Virman(kaynakHesapID, hedefHesapID, tutar, aciklama, kullaniciID, subeID, out islemID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "Virman",
                    "Islem",
                    islemID,
                    null,
                    $"Kaynak: {kaynakHesapID}, Hedef: {hedefHesapID}, Tutar: {tutar}",
                    $"Virman: {CommonFunctions.FormatCurrency(tutar)}",
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

        public string MusterininIslemleri(int musteriID, out DataTable islemler)
        {
            return _bIslem.MusterininIslemleri(musteriID, out islemler);
        }

        public string HesabinIslemleri(int hesapID, out DataTable islemler)
        {
            return _bIslem.HesabinIslemleri(hesapID, out islemler);
        }

        public string IslemOnayla(long islemID, int kullaniciID, string rol)
        {
             try
            {
                string hata = _bIslem.IslemOnayla(islemID, kullaniciID, rol);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "IslemOnay",
                    "Islem",
                    islemID,
                    null,
                    $"Rol: {rol}",
                    $"İşlem onaylandı ({rol})",
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

        public string IslemReddet(long islemID, int kullaniciID, string aciklama)
        {
            try
            {
                string hata = _bIslem.IslemReddet(islemID, kullaniciID, aciklama);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "IslemRed",
                    "Islem",
                    islemID,
                    null,
                    $"Red Nedeni: {aciklama}",
                    "İşlem reddedildi",
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

        public string OnayBekleyenIslemleriGetir(string rol, out DataTable islemler)
        {
            return _bIslem.OnayBekleyenIslemleriGetir(rol, out islemler);
        }
    }
}

