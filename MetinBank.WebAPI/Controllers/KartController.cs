using Microsoft.AspNetCore.Mvc;
using MetinBank.Service;
using MetinBank.WebAPI.DTOs;
using MetinBank.Models;
using System.Data;

namespace MetinBank.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KartController : ControllerBase
    {
        private readonly SKart _sKart;

        public KartController()
        {
            _sKart = new SKart();
        }

        /// <summary>
        /// Müşterinin tüm kartlarını getirir
        /// </summary>
        [HttpGet("musteri/{musteriID}")]
        public IActionResult GetMusteriKartlari(int musteriID)
        {
            try
            {
                DataTable kartlar;
                string hata = _sKart.GetMusteriKartlari(musteriID, out kartlar);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                var kartListesi = new List<object>();
                foreach (DataRow row in kartlar.Rows)
                {
                    kartListesi.Add(new
                    {
                        kartID = Convert.ToInt32(row["KartID"]),
                        kartNo = row["KartNo"].ToString(),
                        kartTipi = row["KartTipi"].ToString(),
                        kartSahibiAdi = row["KartSahibiAdi"].ToString(),
                        sonKullanimTarihi = row["SonKullanmaTarihi"] != DBNull.Value ? Convert.ToDateTime(row["SonKullanmaTarihi"]) : DateTime.MinValue,
                        durum = row["Durum"].ToString(),
                        hesapID = row["HesapID"] != DBNull.Value ? Convert.ToInt32(row["HesapID"]) : 0,
                        iban = row["IBAN"] != DBNull.Value ? row["IBAN"].ToString() : "",
                        hesapTipi = row["HesapTipi"] != DBNull.Value ? row["HesapTipi"].ToString() : "",
                        bakiye = row["Bakiye"] != DBNull.Value ? Convert.ToDecimal(row["Bakiye"]) : 0m,
                        gunlukHarcamaLimiti = row["GunlukHarcamaLimiti"] != DBNull.Value ? Convert.ToDecimal(row["GunlukHarcamaLimiti"]) : 0m,
                        aylikHarcamaLimiti = row["AylikHarcamaLimiti"] != DBNull.Value ? Convert.ToDecimal(row["AylikHarcamaLimiti"]) : 0m,
                        gunlukCekimLimiti = row["GunlukCekimLimiti"] != DBNull.Value ? Convert.ToDecimal(row["GunlukCekimLimiti"]) : 0m
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Kartlar getirildi.",
                    Data = kartListesi
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
        /// Kart detayını getirir
        /// </summary>
        [HttpGet("{kartID}")]
        public IActionResult GetKart(int kartID)
        {
            try
            {
                BankaKartiModel kart;
                string hata = _sKart.GetKartDetay(kartID, out kart);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                // Kart numarasını maskele
                string maskedKartNo = kart.KartNo.ToString().PadLeft(16, '0');
                maskedKartNo = $"{maskedKartNo.Substring(0, 4)} **** **** {maskedKartNo.Substring(12, 4)}";

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Kart detayı getirildi.",
                    Data = new
                    {
                        kart.KartID,
                        KartNumarasi = maskedKartNo,
                        KartTipi = kart.KartTipi,
                        kart.KartSahibiAdi,
                        kart.SonKullanmaTarihi,
                        kart.Durum,
                        kart.HesapID
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
        /// Kartı bloke eder
        /// </summary>
        [HttpPost("{kartID}/bloke")]
        public IActionResult BlokeEt(int kartID, [FromQuery] int kullaniciID = 1)
        {
            try
            {
                string hata = _sKart.BlockCard(kartID, kullaniciID);

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
                    Message = "Kart başarıyla bloke edildi."
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
        /// Blokeli kartı aktifleştirir
        /// </summary>
        [HttpPost("{kartID}/aktif")]
        public IActionResult Aktiflestir(int kartID, [FromQuery] int kullaniciID = 1)
        {
            try
            {
                string hata = _sKart.ActivateCard(kartID, kullaniciID);

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
                    Message = "Kart başarıyla aktifleştirildi."
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
        /// Kart ayarlarını günceller (limitler)
        /// </summary>
        [HttpPut("{kartID}/ayarlar")]
        public IActionResult AyarlariGuncelle(int kartID, [FromBody] KartAyarlarRequest request)
        {
            try
            {
                string hata = _sKart.UpdateCardLimits(
                    kartID,
                    request.GunlukHarcamaLimiti,
                    request.AylikHarcamaLimiti,
                    request.GunlukCekimLimiti
                );

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
                    Message = "Kart ayarları başarıyla güncellendi."
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
        /// Yeni kart başvurusu
        /// </summary>
        [HttpPost("basvuru")]
        public IActionResult KartBasvurusu([FromBody] KartBasvuruRequest request, [FromQuery] int kullaniciID = 1)
        {
            try
            {
                int kartID;
                long kartNo;
                string hata = _sKart.CreateCard(
                    request.HesapID,
                    request.KartMarkasi,
                    request.KartSahibiAdi,
                    kullaniciID,
                    out kartID,
                    out kartNo
                );

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
                    Message = "Kart başvurunuz başarıyla alındı. Kartınız hazırlanıyor.",
                    Data = new { kartID, kartNo = kartNo.ToString() }
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
    }

    // DTOs
    public class KartAyarlarRequest
    {
        public decimal GunlukHarcamaLimiti { get; set; }
        public decimal AylikHarcamaLimiti { get; set; }
        public decimal GunlukCekimLimiti { get; set; }
    }

    public class KartBasvuruRequest
    {
        public int HesapID { get; set; }
        public string KartMarkasi { get; set; } = "Troy"; // Troy veya Mastercard
        public string KartSahibiAdi { get; set; } = string.Empty;
    }
}
