using System;
using System.Data;
using MetinBank.Business;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Service
{
    public class SMusteri
    {
        private readonly BMusteri _bMusteri;
        private readonly BLog _bLog;

        public SMusteri()
        {
            _bMusteri = new BMusteri();
            _bLog = new BLog();
        }

        public string MusteriEkle(MusteriModel musteri, out int musteriID)
        {
            musteriID = 0;

            try
            {
                string hata = _bMusteri.MusteriEkle(musteri, out musteriID);

                _bLog.IslemLoguKaydet(
                    null,
                    "MusteriEkle",
                    "Musteri",
                    musteriID,
                    null,
                    $"TCKN: {musteri.TCKN}",
                    $"Yeni müşteri eklendi: {musteri.Ad} {musteri.Soyad}",
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

        public string MusteriGuncelle(MusteriModel musteri)
        {
            try
            {
                string hata = _bMusteri.MusteriGuncelle(musteri);

                _bLog.IslemLoguKaydet(
                    null,
                    "MusteriGuncelle",
                    "Musteri",
                    musteri.MusteriID,
                    null,
                    null,
                    $"Müşteri güncellendi: ID {musteri.MusteriID}",
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

        public string MusterileriGetir(out DataTable musteriListesi)
        {
            return _bMusteri.MusterileriGetir(out musteriListesi);
        }

        public string MusteriGetir(int musteriID, out MusteriModel musteri)
        {
            return _bMusteri.MusteriGetir(musteriID, out musteri);
        }

        public string MusteriGetirTCKN(long tckn, out MusteriModel musteri)
        {
            return _bMusteri.MusteriGetirTCKN(tckn, out musteri);
        }

        public string MusteriAra(string aramaKelimesi, out DataTable sonuclar)
        {
            return _bMusteri.MusteriAra(aramaKelimesi, out sonuclar);
        }

        /// <summary>
        /// Şube bazlı müşteri arama
        /// </summary>
        public string MusteriAra(string aramaKelimesi, int? subeID, bool isGenelMerkez, out DataTable sonuclar)
        {
            return _bMusteri.MusteriAra(aramaKelimesi, subeID, isGenelMerkez, out sonuclar);
        }

        /// <summary>
        /// Şubenin müşteri listesi (Müdür için)
        /// </summary>
        public string SubeninMusterileri(int subeID, out DataTable sonuclar)
        {
            return _bMusteri.SubeninMusterileri(subeID, out sonuclar);
        }

        /// <summary>
        /// Tüm müşterileri getirir (Genel Merkez için)
        /// </summary>
        public string TumMusteriler(out DataTable sonuclar)
        {
            return _bMusteri.MusterileriGetir(out sonuclar);
        }
    }
}

