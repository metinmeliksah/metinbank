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
        private SSubeDegisiklik _sSubeDegisiklik;

        public FrmOnayBekleyenler(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
            _sKredi = new SKredi();
            _sSubeDegisiklik = new SSubeDegisiklik();
        }

        private void FrmOnayBekleyenler_Load(object sender, EventArgs e)
        {
            OnaylariYukle();
        }

        private void OnaylariYukle()
        {
            // 1. Para Transferleri (İşlemler)
            try
            {
                DataTable dtIslemler;
                string hata = _sIslem.OnayBekleyenIslemleriGetir(_kullanici.RolAdi, out dtIslemler);

                if (hata != null) XtraMessageBox.Show("İşlem listesi hatası: " + hata);
                else
                {
                    gridOnaylar.DataSource = dtIslemler;
                    ConfigGridIslemler();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("İşlem listesi yüklenirken hata: " + ex.Message);
            }

            // 2. Kredi Başvuruları
            try
            {
                DataTable dtKrediler = _sKredi.GetBekleyenBasvurular();
                gridKrediler.DataSource = dtKrediler;
                ConfigGridKrediler();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Kredi listesi hatası: " + ex.Message);
            }

            // 3. Şube Değişikliği Talepleri
            try
            {
                DataTable dtSubeDegisiklik;
                string hata = _sSubeDegisiklik.BekleyenTalepleriGetir(out dtSubeDegisiklik);
                
                if (hata != null) XtraMessageBox.Show("Şube değişiklik listesi hatası: " + hata);
                else
                {
                    gridSubeDegisiklik.DataSource = dtSubeDegisiklik;
                    ConfigGridSubeDegisiklik();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Şube değişiklik listesi yüklenirken hata: " + ex.Message);
            }

            ClearDetailPanel();
        }

        private void ConfigGridIslemler()
        {
            gridViewOnaylar.OptionsBehavior.Editable = false;
            gridViewOnaylar.OptionsView.ShowGroupPanel = false;

            // Tüm kolonları önce gizle
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewOnaylar.Columns)
            {
                col.Visible = false;
            }

            // İstenen kolonları göster ve sırala
            string[] visibleCols = { "IslemTanimi", "Tutar", "GonderenAdSoyad", "AliciAdSoyad", "OlusturanPersonel" };
            int visibleIndex = 0;
            
            if (gridViewOnaylar.Columns["IslemTanimi"] == null && gridViewOnaylar.Columns["IslemTipi"] != null)
                gridViewOnaylar.Columns["IslemTipi"].Visible = true; // Fallback
            
            foreach (string colName in visibleCols)
            {
                if (gridViewOnaylar.Columns[colName] != null)
                {
                    gridViewOnaylar.Columns[colName].Visible = true;
                    gridViewOnaylar.Columns[colName].VisibleIndex = visibleIndex++;
                }
            }

            // Başlıkları Ayarla
            if (gridViewOnaylar.Columns["IslemTanimi"] != null) gridViewOnaylar.Columns["IslemTanimi"].Caption = "İşlem Tipi";
            if (gridViewOnaylar.Columns["Tutar"] != null)
            {
                gridViewOnaylar.Columns["Tutar"].Caption = "Tutar";
                gridViewOnaylar.Columns["Tutar"].DisplayFormat.FormatString = "N2";
                gridViewOnaylar.Columns["Tutar"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            }
            if (gridViewOnaylar.Columns["GonderenAdSoyad"] != null) gridViewOnaylar.Columns["GonderenAdSoyad"].Caption = "Gönderen";
            if (gridViewOnaylar.Columns["AliciAdSoyad"] != null) gridViewOnaylar.Columns["AliciAdSoyad"].Caption = "Alıcı";
            if (gridViewOnaylar.Columns["OlusturanPersonel"] != null) gridViewOnaylar.Columns["OlusturanPersonel"].Caption = "İşlemi Yapan Personel";

            gridViewOnaylar.BestFitColumns();
        }

        private void ConfigGridKrediler()
        {
            gridViewKrediler.OptionsBehavior.Editable = false;
            gridViewKrediler.OptionsView.ShowGroupPanel = false;

            // Gizlenecek kolonlar
            string[] hiddenCols = { "BasvuruID", "MusteriID", "SubeID", "FaizOrani", "OnaylandiMi", "RedNedeni", "OnaylayanKullaniciID", "KullandirimTarihi" };
            foreach (string col in hiddenCols) { if (gridViewKrediler.Columns[col] != null) gridViewKrediler.Columns[col].Visible = false; }

            if (gridViewKrediler.Columns["TalepEdilenTutar"] != null)
            {
                gridViewKrediler.Columns["TalepEdilenTutar"].Caption = "Tutar";
                gridViewKrediler.Columns["TalepEdilenTutar"].DisplayFormat.FormatString = "N2";
                gridViewKrediler.Columns["TalepEdilenTutar"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            }
            if (gridViewKrediler.Columns["AdSoyad"] != null) gridViewKrediler.Columns["AdSoyad"].Caption = "Müşteri";
            if (gridViewKrediler.Columns["TalepEdilenVade"] != null) gridViewKrediler.Columns["TalepEdilenVade"].Caption = "Vade (Ay)";
            if (gridViewKrediler.Columns["BasvuruTarihi"] != null) gridViewKrediler.Columns["BasvuruTarihi"].Caption = "Tarih";

            gridViewKrediler.BestFitColumns();
        }

        private void ConfigGridSubeDegisiklik()
        {
            gridViewSubeDegisiklik.OptionsBehavior.Editable = false;
            gridViewSubeDegisiklik.OptionsView.ShowGroupPanel = false;

            // Gizlenecek kolonlar
            string[] hiddenCols = { "TalepID", "KullaniciID", "MevcutSubeID", "YeniSubeID", "OnaylayanKullaniciID", "OnayTarihi", "RedNedeni" };
            foreach (string col in hiddenCols) { if (gridViewSubeDegisiklik.Columns[col] != null) gridViewSubeDegisiklik.Columns[col].Visible = false; }

            // Caption ayarları
            if (gridViewSubeDegisiklik.Columns["TalepNedeni"] != null) gridViewSubeDegisiklik.Columns["TalepNedeni"].Caption = "Talep Nedeni";
            if (gridViewSubeDegisiklik.Columns["TalepTarihi"] != null) gridViewSubeDegisiklik.Columns["TalepTarihi"].Caption = "Tarih";
            if (gridViewSubeDegisiklik.Columns["KullaniciAdSoyad"] != null) gridViewSubeDegisiklik.Columns["KullaniciAdSoyad"].Caption = "Personel";
            if (gridViewSubeDegisiklik.Columns["MevcutSubeAdi"] != null) gridViewSubeDegisiklik.Columns["MevcutSubeAdi"].Caption = "Mevcut Şube";
            if (gridViewSubeDegisiklik.Columns["YeniSubeAdi"] != null) gridViewSubeDegisiklik.Columns["YeniSubeAdi"].Caption = "Hedef Şube";

            gridViewSubeDegisiklik.BestFitColumns();
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
            if (gridViewOnaylar.FocusedRowHandle < 0) { ClearDetailPanel(); return; }

            try
            {
                // Yeni sorgudan gelen alanlar
                object tip = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "IslemTanimi"); // veya IslemTipi
                object tutar = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "Tutar");
                object tarih = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "IslemTarihi");
                object yapan = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "OlusturanPersonel");
                
                object gonderenAd = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "GonderenAdSoyad");
                object gonderenIBAN = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "GonderenIBAN");
                object aliciAd = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "AliciAdSoyad");
                object aliciIBAN = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "AliciIBAN");

                lblIslemTipi.Text = $"İşlem: {tip}";
                lblTutar.Text = $"Tutar: {Convert.ToDecimal(tutar):N2} TL";
                lblTarih.Text = $"Tarih: {tarih}";
                lblOlusturan.Text = $"Yapan: {yapan}";

                // Detay panel başlığına ekstra bilgi ekleyelim veya label ekleyebiliriz ama 
                // şuan mevcut label'ları kullanarak zengin içerik gösterelim
                // LabelControl HTML formatlamayı destekliyorsa (AllowHtmlString) daha iyi olur ama standart text kullanalım.
                
                // NOT: Mevcut Label'lar kısıtlı, GroupControl textine detayları koyalım veya tooltipe
                string detayMetni = $"📤 Gönderen: {gonderenAd}\n({gonderenIBAN})\n\n📥 Alıcı: {aliciAd}\n({aliciIBAN})";
                
                // GroupControl'un text'ini kullanarak pratik bir çözüm
                grpDetay.Text = $"📋 Detay: {gonderenAd} ➡️ {aliciAd}";
                
                // Tooltip atamaları hatalı olduğu için kaldırıldı.
                // Detaylar zaten panel başlığında gösteriliyor.
                grpDetay.Text = $"📋 Detay: {gonderenAd} ➡️ {aliciAd}";
            }
            catch { }
        }

        private void GridViewKrediler_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (tabControl.SelectedTabPage != tabKrediler) return;
            UpdateDetailFromGrid(gridViewKrediler, "Kanal", "TalepEdilenTutar", "BasvuruTarihi", "AdSoyad");
            lblIslemTipi.Text = "İşlem Tipi: Kredi Başvurusu";
        }

        private void GridViewSubeDegisiklik_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (tabControl.SelectedTabPage != tabSubeDegisiklik) return;
            // Şube değişikliğinde Tutar yoktur, onun yerine Hedef Şube'yi gösterelim
            if (gridViewSubeDegisiklik.FocusedRowHandle < 0) { ClearDetailPanel(); return; }

            try
            {
                object personel = gridViewSubeDegisiklik.GetRowCellValue(gridViewSubeDegisiklik.FocusedRowHandle, "KullaniciAdSoyad");
                object hedefSube = gridViewSubeDegisiklik.GetRowCellValue(gridViewSubeDegisiklik.FocusedRowHandle, "YeniSubeAdi");
                object tarih = gridViewSubeDegisiklik.GetRowCellValue(gridViewSubeDegisiklik.FocusedRowHandle, "TalepTarihi");
                object neden = gridViewSubeDegisiklik.GetRowCellValue(gridViewSubeDegisiklik.FocusedRowHandle, "TalepNedeni");

                lblIslemTipi.Text = "İşlem: Şube Değişikliği";
                lblTutar.Text = $"Hedef: {hedefSube}"; // Tutar label'ını hedef şube için kullanıyoruz
                lblTarih.Text = $"Tarih: {tarih}";
                lblOlusturan.Text = $"Personel: {personel}";
                
                // Tooltip ile nedeni göster
                grpDetay.Text = $"📋 Detay: {neden}";
            }
            catch { }
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
                grpDetay.Text = "📋 İşlem Detayı";
            }
            catch { }
        }

        private void BtnOnayla_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTabPage == tabIslemler)
            {
                IslemOnayla();
            }
            else if (tabControl.SelectedTabPage == tabKrediler)
            {
                KrediOnayla();
            }
            else if (tabControl.SelectedTabPage == tabSubeDegisiklik)
            {
                SubeDegisiklikOnayla();
            }
        }

        private void BtnReddet_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTabPage == tabIslemler)
            {
                IslemReddet();
            }
            else if (tabControl.SelectedTabPage == tabKrediler)
            {
                KrediReddet();
            }
            else if (tabControl.SelectedTabPage == tabSubeDegisiklik)
            {
                SubeDegisiklikReddet();
            }
        }

        private void IslemOnayla()
        {
            if (gridViewOnaylar.FocusedRowHandle < 0) return;
            long id = Convert.ToInt64(gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "IslemID"));

            if (XtraMessageBox.Show("Transfer işlemini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string hata = _sIslem.IslemOnayla(id, _kullanici.KullaniciID, _kullanici.RolAdi);
                if (hata != null) XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else { XtraMessageBox.Show("İşlem onaylandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); OnaylariYukle(); }
            }
        }

        private void IslemReddet()
        {
            if (gridViewOnaylar.FocusedRowHandle < 0) return;
            long id = Convert.ToInt64(gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "IslemID"));
            string neden = XtraInputBox.Show("Red sebebi:", "Red", "");
            if (string.IsNullOrEmpty(neden)) return;

            string hata = _sIslem.IslemReddet(id, _kullanici.KullaniciID, neden);
            if (hata != null) XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else { XtraMessageBox.Show("İşlem reddedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); OnaylariYukle(); }
        }

        private void KrediOnayla()
        {
            if (gridViewKrediler.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridViewKrediler.GetRowCellValue(gridViewKrediler.FocusedRowHandle, "BasvuruID"));

            if (XtraMessageBox.Show("Kredi başvurusunu onaylıyor musunuz?\nPara müşterinin hesabına geçecektir.", "Kredi Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                // 1. Durumu ONAYLANDI yap
                string hata = _sKredi.BasvuruOnaylaReddet(id, true, _kullanici.KullaniciID);
                if (hata != null)
                {
                    XtraMessageBox.Show("Onay hatası: " + hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // 2. Krediyi Kullandır (Para hesaba geçer)
                    _sKredi.KrediKullandir(id);
                    XtraMessageBox.Show("Kredi onaylandı ve kullandırıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnaylariYukle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Kredi kullandırılırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void KrediReddet()
        {
            if (gridViewKrediler.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridViewKrediler.GetRowCellValue(gridViewKrediler.FocusedRowHandle, "BasvuruID"));
            string neden = XtraInputBox.Show("Red sebebi:", "Kredi Red", "");
            if (string.IsNullOrEmpty(neden)) return;

            string hata = _sKredi.BasvuruOnaylaReddet(id, false, _kullanici.KullaniciID, neden);
            if (hata != null) XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else { XtraMessageBox.Show("Başvuru reddedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); OnaylariYukle(); }
        }

        private void SubeDegisiklikOnayla()
        {
            if (gridViewSubeDegisiklik.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridViewSubeDegisiklik.GetRowCellValue(gridViewSubeDegisiklik.FocusedRowHandle, "TalepID"));

            if (XtraMessageBox.Show("Şube değişikliği talebini onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string hata = _sSubeDegisiklik.TalepOnayla(id, _kullanici.KullaniciID);
                if (hata != null) XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else { XtraMessageBox.Show("Talep onaylandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); OnaylariYukle(); }
            }
        }

        private void SubeDegisiklikReddet()
        {
            if (gridViewSubeDegisiklik.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridViewSubeDegisiklik.GetRowCellValue(gridViewSubeDegisiklik.FocusedRowHandle, "TalepID"));
            string neden = XtraInputBox.Show("Red sebebi:", "Red", "");
            if (string.IsNullOrEmpty(neden)) return;

            string hata = _sSubeDegisiklik.TalepReddet(id, _kullanici.KullaniciID, neden);
            if (hata != null) XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else { XtraMessageBox.Show("Talep reddedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); OnaylariYukle(); }
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
