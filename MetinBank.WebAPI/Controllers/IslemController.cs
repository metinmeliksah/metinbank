using Microsoft.AspNetCore.Mvc;
using MetinBank.Service;
using MetinBank.WebAPI.DTOs;
using System.Data;
using MetinBank.Util;

namespace MetinBank.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IslemController : ControllerBase
    {
        private readonly SIslem _sIslem;

        public IslemController()
        {
            _sIslem = new SIslem();
        }

        /// <summary>
        /// Havale işlemi
        /// </summary>
        [HttpPost("havale")]
        public IActionResult Havale([FromBody] HavaleRequest request, [FromQuery] int kullaniciID = 1, [FromQuery] int subeID = 1)
        {
            try
            {
                long islemID;
                string hata = _sIslem.Havale(
                    request.KaynakHesapID,
                    request.HedefIBAN,
                    request.Tutar,
                    request.Aciklama,
                    request.AliciAdi,
                    kullaniciID,
                    subeID,
                    request.IslemUcreti,
                    out islemID
                );

                if (hata != null)
                {
                    return Ok(new IslemResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                return Ok(new IslemResponse
                {
                    Success = true,
                    Message = "Havale işlemi başarılı.",
                    IslemID = islemID,
                    IslemReferansNo = $"HVL{DateTime.Now:yyyyMMdd}{islemID}",
                    Data = new { islemID, islemReferansNo = $"HVL{DateTime.Now:yyyyMMdd}{islemID}" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IslemResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// EFT işlemi
        /// </summary>
        [HttpPost("eft")]
        public IActionResult EFT([FromBody] EFTRequest request, [FromQuery] int kullaniciID = 1, [FromQuery] int subeID = 1)
        {
            try
            {
                long islemID;
                string hata = _sIslem.EFT(
                    request.KaynakHesapID,
                    request.HedefIBAN,
                    request.Tutar,
                    request.Aciklama,
                    request.AliciAdi,
                    kullaniciID,
                    subeID,
                    request.IslemUcreti,
                    out islemID
                );

                if (hata != null)
                {
                    return Ok(new IslemResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                return Ok(new IslemResponse
                {
                    Success = true,
                    Message = "EFT işlemi başarılı.",
                    IslemID = islemID,
                    IslemReferansNo = $"EFT{DateTime.Now:yyyyMMdd}{islemID}",
                    Data = new { islemID, islemReferansNo = $"EFT{DateTime.Now:yyyyMMdd}{islemID}" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IslemResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Virman işlemi
        /// </summary>
        [HttpPost("virman")]
        public IActionResult Virman([FromBody] VirmanRequest request, [FromQuery] int kullaniciID = 1, [FromQuery] int subeID = 1)
        {
            try
            {
                long islemID;
                string hata = _sIslem.Virman(
                    request.KaynakHesapID,
                    request.HedefHesapID,
                    request.Tutar,
                    request.Aciklama,
                    kullaniciID,
                    subeID,
                    out islemID
                );

                if (hata != null)
                {
                    return Ok(new IslemResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                return Ok(new IslemResponse
                {
                    Success = true,
                    Message = "Virman işlemi başarılı.",
                    IslemID = islemID,
                    IslemReferansNo = $"VRM{DateTime.Now:yyyyMMdd}{islemID}",
                    Data = new { islemID, islemReferansNo = $"VRM{DateTime.Now:yyyyMMdd}{islemID}" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IslemResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Müşterinin işlem geçmişi
        /// </summary>
        [HttpGet("musteri/{musteriID}/gecmis")]
        public IActionResult GetMusteriIslemleri(int musteriID)
        {
            try
            {
                DataTable islemler;
                string hata = _sIslem.MusterininIslemleri(musteriID, out islemler);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                var islemListesi = new List<object>();
                foreach (DataRow row in islemler.Rows)
                {
                    islemListesi.Add(new
                    {
                        IslemID = Convert.ToInt64(row["IslemID"]),
                        IslemTipi = row["IslemTipi"].ToString(),
                        Tutar = Convert.ToDecimal(row["Tutar"]),
                        ParaBirimi = row["ParaBirimi"].ToString(),
                        IslemTarihi = Convert.ToDateTime(row["IslemTarihi"]),
                        Aciklama = row["Aciklama"].ToString(),
                        OnayDurumu = row["OnayDurumu"].ToString()
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "İşlem geçmişi getirildi.",
                    Data = islemListesi
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
        /// Hesabın işlem geçmişi (ekstre)
        /// </summary>
        [HttpGet("hesap/{hesapID}/ekstre")]
        public IActionResult GetHesapEkstresi(int hesapID)
        {
            try
            {
                DataTable islemler;
                string hata = _sIslem.HesabinIslemleri(hesapID, out islemler);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                var islemListesi = new List<object>();
                foreach (DataRow row in islemler.Rows)
                {
                    islemListesi.Add(new
                    {
                        IslemID = Convert.ToInt64(row["IslemID"]),
                        IslemTipi = row["IslemTipi"].ToString(),
                        Tutar = Convert.ToDecimal(row["Tutar"]),
                        ParaBirimi = row["ParaBirimi"].ToString(),
                        IslemTarihi = Convert.ToDateTime(row["IslemTarihi"]),
                        Aciklama = row["Aciklama"].ToString(),
                        AliciAdi = row.Table.Columns.Contains("AliciAdi") ? row["AliciAdi"].ToString() : "",
                        OnayDurumu = row["OnayDurumu"].ToString()
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Hesap ekstresi getirildi.",
                    Data = islemListesi
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
