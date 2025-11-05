# MetinBank - Ek Standartlar

**Tarih:** 4 Kasım 2025  
**Eklenen Standartlar**

## 📋 GENEL STANDARTLAR

### 1. Hata Yönetimi
```csharp
// YANLIŞ ✗ - Global hata değişkeni
public class SMusteriService
{
    private string hata; // YANLIŞ - Global tanımlama
}

// DOĞRU ✓ - Method içinde tanımlama
public class SMusteriService
{
    public string MusteriEkle()
    {
        string hata = null; // Method içinde
        try
        {
            // İşlemler
        }
        catch (Exception ex)
        {
            hata = ex.Message;
        }
        return hata;
    }
}
```

### 2. Class İsimlendirme
```csharp
// DOĞRU ✓ - Her kelimenin ilk harfi büyük
public class FrmHoizHesapla { }
public class SMusteriBilgi { }
public class BKrediHesapla { }

// YANLIŞ ✗
public class Frmhoizhesapla { }
public class Smusteribilgi { }
```

### 3. Method İsimlendirme
```csharp
// DOĞRU ✓ - Her kelimenin ilk harfi büyük (PascalCase)
public string GetMusteriBilgi() { }
public void HesapBakiyeGuncelle() { }
public DataTable MusteriListesiGetir() { }

// YANLIŞ ✗
public string getMusteriBilgi() { }
public void hesapbakiyeguncelle() { }
```

### 4. Parametre İsimlendirme
```csharp
// DOĞRU ✓ - İlk kelime küçük, sonrakiler büyük (camelCase)
public void MusteriEkle(string tcKimlikNo, string adSoyad, int subeKod, string opAdi)
{
    // İşlemler
}

// YANLIŞ ✗
public void MusteriEkle(string TCKimlikNo, string AdSoyad, int SubeKod)
{
}
```

### 5. Private Değişkenler
```csharp
// DOĞRU ✓ - Class başında tanımlama
public class SMusteriService
{
    // Private değişkenler en başta
    private string _connectionString;
    private int _timeout;
    
    // Constructor
    public SMusteriService() { }
    
    // Metodlar
    public string MusteriEkle() { }
}

// YANLIŞ ✗
public class SMusteriService
{
    public string MusteriEkle() 
    {
        // Method içinde tanımlama - YANLIŞ
        private string _connectionString;
    }
}
```

### 6. Property ile Private Değişken
```csharp
// DOĞRU ✓ - Private değişken _ ile başlar
public class Musteri
{
    private string _musteriNo;  // _ ile başlar
    private decimal _bakiye;    // _ ile başlar
    
    public string MusteriNo
    {
        get { return _musteriNo; }
        set { _musteriNo = value; }
    }
    
    public decimal Bakiye
    {
        get { return _bakiye; }
        set { _bakiye = value; }
    }
}
```

### 7. Class ve Dosya İsimleri
```csharp
// DOĞRU ✓ - Class ismi = Dosya ismi
// Dosya: SCommon.cs
public class SCommon { }

// Dosya: BMusteriIslem.cs
public class BMusteriIslem { }

// Dosya: FrmMusteriTanim.cs
public class FrmMusteriTanim { }
```

### 8. Kod Açıklamaları (XML Comments)
```csharp
/// <summary>
/// Müşteri ekler ve müşteri numarası döndürür
/// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
/// </summary>
/// <param name="tcKimlikNo">TC Kimlik Numarası</param>
/// <param name="adSoyad">Müşteri ad soyad</param>
/// <param name="subeKod">Şube kodu</param>
/// <returns>Hata varsa mesaj, yoksa null</returns>
public string MusteriEkle(string tcKimlikNo, string adSoyad, int subeKod)
{
    // Method implementasyonu
}
```

### 9. Rapor ve Template Path'leri
```csharp
// DOĞRU ✓ - CommonFunction kullan
string raporPath = CommonFunction.GetReportDirectoryPath();
string templatePath = CommonFunction.GetTemplateDirectoryPath();

// Kullanım
string dekontPath = Path.Combine(raporPath, "Dekont.pdf");
string sablon = Path.Combine(templatePath, "MusteriSozlesme.docx");
```

---

## 📋 FORMS STANDARTLARI

