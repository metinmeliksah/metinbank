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

            // Rol kontrolü - Sadece Müdür ve Genel Merkez erişebilir
            if (!IsMudurOrGenelMerkez())
            {
                XtraMessageBox.Show("Bu sayfaya erişim yetkiniz bulunmamaktadır.\nSadece Müdür ve Genel Merkez yetkilileri müşteri listesini görüntüleyebilir.",
                    "Erişim Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Load += (s, e) => this.Close();
            }
        }

        private bool IsMudurOrGenelMerkez()
        {
            string rol = _kullanici.RolAdi;
            return rol.IndexOf("Müdür", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   rol.IndexOf("Mudur", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   rol.IndexOf("Genel Merkez", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   rol.IndexOf("Merkez", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool IsGenelMerkez()
        {
            string rol = _kullanici.RolAdi;
            return rol.IndexOf("Genel Merkez", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   rol.IndexOf("Genel", StringComparison.OrdinalIgnoreCase) >= 0;
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
            
            // MusteriID kolonunu gizle
            if (gridViewMusteriler.Columns["MusteriID"] != null)
            {
                gridViewMusteriler.Columns["MusteriID"].Visible = false;
            }
            
            if (gridViewMusteriler.Columns["KayitSubeID"] != null)
            {
                gridViewMusteriler.Columns["KayitSubeID"].Visible = false;
            }
            
            // Header görünürlüğünü runtime'da ayarla (okunaklı renkler)
            gridViewMusteriler.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            gridViewMusteriler.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            gridViewMusteriler.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            gridViewMusteriler.Appearance.HeaderPanel.Options.UseBackColor = true;
            gridViewMusteriler.Appearance.HeaderPanel.Options.UseForeColor = true;
            gridViewMusteriler.Appearance.HeaderPanel.Options.UseFont = true;
        }

        private void MusterileriYukle()
        {
            DataTable dt;
            string hata;

            // Genel Merkez tüm müşterileri görebilir, Müdür sadece kendi şubesini
            if (IsGenelMerkez())
            {
                hata = _sMusteri.MusterileriGetir(out dt);
            }
            else
            {
                // Müdür - Sadece kendi şubesindeki müşteriler
                if (!_kullanici.SubeID.HasValue)
                {
                    XtraMessageBox.Show("Kullanıcı şube bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                hata = _sMusteri.SubeninMusterileri(_kullanici.SubeID.Value, out dt);
            }

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
            string hata;

            // Şube bazlı arama
            if (IsGenelMerkez())
            {
                // Genel Merkez - Tüm müşterilerde ara
                hata = _sMusteri.MusteriAra(txtArama.Text, null, true, out dt);
            }
            else
            {
                // Müdür - Sadece kendi şubesinde ara
                if (!_kullanici.SubeID.HasValue)
                {
                    XtraMessageBox.Show("Kullanıcı şube bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                hata = _sMusteri.MusteriAra(txtArama.Text, _kullanici.SubeID.Value, false, out dt);
            }

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
