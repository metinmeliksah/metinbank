using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmVadesizHesapAc : XtraForm
    {
        private KullaniciModel _kullanici;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private int _seciliMusteriID;
        private string _seciliMusteriAd;
        private System.Windows.Forms.Timer _aramaTimer;

        public FrmVadesizHesapAc(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();

            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };

            txtMusteriArama.TextChanged += (s, e) => { _aramaTimer.Stop(); _aramaTimer.Start(); };
            gridViewMusteriler.RowClick += GridViewMusteriler_RowClick;
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

        private void BtnHesapAc_Click(object sender, EventArgs e)
        {
            if (_seciliMusteriID == 0)
            {
                XtraMessageBox.Show("Lütfen müşteri seçiniz.");
                return;
            }

            try
            {
                string pb = cmbParaBirimi.Text;
                HesapModel hesap = new HesapModel
                {
                    MusteriID = _seciliMusteriID,
                    HesapTipi = pb,
                    HesapCinsi = "Vadesiz",
                    FaizOrani = 0,
                    Bakiye = 0,
                    SubeID = _kullanici.SubeID ?? 1,
                    OlusturanKullaniciID = _kullanici.KullaniciID,
                    AcilisTarihi = DateTime.Now,
                    Durum = "Aktif"
                };

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
                    XtraMessageBox.Show($"Vadesiz Hesap Açıldı!\nIBAN: {yeniHesap?.IBAN}");
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
