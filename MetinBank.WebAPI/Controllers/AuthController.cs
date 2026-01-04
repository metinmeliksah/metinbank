using Microsoft.AspNetCore.Mvc;
using MetinBank.Service;
using MetinBank.WebAPI.DTOs;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SAuth _sAuth;
        private readonly SMusteri _sMusteri;
        private readonly SMusteriSifre _sMusteriSifre;

        public AuthController()
        {
            _sAuth = new SAuth();
            _sMusteri = new SMusteri();
            _sMusteriSifre = new SMusteriSifre();
        }

        /// <summary>
        /// Müşteri doğrulama (şifre sıfırlama/oluşturma için)
        /// </summary>
        [HttpPost("musteri-dogrula")]
        public IActionResult MusteriDogrula([FromBody] MusteriDogrulamaRequest request)
        {
            try
            {
                MusteriModel musteri;
                string hata;

                // TCKN mi yoksa Müşteri No mu kontrol et
                bool isTCKN = long.TryParse(request.TCKNVeyaMusteriNo, out long tckn);
                
                if (isTCKN && request.TCKNVeyaMusteriNo.Length == 11)
                {
                    hata = _sMusteri.MusteriGetirTCKN(tckn, out musteri);
                }
                else
                {
                    // Müşteri numarası ile arama
                    System.Data.DataTable dt;
                    hata = _sMusteri.MusteriAra(request.TCKNVeyaMusteriNo, out dt);
                    
                    if (hata == null && dt.Rows.Count > 0)
                    {
                        int musteriID = Convert.ToInt32(dt.Rows[0]["MusteriID"]);
                        hata = _sMusteri.MusteriGetir(musteriID, out musteri);
                    }
                    else
                    {
                        musteri = null;
                    }
                }

                if (hata != null || musteri == null)
                {
                    return Ok(new MusteriDogrulamaResponse
                    {
                        Success = false,
                        Message = "Müşteri bulunamadı."
                    });
                }

                if (!string.IsNullOrWhiteSpace(request.DogumTarihi))
                {
                    if (DateTime.TryParse(request.DogumTarihi, out DateTime dogumTarihi))
                    {
                        if (musteri.DogumTarihi.HasValue && musteri.DogumTarihi.Value.Date != dogumTarihi.Date)
                        {
                            return Ok(new MusteriDogrulamaResponse
                            {
                                Success = false,
                                Message = "Doğum tarihi hatalı."
                            });
                        }
                    }
                }

                // Anne adı kontrolü
                if (!string.IsNullOrWhiteSpace(request.AnneAdi))
                {
                    if (!musteri.AnneAdi.Equals(request.AnneAdi, StringComparison.OrdinalIgnoreCase))
                    {
                        return Ok(new MusteriDogrulamaResponse
                        {
                            Success = false,
                            Message = "Anne adı hatalı."
                        });
                    }
                }

                // Cep telefonu kontrolü
                if (!string.IsNullOrWhiteSpace(request.CepTelefon))
                {
                    string temizTelefon = request.CepTelefon.Replace(" ", "").Replace("-", "");
                    string musteriTelefon = musteri.CepTelefon?.Replace(" ", "").Replace("-", "");
                    
                    if (temizTelefon != musteriTelefon)
                    {
                        return Ok(new MusteriDogrulamaResponse
                        {
                            Success = false,
                            Message = "Cep telefonu hatalı."
                        });
                    }
                }

                // Tüm doğrulamalar başarılı
                return Ok(new MusteriDogrulamaResponse
                {
                    Success = true,
                    Message = "Doğrulama başarılı.",
                    MusteriID = musteri.MusteriID,
                    MusteriNo = musteri.MusteriNo,
                    TCKN = musteri.TCKN
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MusteriDogrulamaResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Şifre sıfırla/oluştur (doğrulama sonrası)
        /// </summary>
        [HttpPost("sifre-sifirla")]
        public IActionResult SifreSifirla([FromBody] SifreSifirlaRequest request)
        {
            try
            {
                // Şifre ve tekrar kontrolü
                if (string.IsNullOrWhiteSpace(request.YeniSifre) || string.IsNullOrWhiteSpace(request.YeniSifreTekrar))
                {
                    return Ok(new MusteriSifreOlusturResponse
                    {
                        Success = false,
                        Message = "Şifre boş olamaz."
                    });
                }

                if (request.YeniSifre != request.YeniSifreTekrar)
                {
                    return Ok(new MusteriSifreOlusturResponse
                    {
                        Success = false,
                        Message = "Şifreler eşleşmiyor."
                    });
                }

                // IP adresi
                string ipAdresi = HttpContext.Connection.RemoteIpAddress?.ToString();

                // Şifre oluştur/sıfırla
                int musteriSifreID;
                string hata = _sMusteriSifre.SifreOlustur(request.MusteriID, request.YeniSifre, ipAdresi, out musteriSifreID);

                if (hata != null)
                {
                    return Ok(new MusteriSifreOlusturResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                return Ok(new MusteriSifreOlusturResponse
                {
                    Success = true,
                    Message = "Şifre başarıyla oluşturuldu/sıfırlandı. Şimdi giriş yapabilirsiniz."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MusteriSifreOlusturResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Müşteri şifre var mı kontrol et
        /// </summary>
        [HttpPost("musteri-sifre-var-mi")]
        public IActionResult MusteriSifreVarMi([FromBody] MusteriLoginRequest request)
        {
            try
            {
                // TCKN ile müşteriyi bul
                MusteriModel musteri;
                string hata = _sMusteri.MusteriGetirTCKN(request.TCKN, out musteri);

                if (hata != null || musteri == null)
                {
                    return Ok(new SifreVarMiResponse
                    {
                        Success = false,
                        SifreVar = false,
                        Message = "Müşteri bulunamadı."
                    });
                }

                // Müşteri numarası kontrolü
                if (musteri.MusteriNo != request.MusteriNo)
                {
                    return Ok(new SifreVarMiResponse
                    {
                        Success = false,
                        SifreVar = false,
                        Message = "Müşteri numarası hatalı."
                    });
                }

                // Şifre var mı kontrol et
                bool sifreVar;
                hata = _sMusteriSifre.SifreVarMi(musteri.MusteriID, out sifreVar);

                return Ok(new SifreVarMiResponse
                {
                    Success = true,
                    SifreVar = sifreVar,
                    Message = sifreVar ? "Şifre mevcut" : "Şifre oluşturulmalı"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new SifreVarMiResponse
                {
                    Success = false,
                    SifreVar = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Müşteri şifre oluştur
        /// </summary>
        [HttpPost("musteri-sifre-olustur")]
        public IActionResult MusteriSifreOlustur([FromBody] MusteriSifreOlusturRequest request)
        {
            try
            {
                // TCKN ile müşteriyi bul
                MusteriModel musteri;
                string hata = _sMusteri.MusteriGetirTCKN(request.TCKN, out musteri);

                if (hata != null || musteri == null)
                {
                    return Ok(new MusteriSifreOlusturResponse
                    {
                        Success = false,
                        Message = "Müşteri bulunamadı."
                    });
                }

                // Müşteri numarası kontrolü
                if (musteri.MusteriNo != request.MusteriNo)
                {
                    return Ok(new MusteriSifreOlusturResponse
                    {
                        Success = false,
                        Message = "Müşteri numarası hatalı."
                    });
                }

                // Şifre ve tekrar kontrolü
                if (string.IsNullOrWhiteSpace(request.YeniSifre) || string.IsNullOrWhiteSpace(request.YeniSifreTekrar))
                {
                    return Ok(new MusteriSifreOlusturResponse
                    {
                        Success = false,
                        Message = "Şifre boş olamaz."
                    });
                }

                if (request.YeniSifre != request.YeniSifreTekrar)
                {
                    return Ok(new MusteriSifreOlusturResponse
                    {
                        Success = false,
                        Message = "Şifreler eşleşmiyor."
                    });
                }

                // IP adresi
                string ipAdresi = HttpContext.Connection.RemoteIpAddress?.ToString();

                // Şifre oluştur
                int musteriSifreID;
                hata = _sMusteriSifre.SifreOlustur(musteri.MusteriID, request.YeniSifre, ipAdresi, out musteriSifreID);

                if (hata != null)
                {
                    return Ok(new MusteriSifreOlusturResponse
                    {
                        Success = false,
                        Message = hata
                    });
                }

                return Ok(new MusteriSifreOlusturResponse
                {
                    Success = true,
                    Message = "Şifre başarıyla oluşturuldu. Şimdi giriş yapabilirsiniz."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MusteriSifreOlusturResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Müşteri girişi (TCKN + Müşteri No + Şifre)
        /// </summary>
        [HttpPost("musteri-login")]
        public IActionResult MusteriLogin([FromBody] MusteriLoginRequest request)
        {
            try
            {
                MusteriModel musteri;
                string hata;

                // TCKN veya Müşteri No ile müşteriyi bul
                if (request.TCKN > 0)
                {
                    // TCKN ile arama
                    hata = _sMusteri.MusteriGetirTCKN(request.TCKN, out musteri);
                }
                else if (!string.IsNullOrWhiteSpace(request.MusteriNo))
                {
                    // Müşteri No ile arama
                    System.Data.DataTable dt;
                    hata = _sMusteri.MusteriAra(request.MusteriNo, out dt);
                    
                    if (hata == null && dt.Rows.Count > 0)
                    {
                        int musteriID = Convert.ToInt32(dt.Rows[0]["MusteriID"]);
                        hata = _sMusteri.MusteriGetir(musteriID, out musteri);
                    }
                    else
                    {
                        musteri = null;
                    }
                }
                else
                {
                    return Ok(new LoginResponse
                    {
                        Success = false,
                        Message = "TCKN veya Müşteri Numarası giriniz."
                    });
                }

                if (hata != null || musteri == null)
                {
                    return Ok(new LoginResponse
                    {
                        Success = false,
                        Message = "Müşteri bulunamadı."
                    });
                }

                // IP adresi
                string ipAdresi = HttpContext.Connection.RemoteIpAddress?.ToString();

                // Şifre doğrulama
                bool basarili;
                hata = _sMusteriSifre.SifreDogrula(musteri.MusteriID, request.Sifre, ipAdresi, out basarili);

                if (!basarili || hata != null)
                {
                    return Ok(new LoginResponse
                    {
                        Success = false,
                        Message = hata ?? "Şifre hatalı."
                    });
                }
                
                // JWT Token oluştur
                var kullaniciModel = new KullaniciModel
                {
                    KullaniciID = musteri.MusteriID,
                    KullaniciAdi = musteri.MusteriNo,
                    Ad = musteri.Ad,
                    Soyad = musteri.Soyad,
                    Email = musteri.Email,
                    RolAdi = "Musteri"
                };

                string token = _sAuth.GenerateJwtToken(kullaniciModel);

                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "Giriş başarılı",
                    Token = token,
                    Kullanici = new KullaniciInfo
                    {
                        MusteriID = musteri.MusteriID,
                        MusteriNo = musteri.MusteriNo,
                        Ad = musteri.Ad,
                        Soyad = musteri.Soyad,
                        Email = musteri.Email ?? "",
                        Telefon = musteri.CepTelefon ?? ""
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponse
                {
                    Success = false,
                    Message = $"Sunucu hatası: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Şifre değiştirme
        /// </summary>
        [HttpPost("sifre-degistir")]
        public IActionResult SifreDegistir([FromBody] SifreDegistirRequest request)
        {
            try
            {
                string ipAdresi = HttpContext.Connection.RemoteIpAddress?.ToString();
                string hata = _sMusteriSifre.SifreDegistir(request.MusteriID, request.EskiSifre, request.YeniSifre, ipAdresi);

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
                    Message = "Şifre başarıyla değiştirildi."
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
        /// Profil bilgilerini getir
        /// </summary>
        [HttpGet("profil/{musteriID}")]
        public IActionResult GetProfil(int musteriID)
        {
            try
            {
                MusteriModel musteri;
                string hata = _sMusteri.MusteriGetir(musteriID, out musteri);

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
                    Message = "Profil bilgileri getirildi.",
                    Data = musteri
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
