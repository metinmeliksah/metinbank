# METİN BANK - Kapsamlı Bankacılık Uygulaması

## Proje Özeti

Metin Bank, şube işlemleri, genel merkez denetimi ve müşteri web portalı içeren kapsamlı bir bankacılık uygulamasıdır. 
Proje, OOP prensipleri ile C# .NET platformunda geliştirilmiş, çok katmanlı mimari yapıda tasarlanmıştır.

## Teknoloji Stack

- **Backend**: C# .NET Framework 4.8 (Windows Forms - DevExpress), .NET Core 6.0 Web API
- **Frontend**: DevExpress (Windows), HTML/CSS/JS (Web Portal)
- **Veritabanı**: MySQL 8.0
- **Güvenlik**: JWT Token, SHA256 Hash, AES-256 Encryption
- **ORM**: ADO.NET (Manual Data Access Layer)

## Proje Yapısı

```
MetinBank/
├── Database/
│   └── MetinBank_Schema.sql          # Veritabanı şeması ve initial data
├── MetinBank.Util/                    # Utility Layer
│   ├── DataAccess.cs                  # MySQL bağlantı yönetimi
│   ├── SecurityHelper.cs              # SHA256, AES şifreleme
│   ├── IbanHelper.cs                  # IBAN üretim ve doğrulama
│   ├── ValidationHelper.cs            # Veri doğrulama
│   └── CommonFunctions.cs             # Ortak fonksiyonlar
├── MetinBank.Models/                  # Model Layer
│   ├── KullaniciModel.cs
│   ├── MusteriModel.cs
│   ├── HesapModel.cs
│   ├── IslemModel.cs
│   ├── BankaKartiModel.cs
│   ├── SubeModel.cs
│   ├── OnayModel.cs
│   ├── LogModel.cs
│   ├── BildirimModel.cs
│   └── DovizKurModel.cs
├── MetinBank.Interface/               # Interface Layer
│   ├── IMusteri.cs
│   ├── IHesap.cs
│   ├── IIslem.cs
│   ├── IAuth.cs
│   ├── IOnay.cs
│   └── ILog.cs
├── MetinBank.Business/                # Business Logic Layer
│   ├── BMusteri.cs
│   ├── BHesap.cs
│   ├── BIslem.cs
│   ├── BAuth.cs
│   ├── BOnay.cs
│   └── BLog.cs
├── MetinBank.Service/                 # Service Layer
│   ├── SMusteri.cs
│   ├── SHesap.cs
│   ├── SIslem.cs
│   ├── SAuth.cs
│   ├── SOnay.cs
│   ├── SIban.cs
│   └── SLog.cs
├── MetinBank.Forms/                   # Windows Forms Application
│   ├── FrmGiris.cs
│   ├── FrmAnaSayfa.cs
│   ├── FrmMusteriIslem.cs
│   ├── FrmHesapIslem.cs
│   ├── FrmParaYatir.cs
│   ├── FrmParaCek.cs
│   ├── FrmHavaleEFT.cs
│   └── ... (diğer formlar)
├── MetinBank.WebAPI/                  # Web API (.NET Core 6.0)
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── MusteriController.cs
│   │   ├── HesapController.cs
│   │   └── IslemController.cs
│   └── Middleware/
│       ├── JwtMiddleware.cs
│       └── LogMiddleware.cs
└── MetinBank.Web/                     # Web Portal (Müşteri)
    ├── index.html
    ├── login.html
    ├── dashboard.html
    └── assets/
        ├── css/
        ├── js/
        └── img/
```

## Veritabanı Kurulumu

### 1. MySQL 8.0 Kurulumu

Windows için MySQL 8.0 indirin ve kurun: https://dev.mysql.com/downloads/mysql/

### 2. Veritabanı Oluşturma

```bash
# MySQL'e bağlanın
mysql -u root -p

# Scripti çalıştırın
source Database/MetinBank_Schema.sql

# veya MySQL Workbench ile MetinBank_Schema.sql dosyasını çalıştırın
```

### 3. Bağlantı Yapılandırması

`App.config` dosyasında bağlantı stringini güncelleyin:

```xml
<connectionStrings>
  <add name="MetinBankDB" 
       connectionString="Server=localhost;Database=MetinBankDB;Uid=root;Pwd=your_password;CharSet=utf8mb4;" 
       providerName="MySql.Data.MySqlClient"/>
</connectionStrings>
```

## Kullanıcı Rolleri ve Varsayılan Kullanıcılar

