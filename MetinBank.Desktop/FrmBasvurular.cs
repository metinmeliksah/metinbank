using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmBasvurular : XtraForm
    {
        private KullaniciModel _kullanici;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private SKart _sKart;
        private int _seciliMusteriID;
        private int _seciliHesapID;
        private System.Windows.Forms.Timer _aramaTimer;

        public FrmBasvurular(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            _sKart = new SKart();
            
            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmBasvurular_Load(object sender, EventArgs e)
        {
            this.Text = "Kart Başvuruları";
            
            // Başvuru tipi: Troy ve Mastercard
            cmbBasvuruTipi.Properties.Items.Clear();
            cmbBasvuruTipi.Properties.Items.AddRange(new string[] { "Troy", "Mastercard" });
            cmbBasvuruTipi.SelectedIndex = 0;
            
            // Buton textlerini güncelle
            btnYeniBasvuru.Text = "💳  Kart Başvurusu Yap";
            btnYenile.Text = "🔄  Yenile";
            btnKapat.Text = "❌  Kapat";
        }

        private void TxtMusteriArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMusteriArama.Text))
            {
                gridMusteriler.DataSource = null;
                gridBasvurular.DataSource = null;
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
                _seciliMusteriID = CommonFunctions.DbNullToInt(musteriIDObj);
                if (_seciliMusteriID == 0) return;
                
                // Müşterinin hesaplarını yükle
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
                if (_seciliMusteriID == 0)
                {
                    gridBasvurular.DataSource = null;
                    return;
                }

                DataTable hesaplar;
                string hata = _sKart.GetMusteriHesaplari(_seciliMusteriID, out hesaplar);

                if (hata != null)
                {
                    gridBasvurular.DataSource = null;
                    return;
                }

                gridBasvurular.DataSource = hesaplar;
                gridViewBasvurular.BestFitColumns();
            }
            catch (Exception ex)
            {
                gridBasvurular.DataSource = null;
            }
        }

        private void BtnYeniBasvuru_Click(object sender, EventArgs e)
        {
            try
            {
                if (_seciliMusteriID == 0)
                {
                    XtraMessageBox.Show("Lütfen önce bir müşteri seçiniz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Seçili hesap al
                if (gridViewBasvurular.RowCount == 0)
                {
                    XtraMessageBox.Show("Müşterinin aktif hesabı bulunmamaktadır.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int selectedRow = gridViewBasvurular.FocusedRowHandle;
                if (selectedRow < 0)
                {
                    XtraMessageBox.Show("Lütfen kart için bir hesap seçiniz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                object hesapIDObj = gridViewBasvurular.GetRowCellValue(selectedRow, "HesapID");
                _seciliHesapID = CommonFunctions.DbNullToInt(hesapIDObj);

                if (_seciliHesapID == 0)
                {
                    XtraMessageBox.Show("Geçersiz hesap seçimi.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kart markası al
                string kartMarkasi = cmbBasvuruTipi.Text;
                if (string.IsNullOrEmpty(kartMarkasi))
                {
                    XtraMessageBox.Show("Lütfen kart markası seçiniz (Troy veya Mastercard).", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Müşteri adını al
                string musteriAdi = "";
                MusteriModel musteri;
                string hataMusteri = _sMusteri.MusteriGetir(_seciliMusteriID, out musteri);
                if (hataMusteri == null && musteri != null)
                {
                    musteriAdi = $"{musteri.Ad} {musteri.Soyad}";
                }

                // Onay iste
                DialogResult result = XtraMessageBox.Show(
                    $"Kart Başvurusu Onayı\n\n" +
                    $"Kart Markası: {kartMarkasi}\n" +
                    $"Kart Sahibi: {musteriAdi}\n" +
                    $"Son Kullanma: {DateTime.Now.AddYears(5):MM/yyyy}\n\n" +
                    $"Başvuruyu onaylıyor musunuz?",
                    "Kart Başvurusu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Kartı oluştur
                int kartID;
                string hata = _sKart.CreateCard(_seciliHesapID, kartMarkasi, musteriAdi, _kullanici.KullaniciID, out kartID);

                if (hata != null)
                {
                    XtraMessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Başarılı mesajı
                long kartNo = SKart.GenerateCardNumber(kartMarkasi);
                string kartNoFormatli = $"{kartNo.ToString("D16").Substring(0, 4)} {kartNo.ToString("D16").Substring(4, 4)} " +
                                       $"{kartNo.ToString("D16").Substring(8, 4)} {kartNo.ToString("D16").Substring(12, 4)}";

                XtraMessageBox.Show(
                    $"✅ Kart başvurusu başarıyla tamamlandı!\n\n" +
                    $"Kart No: {kartNoFormatli}\n" +
                    $"Kart Markası: {kartMarkasi}\n" +
                    $"Son Kullanma: {DateTime.Now.AddYears(5):MM/yyyy}\n" +
                    $"CVV: ***",
                    "Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Hesapları yenile
                HesaplariYukle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnYenile_Click(object sender, EventArgs e)
        {
            HesaplariYukle();
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
