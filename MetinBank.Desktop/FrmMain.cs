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
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            InitializeComponent();
            _kullanici = kullanici;
            this.IsMdiContainer = true;
            ConfigureUI();
            LoadNavBarIcons();
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
            // DevExpress built-in SVG images for banking icons
            try
            {
                // Use DevExpress Office icons directly on navbar items
                svgImageCollection1.Add("customers", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.People.user.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("adduser", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.People.adduser.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("userlist", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.People.team.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("account", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Business Objects.bobusinessobject.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("accountopen", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Actions.add.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("deposit", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.XAF.action_loadsummariessettings.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("withdraw", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.XAF.action_savesummariessettings.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("transfer", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.XAF.action_object_refresh.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("havale", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Actions.apply.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("eft", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.XAF.action_export.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("virman", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Arrows.repeat.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("card", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Scheduling.today.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("apps", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Actions.new.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("pending", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Scheduling.wait.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));
                svgImageCollection1.Add("other", DevExpress.Utils.Svg.SvgImage.FromResources("DevExpress.Utils.Svg.Images.Setup.options.svg", typeof(DevExpress.Utils.Svg.SvgImage).Assembly));

                // Set icon indices for navbar groups and items
                navBarGroupMusteriIslemleri.SmallImageIndex = 0;  // customers
                navBarItemMusteriEkle.SmallImageIndex = 1;        // adduser
                navBarItemMusteriIslem.SmallImageIndex = 2;       // userlist
                navBarItemIslemGecmisi.SmallImageIndex = 5;       // history
                
                navBarGroupHesapIslemleri.SmallImageIndex = 3;    // account
                navBarItemHesapIslem.SmallImageIndex = 4;         // accountopen
                navBarItemParaYatir.SmallImageIndex = 5;          // deposit
                navBarItemParaCek.SmallImageIndex = 6;            // withdraw
                
                navBarGroupTransferIslemleri.SmallImageIndex = 7; // transfer
                navBarItemHavale.SmallImageIndex = 8;             // havale
                navBarItemEFT.SmallImageIndex = 9;                // eft
                navBarItemVirman.SmallImageIndex = 10;            // virman
                
                navBarGroupDigerIslemler.SmallImageIndex = 14;    // other
                navBarItemKartlar.SmallImageIndex = 11;           // card
                navBarItemBasvurular.SmallImageIndex = 12;        // apps
                navBarItemOnayBekleyenler.SmallImageIndex = 13;   // pending
            }
            catch
            {
                // Icon loading failed silently
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
            if (XtraMessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?", 
                "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?", 
                "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
