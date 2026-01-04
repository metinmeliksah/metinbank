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
                    Bakiye = tutar, // Başlangıç bakiyesi
                    SubeID = _kullanici.SubeID ?? 1,
                    OlusturanKullaniciID = _kullanici.KullaniciID,
                    VadeTarihi = DateTime.Now.AddDays(gun),
                    AcilisTarihi = DateTime.Now,
                    Durum = "Aktif"
                };

                // Not: Gerçek senaryoda bu parayı başka hesaptan düşmek gerekir ama
                // Şube operasyonunda 'Nakit' yatırılıyor varsayıyoruz (yada gişeci elle işlem yapacak).
                // Basitlik için direkt açıyoruz.

                int hesapID;
                string hata = _sHesap.HesapAc(hesap, out hesapID);
                if (hata != null)
                {
                    XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    HesapModel yeniHesap;
                    _sHesap.HesapGetir(hesapID, out yeniHesap);
                    XtraMessageBox.Show($"Vadeli Hesap Açıldı!\nIBAN: {yeniHesap?.IBAN}\nVade Sonu: {hesap.VadeTarihi:dd.MM.yyyy}");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hesap açılış hatası: " + ex.Message);
            }
        }
    }
}
