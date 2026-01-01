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
            // Create icons programmatically using DevExpress icon builder
            try
            {
                // İkon ayarları - koyu mavi tema
                var iconColor = Color.White;
                
                // ImageCollection'a programatik ikonlar eklenecek
                // Bu kısım projenin Resources'ında icon dosyaları olduğunda aktif edilebilir
            }
            catch
            {
                // İkon yükleme hatası sessizce geçilir
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

        private void navBarItemHavaleEFT_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenMdiChild(new FrmHavaleEFT(_kullanici));
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

            // Open new form as MDI child
            childForm.MdiParent = this;
            childForm.Show();
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
