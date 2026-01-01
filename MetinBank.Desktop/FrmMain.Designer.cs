namespace MetinBank.Desktop
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barStaticItemLogo = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemTarih = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemKullanici = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemCikis = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroupMusteriIslemleri = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemMusteriEkle = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemMusteriIslem = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupHesapIslemleri = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemHesapIslem = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemParaYatir = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemParaCek = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupTransferIslemleri = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemHavaleEFT = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemVirman = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupDigerIslemler = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemKartlar = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemBasvurular = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemOnayBekleyenler = new DevExpress.XtraNavBar.NavBarItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItemLogo,
            this.barStaticItemTarih,
            this.barStaticItemKullanici,
            this.barButtonItemCikis});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "MainBar";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemLogo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemTarih),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemKullanici),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCikis)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "MainBar";
            // 
            // barStaticItemLogo
            // 
            this.barStaticItemLogo.Caption = "MetinBank";
            this.barStaticItemLogo.Id = 0;
            this.barStaticItemLogo.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.barStaticItemLogo.ItemAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.barStaticItemLogo.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItemLogo.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barStaticItemLogo.Name = "barStaticItemLogo";
            this.barStaticItemLogo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barStaticItemTarih
            // 
            this.barStaticItemTarih.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barStaticItemTarih.Caption = "01.01.2026 00:00";
            this.barStaticItemTarih.Id = 1;
            this.barStaticItemTarih.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.barStaticItemTarih.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItemTarih.Name = "barStaticItemTarih";
            // 
            // barStaticItemKullanici
            // 
            this.barStaticItemKullanici.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barStaticItemKullanici.Caption = "Kullanıcı";
            this.barStaticItemKullanici.Id = 2;
            this.barStaticItemKullanici.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.barStaticItemKullanici.ItemAppearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.barStaticItemKullanici.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItemKullanici.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barStaticItemKullanici.Name = "barStaticItemKullanici";
            // 
            // barButtonItemCikis
            // 
            this.barButtonItemCikis.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemCikis.Caption = "Çıkış";
            this.barButtonItemCikis.Id = 3;
            this.barButtonItemCikis.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.barButtonItemCikis.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItemCikis.Name = "barButtonItemCikis";
            this.barButtonItemCikis.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCikis_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1400, 36);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 900);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1400, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 36);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 864);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1400, 36);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 864);
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroupMusteriIslemleri;
            this.navBarControl1.Appearance.Background.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.navBarControl1.Appearance.Background.Options.UseBackColor = true;
            this.navBarControl1.Appearance.GroupHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.navBarControl1.Appearance.GroupHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.navBarControl1.Appearance.GroupHeader.ForeColor = System.Drawing.Color.White;
            this.navBarControl1.Appearance.GroupHeader.Options.UseBackColor = true;
            this.navBarControl1.Appearance.GroupHeader.Options.UseFont = true;
            this.navBarControl1.Appearance.GroupHeader.Options.UseForeColor = true;
            this.navBarControl1.Appearance.Item.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.navBarControl1.Appearance.Item.ForeColor = System.Drawing.Color.White;
            this.navBarControl1.Appearance.Item.Options.UseFont = true;
            this.navBarControl1.Appearance.Item.Options.UseForeColor = true;
            this.navBarControl1.Appearance.ItemActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.navBarControl1.Appearance.ItemActive.Options.UseBackColor = true;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroupMusteriIslemleri,
            this.navBarGroupHesapIslemleri,
            this.navBarGroupTransferIslemleri,
            this.navBarGroupDigerIslemler});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItemMusteriEkle,
            this.navBarItemMusteriIslem,
            this.navBarItemHesapIslem,
            this.navBarItemParaYatir,
            this.navBarItemParaCek,
            this.navBarItemHavaleEFT,
            this.navBarItemVirman,
            this.navBarItemKartlar,
            this.navBarItemBasvurular,
            this.navBarItemOnayBekleyenler});
            this.navBarControl1.Location = new System.Drawing.Point(0, 36);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 220;
            this.navBarControl1.Size = new System.Drawing.Size(220, 864);
            this.navBarControl1.SmallImages = this.imageCollection1;
            this.navBarControl1.TabIndex = 4;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroupMusteriIslemleri
            // 
            this.navBarGroupMusteriIslemleri.Caption = "  Müşteri İşlemleri";
            this.navBarGroupMusteriIslemleri.Expanded = true;
            this.navBarGroupMusteriIslemleri.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.navBarGroupMusteriIslemleri.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemMusteriEkle),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemMusteriIslem)});
            this.navBarGroupMusteriIslemleri.Name = "navBarGroupMusteriIslemleri";
            this.navBarGroupMusteriIslemleri.SmallImageIndex = 0;
            // 
            // navBarItemMusteriEkle
            // 
            this.navBarItemMusteriEkle.Caption = "  Müşteri Ekle";
            this.navBarItemMusteriEkle.Name = "navBarItemMusteriEkle";
            this.navBarItemMusteriEkle.SmallImageIndex = 1;
            this.navBarItemMusteriEkle.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemMusteriEkle_LinkClicked);
            // 
            // navBarItemMusteriIslem
            // 
            this.navBarItemMusteriIslem.Caption = "  Müşteri Listesi";
            this.navBarItemMusteriIslem.Name = "navBarItemMusteriIslem";
            this.navBarItemMusteriIslem.SmallImageIndex = 2;
            this.navBarItemMusteriIslem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemMusteriIslem_LinkClicked);
            // 
            // navBarGroupHesapIslemleri
            // 
            this.navBarGroupHesapIslemleri.Caption = "  Hesap İşlemleri";
            this.navBarGroupHesapIslemleri.Expanded = true;
            this.navBarGroupHesapIslemleri.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.navBarGroupHesapIslemleri.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemHesapIslem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemParaYatir),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemParaCek)});
            this.navBarGroupHesapIslemleri.Name = "navBarGroupHesapIslemleri";
            this.navBarGroupHesapIslemleri.SmallImageIndex = 3;
            // 
            // navBarItemHesapIslem
            // 
            this.navBarItemHesapIslem.Caption = "  Hesap Aç";
            this.navBarItemHesapIslem.Name = "navBarItemHesapIslem";
            this.navBarItemHesapIslem.SmallImageIndex = 4;
            this.navBarItemHesapIslem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemHesapIslem_LinkClicked);
            // 
            // navBarItemParaYatir
            // 
            this.navBarItemParaYatir.Caption = "  Para Yatır";
            this.navBarItemParaYatir.Name = "navBarItemParaYatir";
            this.navBarItemParaYatir.SmallImageIndex = 5;
            this.navBarItemParaYatir.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemParaYatir_LinkClicked);
            // 
            // navBarItemParaCek
            // 
            this.navBarItemParaCek.Caption = "  Para Çek";
            this.navBarItemParaCek.Name = "navBarItemParaCek";
            this.navBarItemParaCek.SmallImageIndex = 6;
            this.navBarItemParaCek.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemParaCek_LinkClicked);
            // 
            // navBarGroupTransferIslemleri
            // 
            this.navBarGroupTransferIslemleri.Caption = "  Transfer İşlemleri";
            this.navBarGroupTransferIslemleri.Expanded = true;
            this.navBarGroupTransferIslemleri.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.navBarGroupTransferIslemleri.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemHavaleEFT),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemVirman)});
            this.navBarGroupTransferIslemleri.Name = "navBarGroupTransferIslemleri";
            this.navBarGroupTransferIslemleri.SmallImageIndex = 7;
            // 
            // navBarItemHavaleEFT
            // 
            this.navBarItemHavaleEFT.Caption = "  Havale / EFT";
            this.navBarItemHavaleEFT.Name = "navBarItemHavaleEFT";
            this.navBarItemHavaleEFT.SmallImageIndex = 8;
            this.navBarItemHavaleEFT.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemHavaleEFT_LinkClicked);
            // 
            // navBarItemVirman
            // 
            this.navBarItemVirman.Caption = "  Virman";
            this.navBarItemVirman.Name = "navBarItemVirman";
            this.navBarItemVirman.SmallImageIndex = 9;
            this.navBarItemVirman.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemVirman_LinkClicked);
            // 
            // navBarGroupDigerIslemler
            // 
            this.navBarGroupDigerIslemler.Caption = "  Diğer İşlemler";
            this.navBarGroupDigerIslemler.Expanded = true;
            this.navBarGroupDigerIslemler.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.navBarGroupDigerIslemler.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemKartlar),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemBasvurular),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemOnayBekleyenler)});
            this.navBarGroupDigerIslemler.Name = "navBarGroupDigerIslemler";
            this.navBarGroupDigerIslemler.SmallImageIndex = 10;
            // 
            // navBarItemKartlar
            // 
            this.navBarItemKartlar.Caption = "  Kartlar";
            this.navBarItemKartlar.Name = "navBarItemKartlar";
            this.navBarItemKartlar.SmallImageIndex = 11;
            this.navBarItemKartlar.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemKartlar_LinkClicked);
            // 
            // navBarItemBasvurular
            // 
            this.navBarItemBasvurular.Caption = "  Başvurular";
            this.navBarItemBasvurular.Name = "navBarItemBasvurular";
            this.navBarItemBasvurular.SmallImageIndex = 12;
            this.navBarItemBasvurular.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemBasvurular_LinkClicked);
            // 
            // navBarItemOnayBekleyenler
            // 
            this.navBarItemOnayBekleyenler.Caption = "  Onay Bekleyenler";
            this.navBarItemOnayBekleyenler.Name = "navBarItemOnayBekleyenler";
            this.navBarItemOnayBekleyenler.SmallImageIndex = 13;
            this.navBarItemOnayBekleyenler.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemOnayBekleyenler_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MetinBank - Banka Yönetim Sistemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem barStaticItemLogo;
        private DevExpress.XtraBars.BarStaticItem barStaticItemTarih;
        private DevExpress.XtraBars.BarStaticItem barStaticItemKullanici;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCikis;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupMusteriIslemleri;
        private DevExpress.XtraNavBar.NavBarItem navBarItemMusteriEkle;
        private DevExpress.XtraNavBar.NavBarItem navBarItemMusteriIslem;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupHesapIslemleri;
        private DevExpress.XtraNavBar.NavBarItem navBarItemHesapIslem;
        private DevExpress.XtraNavBar.NavBarItem navBarItemParaYatir;
        private DevExpress.XtraNavBar.NavBarItem navBarItemParaCek;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupTransferIslemleri;
        private DevExpress.XtraNavBar.NavBarItem navBarItemHavaleEFT;
        private DevExpress.XtraNavBar.NavBarItem navBarItemVirman;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupDigerIslemler;
        private DevExpress.XtraNavBar.NavBarItem navBarItemKartlar;
        private DevExpress.XtraNavBar.NavBarItem navBarItemBasvurular;
        private DevExpress.XtraNavBar.NavBarItem navBarItemOnayBekleyenler;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
