# MetinBank Web API

## 🚀 Başlangıç

### Gereksinimler
- .NET 8.0 SDK veya üzeri
- MySQL Server 8.0 veya üzeri
- Visual Studio 2022 veya VS Code

### Kurulum

1. **Veritabanını Oluşturun**
```sql
-- Database/MetinBank_Schema.sql dosyasını çalıştırın
mysql -u root -p < Database/MetinBank_Schema.sql
```

2. **Connection String'i Güncelleyin**
`appsettings.json` dosyasında veritabanı bağlantı bilgilerini güncelleyin:
```json
"ConnectionStrings": {
  "MetinBankDB": "Server=localhost;Database=MetinBankDB;Uid=root;Pwd=yourpassword;"
}
```

3. **Uygulamayı Çalıştırın**
```bash
cd MetinBank.WebAPI
dotnet restore
dotnet run
```

API şu adreste çalışmaya başlayacak:
- **API**: http://localhost:5000
- **Swagger**: http://localhost:5000/swagger

## 📚 API Endpoints

### 🔐 Authentication (api/auth)

#### Müşteri Girişi
```http
POST /api/auth/musteri-login
Content-Type: application/json

{
  "musteriNo": "M123456" veya "",
  "tckn": 12345678901 veya 0,
  "sifre": "123456"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Giriş başarılı",
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "kullanici": {
    "musteriID": 1,
    "musteriNo": "M123456",
    "ad": "Ahmet",
    "soyad": "Yılmaz",
    "email": "ahmet@example.com",
    "telefon": "05551234567"
  }
}
```

#### Müşteri Doğrulama (Şifre Sıfırlama İçin)
```http
POST /api/auth/musteri-dogrula
Content-Type: application/json

{
  "tckNVeyaMusteriNo": "12345678901",
  "dogumTarihi": "1990-01-15",
  "anneAdi": "Ayşe",
  "cepTelefon": "05551234567"
}
```

#### Şifre Sıfırlama
```http
POST /api/auth/sifre-sifirla
Content-Type: application/json

{
  "musteriID": 1,
  "yeniSifre": "yeni123",
  "yeniSifreTekrar": "yeni123"
}
```

#### Şifre Değiştirme
```http
POST /api/auth/sifre-degistir
Authorization: Bearer {token}
Content-Type: application/json

{
  "musteriID": 1,
  "eskiSifre": "eski123",
  "yeniSifre": "yeni456"
}
```

#### Profil Bilgisi
```http
GET /api/auth/profil/{musteriID}
Authorization: Bearer {token}
```

---

### 🏦 Hesap İşlemleri (api/hesap)

#### Müşterinin Tüm Hesapları
```http
GET /api/hesap/musteri/{musteriID}
Authorization: Bearer {token}
```

**Response:**
```json
{
  "success": true,
  "message": "Hesaplar getirildi",
  "data": [
    {
      "hesapID": 1,
      "hesapNo": 1001234567,
      "iban": "TR330006100519786123456789",
      "hesapTipi": "TL",
      "hesapCinsi": "VADESIZ",
      "bakiye": 15000.00,
      "kullanilabilirBakiye": 14500.00,
      "durum": "Aktif",
      "acilisTarihi": "2024-01-15T00:00:00"
    }
  ]
}
```

#### Tek Hesap Detayı
```http
GET /api/hesap/{hesapID}
Authorization: Bearer {token}
```

#### Hesap Bakiyesi
```http
GET /api/hesap/{hesapID}/bakiye
Authorization: Bearer {token}
```

#### IBAN Sorgulama
```http
POST /api/hesap/iban-sorgula
Content-Type: application/json

{
  "iban": "TR330006100519786123456789"
}
```

**Response:**
```json
{
  "success": true,
  "message": "IBAN bulundu",
  "musteriAdi": "Ahmet Yılmaz",
  "bankaAdi": "MetinBank"
}
```

#### Yeni Hesap Açma
```http
POST /api/hesap/hesap-ac
Authorization: Bearer {token}
Content-Type: application/json

{
  "musteriID": 1,
  "hesapTipi": "TL",
  "subeID": 1
}
```

---

### 💸 İşlem (api/islem)

#### Havale
```http
POST /api/islem/havale?kullaniciID=1&subeID=1
Authorization: Bearer {token}
Content-Type: application/json

{
  "kaynakHesapID": 1,
  "hedefIBAN": "TR330006100519786123456789",
  "tutar": 1000.00,
  "aciklama": "Test havalesi",
  "aliciAdi": "Mehmet Demir"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Havale işlemi başarılı",
  "islemID": 12345,
  "islemReferansNo": "HVL2026010612345"
}
```

#### EFT
```http
POST /api/islem/eft?kullaniciID=1&subeID=1
Authorization: Bearer {token}
Content-Type: application/json

{
  "kaynakHesapID": 1,
  "hedefIBAN": "TR110001000123456789012345",
  "tutar": 500.00,
  "aciklama": "EFT işlemi",
  "aliciAdi": "Ali Kaya"
}
```

#### Virman
```http
POST /api/islem/virman?kullaniciID=1&subeID=1
Authorization: Bearer {token}
Content-Type: application/json

{
  "kaynakHesapID": 1,
  "hedefHesapID": 2,
  "tutar": 300.00,
  "aciklama": "Hesaplar arası virman"
}
```

#### Müşteri İşlem Geçmişi
```http
GET /api/islem/musteri/{musteriID}/gecmis
Authorization: Bearer {token}
```

#### Hesap Ekstresi
```http
GET /api/islem/hesap/{hesapID}/ekstre
Authorization: Bearer {token}
```

