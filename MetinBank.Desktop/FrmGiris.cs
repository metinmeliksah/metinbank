using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    /// <summary>
    /// Giriş formu
    /// </summary>
    public partial class FrmGiris : XtraForm
    {
        private readonly SAuth _sAuth;

        public FrmGiris()
        {
            InitializeComponent();
            _sAuth = new SAuth();
        }

        /// <summary>
        /// Form yüklendiğinde
        /// </summary>
        private void FrmGiris_Load(object sender, EventArgs e)
        {
            // Form ayarları
            this.Text = "Metin Bank - Kullanıcı Girişi";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Modern görünüm ayarları
            ApplyModernStyling();

            // Varsayılan değerler (test için)
            if (System.Diagnostics.Debugger.IsAttached)
            {
                txtKullaniciAdi.Text = "merkez.calisan1";
                txtSifre.Text = "Password123!";
            }

            // Enter tuşu ile giriş
            txtKullaniciAdi.KeyPress += TxtKeyPress;
            txtSifre.KeyPress += TxtKeyPress;

            txtKullaniciAdi.Focus();
        }

        /// <summary>
        /// Modern stil ayarları
        /// </summary>
        private void ApplyModernStyling()
        {
            // Gradient arka plan efekti için form boyama
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            
            // Layout control görünümü
            layoutControl1.Appearance.Control.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            layoutControl1.Appearance.Control.Options.UseBackColor = true;

            // Giriş butonu - Modern mavi stili
            btnGiris.Appearance.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            btnGiris.Appearance.BackColor2 = System.Drawing.Color.FromArgb(25, 118, 210);
            btnGiris.Appearance.ForeColor = System.Drawing.Color.White;
            btnGiris.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnGiris.Appearance.Options.UseBackColor = true;
            btnGiris.Appearance.Options.UseForeColor = true;
            btnGiris.Appearance.Options.UseFont = true;
            btnGiris.Text = "🔐  GİRİŞ YAP";

            // Çıkış butonu
            btnCikis.Appearance.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            btnCikis.Appearance.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            btnCikis.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnCikis.Appearance.Options.UseBackColor = true;
            btnCikis.Appearance.Options.UseForeColor = true;
            btnCikis.Appearance.Options.UseFont = true;
            btnCikis.Text = "✖  ÇIKIŞ";

            // Text kutuları için modern görünüm
            txtKullaniciAdi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtKullaniciAdi.Properties.Appearance.Options.UseFont = true;
            txtKullaniciAdi.Properties.NullValuePrompt = "👤  Kullanıcı adınızı giriniz";
            txtKullaniciAdi.Properties.NullValuePromptShowForEmptyValue = true;

            txtSifre.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtSifre.Properties.Appearance.Options.UseFont = true;
            txtSifre.Properties.NullValuePrompt = "🔒  Şifrenizi giriniz";
            txtSifre.Properties.NullValuePromptShowForEmptyValue = true;
        }

        /// <summary>
        /// Enter tuşu ile giriş
        /// </summary>
        private void TxtKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnGiris_Click(sender, e);
            }
        }

        /// <summary>
        /// Giriş butonu
        /// </summary>
        private void BtnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyon
                if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
                {
                    MessageBox.Show("Kullanıcı adı boş olamaz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtKullaniciAdi.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSifre.Text))
                {
                    MessageBox.Show("Şifre boş olamaz.", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSifre.Focus();
                    return;
                }

                // Loading göster
                btnGiris.Enabled = false;
                btnGiris.Text = "Giriş yapılıyor...";
                Application.DoEvents();

                // IP ve MAC adresi al
                string ipAdresi = CommonFunctions.GetLocalIPAddress();
                string macAdresi = CommonFunctions.GetMacAddress();

                // Login işlemi
                KullaniciModel kullanici;
                string hata = _sAuth.Login(
                    txtKullaniciAdi.Text.Trim(),
                    txtSifre.Text,
                    ipAdresi,
                    macAdresi,
                    out kullanici
                );

                if (hata != null)
                {
                    MessageBox.Show(hata, "Giriş Başarısız", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    btnGiris.Enabled = true;
                    btnGiris.Text = "Giriş Yap";
                    txtSifre.Clear();
                    txtSifre.Focus();
                    return;
                }

                // Başarılı giriş
                MessageBox.Show($"Hoş geldiniz, {kullanici.TamAd}", "Giriş Başarılı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ana MDI formuna yönlendir
                this.Hide();
                FrmMain frmMain = new FrmMain(kullanici);
                frmMain.ShowDialog();
                
                // Ana form kapandığında giriş formunu tekrar göster
                this.Show();
                txtSifre.Clear();
                txtKullaniciAdi.Focus();
                
                btnGiris.Enabled = true;
                btnGiris.Text = "Giriş Yap";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                btnGiris.Enabled = true;
                btnGiris.Text = "Giriş Yap";
            }
        }

        /// <summary>
        /// İptal butonu
        /// </summary>
        private void BtnIptal_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Uygulamadan çıkmak istediğinize emin misiniz?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Şifremi Unuttum linki
        /// </summary>
        private void LnkSifremiUnuttum_LinkClicked(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Şifre sıfırlama için lütfen sistem yöneticiniz ile iletişime geçin.\n\n" +
                "Yetkili: Genel Merkez\n" +
                "Telefon: 0212 123 45 67",
                "Şifremi Unuttum",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Çıkış butonu
        /// </summary>
        private void BtnCikis_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Uygulamadan çıkmak istediğinize emin misiniz?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}