### Genel Merkez
- **Kullanıcı Adı**: `gm.admin`
- **Şifre**: `Password123!`
- **Yetkiler**: Tüm sistem yetkisi

### Şube Müdürü
- **Kullanıcı Adı**: `merkez.mudur`
- **Şifre**: `Password123!`
- **Yetkiler**: Şube yönetimi, orta seviye onaylar

### Şube Çalışanı
- **Kullanıcı Adı**: `merkez.calisan1`
- **Şifre**: `Password123!`
- **Yetkiler**: Hesap işlemleri, para işlemleri

**NOT**: Tüm şifreler ilk kurulumda `TEMP_HASH` ve `TEMP_SALT` değerleri ile oluşturulmuştur. 
İlk çalıştırmadan önce, gerçek hash değerleri ile güncellenmelidir.

## Özellikler

### 1. Hesap İşlemleri
- ✅ Hesap Açma (TL, USD, EUR)
- ✅ Hesap Kapatma/Pasif Etme
- ✅ IBAN Otomatik Üretimi (Mod 97 Algoritması)
- ✅ Hesap Türleri: Vadesiz, Vadeli, Çocuk Tasarruf, Maaş, Yatırım

### 2. Para İşlemleri
- ✅ Para Yatırma (Nakit, Transfer)
- ✅ Para Çekme (Günlük limit: 50.000 TL)
- ✅ Havale (IBAN ile, ücretsiz)
- ✅ EFT (Farklı banka, 5 TL ücret)
- ✅ Virman (Kendi hesapları arası, ücretsiz)
- ✅ Döviz İşlemleri (Alış/Satış)

### 3. Onay Mekanizması
- ✅ Tutar Bazlı Onay Seviyeleri
  - 0-5.000 TL: Çalışan (Direkt)
  - 5.001-10.000 TL: Müdür Onayı
  - 10.001 TL üzeri: Genel Merkez Onayı
- ✅ Onay Süreci İş Akışı
- ✅ Otomatik Bildirimler

### 4. Banka Kartı İşlemleri
- ✅ Kart Başvurusu
- ✅ Kart Aktivasyon
- ✅ Kart Bloke/Bloke Kaldırma
- ✅ Kayıp/Çalıntı Bildirimi
- ✅ Kart Limiti Belirleme

### 5. Güvenlik
- ✅ SHA256 + Salt ile Şifre Hash'leme
- ✅ AES-256 ile CVV Şifreleme
- ✅ JWT Token Authenticat ion (Web API)
- ✅ IP ve MAC Adresi Takibi
- ✅ 3 Başarısız Giriş = 30 Dakika Kilitleme
- ✅ 5 Başarısız Giriş = Tam Kilitleme

### 6. Log Yönetimi
- ✅ İşlem Logları (7 yıl saklama)
- ✅ Görüntüleme Logları
- ✅ Sistem Logları
- ✅ Güvenlik Logları
- ✅ Login/Logout Logları

### 7. Raporlama
- ✅ Hesap Ekstres i (PDF, Excel)
- ✅ Günlük İşlem Raporları
- ✅ Şube Performans Raporları
- ✅ Müşteri İstatistikleri

## IBAN Üretim Sistemi

### IBAN Yapısı (26 Karakter)
```
TR33 0001 0012 3456 7890 1234 56
│ │  │    │    │                │
│ │  │    │    │                └─ Hesap No (son 6 hane)
│ │  │    │    └────────────────── Hesap No (16 hane)
│ │  │    └─────────────────────── Şube Kodu (5 hane)
│ │  └──────────────────────────── Banka Kodu + Rezerv (6 hane)
│ └─────────────────────────────── Kontrol Rakamı (2 hane)
└───────────────────────────────── Ülke Kodu (TR)
```

### Kontrol Rakamı Hesaplama (Mod 97)
1. IBAN'ı yeniden düzenle: Banka+Rezerv+Şube+Hesap+TR(2734)+00
2. Harfleri sayıya çevir (A=10, B=11, ... Z=35)
3. Mod 97 işlemi uygula
4. Kontrol Rakamı = 98 - (Mod 97 sonucu)

### Örnek Kod
```csharp
string iban = IbanHelper.GenerateIban("00001", "0000000000000001");
// Sonuç: TR33 0001 0000 0100 0000 0000 0001

string hata = IbanHelper.ValidateIban("TR33 0001 0000 0100 0000 0000 0001");
// hata == null ise IBAN geçerli
```

