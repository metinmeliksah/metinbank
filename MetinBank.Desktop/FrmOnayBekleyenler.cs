using System;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Business;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmOnayBekleyenler : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;
        private SKredi _sKredi;

        public FrmOnayBekleyenler(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
            _sKredi = new SKredi();
        }

        private void FrmOnayBekleyenler_Load(object sender, EventArgs e)
        {
            OnaylariYukle();
        }

        private void OnaylariYukle()
        {
            // 1. Para Transferleri (İşlemler)
            DataTable dtIslemler;
            string hata = _sIslem.OnayBekleyenIslemleriGetir(_kullanici.RolAdi, out dtIslemler);
            
            if (hata != null) XtraMessageBox.Show("İşlem listesi hatası: " + hata);
            else 
            {
                gridOnaylar.DataSource = dtIslemler;
                ConfigGridIslemler();
            }

            // 2. Kredi Başvuruları
            try
            {
                DataTable dtKrediler = _sKredi.GetBekleyenBasvurular();
                gridKrediler.DataSource = dtKrediler;
                ConfigGridKrediler();
            }
            catch(Exception ex) 
            { 
                 XtraMessageBox.Show("Kredi listesi hatası: " + ex.Message);
            }

            ClearDetailPanel();
        }

        private void ConfigGridIslemler()
        {
            gridViewOnaylar.OptionsBehavior.Editable = false;
            gridViewOnaylar.OptionsView.ShowGroupPanel = false;
            
            string[] hiddenCols = { "IslemID", "KaynakHesapID", "HedefHesapID", "KullaniciID", "SubeID", "IPAdresi", "IslemCikisTarihi", "BasariliMi", "ParaBirimi", "IslemUcreti", "IslemTipi", "HedefIBAN", "IslemReferansNo", "AliciAdi" };
            foreach (string col in hiddenCols) { if (gridViewOnaylar.Columns[col] != null) gridViewOnaylar.Columns[col].Visible = false; }
            
            if (gridViewOnaylar.Columns["Tutar"] != null) gridViewOnaylar.Columns["Tutar"].DisplayFormat.FormatString = "N2";
            
            gridViewOnaylar.BestFitColumns();
        }

        private void ConfigGridKrediler()
        {
            gridViewKrediler.OptionsBehavior.Editable = false;
            gridViewKrediler.OptionsView.ShowGroupPanel = false;
            
            // Kolonlar: BasvuruID, MusteriID, AdSoyad, Tutar, Vade, Faiz, ...
            if (gridViewKrediler.Columns["BasvuruID"] != null) gridViewKrediler.Columns["BasvuruID"].Visible = false;
            if (gridViewKrediler.Columns["MusteriID"] != null) gridViewKrediler.Columns["MusteriID"].Visible = false;
            
            if (gridViewKrediler.Columns["TalepEdilenTutar"] != null) 
            {
                gridViewKrediler.Columns["TalepEdilenTutar"].Caption = "Tutar";
                gridViewKrediler.Columns["TalepEdilenTutar"].DisplayFormat.FormatString = "N2";
            }
            if (gridViewKrediler.Columns["AdSoyad"] != null) gridViewKrediler.Columns["AdSoyad"].Caption = "Müşteri";
            if (gridViewKrediler.Columns["TalepEdilenVade"] != null) gridViewKrediler.Columns["TalepEdilenVade"].Caption = "Vade (Ay)";
            if (gridViewKrediler.Columns["BasvuruTarihi"] != null) gridViewKrediler.Columns["BasvuruTarihi"].Caption = "Tarih";

            gridViewKrediler.BestFitColumns();
            
            gridViewKrediler.FocusedRowChanged += GridViewKrediler_FocusedRowChanged;
        }

        private void ClearDetailPanel()
        {
            lblIslemTipi.Text = "İşlem Tipi: -";
            lblTutar.Text = "Tutar: -";
            lblTarih.Text = "Tarih: -";
            lblOlusturan.Text = "Kişi: -";
        }

        private void GridViewOnaylar_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (tabControl.SelectedTabPage != tabIslemler) return;
            UpdateDetailFromGrid(gridViewOnaylar, "IslemTipi", "Tutar", "IslemTarihi", "MusteriAdSoyad");
        }
        
        private void GridViewKrediler_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (tabControl.SelectedTabPage != tabKrediler) return;
            UpdateDetailFromGrid(gridViewKrediler, "Kanal", "TalepEdilenTutar", "BasvuruTarihi", "AdSoyad");
            lblIslemTipi.Text = "İşlem Tipi: Kredi Başvurusu";
        }

        private void UpdateDetailFromGrid(DevExpress.XtraGrid.Views.Grid.GridView view, string colTip, string colTutar, string colTarih, string colKisi)
        {
            if (view.FocusedRowHandle < 0) { ClearDetailPanel(); return; }
            
            try
            {
                object tip = view.GetRowCellValue(view.FocusedRowHandle, colTip);
                object tutar = view.GetRowCellValue(view.FocusedRowHandle, colTutar);
                object tarih = view.GetRowCellValue(view.FocusedRowHandle, colTarih);
                object kisi = view.GetRowCellValue(view.FocusedRowHandle, colKisi);

                lblIslemTipi.Text = colTip == "Kanal" ? "Kanal: " + tip : "İşlem: " + tip;
                lblTutar.Text = $"Tutar: {Convert.ToDecimal(tutar):N2} TL";
                lblTarih.Text = $"Tarih: {tarih}";
                lblOlusturan.Text = $"Kişi: {kisi}";
            }
            catch {}
        }

        private void BtnOnayla_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTabPage == tabIslemler)
            {
                IslemOnayla();
            }
            else
            {
                KrediOnayla();
            }
        }

        private void BtnReddet_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTabPage == tabIslemler)
            {
                IslemReddet();
            }
            else
            {
                KrediReddet();
            }
        }
        
        private void IslemOnayla()
        {
             if (gridViewOnaylar.FocusedRowHandle < 0) return;
             long id = Convert.ToInt64(gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "IslemID"));
             
             if (XtraMessageBox.Show("Transfer işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
             {
                 string hata = _sIslem.IslemOnayla(id, _kullanici.KullaniciID, _kullanici.RolAdi);
                 if (hata != null) XtraMessageBox.Show(hata);
                 else { XtraMessageBox.Show("Onaylandı."); OnaylariYukle(); }
             }
        }
        
        private void IslemReddet()
        {
             if (gridViewOnaylar.FocusedRowHandle < 0) return;
             long id = Convert.ToInt64(gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "IslemID"));
             string neden = XtraInputBox.Show("Red sebebi:", "Red", "");
             if(string.IsNullOrEmpty(neden)) return;

             string hata = _sIslem.IslemReddet(id, _kullanici.KullaniciID, neden);
             if (hata != null) XtraMessageBox.Show(hata);
             else { XtraMessageBox.Show("Reddedildi."); OnaylariYukle(); }
        }

        private void KrediOnayla()
        {
            if (gridViewKrediler.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridViewKrediler.GetRowCellValue(gridViewKrediler.FocusedRowHandle, "BasvuruID"));

            if (XtraMessageBox.Show("Kredi başvurusunu onaylıyor musunuz?\nPara müşterinin hesabına geçecektir.", "Kredi Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                // 1. Önce Durumu ONAYLANDI yap
                string hata = _sKredi.BasvuruOnaylaReddet(id, true, _kullanici.KullaniciID);
                if (hata != null) 
                {
                    XtraMessageBox.Show("Onay hatası: " + hata);
                    return;
                }
                
                // 2. Krediyi Kullandır (Para hesaba geçer, ödeme planı oluşur)
                // SKredi'de KrediKullandir metodu lazım. BKredi'de var.
                // Eğer KrediKullandir çağrılmazsa sadece onaylı kalır. Kullanıcı "Kullandır" mı demeli yoksa Onay=Kullandır mı?
                // Genelde Onay sonrası sözleşme vs olur ama burada "Onay" direkt parayı yatırsın.
                
                try 
                {
                    // BKredi instance'ına erişim yok, SSc aracılığıyla yapmalıyız.
                    // SKredi sınıfına KrediKullandir metodunu açmamışız.
                    // Ancak Business katmanında OnaylaReddet sadece durumu değiştiriyor.
                    // Biz Onay'dan sonra otomatik kullandırım istiyoruz.
                    // Hızlıca BKredi logic'ini çağırabilmeliyiz. 
                    // SKredi'yi update etmek yerine reflection veya direkt Business referansı (zaten using var) ile yapalım mı? Hayır, Service üzerinden gidelim.
                    // SKredi'ye KrediKullandir eklemek en doğrusu ama dosyayı değiştirmek uzun.
                    // BKredi'yi direkt burada instantiate edebiliriz (Using MetinBank.Business var).
                    
                    BKredi bKredi = new BKredi();
                    bKredi.KrediKullandir(id); // Bu metodu daha önce BKredi public yapmıştık.
                    
                    XtraMessageBox.Show("Kredi onaylandı ve kullandırıldı.", "Başarılı");
                    OnaylariYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Kredi kullandırılırken hata oluştu: " + ex.Message);
                }
            }
        }

        private void KrediReddet()
        {
            if (gridViewKrediler.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridViewKrediler.GetRowCellValue(gridViewKrediler.FocusedRowHandle, "BasvuruID"));
            string neden = XtraInputBox.Show("Red sebebi:", "Kredi Red", "");
             if(string.IsNullOrEmpty(neden)) return;

            string hata = _sKredi.BasvuruOnaylaReddet(id, false, _kullanici.KullaniciID, neden);
            if (hata != null) XtraMessageBox.Show(hata);
            else { XtraMessageBox.Show("Başvuru reddedildi."); OnaylariYukle(); }
        }

        private void BtnYenile_Click(object sender, EventArgs e)
        {
            OnaylariYukle();
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
