using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmSubeDegisiklik : XtraForm
    {
        private readonly KullaniciModel _kullanici;
        private readonly SSubeDegisiklik _sSubeDegisiklik;
        private readonly DataAccess _dataAccess;

        public FrmSubeDegisiklik(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sSubeDegisiklik = new SSubeDegisiklik();
            _dataAccess = new DataAccess();
        }

        private void FrmSubeDegisiklik_Load(object sender, EventArgs e)
        {
            // Mevcut şube bilgisini göster
            if (_kullanici.SubeID.HasValue)
            {
                lblMevcutSube.Text = $"Mevcut Şube: {_kullanici.SubeAdi} ({_kullanici.SubeID})";
            }
            else
            {
                lblMevcutSube.Text = "Mevcut Şube: Atanmamış";
                XtraMessageBox.Show("Henüz bir şubeye atanmamışsınız.\nŞube değişikliği talebi oluşturamazsınız.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnGonder.Enabled = false;
                return;
            }

            // Şubeleri yükle
            SubeleriYukle();

            // Mevcut talep durumunu kontrol et
            TalepDurumuKontrol();
        }

        private void SubeleriYukle()
        {
            try
            {
                string query = "SELECT SubeID, SubeKodu, SubeAdi, Sehir FROM Sube WHERE AktifMi = 1 ORDER BY SubeAdi";
                DataTable dtSubeler;
                string hata = _dataAccess.ExecuteQuery(query, null, out dtSubeler);

                if (hata != null)
                {
                    XtraMessageBox.Show($"Şubeler yüklenirken hata oluştu: {hata}", 
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mevcut şubeyi listeden çıkar
                if (_kullanici.SubeID.HasValue)
                {
                    DataRow[] rows = dtSubeler.Select($"SubeID = {_kullanici.SubeID.Value}");
                    foreach (DataRow row in rows)
                    {
                        dtSubeler.Rows.Remove(row);
                    }
                }

                lookUpYeniSube.Properties.DataSource = dtSubeler;
                lookUpYeniSube.Properties.DisplayMember = "SubeAdi";
                lookUpYeniSube.Properties.ValueMember = "SubeID";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Şubeler yüklenirken hata oluştu: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void TalepDurumuKontrol()
        {
            try
            {
                DataTable dtTalepler;
                string hata = _sSubeDegisiklik.KullaniciTalepDurumuGetir(_kullanici.KullaniciID, out dtTalepler);

                if (hata != null)
                {
                    // Hata olsa bile devam et
                    return;
                }

                if (dtTalepler != null && dtTalepler.Rows.Count > 0)
                {
                    // En son talebi al
                    DataRow talepRow = dtTalepler.Rows[0];
                    string onayDurumu = talepRow["OnayDurumu"].ToString();

                    if (onayDurumu == "Beklemede")
                    {
                        // Bekleyen talep var - Formu disable et
                        panelTalepDurum.Visible = true;
                        lookUpYeniSube.Enabled = false;
                        memoTalepNedeni.Enabled = false;
                        btnGonder.Enabled = false;

                        // Durum bilgilerini göster
                        lblTalepDurumu.Text = "Durum: Onay Bekliyor";
                        lblTalepDurumu.Appearance.ForeColor = System.Drawing.Color.Orange;
                        lblTalepTarihi.Text = $"Talep Tarihi: {Convert.ToDateTime(talepRow["TalepTarihi"]):dd.MM.yyyy HH:mm}";
                       
                        string yeniSubeAdi = talepRow["YeniSubeAdi"].ToString();
                        XtraMessageBox.Show($"Zaten beklemede olan bir talebiniz bulunmaktadır.\n\nTalep Edilen Şube: {yeniSubeAdi}\n\nLütfen bu talebin sonuçlanmasını bekleyiniz.",
                            "Bekleyen Talep", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (onayDurumu == "Onaylandı")
                    {
                        panelTalepDurum.Visible = true;
                        lblTalepDurumu.Text = "Durum: Onaylandı";
                        lblTalepDurumu.Appearance.ForeColor = System.Drawing.Color.Green;
                        lblTalepTarihi.Text = $"Talep Tarihi: {Convert.ToDateTime(talepRow["TalepTarihi"]):dd.MM.yyyy HH:mm}";
                        lblOnayTarihi.Visible = true;
                        lblOnayTarihi.Text = $"Onay Tarihi: {Convert.ToDateTime(talepRow["OnayTarihi"]):dd.MM.yyyy HH:mm}";
                    }
                    else if (onayDurumu == "Reddedildi")
                    {
                        panelTalepDurum.Visible = true;
                        lblTalepDurumu.Text = "Durum: Reddedildi";
                        lblTalepDurumu.Appearance.ForeColor = System.Drawing.Color.Red;
                        lblTalepTarihi.Text = $"Talep Tarihi: {Convert.ToDateTime(talepRow["TalepTarihi"]):dd.MM.yyyy HH:mm}";
                        lblOnayTarihi.Visible = true;
                        lblOnayTarihi.Text = $"Red Tarihi: {Convert.ToDateTime(talepRow["OnayTarihi"]):dd.MM.yyyy HH:mm}";
                        
                        string redNedeni = talepRow["RedNedeni"]?.ToString();
                        if (!string.IsNullOrEmpty(redNedeni))
                        {
                            XtraMessageBox.Show($"Son talebiniz reddedilmiştir.\n\nRed Nedeni: {redNedeni}",
                                "Reddedilen Talep", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Sessizce geç
            }
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyonlar
                if (lookUpYeniSube.EditValue == null)
                {
                    XtraMessageBox.Show("Lütfen talep etmek istediğiniz şubeyi seçiniz.",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lookUpYeniSube.Focus();
                    return;
                }

                string talepNedeni = memoTalepNedeni.Text?.Trim();
                if (string.IsNullOrWhiteSpace(talepNedeni))
                {
                    XtraMessageBox.Show("Lütfen talep nedeninizi belirtiniz.",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    memoTalepNedeni.Focus();
                    return;
                }

                if (talepNedeni.Length < 20)
                {
                    XtraMessageBox.Show("Talep nedeni en az 20 karakter olmalıdır.\n\nLütfen detaylı bir açıklama giriniz.",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    memoTalepNedeni.Focus();
                    return;
                }

                int yeniSubeID = Convert.ToInt32(lookUpYeniSube.EditValue);
                
                // Onay iste
                if (XtraMessageBox.Show($"Şube değişikliği talebinizi göndermek istediğinize emin misiniz?\n\nYeni Şube: {lookUpYeniSube.Text}\n\nTalep, müdürünüzün onayına sunulacaktır.",
                    "Talep Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                // Talebi oluştur
                int talepID;
                string hata = _sSubeDegisiklik.TalepOlustur(
                    _kullanici.KullaniciID,
                    _kullanici.SubeID.Value,
                    yeniSubeID,
                    talepNedeni,
                    out talepID
                );

                if (hata != null)
                {
                    XtraMessageBox.Show($"Talep oluşturulurken hata oluştu:\n\n{hata}",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XtraMessageBox.Show("Şube değişikliği talebiniz başarıyla oluşturuldu.\n\nTalep numaranız: " + talepID + "\n\nMüdürünüzün onayı bekleniyor.",
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Formu kapat
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen bir hata oluştu:\n\n{ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
