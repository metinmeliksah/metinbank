using System;
using System.Data;
using MetinBank.Business;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Service
{
    public class SHesap
    {
        private readonly BHesap _bHesap;
        private readonly BLog _bLog;

        public SHesap()
        {
            _bHesap = new BHesap();
            _bLog = new BLog();
        }

        public string HesapAc(HesapModel hesap, out int hesapID)
        {
            hesapID = 0;

            try
            {
                string hata = _bHesap.HesapAc(hesap, out hesapID);

                _bLog.IslemLoguKaydet(
                    hesap.OlusturanKullaniciID,
                    "HesapAc",
                    "Hesap",
                    hesapID,
                    null,
                    $"MusteriID: {hesap.MusteriID}, Tip: {hesap.HesapTipi}",
                    $"Yeni hesap açıldı: {hesap.HesapTipi} {hesap.HesapCinsi}",
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

        public string HesapGetir(int hesapID, out HesapModel hesap)
        {
            return _bHesap.HesapGetir(hesapID, out hesap);
        }

        public string HesapGetirIBAN(string iban, out HesapModel hesap)
        {
            return _bHesap.HesapGetirIBAN(iban, out hesap);
        }

        public string MusterininHesaplari(int musteriID, out DataTable hesaplar)
        {
            return _bHesap.MusterininHesaplari(musteriID, out hesaplar);
        }

        public string HesapKapat(int hesapID, int kullaniciID)
        {
            try
            {
                string hata = _bHesap.HesapKapat(hesapID, kullaniciID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "HesapKapat",
                    "Hesap",
                    hesapID,
                    null,
                    "Durum: Kapalı",
                    $"Hesap kapatıldı: ID {hesapID}",
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
    }
}

