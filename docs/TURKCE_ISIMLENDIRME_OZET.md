# MetinBank - Türkçe İsimlendirme Özeti

**Tarih:** 4 Kasım 2025  
**Durum:** Tamamlandı ✅

## 📋 Özet

MetinBank projesi, verilen standartlara uygun olarak **tamamen Türkçe isimlendirme** ile yapılandırıldı.

## ✅ Tamamlanan Türkçe Dönüşümler

### 1. Entity Sınıfları

#### Musteri.cs (Customer → Musteri)
```csharp
namespace MetinBank.Common.Entity
{
    public class Musteri : BaseEntity
    {
        // Private değişkenler - Türkçe
        private string _musteriNo;
        private string _ad;
        private string _soyad;
        private string _eposta;
        private string _telefon;
        private DateTime? _dogumTarih;
        private string _sifreHash;
        
        // Property'ler - Türkçe PascalCase
        public string MusteriNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public DateTime? DogumTarih { get; set; }
        public string SifreHash { get; set; }
        
        // Public değişkenler - Türkçe camelCase
        public long sicilNo;
        public string kisaAd;
        public int subeKod;
    }
}
```

#### Hesap.cs (Account → Hesap)
```csharp
namespace MetinBank.Common.Entity
{
    public class Hesap : BaseEntity
    {
        // Private değişkenler - Türkçe
        private string _hesapNo;
        private Guid _musteriId;
        private int _hesapTip;
        private int _dovizKod;
        private decimal _bakiye; // ÖNEMLİ: Standart format
        private decimal _kullanilabilirBakiye;
        private DateTime _acilisTarih;
        private DateTime? _vadeTarih;
        private decimal? _faizOran;
        
        // Property'ler - Türkçe PascalCase
        public string HesapNo { get; set; }
        public Guid MusteriId { get; set; }
        public int HesapTip { get; set; }
        public int DovizKod { get; set; }
        public decimal Bakiye { get; set; } // Property: Bakiye, private: _bakiye
        public decimal KullanilabilirBakiye { get; set; }
        public DateTime AcilisTarih { get; set; }
        public DateTime? VadeTarih { get; set; }
        public decimal? FaizOran { get; set; }
        
        // Public değişkenler - Türkçe camelCase
        public string hesapNo;
        public long musteriNo;
        public int adresKod;
        public int subeKod;
    }
}
```

### 2. Enum Sınıfları

#### MusteriTip (CustomerType → MusteriTip)
```csharp
namespace MetinBank.Common.Enums
{
    public enum MusteriTip
    {
        Bireysel = 1,    // Retail → Bireysel
        Kurumsal = 2     // Corporate → Kurumsal
    }
}
```

#### HesapTip (AccountType → HesapTip)
```csharp
namespace MetinBank.Common.Enums
{
    public enum HesapTip
    {
        Vadesiz = 1,     // DemandDeposit → Vadesiz
        Vadeli = 2,      // TimeDeposit → Vadeli
        Doviz = 3,       // ForeignCurrency → Doviz
        KMH = 4,         // Overdraft → KMH
        Yatirim = 5      // Investment → Yatirim
    }
}
```

### 3. SP Katmanı - Türkçe Metodlar

#### SpMusteri.cs
```csharp
namespace MetinBank.Musteri.SP
{
    public static class SpMusteri
    {
        // Package isimleri (Database ile aynı)
        public const string T_MUSTERI = "PKG_MUSTERI";
        public const string P_MUSTERI_EKLE = "P_MUSTERI_EKLE";
        
        /// <summary>
        /// Müşteri ekler - Türkçe parametreler
        /// </summary>
        public static long MusteriEkle(OracleConnection conn, 
                                       OracleTransaction trans,
                                       string tcKimlikNo, 
                                       string ad, 
                                       string soyad,
                                       string eposta,  // email → eposta
                                       string telefon)
        {
            // Oracle nesneleri - standart
            OracleCommand cmd = new OracleCommand();
            OracleParameter prm = new OracleParameter();
            
            // SQL - Türkçe kolon isimleri
            string sql = @"SELECT musteri_no,
                                 tc_kimlik_no,
                                 ad,
                                 soyad,
                                 eposta,
                                 telefon,
                                 durum,
                                 kayit_tarih
                          FROM musteriler
                          WHERE tc_kimlik_no = :tc_kimlik_no
                            AND aktif = 1";
        }
        
        /// <summary>
        /// Database SP ismiyle aynı
        /// </summary>
        public static decimal get_bakiye(OracleConnection conn, string hesapNo)
        {
            // SP ismi database ile birebir aynı olmalı
        }
    }
}
```

