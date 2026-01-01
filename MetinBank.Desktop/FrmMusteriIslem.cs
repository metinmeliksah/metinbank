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
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            // Configure column headers
            gridViewMusteriler.OptionsView.ColumnAutoWidth = true;
            gridViewMusteriler.BestFitColumns();
        }

        private void MusterileriYukle()
        {
            DataTable dt;
            string hata = _sMusteri.MusterileriGetir(out dt);
            if (hata != null)
            {
                XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvMusteriler.DataSource = dt;
            gridViewMusteriler.BestFitColumns();
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
                XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvMusteriler.DataSource = dt;
            gridViewMusteriler.BestFitColumns();
        }

        private void TxtArama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnAra_Click(sender, e);
            }
        }

        private void BtnYeniMusteri_Click(object sender, EventArgs e)
        {
            FrmMusteriEkle frm = new FrmMusteriEkle(_kullanici);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MusterileriYukle();
            }
        }

        private void BtnDuzenle_Click(object sender, EventArgs e)
        {
            if (gridViewMusteriler.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Lütfen düzenlemek istediğiniz müşteriyi seçin.", 
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Open edit form with selected customer
            int musteriID = Convert.ToInt32(gridViewMusteriler.GetRowCellValue(gridViewMusteriler.FocusedRowHandle, "MusteriID"));
            XtraMessageBox.Show($"Müşteri düzenleme formu açılacak. Müşteri ID: {musteriID}", 
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
