using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmDovizAlSat : XtraForm
    {
        private KullaniciModel _kullanici;
        private SDoviz _sDoviz;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private int _seciliMusteriID;
        private System.Windows.Forms.Timer _aramaTimer;
        private bool _isGenelMerkez;

        // Kurlar
        private decimal _usdAlis, _usdSatis;
        private decimal _eurAlis, _eurSatis;
        private decimal _gbpAlis, _gbpSatis;

        public FrmDovizAlSat(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sDoviz = new SDoviz();
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();

            _isGenelMerkez = _kullanici.RolAdi != null &&
                            (_kullanici.RolAdi.Contains("Genel") || _kullanici.RolAdi.Contains("Merkez"));

            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmDovizAlSat_Load(object sender, EventArgs e)
        {
            this.Text = "Döviz Al / Sat";
            KurlariYukle();

            // Döviz cinsi seçenekleri
            cmbDovizCinsi.Properties.Items.Clear();
            cmbDovizCinsi.Properties.Items.Add("USD");
            cmbDovizCinsi.Properties.Items.Add("EUR");
            cmbDovizCinsi.Properties.Items.Add("GBP");
            cmbDovizCinsi.SelectedIndex = 0;

            // İşlem tipi
            rgIslemTipi.SelectedIndex = 0; // Alım

            gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            gridViewHesaplar.OptionsView.ShowGroupPanel = false;
        }

        private void KurlariYukle()
        {
            try
            {
                _sDoviz.GetDovizKuru("USD", out _usdAlis, out _usdSatis);
                _sDoviz.GetDovizKuru("EUR", out _eurAlis, out _eurSatis);
                _sDoviz.GetDovizKuru("GBP", out _gbpAlis, out _gbpSatis);

                // Kur bilgilerini göster
                lblUSD.Text = $"USD: Alış {_usdAlis:N4} / Satış {_usdSatis:N4}";
                lblEUR.Text = $"EUR: Alış {_eurAlis:N4} / Satış {_eurSatis:N4}";
                lblGBP.Text = $"GBP: Alış {_gbpAlis:N4} / Satış {_gbpSatis:N4}";
            }
            catch
            {
                lblUSD.Text = "Kurlar yüklenemedi";
            }
        }

        private void TxtMusteriArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMusteriArama.Text))
            {
                gridMusteriler.DataSource = null;
                return;
            }

            _aramaTimer.Stop();
            _aramaTimer.Start();
        }

        private void MusteriAra()
        {
            try
            {
                string arama = txtMusteriArama.Text.Trim();
                if (string.IsNullOrWhiteSpace(arama) || arama.Length < 2)
                {
                    gridMusteriler.DataSource = null;
                    return;
                }

                DataTable sonuclar;
                string hata = _sMusteri.MusteriAra(arama, _kullanici.SubeID, _isGenelMerkez, out sonuclar);

                if (hata == null)
                {
                    gridMusteriler.DataSource = sonuclar;
                    gridViewMusteriler.BestFitColumns();
                    GizliSutunlar(gridViewMusteriler, "MusteriID", "KayitSubeID");
                }
            }
            catch { }
        }

        private void GizliSutunlar(DevExpress.XtraGrid.Views.Grid.GridView gv, params string[] cols)
        {
            foreach (var c in cols)
                if (gv.Columns[c] != null) gv.Columns[c].Visible = false;
        }

        private void GridViewMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;

            object musteriIDObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriID");
            _seciliMusteriID = CommonFunctions.DbNullToInt(musteriIDObj);
            if (_seciliMusteriID > 0)
            {
                HesaplariYukle();
            }
        }

        private void HesaplariYukle()
        {
            try
            {
                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(_seciliMusteriID, out hesaplar);

                if (hata == null)
                {
                    gridHesaplar.DataSource = hesaplar;
                    gridViewHesaplar.BestFitColumns();
                    GizliSutunlar(gridViewHesaplar, "HesapID", "MusteriID");

                    // TRY ve Döviz hesaplarını combobox'lara doldur
                    cmbTRYHesap.Properties.Items.Clear();
                    cmbDovizHesap.Properties.Items.Clear();

                    foreach (DataRow row in hesaplar.Rows)
                    {
                        string hesapTipi = row["HesapTipi"].ToString();
                        string iban = row["IBAN"].ToString();
                        decimal bakiye = Convert.ToDecimal(row["Bakiye"] ?? 0);
                        int hesapID = Convert.ToInt32(row["HesapID"]);

                        string display = $"{iban} - {bakiye:N2} {hesapTipi}";

                        if (hesapTipi == "TL" || hesapTipi == "TRY")
                        {
                            cmbTRYHesap.Properties.Items.Add(new HesapItem(hesapID, display));
                        }
                        else
                        {
                            cmbDovizHesap.Properties.Items.Add(new HesapItem(hesapID, display));
                        }
                    }

                    if (cmbTRYHesap.Properties.Items.Count > 0) cmbTRYHesap.SelectedIndex = 0;
                    if (cmbDovizHesap.Properties.Items.Count > 0) cmbDovizHesap.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void NumTutar_EditValueChanged(object sender, EventArgs e)
        {
            HesaplaOzet();
        }

        private void CmbDovizCinsi_SelectedIndexChanged(object sender, EventArgs e)
        {
            HesaplaOzet();
        }

        private void RgIslemTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            HesaplaOzet();
        }

        private void HesaplaOzet()
        {
            try
            {
                decimal tutar = numTutar.Value;
                string doviz = cmbDovizCinsi.Text;
                bool isAlim = rgIslemTipi.SelectedIndex == 0;

                decimal alis = 0, satis = 0;
                switch (doviz)
                {
                    case "USD": alis = _usdAlis; satis = _usdSatis; break;
                    case "EUR": alis = _eurAlis; satis = _eurSatis; break;
                    case "GBP": alis = _gbpAlis; satis = _gbpSatis; break;
                }

                decimal kur = isAlim ? satis : alis;
                decimal tryTutar = tutar * kur;

                if (isAlim)
                {
                    lblOzet.Text = $"💱 {tutar:N2} {doviz} alımı için {tryTutar:N2} TL ödenecek (Kur: {kur:N4})";
                }
                else
                {
                    lblOzet.Text = $"💱 {tutar:N2} {doviz} satımı için {tryTutar:N2} TL alınacak (Kur: {kur:N4})";
                }
            }
            catch
            {
                lblOzet.Text = "";
            }
        }

        private void BtnIslemYap_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seciliMusteriID == 0)
                {
                    XtraMessageBox.Show("Lütfen bir müşteri seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var tryItem = cmbTRYHesap.SelectedItem as HesapItem;
                var dovizItem = cmbDovizHesap.SelectedItem as HesapItem;

                if (tryItem == null || dovizItem == null)
                {
                    XtraMessageBox.Show("Lütfen TRY ve döviz hesaplarını seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numTutar.Value <= 0)
                {
                    XtraMessageBox.Show("Tutar 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool isAlim = rgIslemTipi.SelectedIndex == 0;
                string dovizCinsi = cmbDovizCinsi.Text;
                decimal tutar = numTutar.Value;
                int subeID = _kullanici.SubeID ?? 1;

                long islemID;
                string hata;

                if (isAlim)
                {
                    hata = _sDoviz.DovizAl(tryItem.HesapID, dovizItem.HesapID, tutar, dovizCinsi,
                                           _kullanici.KullaniciID, subeID, out islemID);
                }
                else
                {
                    hata = _sDoviz.DovizSat(dovizItem.HesapID, tryItem.HesapID, tutar, dovizCinsi,
                                            _kullanici.KullaniciID, subeID, out islemID);
                }

                if (hata != null)
                {
                    XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string islemTipi = isAlim ? "Döviz alım" : "Döviz satım";
                XtraMessageBox.Show($"{islemTipi} işlemi başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {tutar:N2} {dovizCinsi}",
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HesaplariYukle();
                numTutar.Value = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