### 4. Interface Katmanı - Türkçe

#### IMusteriService.cs
```csharp
namespace MetinBank.Musteri.Interface
{
    public interface IMusteriService
    {
        /// <summary>
        /// Müşteri ekler - Türkçe parametreler
        /// </summary>
        long MusteriEkle(string tcKimlikNo, string ad, string soyad, 
                        string eposta, string telefon);
        
        DataTable MusteriBul(string tcKimlikNo);
        
        string MusteriGuncelle(long musteriNo, string ad, string soyad, 
                              string eposta);
        
        DataTable MusterileriGetir();
    }
}
```

## 🎯 Türkçe İsimlendirme Kuralları Özeti

### Private Değişkenler
```csharp
// DOĞRU ✓ - Türkçe, _ ile başlar
private string _musteriNo;
private string _ad;
private string _soyad;
private string _eposta;
private decimal _bakiye;
private int _subeKod;
private DateTime _dogumTarih;
private DateTime _kayitTarih;

// YANLIŞ ✗
private string _customerNumber;
private string _email;
private decimal _balance;
```

### Public Property'ler
```csharp
// DOĞRU ✓ - Türkçe PascalCase
public string MusteriNo { get; set; }
public string Ad { get; set; }
public string Soyad { get; set; }
public string Eposta { get; set; }
public decimal Bakiye { get; set; }
public int SubeKod { get; set; }
public DateTime DogumTarih { get; set; }

// YANLIŞ ✗
public string CustomerNumber { get; set; }
public string Email { get; set; }
public decimal Balance { get; set; }
```

### Public Değişkenler
```csharp
// DOĞRU ✓ - Türkçe camelCase
public long sicilNo;
public string kisaAd;
public int subeKod;
public string hesapNo;
public long musteriNo;
public int adresKod;

// YANLIŞ ✗
public long registrationNo;
public string shortName;
public int branchCode;
```

### Metodlar
```csharp
// DOĞRU ✓ - Türkçe PascalCase
public void MusteriEkle(string ad, string soyad) { }
public decimal BakiyeGetir(string hesapNo) { }
public void HesapKapat(string hesapNo) { }
public DataTable MusteriBul(string tcKimlikNo) { }

// Database SP isimleri - küçük harf, database ile aynı
public decimal get_bakiye(OracleConnection conn, string hesapNo) { }

// YANLIŞ ✗
public void AddCustomer(string name) { }
public decimal GetBalance(string accountNo) { }
```

### Enum'lar
```csharp
// DOĞRU ✓ - Türkçe PascalCase
public enum MusteriTip
{
    Bireysel = 1,
    Kurumsal = 2
}

public enum HesapTip
{
    Vadesiz = 1,
    Vadeli = 2,
    Doviz = 3
}

// YANLIŞ ✗
public enum CustomerType
{
    Retail = 1,
    Corporate = 2
}
```

### Class İsimleri
```csharp
// DOĞRU ✓ - Türkçe
public class Musteri : BaseEntity { }
public class Hesap : BaseEntity { }
public class Kart : BaseEntity { }
public class Kredi : BaseEntity { }

// YANLIŞ ✗
public class Customer : BaseEntity { }
public class Account : BaseEntity { }
```

## 📝 SQL Kolon İsimleri (Türkçe)

```sql
-- Müşteri tablosu
SELECT musteri_no,
       tc_kimlik_no,
       ad,
       soyad,
       eposta,
       telefon,
       durum,
       kayit_tarih,
       dogum_tarih,
       sifre_hash
FROM musteriler
WHERE aktif = 1;

-- Hesap tablosu
SELECT hesap_no,
       musteri_id,
       hesap_tip,
       doviz_kod,
       bakiye,
       kullanilabilir_bakiye,
       acilis_tarih,
       vade_tarih,
       faiz_oran
FROM hesaplar
WHERE aktif = 1;
```

## 🎨 Kontrol İsimlendirmeleri (Windows Forms)

### Standart Kontroller - Türkçe içerikle
```csharp
// Label
private Label lblAd;
private Label lblSoyad;
private Label lblMusteriNo;
private Label lblSubeKod;

// TextBox
private TextBox txtAd;
private TextBox txtSoyad;
private TextBox txtTcKimlikNo;
private TextBox txtEposta;
private TextBox txtTelefon;

// Button
private Button btnKaydet;
private Button btnSil;
private Button btnIptal;
private Button btnAra;

// ComboBox
private ComboBox cmbSubeKod;
private ComboBox cmbHesapTip;
private ComboBox cmbDovizKod;

// DateTimePicker
private DateTimePicker dtpDogumTarih;
private DateTimePicker dtpKayitTarih;
private DateTimePicker dtpVadeTarih;

// DataGridView
private DataGridView grdMusteriler;
private DataGridView grdHesaplar;
```

