using System;
using System.Windows.Forms;
using MetinBank.Models;

namespace MetinBank.Forms
{
    public partial class FrmMusteriEkle : Form
    {
        private readonly KullaniciModel _kullanici;

        public FrmMusteriEkle(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
        }

        private void FrmMusteriEkle_Load(object sender, EventArgs e)
        {
            this.Text = "Yeni Müşteri Ekle";
            MessageBox.Show("Müşteri ekleme formu henüz tamamlanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

