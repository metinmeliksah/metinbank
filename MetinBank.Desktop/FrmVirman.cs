using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmVirman : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        
        private int _seciliMusteriID;
        private string _seciliMusteriAd;
        
        // Kaynak Hesap
        private int _kaynakHesapID;
        private string _kaynakIBAN;
        private decimal _kaynakBakiye;
        
        // Hedef Hesap
        private int _hedefHesapID;
        private string _hedefIBAN;
        private decimal _hedefBakiye;
        
        private System.Windows.Forms.Timer _aramaTimer;

        public FrmVirman(KullaniciModel kullanici)
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

        private void FrmVirman_Load(object sender, EventArgs e)
        {
            this.Text = "Virman (Hesaplar Arası Transfer)";
            
            // Grid ayarları
            gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            gridViewKaynakHesaplar.OptionsView.ShowGroupPanel = false;
            gridViewHedefHesaplar.OptionsView.ShowGroupPanel = false;
            
            // Layout düzenlemesi
            AyarlaPanelBoyutlari();
        }
        
        private void AyarlaPanelBoyutlari()
        {
            layoutControl1.BeginUpdate();
            try
            {
                DevExpress.XtraLayout.LayoutControlGroup splitGroup = layoutControlGroup1.Items.FindByName("splitGroup") as DevExpress.XtraLayout.LayoutControlGroup;
                if (splitGroup == null)
                {
                    splitGroup = layoutControlGroup1.AddGroup();
                    splitGroup.Name = "splitGroup";
                    splitGroup.GroupBordersVisible = false;
                    splitGroup.TextVisible = false;
                    
                    splitGroup.AddItem(grpKaynak);
                    splitGroup.AddItem(grpHedef);
                    
                    splitGroup.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
                    splitGroup.OptionsTableLayoutGroup.ColumnDefinitions.Clear();
                    splitGroup.OptionsTableLayoutGroup.ColumnDefinitions.Add(new DevExpress.XtraLayout.ColumnDefinition { SizeType = System.Windows.Forms.SizeType.Percent, Width = 50 });
                    splitGroup.OptionsTableLayoutGroup.ColumnDefinitions.Add(new DevExpress.XtraLayout.ColumnDefinition { SizeType = System.Windows.Forms.SizeType.Percent, Width = 50 });
                    
                    splitGroup.OptionsTableLayoutGroup.RowDefinitions.Clear();
                    splitGroup.OptionsTableLayoutGroup.RowDefinitions.Add(new DevExpress.XtraLayout.RowDefinition { SizeType = System.Windows.Forms.SizeType.Percent, Height = 100 });
                    
                    grpKaynak.OptionsTableLayoutItem.ColumnIndex = 0;
                    grpKaynak.OptionsTableLayoutItem.RowIndex = 0;
                    
                    grpHedef.OptionsTableLayoutItem.ColumnIndex = 1;
                    grpHedef.OptionsTableLayoutItem.RowIndex = 0;
                    
                    grpKaynak.MaxSize = new System.Drawing.Size(0, 0);
                    grpKaynak.MinSize = new System.Drawing.Size(100, 100);
                    grpHedef.MaxSize = new System.Drawing.Size(0, 0);
                    grpHedef.MinSize = new System.Drawing.Size(100, 100);

                    // Müşteri Arama grubunun altına taşı
                    splitGroup.Move(grpMusteriArama, DevExpress.XtraLayout.Utils.InsertType.Bottom);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Layout hatası: " + ex.Message);
            }
            finally
            {
                layoutControl1.EndUpdate();
            }
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
                gridKaynakHesaplar.DataSource = null;
                gridHedefHesaplar.DataSource = null;
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
                
                // Her iki panele de aynı müşterinin hesaplarını yükle
                HesaplariYukle();
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
                    gridKaynakHesaplar.DataSource = null;
                    gridHedefHesaplar.DataSource = null;
                    return;
                }

                // Her iki panele de aynı hesapları yükle
                gridKaynakHesaplar.DataSource = hesaplar;
                gridViewKaynakHesaplar.BestFitColumns();
                GizliSutunlariAyarla(gridViewKaynakHesaplar, "HesapID", "MusteriID");
                
                // DataTable'ın bir kopyasını oluştur (aynı referansı kullanmamak için)
                DataTable hesaplarKopyasi = hesaplar.Copy();
                gridHedefHesaplar.DataSource = hesaplarKopyasi;
                gridViewHedefHesaplar.BestFitColumns();
                GizliSutunlariAyarla(gridViewHedefHesaplar, "HesapID", "MusteriID");
                
                // Seçimleri temizle
                _kaynakHesapID = 0;
                _hedefHesapID = 0;
                lblKaynakInfo.Text = "📤 Kaynak Hesap: Seçilmedi";
                lblHedefInfo.Text = "📥 Hedef Hesap: Seçilmedi";
            }
            catch
            {
                gridKaynakHesaplar.DataSource = null;
                gridHedefHesaplar.DataSource = null;
            }
        }

        // ========== KAYNAK HESAP SEÇİMİ ==========
        private void GridViewKaynakHesaplar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object hesapIDObj = gridViewKaynakHesaplar.GetRowCellValue(e.RowHandle, "HesapID");
                object ibanObj = gridViewKaynakHesaplar.GetRowCellValue(e.RowHandle, "IBAN");
                object bakiyeObj = gridViewKaynakHesaplar.GetRowCellValue(e.RowHandle, "Bakiye");

                _kaynakHesapID = CommonFunctions.DbNullToInt(hesapIDObj);
                _kaynakIBAN = CommonFunctions.DbNullToString(ibanObj);
                _kaynakBakiye = CommonFunctions.DbNullToDecimal(bakiyeObj);
                
                lblKaynakInfo.Text = $"📤 Kaynak: {_seciliMusteriAd} | IBAN: {IbanHelper.FormatIban(_kaynakIBAN)} | Bakiye: {_kaynakBakiye:N2} TL";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== HEDEF HESAP SEÇİMİ ==========
        private void GridViewHedefHesaplar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object hesapIDObj = gridViewHedefHesaplar.GetRowCellValue(e.RowHandle, "HesapID");
                object ibanObj = gridViewHedefHesaplar.GetRowCellValue(e.RowHandle, "IBAN");
                object bakiyeObj = gridViewHedefHesaplar.GetRowCellValue(e.RowHandle, "Bakiye");

                _hedefHesapID = CommonFunctions.DbNullToInt(hesapIDObj);
                _hedefIBAN = CommonFunctions.DbNullToString(ibanObj);
                _hedefBakiye = CommonFunctions.DbNullToDecimal(bakiyeObj);
                
                lblHedefInfo.Text = $"📥 Hedef: {_seciliMusteriAd} | IBAN: {IbanHelper.FormatIban(_hedefIBAN)} | Bakiye: {_hedefBakiye:N2} TL";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== VİRMAN GÖNDER ==========
        private void BtnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_kaynakHesapID == 0)
                {
                    MessageBox.Show("Lütfen kaynak hesap seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_hedefHesapID == 0)
                {
                    MessageBox.Show("Lütfen hedef hesap seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_kaynakHesapID == _hedefHesapID)
                {
                    MessageBox.Show("Kaynak ve hedef hesap aynı olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numTutar.Value <= 0)
                {
                    MessageBox.Show("Tutar 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_kaynakBakiye < numTutar.Value)
                {
                    MessageBox.Show($"Yetersiz bakiye! Mevcut: {_kaynakBakiye:N2} TL, İstenen: {numTutar.Value:N2} TL", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Onay mesajı
                DialogResult dr = MessageBox.Show(
                    $"Virman işlemini onaylıyor musunuz?\n\n" +
                    $"Kaynak Hesap: {IbanHelper.FormatIban(_kaynakIBAN)}\n" +
                    $"Hedef Hesap: {IbanHelper.FormatIban(_hedefIBAN)}\n\n" +
                    $"Tutar: {numTutar.Value:N2} TL",
                    "Virman Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr != DialogResult.Yes) return;

                // SubeID null ise varsayılan değer kullan
                int subeID = _kullanici.SubeID ?? 1;

                long islemID;
                string hata = _sIslem.Virman(
                    _kaynakHesapID,
                    _hedefHesapID,
                    numTutar.Value,
                    txtAciklama.Text,
                    _kullanici.KullaniciID,
                    subeID,
                    out islemID
                );

                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"✅ Virman işlemi başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {numTutar.Value:N2} TL", 
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Formu temizle
                TemizleForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TemizleForm()
        {
            // Müşteri temizle
            _seciliMusteriID = 0;
            _seciliMusteriAd = "";
            txtMusteriArama.Text = "";
            gridMusteriler.DataSource = null;
            
            // Kaynak temizle
            _kaynakHesapID = 0;
            _kaynakIBAN = "";
            _kaynakBakiye = 0;
            gridKaynakHesaplar.DataSource = null;
            lblKaynakInfo.Text = "📤 Kaynak Hesap: Seçilmedi";
            
            // Hedef temizle
            _hedefHesapID = 0;
            _hedefIBAN = "";
            _hedefBakiye = 0;
            gridHedefHesaplar.DataSource = null;
            lblHedefInfo.Text = "📥 Hedef Hesap: Seçilmedi";
            
            // Transfer temizle
            numTutar.Value = 0;
            txtAciklama.Text = "";
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
