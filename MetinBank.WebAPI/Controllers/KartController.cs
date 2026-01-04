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
                    string kartNo = row["KartNo"].ToString() ?? "";
                    string maskedKartNo = kartNo.Length >= 16 
                        ? $"{kartNo.Substring(0, 4)} **** **** {kartNo.Substring(12, 4)}" 
                        : kartNo;

                    kartListesi.Add(new
                    {
                        KartID = Convert.ToInt32(row["KartID"]),
                        KartNo = maskedKartNo,  // Used in frontend as kartNo (camelCase)
                        KartTipi = row["KartTipi"].ToString(),
                        KartSahibiAdi = row["KartSahibiAdi"].ToString(),
                        SonKullanimTarihi = Convert.ToDateTime(row["SonKullanmaTarihi"]),
                        Durum = row["Durum"].ToString(),
                        HesapID = Convert.ToInt32(row["HesapID"])  // From JOIN with Hesap
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
        [HttpPost("{kartID}/aktiflestir")]
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
    }
}
