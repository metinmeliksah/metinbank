using System;
using System.Windows.Forms;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Forms
{
    public partial class FrmHavaleEFT : Form
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;

        public FrmHavaleEFT(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
        }

        private void FrmHavaleEFT_Load(object sender, EventArgs e)
        {
            cmbIslemTipi.Properties.Items.AddRange(new string[] { "Havale", "EFT" });
            cmbIslemTipi.SelectedIndex = 0;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtKaynakHesapID.Text))
                {
                    MessageBox.Show("Kaynak hesap ID giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        int.Parse(txtKaynakHesapID.Text),
                        txtHedefIBAN.Text,
                        numTutar.Value,
                        txtAciklama.Text,
                        txtAliciAdi.Text,
                        _kullanici.KullaniciID,
                        _kullanici.SubeID.Value,
                        out islemID
                    );
                }
                else
                {
                    hata = _sIslem.EFT(
                        int.Parse(txtKaynakHesapID.Text),
                        txtHedefIBAN.Text,
                        numTutar.Value,
                        txtAciklama.Text,
                        txtAliciAdi.Text,
                        _kullanici.KullaniciID,
                        _kullanici.SubeID.Value,
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

                this.Close();
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