### 1. Form İsimlendirme
```csharp
// Format: Modul[.AltModul].Forms.kisa_ad
// Örnek: Musteri.Kisi.Forms.kshvz

namespace MetinBank.Musteri.Kisi.Forms
{
    // Design class: Frm[kisa_ad]
    public partial class FrmKshvz : Form
    {
    }
    
    // Yardımcı class: F[kisa_ad]
    public class FKshvz
    {
        // Helper metodlar
    }
}
```

### 2. Form Özellikleri
```csharp
public partial class FrmMusteriTanim : Form
{
    public FrmMusteriTanim()
    {
        InitializeComponent();
        
        // Form özellikleri
        this.Size = new Size(770, 700);  // Max 770x700
        this.AutoScroll = true;           // AutoScroll = true
        this.Text = "Müşteri Tanımlama";  // Büyük harf ile başla
    }
}
```

### 3. Interface Çağrılarında Kontrol
```csharp
// DOĞRU ✓ - Hata kontrolü yapılmalı
private void btnKaydet_Click(object sender, EventArgs e)
{
    SMusteriService service = new SMusteriService();
    string hata = service.MusteriEkle(tcKimlikNo, ad, soyad);
    
    if (hata != null) // Mutlaka kontrol edilmeli
    {
        MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }
    
    MessageBox.Show("İşlem başarılı");
}

// YANLIŞ ✗ - Hata kontrolü yok
private void btnKaydet_Click(object sender, EventArgs e)
{
    service.MusteriEkle(tcKimlikNo, ad, soyad); // Hata kontrolü yok
}
```

### 4. DML İşlemleri
```csharp
// DOĞRU ✓ - DMLManager kullan
using (DMLManager dmlManager = new DMLManager())
{
    string hata = dmlManager.ExecuteNonQuery(sql, parameters);
    if (hata != null)
    {
        MessageBox.Show(hata);
    }
}

// YANLIŞ ✗ - Direkt SQL kullanma
// Form'larda SQL KULLANILMAMALI!
```

### 5. Kontrol İsimleri
```csharp
// DOĞRU ✓ - İlk harf büyük
btnSorgula.Text = "Sorgula";       // ✓
btnAramaYap.Text = "Arama Yap";    // ✓
lblMusteriAd.Text = "Müşteri Adı"; // ✓

// YANLIŞ ✗
btnSorgula.Text = "sorgula";       // ✗
btnAramaYap.Text = "arama yap";    // ✗
```

### 6. DataGridView Çift Tıklama
```csharp
private void grdMusteriler_DoubleClick(object sender, EventArgs e)
{
    // Seçili satırı düzeltme moduna al
    if (grdMusteriler.SelectedRows.Count > 0)
    {
        DataGridViewRow selectedRow = grdMusteriler.SelectedRows[0];
        // Düzeltme işlemleri
    }
}
```

### 7. Referans Kuralları
```csharp
// FORM PROJELERINDE ASLA BUNLAR REFERANS EDİLMEMELİ:
// ✗ Service dll'leri
// ✗ Business dll'leri
// ✗ Util.DataAccess dll'leri

// SADECE BUNLAR REFERANS EDİLEBİLİR:
// ✓ Interface dll'leri
// ✓ Entity dll'leri
// ✓ Common dll'leri
// ✓ User Control dll'leri
```

### 8. Assembly Versiyon
```csharp
// AssemblyInfo.cs veya .csproj içinde
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// kul_ekran tablosuna kayıt
INSERT INTO kul_ekran (ekran_kod, versiyon, menudeki_adi)
VALUES ('MST001', '1.0.0.0', 'Müşteri Tanımlama');
```

---

## 📋 SERVICE STANDARTLARI

### 1. Try-Catch Yapısı
```csharp
public class SMusteriService
{
    /// <summary>
    /// Müşteri ekler
    /// </summary>
    public string MusteriEkle(string tcKimlikNo, string ad, string soyad)
    {
        string hata = null; // Method içinde tanımla
        
        using (ServiceManager sMan = new ServiceManager()) // using ile kullan
        {
            try
            {
                // İşlemler
                sMan.BeginTransaction();
                
                // SP çağrısı - SPBuilder kullan (ExecuteSP DEĞİL)
                long musteriNo = SpMusteri.MusteriEkle(sMan.Connection, sMan.Transaction,
                    tcKimlikNo, ad, soyad);
                
                sMan.Commit();
            }
            catch (Exception ex)
            {
                sMan?.Rollback();
                hata = ex.Message; // Exception'da message'a eşitle
            }
        }
        
        return hata; // String döndür
    }
}
```

