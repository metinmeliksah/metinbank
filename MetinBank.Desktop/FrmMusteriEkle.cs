using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;

namespace MetinBank.Desktop
{
    public partial class FrmMusteriEkle : XtraForm
    {
        private readonly KullaniciModel _kullanici;
        private readonly SMusteri _sMusteri;

        public FrmMusteriEkle(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
        }

        private void FrmMusteriEkle_Load(object sender, EventArgs e)
        {
            // Use BeginInvoke to ensure form is fully loaded before initializing controls
            this.BeginInvoke(new Action(() =>
            {
                // Default values with null checks
                if (cmbMusteriTipi.Properties.Items.Count > 0)
                    cmbMusteriTipi.SelectedIndex = 0; // Bireysel
                if (cmbMusteriSegmenti.Properties.Items.Count > 0)
                    cmbMusteriSegmenti.SelectedIndex = 0; // Standart
                if (cmbCinsiyet.Properties.Items.Count > 0)
                    cmbCinsiyet.SelectedIndex = 0; // Erkek
                if (cmbMedeniDurum.Properties.Items.Count > 0)
                    cmbMedeniDurum.SelectedIndex = 0; // Bekar
                dtDogumTarihi.DateTime = DateTime.Now.AddYears(-25);
                
                // Ensure layout is correct
                this.layoutControl1.Refresh();
            }));
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validations
                if (string.IsNullOrWhiteSpace(txtTCKN.Text))
                {
                    XtraMessageBox.Show("TCKN alanı zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTCKN.Focus();
                    return;
                }

                if (txtTCKN.Text.Length != 11)
                {
                    XtraMessageBox.Show("TCKN 11 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTCKN.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAd.Text))
                {
                    XtraMessageBox.Show("Ad alanı zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAd.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSoyad.Text))
                {
                    XtraMessageBox.Show("Soyad alanı zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoyad.Focus();
                    return;
                }

                // Cep telefon validasyonu - mask karakterlerini temizle
                string cepTelefonRaw = txtCepTelefon.Text
                    .Replace("(", "").Replace(")", "")
                    .Replace(" ", "").Trim();
                
                // Başındaki 0'ı kaldır (maskeden geliyorsa)
                if (cepTelefonRaw.StartsWith("0"))
                    cepTelefonRaw = cepTelefonRaw.Substring(1);

                if (string.IsNullOrWhiteSpace(cepTelefonRaw) || cepTelefonRaw.Length != 10)
                {
                    XtraMessageBox.Show("Cep telefon numarası 10 haneli olmalıdır (05XX...)", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCepTelefon.Focus();
                    return;
                }

                // Ev telefonu temizle (opsiyonel)
                string telefonRaw = txtTelefon.Text
                    .Replace("(", "").Replace(")", "")
                    .Replace(" ", "").Trim();
                
                if (telefonRaw.StartsWith("0"))
                    telefonRaw = telefonRaw.Substring(1);

                // Create customer model
                MusteriModel musteri = new MusteriModel
                {
                    TCKN = long.Parse(txtTCKN.Text.Replace(" ", "")),
                    Ad = txtAd.Text.Trim(),
                    Soyad = txtSoyad.Text.Trim(),
                    DogumTarihi = dtDogumTarihi.DateTime,
                    DogumYeri = txtDogumYeri.Text.Trim(),
                    Cinsiyet = cmbCinsiyet.Text,
                    MedeniDurum = cmbMedeniDurum.Text,
                    AnneAdi = txtAnneAdi.Text.Trim(),
                    BabaAdi = txtBabaAdi.Text.Trim(),
                    Telefon = string.IsNullOrEmpty(telefonRaw) ? "" : "90" + telefonRaw,
                    CepTelefon = "90" + cepTelefonRaw,
                    Email = txtEmail.Text.Trim(),
                    Adres = txtAdres.Text.Trim(),
                    Il = txtIl.Text.Trim(),
                    Ilce = txtIlce.Text.Trim(),
                    PostaKodu = txtPostaKodu.Text.Trim(),
                    MusteriTipi = cmbMusteriTipi.Text,
                    MusteriSegmenti = cmbMusteriSegmenti.Text,
                    MeslekBilgisi = txtMeslekBilgisi.Text.Trim(),
                    GelirDurumu = numGelirDurumu.Value,
                    KayitSubeID = _kullanici.SubeID ?? 1,
                    AktifMi = true
                };

                // Save customer
                int musteriID;
                string hata = _sMusteri.MusteriEkle(musteri, out musteriID);

                if (hata != null)
                {
                    XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XtraMessageBox.Show($"Müşteri başarıyla eklendi!\n\nMüşteri No: {musteriID}", 
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (FormatException)
            {
                XtraMessageBox.Show("TCKN sadece rakamlardan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTCKN.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnIptal_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("İşlemi iptal etmek istediğinize emin misiniz?", 
                "İptal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
