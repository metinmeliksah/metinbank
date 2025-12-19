# MetinBank

Nesne Yönelimli Programlama (OOP) prensiplerine ve Fırat Üniversitesi standartlarına uygun, DevExpress destekli kurumsal bankacılık uygulaması.

## Teknolojiler

- **Backend:** C# .NET 6.0
- **Database:** MsSQL (Transactional) + PostgreSQL (Analytics)
- **UI:** WinForms + DevExpress
- **Web:** .NET Core MVC (Aşama 4)
- **AI:** Python (Aşama 4)

## Hızlı Başlangıç

1. **Veritabanını kurun:**
   ```sql
   -- SQL Server'da çalıştırın
   Database/01_MsSQL_CreateDatabase.sql
   ```

2. **Connection string'i ayarlayın:**
   [BaseSP.cs](MetinBank.Modul.SPObject/BaseSP.cs) dosyasında günceleyin.

3. **Projeyi çalıştırın:**
   ```powershell
   dotnet build MetinBank.sln
   dotnet run --project MetinBank.Modul.Forms/MetinBank.Modul.Forms.csproj
   ```

4. **Giriş yapın:**
   - Kullanıcı: `admin`
   - Şifre: `123456`

## Detaylı Dokümantasyon

👉 [KLAVUZ.md](KLAVUZ.md) dosyasına bakın.

## Aşamalar

- ✅ **Aşama 1:** Altyapı ve Temel Yapı (Tamamlandı)
- ⏳ **Aşama 2:** DevExpress UI ve Müşteri Modülü
- ⏳ **Aşama 3:** İşlem ve Onay Mekanizması
- ⏳ **Aşama 4:** Web ve AI Entegrasyonu

## Proje Yapısı

```
MetinBank/
├── MetinBank.Entities/           # Entity sınıfları
├── MetinBank.Modul.Interface/    # Interface katmanı
├── MetinBank.Modul.SPObject/     # SP çağrıları
├── MetinBank.Modul.Business/     # İş kuralları
├── MetinBank.Modul.Service/      # Servis katmanı
├── MetinBank.Modul.Forms/        # WinForms UI
└── Database/                     # SQL scriptleri
```

## Lisans

Eğitim amaçlı proje.
