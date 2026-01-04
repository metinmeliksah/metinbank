namespace MetinBank.Desktop
{
    partial class FrmHesapIslem
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        
        // Müşteri Arama
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        
        // Mevcut Hesaplar
        private DevExpress.XtraGrid.GridControl gridHesaplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHesaplar;
        
        // Hesap Açma
        private DevExpress.XtraEditors.ComboBoxEdit cmbHesapTipi;
        private DevExpress.XtraEditors.ComboBoxEdit cmbHesapCinsi;
        private DevExpress.XtraEditors.SpinEdit numFaizOrani;
        private DevExpress.XtraEditors.TextEdit txtIBAN;
        private DevExpress.XtraEditors.SimpleButton btnHesapAc;
        
        // Seçili Müşteri Bilgisi
        private DevExpress.XtraEditors.LabelControl lblSeciliMusteri;
        
        // Layout Items
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriArama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGridMusteriler;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGridHesaplar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHesapTipi;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHesapCinsi;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemFaizOrani;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemIBAN;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHesapAc;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemSeciliMusteri;
        private DevExpress.XtraLayout.LayoutControlGroup grpMusteriSec;
        private DevExpress.XtraLayout.LayoutControlGroup grpMevcutHesaplar;
        private DevExpress.XtraLayout.LayoutControlGroup grpYeniHesap;

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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtMusteriArama = new DevExpress.XtraEditors.TextEdit();
            this.gridMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridHesaplar = new DevExpress.XtraGrid.GridControl();
            this.gridViewHesaplar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblSeciliMusteri = new DevExpress.XtraEditors.LabelControl();
            this.cmbHesapTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbHesapCinsi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.numFaizOrani = new DevExpress.XtraEditors.SpinEdit();
            this.txtIBAN = new DevExpress.XtraEditors.TextEdit();
            this.btnHesapAc = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpMusteriSec = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemMusteriArama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGridMusteriler = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpMevcutHesaplar = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemGridHesaplar = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpYeniHesap = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemSeciliMusteri = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHesapTipi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHesapCinsi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemFaizOrani = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemIBAN = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHesapAc = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapCinsi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaizOrani.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIBAN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMusteriSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMevcutHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGridHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpYeniHesap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSeciliMusteri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesapTipi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesapCinsi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemFaizOrani)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIBAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesapAc)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtMusteriArama);
            this.layoutControl1.Controls.Add(this.gridMusteriler);
            this.layoutControl1.Controls.Add(this.gridHesaplar);
            this.layoutControl1.Controls.Add(this.lblSeciliMusteri);
            this.layoutControl1.Controls.Add(this.cmbHesapTipi);
            this.layoutControl1.Controls.Add(this.cmbHesapCinsi);
            this.layoutControl1.Controls.Add(this.numFaizOrani);
            this.layoutControl1.Controls.Add(this.txtIBAN);
            this.layoutControl1.Controls.Add(this.btnHesapAc);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControl1.TabIndex = 0;
            // 
            // txtMusteriArama
            // 
            this.txtMusteriArama.Location = new System.Drawing.Point(122, 45);
            this.txtMusteriArama.Name = "txtMusteriArama";
            this.txtMusteriArama.Properties.NullValuePrompt = "Müşteri No, TCKN veya Ad Soyad ile ara...";
            this.txtMusteriArama.Size = new System.Drawing.Size(1054, 20);
            this.txtMusteriArama.StyleController = this.layoutControl1;
            this.txtMusteriArama.TabIndex = 0;
            this.txtMusteriArama.TextChanged += new System.EventHandler(this.TxtMusteriArama_TextChanged);
            // 
            // gridMusteriler
            // 
            this.gridMusteriler.Location = new System.Drawing.Point(24, 69);
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.Name = "gridMusteriler";
            this.gridMusteriler.Size = new System.Drawing.Size(1152, 195);
            this.gridMusteriler.TabIndex = 1;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMusteriler});
            // 
            // gridViewMusteriler
            // 
            this.gridViewMusteriler.GridControl = this.gridMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewMusteriler_RowClick);
            // 
            // gridHesaplar
            // 
            this.gridHesaplar.Location = new System.Drawing.Point(24, 313);
            this.gridHesaplar.MainView = this.gridViewHesaplar;
            this.gridHesaplar.Name = "gridHesaplar";
            this.gridHesaplar.Size = new System.Drawing.Size(1152, 219);
            this.gridHesaplar.TabIndex = 2;
            this.gridHesaplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewHesaplar});
            // 
            // gridViewHesaplar
            // 
            this.gridViewHesaplar.GridControl = this.gridHesaplar;
            this.gridViewHesaplar.Name = "gridViewHesaplar";
            this.gridViewHesaplar.OptionsBehavior.Editable = false;
            this.gridViewHesaplar.OptionsView.ShowGroupPanel = false;
            // 
            // lblSeciliMusteri
            // 
            this.lblSeciliMusteri.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSeciliMusteri.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblSeciliMusteri.Appearance.Options.UseFont = true;
            this.lblSeciliMusteri.Appearance.Options.UseForeColor = true;
            this.lblSeciliMusteri.Location = new System.Drawing.Point(122, 581);
            this.lblSeciliMusteri.Name = "lblSeciliMusteri";
            this.lblSeciliMusteri.Size = new System.Drawing.Size(109, 17);
            this.lblSeciliMusteri.StyleController = this.layoutControl1;
            this.lblSeciliMusteri.TabIndex = 3;
            this.lblSeciliMusteri.Text = "Müşteri seçilmedi";
            // 
            // cmbHesapTipi
            // 
            this.cmbHesapTipi.Location = new System.Drawing.Point(122, 602);
            this.cmbHesapTipi.Name = "cmbHesapTipi";
            this.cmbHesapTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHesapTipi.Properties.Items.AddRange(new object[] {
            "TL",
            "USD",
            "EUR",
            "GBP"});
            this.cmbHesapTipi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbHesapTipi.Size = new System.Drawing.Size(188, 20);
            this.cmbHesapTipi.StyleController = this.layoutControl1;
            this.cmbHesapTipi.TabIndex = 4;
            // 
            // cmbHesapCinsi
            // 
            this.cmbHesapCinsi.Location = new System.Drawing.Point(412, 602);
            this.cmbHesapCinsi.Name = "cmbHesapCinsi";
            this.cmbHesapCinsi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHesapCinsi.Properties.Items.AddRange(new object[] {
            "Vadesiz",
            "Vadeli",
            "Maaş",
            "Yatırım"});
            this.cmbHesapCinsi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbHesapCinsi.Size = new System.Drawing.Size(188, 20);
            this.cmbHesapCinsi.StyleController = this.layoutControl1;
            this.cmbHesapCinsi.TabIndex = 5;
            // 
            // numFaizOrani
            // 
            this.numFaizOrani.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numFaizOrani.Location = new System.Drawing.Point(702, 602);
            this.numFaizOrani.Name = "numFaizOrani";
            this.numFaizOrani.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numFaizOrani.Properties.DisplayFormat.FormatString = "#0.00\'%\'";
            this.numFaizOrani.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numFaizOrani.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numFaizOrani.Size = new System.Drawing.Size(474, 20);
            this.numFaizOrani.StyleController = this.layoutControl1;
            this.numFaizOrani.TabIndex = 6;
            // 
            // txtIBAN
            // 
            this.txtIBAN.Location = new System.Drawing.Point(122, 626);
            this.txtIBAN.Name = "txtIBAN";
            this.txtIBAN.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.txtIBAN.Properties.Appearance.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtIBAN.Properties.Appearance.Options.UseBackColor = true;
            this.txtIBAN.Properties.Appearance.Options.UseFont = true;
            this.txtIBAN.Properties.ReadOnly = true;
            this.txtIBAN.Size = new System.Drawing.Size(1054, 22);
            this.txtIBAN.StyleController = this.layoutControl1;
            this.txtIBAN.TabIndex = 7;
            // 
            // btnHesapAc
            // 
            this.btnHesapAc.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnHesapAc.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHesapAc.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnHesapAc.Appearance.Options.UseBackColor = true;
            this.btnHesapAc.Appearance.Options.UseFont = true;
            this.btnHesapAc.Appearance.Options.UseForeColor = true;
            this.btnHesapAc.Location = new System.Drawing.Point(12, 664);
            this.btnHesapAc.Name = "btnHesapAc";
            this.btnHesapAc.Size = new System.Drawing.Size(1176, 24);
            this.btnHesapAc.StyleController = this.layoutControl1;
            this.btnHesapAc.TabIndex = 8;
            this.btnHesapAc.Text = "🏦 HESAP AÇ";
            this.btnHesapAc.Click += new System.EventHandler(this.BtnHesapAc_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.grpMusteriSec,
            this.grpMevcutHesaplar,
            this.grpYeniHesap,
            this.layoutItemHesapAc});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // grpMusteriSec
            // 
            this.grpMusteriSec.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpMusteriSec.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpMusteriSec.AppearanceGroup.Options.UseFont = true;
            this.grpMusteriSec.AppearanceGroup.Options.UseForeColor = true;
            this.grpMusteriSec.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemMusteriArama,
            this.layoutItemGridMusteriler});
            this.grpMusteriSec.Location = new System.Drawing.Point(0, 0);
            this.grpMusteriSec.Name = "grpMusteriSec";
            this.grpMusteriSec.Size = new System.Drawing.Size(1180, 268);
            this.grpMusteriSec.Text = "👤 Müşteri Seç";
            // 
            // layoutItemMusteriArama
            // 
            this.layoutItemMusteriArama.Control = this.txtMusteriArama;
            this.layoutItemMusteriArama.Location = new System.Drawing.Point(0, 0);
            this.layoutItemMusteriArama.Name = "layoutItemMusteriArama";
            this.layoutItemMusteriArama.Size = new System.Drawing.Size(1156, 24);
            this.layoutItemMusteriArama.Text = "Ara:";
            this.layoutItemMusteriArama.TextSize = new System.Drawing.Size(86, 13);
            // 
            // layoutItemGridMusteriler
            // 
            this.layoutItemGridMusteriler.Control = this.gridMusteriler;
            this.layoutItemGridMusteriler.Location = new System.Drawing.Point(0, 24);
            this.layoutItemGridMusteriler.Name = "layoutItemGridMusteriler";
            this.layoutItemGridMusteriler.Size = new System.Drawing.Size(1156, 199);
            this.layoutItemGridMusteriler.TextVisible = false;
            // 
            // grpMevcutHesaplar
            // 
            this.grpMevcutHesaplar.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpMevcutHesaplar.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpMevcutHesaplar.AppearanceGroup.Options.UseFont = true;
            this.grpMevcutHesaplar.AppearanceGroup.Options.UseForeColor = true;
            this.grpMevcutHesaplar.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemGridHesaplar});
            this.grpMevcutHesaplar.Location = new System.Drawing.Point(0, 268);
            this.grpMevcutHesaplar.Name = "grpMevcutHesaplar";
            this.grpMevcutHesaplar.Size = new System.Drawing.Size(1180, 268);
            this.grpMevcutHesaplar.Text = "📋 Mevcut Hesaplar";
            // 
            // layoutItemGridHesaplar
            // 
            this.layoutItemGridHesaplar.Control = this.gridHesaplar;
            this.layoutItemGridHesaplar.Location = new System.Drawing.Point(0, 0);
            this.layoutItemGridHesaplar.Name = "layoutItemGridHesaplar";
            this.layoutItemGridHesaplar.Size = new System.Drawing.Size(1156, 223);
            this.layoutItemGridHesaplar.TextVisible = false;
            // 
            // grpYeniHesap
            // 
            this.grpYeniHesap.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpYeniHesap.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpYeniHesap.AppearanceGroup.Options.UseFont = true;
            this.grpYeniHesap.AppearanceGroup.Options.UseForeColor = true;
            this.grpYeniHesap.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemSeciliMusteri,
            this.layoutItemHesapTipi,
            this.layoutItemHesapCinsi,
            this.layoutItemFaizOrani,
            this.layoutItemIBAN});
            this.grpYeniHesap.Location = new System.Drawing.Point(0, 536);
            this.grpYeniHesap.Name = "grpYeniHesap";
            this.grpYeniHesap.Size = new System.Drawing.Size(1180, 116);
            this.grpYeniHesap.Text = "➕ Yeni Hesap Aç";
            // 
            // layoutItemSeciliMusteri
            // 
            this.layoutItemSeciliMusteri.Control = this.lblSeciliMusteri;
            this.layoutItemSeciliMusteri.Location = new System.Drawing.Point(0, 0);
            this.layoutItemSeciliMusteri.Name = "layoutItemSeciliMusteri";
            this.layoutItemSeciliMusteri.Size = new System.Drawing.Size(1156, 21);
            this.layoutItemSeciliMusteri.Text = "Seçili Müşteri:";
            this.layoutItemSeciliMusteri.TextSize = new System.Drawing.Size(86, 13);
            // 
            // layoutItemHesapTipi
            // 
            this.layoutItemHesapTipi.Control = this.cmbHesapTipi;
            this.layoutItemHesapTipi.Location = new System.Drawing.Point(0, 21);
            this.layoutItemHesapTipi.Name = "layoutItemHesapTipi";
            this.layoutItemHesapTipi.Size = new System.Drawing.Size(290, 24);
            this.layoutItemHesapTipi.Text = "Para Birimi:";
            this.layoutItemHesapTipi.TextSize = new System.Drawing.Size(86, 13);
            // 
            // layoutItemHesapCinsi
            // 
            this.layoutItemHesapCinsi.Control = this.cmbHesapCinsi;
            this.layoutItemHesapCinsi.Location = new System.Drawing.Point(290, 21);
            this.layoutItemHesapCinsi.Name = "layoutItemHesapCinsi";
            this.layoutItemHesapCinsi.Size = new System.Drawing.Size(290, 24);
            this.layoutItemHesapCinsi.Text = "Hesap Cinsi:";
            this.layoutItemHesapCinsi.TextSize = new System.Drawing.Size(86, 13);
            // 
            // layoutItemFaizOrani
            // 
            this.layoutItemFaizOrani.Control = this.numFaizOrani;
            this.layoutItemFaizOrani.Location = new System.Drawing.Point(580, 21);
            this.layoutItemFaizOrani.Name = "layoutItemFaizOrani";
            this.layoutItemFaizOrani.Size = new System.Drawing.Size(576, 24);
            this.layoutItemFaizOrani.Text = "Faiz Oranı:";
            this.layoutItemFaizOrani.TextSize = new System.Drawing.Size(86, 13);
            // 
            // layoutItemIBAN
            // 
            this.layoutItemIBAN.Control = this.txtIBAN;
            this.layoutItemIBAN.Location = new System.Drawing.Point(0, 45);
            this.layoutItemIBAN.Name = "layoutItemIBAN";
            this.layoutItemIBAN.Size = new System.Drawing.Size(1156, 26);
            this.layoutItemIBAN.Text = "Oluşturulan IBAN:";
            this.layoutItemIBAN.TextSize = new System.Drawing.Size(86, 13);
            // 
            // layoutItemHesapAc
            // 
            this.layoutItemHesapAc.Control = this.btnHesapAc;
            this.layoutItemHesapAc.Location = new System.Drawing.Point(0, 652);
            this.layoutItemHesapAc.Name = "layoutItemHesapAc";
            this.layoutItemHesapAc.Size = new System.Drawing.Size(1180, 28);
            this.layoutItemHesapAc.TextVisible = false;
            // 
            // FrmHesapIslem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmHesapIslem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hesap İşlemleri";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmHesapIslem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapCinsi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaizOrani.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIBAN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMusteriSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMevcutHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGridHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpYeniHesap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSeciliMusteri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesapTipi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesapCinsi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemFaizOrani)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIBAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesapAc)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