### DevExpress Kontroller - Türkçe içerikle
```csharp
// TextEdit
private TextEdit txtAd;
private TextEdit txtSoyad;

// DateEdit
private DateEdit dateDogumTarih;
private DateEdit dateKayitTarih;

// LookUpEdit
private LookUpEdit lueSubeKod;
private LookUpEdit lueMusteriTip;

// GridControl & GridView
private GridControl grdMusteriler;
private GridView grdwMusteriler;
```

## 📊 Karşılaştırma Tablosu

| İngilizce | Türkçe | Kullanım |
|-----------|--------|----------|
| Customer | Musteri | Class, Entity |
| Account | Hesap | Class, Entity |
| Balance | Bakiye | Property, değişken |
| Email | Eposta | Property, değişken |
| Phone | Telefon | Property, değişken |
| BirthDate | DogumTarih | Property |
| OpenDate | AcilisTarih | Property |
| MaturityDate | VadeTarih | Property |
| InterestRate | FaizOran | Property |
| BranchCode | SubeKod | Property, değişken |
| CustomerNo | MusteriNo | Property |
| AccountNo | HesapNo | Property |
| Retail | Bireysel | Enum value |
| Corporate | Kurumsal | Enum value |
| DemandDeposit | Vadesiz | Enum value |
| TimeDeposit | Vadeli | Enum value |
| ForeignCurrency | Doviz | Enum value |

## ✅ Kontrol Listesi

- [x] Entity sınıfları Türkçe'ye çevrildi
- [x] Property'ler Türkçe PascalCase
- [x] Private değişkenler Türkçe _ ile
- [x] Public değişkenler Türkçe camelCase
- [x] Enum'lar Türkçe
- [x] Enum değerleri Türkçe
- [x] Metodlar Türkçe
- [x] Parametreler Türkçe
- [x] SQL kolon isimleri Türkçe
- [x] Kontrol isimleri prefix'li (lblAd, txtAd, vb.)
- [x] Açıklama satırları Türkçe
- [x] XML comment'ler Türkçe

## 🎯 Önemli Notlar

1. **Bakiye Standardı:**
   - Property: `Bakiye` (PascalCase)
   - Private: `_bakiye` (camelCase with _)
   - Bu standar mutlaka uygulanmalı

2. **Long Tip Kullanımı:**
   ```csharp
   public long sicilNo;
   public long musteriNo;
   ```
   Müşteri No, Sicil No, Vergi No gibi sayısal değerlerde `long` kullanılmalı

3. **Database SP İsimleri:**
   ```csharp
   public static decimal get_bakiye(...)  // Database ile aynı - küçük harf
   ```

4. **camelCase vs PascalCase:**
   - Private değişkenler: `_musteriNo` (camelCase with _)
   - Public değişkenler: `musteriNo` (camelCase)
   - Property'ler: `MusteriNo` (PascalCase)
   - Metodlar: `MusteriEkle` (PascalCase)

## 📁 Dosya Yapısı

```
src/Backend/
├── MetinBank.Common.Entity/
│   ├── BaseEntity.cs
│   ├── Musteri.cs              ✅ Türkçe
│   └── Hesap.cs                ✅ Türkçe
├── MetinBank.Common.Enums/
│   ├── MusteriTip.cs           ✅ Türkçe
│   └── HesapTip.cs             ✅ Türkçe
├── MetinBank.Musteri.SP/
│   └── SpMusteri.cs            ✅ Türkçe
├── MetinBank.Musteri.Interface/
│   └── IMusteriService.cs      ✅ Türkçe
├── MetinBank.Musteri.Business/ ⏳ Yapılacak
└── MetinBank.Musteri.Service/  ⏳ Yapılacak
```

## 🚀 Sonraki Adımlar

1. ⏳ Business katmanı - Türkçe
2. ⏳ Service katmanı - Türkçe
3. ⏳ Helper katmanı - Türkçe
4. ⏳ Windows Forms - Türkçe kontroller
5. ⏳ Diğer modüller (Kart, Kredi, Transfer)

---

**Son Güncelleme:** 4 Kasım 2025  
**Durum:** Türkçe isimlendirme tamamlandı ✅


