using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Forms
{
    public partial class FrmHesapIslem : XtraForm
    {
        private KullaniciModel _kullanici;
        private SHesap _sHesap;

        public FrmHesapIslem(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sHesap = new SHesap();
        }

        private void FrmHesapIslem_Load(object sender, EventArgs e)
        {
            cmbHesapTipi.Properties.Items.AddRange(new string[] { "TL", "USD", "EUR" });
            cmbHesapCinsi.Properties.Items.AddRange(new string[] { "Vadesiz", "Vadeli", "Maas", "Yatirim" });
            cmbHesapTipi.SelectedIndex = 0;
            cmbHesapCinsi.SelectedIndex = 0;
        }

        private void BtnHesapAc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMusteriID.Text))
                {
                    MessageBox.Show("Müşteri ID giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                HesapModel hesap = new HesapModel
                {
                    MusteriID = int.Parse(txtMusteriID.Text),
                    HesapTipi = cmbHesapTipi.SelectedItem.ToString(),
                    HesapCinsi = cmbHesapCinsi.SelectedItem.ToString(),
                    FaizOrani = numFaizOrani.Value,
                    SubeID = _kullanici.SubeID.Value,
                    OlusturanKullaniciID = _kullanici.KullaniciID,
                    GunlukTransferLimiti = 20000,
                    AylikTransferLimiti = 500000
                };

                int hesapID;
                string hata = _sHesap.HesapAc(hesap, out hesapID);

                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hesap bilgilerini göster
                HesapModel yeniHesap;
                hata = _sHesap.HesapGetir(hesapID, out yeniHesap);

                if (hata == null)
                {
                    txtIBAN.Text = yeniHesap.IBANFormatli;
                    MessageBox.Show($"Hesap başarıyla açıldı!\n\nIBAN: {yeniHesap.IBANFormatli}", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
            