### 2. Service İsimlendirme
```csharp
// Format: Modul.Service
namespace MetinBank.Musteri.Service
{
    /// <summary>
    /// Müşteri Service
    /// Class ismi S ile başlamalı
    /// </summary>
    public class SMusteriService // S prefix'i zorunlu
    {
        // Class bazlı değişken ASLA tanımlanmamalı
        // ✗ private string hata; // YANLIŞ
        // ✗ private OracleConnection conn; // YANLIŞ
        
        // Tüm değişkenler method içinde tanımlanmalı
        public string MusteriEkle(string tcKimlikNo)
        {
            string hata = null; // ✓ Method içinde
            // İşlemler
            return hata;
        }
    }
}
```

### 3. Method Dönüş Tipi
```csharp
// DOĞRU ✓ - Tüm metodlar string döndürmeli
public string MusteriEkle() { return null; }
public string MusteriGuncelle() { return null; }
public string MusteriSil() { return null; }

// YANLIŞ ✗ - void veya başka tip dönmemeli
public void MusteriEkle() { } // ✗
public bool MusteriGuncelle() { return true; } // ✗
```

### 4. SP Çağrısı
```csharp
// DOĞRU ✓ - SPBuilder'dan oluşturulan SP dll kullan
long musteriNo = SpMusteri.MusteriEkle(conn, trans, tcKimlikNo, ad, soyad);

// YANLIŞ ✗ - ExecuteSP kullanma
sMan.ExecuteSP("PKG_MUSTERI.P_MUSTERI_EKLE", parameters); // YANLIŞ
```

---

## 📋 INTERFACE STANDARTLARI

### 1. Interface İsimlendirme
```csharp
// Format: Modul.Interface
namespace MetinBank.Musteri.Interface
{
    /// <summary>
    /// Müşteri Service Interface
    /// Class ismi I ile başlamalı
    /// </summary>
    public interface IMusteriService // I prefix'i zorunlu
    {
        string MusteriEkle(string tcKimlikNo, string ad, string soyad);
        string MusteriGuncelle(long musteriNo, string ad, string soyad);
        string MusteriSil(long musteriNo);
    }
}

// Hesap Interface
namespace MetinBank.Hesap.Interface
{
    public interface IHesapService // I prefix'i
    {
        string HesapAc(long musteriNo, string hesapNo);
        string ParaYatir(string hesapNo, decimal tutar);
    }
}
```

---

## 📊 STANDART KONTROL LİSTESİ

### Genel
- [ ] Hata değişkeni global değil, method içinde tanımlı mı?
- [ ] Class isimleri PascalCase mi? (FrmHoizHesapla)
- [ ] Method isimleri PascalCase mi? (GetMusteriBilgi)
- [ ] Parametreler camelCase mi? (subeKod, opAdi)
- [ ] Private değişkenler class başında tanımlı mı?
- [ ] Property'lerde _ kullanılıyor mu?
- [ ] Class ismi = Dosya ismi mi?
- [ ] XML comment'ler var mı?

### Forms
- [ ] Form isimlendirme: Modul.Forms.kisa_ad
- [ ] Yardımcı class F[kisa_ad] mi?
- [ ] Design class Frm[kisa_ad] mi?
- [ ] if(hata!=null) kontrolü var mı?
- [ ] DMLManager kullanılıyor mu?
- [ ] Kontroller büyük harfle başlıyor mu?
- [ ] DataGridView çift tıklama var mı?
- [ ] Size 770x700'ü geçmiyor mu?
- [ ] AutoScroll = true mi?
- [ ] Form'da SQL kullanılmıyor mu?
- [ ] Service/Business dll referans edilmemiş mi?
- [ ] Assembly version verilmiş mi?
- [ ] Sadece UC kullanılıyor mu?

### Service
- [ ] Try-catch düzgün mü?
- [ ] sMan using ile kullanılmış mı?
- [ ] string hata = null tanımlı mı?
- [ ] Tüm metodlar string döndürüyor mu?
- [ ] Service isimlendirme: Modul.Service
- [ ] Class ismi S ile başlıyor mu?
- [ ] SPBuilder kullanılıyor mu? (ExecuteSP değil)
- [ ] Class bazlı değişken yok mu?

### Interface
- [ ] Interface isimlendirme: Modul.Interface
- [ ] Class ismi I ile başlıyor mu?

---

**Son Güncelleme:** 4 Kasım 2025  
**Durum:** Ek standartlar eklendi


