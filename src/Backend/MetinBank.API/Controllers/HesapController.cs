/*
 * MetinBank - Hesap API Controller
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Hesap işlemleri için API endpoints
 * Standart: Controller suffix, metodlar PascalCase
 */

using Microsoft.AspNetCore.Mvc;
using System;

namespace MetinBank.API.Controllers
{
    /// <summary>
    /// Hesap API Controller
    /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HesapController : ControllerBase
    {
        // Private değişkenler - class başında tanımlı
        // private readonly ISHesapService _hesapService;

        /// <summary>
        /// Constructor
        /// </summary>
        public HesapController()
        {
            // DI ile service inject edilecek
        }

        /// <summary>
        /// Hesap açar
        /// POST: api/Hesap/Ac
        /// </summary>
        [HttpPost("Ac")]
        public IActionResult HesapAc([FromBody] HesapAcRequest request)
        {
            string hata = null; // Method içinde tanımlama - Standart

            try
            {
                if (request == null || request.MusteriNo <= 0)
                {
                    return BadRequest(new { Basarili = false, Mesaj = "Geçersiz istek" });
                }

                // Service çağrısı
                // string hesapNo = _hesapService.HesapAc(
                //     request.MusteriNo,
                //     request.HesapTip,
                //     request.DovizKod,
                //     request.IlkBakiye
                // );
                
                // if (hata != null)
                // {
                //     return BadRequest(new { Basarili = false, Mesaj = hata });
                // }

                return Ok(new
                {
                    Basarili = true,
                    Mesaj = "Hesap başarıyla açıldı",
                    Data = new { HesapNo = "TR330006200000000001234567" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Basarili = false, Mesaj = "Sunucu hatası: " + ex.Message });
            }
        }

        /// <summary>
        /// Para yatırır
        /// POST: api/Hesap/ParaYatir
        /// Standart: Method ismi PascalCase
        /// </summary>
        [HttpPost("ParaYatir")]
        public IActionResult ParaYatir([FromBody] ParaYatirRequest request)
        {
            string hata = null;

            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.HesapNo))
                {
                    return BadRequest(new { Basarili = false, Mesaj = "Geçersiz istek" });
                }

                if (request.Tutar <= 0)
                {
                    return BadRequest(new { Basarili = false, Mesaj = "Geçersiz tutar" });
                }

                // Service çağrısı
                // hata = _hesapService.ParaYatir(request.HesapNo, request.Tutar);
                
                // if (hata != null)
                // {
                //     return BadRequest(new { Basarili = false, Mesaj = hata });
                // }

                return Ok(new
                {
                    Basarili = true,
                    Mesaj = "Para yatırma işlemi başarılı",
                    Data = new { YeniBakiye = 1500.50m }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Basarili = false, Mesaj = "Sunucu hatası: " + ex.Message });
            }
        }

        /// <summary>
        /// Para çeker
        /// POST: api/Hesap/ParaCek
        /// </summary>
        [HttpPost("ParaCek")]
        public IActionResult ParaCek([FromBody] ParaCekRequest request)
        {
            string hata = null;

            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.HesapNo))
                {
                    return BadRequest(new { Basarili = false, Mesaj = "Geçersiz istek" });
                }

                if (request.Tutar <= 0)
                {
                    return BadRequest(new { Basarili = false, Mesaj = "Geçersiz tutar" });
                }

                // Service çağrısı
                // hata = _hesapService.ParaCek(request.HesapNo, request.Tutar);
                
                // if (hata != null)
                // {
                //     return BadRequest(new { Basarili = false, Mesaj = hata });
                // }

                return Ok(new
                {
                    Basarili = true,
                    Mesaj = "Para çekme işlemi başarılı",
                    Data = new { YeniBakiye = 500.50m }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Basarili = false, Mesaj = "Sunucu hatası: " + ex.Message });
            }
        }

        /// <summary>
        /// Bakiye sorgular
        /// GET: api/Hesap/Bakiye/{hesapNo}
        /// </summary>
        [HttpGet("Bakiye/{hesapNo}")]
        public IActionResult BakiyeSorgula(string hesapNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hesapNo))
                {
                    return BadRequest(new { Basarili = false, Mesaj = "Hesap numarası gerekli" });
                }

                // Service çağrısı
                // decimal bakiye = _hesapService.BakiyeSorgula(hesapNo);

                return Ok(new
                {
                    Basarili = true,
                    Mesaj = "Başarılı",
                    Data = new
                    {
                        HesapNo = hesapNo,
                        Bakiye = 1000.00m,
                        KullanilabilirBakiye = 950.00m,
                        DovizKod = "TRY"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Basarili = false, Mesaj = "Sunucu hatası: " + ex.Message });
            }
        }

        /// <summary>
        /// Hesap listesini getirir
        /// GET: api/Hesap/Liste/{musteriNo}
        /// </summary>
        [HttpGet("Liste/{musteriNo}")]
        public IActionResult HesapListesi(long musteriNo)
        {
            try
            {
                if (musteriNo <= 0)
                {
                    return BadRequest(new { Basarili = false, Mesaj = "Geçersiz müşteri numarası" });
                }

                // Service çağrısı
                // DataTable dt = _hesapService.HesaplariGetir(musteriNo);

                return Ok(new
                {
                    Basarili = true,
                    Mesaj = "Başarılı",
                    Data = new System.Collections.Generic.List<HesapDto>()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Basarili = false, Mesaj = "Sunucu hatası: " + ex.Message });
            }
        }
    }

    #region Request Models

    /// <summary>
    /// Hesap açma request
    /// </summary>
    public class HesapAcRequest
    {
        public long MusteriNo { get; set; }
        public int HesapTip { get; set; }
        public int DovizKod { get; set; }
        public decimal IlkBakiye { get; set; }
    }

    /// <summary>
    /// Para yatırma request
    /// </summary>
    public class ParaYatirRequest
    {
        public string HesapNo { get; set; }
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; }
    }

    /// <summary>
    /// Para çekme request
    /// </summary>
    public class ParaCekRequest
    {
        public string HesapNo { get; set; }
        public decimal Tutar { get; set; }
        public string Aciklama { get; set; }
    }

    /// <summary>
    /// Hesap DTO
    /// </summary>
    public class HesapDto
    {
        public string HesapNo { get; set; }
        public int HesapTip { get; set; }
        public string HesapTipAd { get; set; }
        public decimal Bakiye { get; set; }
        public string DovizKod { get; set; }
        public DateTime AcilisTarih { get; set; }
    }

    #endregion
}


