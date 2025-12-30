using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Forms
{
    public partial class FrmBasvurular : XtraForm
    {
        private KullaniciModel _kullanici;
        private SMusteri _sMusteri;
        private SHesap _sHesap;
        private int _seciliMusteriID;
        private System.Windows.Forms.Timer _aramaTimer;

        public FrmBasvurular(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
            _sHesap = new SHesap();
            
            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };
        }

        private void FrmBasvurular_Load(object sender, EventArgs e)
        {
            this.Text = "Başvurular";
            cmbBasvuruTipi.Properties.Items.AddRange(new string[] { "Kart Başvurusu", "Kart İptal Başvurusu" });
            cmbBasvuruTipi.SelectedIndex = 0;
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
                BasvurulariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BasvurulariYukle()
        {
            try
            {
                if (_seciliMusteriID == 0)
                {
                    gridBasvurular.DataSource = null;
                    return;
                }

                DataAccess dataAccess = new DataAccess();
                string query = @"SELECT k.KartID, k.KartNo, k.KartTipi, k.Durum, k.BasvuruTarihi,
                                k.KartSahibiAdi, h.IBAN, m.MusteriNo, CONCAT(m.Ad, ' ', m.Soyad) as MusteriAdi
                                FROM BankaKarti k
                                INNER JOIN Hesap h ON k.HesapID = h.HesapID
                                INNER JOIN Musteri m ON h.MusteriID = m.MusteriID
                                WHERE m.MusteriID = @musteriID AND k.Durum IN ('Basvuru', 'Iptal')
                                ORDER BY k.BasvuruTarihi DESC";

                MySql.Data.MySqlClient.MySqlParameter[] parameters = new MySql.Data.MySqlClient.MySqlParameter[]
                {
                    new MySql.Data.MySqlClient.MySqlParameter("@musteriID", _seciliMusteriID)
                };

                DataTable dt;
                string hata = dataAccess.ExecuteQuery(query, parameters, out dt);
                dataAccess.CloseConnection();

                if (hata != null)
                {
                    gridBasvurular.DataSource = null;
                    return;
                }

                gridBasvurular.DataSource = dt;
                gridViewBasvurular.BestFitColumns();
            }
            catch (Exception ex)
            {
                gridBasvurular.DataSource = null;
            }
        }

        private void BtnYeniBasvuru_Click(object sender, EventArgs e)
        {
            if (_seciliMusteriID == 0)
            {
                MessageBox.Show("Lütfen önce bir müşteri seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Yeni başvuru formu açılacak. (TODO: Hesap seçimi ile)", 
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnYenile_Click(object sender, EventArgs e)
        {
            BasvurulariYukle();
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
