# 🏦 MetinBank - Başlangıç Rehberi

Projeye hoş geldiniz! Bu dosya size projenin ne durumda olduğunu ve nasıl devam edeceğinizi anlatıyor.

## 📌 Hızlı Başlangıç

### 1. Projenin Mevcut Durumu

✅ **TAMAMLANAN İŞLER:**
- Proje yapısı oluşturuldu
- .NET 8 Backend projesi kuruldu (4 katman)
- 30+ Entity sınıfı tanımlandı
- 11 Enum tanımlandı
- Python Analytics servisi temel yapısı hazır
- Dokümantasyon hazırlandı
- Database script'leri başlatıldı

📊 **TAMAMLANMA ORANI:** ~15%

### 2. Şu An Ne Yapabilirsiniz?

#### A. Projeyi İnceleme
```bash
# Projeyi klonlayın/açın
cd D:\Github\metinbank

# Yapıyı inceleyin
tree /F
```

#### B. Dokümantasyonu Okuma
1. **[README.md](README.md)** - Genel bilgiler
2. **[docs/PROJE_DURUMU.md](docs/PROJE_DURUMU.md)** - Detaylı durum raporu
3. **[docs/KURULUM_REHBERI.md](docs/KURULUM_REHBERI.md)** - Kurulum adımları
4. **[gereksinim.pdf](gereksinim.pdf)** - Orijinal gereksinimler

#### C. Backend'i Derleme
```bash
cd src\Backend
dotnet build
```

## 🚀 Sonraki Adımlar

### Öncelik 1: Database Kurulumu

1. **Oracle XE Kurulumu**
   - Oracle XE 21c'yi kurun
   - `database/oracle/01_create_tables.sql` dosyasını çalıştırın

2. **PostgreSQL Kurulumu**
   - PostgreSQL 15+ kurun
   - Log database'ini oluşturun

**Detaylar:** [docs/KURULUM_REHBERI.md](docs/KURULUM_REHBERI.md)

### Öncelik 2: DbContext Oluşturma

```csharp
// src/Backend/MetinBank.Infrastructure/Data/OracleDbContext.cs
public class OracleDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    // ... diğer entity'ler
}
```

### Öncelik 3: Repository Pattern

```csharp
// Generic repository ve Unit of Work implementasyonu
public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
```

### Öncelik 4: Authentication

JWT token servisi ve kullanıcı authentication sistemi.

### Öncelik 5: İlk API Controller'lar

- AuthController (Login/Register)
- CustomerController (CRUD)
- AccountController (Balance, Transactions)

## 📁 Proje Yapısı

```
metinbank/
├── src/
│   ├── Backend/
│   │   ├── MetinBank.API/          # Web API (✅ Hazır)
│   │   ├── MetinBank.Core/         # Entities (✅ Hazır)
│   │   ├── MetinBank.Infrastructure/ # Data Access (⏳ Yapılıyor)
│   │   └── MetinBank.Services/     # Business Logic (❌ Yapılacak)
│   ├── Python/                     # Analytics (✅ Temel hazır)
│   ├── Frontend/                   # React/Angular (❌ Yapılacak)
│   ├── Mobile/                     # React Native (❌ Yapılacak)
│   └── Desktop/                    # WinForms (❌ Yapılacak)
├── database/                       # DB Scripts (✅ Başlatıldı)
├── docs/                           # Dokümantasyon (✅ Hazır)
└── README.md                       # Ana README (✅ Hazır)
```

## 🎯 Modüller ve Durumları

### ✅ Tamamlanan
- Proje iskelet yapısı
- Entity sınıfları (30+)
- Enum tanımları (11)
- Python analytics temel
- Dokümantasyon

### ⏳ Devam Eden
- Database context
- Repository pattern
- Migration scripts

### ❌ Yapılacak
- Authentication/Authorization
- API Controllers (15+)
- Business Services
- RabbitMQ integration
- Frontend (Web/Mobile)
- Windows Forms (Şube/ATM)
- Chatbot
- Test coverage

## 💡 Önemli Bilgiler

### Teknoloji Stack

**Backend:**
- .NET 8 Web API
- Entity Framework Core 8
- Oracle XE (Ana DB)
- PostgreSQL (Log DB)
- Redis (Cache/Session)
- JWT Authentication

**Analytics:**
- Python 3.11+
- Flask/FastAPI
- NumPy, Pandas, Scikit-learn

**Frontend:** (Yapılacak)
- React.js / Angular
- React Native / Flutter (Mobile)
- Windows Forms (.NET)

### Güvenlik

- ✅ HTTPS (TLS 1.3)
- ✅ JWT Token
- ✅ 2FA (Planlandı)
- ✅ Şifreleme (AES-256)
- ✅ Kart tokenizasyonu (Planlandı)

### Performans Hedefleri

- 10,000 eşzamanlı kullanıcı
- API yanıt < 200ms
- Risk analizi < 500ms
- %99.9 uptime

## 📞 Yardım & Destek

### Sorularınız mı var?

1. **Dokümantasyon:**
   - [PROJE_DURUMU.md](docs/PROJE_DURUMU.md) - Ne yapıldı, ne yapılacak?
   - [KURULUM_REHBERI.md](docs/KURULUM_REHBERI.md) - Nasıl kurulur?

2. **Gereksinimler:**
   - [gereksinim.pdf](gereksinim.pdf) - Orijinal SRS dökümanı

3. **Kod İnceleme:**
   - `src/Backend/MetinBank.Core/Entities/` - Entity modelleri
   - `src/Backend/MetinBank.Core/Enums/` - Enum tanımları
   - `src/Python/app.py` - Analytics servisi

## 🔄 Geliştime Süreci Önerisi

### Faz 1: Temel Altyapı (2 hafta)
1. Database setup ✅ (Başlatıldı)
2. DbContext & Migrations
3. Repository & UnitOfWork
4. Authentication & Authorization
5. İlk API endpoints

### Faz 2: Bireysel Bankacılık (2 hafta)
1. Customer Management
2. Account Operations
3. Card Management
4. Transfers & Payments
5. eKYC Flow

### Faz 3: Krediler & Yatırım (1 hafta)
1. Loan Module
2. Investment Module
3. Credit Scoring (Python)

### Faz 4: Kurumsal Bankacılık (1-2 hafta)
1. Corporate Users
2. Payroll/Bulk Payments
3. POS Integration
4. Trade Finance

### Faz 5: Frontend & Mobile (2 hafta)
1. Web UI (React/Angular)
2. Mobile App (React Native)
3. Windows Forms (Şube/ATM)

### Faz 6: Entegrasyonlar (1 hafta)
1. RabbitMQ/Kafka
2. Notification Service
3. Chatbot
4. Analytics Dashboard

### Faz 7: Test & Deploy (1 hafta)
1. Unit & Integration Tests
2. Load Testing
3. Security Testing
4. Documentation
5. Deployment

**TOPLAM TAHMİNİ SÜRE: 8-10 hafta (tek kişi, full-time)**

## 🎉 Başarılar Dileriz!

Bu çok büyük ve kapsamlı bir proje. Adım adım ilerlemeniz önerilir.

**İlk yapmanız gereken:**
1. ✅ Dokümantasyonu okuyun
2. ⏳ Database'leri kurun
3. ⏳ Backend'i derleyin
4. ⏳ Python servisini test edin

**Sorularınız için:**
- GitHub Issues
- Proje dokümantasyonu
- Inline kod yorumları

---

**Hazırlayan:** Metin Melikşah Dermencioğlu  
**Tarih:** 4 Kasım 2025  
**Versiyon:** 1.0

**Başarılar! 🚀**


