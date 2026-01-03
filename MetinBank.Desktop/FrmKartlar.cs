using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmKartlar : XtraForm
    {
        private KullaniciModel _kullanici;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private SKart _sKart;
        private int _seciliMusteriID;
        private System.Windows.Forms.Timer _aramaTimer;
        private bool _isGenelMerkez;

        public FrmKartlar(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            _sKart = new SKart();
            
            // Genel Merkez kontrolü
            _isGenelMerkez = _kullanici.RolAdi != null && 
                            (_kullanici.RolAdi.Contains("Genel") || _kullanici.RolAdi.Contains("Merkez"));
            
            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmKartlar_Load(object sender, EventArgs e)
        {
            this.Text = "Kartlar";
            
            // Grid ayarları
            gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            gridViewKartlar.OptionsView.ShowGroupPanel = false;
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
                gridKartlar.DataSource = null;
                return;
            }

            _aramaTimer.Stop();
            _aramaTimer.Start();
        }

        private void GridViewMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object musteriIDObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriID");
                _seciliMusteriID = CommonFunctions.DbNullToInt(musteriIDObj);
                if (_seciliMusteriID == 0) return;
                KartlariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KartlariYukle()
        {
            try
            {
                if (_seciliMusteriID == 0)
                {
                    gridKartlar.DataSource = null;
                    return;
                }

                DataTable dt;
                string hata = _sKart.GetMusteriKartlari(_seciliMusteriID, out dt);

                if (hata != null)
                {
                    gridKartlar.DataSource = null;
                    return;
                }

                gridKartlar.DataSource = dt;
                gridViewKartlar.BestFitColumns();
                
                // ID sütunlarını gizle
                GizliSutunlariAyarla(gridViewKartlar, "KartID", "HesapID", "MusteriID");
                    
                // Kart numarasını maskele (ilk 6 + son 4)
                if (gridViewKartlar.Columns["KartNo"] != null)
                {
                    gridViewKartlar.Columns["KartNo"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    gridViewKartlar.CustomColumnDisplayText += (s, args) => {
                        if (args.Column.FieldName == "KartNo" && args.Value != null)
                        {
                            string kartNo = args.Value.ToString();
                            if (kartNo.Length >= 16)
                            {
                                args.DisplayText = kartNo.Substring(0, 6) + " **** **** " + kartNo.Substring(12, 4);
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                gridKartlar.DataSource = null;
            }
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
                
                // ID sütunlarını gizle
                GizliSutunlariAyarla(gridViewMusteriler, "MusteriID", "KayitSubeID");
            }
            catch
            {
                gridMusteriler.DataSource = null;
            }
        }

        private void BtnYenile_Click(object sender, EventArgs e)
        {
            KartlariYukle();
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Kart iptal işlemi
        /// </summary>
        public void KartIptalEt()
        {
            try
            {
                if (gridViewKartlar.RowCount == 0)
                {
                    MessageBox.Show("İptal edilecek kart bulunamadı.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedRow = gridViewKartlar.FocusedRowHandle;
                if (selectedRow < 0)
                {
                    MessageBox.Show("Lütfen iptal edilecek kartı seçiniz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                object kartIDObj = gridViewKartlar.GetRowCellValue(selectedRow, "KartID");
                int kartID = CommonFunctions.DbNullToInt(kartIDObj);

                if (kartID == 0)
                {
                    MessageBox.Show("Geçersiz kart seçimi.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Onay iste
                DialogResult result = MessageBox.Show(
                    "Seçili kartı iptal etmek istediğinize emin misiniz?\n\n" +
                    "Bu işlem geri alınamaz!",
                    "Kart İptal",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result != DialogResult.Yes)
                    return;

                string hata = _sKart.CancelCard(kartID, _kullanici.KullaniciID);

                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Kart başarıyla iptal edildi.", "Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                KartlariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