## Güvenlik Özellikleri

### Şifre Politikası
- En az 8 karakter
- En az 1 büyük harf
- En az 1 küçük harf
- En az 1 rakam
- En az 1 özel karakter
- 90 günde bir şifre değişimi zorunlu
- Son 5 şifre tekrar kullanılamaz

### Şifreleme
```csharp
// Şifre Hash'leme
string salt = SecurityHelper.GenerateSalt();
string hashedPassword = SecurityHelper.HashPassword("Password123!", salt);

// AES Şifreleme (CVV için)
string cvvEncrypted = SecurityHelper.EncryptAES("123");
string cvvDecrypted = SecurityHelper.DecryptAES(cvvEncrypted);
```

## API Kullanımı

### Base URL
```
http://localhost:5000/api
```

### Authentication
```http
POST /api/Auth/Login
Content-Type: application/json

{
  "kullaniciAdi": "merkez.calisan1",
  "sifre": "Password123!"
}

Response:
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "kullanici": { ... },
  "expiresAt": "2024-01-01T12:00:00Z"
}
```

### Hesap Bakiye Sorgulama
```http
GET /api/Hesap/Bakiye/{hesapID}
Authorization: Bearer {token}

Response:
{
  "hesapID": 1,
  "iban": "TR33 0001 0000 0100 0000 0000 0001",
  "bakiye": 10000.50,
  "kullanilabilirBakiye": 9500.50,
  "blokeBakiye": 500.00
}
```

### Havale İşlemi
```http
POST /api/Islem/Havale
Authorization: Bearer {token}
Content-Type: application/json

{
  "kaynakHesapID": 1,
  "hedefIBAN": "TR44 0001 0000 0200 0000 0000 0002",
  "tutar": 1000.00,
  "aciklama": "Kira ödemesi",
  "aliciAdi": "Ahmet Yılmaz"
}

Response:
{
  "basarili": true,
  "islemReferansNo": "TRX20240101120000001",
  "onayGerekiyor": false,
  "mesaj": "İşlem başarıyla tamamlandı."
}
```

## Geliştirme Notları

### Kodlama Standartları
- ✅ Tüm class'lar ve metodlar `/// summary` açıklamalı
- ✅ Class isimleri ile dosya isimleri aynı
- ✅ Private değişkenler `_` ile başlıyor
- ✅ PascalCase ve camelCase kuralları
- ✅ Service class'ları `S` ile başlıyor
- ✅ Business class'ları `B` ile başlıyor
- ✅ Interface'ler `I` ile başlıyor
- ✅ Try-catch blokları eksiksiz
- ✅ Formlardan SQL çağrısı YOK

### Form Standartları
- Font: Tahoma 8.25pt
- Form boyutu: Maksimum 770x700
- AutoScroll: Aktif
- Readonly alanlar: LightYellow arka plan
- Zorunlu alanlar: `*` işareti ile belirtili
- Kontrol isimlendirme: btn, txt, lbl, cmb prefix

### Performans
- Sorgu süreleri: < 2 saniye
- İşlem kayıt: < 3 saniye
- Index kullanımı (sık sorgulanan alanlarda)
- Transaction yönetimi
- Connection pooling

## Test Kullanıcıları ve Senaryolar

### Senaryo 1: Yeni Müşteri ve Hesap Açma
1. `merkez.calisan1` ile giriş yap
2. Yeni müşteri ekle (TCKN: 12345678901)
3. Müşteriye TL Vadesiz Hesap aç
4. IBAN otomatik oluşturulur
5. Müdür onayı bekle

### Senaryo 2: Para Yatırma ve Havale
1. Müşteri hesabına 10.000 TL yatır
2. 3.000 TL havale yap (direkt işlem)
3. 7.000 TL havale yap (müdür onayı gerekir)
4. Onay bekleyen işlemleri görüntüle

### Senaryo 3: Banka Kartı Başvurusu
1. Müşteri için kart başvurusu oluştur
2. Kart bilgileri (CVV şifreli) saklanır
3. 3-5 iş günü sonra kart aktif edilir
4. Günlük/aylık limitler belirlenir

## Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## İletişim

Proje Sahibi: Metin Bank Development Team
Email: dev@metinbank.com.tr

---

**NOT**: Bu proje kapsamlı bir bankacılık uygulaması örneğidir. Gerçek bir production ortamında 
kullanmadan önce ek güvenlik testleri, penetrasyon testleri ve code review yapılmalıdır.

