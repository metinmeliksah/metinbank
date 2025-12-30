using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using System.Threading;
using System.Threading.Tasks;

namespace MetinBank.Forms
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

        public FrmParaCek(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            
            // Canlı arama timer'ı
            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500; // 500ms bekle
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmParaCek_Load(object sender, EventArgs e)
        {
            this.Text = "Para Çek";
        }

        private void TxtMusteriArama_TextChanged(object sender, EventArgs e)
        {
            // Canlı arama - yazarken listele
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
                string hata = _sMusteri.MusteriAra(arama, out sonuclar);
                
                if (hata != null)
                {
                    gridMusteriler.DataSource = null;
                    return;
                }

                gridMusteriler.DataSource = sonuclar;
                gridViewMusteriler.BestFitColumns();
            }
            catch (Exception ex)
            {
                // Sessizce hata yok say
            }
        }

        private void GridViewMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object musteriIDObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriID");
                if (musteriIDObj == null || musteriIDObj == DBNull.Value) return;

                _seciliMusteriID = Convert.ToInt32(musteriIDObj);
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
                if (hesapIDObj == null || hesapIDObj == DBNull.Value) return;

                _seciliHesapID = Convert.ToInt32(hesapIDObj);
                
                object ibanObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "IBAN");
                object bakiyeObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "Bakiye");

                string iban = ibanObj != null && ibanObj != DBNull.Value ? ibanObj.ToString() : "";
                decimal bakiye = bakiyeObj != null && bakiyeObj != DBNull.Value ? Convert.ToDecimal(bakiyeObj) : 0;

                txtHesapID.Text = _seciliHesapID.ToString();
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

                long islemID;
                string hata = _sIslem.ParaCek(
                    _seciliHesapID,
                    numTutar.Value,
                    txtAciklama.Text,
                    _kullanici.KullaniciID,
                    _kullanici.SubeID.Value,
                    out islemID
                );

                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Para çekme işlemi başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {numTutar.Value:N2} TL", 
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Hesapları yenile
                HesaplariYukle();
                numTutar.Value = 0;
                txtAciklama.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
