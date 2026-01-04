using Microsoft.AspNetCore.Mvc;
using MetinBank.Service;
using MetinBank.WebAPI.DTOs;
using System.Data;

namespace MetinBank.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DovizController : ControllerBase
    {
        private readonly SDoviz _sDoviz;

        public DovizController()
        {
            _sDoviz = new SDoviz();
        }

        /// <summary>
        /// Güncel döviz kurlarını getirir
        /// </summary>
        [HttpGet("kurlar")]
        public IActionResult GetDovizKurlari()
        {
            try
            {
                DataTable kurlar;
                string hata = _sDoviz.GetDovizKurlari(out kurlar);

                if (hata != null)
                {
                    return Ok(new ApiResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                var kurListesi = new List<object>();
                foreach (DataRow row in kurlar.Rows)
                {
                    kurListesi.Add(new
                    {
                        ParaBirimi = row["ParaBirimi"].ToString(),
                        AlisFiyati = Convert.ToDecimal(row["AlisFiyati"]),
                        SatisFiyati = Convert.ToDecimal(row["SatisFiyati"]),
                        GuncellemeTarihi = Convert.ToDateTime(row["GuncellemeTarihi"])
                    });
                }

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Döviz kurları getirildi.",
                    Data = kurListesi
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
        /// Belirli bir döviz kurunu getirir
        /// </summary>
        [HttpGet("kur/{paraBirimi}")]
        public IActionResult GetDovizKuru(string paraBirimi)
        {
            try
            {
                decimal alisFiyati, satisFiyati;
                string hata = _sDoviz.GetDovizKuru(paraBirimi, out alisFiyati, out satisFiyati);

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
                    Message = "Döviz kuru getirildi.",
                    Data = new
                    {
                        ParaBirimi = paraBirimi,
                        AlisFiyati = alisFiyati,
                        SatisFiyati = satisFiyati
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
    }
}
