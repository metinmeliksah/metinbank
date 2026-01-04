using System;
using System.Data;
using MetinBank.Business;
using MetinBank.Util;

namespace MetinBank.Service
{
    public class SMusteriSifre
    {
        private readonly BMusteriSifre _bMusteriSifre;

        public SMusteriSifre()
        {
            _bMusteriSifre = new BMusteriSifre();
        }

        /// <summary>
        /// Müşteri için yeni şifre oluşturur
        /// </summary>
        public string SifreOlustur(int musteriID, string yeniSifre, string ipAdresi, out int musteriSifreID)
        {
            musteriSifreID = 0;

            try
            {
                string hata = _bMusteriSifre.SifreOlustur(musteriID, yeniSifre, ipAdresi, out musteriSifreID);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşteri şifre doğrulama
        /// </summary>
        public string SifreDogrula(int musteriID, string girilenSifre, string ipAdresi, out bool basarili)
        {
            basarili = false;

            try
            {
                string hata = _bMusteriSifre.SifreDogrula(musteriID, girilenSifre, ipAdresi, out basarili);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Şifre değiştirme
        /// </summary>
        public string SifreDegistir(int musteriID, string eskiSifre, string yeniSifre, string ipAdresi)
        {
            try
            {
                string hata = _bMusteriSifre.SifreDegistir(musteriID, eskiSifre, yeniSifre, ipAdresi);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşterinin şifresi var mı kontrol et
        /// </summary>
        public string SifreVarMi(int musteriID, out bool sifreVar)
        {
            sifreVar = false;

            try
            {
                string hata = _bMusteriSifre.SifreVarMi(musteriID, out sifreVar);
                return hata;
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }
    }
}
