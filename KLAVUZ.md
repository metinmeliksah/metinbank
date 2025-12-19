# 🏦 MetinBank - Geliştirme Klavuzu

## 📋 Proje Hakkında

**MetinBank**, Nesne Yönelimli Programlama (OOP) prensiplerine ve Fırat Üniversitesi uygulama geliştirme standartlarına tam uyumlu, güvenli, ölçeklenebilir, dağıtık veritabanı yapısına sahip (MsSQL & PostgreSQL) ve yapay zeka destekli hibrit bir bankacılık sistemidir.

## 🎯 Proje Vizyonu

Kurumsal standartlarda, DevExpress bileşenleriyle zenginleştirilmiş, katmanlı mimaride geliştirilmiş modern bir bankacılık uygulaması.

---

## 🏗️ Mimari Yapı

### Katmanlar

```
MetinBank/
│
├── MetinBank.Entities/              # Veritabanı entity sınıfları
│   ├── User.cs
│   ├── UserScreen.cs
│   ├── Customer.cs
│   ├── Branch.cs
│   ├── Account.cs
│   └── Transaction.cs
│
├── MetinBank.Modul.Interface/       # Soyutlama (Interface) katmanı
│   ├── IUserService.cs
│   ├── ICustomerService.cs
│   ├── IAccountService.cs
│   └── IBranchService.cs
│
├── MetinBank.Modul.SPObject/        # Stored Procedure çağrıları
│   ├── BaseSP.cs
│   ├── UserSP.cs
│   ├── CustomerSP.cs
│   ├── BranchSP.cs
│   └── AccountSP.cs
│
├── MetinBank.Modul.Business/        # İş kuralları
│   ├── UserBusiness.cs
│   ├── CustomerBusiness.cs
│   ├── BranchBusiness.cs
│   └── AccountBusiness.cs
│
├── MetinBank.Modul.Service/         # Hata yönetimi ve API/Db çağrıları
│   ├── UserService.cs
│   ├── CustomerService.cs
│   ├── BranchService.cs
│   └── AccountService.cs
│
├── MetinBank.Modul.Forms/           # WinForms (DevExpress) UI
│   ├── Program.cs
│   ├── SessionManager.cs
│   ├── FrmLogin.cs
│   ├── FrmMain.cs
│   └── FrmMusteriListesi.cs
│
└── Database/                        # Veritabanı scriptleri
    ├── 01_MsSQL_CreateDatabase.sql
    └── 02_PostgreSQL_CreateAnalytics.sql
```

---

## 🚀 Aşama 1: Tamamlanan İşlemler (Mevcut)

### ✅ Yapılan İşlemler

1. **Proje İskeleti Oluşturuldu**
   - Tüm katmanlar (Entities, Interface, SPObject, Business, Service, Forms)
   - Solution (.sln) dosyası
   - Proje referansları

2. **Entity Sınıfları**
   - User, UserScreen, Customer, Branch, Account, Transaction

3. **Interface Katmanı**
   - IUserService, ICustomerService, IAccountService, IBranchService

4. **SPObject Katmanı**
   - BaseSP (Temel SP çağrıları)
   - UserSP, CustomerSP, BranchSP, AccountSP

5. **Business Katmanı**
   - UserBusiness, CustomerBusiness, BranchBusiness, AccountBusiness
   - İş kuralları validasyonu

6. **Service Katmanı**
   - UserService, CustomerService, BranchService, AccountService
   - Hata yönetimi (string döner: null = başarılı, değilse hata mesajı)

7. **WinForms UI**
   - FrmLogin: Kullanıcı giriş formu
   - FrmMain: Ana menü formu (MDI)
   - FrmMusteriListesi: Müşteri liste formu
   - SessionManager: Oturum yönetimi

8. **Veritabanı Scriptleri**
   - MsSQL: Tablolar ve Stored Procedures
   - PostgreSQL: Analytics tabloları
   - Test verileri

---

## 📦 Kurulum ve Çalıştırma

### 1. Ön Gereksinimler

- **Visual Studio 2022** (veya üzeri)
- **.NET 6.0 SDK**
- **SQL Server** (Express veya üzeri)
- **PostgreSQL** (opsiyonel - Analytics için)
- **DevExpress WinForms** kütüphaneleri (lisans gerekli)

### 2. Veritabanı Kurulumu

#### MsSQL (Transactional)

```powershell
# SQL Server Management Studio'da çalıştırın:
Database/01_MsSQL_CreateDatabase.sql
```

Bu script:
- `MetinBankDB` veritabanını oluşturur
- Tabloları oluşturur
- Stored Procedures oluşturur
- Test verilerini ekler (3 kullanıcı, 3 şube)

