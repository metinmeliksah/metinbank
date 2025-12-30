# METİN BANK PROJESİ - GELİŞTİRME REHBERİ

## 📋 İçindekiler
1. [Proje Yapısı](#proje-yapısı)
2. [Kurulum Adımları](#kurulum-adımları)
3. [Tamamlanması Gereken Kısımlar](#tamamlanması-gereken-kısımlar)
4. [Çalıştırma Talimatları](#çalıştırma-talimatları)
5. [Test Senaryoları](#test-senaryoları)

## 🏗️ Proje Yapısı

Proje çok katmanlı mimari ile tasarlanmıştır:

```
MetinBank/
├── Database/               ✅ TAMAMLANDI
│   └── MetinBank_Schema.sql
├── MetinBank.Util/         ✅ TAMAMLANDI
│   ├── DataAccess.cs
│   ├── SecurityHelper.cs
│   ├── IbanHelper.cs
│   ├── ValidationHelper.cs
│   └── CommonFunctions.cs
├── MetinBank.Models/       ✅ TAMAMLANDI
│   ├── KullaniciModel.cs
│   ├── MusteriModel.cs
│   ├── HesapModel.cs
│   └── ... (diğer modeller)
├── MetinBank.Interface/    ✅ TAMAMLANDI
│   ├── IMusteri.cs
│   ├── IHesap.cs
│   ├── IIslem.cs
│   └── ... (diğer interface'ler)
├── MetinBank.Business/     ⚠️ KISMEN TAMAMLANDI (BMusteri örneği mevcut)
│   ├── BMusteri.cs         ✅
│   ├── BHesap.cs           ❌ Oluşturulacak
│   ├── BIslem.cs           ❌ Oluşturulacak
│   ├── BAuth.cs            ❌ Oluşturulacak
│   ├── BOnay.cs            ❌ Oluşturulacak
│   └── BLog.cs             ❌ Oluşturulacak
├── MetinBank.Service/      ⚠️ KISMEN TAMAMLANDI (SAuth örneği mevcut)
│   ├── SMusteri.cs         ❌ Oluşturulacak
│   ├── SHesap.cs           ❌ Oluşturulacak
│   ├── SIslem.cs           ❌ Oluşturulacak
│   ├── SAuth.cs            ✅
│   └── SLog.cs             ❌ Oluşturulacak
├── MetinBank.Forms/        ⚠️ KISMEN TAMAMLANDI (FrmGiris örneği mevcut)
│   ├── App.config          ✅
│   ├── FrmGiris.cs         ✅
│   ├── FrmAnaSayfa.cs      ❌ Oluşturulacak
│   ├── FrmMusteriIslem.cs  ❌ Oluşturulacak
│   ├── FrmHesapIslem.cs    ❌ Oluşturulacak
│   └── ... (diğer formlar)
├── MetinBank.WebAPI/       ⚠️ KISMEN TAMAMLANDI (Yapılandırma dosyası mevcut)
│   ├── appsettings.json    ✅
│   ├── Controllers/        ❌ Oluşturulacak
│   └── Middleware/         ❌ Oluşturulacak
└── MetinBank.Web/          ⚠️ KISMEN TAMAMLANDI (Login sayfası mevcut)
    ├── login.html          ✅
    ├── dashboard.html      ❌ Oluşturulacak
    └── assets/             ❌ Oluşturulacak
```

## 🚀 Kurulum Adımları

### 1. Gerekli Yazılımlar

- **Visual Studio 2022** (Community Edition yeterli)
- **MySQL 8.0** veya üzeri
- **MySQL Workbench** (opsiyonel, veritabanı yönetimi için)
- **.NET Framework 4.8** (Windows Forms için)
- **.NET Core 6.0 SDK** (Web API için)
- **DevExpress** (Windows Forms için - Trial veya lisanslı)

### 2. NuGet Paketleri

Her proje için gerekli NuGet paketlerini yükleyin:

#### MetinBank.Util, Business, Service
```bash
Install-Package MySql.Data -Version 8.0.33
Install-Package Newtonsoft.Json -Version 13.0.3
```

#### MetinBank.Forms
```bash
Install-Package MySql.Data -Version 8.0.33
Install-Package DevExpress.WindowsForms -Version 23.2.3
```

#### MetinBank.WebAPI
```bash
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 6.0.0
Install-Package Microsoft.IdentityModel.Tokens -Version 6.30.0
Install-Package System.IdentityModel.Tokens.Jwt -Version 6.30.0
Install-Package MySql.Data -Version 8.0.33
Install-Package Newtonsoft.Json -Version 13.0.3
Install-Package Swashbuckle.AspNetCore -Version 6.5.0
```

### 3. Veritabanı Kurulumu

```sql
-- MySQL'e bağlanın
mysql -u root -p

-- Scripti çalıştırın
source D:/Github/metinbank/Database/MetinBank_Schema.sql

-- Veritabanını kontrol edin
USE MetinBankDB;
SHOW TABLES;
```

### 4. Şifre Hash'lerini Güncelleme

Veritabanındaki kullanıcıların şifreleri başlangıçta `TEMP_HASH` ve `TEMP_SALT` değerleri ile oluşturulmuştur. 
Gerçek hash değerlerini oluşturmak için:

```csharp
// C# Console Application veya Test Projesi oluşturun
using MetinBank.Util;

string sifre = "Password123!";
string salt = SecurityHelper.GenerateSalt();
string hashedPassword = SecurityHelper.HashPassword(sifre, salt);

Console.WriteLine($"Salt: {salt}");
Console.WriteLine($"Hashed Password: {hashedPassword}");

// Bu değerleri veritabanında UPDATE edin
```

**SQL Update Örneği:**
```sql
UPDATE Kullanici 
SET Sifre = 'hash_deger', 
    SifreTuzu = 'salt_deger' 
WHERE KullaniciAdi = 'gm.admin';
```

### 5. Bağlantı Stringlerini Yapılandırma

#### MetinBank.Forms/App.config
```xml
<connectionStrings>
  <add name="MetinBankDB" 
       connectionString="Server=localhost;Database=MetinBankDB;Uid=root;Pwd=YOUR_PASSWORD;CharSet=utf8mb4;" 
       providerName="MySql.Data.MySqlClient"/>
</connectionStrings>
```

#### MetinBank.WebAPI/appsettings.json
```json
{
  "ConnectionStrings": {
    "MetinBankDB": "Server=localhost;Database=MetinBankDB;Uid=root;Pwd=YOUR_PASSWORD;CharSet=utf8mb4;"
  }
}
```

## 📝 Tamamlanması Gereken Kısımlar

### A. Business Layer (Yüksek Öncelik)

**BHesap.cs** oluşturun (`BMusteri.cs`'yi referans alarak):
- `HesapAc()` - IBAN üretimi ile hesap açma
- `HesapGetir()` - Hesap detayları
- `MusterininHesaplari()` - Müşteriye ait tüm hesaplar
- `BakiyeGuncelle()` - Bakiye güncelleme

**BIslem.cs** oluşturun:
- `ParaYatir()` - Para yatırma işlemi
- `ParaCek()` - Para çekme işlemi (bakiye kontrolü)
- `Havale()` - Havale işlemi (IBAN doğrulama, limit kontrolü)
- `EFT()` - EFT işlemi (işlem ücreti hesaplama)
- `Virman()` - Virman işlemi

**BAuth.cs** oluşturun:
- `Login()` - Kullanıcı doğrulama, başarısız giriş sayacı
- `Logout()` - Çıkış işlemi
- `SifreDegistir()` - Şifre değiştirme
- `HesapKilidiAc()` - Hesap kilidini açma

**BOnay.cs** oluşturun:
- `OnayTalebiOlustur()` - Yeni onay talebi
- `IslemOnayla()` - İşlemi onaylama
- `IslemReddet()` - İşlemi reddetme
- `OnayBekleyenler()` - Onay bekleyen listesi

**BLog.cs** oluşturun:
- `IslemLoguKaydet()` - İşlem logu kaydetme
- `LoginLoguKaydet()` - Login/Logout logu
- `GuvenlikLoguKaydet()` - Güvenlik logu

### B. Service Layer (Yüksek Öncelik)

Service sınıfları, Business katmanını wrap eder ve ek validasyon/loglama sağlar.

**SMusteri.cs**, **SHesap.cs**, **SIslem.cs** oluşturun (`SAuth.cs`'yi referans alarak).

Her servis metodu:
1. Giriş parametrelerini validate eder
2. Business katmanını çağırır
3. Sonucu loglar
4. Hata mesajını kullanıcı dostu hale getirir

### C. Windows Forms (Orta Öncelik)

**FrmAnaSayfa.cs** - Ana dashboard formu:
- Kullanıcı bilgileri
- Hızlı erişim butonları (rol bazlı)
- İstatistikler
- Bekleyen onaylar (varsa)

**FrmMusteriIslem.cs** - Müşteri CRUD formu:
- Yeni müşteri ekleme
- Müşteri arama
- Müşteri güncelleme
- DevExpress GridControl kullanımı

**FrmHesapIslem.cs** - Hesap işlemleri formu:
- Yeni hesap açma
- IBAN gösterimi
- Hesap listesi
- Hesap detayları

**FrmParaYatir.cs / FrmParaCek.cs** - Para işlemleri:
- Tutar girişi
- Açıklama
- Onay/İptal
- Makbuz yazdırma (opsiyonel)

**FrmHavaleEFT.cs** - Havale/EFT formu:
- Kaynak hesap seçimi
- Hedef IBAN girişi
- IBAN doğrulama
- Tutar ve açıklama
- Onay mekanizması entegrasyonu

**FrmOnayBekleyenler.cs** - Onay listesi formu:
- Rol bazlı onay bekleyen işlemler
- Detay görüntüleme
- Onaylama/Reddetme

### D. Web API (Orta Öncelik)

**Program.cs / Startup.cs** oluşturun:
```csharp
// JWT Authentication
// CORS yapılandırması
// Swagger entegrasyonu
// Middleware pipeline
```

**Controllers/AuthController.cs**:
```csharp
[POST] /api/Auth/Login
[POST] /api/Auth/MusteriLogin
[POST] /api/Auth/Logout
[POST] /api/Auth/SifreDegistir
```

**Controllers/HesapController.cs**:
```csharp
[GET] /api/Hesap/Bakiye/{hesapID}
[GET] /api/Hesap/Hesaplar (müşteriye ait)
[GET] /api/Hesap/Ekstre/{hesapID}
```

**Controllers/IslemController.cs**:
```csharp
[POST] /api/Islem/Havale
[POST] /api/Islem/EFT
[POST] /api/Islem/Virman
[GET] /api/Islem/Gecmis/{hesapID}
```

**Middleware/JwtMiddleware.cs**:
- Token doğrulama
- Kullanıcı bilgilerini HttpContext'e ekleme

**Middleware/LogMiddleware.cs**:
- Tüm istekleri loglama
- Response süresini ölçme

### E. Web Portal (Düşük Öncelik)

**dashboard.html** - Ana sayfa:
- Hesap özeti
- Bakiyeler
- Son işlemler
- Hızlı havale

**assets/css/style.css** - Stil dosyası

**assets/js/app.js** - JavaScript utility fonksiyonlar:
- API çağrıları
- Token yönetimi
- IBAN formatlama

## 🎯 Geliştirme Öncelikleri

### Aşama 1: Temel İşlevsellik (1-2 Hafta)
1. ✅ Veritabanı ve şifre hash'lerini güncelle
2. ✅ BAuth ve SAuth'u tamamla
3. ⚠️ BHesap ve SHesap'ı tamamla
4. ⚠️ BIslem ve SIslem'i tamamla (en az Havale)
5. ⚠️ FrmAnaSayfa'yı oluştur
6. ⚠️ Temel para işlemlerini test et

### Aşama 2: Onay Mekanizması (1 Hafta)
1. BOnay ve SOnay'ı tamamla
2. FrmOnayBekleyenler'i oluştur
3. Onay iş akışını test et

### Aşama 3: Web API (1 Hafta)
1. Web API projesini oluştur
2. JWT Authentication'ı implement et
3. En az 3 controller oluştur
4. Swagger ile test et

### Aşama 4: İleri Özellikler (1-2 Hafta)
1. Banka kartı işlemleri
2. Raporlama
3. Web portal
4. Güvenlik testleri

## 🧪 Test Senaryoları

### Test 1: Müşteri ve Hesap Açma
```
1. FrmGiris ile giriş yap (merkez.calisan1)
2. FrmMusteriIslem'i aç
3. Yeni müşteri ekle
   - Ad: Test
   - Soyad: Müşteri
   - TCKN: 12345678901
4. Hesap aç butonu
5. TL Vadesiz Hesap seç
6. IBAN'ın otomatik oluştuğunu kontrol et
7. Müdür onayı bekle
```

### Test 2: Para Yatırma ve Havale
```
1. Hesaba 10.000 TL yatır
2. 3.000 TL havale yap (direkt işlem, onaysız)
3. 7.000 TL havale yap (müdür onayı gerekir)
4. FrmOnayBekleyenler'de 7.000 TL'lik işlemi gör
5. Müdür olarak giriş yap
6. İşlemi onayla
7. Bakiyeyi kontrol et (0 TL olmalı)
```

### Test 3: IBAN Doğrulama
```csharp
// Unit Test
[TestMethod]
public void TestIbanGeneration()
{
    string iban = IbanHelper.GenerateIban("00001", "0000000000000001");
    Assert.IsNotNull(iban);
    Assert.AreEqual(26, iban.Replace(" ", "").Length);
    
    string hata = IbanHelper.ValidateIban(iban);
    Assert.IsNull(hata); // IBAN geçerli olmalı
}
```

## 📚 Referanslar ve Örnekler

### Business Layer Metod Şablonu
```csharp
public string MetodAdi(ParametreTipi parametre, out SonucTipi sonuc)
{
    sonuc = defaultDeger;

    try
    {
        // 1. Validasyon
        string hata = ValidationHelper.Validate(...);
        if (hata != null) return hata;

        // 2. Business Logic
        // ...

        // 3. Database İşlemi
        string query = "SELECT ...";
        MySqlParameter[] parameters = new MySqlParameter[] { ... };
        
        DataTable dt;
        hata = _dataAccess.ExecuteQuery(query, parameters, out dt);
        if (hata != null) return hata;

        // 4. Sonuç Dönüşümü
        sonuc = ...;

        return null; // Başarılı
    }
    catch (Exception ex)
    {
        return $"Hata: {ex.Message}";
    }
    finally
    {
        _dataAccess.CloseConnection();
    }
}
```

### Service Layer Metod Şablonu
```csharp
public string MetodAdi(ParametreTipi parametre, out SonucTipi sonuc)
{
    sonuc = defaultDeger;

    try
    {
        // 1. Ek Validasyon (varsa)
        
        // 2. Business Katmanını Çağır
        string hata = _business.MetodAdi(parametre, out sonuc);
        
        // 3. Log Kaydet
        _sLog.IslemLoguKaydet(...);
        
        // 4. Sonucu Döndür
        return hata;
    }
    catch (Exception ex)
    {
        return $"Servis hatası: {ex.Message}";
    }
}
```

### Windows Form Event Handler Şablonu
```csharp
private void BtnKaydet_Click(object sender, EventArgs e)
{
    try
    {
        // 1. Form Validasyonu
        if (string.IsNullOrWhiteSpace(txtAd.Text))
        {
            MessageBox.Show("Ad boş olamaz.", "Uyarı", ...);
            return;
        }

        // 2. Model Oluştur
        var model = new Model
        {
            Ad = txtAd.Text,
            ...
        };

        // 3. Servisi Çağır
        SonucTipi sonuc;
        string hata = _service.MetodAdi(model, out sonuc);

        if (hata != null)
        {
            MessageBox.Show(hata, "Hata", ...);
            return;
        }

        // 4. Başarı Mesajı
        MessageBox.Show("İşlem başarılı!", "Başarılı", ...);
        
        // 5. Formu Temizle/Kapat
        this.Close();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", ...);
    }
}
```

## 💡 İpuçları

1. **Kodlama Standartları**: README.md'deki kodlama standartlarına uyun
2. **Try-Catch**: Her metodda try-catch kullanın
3. **Loglama**: Önemli işlemleri mutlaka loglayın
4. **Validasyon**: Hem client-side hem server-side validasyon yapın
5. **Transaction**: Para işlemlerinde mutlaka transaction kullanın
6. **Test**: Her özelliği geliştirdikçe test edin
7. **Commit**: Düzenli commit atın (her özellik için)

## 🐛 Sık Karşılaşılan Hatalar ve Çözümleri

### Hata: "MySql.Data assembly not found"
**Çözüm**: NuGet'ten MySql.Data paketini yükleyin

### Hata: "DevExpress license required"
**Çözüm**: DevExpress trial lisansı indirin veya standard Windows Forms kontrolleri kullanın

### Hata: "Unable to connect to MySQL server"
**Çözüm**: 
- MySQL servisinin çalıştığından emin olun
- Bağlantı stringindeki şifreyi kontrol edin
- Firewall ayarlarını kontrol edin

### Hata: "IBAN validation failed"
**Çözüm**: IbanHelper'daki Mod 97 algoritmasını kontrol edin

## 📞 Destek

Sorularınız için:
- GitHub Issues açın
- Proje dokümantasyonunu okuyun
- Code review isteyin

---

**Not**: Bu proje eğitim amaçlıdır. Production ortamı için ek güvenlik önlemleri alınmalıdır.

