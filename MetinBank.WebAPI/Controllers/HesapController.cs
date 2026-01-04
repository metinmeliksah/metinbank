using Microsoft.AspNetCore.Mvc;
using MetinBank.Service;
using MetinBank.WebAPI.DTOs;
using MetinBank.Models;
using System.Data;

namespace MetinBank.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HesapController : ControllerBase
    {
        private readonly SHesap _sHesap;

        public HesapController()
        {
            _sHesap = new SHesap();
        }

        /// <summary>
        /// Müşterinin tüm hesaplarını getirir
        /// </summary>
        [HttpGet("musteri/{musteriID}")]
        public IActionResult GetMusteriHesaplari(int musteriID)
        {
            try
            {
                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(musteriID, out hesaplar);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                // DataTable'ı List'e dönüştür
                var hesapListesi = new List<object>();
                foreach (DataRow row in hesaplar.Rows)
                {
                    hesapListesi.Add(new
                    {
                        HesapID = row["HesapID"] != DBNull.Value ? Convert.ToInt32(row["HesapID"]) : 0,
                        HesapNo = row["HesapNo"] != DBNull.Value ? Convert.ToInt64(row["HesapNo"]) : 0,
                        IBAN = row["IBAN"] != DBNull.Value ? row["IBAN"].ToString() : "",
                        HesapTipi = row["HesapTipi"] != DBNull.Value ? row["HesapTipi"].ToString() : "",
                        HesapCinsi = row["HesapCinsi"] != DBNull.Value ? row["HesapCinsi"].ToString() : "",
                        Bakiye = row["Bakiye"] != DBNull.Value ? Convert.ToDecimal(row["Bakiye"]) : 0m,
                        KullanilabilirBakiye = row["KullanilabilirBakiye"] != DBNull.Value ? Convert.ToDecimal(row["KullanilabilirBakiye"]) : 0m,
                        Durum = row["Durum"] != DBNull.Value ? row["Durum"].ToString() : "Aktif",
                        AcilisTarihi = row["AcilisTarihi"] != DBNull.Value ? Convert.ToDateTime(row["AcilisTarihi"]) : DateTime.Now
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Hesaplar getirildi.",
                    Data = hesapListesi
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Tek hesap detayını getirir
        /// </summary>
        [HttpGet("{hesapID}")]
        public IActionResult GetHesap(int hesapID)
        {
            try
            {
                HesapModel hesap;
                string hata = _sHesap.HesapGetir(hesapID, out hesap);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Hesap detayı getirildi.",
                    Data = hesap
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Hesap bakiyesini getirir
        /// </summary>
        [HttpGet("{hesapID}/bakiye")]
        public IActionResult GetBakiye(int hesapID)
        {
            try
            {
                HesapModel hesap;
                string hata = _sHesap.HesapGetir(hesapID, out hesap);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Bakiye getirildi.",
                    Data = new
                    {
                        hesap.Bakiye,
                        hesap.BlokeBakiye,
                        hesap.KullanilabilirBakiye,
                        hesap.HesapTipi
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// IBAN ile hesap sorgulama
        /// </summary>
        [HttpGet("iban/{iban}")]
        public IActionResult GetHesapByIBAN(string iban)
        {
            try
            {
                HesapModel hesap;
                string hata = _sHesap.HesapGetirIBAN(iban, out hesap);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Hesap bulundu.",
                    Data = new
                    {
                        hesap.HesapID,
                        hesap.IBAN,
                        hesap.MusteriAdi,
                        hesap.HesapTipi
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// IBAN ile müşteri adı sorgula (Havale için)
        /// </summary>
        [HttpPost("iban-sorgula")]
        public IActionResult IbanSorgula([FromBody] IbanSorguRequest request)
        {
            try
            {
                HesapModel hesap;
                string hata = _sHesap.HesapGetirIBAN(request.IBAN, out hesap);

                if (hata != null || hesap == null)
                {
                    return Ok(new IbanSorguResponse
                    {
                        Success = false,
                        Message = "IBAN bulunamadı."
                    });
                }

                // Müşteri bilgisini getir
                var sMusteri = new MetinBank.Service.SMusteri();
                MusteriModel musteri;
                hata = sMusteri.MusteriGetir(hesap.MusteriID, out musteri);

                if (hata != null || musteri == null)
                {
                    return Ok(new IbanSorguResponse
                    {
                        Success = false,
                        Message = "Müşteri bilgisi bulunamadı."
                    });
                }

                return Ok(new IbanSorguResponse
                {
                    Success = true,
                    Message = "IBAN bulundu.",
                    MusteriAdi = $"{musteri.Ad} {musteri.Soyad}",
                    BankaAdi = "MetinBank"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IbanSorguResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Yeni hesap aç
        /// </summary>
        [HttpPost("hesap-ac")]
        public IActionResult HesapAc([FromBody] HesapAcRequest request)
        {
            try
            {
                // Hesap modeli oluştur
                var hesap = new HesapModel
                {
                    MusteriID = request.MusteriID,
                    HesapTipi = request.HesapTipi, // TL, USD, EUR
                    HesapCinsi = "VADELİ",
                    SubeID = request.SubeID,
                    Bakiye = 0,
                    BlokeBakiye = 0,
                    KullanilabilirBakiye = 0
                };

                int hesapID;
                string hata = _sHesap.HesapAc(hesap, out hesapID);

                if (hata != null)
                {
                    return Ok(new HesapAcResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                // Yeni oluşturulan hesabı getir
                HesapModel yeniHesap;
                hata = _sHesap.HesapGetir(hesapID, out yeniHesap);

                if (hata != null)
                {
                    return Ok(new HesapAcResponse
                    {
                        Success = true,
                        Message = "Hesap açıldı ama detaylar alınamadı.",
                        HesapID = hesapID
                    });
                }

                return Ok(new HesapAcResponse
                {
                    Success = true,
                    Message = "Hesap başarıyla açıldı.",
                    HesapNo = yeniHesap.HesapNo.ToString(),
                    IBAN = yeniHesap.IBAN,
                    HesapID = hesapID
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new HesapAcResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }
    }
}
