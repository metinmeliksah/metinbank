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
                    .Replace(" ", "").Replace("-", "").Trim();
                
                // Sadece rakamları al
                string cepTelefonDigits = "";
                foreach (char c in cepTelefonRaw)
                {
                    if (char.IsDigit(c)) cepTelefonDigits += c;
                }

                // 0 ile başlayıp 11 hane kontrolü
                if (cepTelefonDigits.Length == 10 && !cepTelefonDigits.StartsWith("0"))
                {
                    // 10 hane ama 0 ile başlamıyor - başına 0 ekle
                    cepTelefonDigits = "0" + cepTelefonDigits;
                }

                if (cepTelefonDigits.Length != 11 || !cepTelefonDigits.StartsWith("0"))
                {
                    XtraMessageBox.Show("Cep telefon numarası 0 ile başlayan 11 haneli olmalıdır.\nÖrnek: 0532 123 45 67", 
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCepTelefon.Focus();
                    return;
                }

                // Ev telefonu temizle (opsiyonel)
                string telefonRaw = txtTelefon.Text
                    .Replace("(", "").Replace(")", "")
                    .Replace(" ", "").Replace("-", "").Trim();
                
                string telefonDigits = "";
                foreach (char c in telefonRaw)
                {
                    if (char.IsDigit(c)) telefonDigits += c;
                }
                
                // Eğer 10 hane ise başına 0 ekle
                if (telefonDigits.Length == 10 && !telefonDigits.StartsWith("0"))
                {
                    telefonDigits = "0" + telefonDigits;
                }

                // Create customer model - telefonu 0 ile başlayan 11 hane olarak sakla
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
                    Telefon = string.IsNullOrEmpty(telefonDigits) || telefonDigits.Length < 10 ? "" : telefonDigits,
                    CepTelefon = cepTelefonDigits,
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
