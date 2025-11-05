# MetinBank - İsimlendirme ve Kodlama Standartları

## 📋 İçindekiler
1. [Kontrol İsimlendirmeleri](#kontrol-isimlendirmeleri)
2. [Namespace Yapısı](#namespace-yapısı)
3. [Class İsimlendirmeleri](#class-isimlendirmeleri)
4. [Method İsimlendirmeleri](#method-isimlendirmeleri)
5. [Değişken İsimlendirmeleri](#değişken-isimlendirmeleri)
6. [Database Nesneleri](#database-nesneleri)
7. [Kodlama Standartları](#kodlama-standartları)

## Kontrol İsimlendirmeleri

### Windows Forms Standart Kontroller

| Kontrol Tipi | Prefix | Örnek |
|-------------|--------|-------|
| Label | lbl | lblAd, lblSoyad, lblSubeAd, lblSubeKod |
| LinkLabel | llbl | llblAd, llblSoyad |
| Button | btn | btnKaydet, btnDuzelt, btnSil, btnKapat |
| TextBox | txt | txtAd, txtSoyad, txtSubeAd, txtSubeKod |
| MainMenu | mmenu | mmenuDokuman, mmenuArsiv |
| CheckBox | chk | chkSpor, chkKultur, chkGazete |
| RadioButton | rbtn | rbtnEvli, rbtnBekar, rbtnYeniKayit |
| GroupBox | grp | grpMedeniHal |
| PictureBox | pct | pctImza, pctNufusCuzdan |
| Panel | pnl | pnlKimlik, pnlAdres, pnlTelefon |
| ListBox | lst | lstKategoriTip, lstSubeKod |
| CheckedListBox | clst | clstMailAdres |
| ComboBox | cmb | cmbSubeAd, cmbAdresKod |
| ListView | lview | lviewGorusme |
| TreeView | tview | tviewOrganizasyon |
| TabControl | tab | tabMusteriTanim |
| DateTimePicker | dtp | dtpTarih, dtpIseGirisTarih |
| MonthCalendar | mc | mcTakvim |
| HScrollBar | hsb | hsbGorus |
| VScrollBar | vsb | vsbGorus |
| Timer | timer | timerKayit, timerLog |
| Splitter | splitter | splitterMusteri |
| TrackBar | trackbar | trackbarFileUpload |
| ProgressBar | progbar | progbarFileUpload, progbarMuhasebe |
| RichTextBox | rtxt | rtxtAciklama, rtxtOneri |
| ImageList | ilst | ilstMenu, ilstDokuman |
| HelpProvider | hprv | hprvSicilNo |
| ToolTip | ttip | ttipTara, ttipFarkliKaydet |
| ContextMenu | cmenu | cmenuDosya |
| ToolBar | tbar | tbarKaydet |
| StatusBar | sbar | sbarKaydet, sbarrAc |
| NotifyIcon | nicon | niconUyari |
| ErrorProvider | eprv | eprvSubeKod |
| DataGridView | grd | grdSube |

### DevExpress Kontroller

| Kontrol Tipi | Prefix | Örnek |
|-------------|--------|-------|
| BarManager | barmng | barmngMuhasebe |
| PopupMenu | popmenu | popmenuMusteri |
| NavBarControl | navbar | navbarMuhasebe |
| VGridControl | vgrd | vgrdParametre |
| GridControl | grd | grdParametre |
| GridView | grdw | grdwParametre |
| SimpleButton | btn | btnKaydet |
| DefaultLookAndFeel | dlf | dlfMusteri |
| XtraTabControl | xtab | xtabMusteri |
| ButtonEdit | ebtn | ebtnKaydet |
| CalcEdit | calc | calcKaydet |
| CheckEdit | chk | chkFutbol, chkBasketbol |
| CheckedListBoxControl | clst | clstMailAdres |
| ComboBoxEdit | cmb | cmbSubeKod |
| ControlNavigator | ctrlnav | ctrlnavMusteri |
| DateEdit | date | dateDogumTarih |
| ImageEdit | img | imgMusteri |
| ImageListboxControl | imglist | imglistSubeAd |
| ListboxControl | lst | lstSubeAd |
| LookUpEdit | lue | lueSubeKod |
| MemoEdit | memo | memoAciklama |
| PictureEdit | pct | pctMusteri |
| ProgressBarControl | pbc | pbcUpload |
| RadioGroup | rg | rgSpor |
| SpinEdit | spin | spinAdres |
| TextEdit | txt | txtAd |
| TimeEdit | time | timeBaslangicSaat |
| GroupControl | group | groupMedeniHal |
| HScrollBar | hsb | hsbMusteri |
| VScrollBar | vsb | vsbMusteri |
| ImageCollection | imgcollect | imgcollectCek |
| PanelControl | pnl | pnlMusteri |
| ToolTipController | ttc | ttcKimlik |

## Namespace Yapısı

### Genel Format
```
MetinBank.[Modül].[Katman]
```

### Müşteri Modülü
```csharp
MetinBank.Musteri.Forms
MetinBank.Musteri.Service
MetinBank.Musteri.Interface
MetinBank.Musteri.Business
MetinBank.Musteri.SP
MetinBank.Musteri.Helper
```

### Hesap Modülü
```csharp
MetinBank.Hesap.Forms
MetinBank.Hesap.Service
MetinBank.Hesap.Genel.Service
MetinBank.Hesap.Detay.Service
MetinBank.Hesap.Interface
MetinBank.Hesap.Genel.Interface
MetinBank.Hesap.Detay.Interface
MetinBank.Hesap.Business
MetinBank.Hesap.Genel.Business
MetinBank.Hesap.Detay.Business
MetinBank.Hesap.SP
MetinBank.Hesap.Genel.SP
MetinBank.Hesap.Detay.SP
```

### Diğer Modüller
```csharp
MetinBank.Kart.Forms
MetinBank.Kart.Service
MetinBank.Kart.Business
MetinBank.Kart.SP

MetinBank.Kredi.Forms
MetinBank.Kredi.Service
MetinBank.Kredi.Business
MetinBank.Kredi.SP

MetinBank.Transfer.Forms
MetinBank.Transfer.Service
MetinBank.Transfer.Business
MetinBank.Transfer.SP

MetinBank.Yatirim.Forms
MetinBank.Yatirim.Service
MetinBank.Yatirim.Business
MetinBank.Yatirim.SP

MetinBank.Kurumsal.Forms
MetinBank.Kurumsal.Service
MetinBank.Kurumsal.Business
MetinBank.Kurumsal.SP

MetinBank.Common.Helper
MetinBank.Common.Entity
MetinBank.Common.Enums
```

## Class İsimlendirmeleri

### Katmanlara Göre Class Prefix'leri

| Katman | Prefix | Örnek |
|--------|--------|-------|
| Forms | F | FMusteri, FHesap, FKartTanim |
| Service | S | SMusteri, SHesap, SKart |
| Interface | I | IMusteri, IHesap, IKart |
| Business | B | BMusteri, BHesap, BKart |
| SP | Sp | SpMusteri, SpHesap, SpKart |
| Helper | H | HMusteri, HGenel, HGüvenlik |

### Örnekler

```csharp
// Forms
public class FMusteriTanim : Form { }
public class FHesapIslem : Form { }
public class FKartBasvuru : Form { }

// Service
public class SMusteriService { }
public class SHesapService { }

// Interface
public interface IMusteriService { }
public interface IHesapService { }

// Business
public class BMusteriIslem { }
public class BHesapIslem { }

// SP (Stored Procedures)
public static class SpMusteri 
{
    // T_MUSTERI, P_MUSTERI (database'deki package name'leri)
    public const string T_MUSTERI = "PKG_MUSTERI";
    public const string P_MUSTERI_EKLE = "P_MUSTERI_EKLE";
}

// Helper
public class HMusteriHelper { }
public class HGenelHelper { }
```

## Method İsimlendirmeleri

### PascalCase Kullanımı

```csharp
// Doğru ✓
public void MusteriEkle() { }
public Customer MusteriBul(long sicilNo) { }
public decimal BakiyeGetir(string hesapNo) { }
public bool HesapKapat(string hesapNo) { }

// SP'ler için - database'deki isimle aynı olmalı
public DataTable get_bakiye(OracleConnection conn, string hesapNo) { }
public void p_musteri_ekle(OracleConnection conn, OracleTransaction trans) { }
```

### Method Açıklama Formatı

```csharp
/// <summary>
/// Data table'ı update eder
/// </summary>
/// <param name="ci">ClientInfo</param>
/// <param name="dt">DataTable</param>
/// <param name="onErrorRollBack">Hata durumunda rollback</param>
/// <param name="dtLast">DataTable'ın son hali</param>
/// <param name="rowsAffected">Etkilenen kayıt sayısı</param>
/// <returns>String döner, hata yoksa null döner</returns>
public string DataTableGuncelle(ClientInfo ci, DataTable dt, bool onErrorRollBack, 
                                out DataTable dtLast, out int rowsAffected)
{
    // Method implementation
}
```

## Değişken İsimlendirmeleri

### Public Değişkenler - camelCase

```csharp
public string kisaAd;
public long sicilNo;
public string hesapNo;
public int subeKod;
```

### Private Değişkenler - _ prefix + camelCase

```csharp
// Property'ler için kullanılan private değişkenler
private decimal _bakiye;
private int _adresKod;
private string _kisaAd;
private long _musteriNo;
```

### Property - PascalCase

```csharp
// Property ile birlikte private değişken tanımı
private decimal _bakiye;
public decimal Bakiye 
{ 
    get { return _bakiye; }
    set { _bakiye = value; }
}

private int _adresKod;
public int AdresKod 
{ 
    get { return _adresKod; }
    set { _adresKod = value; }
}
```

### Local Değişkenler

```csharp
// Exception nesneleri
try { }
catch (Exception ex) { }
catch (OracleException ex1) { }
catch (InvalidOperationException ex2) { }

// For döngülerinde counter'lar
for (int i = 0; i < 10; i++) { }
for (int j = 0; j < 5; j++) { }
for (int k = 0; k < 3; k++) { }
```

## Database Nesneleri

### Oracle .NET Provider

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
// DataSet
DataSet ds;
DataSet dsEkran;
DataSet dsOperator;

// DataTable
DataTable dt;
DataTable dtEkran;
DataTable dtOperator;

// DataView
DataView dv;
DataView dvEkran;
DataView dvOperator;

// DataRow
DataRow drow;
DataRow drowKisi;
DataRow drowOperator;

// DataColumn
DataColumn dcol;
DataColumn dcolSubeKod;
DataColumn dcolSubeAd;
```

### Dialog ve Component Nesneleri

```csharp
OpenFileDialog ofdUpload;
OpenFileDialog ofdDownload;
SaveFileDialog sfdExport;
PrintDialog pdDekont;
PrintDialog pdFis;
FolderBrowserDialog fbdYedek;
```

## Control Library Standartları

### User Control İsimlendirme

```csharp
// User Control isimleri
CtrlLibSubeKod
CtrlLibHesapNo
CtrlLibEkNo

// User Control instance'ları
ucSubeKod
ucHukukiYapi
ucHesapNo
```

### Control Library Property ve Method Standartları

```csharp
public class CtrlLibSubeKod : UserControl
{
    /*
     * User Kontrollere yazdığımız her property, metot vs.. 
     * x ile başlamalı. Nedeni ise kod geliştirirken kendi 
     * yazdığımız bu tür özelliklere intellisense'dan 
     * kolayca ulaşmaktır.
     */
    
    // Property'ler x ile başlar
    public string xValue { get; set; }
    public DataTable xEkranParam { get; set; }
    
    // Metodlar x ile başlar
    public void xSetParams(string subeKod, string subeAd) 
    {
        // Implementation
    }
    
    public bool xValidate() 
    {
        // Validation logic
        return true;
    }
    
    public void xClear() 
    {
        // Clear logic
    }
}
```

### Control Library Kuralları

1. **xEkranParam Property:** Veritabanı ile ilişkisi olan her kontrolün bu property'si tanımlamalı
2. **xSetParams Method:** Bir veya birden fazla property'i set eden metot
3. **xValue Property:** Value property gerekiyorsa bu isim kullanılmalı ve set edildiğinde Text property'si de değiştirilmeli
4. **long tip kullanımı:** Hesap No, Kisi No, Vergi No gibi sayısal değerlerde long tipi kullanılmalı

## Kodlama Standartları

### Açıklama Satırları (Comment)

#### Çok Satırlı Açıklamalar

```csharp
/*
 * Açıklama satırı (Created by Metin Dermencioğlu, 04/11/2025)
 * Açıklama satırı (Fonksiyonu, uyarılar)
 * Açıklama satırı (Edited by ..., DD/MM/YYYY, Neden edit edildiği)
 */
public class MusteriService
{
    /*
     * Müşteri ekleme metodu
     * Created by: Metin Dermencioğlu, 04/11/2025
     * Bu metod yeni müşteri kaydı oluşturur
     */
    public void MusteriEkle(Customer musteri)
    {
        // Implementation
    }
}
```

#### Tek Satırlık Açıklamalar

```csharp
// Bakiye kontrolü yapılıyor
if (bakiye < 0) 
{
    /* Yetersiz bakiye durumu */
    throw new Exception("Yetersiz bakiye");
}
```

### Girintili Yazma (Indentation)

.NET editörü default olarak 4 karakter boşluk kullanır. Bu standarda uyulmalı.

#### For Döngüsü

```csharp
for (int i = 0; i < 5; i++)
{
    // İşlemler
    Console.WriteLine(i);
}
```

#### If-Else Koşulu

```csharp
if (a < b)
{
    // a küçükse
    Console.WriteLine("a küçük");
}
else
{
    // b küçükse veya eşitse
    Console.WriteLine("b küçük veya eşit");
}
```

#### Try-Catch Bloğu

```csharp
try
{
    // İşlemler
    MusteriEkle(musteri);
}
catch (OracleException ex)
{
    // Oracle hatası
    LogHata(ex);
    throw;
}
catch (Exception ex)
{
    // Genel hata
    LogHata(ex);
    throw;
}
finally
{
    // Cleanup
    conn?.Close();
}
```

### SQL Yazımı

#### Çok Satırlı SQL

```csharp
string sql = @"SELECT t.kategori,
                      v.versiyon,
                      v.tarih,
                      v.op_adi,
                      v.dokuman_no
               FROM das_dokuman d,
                    das_dokuman_versiyon v,
                    das_kategori_tip t
               WHERE d.dokuman_no = v.dokuman_no
                 AND d.aktif_versiyon = v.versiyon
                 AND d.ana_kategori_tip = t.ana_tip
                 AND d.kategori_tip = t.tip
                 AND d.ana_kategori_tip = " + lueAnaKategori.EditValue.ToString() + @"
                 AND d.takip_no = '" + txtTakipNo.Text + @"'
               ORDER BY t.kategori, v.op_adi";
```

#### UPDATE SQL

```csharp
string sql = @"UPDATE m_operator
               SET sube_kod = " + ucSubeKod.Text + @"
               WHERE op_adi = '" + FSubeDegistir.ekranPrm.kulFrm.OpAd + "'";
```

### String Concatenation

**YANLIŞ ✗**
```csharp
string sonuc = "";
for (int i = 0; i < 10; i++)
{
    sonuc += i.ToString(); // Her iterasyonda yeni string oluşturur
}
```

**DOĞRU ✓**
```csharp
StringBuilder sonuc = new StringBuilder();
for (int i = 0; i < 10; i++)
{
    sonuc.Append(i.ToString()); // Performanslı
}
```

## Form Görsel Tasarım Standartları

### Font Ayarları
- **Font:** Tahoma
- **Font-Size:** 8.25

### Renk Standartları
- **Info alanlar (readonly/disabled):** Web.LightYellow
- **Labellar:** Sağa veya sola yanaşık olabilir

## SPObject Katmanı Standartları

### Genel Kurallar

1. **OracleConnection Kurulumu:** Bu katmanda `OracleConnection` kurulmayacak. Connection bilgisi parametre olarak çağrıldığı yerden (Services veya Business Object) gönderilecektir.

2. **Rowtype ve Type Kullanımı:** .NET-Oracle tür uyuşmazlığı nedeniyle Oracle tarafındaki rowtype veya özel type return eden SP'ler body'si ile birlikte yazılacak. SELECT kısmı .NET tarafında yazılacak.

3. **İsim Uyumu:** Database'deki SP isimleriyle birebir aynı olmasına dikkat edilecek.

### Örnek SPObject Yapısı

```csharp
namespace MetinBank.Musteri.SP
{
    /// <summary>
    /// Müşteri Stored Procedure'leri
    /// Package: PKG_MUSTERI
    /// </summary>
    public static class SpMusteri
    {
        // Package ve Procedure isimleri
        public const string PKG_MUSTERI = "PKG_MUSTERI";
        public const string P_MUSTERI_EKLE = "P_MUSTERI_EKLE";
        public const string P_MUSTERI_GUNCELLE = "P_MUSTERI_GUNCELLE";
        
        /// <summary>
        /// Müşteri ekler
        /// Created by: Metin Dermencioğlu, 04/11/2025
        /// </summary>
        /// <param name="conn">Oracle Connection</param>
        /// <param name="trans">Oracle Transaction</param>
        /// <param name="tcKimlikNo">TC Kimlik No</param>
        /// <param name="ad">Ad</param>
        /// <param name="soyad">Soyad</param>
        /// <returns>Müşteri No</returns>
        public static long MusteriEkle(OracleConnection conn, OracleTransaction trans,
                                       string tcKimlikNo, string ad, string soyad)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = PKG_MUSTERI + "." + P_MUSTERI_EKLE;
            
            // Parametreler
            OracleParameter prmTcNo = new OracleParameter("p_tc_kimlik_no", OracleDbType.Varchar2);
            prmTcNo.Value = tcKimlikNo;
            cmd.Parameters.Add(prmTcNo);
            
            OracleParameter prmAd = new OracleParameter("p_ad", OracleDbType.Varchar2);
            prmAd.Value = ad;
            cmd.Parameters.Add(prmAd);
            
            OracleParameter prmSoyad = new OracleParameter("p_soyad", OracleDbType.Varchar2);
            prmSoyad.Value = soyad;
            cmd.Parameters.Add(prmSoyad);
            
            // Output parameter
            OracleParameter prmMusteriNo = new OracleParameter("p_musteri_no", OracleDbType.Int64);
            prmMusteriNo.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(prmMusteriNo);
            
            cmd.ExecuteNonQuery();
            
            return Convert.ToInt64(prmMusteriNo.Value.ToString());
        }
        
        /// <summary>
        /// Bakiye getirir (database'deki SP ismiyle aynı)
        /// </summary>
        public static decimal get_bakiye(OracleConnection conn, string hesapNo)
        {
            string sql = @"SELECT bakiye 
                          FROM hesaplar 
                          WHERE hesap_no = :hesap_no";
            
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("hesap_no", hesapNo));
            
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToDecimal(result) : 0;
        }
    }
}
```

## BusinessObject Katmanı Standartları

Bu katmanda, birden fazla servis(modül) tarafından kullanılacak, birden fazla SP objesini kullanan veya DML işlemi yapan anlamlı işlemler bütünü yazılır.

**Örnekler:** Havale, Kredi kartı ödemesi, T_MUHASEBE vb.

```csharp
namespace MetinBank.Transfer.Business
{
    /// <summary>
    /// Transfer Business Logic
    /// Created by: Metin Dermencioğlu, 04/11/2025
    /// </summary>
    public class BTransferIslem
    {
        /// <summary>
        /// Havale işlemi yapar
        /// </summary>
        public string HavaleYap(OracleConnection conn, OracleTransaction trans,
                               string gonderenHesap, string aliciHesap, decimal tutar)
        {
            try
            {
                // 1. Bakiye kontrolü
                decimal bakiye = SpHesap.get_bakiye(conn, gonderenHesap);
                if (bakiye < tutar)
                {
                    return "Yetersiz bakiye";
                }
                
                // 2. Gönderen hesaptan düş
                SpHesap.BakiyeDus(conn, trans, gonderenHesap, tutar);
                
                // 3. Alıcı hesaba ekle
                SpHesap.BakiyeEkle(conn, trans, aliciHesap, tutar);
                
                // 4. İşlem kaydı oluştur
                SpTransfer.IslemKaydet(conn, trans, gonderenHesap, aliciHesap, tutar);
                
                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
```

## Service Katmanı Standartları

Client tarafından ilgili modülle alakalı istekleri karşılayan katmandır.

### Kurallar:
1. Başka servislerden de kullanılma ihtimali varsa içerik Business Object katmanında yazılıp buradan çağrılır
2. Sadece bu servise özelse doğrudan bu katmanda geliştirilir

```csharp
namespace MetinBank.Musteri.Service
{
    /// <summary>
    /// Müşteri Service Katmanı
    /// Created by: Metin Dermencioğlu, 04/11/2025
    /// </summary>
    public class SMusteriService : IMusteriService
    {
        private OracleConnection _conn;
        
        public SMusteriService(string connectionString)
        {
            _conn = new OracleConnection(connectionString);
        }
        
        /// <summary>
        /// Yeni müşteri ekler
        /// </summary>
        public long MusteriEkle(Customer musteri)
        {
            OracleTransaction trans = null;
            try
            {
                _conn.Open();
                trans = _conn.BeginTransaction();
                
                // Business katmanını kullan (başka servisler de kullanabilir)
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
        
        /// <summary>
        /// Müşteri bulur (sadece bu servise özel)
        /// </summary>
        public Customer MusteriBul(long musteriNo)
        {
            try
            {
                _conn.Open();
                return SpMusteri.MusteriBul(_conn, musteriNo);
            }
            finally
            {
                _conn?.Close();
            }
        }
    }
}
```

## Dataset ve DataTable Standartları

### Performans Kuralları

1. **Tek satırlık kayıtlar:** Dataset ve DataTable nesneleri tek satırlık kayıt içerseler bile size'ları çok yüksektir. Mümkün mertebe client tarafına parametrelerle geçmekte fayda var.

2. **Network optimizasyonu:** Orta katman ile Oracle arasında aynı network'te olduğu için sorun yok.

3. **Kayıt sayısı kontrolü:** Client'a gönderilecek DataTable'larda mümkünse kayıt sayısı kontrolü konmalı.

```csharp
// Kayıt sayısı sınırlı
string sql = @"SELECT * FROM 
               (SELECT * FROM musteriler ORDER BY musteri_no DESC)
               WHERE ROWNUM < 100";

// Parametreli geçiş (tercih edilir)
public Customer MusteriGetir(long musteriNo)
{
    // DataTable yerine Entity dön
    return new Customer 
    {
        MusteriNo = musteriNo,
        Ad = "...",
        Soyad = "..."
    };
}
```

## Özet Kontrol Listesi

- [ ] Tüm kontroller belirlenen prefix'lerle isimlendirildi
- [ ] Namespace yapısı MetinBank.[Modül].[Katman] formatında
- [ ] Class isimleri F, S, I, B, Sp, H prefix'leriyle
- [ ] Method isimleri PascalCase
- [ ] Public değişkenler camelCase
- [ ] Private değişkenler _camelCase
- [ ] Property'ler PascalCase
- [ ] SP isimleri database ile birebir aynı
- [ ] Control Library property/metodları x ile başlıyor
- [ ] String concatenation'da StringBuilder kullanılıyor
- [ ] SQL'ler @ (verbatim string) ile yazılıyor
- [ ] Tüm metodlar XML comment ile dokümante edilmiş
- [ ] Exception handling standartlara uygun
- [ ] Font: Tahoma, Size: 8.25

---

**Son Güncelleme:** 4 Kasım 2025  
**Versiyon:** 1.0


