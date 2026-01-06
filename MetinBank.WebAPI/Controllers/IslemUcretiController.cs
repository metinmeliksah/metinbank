using Microsoft.AspNetCore.Mvc;
using MetinBank.Service;

namespace MetinBank.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IslemUcretiController : ControllerBase
    {
        private readonly SIslemUcreti _sIslemUcreti;

        public IslemUcretiController()
        {
            _sIslemUcreti = new SIslemUcreti();
        }

        /// <summary>
        /// İşlem ücretini hesaplar
        /// GET: api/IslemUcreti/hesapla?islemTipi=Havale&islemKanali=Internet&tutar=5000
        /// </summary>
        [HttpGet("hesapla")]
        public IActionResult IslemUcretiHesapla(
            [FromQuery] string islemTipi,
            [FromQuery] string islemKanali,
            [FromQuery] decimal tutar)
        {
            try
            {
                if (string.IsNullOrEmpty(islemTipi) || string.IsNullOrEmpty(islemKanali))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "İşlem tipi ve kanal bilgisi gereklidir."
                    });
                }

                if (tutar <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Geçersiz tutar."
                    });
                }

                decimal ucret = _sIslemUcreti.IslemUcretiHesapla(islemTipi, islemKanali, tutar);

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        islemTipi,
                        islemKanali,
                        tutar,
                        ucret,
                        toplamTutar = tutar + ucret
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Tüm işlem ücretlerini getirir
        /// GET: api/IslemUcreti/liste
        /// </summary>
        [HttpGet("liste")]
        public IActionResult TumUcretleriGetir()
        {
            try
            {
                var dt = _sIslemUcreti.TumUcretleriGetir();
                var ucretler = new List<object>();

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    ucretler.Add(new
                    {
                        islemTipi = row["IslemTipi"]?.ToString(),
                        islemKanali = row["IslemKanali"]?.ToString(),
                        minTutar = row["MinTutar"] != DBNull.Value ? Convert.ToDecimal(row["MinTutar"]) : 0,
                        maxTutar = row["MaxTutar"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["MaxTutar"]) : null,
                        ucret = row["Ucret"] != DBNull.Value ? Convert.ToDecimal(row["Ucret"]) : 0,
                        aktif = row["Aktif"] != DBNull.Value && Convert.ToBoolean(row["Aktif"])
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = ucretler
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Belirli işlem tipi için ücretleri getirir
        /// GET: api/IslemUcreti/tip?islemTipi=Havale&islemKanali=Internet
        /// </summary>
        [HttpGet("tip")]
        public IActionResult IslemTipiUcretleriGetir(
            [FromQuery] string islemTipi,
            [FromQuery] string islemKanali)
        {
            try
            {
                if (string.IsNullOrEmpty(islemTipi) || string.IsNullOrEmpty(islemKanali))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "İşlem tipi ve kanal bilgisi gereklidir."
                    });
                }

                var dt = _sIslemUcreti.IslemTipiUcretleriGetir(islemTipi, islemKanali);
                var ucretler = new List<object>();

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    ucretler.Add(new
                    {
                        minTutar = row["MinTutar"] != DBNull.Value ? Convert.ToDecimal(row["MinTutar"]) : 0,
                        maxTutar = row["MaxTutar"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["MaxTutar"]) : null,
                        ucret = row["Ucret"] != DBNull.Value ? Convert.ToDecimal(row["Ucret"]) : 0
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = ucretler
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
