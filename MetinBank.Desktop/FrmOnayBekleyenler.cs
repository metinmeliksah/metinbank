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
                XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gridOnaylar.DataSource = dt;
            gridViewOnaylar.BestFitColumns();
            
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
                object tarihObj = gridViewOnaylar.GetRowCellValue(e.FocusedRowHandle, "OlusturmaTarihi");
                object olusturanObj = gridViewOnaylar.GetRowCellValue(e.FocusedRowHandle, "OlusturanKullanici");

                string islemTipi = CommonFunctions.DbNullToString(islemTipiObj);
                decimal tutar = CommonFunctions.DbNullToDecimal(tutarObj);
                string tarih = tarihObj != DBNull.Value && tarihObj != null 
                    ? Convert.ToDateTime(tarihObj).ToString("dd.MM.yyyy HH:mm") 
                    : "-";
                string olusturan = CommonFunctions.DbNullToString(olusturanObj);

                lblIslemTipi.Text = $"İşlem Tipi: {islemTipi}";
                lblTutar.Text = $"Tutar: {tutar:N2} TL";
                lblTarih.Text = $"Tarih: {tarih}";
                lblOlusturan.Text = $"Oluşturan: {olusturan}";
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

            object onayLogIDObj = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "OnayLogID");
            int onayLogID = CommonFunctions.DbNullToInt(onayLogIDObj);

            if (onayLogID == 0)
            {
                XtraMessageBox.Show("Geçersiz kayıt.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = XtraMessageBox.Show("İşlemi onaylamak istediğinize emin misiniz?", 
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            string hata = _sOnay.IslemOnayla(onayLogID, _kullanici.KullaniciID);

            if (hata != null)
            {
                XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XtraMessageBox.Show("İşlem başarıyla onaylandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            object onayLogIDObj = gridViewOnaylar.GetRowCellValue(gridViewOnaylar.FocusedRowHandle, "OnayLogID");
            int onayLogID = CommonFunctions.DbNullToInt(onayLogIDObj);

            if (onayLogID == 0)
            {
                XtraMessageBox.Show("Geçersiz kayıt.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hata = _sOnay.IslemReddet(onayLogID, _kullanici.KullaniciID, redNedeni);

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
