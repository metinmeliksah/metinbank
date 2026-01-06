using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Data;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmHavale : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private SIslemUcreti _sIslemUcreti;
        
        // Gönderen
        private int _gonderenMusteriID;
        private int _gonderenHesapID;
        private string _gonderenMusteriAd;
        private string _gonderenIBAN;
        private decimal _gonderenBakiye;
        
        // Alıcı
        private int _aliciMusteriID;
        private int _aliciHesapID;
        private string _aliciMusteriAd;
        private string _aliciIBAN;
        
        private System.Windows.Forms.Timer _gonderenAramaTimer;
        private System.Windows.Forms.Timer _aliciAramaTimer;

        public FrmHavale(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sIslem = new SIslem();
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            _sIslemUcreti = new SIslemUcreti();
            
            // Arama timer'ları
            _gonderenAramaTimer = new System.Windows.Forms.Timer();
            _gonderenAramaTimer.Interval = 500;
            _gonderenAramaTimer.Tick += (s, e) => {
                _gonderenAramaTimer.Stop();
                GonderenMusteriAra();
            };
            
            _aliciAramaTimer = new System.Windows.Forms.Timer();
            _aliciAramaTimer.Interval = 500;
            _aliciAramaTimer.Tick += (s, e) => {
                _aliciAramaTimer.Stop();
                AliciMusteriAra();
            };
        }

        private void FrmHavale_Load(object sender, EventArgs e)
        {
            this.Text = "Havale İşlemi (İki Müşteri Arası Transfer)";
            
            // Grid ayarları
            gridViewGonderenMusteriler.OptionsView.ShowGroupPanel = false;
            gridViewGonderenHesaplar.OptionsView.ShowGroupPanel = false;
            gridViewAliciMusteriler.OptionsView.ShowGroupPanel = false;
            gridViewAliciHesaplar.OptionsView.ShowGroupPanel = false;
            
            // Layout düzenlemesi
            AyarlaPanelBoyutlari();
        }
        
        private void AyarlaPanelBoyutlari()
        {
            layoutControl1.BeginUpdate();
            try
            {
                // Kısıtlamaları kaldır (esnek boyutlandırma için)
                grpGonderen.MaxSize = new System.Drawing.Size(0, 0);
                grpGonderen.MinSize = new System.Drawing.Size(100, 100);
                grpAlici.MaxSize = new System.Drawing.Size(0, 0);
                grpAlici.MinSize = new System.Drawing.Size(100, 100);
                
                // Root grubundaki boşlukları temizle
                List<DevExpress.XtraLayout.BaseLayoutItem> itemsToRemove = new List<DevExpress.XtraLayout.BaseLayoutItem>();
                foreach (DevExpress.XtraLayout.BaseLayoutItem item in layoutControlGroup1.Items)
                {
                    if (item is DevExpress.XtraLayout.EmptySpaceItem)
                    {
                        itemsToRemove.Add(item);
                    }
                }
                foreach (var item in itemsToRemove) layoutControlGroup1.Remove(item);
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

        // ========== GÖNDEREN MÜŞTERİ ==========
        private void TxtGonderenArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGonderenArama.Text))
            {
                gridGonderenMusteriler.DataSource = null;
                gridGonderenHesaplar.DataSource = null;
                return;
            }
            _gonderenAramaTimer.Stop();
            _gonderenAramaTimer.Start();
        }

        private void GonderenMusteriAra()
        {
            try
            {
                string arama = txtGonderenArama.Text.Trim();
                if (string.IsNullOrWhiteSpace(arama) || arama.Length < 2)
                {
                    gridGonderenMusteriler.DataSource = null;
                    return;
                }

                DataTable sonuclar;
                string hata = _sMusteri.MusteriAra(arama, out sonuclar);
                
                if (hata != null)
                {
                    gridGonderenMusteriler.DataSource = null;
                    return;
                }

                gridGonderenMusteriler.DataSource = sonuclar;
                gridViewGonderenMusteriler.BestFitColumns();
                GizliSutunlariAyarla(gridViewGonderenMusteriler, "MusteriID");
            }
            catch
            {
                gridGonderenMusteriler.DataSource = null;
            }
        }

        private void GridViewGonderenMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object musteriIDObj = gridViewGonderenMusteriler.GetRowCellValue(e.RowHandle, "MusteriID");
                object adObj = gridViewGonderenMusteriler.GetRowCellValue(e.RowHandle, "Ad");
                object soyadObj = gridViewGonderenMusteriler.GetRowCellValue(e.RowHandle, "Soyad");
                
                _gonderenMusteriID = CommonFunctions.DbNullToInt(musteriIDObj);
                _gonderenMusteriAd = CommonFunctions.DbNullToString(adObj) + " " + CommonFunctions.DbNullToString(soyadObj);
                
                if (_gonderenMusteriID == 0) return;
                GonderenHesaplariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GonderenHesaplariYukle()
        {
            try
            {
                if (_gonderenMusteriID == 0) return;

                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(_gonderenMusteriID, out hesaplar);
                
                if (hata != null)
                {
                    gridGonderenHesaplar.DataSource = null;
                    return;
                }

                gridGonderenHesaplar.DataSource = hesaplar;
                gridViewGonderenHesaplar.BestFitColumns();
                GizliSutunlariAyarla(gridViewGonderenHesaplar, "HesapID", "MusteriID");
            }
            catch
            {
                gridGonderenHesaplar.DataSource = null;
            }
        }

        private void GridViewGonderenHesaplar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object hesapIDObj = gridViewGonderenHesaplar.GetRowCellValue(e.RowHandle, "HesapID");
                object ibanObj = gridViewGonderenHesaplar.GetRowCellValue(e.RowHandle, "IBAN");
                object bakiyeObj = gridViewGonderenHesaplar.GetRowCellValue(e.RowHandle, "Bakiye");

                _gonderenHesapID = CommonFunctions.DbNullToInt(hesapIDObj);
                _gonderenIBAN = CommonFunctions.DbNullToString(ibanObj);
                _gonderenBakiye = CommonFunctions.DbNullToDecimal(bakiyeObj);
                
                // Info label güncelle
                lblGonderenInfo.Text = $"📤 Gönderen: {_gonderenMusteriAd} | IBAN: {IbanHelper.FormatIban(_gonderenIBAN)} | Bakiye: {_gonderenBakiye:N2} TL";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== ALICI MÜŞTERİ ==========
        private void TxtAliciIBAN_Leave(object sender, EventArgs e)
        {
            try
            {
                string iban = txtAliciIBAN.Text.Trim().Replace(" ", "");
                if (string.IsNullOrEmpty(iban) || iban.Length < 26) return;

                // IBAN'ı doğrula
                string hataMesaji = IbanHelper.ValidateIban(iban);
                if (hataMesaji != null)
                {
                    lblAliciInfo.Text = $"📥 Alıcı: ⚠️ {hataMesaji}";
                    return;
                }

                // MetinBank IBAN ise hesabı bul
                if (iban.StartsWith("TR") && iban.Substring(4, 4) == "0127") // MetinBank kodu
                {
                    HesapModel hesap;
                    string hata = _sHesap.HesapGetirIBAN(iban, out hesap);
                    
                    if (hata == null && hesap != null)
                    {
                        _aliciHesapID = hesap.HesapID;
                        _aliciIBAN = hesap.IBAN;
                        _aliciMusteriAd = hesap.MusteriAdi;
                        _aliciMusteriID = hesap.MusteriID;
                        
                        lblAliciInfo.Text = $"📥 Alıcı: {_aliciMusteriAd} | IBAN: {IbanHelper.FormatIban(_aliciIBAN)} ✅";
                        
                        // Grid'leri temizle - IBAN ile zaten hesap bulundu
                        gridAliciMusteriler.DataSource = null;
                        gridAliciHesaplar.DataSource = null;
                    }
                    else
                    {
                        // Dış banka IBAN'ı
                        _aliciIBAN = iban;
                        _aliciMusteriAd = "";
                        _aliciHesapID = 0;
                        lblAliciInfo.Text = $"📥 Alıcı: Harici Hesap (IBAN: {IbanHelper.FormatIban(iban)}) ⚠️ Havale banka dışına";
                    }
                }
                else
                {
                    // Dış banka IBAN'ı - Bu durumda EFT kullanılmalı
                    _aliciIBAN = iban;
                    _aliciMusteriAd = "";
                    _aliciHesapID = 0;
                    lblAliciInfo.Text = $"📥 Alıcı: Harici Banka - Havale için MetinBank IBAN gerekli, diğer bankalar için EFT kullanın";
                }
            }
            catch (Exception ex)
            {
                lblAliciInfo.Text = $"📥 Alıcı: Hata - {ex.Message}";
            }
        }

        private void TxtAliciArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAliciArama.Text))
            {
                gridAliciMusteriler.DataSource = null;
                gridAliciHesaplar.DataSource = null;
                return;
            }
            _aliciAramaTimer.Stop();
            _aliciAramaTimer.Start();
        }

        private void AliciMusteriAra()
        {
            try
            {
                string arama = txtAliciArama.Text.Trim();
                if (string.IsNullOrWhiteSpace(arama) || arama.Length < 2)
                {
                    gridAliciMusteriler.DataSource = null;
                    return;
                }

                DataTable sonuclar;
                string hata = _sMusteri.MusteriAra(arama, out sonuclar);
                
                if (hata != null)
                {
                    gridAliciMusteriler.DataSource = null;
                    return;
                }

                gridAliciMusteriler.DataSource = sonuclar;
                gridViewAliciMusteriler.BestFitColumns();
                GizliSutunlariAyarla(gridViewAliciMusteriler, "MusteriID");
            }
            catch
            {
                gridAliciMusteriler.DataSource = null;
            }
        }

        private void GridViewAliciMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object musteriIDObj = gridViewAliciMusteriler.GetRowCellValue(e.RowHandle, "MusteriID");
                object adObj = gridViewAliciMusteriler.GetRowCellValue(e.RowHandle, "Ad");
                object soyadObj = gridViewAliciMusteriler.GetRowCellValue(e.RowHandle, "Soyad");
                
                _aliciMusteriID = CommonFunctions.DbNullToInt(musteriIDObj);
                _aliciMusteriAd = CommonFunctions.DbNullToString(adObj) + " " + CommonFunctions.DbNullToString(soyadObj);
                
                if (_aliciMusteriID == 0) return;
                AliciHesaplariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AliciHesaplariYukle()
        {
            try
            {
                if (_aliciMusteriID == 0) return;

                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(_aliciMusteriID, out hesaplar);
                
                if (hata != null)
                {
                    gridAliciHesaplar.DataSource = null;
                    return;
                }

                gridAliciHesaplar.DataSource = hesaplar;
                gridViewAliciHesaplar.BestFitColumns();
                GizliSutunlariAyarla(gridViewAliciHesaplar, "HesapID", "MusteriID");
            }
            catch
            {
                gridAliciHesaplar.DataSource = null;
            }
        }

        private void GridViewAliciHesaplar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object hesapIDObj = gridViewAliciHesaplar.GetRowCellValue(e.RowHandle, "HesapID");
                object ibanObj = gridViewAliciHesaplar.GetRowCellValue(e.RowHandle, "IBAN");

                _aliciHesapID = CommonFunctions.DbNullToInt(hesapIDObj);
                _aliciIBAN = CommonFunctions.DbNullToString(ibanObj);
                
                // IBAN alanını güncelle
                txtAliciIBAN.Text = IbanHelper.FormatIban(_aliciIBAN);
                
                // Info label güncelle
                lblAliciInfo.Text = $"📥 Alıcı: {_aliciMusteriAd} | IBAN: {IbanHelper.FormatIban(_aliciIBAN)} ✅";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== HAVALE GÖNDER ==========
        private void BtnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyonlar
                if (_gonderenHesapID == 0)
                {
                    MessageBox.Show("Lütfen önce gönderen müşteri ve hesabı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(_aliciIBAN) && _aliciHesapID == 0)
                {
                    MessageBox.Show("Lütfen alıcı IBAN giriniz veya alıcı müşteriyi seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numTutar.Value <= 0)
                {
                    MessageBox.Show("Tutar 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_gonderenBakiye < numTutar.Value)
                {
                    MessageBox.Show($"Yetersiz bakiye! Mevcut: {_gonderenBakiye:N2} TL, İstenen: {numTutar.Value:N2} TL", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Aynı hesaba transfer kontrolü
                if (_gonderenHesapID == _aliciHesapID)
                {
                    MessageBox.Show("Gönderen ve alıcı hesap aynı olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hedef IBAN belirle
                string hedefIBAN = !string.IsNullOrWhiteSpace(_aliciIBAN) ? _aliciIBAN : "";
                if (string.IsNullOrWhiteSpace(hedefIBAN))
                {
                    MessageBox.Show("Hedef IBAN belirlenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // SubeID null ise varsayılan değer kullan
                int subeID = _kullanici.SubeID ?? 1;

                // Onay mesajı
                string aliciAdi = !string.IsNullOrWhiteSpace(_aliciMusteriAd) ? _aliciMusteriAd : "IBAN ile belirlenen hesap";
                DialogResult dr = MessageBox.Show(
                    $"Havale işlemini onaylıyor musunuz?\n\n" +
                    $"Gönderen: {_gonderenMusteriAd}\n" +
                    $"Hesap: {IbanHelper.FormatIban(_gonderenIBAN)}\n\n" +
                    $"Alıcı: {aliciAdi}\n" +
                    $"IBAN: {IbanHelper.FormatIban(hedefIBAN)}\n\n" +
                    $"Tutar: {numTutar.Value:N2} TL",
                    "Havale Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr != DialogResult.Yes) return;

                // Havale işlemi
                long islemID;
                string hata = _sIslem.Havale(
                    _gonderenHesapID,
                    IbanHelper.RemoveIbanSpaces(hedefIBAN),
                    numTutar.Value,
                    txtAciklama.Text,
                    aliciAdi,
                    _kullanici.KullaniciID,
                    subeID,
                    0m, 
                    out islemID
                );

                if (hata != null)
                {
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string onayMesaji = numTutar.Value > 50000 ? "\n\nNOT: İşlem onay bekliyor." : "";
                MessageBox.Show($"✅ Havale işlemi başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {numTutar.Value:N2} TL{onayMesaji}", 
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
            // Gönderen temizle
            _gonderenMusteriID = 0;
            _gonderenHesapID = 0;
            _gonderenMusteriAd = "";
            _gonderenIBAN = "";
            _gonderenBakiye = 0;
            txtGonderenArama.Text = "";
            gridGonderenMusteriler.DataSource = null;
            gridGonderenHesaplar.DataSource = null;
            lblGonderenInfo.Text = "📤 Gönderen: Seçilmedi";
            
            // Alıcı temizle
            _aliciMusteriID = 0;
            _aliciHesapID = 0;
            _aliciMusteriAd = "";
            _aliciIBAN = "";
            txtAliciArama.Text = "";
            txtAliciIBAN.Text = "";
            gridAliciMusteriler.DataSource = null;
            gridAliciHesaplar.DataSource = null;
            lblAliciInfo.Text = "📥 Alıcı: Seçilmedi";
            
            // Transfer temizle
            numTutar.Value = 0;
            txtAciklama.Text = "";
        }
    }
}
