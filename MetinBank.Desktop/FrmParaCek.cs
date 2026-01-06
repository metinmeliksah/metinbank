using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmParaCek : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private int _seciliMusteriID;
        private int _seciliHesapID;
        private System.Windows.Forms.Timer _aramaTimer;
        private bool _isGenelMerkez;

        public FrmParaCek(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            
            // Genel Merkez kontrolü
            _isGenelMerkez = _kullanici.RolAdi != null && 
                            (_kullanici.RolAdi.Contains("Genel") || _kullanici.RolAdi.Contains("Merkez"));
            
            // Canlı arama timer'ı
            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmParaCek_Load(object sender, EventArgs e)
        {
            this.Text = "Para Çek";
            
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
                gridHesaplar.DataSource = null;
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
                // Şube bazlı arama
                string hata = _sMusteri.MusteriAra(arama, _kullanici.SubeID, _isGenelMerkez, out sonuclar);
                
                if (hata != null)
                {
                    gridMusteriler.DataSource = null;
                    return;
                }

                gridMusteriler.DataSource = sonuclar;
                gridViewMusteriler.BestFitColumns();
                
                // Para yatır/çek için gösterilecek sütunlar: MusteriNo, TCKN, AdSoyad, CepTelefon, Email, MusteriTipi, MusteriSegmenti
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewMusteriler.Columns)
                {
                    string fieldName = col.FieldName;
                    if (fieldName != "MusteriNo" && fieldName != "TCKN" && fieldName != "AdSoyad" && 
                        fieldName != "Ad" && fieldName != "Soyad" &&
                        fieldName != "CepTelefon" && fieldName != "Email" && 
                        fieldName != "MusteriTipi" && fieldName != "MusteriSegmenti")
                    {
                        col.Visible = false;
                    }
                }
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
                if (_seciliMusteriID == 0) return;
                HesaplariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception ex)
            {
                gridHesaplar.DataSource = null;
            }
        }

        private void GridViewHesaplar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object hesapIDObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "HesapID");
                _seciliHesapID = CommonFunctions.DbNullToInt(hesapIDObj);
                if (_seciliHesapID == 0) return;
                
                object hesapNoObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "HesapNo");
                object ibanObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "IBAN");
                object bakiyeObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "Bakiye");

                string hesapNo = CommonFunctions.DbNullToString(hesapNoObj);
                string iban = CommonFunctions.DbNullToString(ibanObj);
                decimal bakiye = CommonFunctions.DbNullToDecimal(bakiyeObj);

                txtHesapID.Text = hesapNo;
                txtIBAN.Text = iban;
                txtBakiye.Text = bakiye.ToString("N2") + " TL";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCek_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seciliMusteriID == 0)
                {
                    MessageBox.Show("Lütfen önce bir müşteri seçiniz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_seciliHesapID == 0)
                {
                    MessageBox.Show("Lütfen bir hesap seçiniz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numTutar.Value <= 0)
                {
                    MessageBox.Show("Tutar 0'dan büyük olmalıdır.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // UI kilitlenmesini önle
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // SubeID null ise varsayılan değer kullan
                int subeID = _kullanici.SubeID ?? 1;

                long islemID;
                string hata = _sIslem.ParaCek(
                    _seciliHesapID,
                    numTutar.Value,
                    txtAciklama.Text,
                    _kullanici.KullaniciID,
                    subeID,
                    out islemID
                );

                this.Cursor = Cursors.Default;

                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (numTutar.Value > 50000)
                {
                    MessageBox.Show($"Para çekme isteği alındı.\n\nİşlem limit (50.000 TL) üzerinde olduğu için yönetici onayına düşmüştür.\nİşlem No: TRX{islemID}", 
                        "Onay Bekliyor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Para çekme işlemi başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {numTutar.Value:N2} TL", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Hesapları yenile
                Application.DoEvents();
                HesaplariYukle();
                numTutar.Value = 0;
                txtAciklama.Text = "";
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
