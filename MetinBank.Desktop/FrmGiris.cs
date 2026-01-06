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
            if (!this.DesignMode)
            {
                _sAuth = new SAuth();
            }
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
            // if (System.Diagnostics.Debugger.IsAttached)
            // {
            //     txtKullaniciAdi.Text = "merkez.calisan1";
            //     txtSifre.Text = "Password123!";
            // }

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
            // WXI Skin handles backgrounds automatically
            // layoutControl1.Appearance.Control.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            // layoutControl1.Appearance.Control.Options.UseBackColor = true;

            // Logo background transparency
            pictureEdit1.BackColor = System.Drawing.Color.Transparent;
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            pictureEdit1.Properties.Appearance.Options.UseBackColor = true;

            // Giriş butonu
            btnGiris.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnGiris.Height = 40;
            btnGiris.Text = "GİRİŞ YAP";

            // Çıkış butonu
            btnCikis.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            btnCikis.Text = "ÇIKIŞ";

            // Text kutuları için modern görünüm
            txtKullaniciAdi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            txtKullaniciAdi.Properties.Appearance.Options.UseFont = true;
            txtKullaniciAdi.Properties.NullValuePrompt = "Kullanıcı Adı";
            txtKullaniciAdi.Properties.NullValuePromptShowForEmptyValue = true;
            try {
                txtKullaniciAdi.Properties.ContextImageOptions.SvgImage = DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.People.user.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly);
                txtKullaniciAdi.Properties.ContextImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            } catch {}

            txtSifre.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            txtSifre.Properties.Appearance.Options.UseFont = true;
            txtSifre.Properties.NullValuePrompt = "Şifre";
            txtSifre.Properties.NullValuePromptShowForEmptyValue = true;
            try {
                txtSifre.Properties.ContextImageOptions.SvgImage = DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Security.key.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly);
                txtSifre.Properties.ContextImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            } catch {}
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

                // Ana mdi formuna yönlendir
                this.Hide();
                FrmMain frmMain = new FrmMain(kullanici);
                DialogResult result = frmMain.ShowDialog();

                if (result == DialogResult.Abort)
                {
                    this.Close();
                    return;
                }
                
                // Ana form kapandığında giriş formunu tekrar göster (Logout)
                this.Show();
                txtSifre.Clear();
                txtKullaniciAdi.Clear();
                txtKullaniciAdi.Focus();
                
                btnGiris.Enabled = true;
                btnGiris.Text = "Giriş Yap";
            }
            catch (Exception ex)
            {
                // Detaylı hata göster - debug için
                string hataMesaji = $"Beklenmeyen hata: {ex.Message}\n\nDetay:\n{ex.StackTrace}";
                if (ex.InnerException != null)
                {
                    hataMesaji += $"\n\nİç Hata: {ex.InnerException.Message}\n{ex.InnerException.StackTrace}";
                }
                MessageBox.Show(hataMesaji, "Hata", 
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

