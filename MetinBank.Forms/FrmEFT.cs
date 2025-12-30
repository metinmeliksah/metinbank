using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Forms
{
    public partial class FrmEFT : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private int _seciliMusteriID;
        private int _seciliHesapID;
        private System.Windows.Forms.Timer _aramaTimer;

        public FrmEFT(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            
            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmEFT_Load(object sender, EventArgs e)
        {
            this.Text = "EFT";
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
                string hata = _sMusteri.MusteriAra(arama, out sonuclar);
                
                if (hata != null)
                {
                    gridMusteriler.DataSource = null;
                    return;
                }

                gridMusteriler.DataSource = sonuclar;
                gridViewMusteriler.BestFitColumns();
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
            }
            catch
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
                
                object ibanObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "IBAN");
                object bakiyeObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "Bakiye");

                string iban = CommonFunctions.DbNullToString(ibanObj);
                decimal bakiye = CommonFunctions.DbNullToDecimal(bakiyeObj);

                txtKaynakHesapID.Text = _seciliHesapID.ToString();
                txtKaynakIBAN.Text = iban;
                txtKaynakBakiye.Text = bakiye.ToString("N2") + " TL";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seciliHesapID == 0)
                {
                    MessageBox.Show("Lütfen kaynak hesap seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHedefIBAN.Text))
                {
                    MessageBox.Show("Hedef IBAN giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numTutar.Value <= 0)
                {
                    MessageBox.Show("Tutar 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ibanHata = IbanHelper.ValidateIban(txtHedefIBAN.Text);
                if (ibanHata != null)
                {
                    MessageBox.Show(ibanHata, "Geçersiz IBAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!_kullanici.SubeID.HasValue)
                {
                    MessageBox.Show("Kullanıcının şube bilgisi bulunamadı.", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                long islemID;
                string hata = _sIslem.EFT(
                    _seciliHesapID,
                    txtHedefIBAN.Text,
                    numTutar.Value,
                    txtAciklama.Text,
                    txtAliciAdi.Text,
                    _kullanici.KullaniciID,
                    _kullanici.SubeID.Value,
                    out islemID
                );

                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string onayMesaji = numTutar.Value > 5000 ? "\n\nNOT: İşlem onay bekliyor." : "";
                MessageBox.Show($"EFT işlemi başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {numTutar.Value:N2} TL{onayMesaji}", 
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HesaplariYukle();
                numTutar.Value = 0;
                txtAciklama.Text = "";
                txtAliciAdi.Text = "";
                txtHedefIBAN.Text = "";
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