**Test Kullanıcıları:**
- Kullanıcı: `admin`, Şifre: `123456` (Tüm yetkiler)
- Kullanıcı: `mudur`, Şifre: `123456` (Görüntüleme + Onay)
- Kullanıcı: `personel`, Şifre: `123456` (Sadece görüntüleme)

#### PostgreSQL (Analytics)

```sql
-- pgAdmin veya psql'de çalıştırın:
Database/02_PostgreSQL_CreateAnalytics.sql
```

### 3. Connection String Ayarı

[BaseSP.cs](MetinBank.Modul.SPObject/BaseSP.cs) dosyasında connection string'i güncelleyin:

```csharp
ConnectionString = "Data Source=.;Initial Catalog=MetinBankDB;Integrated Security=True";
```

veya kendi sunucunuz için:

```csharp
ConnectionString = "Data Source=SUNUCU_ADI;Initial Catalog=MetinBankDB;User Id=SA;Password=SIFRE";
```

### 4. DevExpress Kurulumu

**ÖNEMLİ:** Aşama 1'de DevExpress bileşenleri yer tutucu olarak standart WinForms kontrolleriyle değiştirilmiştir.

DevExpress kurulumu için:

1. DevExpress lisansınızı edinin
2. [DevExpress WinForms](https://www.devexpress.com/products/net/controls/winforms/) paketini yükleyin
3. NuGet paketlerini ekleyin:

```powershell
Install-Package DevExpress.Win.Grid
Install-Package DevExpress.Win.Navigation
Install-Package DevExpress.Win.Editors
```

4. [MetinBank.Modul.Forms.csproj](MetinBank.Modul.Forms/MetinBank.Modul.Forms.csproj) dosyasındaki yorumları kaldırın

### 5. Projeyi Derleyin ve Çalıştırın

```powershell
# Solution'ı derleyin
dotnet build MetinBank.sln

# Projeyi çalıştırın
dotnet run --project MetinBank.Modul.Forms/MetinBank.Modul.Forms.csproj
```

---

## 🎨 DevExpress Standartları

### Bileşen İsimlendirme Önekleri

| Bileşen | Önek | Örnek |
|---------|------|-------|
| SimpleButton | btn | `btnKaydet`, `btnSil` |
| TextEdit | txt | `txtAd`, `txtSoyad` |
| LookUpEdit | lue | `lueSubeKod`, `lueHesapNo` |
| DateEdit | date | `dateDogumTarihi` |
| CalcEdit | calc | `calcTutar` |
| SpinEdit | spin | `spinMiktar` |
| GridControl | grd | `grdMusteri` |
| GridView | grdw | `grdwMusteri` |
| PictureEdit | pct | `pctImza`, `pctLogo` |
| XtraTabControl | xtab | `xtabMusteri` |
| NavBarControl | nav | `navMenu` |
| RibbonControl | ribbon | `ribbonMain` |

### Form Standartları

**YAPMASI GEREKENLER:**
- ✅ Formlar `XtraForm`'dan türetilmeli
- ✅ DevExpress bileşenleri kullanılmalı (Button yerine SimpleButton)
- ✅ Öneklere uyulmalı

**YAPILMAMASI GEREKENLER:**
- ❌ Standart .NET kontrolleri kullanılmamalı (Button, TextBox, ComboBox)
- ❌ Form sınıfı sadece Form'dan türetilmemeli

---

## 📊 Kodlama Standartları

### Service Katmanı

Service metotları **her zaman string döner**:
- `null` = Başarılı işlem
- `string` = Hata mesajı

```csharp
public string? SaveCustomer(Customer customer)
{
    try
    {
        if (customer == null)
            return "Müşteri bilgisi boş olamaz!";
        
        string? result = _customerBusiness.SaveCustomer(customer);
        return result; // null veya hata mesajı
    }
    catch (Exception ex)
    {
        return $"Hata: {ex.Message}";
    }
}
```

### SPObject Katmanı

Stored Procedure çağrıları `SqlParameter` ile yapılır:

```csharp
public DataTable GetCustomerById(int customerId)
{
    SqlParameter[] parameters = new SqlParameter[]
    {
        new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId }
    };
    
    return ExecuteReader("sp_Customer_GetById", parameters);
}
```

### Business Katmanı

İş kuralları validasyonu yapılır:

```csharp
public string? SaveCustomer(Customer customer)
{
    if (string.IsNullOrWhiteSpace(customer.FirstName))
        return "Müşteri adı boş olamaz!";
    
    if (customer.IdentityNumber.Length != 11)
        return "TC Kimlik No 11 haneli olmalıdır!";
    
    // Kaydetme işlemi...
}
```

---

## 🔐 Güvenlik ve Yetkilendirme

### Oturum Yönetimi

[SessionManager.cs](MetinBank.Modul.Forms/SessionManager.cs) kullanılarak:

```csharp
// Giriş sonrası
SessionManager.CurrentUser = user;
SessionManager.UserScreens = screens;

// Yetki kontrolü
if (!SessionManager.HasScreenPermission("MUSTERI_LISTESI"))
{
    MessageBox.Show("Bu ekrana erişim yetkiniz yok!");
    return;
}

// Çıkış
SessionManager.Logout();
```

### Ekran Yetkileri

Veritabanı tablosu: `TBL_KUL_EKRAN`

| Sütun | Açıklama |
|-------|----------|
| ScreenCode | Ekran kodu (ör: MUSTERI_LISTESI) |
| CanView | Görüntüleme yetkisi |
| CanAdd | Ekleme yetkisi |
| CanEdit | Düzenleme yetkisi |
| CanDelete | Silme yetkisi |

---

## 📈 Sonraki Aşamalar

### Aşama 2: Temel Bankacılık (DevExpress UI)

**Yapılacaklar:**
1. DevExpress kurulumu ve entegrasyonu
2. FrmMusteriKarti (XtraTabControl ile sekmeli yapı)
3. GridControl ile gelişmiş listeleme
4. LookUpEdit bağlantıları
5. Validasyon ve DXErrorProvider

**Komut:** "Aşama 2'ye geçelim" veya "Devam"

### Aşama 3: İşlem ve Onaylar

**Yapılacaklar:**
1. Para yatırma/çekme formları
2. CalcEdit ve SpinEdit kullanımı
3. Onay mekanizması (Müdür onayı)
4. ChartControl ile raporlama

### Aşama 4: Web ve AI Entegrasyonu

**Yapılacaklar:**
1. .NET Core MVC web arayüzü
2. Python AI modülü entegrasyonu
3. Risk analizi
4. ETL süreçleri (MsSQL → PostgreSQL)

---

## 🛠️ Sorun Giderme

### Veritabanı Bağlantı Hatası

```
Hata: Cannot open database "MetinBankDB"
```

**Çözüm:** 
1. SQL Server çalışıyor mu kontrol edin
2. Connection string'i doğrulayın
3. Veritabanı scriptini çalıştırın

### DevExpress Lisans Hatası

```
Hata: DevExpress license is not found
```

**Çözüm:**
1. DevExpress lisansınızı kontrol edin
2. `licenses.licx` dosyasını projeye ekleyin
3. Visual Studio'yu yeniden başlatın

### Build Hatası

```
Hata: Could not find project reference
```

**Çözüm:**
1. Solution'ı temizleyin: `dotnet clean`
2. NuGet paketlerini geri yükleyin: `dotnet restore`
3. Tekrar derleyin: `dotnet build`

---

## 📝 Proje Kontrol Listesi

### Aşama 1 (Tamamlandı) ✅

- [x] Proje iskeleti
- [x] Entity sınıfları
- [x] Interface katmanı
- [x] SPObject katmanı
- [x] Business katmanı
- [x] Service katmanı
- [x] WinForms temel yapı
- [x] MsSQL veritabanı ve SP'ler
- [x] PostgreSQL analytics tabloları
- [x] Test verileri

### Aşama 2 (Bekliyor) ⏳

- [ ] DevExpress entegrasyonu
- [ ] FrmMusteriKarti (sekmeli)
- [ ] GridControl implementasyonu
- [ ] LookUpEdit bağlantıları
- [ ] Validasyon ve hata gösterimi

### Aşama 3 (Bekliyor) ⏳

- [ ] İşlem formları
- [ ] Onay mekanizması
- [ ] Raporlama

### Aşama 4 (Bekliyor) ⏳

- [ ] Web arayüzü
- [ ] AI entegrasyonu
- [ ] ETL süreçleri

---

## 🤝 Katkıda Bulunma

Bu proje Fırat Üniversitesi standartlarına göre geliştirilmektedir. Her aşama tamamlandıktan sonra bir sonraki aşamaya geçilmelidir.

**Sonraki adım için komut:**
```
"Aşama 2'ye geçelim"
```

---

## 📞 İletişim

Proje hakkında sorularınız için:
- Geliştirici: MetinBank Ekibi
- Proje: MetinBank ERP
- Versiyon: 1.0 (Aşama 1)

---

## 📄 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

---

**Son Güncelleme:** 19 Aralık 2025  
**Aşama:** 1 / 4  
**Durum:** Altyapı Tamamlandı ✅
