namespace MetinBank.Desktop
{
    partial class FrmOnayBekleyenler
    {
        private System.ComponentModel.IContainer components = null;

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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabIslemler = new DevExpress.XtraTab.XtraTabPage();
            this.gridOnaylar = new DevExpress.XtraGrid.GridControl();
            this.gridViewOnaylar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabKrediler = new DevExpress.XtraTab.XtraTabPage();
            this.gridKrediler = new DevExpress.XtraGrid.GridControl();
            this.gridViewKrediler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabSubeDegisiklik = new DevExpress.XtraTab.XtraTabPage();
            this.gridSubeDegisiklik = new DevExpress.XtraGrid.GridControl();
            this.gridViewSubeDegisiklik = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grpDetay = new DevExpress.XtraEditors.GroupControl();
            this.lblIslemTipi = new DevExpress.XtraEditors.LabelControl();
            this.lblTutar = new DevExpress.XtraEditors.LabelControl();
            this.lblTarih = new DevExpress.XtraEditors.LabelControl();
            this.lblOlusturan = new DevExpress.XtraEditors.LabelControl();
            this.btnOnayla = new DevExpress.XtraEditors.SimpleButton();
            this.btnReddet = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemTabControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemDetay = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemOnayla = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemReddet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemYenile = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemKapat = new DevExpress.XtraLayout.LayoutControlItem();

            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabIslemler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOnaylar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnaylar)).BeginInit();
            this.tabKrediler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKrediler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKrediler)).BeginInit();
            this.tabSubeDegisiklik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubeDegisiklik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSubeDegisiklik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDetay)).BeginInit();
            this.grpDetay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTabControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDetay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemOnayla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemReddet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemYenile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKapat)).BeginInit();
            this.SuspendLayout();

            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tabControl); // Changed from gridOnaylar to tabControl
            this.layoutControl1.Controls.Add(this.grpDetay);
            this.layoutControl1.Controls.Add(this.btnOnayla);
            this.layoutControl1.Controls.Add(this.btnReddet);
            this.layoutControl1.Controls.Add(this.btnYenile);
            this.layoutControl1.Controls.Add(this.btnKapat);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControl1.TabIndex = 0;

            // 
            // tabControl
            // 
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabIslemler;
            this.tabControl.Size = new System.Drawing.Size(876, 590);
            this.tabControl.TabIndex = 6;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabIslemler,
            this.tabKrediler,
            this.tabSubeDegisiklik});

            // 
            // tabIslemler
            // 
            this.tabIslemler.Controls.Add(this.gridOnaylar);
            this.tabIslemler.Name = "tabIslemler";
            this.tabIslemler.Size = new System.Drawing.Size(874, 565);
            this.tabIslemler.Text = "Para Transferleri";

            // 
            // gridOnaylar
            // 
            this.gridOnaylar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOnaylar.MainView = this.gridViewOnaylar;
            this.gridOnaylar.Name = "gridOnaylar";
            this.gridOnaylar.Size = new System.Drawing.Size(874, 565);
            this.gridOnaylar.TabIndex = 0;
            this.gridOnaylar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOnaylar});

            // 
            // gridViewOnaylar
            // 
            this.gridViewOnaylar.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.gridViewOnaylar.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gridViewOnaylar.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gridViewOnaylar.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridViewOnaylar.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewOnaylar.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridViewOnaylar.GridControl = this.gridOnaylar;
            this.gridViewOnaylar.Name = "gridViewOnaylar";
            this.gridViewOnaylar.OptionsBehavior.Editable = false;
            this.gridViewOnaylar.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewOnaylar.OptionsView.ShowGroupPanel = false;
            this.gridViewOnaylar.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridViewOnaylar_FocusedRowChanged);

            // 
            // tabKrediler
            // 
            this.tabKrediler.Controls.Add(this.gridKrediler);
            this.tabKrediler.Name = "tabKrediler";
            this.tabKrediler.Size = new System.Drawing.Size(874, 565);
            this.tabKrediler.Text = "Kredi Başvuruları";

            // 
            // gridKrediler
            // 
            this.gridKrediler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKrediler.MainView = this.gridViewKrediler;
            this.gridKrediler.Name = "gridKrediler";
            this.gridKrediler.Size = new System.Drawing.Size(874, 565);
            this.gridKrediler.TabIndex = 0;
            this.gridKrediler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewKrediler});

            // 
            // gridViewKrediler
            // 
            this.gridViewKrediler.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.gridViewKrediler.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gridViewKrediler.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gridViewKrediler.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridViewKrediler.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewKrediler.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridViewKrediler.GridControl = this.gridKrediler;
            this.gridViewKrediler.Name = "gridViewKrediler";
            this.gridViewKrediler.OptionsBehavior.Editable = false;
            this.gridViewKrediler.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewKrediler.OptionsView.ShowGroupPanel = false;
            this.gridViewKrediler.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridViewKrediler_FocusedRowChanged);

            // 
            // tabSubeDegisiklik
            // 
            this.tabSubeDegisiklik.Controls.Add(this.gridSubeDegisiklik);
            this.tabSubeDegisiklik.Name = "tabSubeDegisiklik";
            this.tabSubeDegisiklik.Size = new System.Drawing.Size(874, 565);
            this.tabSubeDegisiklik.Text = "Şube Değişikliği Talepleri";

            // 
            // gridSubeDegisiklik
            // 
            this.gridSubeDegisiklik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSubeDegisiklik.MainView = this.gridViewSubeDegisiklik;
            this.gridSubeDegisiklik.Name = "gridSubeDegisiklik";
            this.gridSubeDegisiklik.Size = new System.Drawing.Size(874, 565);
            this.gridSubeDegisiklik.TabIndex = 0;
            this.gridSubeDegisiklik.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSubeDegisiklik});

            // 
            // gridViewSubeDegisiklik
            // 
            this.gridViewSubeDegisiklik.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.gridViewSubeDegisiklik.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gridViewSubeDegisiklik.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gridViewSubeDegisiklik.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridViewSubeDegisiklik.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewSubeDegisiklik.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridViewSubeDegisiklik.GridControl = this.gridSubeDegisiklik;
            this.gridViewSubeDegisiklik.Name = "gridViewSubeDegisiklik";
            this.gridViewSubeDegisiklik.OptionsBehavior.Editable = false;
            this.gridViewSubeDegisiklik.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewSubeDegisiklik.OptionsView.ShowGroupPanel = false;
            this.gridViewSubeDegisiklik.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridViewSubeDegisiklik_FocusedRowChanged);

            // 
            // grpDetay
            // 
            this.grpDetay.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpDetay.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.grpDetay.AppearanceCaption.Options.UseFont = true;
            this.grpDetay.AppearanceCaption.Options.UseForeColor = true;
            this.grpDetay.Controls.Add(this.lblIslemTipi);
            this.grpDetay.Controls.Add(this.lblTutar);
            this.grpDetay.Controls.Add(this.lblTarih);
            this.grpDetay.Controls.Add(this.lblOlusturan);
            this.grpDetay.Location = new System.Drawing.Point(892, 12);
            this.grpDetay.Name = "grpDetay";
            this.grpDetay.Size = new System.Drawing.Size(296, 590);
            this.grpDetay.TabIndex = 1;
            this.grpDetay.Text = "📋 İşlem Detayı";

            // 
            // lblIslemTipi
            // 
            this.lblIslemTipi.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblIslemTipi.Location = new System.Drawing.Point(15, 40);
            this.lblIslemTipi.Name = "lblIslemTipi";
            this.lblIslemTipi.Size = new System.Drawing.Size(60, 15);
            this.lblIslemTipi.TabIndex = 0;
            this.lblIslemTipi.Text = "İşlem Tipi: -";

            // 
            // lblTutar
            // 
            this.lblTutar.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTutar.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblTutar.Location = new System.Drawing.Point(15, 70);
            this.lblTutar.Name = "lblTutar";
            this.lblTutar.Size = new System.Drawing.Size(50, 21);
            this.lblTutar.TabIndex = 1;
            this.lblTutar.Text = "Tutar: -";

            // 
            // lblTarih
            // 
            this.lblTarih.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTarih.Location = new System.Drawing.Point(15, 100);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(40, 15);
            this.lblTarih.TabIndex = 2;
            this.lblTarih.Text = "Tarih: -";

            // 
            // lblOlusturan
            // 
            this.lblOlusturan.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOlusturan.Location = new System.Drawing.Point(15, 130);
            this.lblOlusturan.Name = "lblOlusturan";
            this.lblOlusturan.Size = new System.Drawing.Size(65, 15);
            this.lblOlusturan.TabIndex = 3;
            this.lblOlusturan.Text = "Oluşturan: -";

            // 
            // btnOnayla
            // 
            this.btnOnayla.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnOnayla.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOnayla.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnOnayla.Appearance.Options.UseBackColor = true;
            this.btnOnayla.Appearance.Options.UseFont = true;
            this.btnOnayla.Appearance.Options.UseForeColor = true;
            this.btnOnayla.Location = new System.Drawing.Point(12, 606);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(290, 40);
            this.btnOnayla.StyleController = this.layoutControl1;
            this.btnOnayla.TabIndex = 2;
            this.btnOnayla.Text = "✅ ONAYLA";
            this.btnOnayla.Click += new System.EventHandler(this.BtnOnayla_Click);

            // 
            // btnReddet
            // 
            this.btnReddet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnReddet.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReddet.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnReddet.Appearance.Options.UseBackColor = true;
            this.btnReddet.Appearance.Options.UseFont = true;
            this.btnReddet.Appearance.Options.UseForeColor = true;
            this.btnReddet.Location = new System.Drawing.Point(306, 606);
            this.btnReddet.Name = "btnReddet";
            this.btnReddet.Size = new System.Drawing.Size(290, 40);
            this.btnReddet.StyleController = this.layoutControl1;
            this.btnReddet.TabIndex = 3;
            this.btnReddet.Text = "❌ REDDET";
            this.btnReddet.Click += new System.EventHandler(this.BtnReddet_Click);

            // 
            // btnYenile
            // 
            this.btnYenile.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnYenile.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnYenile.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Appearance.Options.UseBackColor = true;
            this.btnYenile.Appearance.Options.UseFont = true;
            this.btnYenile.Appearance.Options.UseForeColor = true;
            this.btnYenile.Location = new System.Drawing.Point(600, 606);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(290, 40);
            this.btnYenile.StyleController = this.layoutControl1;
            this.btnYenile.TabIndex = 4;
            this.btnYenile.Text = "🔄 Yenile";
            this.btnYenile.Click += new System.EventHandler(this.BtnYenile_Click);

            // 
            // btnKapat
            // 
            this.btnKapat.Location = new System.Drawing.Point(894, 606);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(294, 40);
            this.btnKapat.StyleController = this.layoutControl1;
            this.btnKapat.TabIndex = 5;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);

            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemTabControl,
            this.layoutItemDetay,
            this.layoutItemOnayla,
            this.layoutItemReddet,
            this.layoutItemYenile,
            this.layoutItemKapat});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControlGroup1.TextVisible = false;

            // 
            // layoutItemTabControl
            // 
            this.layoutItemTabControl.Control = this.tabControl;
            this.layoutItemTabControl.Location = new System.Drawing.Point(0, 0);
            this.layoutItemTabControl.Name = "layoutItemTabControl";
            this.layoutItemTabControl.Size = new System.Drawing.Size(880, 594);
            this.layoutItemTabControl.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemTabControl.TextVisible = false;

            // 
            // layoutItemDetay
            // 
            this.layoutItemDetay.Control = this.grpDetay;
            this.layoutItemDetay.Location = new System.Drawing.Point(880, 0);
            this.layoutItemDetay.Name = "layoutItemDetay";
            this.layoutItemDetay.Size = new System.Drawing.Size(300, 594);
            this.layoutItemDetay.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemDetay.TextVisible = false;

            // 
            // layoutItemOnayla
            // 
            this.layoutItemOnayla.Control = this.btnOnayla;
            this.layoutItemOnayla.Location = new System.Drawing.Point(0, 594);
            this.layoutItemOnayla.Name = "layoutItemOnayla";
            this.layoutItemOnayla.Size = new System.Drawing.Size(294, 44);
            this.layoutItemOnayla.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemOnayla.TextVisible = false;

            // 
            // layoutItemReddet
            // 
            this.layoutItemReddet.Control = this.btnReddet;
            this.layoutItemReddet.Location = new System.Drawing.Point(294, 594);
            this.layoutItemReddet.Name = "layoutItemReddet";
            this.layoutItemReddet.Size = new System.Drawing.Size(294, 44);
            this.layoutItemReddet.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemReddet.TextVisible = false;

            // 
            // layoutItemYenile
            // 
            this.layoutItemYenile.Control = this.btnYenile;
            this.layoutItemYenile.Location = new System.Drawing.Point(588, 594);
            this.layoutItemYenile.Name = "layoutItemYenile";
            this.layoutItemYenile.Size = new System.Drawing.Size(294, 44);
            this.layoutItemYenile.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemYenile.TextVisible = false;

            // 
            // layoutItemKapat
            // 
            this.layoutItemKapat.Control = this.btnKapat;
            this.layoutItemKapat.Location = new System.Drawing.Point(882, 594);
            this.layoutItemKapat.Name = "layoutItemKapat";
            this.layoutItemKapat.Size = new System.Drawing.Size(298, 44);
            this.layoutItemKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemKapat.TextVisible = false;
            
            // 
            // FrmOnayBekleyenler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmOnayBekleyenler";
            this.Text = "Onay Bekleyen İşlemler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOnayBekleyenler_Load);
            
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabIslemler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOnaylar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnaylar)).EndInit();
            this.tabKrediler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKrediler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKrediler)).EndInit();
            this.tabSubeDegisiklik.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSubeDegisiklik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSubeDegisiklik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDetay)).EndInit();
            this.grpDetay.ResumeLayout(false);
            this.grpDetay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTabControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDetay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemOnayla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemReddet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemYenile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKapat)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabIslemler;
        private DevExpress.XtraGrid.GridControl gridOnaylar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOnaylar;
        private DevExpress.XtraTab.XtraTabPage tabKrediler;
        private DevExpress.XtraGrid.GridControl gridKrediler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKrediler;
        private DevExpress.XtraTab.XtraTabPage tabSubeDegisiklik;
        private DevExpress.XtraGrid.GridControl gridSubeDegisiklik;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSubeDegisiklik;
        private DevExpress.XtraEditors.GroupControl grpDetay;
        private DevExpress.XtraEditors.LabelControl lblIslemTipi;
        private DevExpress.XtraEditors.LabelControl lblTutar;
        private DevExpress.XtraEditors.LabelControl lblTarih;
        private DevExpress.XtraEditors.LabelControl lblOlusturan;
        private DevExpress.XtraEditors.SimpleButton btnOnayla;
        private DevExpress.XtraEditors.SimpleButton btnReddet;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTabControl;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemDetay;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemOnayla;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemReddet;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemYenile;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemKapat;
    }
}
