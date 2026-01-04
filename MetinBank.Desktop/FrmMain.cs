using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using MetinBank.Models;

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
            // Set user information in header
            barStaticItemKullanici.Caption = $"👤 {_kullanici.TamAd} ({_kullanici.RolAdi})";
            
            // Set current date/time
            barStaticItemTarih.Caption = $"📅 {DateTime.Now:dd.MM.yyyy HH:mm}";
            
            // Start timer for updating time
            timer1.Start();
        }

        private void LoadNavBarIcons()
        {
            // Disable code-behind icon loading to allow Designer-set images to show
            try
            {
                // Detach the global collection so individual Item.Image / Item.SmallImage properties work
                navBarControl1.SmallImages = null;
                navBarControl1.LargeImages = null;
                
                // Optional: Clear any programmatically set indices if they exist, 
                // though setting the collection to null usually suffices.
                // We leave the rest to the Designer configuration.
            }
            catch
            {
                // generic error handling
            }

            // Yetki kontrolü - Onay Bekleyenler menüsünü sadece yetkili kişilere göster
            string rol = _kullanici.RolAdi;
            if (rol.IndexOf("Mudur", StringComparison.OrdinalIgnoreCase) >= 0 || 
                rol.IndexOf("Müdür", StringComparison.OrdinalIgnoreCase) >= 0 ||
                rol.IndexOf("Genel", StringComparison.OrdinalIgnoreCase) >= 0 || 
                rol.IndexOf("Merkez", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                navBarItemOnayBekleyenler.Visible = true;
            }
            else
            {
                navBarItemOnayBekleyenler.Visible = false;
            }
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

        private void OpenMdiChild(Form childForm)
        {
            // Check if form is already open
            foreach (Form existingForm in this.MdiChildren)
            {
                if (existingForm.GetType() == childForm.GetType())
                {
                    existingForm.Activate();
                    childForm.Dispose();
                    return;
                }
            }

            // Open new form as MDI child - prevent visible animation
            this.SuspendLayout();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Opacity = 0;
            childForm.Show();
            childForm.Opacity = 1;
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
