namespace MetinBank.Desktop
{
    partial class FrmIslemGecmisi
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        
        // Müşteri Arama
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        
        // Hesap Seçimi
        private DevExpress.XtraEditors.ComboBoxEdit cmbHesapFiltre;
        private DevExpress.XtraEditors.LabelControl lblSeciliMusteri;
        
        // İşlem Listesi
        private DevExpress.XtraGrid.GridControl gridIslemler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewIslemler;
        
        // Butonlar
        private DevExpress.XtraEditors.SimpleButton btnFiltrele;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        
        // Layout Items
        private DevExpress.XtraLayout.LayoutControlGroup grpMusteriArama;
        private DevExpress.XtraLayout.LayoutControlGroup grpIslemler;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriArama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriler;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHesapFiltre;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemSeciliMusteri;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemIslemler;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemFiltrele;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemKapat;

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
            this.cmbHesapFiltre = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblSeciliMusteri = new DevExpress.XtraEditors.LabelControl();
            this.gridIslemler = new DevExpress.XtraGrid.GridControl();
            this.gridViewIslemler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnFiltrele = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpMusteriArama = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpIslemler = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemMusteriArama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemMusteriler = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHesapFiltre = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemSeciliMusteri = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemIslemler = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemFiltrele = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemKapat = new DevExpress.XtraLayout.LayoutControlItem();
            
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapFiltre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridIslemler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewIslemler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMusteriArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesapFiltre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSeciliMusteri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIslemler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemFiltrele)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKapat)).BeginInit();
            this.SuspendLayout();
            
            // layoutControl1
            this.layoutControl1.Controls.Add(this.txtMusteriArama);
            this.layoutControl1.Controls.Add(this.gridMusteriler);
            this.layoutControl1.Controls.Add(this.cmbHesapFiltre);
            this.layoutControl1.Controls.Add(this.lblSeciliMusteri);
            this.layoutControl1.Controls.Add(this.gridIslemler);
            this.layoutControl1.Controls.Add(this.btnFiltrele);
            this.layoutControl1.Controls.Add(this.btnKapat);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1400, 750);
            this.layoutControl1.TabIndex = 0;
            
            // txtMusteriArama
            this.txtMusteriArama.Location = new System.Drawing.Point(100, 45);
            this.txtMusteriArama.Name = "txtMusteriArama";
            this.txtMusteriArama.Properties.NullValuePrompt = "Müşteri No, TCKN veya Ad Soyad ile ara...";
            this.txtMusteriArama.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMusteriArama.Size = new System.Drawing.Size(400, 20);
            this.txtMusteriArama.StyleController = this.layoutControl1;
            this.txtMusteriArama.TabIndex = 0;
            this.txtMusteriArama.TextChanged += new System.EventHandler(this.TxtMusteriArama_TextChanged);
            
            // gridMusteriler
            this.gridMusteriler.Location = new System.Drawing.Point(24, 69);
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.Name = "gridMusteriler";
            this.gridMusteriler.Size = new System.Drawing.Size(600, 150);
            this.gridMusteriler.TabIndex = 1;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewMusteriler });
            
            // gridViewMusteriler
            this.gridViewMusteriler.GridControl = this.gridMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewMusteriler_RowClick);
            
            // cmbHesapFiltre
            this.cmbHesapFiltre.Location = new System.Drawing.Point(750, 45);
            this.cmbHesapFiltre.Name = "cmbHesapFiltre";
            this.cmbHesapFiltre.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbHesapFiltre.Size = new System.Drawing.Size(400, 20);
            this.cmbHesapFiltre.StyleController = this.layoutControl1;
            this.cmbHesapFiltre.TabIndex = 2;
            this.cmbHesapFiltre.SelectedIndexChanged += new System.EventHandler(this.CmbHesapFiltre_SelectedIndexChanged);
            
            // lblSeciliMusteri
            this.lblSeciliMusteri.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSeciliMusteri.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblSeciliMusteri.Location = new System.Drawing.Point(24, 223);
            this.lblSeciliMusteri.Name = "lblSeciliMusteri";
            this.lblSeciliMusteri.Size = new System.Drawing.Size(1352, 25);
            this.lblSeciliMusteri.StyleController = this.layoutControl1;
            this.lblSeciliMusteri.TabIndex = 3;
            this.lblSeciliMusteri.Text = "📋 Lütfen bir müşteri seçiniz...";
            
            // gridIslemler
            this.gridIslemler.Location = new System.Drawing.Point(24, 270);
            this.gridIslemler.MainView = this.gridViewIslemler;
            this.gridIslemler.Name = "gridIslemler";
            this.gridIslemler.Size = new System.Drawing.Size(1352, 400);
            this.gridIslemler.TabIndex = 4;
            this.gridIslemler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewIslemler });
            
            // gridViewIslemler
            this.gridViewIslemler.GridControl = this.gridIslemler;
            this.gridViewIslemler.Name = "gridViewIslemler";
            this.gridViewIslemler.OptionsView.ShowGroupPanel = false;
            this.gridViewIslemler.OptionsView.ShowFooter = true;
            
            // btnFiltrele
            this.btnFiltrele.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnFiltrele.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFiltrele.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnFiltrele.Appearance.Options.UseBackColor = true;
            this.btnFiltrele.Appearance.Options.UseFont = true;
            this.btnFiltrele.Appearance.Options.UseForeColor = true;
            this.btnFiltrele.Location = new System.Drawing.Point(24, 690);
            this.btnFiltrele.Name = "btnFiltrele";
            this.btnFiltrele.Size = new System.Drawing.Size(660, 40);
            this.btnFiltrele.StyleController = this.layoutControl1;
            this.btnFiltrele.TabIndex = 5;
            this.btnFiltrele.Text = "🔍 Filtrele";
            this.btnFiltrele.Click += new System.EventHandler(this.BtnFiltrele_Click);
            
            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(688, 690);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(660, 40);
            this.btnKapat.StyleController = this.layoutControl1;
            this.btnKapat.TabIndex = 6;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            // layoutControlGroup1
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.grpMusteriArama,
                this.grpIslemler,
                this.layoutItemFiltrele,
                this.layoutItemKapat});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1400, 750);
            this.layoutControlGroup1.TextVisible = false;
            
            // grpMusteriArama
            this.grpMusteriArama.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpMusteriArama.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.grpMusteriArama.AppearanceGroup.Options.UseFont = true;
            this.grpMusteriArama.AppearanceGroup.Options.UseForeColor = true;
            this.grpMusteriArama.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemMusteriArama,
                this.layoutItemMusteriler,
                this.layoutItemHesapFiltre,
                this.layoutItemSeciliMusteri});
            this.grpMusteriArama.Location = new System.Drawing.Point(0, 0);
            this.grpMusteriArama.Name = "grpMusteriArama";
            this.grpMusteriArama.Size = new System.Drawing.Size(1380, 240);
            this.grpMusteriArama.Text = "👤 MÜŞTERİ VE HESAP SEÇİMİ";
            
            // grpIslemler
            this.grpIslemler.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpIslemler.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.grpIslemler.AppearanceGroup.Options.UseFont = true;
            this.grpIslemler.AppearanceGroup.Options.UseForeColor = true;
            this.grpIslemler.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemIslemler});
            this.grpIslemler.Location = new System.Drawing.Point(0, 240);
            this.grpIslemler.Name = "grpIslemler";
            this.grpIslemler.Size = new System.Drawing.Size(1380, 440);
            this.grpIslemler.Text = "📊 İŞLEM GEÇMİŞİ";
            
            // Layout Items
            this.layoutItemMusteriArama.Control = this.txtMusteriArama;
            this.layoutItemMusteriArama.Location = new System.Drawing.Point(0, 0);
            this.layoutItemMusteriArama.Name = "layoutItemMusteriArama";
            this.layoutItemMusteriArama.Size = new System.Drawing.Size(600, 24);
            this.layoutItemMusteriArama.Text = "Ara:";
            this.layoutItemMusteriArama.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemMusteriler.Control = this.gridMusteriler;
            this.layoutItemMusteriler.Location = new System.Drawing.Point(0, 24);
            this.layoutItemMusteriler.Name = "layoutItemMusteriler";
            this.layoutItemMusteriler.Size = new System.Drawing.Size(600, 154);
            this.layoutItemMusteriler.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemMusteriler.TextVisible = false;
            
            this.layoutItemHesapFiltre.Control = this.cmbHesapFiltre;
            this.layoutItemHesapFiltre.Location = new System.Drawing.Point(600, 0);
            this.layoutItemHesapFiltre.Name = "layoutItemHesapFiltre";
            this.layoutItemHesapFiltre.Size = new System.Drawing.Size(756, 24);
            this.layoutItemHesapFiltre.Text = "Hesap Filtresi:";
            this.layoutItemHesapFiltre.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemSeciliMusteri.Control = this.lblSeciliMusteri;
            this.layoutItemSeciliMusteri.Location = new System.Drawing.Point(0, 178);
            this.layoutItemSeciliMusteri.Name = "layoutItemSeciliMusteri";
            this.layoutItemSeciliMusteri.Size = new System.Drawing.Size(1356, 19);
            this.layoutItemSeciliMusteri.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemSeciliMusteri.TextVisible = false;
            
            this.layoutItemIslemler.Control = this.gridIslemler;
            this.layoutItemIslemler.Location = new System.Drawing.Point(0, 0);
            this.layoutItemIslemler.Name = "layoutItemIslemler";
            this.layoutItemIslemler.Size = new System.Drawing.Size(1356, 397);
            this.layoutItemIslemler.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemIslemler.TextVisible = false;
            
            this.layoutItemFiltrele.Control = this.btnFiltrele;
            this.layoutItemFiltrele.Location = new System.Drawing.Point(0, 680);
            this.layoutItemFiltrele.Name = "layoutItemFiltrele";
            this.layoutItemFiltrele.Size = new System.Drawing.Size(690, 44);
            this.layoutItemFiltrele.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemFiltrele.TextVisible = false;
            
            this.layoutItemKapat.Control = this.btnKapat;
            this.layoutItemKapat.Location = new System.Drawing.Point(690, 680);
            this.layoutItemKapat.Name = "layoutItemKapat";
            this.layoutItemKapat.Size = new System.Drawing.Size(690, 44);
            this.layoutItemKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemKapat.TextVisible = false;
            
            // FrmIslemGecmisi
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmIslemGecmisi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İşlem Geçmişi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmIslemGecmisi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapFiltre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridIslemler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewIslemler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMusteriArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesapFiltre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSeciliMusteri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIslemler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemFiltrele)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKapat)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
