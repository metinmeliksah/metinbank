using System;
using System.Windows.Forms;
using MetinBank.Models;

namespace MetinBank.Forms
{
    public partial class FrmParaCek : Form
    {
        private readonly KullaniciModel _kullanici;

        public FrmParaCek(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
        }

        private void FrmParaCek_Load(object sender, EventArgs e)
        {
            this.Text = "Para Çek";
            MessageBox.Show("Para çekme formu henüz tamamlanmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

