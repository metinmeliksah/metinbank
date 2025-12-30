using System;
using System.Windows.Forms;
using MetinBank.Models;
using MetinBank.Service;

namespace MetinBank.Forms
{
    public partial class FrmParaYatir : Form
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;

        public FrmParaYatir(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtHesapID.Text))
                {
                    MessageBox.Show("Hesap ID giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numTutar.Value <= 0)
                {
                    MessageBox.Show("Tutar 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long islemID;
                string hata = _sIslem.ParaYatir(
                    int.Parse(txtHesapID.Text),
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

                MessageBox.Show($"Para yatırma işlemi başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {numTutar.Value:N2} TL", 
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnYatir_Click(object sender, EventArgs e)
        {
            BtnKaydet_Click(sender, e);
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmParaYatir_Load(object sender, EventArgs e)
        {
            this.Text = "Para Yatır";
        }
    }
}
