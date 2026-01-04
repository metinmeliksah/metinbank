using System;
using System.Data;
using MetinBank.Business;
using MetinBank.Util;

namespace MetinBank.Service
{
    /// <summary>
    /// Döviz işlemleri servis sınıfı
    /// </summary>
    public class SDoviz
    {
        private readonly BDoviz _bDoviz;
        private readonly BLog _bLog;

        public SDoviz()
        {
            _bDoviz = new BDoviz();
            _bLog = new BLog();
        }

        /// <summary>
        /// Güncel döviz kurlarını getirir
        /// </summary>
        public string GetDovizKurlari(out DataTable kurlar)
        {
            return _bDoviz.GetDovizKurlari(out kurlar);
        }

        /// <summary>
        /// Belirli bir döviz kurunu getirir
        /// </summary>
        public string GetDovizKuru(string paraBirimi, out decimal alisFiyati, out decimal satisFiyati)
        {
            return _bDoviz.GetDovizKuru(paraBirimi, out alisFiyati, out satisFiyati);
        }

        /// <summary>
        /// Döviz alım işlemi (TRY -> Döviz)
        /// </summary>
        public string DovizAl(int tryHesapID, int dovizHesapID, decimal dovizTutar, string dovizCinsi,
                              int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;
            try
            {
                string hata = _bDoviz.DovizAl(tryHesapID, dovizHesapID, dovizTutar, dovizCinsi, 
                                               kullaniciID, subeID, out islemID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "DovizAlim",
                    "Islem",
                    islemID,
                    null,
                    $"TRY Hesap: {tryHesapID}, Döviz Hesap: {dovizHesapID}, Tutar: {dovizTutar} {dovizCinsi}",
                    $"Döviz alım: {dovizTutar:N2} {dovizCinsi}",
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
        /// Döviz satım işlemi (Döviz -> TRY)
        /// </summary>
        public string DovizSat(int dovizHesapID, int tryHesapID, decimal dovizTutar, string dovizCinsi,
                               int kullaniciID, int subeID, out long islemID)
        {
            islemID = 0;
            try
            {
                string hata = _bDoviz.DovizSat(dovizHesapID, tryHesapID, dovizTutar, dovizCinsi,
                                                kullaniciID, subeID, out islemID);

                _bLog.IslemLoguKaydet(
                    kullaniciID,
                    "DovizSatim",
                    "Islem",
                    islemID,
                    null,
                    $"Döviz Hesap: {dovizHesapID}, TRY Hesap: {tryHesapID}, Tutar: {dovizTutar} {dovizCinsi}",
                    $"Döviz satım: {dovizTutar:N2} {dovizCinsi}",
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
