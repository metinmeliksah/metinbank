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
    public partial class FrmKrediBasvuru : XtraForm
    {
        private KullaniciModel _kullanici;
        private SMusteri _sMusteri;
        private SKredi _sKredi;
        private int _seciliMusteriID;
        private string _seciliMusteriAd;
        private long _seciliMusteriTCKN;
        private string _seciliMusteriCep;
        private System.Windows.Forms.Timer _aramaTimer;
        private decimal _hesaplananFaiz;

        public FrmKrediBasvuru(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
            _sKredi = new SKredi();

            _aramaTimer = new System.Windows.Forms.Timer();
            _aramaTimer.Interval = 500;
            _aramaTimer.Tick += (s, e) => {
                _aramaTimer.Stop();
                MusteriAra();
            };

            txtMusteriArama.TextChanged += (s, e) => {
                 _aramaTimer.Stop();
                 _aramaTimer.Start();
            };
            
            gridViewMusteriler.RowClick += GridViewMusteriler_RowClick;
            btnHesapla.Click += BtnHesapla_Click;
            btnBasvur.Click += BtnBasvur_Click;
            txtVade.SelectedIndexChanged += TxtVade_SelectedIndexChanged;

            // Grid Ayarları (Sütun Gizleme)
            gridViewMusteriler.OptionsBehavior.AutoPopulateColumns = false;
            gridViewMusteriler.Columns.Clear();
            
            var colID = gridViewMusteriler.Columns.AddVisible("MusteriID");
            colID.Visible = false;
            
            var colTC = gridViewMusteriler.Columns.AddVisible("TCKN");
            colTC.Caption = "T.C. Kimlik No";
            
            var colAd = gridViewMusteriler.Columns.AddVisible("Ad");
            colAd.Caption = "Adı";
            
            var colSoyad = gridViewMusteriler.Columns.AddVisible("Soyad");
            colSoyad.Caption = "Soyadı";

            // Diğer alanları (Cep, Gelir vb) veri kaynağında olsa bile göstermiyoruz
            // ama RowClick olayında satırdan okuyabiliriz.
        }

        private void TxtVade_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Vade seçilince faiz oranını getirip gösterelim (Tutar girildiyse)
            if (string.IsNullOrEmpty(txtVade.Text)) return;
            
            decimal tutar;
            if (decimal.TryParse(txtTutar.Text, out tutar) && tutar > 0)
            {
                 // Ör: 10000 TL, 12 Ay -> Oran ne?
                 // BKredi servisinde GetUygunOran var.
                 // Ancak şimdilik basitçe Hesapla çağırıp oradan alabiliriz.
                 try
                 {
                     int vade = Convert.ToInt32(txtVade.Text);
                     var sonuc = _sKredi.Hesapla(tutar, vade);
                     if (!sonuc.ContainsKey("Hata"))
                     {
                         decimal oran = (decimal)sonuc["FaizOrani"];
                         lblToplamOdeme.Text = $"Faiz Oranı: %{oran} (Taksitleri görmek için Hesapla'ya basın)";
                     }
                 }
                 catch { }
            }
        }

        private void MusteriAra()
        {
            try
            {
                string arama = txtMusteriArama.Text.Trim();
                // Kullanıcı boş aramaya basınca tüm listeyi getirmesin, en az 2 harf/rakam
                if (arama.Length < 2) return;

                DataTable dt;
                _sMusteri.MusteriAra(arama, _kullanici.SubeID, false, out dt);
                
                // Grid'e sadece TCKN, Ad, Soyad, ID, Cep, Gelir kolonlarını içeren DataTable basılıyor
                // Biz yukarıdaColumns.AddVisible ile hangilerinin görüneceğini seçtik.
                gridMusteriler.DataSource = dt;
            }
            catch {}
        }

        private void GridViewMusteriler_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            
            _seciliMusteriID = Convert.ToInt32(gridViewMusteriler.GetRowCellValue(e.RowHandle, "MusteriID"));
            string ad = gridViewMusteriler.GetRowCellValue(e.RowHandle, "Ad").ToString();
            string soyad = gridViewMusteriler.GetRowCellValue(e.RowHandle, "Soyad").ToString();
            _seciliMusteriTCKN = Convert.ToInt64(gridViewMusteriler.GetRowCellValue(e.RowHandle, "TCKN"));
            
            // Gizli sütunlardan veriyi alırken dikkatli olalım. 
            // Eğer Veri Kaynağında varsa GetRowCellValue ile gelir mi? 
            // DevExpress Grid eğer Column eklenmemişse GetRowCellValue ile döndürmeyebilir (DataSource'a bağlı).
            // En güvenlisi DataRowView'den almak.
            
            DataRow row = gridViewMusteriler.GetDataRow(e.RowHandle);
            if (row != null)
            {
                 _seciliMusteriCep = row["CepTelefon"].ToString();
                 object gelirObj = row["GelirDurumu"];
                 if (gelirObj != DBNull.Value && Convert.ToDecimal(gelirObj) > 0)
                    txtGelir.Text = Convert.ToDecimal(gelirObj).ToString("N2");
            }

            _seciliMusteriAd = ad + " " + soyad;
            lblSeciliMusteri.Text = "Seçili: " + _seciliMusteriAd;
        }

        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                decimal tutar = 0;
                if (!decimal.TryParse(txtTutar.Text, out tutar) || tutar <= 0) 
                { 
                    XtraMessageBox.Show("Geçerli bir tutar giriniz."); 
                    return; 
                }

                if (txtVade.Text == "") 
                { 
                    XtraMessageBox.Show("Vade seçiniz."); 
                    return; 
                }
                int vade = Convert.ToInt32(txtVade.Text);

                var sonuc = _sKredi.Hesapla(tutar, vade);
                if (sonuc.ContainsKey("Hata"))
                {
                    XtraMessageBox.Show(sonuc["Hata"].ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _hesaplananFaiz = (decimal)sonuc["FaizOrani"];
                decimal aylikTaksit = (decimal)sonuc["AylikTaksit"];
                decimal toplamOdeme = (decimal)sonuc["ToplamOdeme"];
                
                lblAylikTaksit.Text = $"Taksit: {aylikTaksit:N2} TL";
                lblToplamOdeme.Text = $"Toplam Geri Ödeme: {toplamOdeme:N2} TL (Faiz: %{_hesaplananFaiz})";

                // Ödeme Planı Tablosunu Oluştur (Simülasyon)
                DataTable dtPlan = new DataTable();
                dtPlan.Columns.Add("TaksitNo", typeof(int));
                dtPlan.Columns.Add("Tarih", typeof(DateTime));
                dtPlan.Columns.Add("TaksitTutari", typeof(decimal));
                dtPlan.Columns.Add("AnaPara", typeof(decimal));
                dtPlan.Columns.Add("Faiz", typeof(decimal));
                dtPlan.Columns.Add("KalanAnaPara", typeof(decimal));

                decimal kalanAnaPara = tutar;
                DateTime tarih = DateTime.Now;

                // Vergi Oranları (Business katmanından gelmeli normalde ama UI simülasyonu için sabit alıyoruz)
                decimal kkdf = 0.15m;
                decimal bsmv = 0.15m;
                decimal vergiCarpani = 1 + kkdf + bsmv;
                
                // _hesaplananFaiz (Örn: 4.29) -> Brüt Aylık Oran'a çevir
                decimal brutAylikOran = (_hesaplananFaiz / 100m) * vergiCarpani;

                for (int i = 1; i <= vade; i++)
                {
                    tarih = tarih.AddMonths(1);
                    
                    // Faiz = Kalan AnaPara * Brüt Aylık Oran
                    decimal faiz = Math.Round(kalanAnaPara * brutAylikOran, 2); 
                    decimal anapara = Math.Round(aylikTaksit - faiz, 2);
                    
                    // Son taksit düzeltmesi
                    if (i == vade || anapara > kalanAnaPara)
                    {
                        anapara = kalanAnaPara;
                        aylikTaksit = faiz + anapara;
                    }

                    kalanAnaPara -= anapara;
                    // Floating point hatalarını önlemek için son adımda 0'a çekelim
                    if (kalanAnaPara < 0) kalanAnaPara = 0;
                    
                    dtPlan.Rows.Add(i, tarih, aylikTaksit, anapara, faiz, kalanAnaPara);
                }

                gridOdemePlani.DataSource = dtPlan;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hesaplama hatası: " + ex.Message);
            }
        }

        private void BtnBasvur_Click(object sender, EventArgs e)
        {
            if (_seciliMusteriID == 0)
            {
                XtraMessageBox.Show("Lütfen müşteri seçiniz.");
                return;
            }

            try
            {
                decimal gelir = 0;
                decimal.TryParse(txtGelir.Text, out gelir);

                if (gelir <= 0) { XtraMessageBox.Show("Aylık gelir bilgisi zorunludur."); return; }

                KrediBasvuruModel model = new KrediBasvuruModel
                {
                    MusteriID = _seciliMusteriID,
                    TCKN = _seciliMusteriTCKN,
                    AdSoyad = _seciliMusteriAd,
                    CepTelefon = _seciliMusteriCep,
                    AylikGelir = gelir,
                    TalepEdilenTutar = Convert.ToDecimal(txtTutar.EditValue),
                    TalepEdilenVade = Convert.ToInt32(txtVade.Text),
                    Kanal = "Sube"
                };

                string sonuc = _sKredi.BasvuruYap(model);
                
                if (sonuc == "ONAYLANDI")
                {
                    XtraMessageBox.Show("Kredi başvurusu ONAYLANDI!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (sonuc == "REDDEDILDI")
                {
                    XtraMessageBox.Show("Kredi başvurusu REDDEDİLDİ. (Gelir/Taksit oranı uygun değil)", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    XtraMessageBox.Show("Başvuru alındı, Müdür Onayına gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Başvuru hatası: " + ex.Message);
            }
        }
    }
}
