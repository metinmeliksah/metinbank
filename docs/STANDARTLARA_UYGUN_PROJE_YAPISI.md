# MetinBank - Standartlara Uygun Proje Yapısı

**Tarih:** 4 Kasım 2025  
**Durum:** Temel yapı tamamlandı, devam ediyor

## 📊 Özet

MetinBank projesi, belirtilen kodlama ve isimlendirme standartlarına göre yeniden yapılandırıldı.

## ✅ Tamamlanan İşler

### 1. İsimlendirme Standartları Dokümantasyonu
- ✅ **docs/ISIMLENDIRME_STANDARTLARI.md** oluşturuldu
- ✅ Tüm kontrol isimlendirmeleri dokümante edildi
- ✅ Namespace yapısı tanımlandı
- ✅ Class, Method, Property standartları belirlendi
- ✅ SP Object katmanı kuralları yazıldı

### 2. Namespace Yapısı (Standartlara Uygun)

```
MetinBank.Common.Entity         ✅ Oluşturuldu
MetinBank.Common.Enums          ✅ Oluşturuldu
MetinBank.Common.Helper         ✅ Oluşturuldu

MetinBank.Musteri.Service       ✅ Oluşturuldu
MetinBank.Musteri.Interface     ✅ Oluşturuldu
MetinBank.Musteri.Business      ✅ Oluşturuldu
MetinBank.Musteri.SP            ✅ Oluşturuldu

MetinBank.Hesap.Service         ✅ Oluşturuldu
MetinBank.Hesap.Interface       ✅ Oluşturuldu
MetinBank.Hesap.Business        ✅ Oluşturuldu
MetinBank.Hesap.SP              ✅ Oluşturuldu
```

### 3. Entity Sınıfları (Standartlara Uygun)

#### BaseEntity.cs ✅
```csharp
namespace MetinBank.Common.Entity
{
    public abstract class BaseEntity
    {
        // Private değişkenler _ ile başlar
        private Guid _id;
        private DateTime _createdAt;
        
        // Property'ler PascalCase
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
```

#### Customer.cs ✅
```csharp
namespace MetinBank.Common.Entity
{
    public class Customer : BaseEntity
    {
        // Private değişkenler
        private string _customerNumber;
        private decimal _bakiye; // Standart: property için _bakiye
        
        // Property'ler
        public string CustomerNumber { get; set; }
        public decimal Bakiye { get; set; } // Property ismi: Bakiye
        
        // Public değişkenler - camelCase
        public long sicilNo;
        public string kisaAd;
    }
}
```

#### Account.cs ✅
```csharp
namespace MetinBank.Common.Entity
{
    public class Account : BaseEntity
    {
        private decimal _bakiye; // Standart format
        public decimal Bakiye { get; set; }
        
        public string hesapNo; // camelCase - standart
        public long musteriNo; // long tip - standart
    }
}
```

### 4. Enum Sınıfları ✅

```csharp
namespace MetinBank.Common.Enums
{
    public enum CustomerType
    {
        Retail = 1,
        Corporate = 2
    }
    
    public enum AccountType
    {
        DemandDeposit = 1,
        TimeDeposit = 2,
        ForeignCurrency = 3,
        Overdraft = 4
    }
}
```

### 5. SP Katmanı (Standartlara Uygun) ✅

#### SpMusteri.cs
```csharp
namespace MetinBank.Musteri.SP
{
    public static class SpMusteri
    {
        // Package isimleri - standart: T_MUSTERI, P_MUSTERI
        public const string T_MUSTERI = "PKG_MUSTERI";
        public const string P_MUSTERI_EKLE = "P_MUSTERI_EKLE";
        
        /// <summary>
        /// Müşteri ekler
        /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">OracleConnection (çağıran katmandan gelir)</param>
        /// <param name="trans">OracleTransaction</param>
        /// <returns>Müşteri No (long tip - standart)</returns>
        public static long MusteriEkle(OracleConnection conn, OracleTransaction trans,
                                       string tcKimlikNo, string ad, string soyad)
        {
            // Oracle nesneleri - standart isimlendirme
            OracleCommand cmd = new OracleCommand();
            OracleParameter prm = new OracleParameter();
            // ...
        }
        
        /// <summary>
        /// Database'deki SP ismiyle birebir aynı
        /// </summary>
        public static decimal get_bakiye(OracleConnection conn, string hesapNo)
        {
            // SP ismi: get_bakiye (database ile aynı)
        }
    }
}
```

**Önemli Kurallar:**
- ✅ Bu katmanda OracleConnection **kurulmaz**
- ✅ Connection bilgisi parametre olarak gelir
- ✅ SP isimleri database ile **birebir aynı**
- ✅ Oracle nesneleri: `conn`, `cmd`, `trans`, `prm`, `da`, `dr`, `cb`

### 6. Interface Katmanı ✅

