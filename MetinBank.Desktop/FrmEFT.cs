using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmEFT : XtraForm
    {
        private KullaniciModel _kullanici;
        private SIslem _sIslem;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        
        // Gönderen
        private int _gonderenMusteriID;
        private int _gonderenHesapID;
        private string _gonderenMusteriAd;
        private string _gonderenIBAN;
        private decimal _gonderenBakiye;
        
        private System.Windows.Forms.Timer _aramaTimer;

        public FrmEFT(KullaniciModel kullanici)
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

        private void FrmEFT_Load(object sender, EventArgs e)
        {
            this.Text = "EFT İşlemi (Elektronik Fon Transferi)";
            
            // Grid ayarları
            gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            gridViewHesaplar.OptionsView.ShowGroupPanel = false;
            
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

        // ========== MÜŞTERİ ARAMA ==========
        private void TxtMusteriArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMusteriArama.Text))
            {
                gridMusteriler.DataSource = null;
                gridHesaplar.DataSource = null;
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
                
                _gonderenMusteriID = CommonFunctions.DbNullToInt(musteriIDObj);
                _gonderenMusteriAd = CommonFunctions.DbNullToString(adObj) + " " + CommonFunctions.DbNullToString(soyadObj);
                
                if (_gonderenMusteriID == 0) return;
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
                if (_gonderenMusteriID == 0) return;

                DataTable hesaplar;
                string hata = _sHesap.MusterininHesaplari(_gonderenMusteriID, out hesaplar);
                
                if (hata != null)
                {
                    gridHesaplar.DataSource = null;
                    return;
                }

                gridHesaplar.DataSource = hesaplar;
                gridViewHesaplar.BestFitColumns();
                
                // ID sütunlarını gizle
                GizliSutunlariAyarla(gridViewHesaplar, "HesapID", "MusteriID");
            }
            catch
            {
                gridHesaplar.DataSource = null;
            }
        }

        private void GridViewHesaplar_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                object hesapIDObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "HesapID");
                object ibanObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "IBAN");
                object bakiyeObj = gridViewHesaplar.GetRowCellValue(e.RowHandle, "Bakiye");

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

        // ========== HEDEF IBAN GİRİŞİ ==========
        private void TxtHedefIBAN_Leave(object sender, EventArgs e)
        {
            try
            {
                string iban = txtHedefIBAN.Text.Trim().Replace(" ", "");
                if (string.IsNullOrEmpty(iban) || iban.Length < 26) 
                {
                    lblAliciInfo.Text = "📥 Alıcı: Harici banka IBAN giriniz";
                    return;
                }

                // IBAN'ı doğrula
                string hataMesaji = IbanHelper.ValidateIban(iban);
                if (hataMesaji != null)
                {
                    lblAliciInfo.Text = $"📥 Alıcı: ⚠️ {hataMesaji}";
                    return;
                }

                // MetinBank IBAN kontrolü - EFT banka dışı olmalı
                if (iban.StartsWith("TR") && iban.Length >= 8 && iban.Substring(4, 4) == "0127")
                {
                    lblAliciInfo.Text = "📥 Alıcı: ⚠️ MetinBank IBAN'ı! Lütfen Havale kullanın.";
                    return;
                }

                // Harici banka - EFT için uygun
                lblAliciInfo.Text = $"📥 Alıcı: ✅ Harici banka IBAN'ı geçerli | {IbanHelper.FormatIban(iban)}";
            }
            catch (Exception ex)
            {
                lblAliciInfo.Text = $"📥 Alıcı: Hata - {ex.Message}";
            }
        }

        // ========== EFT GÖNDER ==========
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

                string hedefIBAN = txtHedefIBAN.Text.Trim().Replace(" ", "");
                if (string.IsNullOrWhiteSpace(hedefIBAN))
                {
                    MessageBox.Show("Hedef IBAN giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // IBAN validasyonu
                string ibanHata = IbanHelper.ValidateIban(hedefIBAN);
                if (ibanHata != null)
                {
                    MessageBox.Show(ibanHata, "Geçersiz IBAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // MetinBank IBAN kontrolü
                if (hedefIBAN.StartsWith("TR") && hedefIBAN.Length >= 8 && hedefIBAN.Substring(4, 4) == "0127")
                {
                    MessageBox.Show("MetinBank IBAN'ına EFT yapılamaz. Lütfen Havale kullanın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                string aliciAdi = txtAliciAdi.Text.Trim();
                if (string.IsNullOrWhiteSpace(aliciAdi))
                {
                    MessageBox.Show("Alıcı adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAliciAdi.Focus();
                    return;
                }

                // Onay mesajı
                DialogResult dr = MessageBox.Show(
                    $"EFT işlemini onaylıyor musunuz?\n\n" +
                    $"Gönderen: {_gonderenMusteriAd}\n" +
                    $"Kaynak IBAN: {IbanHelper.FormatIban(_gonderenIBAN)}\n\n" +
                    $"Alıcı: {aliciAdi}\n" +
                    $"Hedef IBAN: {IbanHelper.FormatIban(hedefIBAN)}\n\n" +
                    $"Tutar: {numTutar.Value:N2} TL",
                    "EFT Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr != DialogResult.Yes) return;

                // SubeID null ise varsayılan değer kullan
                int subeID = _kullanici.SubeID ?? 1;

                long islemID;
                string hata = _sIslem.EFT(
                    _gonderenHesapID,
                    hedefIBAN,
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
                MessageBox.Show($"✅ EFT işlemi başarılı!\n\nİşlem No: TRX{islemID}\nTutar: {numTutar.Value:N2} TL{onayMesaji}", 
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
            txtMusteriArama.Text = "";
            gridMusteriler.DataSource = null;
            gridHesaplar.DataSource = null;
            lblGonderenInfo.Text = "📤 Gönderen: Seçilmedi";
            
            // Alıcı temizle
            txtHedefIBAN.Text = "";
            txtAliciAdi.Text = "";
            lblAliciInfo.Text = "📥 Alıcı: Harici banka hesabına EFT";
            
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
