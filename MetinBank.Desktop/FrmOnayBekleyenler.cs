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
            // GridView Ayarları
            gridViewOnaylar.OptionsBehavior.Editable = false;
            gridViewOnaylar.OptionsView.ShowGroupPanel = false;
            gridViewOnaylar.OptionsView.EnableAppearanceEvenRow = true;
            gridViewOnaylar.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

            // Kolon Başlıkları ve Format
            if (gridViewOnaylar.Columns["IslemTarihi"] != null)
            {
                gridViewOnaylar.Columns["IslemTarihi"].Caption = "İşlem Tarihi";
                gridViewOnaylar.Columns["IslemTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridViewOnaylar.Columns["IslemTarihi"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            }
            if (gridViewOnaylar.Columns["Tutar"] != null)
            {
                gridViewOnaylar.Columns["Tutar"].Caption = "Tutar";
                gridViewOnaylar.Columns["Tutar"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewOnaylar.Columns["Tutar"].DisplayFormat.FormatString = "N2";
            }
            if (gridViewOnaylar.Columns["Aciklama"] != null) gridViewOnaylar.Columns["Aciklama"].Caption = "Açıklama";
            if (gridViewOnaylar.Columns["IslemTanimi"] != null) gridViewOnaylar.Columns["IslemTanimi"].Caption = "İşlem Türü";
            if (gridViewOnaylar.Columns["MusteriAdSoyad"] != null) gridViewOnaylar.Columns["MusteriAdSoyad"].Caption = "Müşteri";
            if (gridViewOnaylar.Columns["HesapNo"] != null) gridViewOnaylar.Columns["HesapNo"].Caption = "Hesap No";
            if (gridViewOnaylar.Columns["OnayDurumu"] != null) gridViewOnaylar.Columns["OnayDurumu"].Caption = "Durum";
            
            // Gizlenmesi gereken kolonlar
            string[] hiddenCols = { "IslemID", "KaynakHesapID", "HedefHesapID", "KullaniciID", "SubeID", "IPAdresi", "IslemCikisTarihi", "BasariliMi", "ParaBirimi", "IslemUcreti", "IslemTipi", "HedefIBAN", "IslemReferansNo", "AliciAdi" };
            foreach (string col in hiddenCols)
            {
                if (gridViewOnaylar.Columns[col] != null)
                    gridViewOnaylar.Columns[col].Visible = false;
            }

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
