using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmIslemGecmisi : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        
        private int _seciliMusteriID;
        private string _seciliMusteriAd;
        private int _seciliHesapID;
        private DataTable _musteriHesaplari;
        
        private System.Windows.Forms.Timer _aramaTimer;

        public FrmIslemGecmisi(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            
            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmIslemGecmisi_Load(object sender, EventArgs e)
        {
            this.Text = "İşlem Geçmişi";
            
            // Grid ayarları
            gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            gridViewIslemler.OptionsView.ShowGroupPanel = false;
            gridViewIslemler.OptionsView.ShowFooter = true;
            
            // Hesap filtre combobox'ı başlat
            cmbHesapFiltre.Properties.Items.Clear();
            cmbHesapFiltre.Properties.Items.Add("-- Tüm Hesaplar --");
            cmbHesapFiltre.SelectedIndex = 0;
        }
        
        /// <summary>
        /// ID sütunlarını gizler
        /// </summary>
        private void GizliSutunlariAyarla(DevExpress.XtraGrid.Views.Grid.GridView gridView, params string[] sutunlar)
        {
            foreach (string sutun in sutunlar)
            {
                if (gridView.Columns[sutun] != null)
                    gridView.Columns[sutun].Visible = false;
            }
        }

        // ========== MÜŞTERİ ARAMA ==========
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
                string hata = _sMusteri.MusteriAra(arama, out sonuclar);
                
                if (hata != null)
                {
                    gridMusteriler.DataSource = null;
                    return;
                }

                gridMusteriler.DataSource = sonuclar;
                gridViewMusteriler.BestFitColumns();
                
                // ID sütunlarını gizle
                GizliSutunlariAyarla(gridViewMusteriler, "MusteriID");
            }
            catch
            {
                gridMusteriler.DataSource = null;
            }
        }

        private void GridViewMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object musteriIDObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriID");
                object adObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "Ad");
                object soyadObj = gridViewMusteriler.GetRowCellValue(e.RowHandle, "Soyad");
                
                _seciliMusteriID = CommonFunctions.DbNullToInt(musteriIDObj);
                _seciliMusteriAd = CommonFunctions.DbNullToString(adObj) + " " + CommonFunctions.DbNullToString(soyadObj);
                
                if (_seciliMusteriID == 0) return;
                
                // Müşteri bilgisini göster
                lblSeciliMusteri.Text = $"📋 Seçili Müşteri: {_seciliMusteriAd}";
                
                // Hesapları yükle
                HesaplariYukle();
                
                // İşlemleri yükle
                IslemleriYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HesaplariYukle()
        {
            try
            {
                if (_seciliMusteriID == 0) return;

                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(_seciliMusteriID, out hesaplar);
                
                if (hata != null)
                {
                    _musteriHesaplari = null;
                    return;
                }

                _musteriHesaplari = hesaplar;
                
                // Combobox'ı doldur
                cmbHesapFiltre.Properties.Items.Clear();
                cmbHesapFiltre.Properties.Items.Add("-- Tüm Hesaplar --");
                
                foreach (DataRow row in hesaplar.Rows)
                {
                    string iban = CommonFunctions.DbNullToString(row["IBAN"]);
                    string hesapTipi = CommonFunctions.DbNullToString(row["HesapTipi"]);
                    decimal bakiye = CommonFunctions.DbNullToDecimal(row["Bakiye"]);
                    int hesapID = CommonFunctions.DbNullToInt(row["HesapID"]);
                    
                    string displayText = $"{IbanHelper.FormatIban(iban)} - {hesapTipi} ({bakiye:N2} TL)";
                    cmbHesapFiltre.Properties.Items.Add(new HesapItem(hesapID, displayText));
                }
                
                cmbHesapFiltre.SelectedIndex = 0;
            }
            catch
            {
                _musteriHesaplari = null;
            }
        }

        private void CmbHesapFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçili hesabı güncelle
            if (cmbHesapFiltre.SelectedIndex <= 0)
            {
                _seciliHesapID = 0;
            }
            else
            {
                HesapItem item = cmbHesapFiltre.SelectedItem as HesapItem;
                if (item != null)
                {
                    _seciliHesapID = item.HesapID;
                }
            }
            
            // İşlemleri yeniden yükle
            if (_seciliMusteriID > 0)
            {
                IslemleriYukle();
            }
        }

        private void IslemleriYukle()
        {
            try
            {
                if (_seciliMusteriID == 0)
                {
                    gridIslemler.DataSource = null;
                    return;
                }

                DataTable islemler;
                string hata;
                
                if (_seciliHesapID > 0)
                {
                    // Belirli hesabın işlemleri
                    hata = _sIslem.HesabinIslemleri(_seciliHesapID, out islemler);
                }
                else
                {
                    // Müşterinin tüm işlemleri
                    hata = _sIslem.MusterininIslemleri(_seciliMusteriID, out islemler);
                }
                
                if (hata != null)
                {
                    gridIslemler.DataSource = null;
                    return;
                }

                gridIslemler.DataSource = islemler;
                gridViewIslemler.BestFitColumns();
                
                // ID sütunlarını gizle
                GizliSutunlariAyarla(gridViewIslemler, "IslemID");
                
                // Sütun başlıklarını düzenle
                if (gridViewIslemler.Columns["IslemReferansNo"] != null)
                    gridViewIslemler.Columns["IslemReferansNo"].Caption = "Referans No";
                if (gridViewIslemler.Columns["IslemTipi"] != null)
                    gridViewIslemler.Columns["IslemTipi"].Caption = "İşlem Tipi";
                if (gridViewIslemler.Columns["Tutar"] != null)
                {
                    gridViewIslemler.Columns["Tutar"].Caption = "Tutar";
                    gridViewIslemler.Columns["Tutar"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridViewIslemler.Columns["Tutar"].DisplayFormat.FormatString = "N2";
                    gridViewIslemler.Columns["Tutar"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    gridViewIslemler.Columns["Tutar"].SummaryItem.DisplayFormat = "Toplam: {0:N2} TL";
                }
                if (gridViewIslemler.Columns["ParaBirimi"] != null)
                    gridViewIslemler.Columns["ParaBirimi"].Caption = "Para Birimi";
                if (gridViewIslemler.Columns["IslemTarihi"] != null)
                {
                    gridViewIslemler.Columns["IslemTarihi"].Caption = "Tarih";
                    gridViewIslemler.Columns["IslemTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gridViewIslemler.Columns["IslemTarihi"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                }
                if (gridViewIslemler.Columns["OnayDurumu"] != null)
                    gridViewIslemler.Columns["OnayDurumu"].Caption = "Durum";
                if (gridViewIslemler.Columns["Aciklama"] != null)
                    gridViewIslemler.Columns["Aciklama"].Caption = "Açıklama";
                if (gridViewIslemler.Columns["AliciAdi"] != null)
                    gridViewIslemler.Columns["AliciAdi"].Caption = "Alıcı";
                if (gridViewIslemler.Columns["KaynakIBAN"] != null)
                    gridViewIslemler.Columns["KaynakIBAN"].Caption = "Kaynak IBAN";
                if (gridViewIslemler.Columns["HedefIBAN"] != null)
                    gridViewIslemler.Columns["HedefIBAN"].Caption = "Hedef IBAN";
                
                // İşlem sayısını göster
                int islemSayisi = islemler?.Rows.Count ?? 0;
                lblSeciliMusteri.Text = $"📋 Seçili Müşteri: {_seciliMusteriAd} | Toplam {islemSayisi} işlem bulundu";
            }
            catch (Exception ex)
            {
                gridIslemler.DataSource = null;
                MessageBox.Show($"İşlemler yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFiltrele_Click(object sender, EventArgs e)
        {
            IslemleriYukle();
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
    /// <summary>
    /// ComboBox için hesap öğesi
    /// </summary>
    internal class HesapItem
    {
        public int HesapID { get; set; }
        public string DisplayText { get; set; }
        
        public HesapItem(int hesapID, string displayText)
        {
            HesapID = hesapID;
            DisplayText = displayText;
        }
        
        public override string ToString()
        {
            return DisplayText;
        }
    }
}
