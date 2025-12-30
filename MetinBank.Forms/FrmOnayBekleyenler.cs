using System;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using MetinBank.Models;
using MetinBank.Service;

namespace MetinBank.Forms
{
    public partial class FrmOnayBekleyenler : XtraForm
    {
        private KullaniciModel _kullanici;
        private SOnay _sOnay;

        public FrmOnayBekleyenler(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sOnay = new SOnay();
        }

        private void FrmOnayBekleyenler_Load(object sender, EventArgs e)
        {
            OnaylariYukle();
        }

        private void OnaylariYukle()
        {
            DataTable dt;
            string hata = _sOnay.OnayBekleyenler(_kullanici.RolAdi, out dt);
            
            if (hata != null)
            {
                MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gridOnaylar.DataSource = dt;
        }

        private void BtnOnayla_Click(object sender, EventArgs e)
        {
            if (dgvOnaylar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int onayLogID = Convert.ToInt32(dgvOnaylar.SelectedRows[0].Cells["OnayLogID"].Value);

            DialogResult result = MessageBox.Show("İşlemi onaylamak istediğinize emin misiniz?", 
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            string hata = _sOnay.IslemOnayla(onayLogID, _kullanici.KullaniciID);

            if (hata != null)
            {
                MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("İşlem başarıyla onaylandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnaylariYukle();
        }

        private void BtnReddet_Click(object sender, EventArgs e)
        {
            if (dgvOnaylar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string redNedeni = Microsoft.VisualBasic.Interaction.InputBox("Red nedeni giriniz:", "İşlem Reddet");

            if (string.IsNullOrWhiteSpace(redNedeni))
            {
                MessageBox.Show("Red nedeni girilmelidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int onayLogID = Convert.ToInt32(dgvOnaylar.SelectedRows[0].Cells["OnayLogID"].Value);

            string hata = _sOnay.IslemReddet(onayLogID, _kullanici.KullaniciID, redNedeni);

            if (hata != null)
            {
                MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("İşlem reddedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnaylariYukle();
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