Tarih filtreli:
```http
GET /api/islem/hesap/{hesapID}/ekstre?baslangicTarihi=2024-01-01&bitisTarihi=2024-01-31
```

---

### 💳 Kart İşlemleri (api/kart)

#### Müşterinin Kartları
```http
GET /api/kart/musteri/{musteriID}
Authorization: Bearer {token}
```

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "kartID": 1,
      "kartNo": 9876543210123456,
      "kartTipi": "Banka Kartı",
      "kartSahibiAdi": "AHMET YILMAZ",
      "sonKullanimTarihi": "2028-12-31",
      "durum": "Aktif"
    }
  ]
}
```

---

### 💱 Döviz İşlemleri (api/doviz)

#### Güncel Kurlar
```http
GET /api/doviz/kurlar
```

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "paraBirimi": "USD",
      "alisFiyati": 32.5000,
      "satisFiyati": 32.7500,
      "guncellemeTarihi": "2024-01-06T10:00:00"
    },
    {
      "paraBirimi": "EUR",
      "alisFiyati": 35.2000,
      "satisFiyati": 35.5000,
      "guncellemeTarihi": "2024-01-06T10:00:00"
    }
  ]
}
```

#### Belirli Döviz Kuru
```http
GET /api/doviz/kur/USD
```

---

### 💰 Kredi İşlemleri (api/kredi)

#### Kredi Faiz Oranları
```http
GET /api/kredi/oranlar
```

#### Kredi Hesaplama
```http
POST /api/kredi/hesapla
Content-Type: application/json

{
  "tutar": 100000,
  "vade": 36
}
```

#### Kredi Başvurusu
```http
POST /api/kredi/basvuru
Authorization: Bearer {token}
Content-Type: application/json

{
  "musteriID": 1,
  "krediTuru": "Ihtiyac",
  "tutar": 50000,
  "vade": 24,
  "aylikGelir": 15000,
  "aciklama": "İhtiyaç kredisi"
}
```

---

### 💵 İşlem Ücreti (api/islemUcreti)

#### Ücret Hesaplama
```http
GET /api/islemUcreti/hesapla?islemTipi=Havale&islemKanali=Internet&tutar=1000
```

**Response:**
```json
{
  "success": true,
  "data": {
    "ucret": 5.50,
    "islemTipi": "Havale",
    "islemKanali": "Internet"
  }
}
```

---

## 🔒 Authorization

Çoğu endpoint JWT token gerektirir. Token'ı header'da göndermeniz gerekir:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Token, login işleminden sonra döner ve 60 dakika geçerlidir.

## ⚙️ Yapılandırma

### appsettings.json

```json
{
  "ConnectionStrings": {
    "MetinBankDB": "Server=localhost;Database=MetinBankDB;Uid=root;Pwd=;"
  },
  "JwtSettings": {
    "SecretKey": "MetinBank2024SecretKeyForJWTTokenGeneration!@#$%",
    "Issuer": "MetinBankAPI",
    "Audience": "MetinBankClients",
    "ExpiryMinutes": 60
  },
  "TransactionLimits": {
    "DailyTransferLimit": 20000,
    "MonthlyTransferLimit": 500000,
    "DailyWithdrawalLimit": 50000
  }
}
```

## 🧪 Test

### Swagger UI Kullanarak Test
1. http://localhost:5000/swagger adresine gidin
2. Endpoint'i seçin
3. "Try it out" butonuna tıklayın
4. Request body'yi doldurun
5. "Execute" butonuna tıklayın

### Postman/Insomnia Kullanarak Test
1. Collection import edin
2. Environment değişkenlerini ayarlayın:
   - `baseUrl`: http://localhost:5000
   - `token`: Login'den dönen token
3. Request'leri çalıştırın

### cURL Kullanarak Test

**Login:**
```bash
curl -X POST http://localhost:5000/api/auth/musteri-login \
  -H "Content-Type: application/json" \
  -d '{
    "musteriNo": "M123456",
    "tckn": 0,
    "sifre": "123456"
  }'
```

**Hesapları Getir:**
```bash
curl -X GET http://localhost:5000/api/hesap/musteri/1 \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## 🐛 Sorun Giderme

### Port zaten kullanılıyor
```bash
# Farklı bir port kullanın
dotnet run --urls="http://localhost:5001"
```

### Veritabanına bağlanılamıyor
1. MySQL Server'ın çalıştığından emin olun
2. Connection string'i kontrol edin
3. Veritabanının oluşturulduğundan emin olun

### CORS Hatası
`appsettings.json` dosyasında allowed origins'e uygulamanızın URL'ini ekleyin.

## 📊 Response Formatı

Tüm API response'ları aşağıdaki formatta döner:

**Başarılı:**
```json
{
  "success": true,
  "message": "İşlem başarılı",
  "data": { ... }
}
```

**Hata:**
```json
{
  "success": false,
  "message": "Hata mesajı",
  "data": null
}
```

## 🔐 Güvenlik

- ✅ JWT Token Authentication
- ✅ Password Hashing (BCrypt)
- ✅ CORS Policy
- ✅ HTTPS (Production)
- ✅ SQL Injection Protection
- ✅ Rate Limiting
- ✅ Request Validation

## 📝 Notlar

- Token süresi: 60 dakika
- Günlük transfer limiti: 20,000 TL
- Aylık transfer limiti: 500,000 TL
- EFT ücreti: 5.00 TL
- Havale ücreti: 2.50 TL

## 📞 Destek

Sorun yaşarsanız veya sorunuz varsa:
- Issue açın: GitHub Issues
- E-posta: support@metinbank.com.tr

## 📄 Lisans

© 2026 MetinBank A.Ş. Tüm hakları saklıdır.