#### IMusteriService.cs
```csharp
namespace MetinBank.Musteri.Interface
{
    /// <summary>
    /// Müşteri Service Interface
    /// Standart: Interface prefix'i I
    /// </summary>
    public interface IMusteriService
    {
        long MusteriEkle(string tcKimlikNo, string ad, string soyad);
        DataTable MusteriBul(string tcKimlikNo);
        string MusteriGuncelle(long musteriNo, string ad, string soyad);
    }
}
```

## 📋 Yapılacaklar (Kalan İşler)

### Öncelik 1: Business Katmanı
```csharp
namespace MetinBank.Musteri.Business
{
    /// <summary>
    /// Müşteri Business Logic
    /// Prefix: B (BMusteriIslem)
    /// </summary>
    public class BMusteriIslem
    {
        /// <summary>
        /// Birden fazla SP kullanır
        /// Anlamlı işlemler bütünü
        /// </summary>
        public static long MusteriEkle(OracleConnection conn, 
                                       OracleTransaction trans,
                                       Customer musteri)
        {
            // 1. Validasyon
            // 2. SpMusteri.MusteriEkle çağır
            // 3. SpLog.LogEkle çağır
            // 4. Return musteri_no
        }
    }
}
```

### Öncelik 2: Service Katmanı
```csharp
namespace MetinBank.Musteri.Service
{
    /// <summary>
    /// Müşteri Service
    /// Prefix: S (SMusteriService)
    /// </summary>
    public class SMusteriService : IMusteriService
    {
        private OracleConnection _conn;
        
        public long MusteriEkle(string tcKimlikNo, string ad, string soyad)
        {
            OracleTransaction trans = null;
            try
            {
                _conn.Open();
                trans = _conn.BeginTransaction();
                
                // Business katmanını çağır
                long musteriNo = BMusteriIslem.MusteriEkle(_conn, trans, musteri);
                
                trans.Commit();
                return musteriNo;
            }
            catch (Exception ex)
            {
                trans?.Rollback();
                throw;
            }
            finally
            {
                _conn?.Close();
            }
        }
    }
}
```

### Öncelik 3: Helper Katmanı
```csharp
namespace MetinBank.Common.Helper
{
    /// <summary>
    /// Genel Helper sınıfı
    /// Prefix: H (HGenelHelper)
    /// </summary>
    public static class HGenelHelper
    {
        /// <summary>
        /// String şifreleme
        /// </summary>
        public static string Sifrele(string text)
        {
            // AES-256 şifreleme
        }
        
        /// <summary>
        /// IBAN oluştur
        /// </summary>
        public static string IbanOlustur(string subeKod, string hesapNo)
        {
            // IBAN algoritması
        }
    }
}
```

### Öncelik 4: Windows Forms Projesi
```
MetinBank.Desktop.Forms/
├── FMusteriTanim.cs          // Form prefix: F
├── FHesapIslem.cs
├── FKartBasvuru.cs
└── Common/
    ├── FBaseForm.cs          // Base form
    └── Controls/
        ├── CtrlLibSubeKod.cs // User Control
        └── ucHesapNo.cs      // User Control instance: uc
```

**Form Kontrol İsimlendirmeleri:**
```csharp
public partial class FMusteriTanim : Form
{
    // Standart kontroller
    private Label lblAd;
    private Label lblSoyad;
    private TextBox txtAd;
    private TextBox txtSoyad;
    private Button btnKaydet;
    private Button btnKapat;
    private ComboBox cmbSubeKod;
    private DateTimePicker dtpDogumTarih;
    private CheckBox chkAktif;
    private RadioButton rbtnEvli;
    private RadioButton rbtnBekar;
    private DataGridView grdMusteriler;
    
    // DevExpress kontroller
    private SimpleButton btnDevxKaydet;
    private TextEdit txtDevxAd;
    private DateEdit dateDevxDogumTarih;
    private GridControl grdDevxParametre;
    private GridView grdwDevxParametre;
    private LookUpEdit lueDevxSubeKod;
}
```

### Öncelik 5: Control Library Projesi
```csharp
namespace MetinBank.Common.ControlLib
{
    /// <summary>
    /// Şube Kod User Control
    /// Standart: CtrlLib prefix
    /// </summary>
    public partial class CtrlLibSubeKod : UserControl
    {
        // Property ve metodlar x ile başlar
        public string xValue { get; set; }
        public DataTable xEkranParam { get; set; }
        
        public void xSetParams(string subeKod, string subeAd)
        {
            // Set işlemleri
        }
        
        public bool xValidate()
        {
            // Validasyon
            return true;
        }
    }
}

// Kullanımı:
private CtrlLibSubeKod ucSubeKod; // instance: uc prefix
```

## 🎯 Önemli Standartlar Özeti

### Değişken İsimlendirme
```csharp
// Public değişkenler - camelCase
public string kisaAd;
public long sicilNo;
public string hesapNo;

// Private değişkenler - _camelCase
private decimal _bakiye;
private int _adresKod;
private string _kisaAd;

// Property - PascalCase
public decimal Bakiye { get; set; }
public int AdresKod { get; set; }

// Local değişkenler
try { }
catch (Exception ex) { }      // ex, ex1, ex2
catch (OracleException ex1) { }

for (int i = 0; i < 10; i++) { } // i, j, k
```

