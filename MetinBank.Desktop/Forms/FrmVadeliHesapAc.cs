using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmVadeliHesapAc : XtraForm
    {
        private KullaniciModel _kullanici;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private SMevduat _sMevduat;
        private SIslem _sIslem;
        private int _seciliMusteriID;
        private string _seciliMusteriAd;
        private System.Windows.Forms.Timer _aramaTimer;
        
        // Hesaplanan değerler
        private decimal _faizOrani;

        public FrmVadeliHesapAc(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            _sMevduat = new SMevduat();
            _sIslem = new SIslem();

            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };

            txtMusteriArama.TextChanged += (s, e) => { _aramaTimer.Stop(); _aramaTimer.Start(); };
            gridViewMusteriler.RowClick += GridViewMusteriler_RowClick;
            
            btnHesapla.Click += BtnHesapla_Click;
            btnHesapAc.Click += BtnHesapAc_Click;
            rgOdemeYontemi.SelectedIndexChanged += RgOdemeYontemi_SelectedIndexChanged;
        }

        // Kaynak hesap seçimi için yardımcı sınıf
        private class HesapItem
        {
            public int HesapID { get; set; }
            public string DisplayText { get; set; }
            public HesapItem(int id, string text) { HesapID = id; DisplayText = text; }
            public override string ToString() { return DisplayText; }
        }

        private void RgOdemeYontemi_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isVirman = rgOdemeYontemi.SelectedIndex == 1;
            lblKaynakHesap.Visible = isVirman;
            cmbKaynakHesap.Visible = isVirman;
            
            if (isVirman && _seciliMusteriID > 0)
            {
                MusterininVadesizHesaplariniYukle();
            }
        }
        
        private void MusterininVadesizHesaplariniYukle()
        {
            try
            {
                cmbKaynakHesap.Properties.Items.Clear();
                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(_seciliMusteriID, out hesaplar);
                if (hata == null && hesaplar != null)
                {
                    foreach (DataRow row in hesaplar.Rows)
                    {
                        string hesapCinsi = row["HesapCinsi"].ToString();
                        if (hesapCinsi == "Vadesiz")
                        {
                            string hesapTipi = row["HesapTipi"].ToString();
                            decimal bakiye = Convert.ToDecimal(row["Bakiye"]);
                            int hesapID = Convert.ToInt32(row["HesapID"]);
                            string iban = row["IBAN"].ToString();
                            cmbKaynakHesap.Properties.Items.Add(new HesapItem(hesapID, $"{hesapTipi} - {iban} ({bakiye:N2} {hesapTipi})"));
                        }
                    }
                }
                if (cmbKaynakHesap.Properties.Items.Count > 0)
                    cmbKaynakHesap.SelectedIndex = 0;
            }
            catch { }
        }

        private void MusteriAra()
        {
            try
            {
                string arama = txtMusteriArama.Text.Trim();
                if (arama.Length < 2) return;
                DataTable dt;
                _sMusteri.MusteriAra(arama, _kullanici.SubeID, false, out dt);
                gridMusteriler.DataSource = dt;
                gridViewMusteriler.BestFitColumns();
                
                // Sadece MusteriNo, TCKN, Ad, Soyad görünsün - diğer sütunları gizle (AdSoyad da gizli)
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewMusteriler.Columns)
                {
                    string fieldName = col.FieldName;
                    if (fieldName != "MusteriNo" && fieldName != "TCKN" && 
                        fieldName != "Ad" && fieldName != "Soyad")
                    {
                        col.Visible = false;
                    }
                }
            }
            catch { }
        }

        private void GridViewMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            _seciliMusteriID = Convert.ToInt32(gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriID"));
            string ad = gridViewMusteriler.GetRowCellValue(e.RowHandle, "Ad").ToString();
            string soyad = gridViewMusteriler.GetRowCellValue(e.RowHandle, "Soyad").ToString();
            _seciliMusteriAd = ad + " " + soyad;
            lblSeciliMusteri.Text = "Seçili: " + _seciliMusteriAd;
            
            // Eğer virman seçiliyse hesapları yükle
            if (rgOdemeYontemi.SelectedIndex == 1)
            {
                MusterininVadesizHesaplariniYukle();
            }
        }

        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                decimal tutar = Convert.ToDecimal(txtTutar.EditValue);
                int gun = Convert.ToInt32(txtGun.Text);
                string pb = cmbParaBirimi.Text;

                if (tutar <= 0) { XtraMessageBox.Show("Tutar giriniz."); return; }
                if (gun <= 0) { XtraMessageBox.Show("Vade (gün) giriniz."); return; }

                var sonuc = _sMevduat.HesaplaGetiri(tutar, gun, pb);
                if (sonuc.ContainsKey("Hata"))
                {
                    XtraMessageBox.Show(sonuc["Hata"].ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    groupSonuc.Visible = false;
                    return;
                }

                _faizOrani = (decimal)sonuc["FaizOrani"];
                decimal netKazanc = (decimal)sonuc["NetGetiri"];
                decimal toplam = (decimal)sonuc["ToplamEleGecen"];
                decimal stopajOran = (decimal)sonuc["StopajOrani"];

                lblFaiz.Text = $"Brüt Faiz: %{_faizOrani} (Stopaj: %{stopajOran})";
                lblNetKazanc.Text = $"Net Kazanç: {netKazanc:N2} {pb}";
                lblToplam.Text = $"Vade Sonu: {toplam:N2} {pb}";
                
                // Vade tarihleri göster
                DateTime baslangic = DateTime.Now;
                DateTime bitis = DateTime.Now.AddDays(gun);
                if (lblFaiz.Tag == null) lblFaiz.Tag = lblFaiz.Text;
                lblFaiz.Text = $"Brüt Faiz: %{_faizOrani} (Stopaj: %{stopajOran})\nVade: {baslangic:dd.MM.yyyy} - {bitis:dd.MM.yyyy}";

                    groupSonuc.Visible = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hesaplama hatası: " + ex.Message);
            }
        }

        private void BtnHesapAc_Click(object sender, EventArgs e)
        {
            if (_seciliMusteriID == 0)
            {
                XtraMessageBox.Show("Lütfen müşteri seçiniz.");
                return;
            }

            try
            {
                decimal tutar = Convert.ToDecimal(txtTutar.EditValue);
                int gun = Convert.ToInt32(txtGun.Text);
                string pb = cmbParaBirimi.Text;

                HesapModel hesap = new HesapModel
                {
                    MusteriID = _seciliMusteriID,
                    HesapTipi = pb,
                    HesapCinsi = "Vadeli",
                    FaizOrani = _faizOrani,
                    Bakiye = tutar,
                    SubeID = _kullanici.SubeID ?? 1,
                    OlusturanKullaniciID = _kullanici.KullaniciID,
                    VadeTarihi = DateTime.Now.AddDays(gun),
                    AcilisTarihi = DateTime.Now,
                    Durum = "Aktif"
                };

                // Ödeme yöntemi kontrolü
                bool isVirman = rgOdemeYontemi.SelectedIndex == 1;
                int kaynakHesapID = 0;
                
                if (isVirman)
                {
                    if (cmbKaynakHesap.SelectedIndex < 0)
                    {
                        XtraMessageBox.Show("Lütfen kaynak hesap seçiniz.");
                        return;
                    }
                    var seciliHesap = cmbKaynakHesap.SelectedItem as HesapItem;
                    if (seciliHesap == null)
                    {
                        XtraMessageBox.Show("Geçersiz kaynak hesap.");
                        return;
                    }
                    kaynakHesapID = seciliHesap.HesapID;
                    
                    // Bakiye kontrol
                    HesapModel kaynakHesapModel;
                    string bakiyeHata = _sHesap.HesapGetir(kaynakHesapID, out kaynakHesapModel);
                    if (bakiyeHata != null || kaynakHesapModel == null)
                    {
                        XtraMessageBox.Show("Kaynak hesap bulunamadı.");
                        return;
                    }
                    if (kaynakHesapModel.KullanilabilirBakiye < tutar)
                    {
                        XtraMessageBox.Show($"Yetersiz bakiye. Kullanılabilir: {kaynakHesapModel.KullanilabilirBakiye:N2} {kaynakHesapModel.HesapTipi}");
                        return;
                    }
                }

                // Vadeli hesap aç
                int hesapID;
                string hata = _sHesap.HesapAc(hesap, out hesapID);
                if (hata != null)
                {
                    XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Virman ise kaynak hesaptan düş
                if (isVirman && kaynakHesapID > 0)
                {
                    long islemID;
                    string virmanHata = _sIslem.Virman(kaynakHesapID, hesapID, tutar, "Vadeli hesap açılış virmanı", _kullanici.KullaniciID, _kullanici.SubeID ?? 1, out islemID);
                    if (virmanHata != null)
                    {
                        XtraMessageBox.Show($"Hesap açıldı ancak virman hatası: {virmanHata}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    HesapModel yeniHesap;
                    _sHesap.HesapGetir(hesapID, out yeniHesap);
                    XtraMessageBox.Show($"Vadeli Hesap Açıldı!\nIBAN: {yeniHesap?.IBAN}\nVade Sonu: {hesap.VadeTarihi:dd.MM.yyyy}");
                    
                    // Formu sıfırla, kapatma
                    _seciliMusteriID = 0;
                    _seciliMusteriAd = "";
                    _faizOrani = 0;
                    lblSeciliMusteri.Text = "Seçili: -";
                    txtMusteriArama.Text = "";
                    gridMusteriler.DataSource = null;
                    txtTutar.EditValue = null;
                    txtGun.Text = "";
                    cmbParaBirimi.SelectedIndex = 0;
                    groupSonuc.Visible = false;
                    lblFaiz.Text = "Faiz Oranı: %0.00";
                    lblNetKazanc.Text = "Net Kazanç: 0.00 TL";
                    lblToplam.Text = "Vade Sonu Toplam: 0.00 TL";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hesap açılış hatası: " + ex.Message);
            }
        }
    }
}
