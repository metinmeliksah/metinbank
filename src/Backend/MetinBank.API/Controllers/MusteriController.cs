/*
 * MetinBank - Müşteri API Controller
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Müşteri işlemleri için API endpoints
 * Standart: Controller suffix, metodlar PascalCase, parametreler camelCase
 */

using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace MetinBank.API.Controllers
{
    /// <summary>
    /// Müşteri API Controller
    /// Standart: [Controller] suffix kullanılmalı (MusteriController)
    /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MusteriController : ControllerBase
    {
        // Private değişkenler - class başında tanımlı (Standart)
        // NOT: Service referansı DI (Dependency Injection) ile verilmeli
        // private readonly ISMusteriService _musteriService;

        /// <summary>
        /// Constructor
        /// </summary>
        public MusteriController()
        {
            // DI ile service inject edilecek
            // _musteriService = musteriService;
        }

        /// <summary>
        /// Müşteri ekler
        /// POST: api/Musteri/Ekle
        /// Standart: Method ismi PascalCase (MusteriEkle)
        /// </summary>
        /// <param name="request">Müşteri ekleme request model</param>
        /// <returns>ApiResponse</returns>
        [HttpPost("Ekle")]
        public IActionResult MusteriEkle([FromBody] MusteriEkleRequest request)
        {
            string hata = null; // Method içinde tanımlama - Standart

            try
            {
                // Validasyon
                if (request == null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Basarili = false,
                        Mesaj = "Geçersiz istek"
                    });
                }

                if (string.IsNullOrWhiteSpace(request.TcKimlikNo))
                {
                    return BadRequest(new ApiResponse
                    {
                        Basarili = false,
                        Mesaj = "TC Kimlik No gerekli"
                    });
                }

                // Service çağrısı
                // Standart: if(hata!=null) kontrolü yapılmalı
                // long musteriNo = _musteriService.MusteriEkle(
                //     request.TcKimlikNo,
                //     request.Ad,
                //     request.Soyad,
                //     request.Eposta,
                //     request.Telefon
                // );
                
                // if (hata != null)
                // {
                //     return BadRequest(new ApiResponse
                //     {
                //         Basarili = false,
                //         Mesaj = hata
                //     });
                // }

                return Ok(new ApiResponse<long>
                {
                    Basarili = true,
                    Mesaj = "Müşteri başarıyla eklendi",
                    Data = 100001 // musteriNo
                });
            }
            catch (Exception ex) // Exception standart: ex
            {
                return StatusCode(500, new ApiResponse
                {
                    Basarili = false,
                    Mesaj = "Sunucu hatası: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri bilgilerini getirir
        /// GET: api/Musteri/Getir/{musteriNo}
        /// Standart: Parametreler camelCase (musteriNo)
        /// </summary>
        [HttpGet("Getir/{musteriNo}")]
        public IActionResult MusteriGetir(long musteriNo)
        {
            string hata = null;

            try
            {
                if (musteriNo <= 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Basarili = false,
                        Mesaj = "Geçersiz müşteri numarası"
                    });
                }

                // Service çağrısı
                // DataTable dt = _musteriService.MusteriBul(musteriNo.ToString());
                
                // if (dt == null || dt.Rows.Count == 0)
                // {
                //     return NotFound(new ApiResponse
                //     {
                //         Basarili = false,
                //         Mesaj = "Müşteri bulunamadı"
                //     });
                // }

                // Örnek response
                return Ok(new ApiResponse<MusteriDto>
                {
                    Basarili = true,
                    Mesaj = "Başarılı",
                    Data = new MusteriDto
                    {
                        MusteriNo = musteriNo,
                        Ad = "Metin",
                        Soyad = "Dermencioğlu",
                        TcKimlikNo = "12345678901",
                        Eposta = "metin@example.com",
                        Telefon = "05551234567"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Basarili = false,
                    Mesaj = "Sunucu hatası: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri listesini getirir
        /// GET: api/Musteri/Liste
        /// </summary>
        [HttpGet("Liste")]
        public IActionResult MusteriListesi([FromQuery] int sayfa = 1, [FromQuery] int sayfaBoyut = 50)
        {
            try
            {
                // Standart: Sayfa boyutu kontrolü (max 100)
                if (sayfaBoyut > 100)
                    sayfaBoyut = 100;

                // Service çağrısı
                // DataTable dt = _musteriService.MusterileriGetir();
                
                // Örnek response
                return Ok(new ApiResponse<MusteriListeDto>
                {
                    Basarili = true,
                    Mesaj = "Başarılı",
                    Data = new MusteriListeDto
                    {
                        ToplamKayit = 0,
                        Sayfa = sayfa,
                        SayfaBoyut = sayfaBoyut,
                        Musteriler = new System.Collections.Generic.List<MusteriDto>()
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Basarili = false,
                    Mesaj = "Sunucu hatası: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri günceller
        /// PUT: api/Musteri/Guncelle
        /// </summary>
        [HttpPut("Guncelle")]
        public IActionResult MusteriGuncelle([FromBody] MusteriGuncelleRequest request)
        {
            string hata = null;

            try
            {
                if (request == null || request.MusteriNo <= 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Basarili = false,
                        Mesaj = "Geçersiz istek"
                    });
                }

                // Service çağrısı
                // hata = _musteriService.MusteriGuncelle(
                //     request.MusteriNo,
                //     request.Ad,
                //     request.Soyad,
                //     request.Eposta
                // );

                // if (hata != null)
                // {
                //     return BadRequest(new ApiResponse { Basarili = false, Mesaj = hata });
                // }

                return Ok(new ApiResponse
                {
                    Basarili = true,
                    Mesaj = "Müşteri başarıyla güncellendi"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Basarili = false,
                    Mesaj = "Sunucu hatası: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Müşteri siler
        /// DELETE: api/Musteri/Sil/{musteriNo}
        /// </summary>
        [HttpDelete("Sil/{musteriNo}")]
        public IActionResult MusteriSil(long musteriNo)
        {
            string hata = null;

            try
            {
                if (musteriNo <= 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Basarili = false,
                        Mesaj = "Geçersiz müşteri numarası"
                    });
                }

                // Service çağrısı
                // hata = _musteriService.MusteriSil(musteriNo);
                
                // if (hata != null)
                // {
                //     return BadRequest(new ApiResponse { Basarili = false, Mesaj = hata });
                // }

                return Ok(new ApiResponse
                {
                    Basarili = true,
                    Mesaj = "Müşteri başarıyla silindi"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Basarili = false,
                    Mesaj = "Sunucu hatası: " + ex.Message
                });
            }
        }
    }

    #region Request/Response Models

    /// <summary>
    /// Müşteri ekleme request model
    /// Standart: PascalCase property isimleri
    /// </summary>
    public class MusteriEkleRequest
    {
        public string TcKimlikNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public int SubeKod { get; set; }
    }

    /// <summary>
    /// Müşteri güncelleme request model
    /// </summary>
    public class MusteriGuncelleRequest
    {
        public long MusteriNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
    }

    /// <summary>
    /// Müşteri DTO
    /// </summary>
    public class MusteriDto
    {
        public long MusteriNo { get; set; }
        public string TcKimlikNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public int SubeKod { get; set; }
        public DateTime KayitTarih { get; set; }
    }

    /// <summary>
    /// Müşteri liste DTO
    /// </summary>
    public class MusteriListeDto
    {
        public int ToplamKayit { get; set; }
        public int Sayfa { get; set; }
        public int SayfaBoyut { get; set; }
        public System.Collections.Generic.List<MusteriDto> Musteriler { get; set; }
    }

    /// <summary>
    /// API Response model
    /// </summary>
    public class ApiResponse
    {
        public bool Basarili { get; set; }
        public string Mesaj { get; set; }
    }

    /// <summary>
    /// API Response with data
    /// </summary>
    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }

    #endregion
}