### Oracle Nesneleri
```csharp
OracleConnection conn;
OracleCommand cmd;
OracleTransaction trans;
OracleParameter prm;
OracleDataAdapter da;
OracleDataReader dr;
OracleCommandBuilder cb;
```

### System.Data Nesneleri
```csharp
DataSet ds, dsEkran, dsOperator;
DataTable dt, dtEkran, dtOperator;
DataView dv, dvEkran, dvOperator;
DataRow drow, drowKisi, drowOperator;
DataColumn dcol, dcolSubeKod, dcolSubeAd;
```

### String Concatenation
```csharp
// YANLIŞ ✗
string sonuc = "";
for (int i = 0; i < 10; i++)
{
    sonuc += i.ToString();
}

// DOĞRU ✓
StringBuilder sonuc = new StringBuilder();
for (int i = 0; i < 10; i++)
{
    sonuc.Append(i.ToString());
}
```

### SQL Yazımı
```csharp
string sql = @"SELECT t.kategori,
                      v.versiyon,
                      v.tarih
               FROM das_dokuman d,
                    das_dokuman_versiyon v
               WHERE d.dokuman_no = v.dokuman_no
                 AND d.sube_kod = " + ucSubeKod.Text + @"
                 AND d.takip_no = '" + txtTakipNo.Text + @"'
               ORDER BY t.kategori";
```

### Açıklama Satırları
```csharp
/*
 * Müşteri ekleme metodu
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Bu metod yeni müşteri kaydı oluşturur
 * Edited by: ..., DD/MM/YYYY, Neden edit edildiği
 */
public void MusteriEkle(Customer musteri)
{
    // Bakiye kontrolü yapılıyor
    if (bakiye < 0)
    {
        /* Yetersiz bakiye durumu */
        throw new Exception("Yetersiz bakiye");
    }
}
```

## 📁 Güncel Proje Yapısı

```
metinbank/
├── src/
│   ├── Backend/
│   │   ├── MetinBank.Common.Entity/          ✅ Oluşturuldu
│   │   │   ├── BaseEntity.cs                 ✅
│   │   │   ├── Customer.cs                   ✅
│   │   │   └── Account.cs                    ✅
│   │   ├── MetinBank.Common.Enums/           ✅ Oluşturuldu
│   │   │   ├── CustomerType.cs               ✅
│   │   │   └── AccountType.cs                ✅
│   │   ├── MetinBank.Common.Helper/          ✅ Oluşturuldu
│   │   ├── MetinBank.Musteri.SP/             ✅ Oluşturuldu
│   │   │   └── SpMusteri.cs                  ✅
│   │   ├── MetinBank.Musteri.Interface/      ✅ Oluşturuldu
│   │   │   └── IMusteriService.cs            ✅
│   │   ├── MetinBank.Musteri.Business/       ✅ Oluşturuldu
│   │   ├── MetinBank.Musteri.Service/        ✅ Oluşturuldu
│   │   ├── MetinBank.Hesap.SP/               ✅ Oluşturuldu
│   │   ├── MetinBank.Hesap.Interface/        ✅ Oluşturuldu
│   │   ├── MetinBank.Hesap.Business/         ✅ Oluşturuldu
│   │   └── MetinBank.Hesap.Service/          ✅ Oluşturuldu
│   ├── Desktop/                              ⏳ Yapılacak
│   │   └── MetinBank.Forms/
│   │       ├── FMusteriTanim.cs
│   │       └── FHesapIslem.cs
│   └── Python/                               ✅ Mevcut
└── docs/
    ├── ISIMLENDIRME_STANDARTLARI.md         ✅ Oluşturuldu
    └── STANDARTLARA_UYGUN_PROJE_YAPISI.md   ✅ Bu dosya
```

## 🚀 Sonraki Adımlar

1. ⏳ Business katmanı implementasyonu
2. ⏳ Service katmanı implementasyonu
3. ⏳ Helper katmanı implementasyonu
4. ⏳ Windows Forms projesi oluşturma
5. ⏳ Control Library projesi
6. ⏳ Diğer modüller (Kart, Kredi, Transfer, vb.)

## 📞 Not

Bu proje **çok kapsamlı** olduğu için adım adım geliştirilmektedir. Temel yapı ve standartlar tam uygulanmıştır. Devam eden geliştirmeler için:

- **docs/ISIMLENDIRME_STANDARTLARI.md** - Detaylı standartlar
- **docs/PROJE_DURUMU.md** - Genel proje durumu
- **docs/KURULUM_REHBERI.md** - Kurulum adımları

---

**Son Güncelleme:** 4 Kasım 2025  
**Durum:** Temel yapı tamamlandı, devam ediyor


