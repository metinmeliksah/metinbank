using System;
using System.Data;
using MetinBank.Business;
using MetinBank.Util;

namespace MetinBank.Service
{
    public class SOnay
    {
        private readonly BOnay _bOnay;
        private readonly BLog _bLog;

        public SOnay()
        {
            _bOnay = new BOnay();
            _bLog = new BLog();
        }

        public string OnayTalebiOlustur(long islemID, string islemTipi, int talepEdenID, string beklenenRol, out int onayLogID)
        {
            onayLogID = 0;

            try
            {
                string hata = _bOnay.OnayTalebiOlustur(islemID, islemTipi, talepEdenID, beklenenRol, out onayLogID);

                _bLog.IslemLoguKaydet(
                    talepEdenID,
                    "OnayTalebiOlustur",
                    "OnayLog",
                    onayLogID,
                    null,
                    $"IslemID: {islemID}",
                    $"Onay talebi oluşturuldu: {islemTipi}",
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

        public string IslemOnayla(int onayLogID, int onaylayanID)
        {
            try
            {
                string hata = _bOnay.IslemOnayla(onayLogID, onaylayanID);

                _bLog.IslemLoguKaydet(
                    onaylayanID,
                    "IslemOnayla",
                    "OnayLog",
                    onayLogID,
                    "Beklemede",
                    "Onaylandı",
                    $"İşlem onaylandı: OnayLogID {onayLogID}",
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

        public string IslemReddet(int onayLogID, int onaylayanID, string redNedeni)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(redNedeni))
                    return "Red nedeni girilmelidir.";

                string hata = _bOnay.IslemReddet(onayLogID, onaylayanID, redNedeni);

                _bLog.IslemLoguKaydet(
                    onaylayanID,
                    "IslemReddet",
                    "OnayLog",
                    onayLogID,
                    "Beklemede",
                    "Reddedildi",
                    $"İşlem reddedildi: {redNedeni}",
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

        public string OnayBekleyenler(string rolAdi, out DataTable onaylar)
        {
            return _bOnay.OnayBekleyenler(rolAdi, out onaylar);
        }
    }
}

