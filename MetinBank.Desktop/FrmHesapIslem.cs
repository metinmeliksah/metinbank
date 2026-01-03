using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmHesapIslem : XtraForm
    {
        private KullaniciModel _kullanici;
        private SHesap _sHesap;
        private SMusteri _sMusteri;
        private int _seciliMusteriID;
        private string _seciliMusteriAd;
        private System.Windows.Forms.Timer _aramaTimer;

        public FrmHesapIslem(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sHesap = new SHesap();
            _sMusteri = new SMusteri();
            
            // Live search timer
            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmHesapIslem_Load(object sender, EventArgs e)
        {
            cmbHesapTipi.SelectedIndex = 0; // TL
            cmbHesapCinsi.SelectedIndex = 0; // Vadesiz
            numFaizOrani.Value = 0;
            
            // Grid ayarları
            gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            gridViewHesaplar.OptionsView.ShowGroupPanel = false;
        }
        
        /// <summary>
        /// ID sütunlarını gizler
        /// </summary>
        private void GizliSutunlariAyarla(DevExpress.XtraGrid.Views.Grid.GridView gridView, params string[] sutunlar)
        {
            foreach (string sutun in sutunlar)
            {
                if (gridView.Columns[sutun] != null)
                    gridView.Columns[sutun].Visible = false;
            }
        }

        private void TxtMusteriArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMusteriArama.Text))
            {
                gridMusteriler.DataSource = null;
                return;
            }

            _aramaTimer.Stop();
            _aramaTimer.Start();
        }

        private void MusteriAra()
        {
            try
            {
                string arama = txtMusteriArama.Text.Trim();
                if (string.IsNullOrWhiteSpace(arama) || arama.Length < 2)
                {
                    gridMusteriler.DataSource = null;
                    return;
                }

                DataTable sonuclar;
                string hata = _sMusteri.MusteriAra(arama, out sonuclar);
                
                if (hata != null)
                {
                    gridMusteriler.DataSource = null;
                    return;
                }

                gridMusteriler.DataSource = sonuclar;
                gridViewMusteriler.BestFitColumns();
                
                // ID sütunlarını gizle
                GizliSutunlariAyarla(gridViewMusteriler, "MusteriID");
            }
            catch
            {
                gridMusteriler.DataSource = null;
            }
        }

        private void GridViewMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object musteriIDObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriID");
                _seciliMusteriID = CommonFunctions.DbNullToInt(musteriIDObj);
                
                object adObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "Ad");
                object soyadObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "Soyad");
                object musteriNoObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriNo");
                
                string ad = CommonFunctions.DbNullToString(adObj);
                string soyad = CommonFunctions.DbNullToString(soyadObj);
                string musteriNo = CommonFunctions.DbNullToString(musteriNoObj);
                
                _seciliMusteriAd = $"{ad} {soyad} (#{musteriNo})";
                lblSeciliMusteri.Text = $"✅ {_seciliMusteriAd}";
                
                if (_seciliMusteriID > 0)
                {
                    HesaplariYukle();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HesaplariYukle()
        {
            try
            {
                if (_seciliMusteriID == 0) return;

                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(_seciliMusteriID, out hesaplar);
                
                if (hata != null)
                {
                    gridHesaplar.DataSource = null;
                    return;
                }

                gridHesaplar.DataSource = hesaplar;
                gridViewHesaplar.BestFitColumns();
                
                // ID sütunlarını gizle
                GizliSutunlariAyarla(gridViewHesaplar, "HesapID", "MusteriID");
            }
            catch
            {
                gridHesaplar.DataSource = null;
            }
        }

        private void BtnHesapAc_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seciliMusteriID == 0)
                {
                    XtraMessageBox.Show("Lütfen önce bir müşteri seçiniz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                HesapModel hesap = new HesapModel
                {
                    MusteriID = _seciliMusteriID,
                    HesapTipi = cmbHesapTipi.Text,
                    HesapCinsi = cmbHesapCinsi.Text,
                    FaizOrani = numFaizOrani.Value,
                    SubeID = _kullanici.SubeID ?? 1,
                    OlusturanKullaniciID = _kullanici.KullaniciID,
                    GunlukTransferLimiti = 20000,
                    AylikTransferLimiti = 500000
                };

                int hesapID;
                string hata = _sHesap.HesapAc(hesap, out hesapID);

                if (hata != null)
                {
                    XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get and show new account IBAN
                HesapModel yeniHesap;
                hata = _sHesap.HesapGetir(hesapID, out yeniHesap);

                if (hata == null && yeniHesap != null)
                {
                    txtIBAN.Text = yeniHesap.IBANFormatli;
                    XtraMessageBox.Show(
                        $"Hesap başarıyla açıldı!\n\n" +
                        $"Müşteri: {_seciliMusteriAd}\n" +
                        $"IBAN: {yeniHesap.IBANFormatli}\n" +
                        $"Hesap Tipi: {cmbHesapTipi.Text} - {cmbHesapCinsi.Text}", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Refresh accounts list
                    HesaplariYukle();
                }
                else
                {
                     XtraMessageBox.Show($"Hesap açıldı ancak bilgileri getirilemedi: {hata}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     HesaplariYukle();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}