# MetinBank - Banka Otomasyonu Sistemi

**Versiyon:** 1.0  
**Hazırlayan:** Metin Melikşah Dermencioğlu  
**Tarih:** 28 Ekim 2025

## 📋 Proje Hakkında

MetinBank, modern bankacılık ihtiyaçlarını karşılamak üzere tasarlanmış kapsamlı bir banka otomasyon sistemidir. Bireysel ve kurumsal müşteriler için eksiksiz bankacılık hizmetleri sunar.

## 🏗️ Sistem Mimarisi

### Teknoloji Stack

#### Backend
- **Framework:** .NET 8 Web API
- **Ana Veritabanı:** Oracle XE (İşlemsel veriler)
- **Log/Analitik DB:** PostgreSQL
- **Mesaj Kuyruğu:** RabbitMQ / Kafka
- **Cache:** Redis (Oturum yönetimi)
- **Analitik Servis:** Python (Flask/FastAPI)

#### Frontend
- **Web Şube:** React.js / Angular
- **Mobil Uygulama:** React Native / Flutter (Android & iOS)
- **Şube/ATM:** Windows Forms (.NET)

#### Güvenlik
- HTTPS (TLS 1.3)
- JWT Token Based Authentication
- OAuth2 Authorization
- 2FA (SMS OTP & Mobile Push)
- AES-256 Şifreleme
- HSM Token Integration (Kart güvenliği)

## 📁 Proje Yapısı

```
metinbank/
├── src/
│   ├── Backend/              # .NET 8 Web API
│   │   ├── MetinBank.API/
│   │   ├── MetinBank.Core/
│   │   ├── MetinBank.Infrastructure/
│   │   └── MetinBank.Services/
│   ├── Frontend/             # Web Uygulaması (React/Angular)
│   ├── Mobile/               # Mobil Uygulama (React Native/Flutter)
│   ├── Desktop/              # Windows Forms (Şube & ATM)
│   └── Python/               # Analytics & Risk Service
├── database/                 # Database Scripts
│   ├── oracle/
│   └── postgresql/
├── docs/                     # Dokümantasyon
├── scripts/                  # Deployment & Utility Scripts
└── tests/                    # Test Projeleri
```

## 🎯 Özellikler

### Bireysel Bankacılık
- ✅ **eKYC (Elektronik Müşteri Tanıma)**
  - NFC ile kimlik okuma
  - OCR ile belge tarama
  - Canlılık testi (Liveness)
  
- ✅ **Hesap Yönetimi**
  - Vadesiz, Vadeli, Döviz hesapları
  - Kredili Mevduat Hesabı (KMH)
  
- ✅ **Kart İşlemleri**
  - Banka Kartı (Debit)
  - Kredi Kartı (Credit)
  - Sanal Kart
  
- ✅ **Para Transferleri**
  - Havale (Anlık)
  - EFT (Simülasyon)
  - QR ile transfer
  
- ✅ **Ödemeler**
  - Fatura ödeme
  - Vergi/SGK ödemeleri
  - Otomatik ödeme talimatı
  
- ✅ **Yatırım Ürünleri**
  - Yatırım fonları
  - Hisse senedi
  - Kıymetli maden (Altın/Gümüş)
  
- ✅ **Krediler**
  - İhtiyaç kredisi
  - Konut kredisi
  - Otomatik kredi skoru

### Kurumsal Bankacılık
- ✅ **Kullanıcı Yönetimi**
  - Firma yöneticisi rolü
  - Hazırlayıcı/Onaylayıcı rolleri
  - Yetki matrisi yönetimi
  
- ✅ **Toplu Ödemeler**
  - Maaş ödemeleri
  - Tedarikçi ödemeleri
  - Excel/CSV import
  
- ✅ **Ticari Krediler**
  - İşletme kredisi
  - Makine/Ekipman kredisi
  
- ✅ **Çek/Senet İşlemleri**
  - Çek karnesiyönetimi
  - Senet takibi
  
- ✅ **POS & Üye İşyeri**
  - POS raporlama
  - Mutabakat
  
- ✅ **Dış Ticaret**
  - Teminat Mektubu (L/G)
  - Akreditif (L/C)

### Diğer Özellikler
- ✅ **ATM Simülasyonu**
  - Para çekme/yatırma
  - QR ile kartsız işlem
  - Fatura ödeme
  
