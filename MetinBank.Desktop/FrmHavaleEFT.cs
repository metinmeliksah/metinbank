using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmHavaleEFT : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private MusteriModel _seciliMusteri;
        private int _seciliHesapID;

        public FrmHavaleEFT(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
        }

        private void FrmHavaleEFT_Load(object sender, EventArgs e)
        {
            cmbIslemTipi.Properties.Items.AddRange(new string[] { "Havale", "EFT" });
            cmbIslemTipi.SelectedIndex = 0;
        }

        private void BtnMusteriAra_Click(object sender, EventArgs e)
        {
            MusteriAra();
        }

        private void MusteriAra()
        {
            try
            {
                string arama = txtMusteriArama.Text.Trim();
                if (string.IsNullOrWhiteSpace(arama))
                {
                    MessageBox.Show("Lütfen müşteri numarası, TCKN veya ad soyad giriniz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable sonuclar;
                string hata = _sMusteri.MusteriAra(arama, out sonuclar);
                
                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridMusteriler.DataSource = sonuclar;
                gridViewMusteriler.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                int musteriID = Convert.ToInt32(gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriID"));
                
                MusteriModel musteri;
                string hata = _sMusteri.MusteriGetir(musteriID, out musteri);
                
                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _seciliMusteri = musteri;
                MusteriBilgileriniGoster();
                HesaplariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MusteriBilgileriniGoster()
        {
            if (_seciliMusteri == null) return;

            txtMusteriAd.Text = _seciliMusteri.Ad;
            txtMusteriSoyad.Text = _seciliMusteri.Soyad;
            txtMusteriTCKN.Text = _seciliMusteri.TCKN.ToString();
        }

        private void HesaplariYukle()
        {
            try
            {
                if (_seciliMusteri == null) return;

                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(_seciliMusteri.MusteriID, out hesaplar);
                
                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridHesaplar.DataSource = hesaplar;
                gridViewHesaplar.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridViewHesaplar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                _seciliHesapID = Convert.ToInt32(gridViewHesaplar.GetRowCellValue(e.RowHandle, "HesapID"));
                string iban = gridViewHesaplar.GetRowCellValue(e.RowHandle, "IBAN")?.ToString();
                decimal bakiye = Convert.ToDecimal(gridViewHesaplar.GetRowCellValue(e.RowHandle, "Bakiye"));

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

                // IBAN doğrulama
                string ibanHata = IbanHelper.ValidateIban(txtHedefIBAN.Text);
                if (ibanHata != null)
                {
                    MessageBox.Show(ibanHata, "Geçersiz IBAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                long islemID;
                string hata = null;

                if (cmbIslemTipi.SelectedItem.ToString() == "Havale")
                {
                    hata = _sIslem.Havale(
                        _seciliHesapID,
                        txtHedefIBAN.Text,
                        numTutar.Value,
                        txtAciklama.Text,
                        txtAliciAdi.Text,
                        _kullanici.KullaniciID,
                        _kullanici.SubeID.Value,
                        0m,
                        out islemID
                    );
                }
                else
                {
                    hata = _sIslem.EFT(
                        _seciliHesapID,
                        txtHedefIBAN.Text,
                        numTutar.Value,
                        txtAciklama.Text,
                        txtAliciAdi.Text,
                        _kullanici.KullaniciID,
                        _kullanici.SubeID.Value,
                        0m,
                        out islemID
                    );
                }

                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string onayMesaji = numTutar.Value > 5000 ? "\n\nNOT: İşlem onay bekliyor." : "";
                MessageBox.Show($"İşlem başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {numTutar.Value:N2} TL{onayMesaji}", 
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Hesapları yenile
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

        private void TxtMusteriArama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnMusteriAra_Click(sender, e);
            }
        }
    }
}
