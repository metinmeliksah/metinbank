using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using MetinBank.Models;

using System.Text;

namespace MetinBank.Desktop
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        private readonly KullaniciModel _kullanici;

        public FrmMain(KullaniciModel kullanici)
        {
            // Apply DevExpress skin before InitializeComponent
            // UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            InitializeComponent();
            _kullanici = kullanici;
            this.IsMdiContainer = true;
            ConfigureUI();
            ConfigureCleanUI();
            LoadNavBarIcons();
            
            // Load dashboard as default opening screen
            LoadDashboard();
        }

        private void ConfigureCleanUI()
        {
            // Reset manual colors to allow WXI Skin to take full effect
            navBarControl1.Appearance.Background.BackColor = System.Drawing.Color.Empty;
            navBarControl1.Appearance.Background.Options.UseBackColor = false;
            
            navBarControl1.Appearance.GroupHeader.BackColor = System.Drawing.Color.Empty;
            navBarControl1.Appearance.GroupHeader.Options.UseBackColor = false;
            navBarControl1.Appearance.GroupHeader.ForeColor = System.Drawing.Color.Empty;
            navBarControl1.Appearance.GroupHeader.Options.UseForeColor = false;
            
            navBarControl1.Appearance.Item.ForeColor = System.Drawing.Color.Empty;
            navBarControl1.Appearance.Item.Options.UseForeColor = false;
            
            navBarControl1.Appearance.ItemActive.BackColor = System.Drawing.Color.Empty;
            navBarControl1.Appearance.ItemActive.Options.UseBackColor = false;
            navBarControl1.Appearance.ItemActive.ForeColor = System.Drawing.Color.Empty;
            navBarControl1.Appearance.ItemActive.Options.UseForeColor = false;
            
            // Clean Bar items if they have hardcoded colors
            barStaticItemLogo.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Empty;
            barStaticItemLogo.ItemAppearance.Normal.Options.UseForeColor = false;
            
            barStaticItemKullanici.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Empty;
            barStaticItemKullanici.ItemAppearance.Normal.Options.UseForeColor = false;
        }

        private void ConfigureUI()
        {
            // Set user information in header - null check eklendi
            string tamAd = _kullanici?.TamAd ?? "Kullanıcı";
            string rolAdi = _kullanici?.RolAdi ?? "Bilinmiyor";
            barStaticItemKullanici.Caption = $"👤 {tamAd} ({rolAdi})";
            
            // Set current date/time
            barStaticItemTarih.Caption = $"📅 {DateTime.Now:dd.MM.yyyy HH:mm}";
            
            // Start timer for updating time
            timer1.Start();
        }

        private void LoadNavBarIcons()
        {
            try
            {
                // Müşteri İşlemleri
                SetNavBarIcon(navBarItemMusteriEkle, "account-plus");
                SetNavBarIcon(navBarItemMusteriIslem, "account-group");
                SetNavBarIcon(navBarItemIslemGecmisi, "history");

                // Hesap İşlemleri
                SetNavBarIcon(navBarItemVadesizHesap, "wallet-plus");
                SetNavBarIcon(navBarItemVadeliHesap, "briefcase-plus");
                SetNavBarIcon(navBarItemParaYatir, "cloud-download");
                SetNavBarIcon(navBarItemParaCek, "cloud-upload");

                // Transfer İşlemleri
                SetNavBarIcon(navBarItemHavale, "bank-transfer");
                SetNavBarIcon(navBarItemEFT, "bank-transfer-out");
                SetNavBarIcon(navBarItemVirman, "swap-horizontal");

                // Kart & Başvuru
                SetNavBarIcon(navBarItemKartlar, "credit-card");
                SetNavBarIcon(navBarItemKrediBasvuru, "cash-multiple");
                SetNavBarIcon(navBarItemBasvurular, "file-document");

                // Yönetim
                SetNavBarIcon(navBarItemOnayBekleyenler, "clipboard-check");
                SetNavBarIcon(navBarItemDovizAlSat, "currency-usd");
                SetNavBarIcon(navBarItemSubeDegisiklik, "domain");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Icon loading error: {ex.Message}");
            }

            // Yetki kontrolleri
            try
            {
                string rol = _kullanici?.RolAdi ?? "";
                bool isMudurOrMerkez = rol.IndexOf("Mudur", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    rol.IndexOf("Müdür", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    rol.IndexOf("Genel", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    rol.IndexOf("Merkez", StringComparison.OrdinalIgnoreCase) >= 0;

                if (navBarItemOnayBekleyenler != null)
                    navBarItemOnayBekleyenler.Visible = isMudurOrMerkez;

                if (navBarItemMusteriIslem != null)
                    navBarItemMusteriIslem.Visible = isMudurOrMerkez;

                if (navBarItemSubeDegisiklik != null)
                    navBarItemSubeDegisiklik.Visible = _kullanici?.SubeID.HasValue == true;
            }
            catch { }
        }

        private void SetNavBarIcon(DevExpress.XtraNavBar.NavBarItem item, string iconName)
        {
            if (item == null) return;
            try
            {
                string svgContent = GetSvgContent(iconName);
                if (!string.IsNullOrEmpty(svgContent))
                {
                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(svgContent)))
                    {
                        item.ImageOptions.SvgImage = DevExpress.Utils.Svg.SvgImage.FromStream(ms);
                        // Optional: Reset size to let it scale or fixed 32x32
                        // item.ImageOptions.SvgImageSize = new Size(32, 32); 
                    }
                }
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine($"Error setting icon {iconName}: {ex.Message}"); 
            }
        }

        private string GetSvgContent(string name)
        {
            string color = "#1565C0"; // Dark Blue (MetinBank Theme)
            string path = "";
            switch (name)
            {
                case "account-plus": path = "M15,14C12.33,14 7,15.33 7,18V20H23V18C23,15.33 17.67,14 15,14M6,10V7H4V10H1V12H4V15H6V12H9V10M15,12A4,4 0 0,0 19,8A4,4 0 0,0 15,4A4,4 0 0,0 11,8A4,4 0 0,0 15,12Z"; break;
                case "account-group": path = "M12,5.5A3.5,3.5 0 0,1 15.5,9A3.5,3.5 0 0,1 12,12.5A3.5,3.5 0 0,1 8.5,9A3.5,3.5 0 0,1 12,5.5M5,8C5.56,8 6.08,8.15 6.53,8.42C6.38,9.85 6.8,11.27 7.66,12.38C7.16,13.34 6.16,14 5,14A3,3 0 0,1 2,11A3,3 0 0,1 5,8M19,8A3,3 0 0,1 22,11A3,3 0 0,1 19,14C17.84,14 16.84,13.34 16.34,12.38C17.2,11.27 17.62,9.85 17.47,8.42C17.92,8.15 18.44,8 19,8M5.5,18.25C5.5,16.18 8.41,14.5 12,14.5C15.59,14.5 18.5,16.18 18.5,18.25V20H5.5V18.25M0,20V18.5C0,17.11 1.89,15.94 4.45,15.6C3.86,16.28 3.5,17.22 3.5,18.25V20H0M24,20H20.5V18.25C20.5,17.22 20.14,16.28 19.55,15.6C22.11,15.94 24,17.11 24,18.5V20Z"; break;
                case "history": path = "M13.5,8H12V13L16.28,15.54L17,14.33L13.5,12.25V8M13,3A9,9 0 0,0 4,12H1L4.96,16.03L9,12H6A7,7 0 0,1 13,5A7,7 0 0,1 20,12A7,7 0 0,1 13,19C11.07,19 9.32,18.21 8.06,16.94L6.64,18.36C8.27,20 10.5,21 13,21A9,9 0 0,0 22,12A9,9 0 0,0 13,3"; break;
                case "wallet-plus": path = "M13 19C13 19.34 13.04 19.67 13.09 20H4C2.9 20 2 19.11 2 18V6C2 4.89 2.9 4 4 4H10V2H12V4H20C21.1 4 22 4.89 22 6V13.81C21.12 13.3 20.1 13 19 13C15.69 13 13 15.69 13 19M20 6H4V18H11.09C11.04 17.67 11 17.34 11 17C11 15.93 11.21 14.92 11.58 14H4V8H20V6M18 15V18H15V20H18V23H20V20H23V18H20V15H18Z"; break;
                case "briefcase-plus": path = "M10,2H14A2,2 0 0,1 16,4V6H20A2,2 0 0,1 22,8V13.53C20.94,12.58 19.54,12 18,12A6,6 0 0,0 12,18C12,19.09 12.29,20.12 12.8,21H4C2.89,21 2,20.1 2,19V8C2,6.89 2.89,6 4,6H8V4C8,2.89 8.89,2 10,2M14,6V4H10V6H14M17,14H19V17H22V19H19V22H17V19H14V17H17V14Z"; break;
                case "cloud-download": path = "M5,20H19V18H5M19,9H15V3H9V9H5L12,16L19,9Z"; break;
                case "cloud-upload": path = "M9,16V10H5L12,3L19,10H15V16H9M5,20V18H19V20H5Z"; break;
                case "bank-transfer": path = "M15,14L21,17L15,20V18H1V16H15M9,10L3,7L9,4V6H23V8H9V10Z"; break;
                case "bank-transfer-out": path = "M3,22V12L11,17L3,22M22,14V16H13V14H22M22,10V12H13V10H22M22,6V8H13V6H22M11,2H2V4H4V9H6V4H11V2Z"; break;
                case "swap-horizontal": path = "M21,9L17,5V8H10V10H17V13M7,11L3,15L7,19V16H14V14H7V11Z"; break;
                case "credit-card": path = "M20 4H4C2.89 4 2 4.89 2 6V18C2 19.11 2.9 20 4 20H20C21.11 20 22 19.11 22 18V6C22 4.89 21.11 4 20 4M20 18H4V12H20V18M20 8H4V6H20V8Z"; break;
                case "cash-multiple": path = "M5,6H23V4H5M5,10H23V8H5M5,14H23V12H5M1,18H23V16H1M1,22H23V20H1"; break;
                case "file-document": path = "M14,2H6A2,2 0 0,0 4,4V20A2,2 0 0,0 6,22H18A2,2 0 0,0 20,20V8L14,2M16,11V9H13V11H16M16,15V13H13V15H16M16,19V17H13V19H16M11,11V9H8V11H11M11,15V13H8V15H11M11,19V17H8V19H11Z"; break;
                case "clipboard-check": path = "M12,2A3,3 0 0,1 15,5H19A2,2 0 0,1 21,7V19A2,2 0 0,1 19,21H5A2,2 0 0,1 3,19V7A2,2 0 0,1 5,5H9A3,3 0 0,1 12,2M12,4A1,1 0 0,0 11,5A1,1 0 0,0 12,6A1,1 0 0,0 13,5A1,1 0 0,0 12,4M10,17L6,13L7.41,11.59L10,14.17L16.59,7.58L18,9L10,17Z"; break;
                case "currency-usd": path = "M7,15H9C9,16.08 10.37,17 12,17C13.63,17 15,16.08 15,15C15,13.9 13.96,13.5 11.76,12.97C9.64,12.44 7,11.78 7,9C7,7.21 8.47,5.69 10.5,5.18V3H13.5V5.18C15.53,5.69 17,7.21 17,9H15C15,7.92 13.63,7 12,7C10.37,7 9,7.92 9,9C9,10.1 10.04,10.5 12.24,11.03C14.36,11.56 17,12.22 17,15C17,16.79 15.53,18.31 13.5,18.82V21H10.5V18.82C8.47,18.31 7,16.79 7,15Z"; break;
                case "domain": path = "M12,7V3H2V7H12M22,22H2V20H22V22M22,18H2V12H22V18M14,7V3H22V7H14M14,9H22V11H14V9M2,9H12V11H2V9Z"; break;
            }
            return $"<svg viewBox='0 0 24 24' xmlns='http://www.w3.org/2000/svg'><path fill='{color}' d='{path}' /></svg>";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            barStaticItemTarih.Caption = $"📅 {DateTime.Now:dd.MM.yyyy HH:mm}";
        }

        private void navBarItemMusteriEkle_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmMusteriEkle(_kullanici));
        }

        private void navBarItemMusteriIslem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmMusteriIslem(_kullanici));
        }

        private void navBarItemIslemGecmisi_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmIslemGecmisi(_kullanici));
        }

        private void navBarItemHesapIslem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmHesapIslem(_kullanici));
        }

        private void navBarItemParaYatir_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmParaYatir(_kullanici));
        }

        private void navBarItemParaCek_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmParaCek(_kullanici));
        }

        private void navBarItemHavale_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmHavale(_kullanici));
        }

        private void navBarItemEFT_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmEFT(_kullanici));
        }

        private void navBarItemVirman_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmVirman(_kullanici));
        }

        private void navBarItemKartlar_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmKartlar(_kullanici));
        }

        private void navBarItemBasvurular_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmBasvurular(_kullanici));
        }

        private void navBarItemOnayBekleyenler_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmOnayBekleyenler(_kullanici));
        }

        private void navBarItemDovizAlSat_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmDovizAlSat(_kullanici));
        }

        private void navBarItemSubeDegisiklik_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmSubeDegisiklik(_kullanici));
        }

        private void navBarItemKrediBasvuru_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmKrediBasvuru(_kullanici));
        }

        private void navBarItemVadeliHesap_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmVadeliHesapAc(_kullanici));
        }

        private void navBarItemVadesizHesap_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmVadesizHesapAc(_kullanici));
        }

        private void LoadDashboard()
        {
            OpenMdiChild(new FrmDashboard(_kullanici));
        }

        private void OpenMdiChild(Form childForm)
        {
            // Check if same form is already open - just activate it
            foreach (Form existingForm in this.MdiChildren)
            {
                if (existingForm.GetType() == childForm.GetType())
                {
                    existingForm.Activate();
                    childForm.Dispose();
                    return;
                }
            }

            // Close all other MDI children for smooth transition
            this.SuspendLayout();
            foreach (Form existingForm in this.MdiChildren)
            {
                existingForm.Close();
            }

            // Open new form as MDI child - prevent visible animation
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
            this.ResumeLayout(true);
        }

        private void barButtonItemCikis_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?", 
                "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Abort;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
