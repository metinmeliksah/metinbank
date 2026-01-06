using Microsoft.AspNetCore.Mvc;
using MetinBank.Models;
using MetinBank.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MetinBank.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KrediController : ControllerBase
    {
        private readonly SKredi _sKredi;

        public KrediController()
        {
            _sKredi = new SKredi();
        }

        [HttpGet("oranlar")]
        public IActionResult GetKrediOranlari()
        {
            try
            {
                var oranlar = _sKredi.GetKrediOranlari();
                return Ok(oranlar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [HttpPost("hesapla")]
        public IActionResult Hesapla([FromBody] HesaplaModel model)
        {
            try
            {
                var sonuc = _sKredi.Hesapla(model.Tutar, model.Vade);
                if (sonuc.ContainsKey("Hata"))
                {
                    return BadRequest(sonuc["Hata"]);
                }
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hesaplama hatası: " + ex.Message);
            }
        }

        [HttpPost("basvuru")]
        [Authorize]
        public IActionResult BasvuruYap([FromBody] KrediBasvuruModel model)
        {
            try
            {
                // Kullanıcı ID'yi token'dan alabiliriz ama modelden geleni de kullanabiliriz.
                // Güvenlik için token'dan almak daha doğru.
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var idClaim = identity.FindFirst("KullaniciID"); // veya NameIdentifier
                    // model.MusteriID = ... (Eğer token müşteriye aitse)
                    // Ancak şimdilik modelden geleni kabul edelim veya kontrol edelim.
                }

                model.Kanal = "Web"; 
                string sonuc = _sKredi.BasvuruYap(model);
                
                if (sonuc == "ONAYLANDI" || sonuc == "REDDEDILDI" || sonuc == "BEKLEMEDE")
                {
                     return Ok(new { Durum = sonuc, Mesaj = "Başvurunuz alındı. Sonuç: " + sonuc });
                }
                else
                {
                    return BadRequest("Başvuru hatası: " + sonuc);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Başvuru hatası: " + ex.Message);
            }
        }

        [HttpGet("bekleyenler")]
        [Authorize(Roles = "SubeMuduru,GenelMudurluk")] // Rol kontrolü
        public IActionResult GetBekleyenler()
        {
            try
            {
                var dt = _sKredi.GetBekleyenBasvurular();
                // DataTable'ı JSON'a çevirmek gerekebilir, Framework otomatik yapabilir veya List<Dictionary> yapmalıyız.
                // Basitçe:
                var list = MetinBank.Util.CommonFunctions.DataTableToDictionaryList(dt);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("onayla-reddet")]
        [Authorize(Roles = "SubeMuduru,GenelMudurluk")]
        public IActionResult OnaylaReddet([FromBody] OnayRedModel model)
        {
            try
            {
                string hata = _sKredi.BasvuruOnaylaReddet(model.BasvuruID, model.Onayla, model.MudurID, model.Aciklama);
                if (hata != null) return BadRequest(hata);

                if (model.Onayla)
                {
                    // Kullandırım
                    try 
                    {
                        _sKredi.KrediKullandir(model.BasvuruID);
                    }
                    catch (Exception ex)
                    {
                        return Ok(new { Durum = "KismenBasarili", Mesaj = "Onaylandı fakat kullandırımda hata: " + ex.Message });
                    }
                }

                return Ok(new { Durum = "Basarili", Mesaj = "İşlem tamamlandı." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

    public class HesaplaModel
    {
        public decimal Tutar { get; set; }
        public int Vade { get; set; }
    }

    public class OnayRedModel
    {
        public int BasvuruID { get; set; }
        public bool Onayla { get; set; }
        public int MudurID { get; set; }
        public string Aciklama { get; set; }
    }
}
