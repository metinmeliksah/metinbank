namespace MetinBank.Forms
{
    partial class FrmAnaSayfa
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageIslemler;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageYonetim;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupMusteri;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupHesap;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupIslem;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupKart;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupOnay;
        private DevExpress.XtraBars.BarButtonItem btnMusteriIslem;
        private DevExpress.XtraBars.BarButtonItem btnHesapIslem;
        private DevExpress.XtraBars.BarButtonItem btnParaYatir;
        private DevExpress.XtraBars.BarButtonItem btnParaCek;
        private DevExpress.XtraBars.BarButtonItem btnHavaleEFT;
        private DevExpress.XtraBars.BarButtonItem btnKartlar;
        private DevExpress.XtraBars.BarButtonItem btnBasvurular;
        private DevExpress.XtraBars.BarButtonItem btnOnayBekleyenler;
        private DevExpress.XtraBars.BarStaticItem barStaticKullanici;
        private DevExpress.XtraBars.BarStaticItem barStaticRol;
        private DevExpress.XtraBars.BarStaticItem barStaticSube;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnMusteriIslem = new DevExpress.XtraBars.BarButtonItem();
            this.btnHesapIslem = new DevExpress.XtraBars.BarButtonItem();
            this.btnParaYatir = new DevExpress.XtraBars.BarButtonItem();
            this.btnParaCek = new DevExpress.XtraBars.BarButtonItem();
            this.btnHavaleEFT = new DevExpress.XtraBars.BarButtonItem();
            this.btnKartlar = new DevExpress.XtraBars.BarButtonItem();
            this.btnBasvurular = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnayBekleyenler = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticKullanici = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticRol = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticSube = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPageIslemler = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupMusteri = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupHesap = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupIslem = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupKart = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageYonetim = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupOnay = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.SuspendLayout();
            
            // ribbonControl
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
                this.ribbonControl.ExpandCollapseItem,
                this.btnMusteriIslem,
                this.btnHesapIslem,
                this.btnParaYatir,
                this.btnParaCek,
                this.btnHavaleEFT,
                this.btnKartlar,
                this.btnBasvurular,
                this.btnOnayBekleyenler,
                this.barStaticKullanici,
                this.barStaticRol,
                this.barStaticSube});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 12;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
                this.ribbonPageIslemler,
                this.ribbonPageYonetim});
            this.ribbonControl.Size = new System.Drawing.Size(1200, 158);
            
            // btnMusteriIslem
            this.btnMusteriIslem.Caption = "Müşteri İşlemleri";
            this.btnMusteriIslem.Id = 1;
            this.btnMusteriIslem.Name = "btnMusteriIslem";
            this.btnMusteriIslem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnMusteriIslem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnMusteriIslem_Click);
            
            // btnHesapIslem
            this.btnHesapIslem.Caption = "Hesap İşlemleri";
            this.btnHesapIslem.Id = 2;
            this.btnHesapIslem.Name = "btnHesapIslem";
            this.btnHesapIslem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnHesapIslem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnHesapIslem_Click);
            
            // btnParaYatir
            this.btnParaYatir.Caption = "Para Yatır";
            this.btnParaYatir.Id = 3;
            this.btnParaYatir.Name = "btnParaYatir";
            this.btnParaYatir.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnParaYatir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnParaYatir_Click);
            
            // btnParaCek
            this.btnParaCek.Caption = "Para Çek";
            this.btnParaCek.Id = 4;
            this.btnParaCek.Name = "btnParaCek";
            this.btnParaCek.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnParaCek.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnParaCek_Click);
            
            // btnHavaleEFT
            this.btnHavaleEFT.Caption = "Havale / EFT";
            this.btnHavaleEFT.Id = 5;
            this.btnHavaleEFT.Name = "btnHavaleEFT";
            this.btnHavaleEFT.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnHavaleEFT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnHavaleEFT_Click);
            
            // btnKartlar
            this.btnKartlar.Caption = "Kartlar";
            this.btnKartlar.Id = 6;
            this.btnKartlar.Name = "btnKartlar";
            this.btnKartlar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnKartlar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnKartlar_Click);
            
            // btnBasvurular
            this.btnBasvurular.Caption = "Başvurular";
            this.btnBasvurular.Id = 7;
            this.btnBasvurular.Name = "btnBasvurular";
            this.btnBasvurular.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBasvurular.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnBasvurular_Click);
            
            // btnOnayBekleyenler
            this.btnOnayBekleyenler.Caption = "Onay Bekleyenler";
            this.btnOnayBekleyenler.Id = 8;
            this.btnOnayBekleyenler.Name = "btnOnayBekleyenler";
            this.btnOnayBekleyenler.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnOnayBekleyenler.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnOnayBekleyenler_Click);
            
            // barStaticKullanici
            this.barStaticKullanici.Caption = "Kullanıcı: ";
            this.barStaticKullanici.Id = 9;
            this.barStaticKullanici.Name = "barStaticKullanici";
            
            // barStaticRol
            this.barStaticRol.Caption = "Rol: ";
            this.barStaticRol.Id = 10;
            this.barStaticRol.Name = "barStaticRol";
            
            // barStaticSube
            this.barStaticSube.Caption = "Şube: ";
            this.barStaticSube.Id = 11;
            this.barStaticSube.Name = "barStaticSube";
            
            // ribbonPageIslemler
            this.ribbonPageIslemler.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
                this.ribbonPageGroupMusteri,
                this.ribbonPageGroupHesap,
                this.ribbonPageGroupIslem,
                this.ribbonPageGroupKart});
            this.ribbonPageIslemler.Name = "ribbonPageIslemler";
            this.ribbonPageIslemler.Text = "İşlemler";
            
            // ribbonPageGroupMusteri
            this.ribbonPageGroupMusteri.ItemLinks.Add(this.btnMusteriIslem);
            this.ribbonPageGroupMusteri.Name = "ribbonPageGroupMusteri";
            this.ribbonPageGroupMusteri.Text = "Müşteri";
            
            // ribbonPageGroupHesap
            this.ribbonPageGroupHesap.ItemLinks.Add(this.btnHesapIslem);
            this.ribbonPageGroupHesap.Name = "ribbonPageGroupHesap";
            this.ribbonPageGroupHesap.Text = "Hesap";
            
            // ribbonPageGroupIslem
            this.ribbonPageGroupIslem.ItemLinks.Add(this.btnParaYatir);
            this.ribbonPageGroupIslem.ItemLinks.Add(this.btnParaCek);
            this.ribbonPageGroupIslem.ItemLinks.Add(this.btnHavaleEFT);
            this.ribbonPageGroupIslem.Name = "ribbonPageGroupIslem";
            this.ribbonPageGroupIslem.Text = "Para İşlemleri";
            
            // ribbonPageGroupKart
            this.ribbonPageGroupKart.ItemLinks.Add(this.btnKartlar);
            this.ribbonPageGroupKart.ItemLinks.Add(this.btnBasvurular);
            this.ribbonPageGroupKart.Name = "ribbonPageGroupKart";
            this.ribbonPageGroupKart.Text = "Kart İşlemleri";
            
            // ribbonPageYonetim
            this.ribbonPageYonetim.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
                this.ribbonPageGroupOnay});
            this.ribbonPageYonetim.Name = "ribbonPageYonetim";
            this.ribbonPageYonetim.Text = "Yönetim";
            
            // ribbonPageGroupOnay
            this.ribbonPageGroupOnay.ItemLinks.Add(this.btnOnayBekleyenler);
            this.ribbonPageGroupOnay.Name = "ribbonPageGroupOnay";
            this.ribbonPageGroupOnay.Text = "Onay İşlemleri";
            
            // FrmAnaSayfa
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.ribbonControl);
            this.IsMdiContainer = true;
            this.Name = "FrmAnaSayfa";
            this.Ribbon = this.ribbonControl;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metin Bank - Ana Sayfa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmAnaSayfa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
