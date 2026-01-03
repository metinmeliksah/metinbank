using System;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmOnayBekleyenler : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;

        public FrmOnayBekleyenler(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
        }

        private void FrmOnayBekleyenler_Load(object sender, EventArgs e)
        {
            OnaylariYukle();
        }

        private void OnaylariYukle()
        {
            DataTable dt;
            string hata = _sIslem.OnayBekleyenIslemleriGetir(_kullanici.RolAdi, out dt);
            
            if (hata != null)
            {
                XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gridOnaylar.DataSource = dt;
            gridViewOnaylar.BestFitColumns();
            
            // Gizlenmesi gereken kolonlar
            string[] hiddenCols = { "IslemID", "KaynakHesapID", "HedefHesapID", "KullaniciID", "SubeID", "IPAdresi", "IslemCikisTarihi", "BasariliMi" };
            foreach (string col in hiddenCols)
            {
                if (gridViewOnaylar.Columns[col] != null)
                    gridViewOnaylar.Columns[col].Visible = false;
            }

            // Clear detail panel
            ClearDetailPanel();
        }

        private void ClearDetailPanel()
        {
            lblIslemTipi.Text = "İşlem Tipi: -";
            lblTutar.Text = "Tutar: -";
            lblTarih.Text = "Tarih: -";
            lblOlusturan.Text = "Oluşturan: -";
        }

        private void GridViewOnaylar_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle < 0)
                {
                    ClearDetailPanel();
                    return;
                }

                // Get row data and update detail panel
                object islemTipiObj = gridViewOnaylar.GetRowCellValue(e.FocusedRowHandle, "IslemTipi");
                object tutarObj = gridViewOnaylar.GetRowCellValue(e.FocusedRowHandle, "Tutar");
                object tarihObj = gridViewOnaylar.GetRowCellValue(e.FocusedRowHandle, "IslemTarihi");
                object olusturanObj = gridViewOnaylar.GetRowCellValue(e.FocusedRowHandle, "MusteriAdSoyad"); // Updated query returns this

                string islemTipi = CommonFunctions.DbNullToString(islemTipiObj);
                decimal tutar = CommonFunctions.DbNullToDecimal(tutarObj);
                string tarih = tarihObj != DBNull.Value && tarihObj != null 
                    ? Convert.ToDateTime(tarihObj).ToString("dd.MM.yyyy HH:mm") 
                    : "-";
                string olusturan = CommonFunctions.DbNullToString(olusturanObj);

                lblIslemTipi.Text = $"İşlem Tipi: {islemTipi}";
                lblTutar.Text = $"Tutar: {tutar:N2} TL";
                lblTarih.Text = $"Tarih: {tarih}";
                lblOlusturan.Text = $"Müşteri: {olusturan}";
            }
            catch
            {
                ClearDetailPanel();
            }
        }

        private void BtnOnayla_Click(object sender, EventArgs e)
        {
            if (gridViewOnaylar.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object islemIDObj = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "IslemID");
            long islemID = islemIDObj != DBNull.Value ? Convert.ToInt64(islemIDObj) : 0;

            if (islemID == 0)
            {
                XtraMessageBox.Show("Geçersiz kayıt.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = XtraMessageBox.Show("İşlemi onaylamak istediğinize emin misiniz?", 
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            string hata = _sIslem.IslemOnayla(islemID, _kullanici.KullaniciID, _kullanici.RolAdi);

            if (hata != null)
            {
                XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XtraMessageBox.Show("İşlem başarıyla onaylandı (veya bir sonraki onaya gönderildi)!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnaylariYukle();
        }

        private void BtnReddet_Click(object sender, EventArgs e)
        {
            if (gridViewOnaylar.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Use DevExpress input dialog
            string redNedeni = XtraInputBox.Show("Red nedeni giriniz:", "İşlem Reddet", "");

            if (string.IsNullOrWhiteSpace(redNedeni))
            {
                XtraMessageBox.Show("Red nedeni girilmelidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object islemIDObj = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "IslemID");
            long islemID = islemIDObj != DBNull.Value ? Convert.ToInt64(islemIDObj) : 0;

            if (islemID == 0)
            {
                XtraMessageBox.Show("Geçersiz kayıt.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hata = _sIslem.IslemReddet(islemID, _kullanici.KullaniciID, redNedeni);

            if (hata != null)
            {
                XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XtraMessageBox.Show("İşlem reddedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnaylariYukle();
        }

        private void BtnYenile_Click(object sender, EventArgs e)
        {
            OnaylariYukle();
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