- ✅ **Chatbot**
  - NLP tabanlı müşteri asistanı
  - Bakiye sorgulama
  - İşlem başlatma
  
- ✅ **Bildirim Sistemi**
  - Mobile Push (FCM/APNS)
  - SMS (OTP & Uyarılar)
  - E-posta (Dekont & Ekstre)

## 🔐 Güvenlik Özellikleri

### Kimlik Doğrulama
- OAuth2 + JWT Token
- 2FA (İki Faktörlü Doğrulama)
- Cihaz kayıt mekanizması
- Biyometrik giriş desteği

### Veri Güvenliği
- AES-256 şifreleme
- PBKDF2/bcrypt hash
- Kart tokenizasyonu
- HSM entegrasyonu

### İşlem Güvenliği
- Anlık risk analizi (Python ML)
- Çok katmanlı onay mekanizması
- Hiyerarşik yetkilendirme
- RBAC (Role-Based Access Control)

### Uyumluluk
- KVKK (Kişisel Verilerin Korunması)
- PCI-DSS (Kart güvenliği)
- BDDK mevzuatı

## 🚀 Kurulum

### Gereksinimler
- .NET 8 SDK
- Oracle XE 21c
- PostgreSQL 15+
- Redis
- RabbitMQ / Kafka
- Python 3.11+
- Node.js 18+ (Frontend için)

### Backend Kurulum
```bash
cd src/Backend
dotnet restore
dotnet build
dotnet run --project MetinBank.API
```

### Python Analytics Kurulum
```bash
cd src/Python
python -m venv venv
venv\Scripts\activate  # Windows
pip install -r requirements.txt
python app.py
```

### Frontend Kurulum
```bash
cd src/Frontend
npm install
npm start
```

## 📊 Performans Hedefleri

- **Eşzamanlı Kullanıcı:** 10,000
- **API Yanıt Süresi:** < 200ms (ortalama)
- **Risk Analizi:** < 500ms
- **Başarı Oranı:** %95+
- **Uptime:** %99.9

## 🧪 Test

### Birim Testleri
```bash
dotnet test
```

### Entegrasyon Testleri
```bash
dotnet test --filter Category=Integration
```

### Yük Testleri
```bash
k6 run scripts/load-test.js
```

## 📖 API Dokümantasyonu

API dokümantasyonu Swagger üzerinden erişilebilir:
- Development: `http://localhost:5000/swagger`
- Staging: `https://staging-api.metinbank.com/swagger`

## 🔄 Veri Akışı

### Örnek: Kurumsal Maaş Ödemesi
1. Firma Hazırlayıcı → Maaş listesi yükler
2. Sistem → PENDING_FIRM_APPROVAL (Firma onayı bekliyor)
3. Firma Onaylayıcı → Listeyi onaylar
4. Sistem → PENDING_BANK_APPROVAL (Banka onayı bekliyor)
5. Risk Servisi → Python'da risk analizi
6. Banka Personeli → Onay/Red
7. Sistem → İşlemi gerçekleştirir (EFT/Havale)
8. Bildirim Servisi → Taraflara bildirim gönderir

## 📝 Lisans

Bu proje özel bir banka otomasyon sistemidir ve tüm hakları saklıdır.

## 👥 İletişim

**Proje Yöneticisi:** Metin Melikşah Dermencioğlu  
**Tarih:** 28 Ekim 2025

---

## 🗺️ Roadmap

### Faz 1 - Core Banking (Tamamlandı)
- [x] Proje yapısı
- [x] Authentication/Authorization
- [x] Müşteri yönetimi
- [x] Hesap işlemleri

### Faz 2 - Payment & Cards (Devam Ediyor)
- [ ] Kart yönetimi
- [ ] Transfer sistemleri
- [ ] Ödeme sistemleri

### Faz 3 - Investment & Loans
- [ ] Yatırım ürünleri
- [ ] Kredi yönetimi

### Faz 4 - Corporate Banking
- [ ] Kurumsal modüller
- [ ] Toplu ödemeler
- [ ] Dış ticaret

### Faz 5 - Advanced Features
- [ ] Chatbot
- [ ] Mobile app
- [ ] ATM simülasyonu

### Faz 6 - Production Ready
- [ ] Performance optimization
- [ ] Security hardening
- [ ] Documentation
- [ ] Deployment automation


