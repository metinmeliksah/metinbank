using System;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using MetinBank.Models;
using MetinBank.Service;

namespace MetinBank.Desktop
{
    public partial class FrmMusteriIslem : XtraForm
    {
        private KullaniciModel _kullanici;
        private SMusteri _sMusteri;

        public FrmMusteriIslem(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
        }

        private void FrmMusteriIslem_Load(object sender, EventArgs e)
        {
            MusterileriYukle();
        }

        private void MusterileriYukle()
        {
            DataTable dt;
            string hata = _sMusteri.MusterileriGetir(out dt);
            if (hata != null)
            {
                MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvMusteriler.DataSource = dt;
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArama.Text))
            {
                MusterileriYukle();
                return;
            }

            DataTable dt;
            string hata = _sMusteri.MusteriAra(txtArama.Text, out dt);
            if (hata != null)
            {
                MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvMusteriler.DataSource = dt;
        }

        private void BtnYeniMusteri_Click(object sender, EventArgs e)
        {
            FrmMusteriEkle frm = new FrmMusteriEkle(_kullanici);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MusterileriYukle();
            }
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
