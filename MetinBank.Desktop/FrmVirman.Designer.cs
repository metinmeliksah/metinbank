namespace MetinBank.Desktop
{
    partial class FrmVirman
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        
        // Müşteri Arama
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        
        // Kaynak Hesap (Sol Panel)
        private DevExpress.XtraGrid.GridControl gridKaynakHesaplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKaynakHesaplar;
        private DevExpress.XtraEditors.LabelControl lblKaynakInfo;
        
        // Hedef Hesap (Sağ Panel)
        private DevExpress.XtraGrid.GridControl gridHedefHesaplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHedefHesaplar;
        private DevExpress.XtraEditors.LabelControl lblHedefInfo;
        
        // Transfer Bilgileri
        private DevExpress.XtraEditors.SpinEdit numTutar;
        private DevExpress.XtraEditors.MemoEdit txtAciklama;
        private DevExpress.XtraEditors.SimpleButton btnGonder;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        
        // Layout Items
        private DevExpress.XtraLayout.LayoutControlGroup grpMusteriArama;
        private DevExpress.XtraLayout.LayoutControlGroup grpKaynak;
        private DevExpress.XtraLayout.LayoutControlGroup grpHedef;
        private DevExpress.XtraLayout.LayoutControlGroup grpTransfer;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriArama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriler;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemKaynakHesaplar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemKaynakInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHedefHesaplar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHedefInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTutar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAciklama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGonder;
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
            this.gridKaynakHesaplar = new DevExpress.XtraGrid.GridControl();
            this.gridViewKaynakHesaplar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblKaynakInfo = new DevExpress.XtraEditors.LabelControl();
            this.gridHedefHesaplar = new DevExpress.XtraGrid.GridControl();
            this.gridViewHedefHesaplar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblHedefInfo = new DevExpress.XtraEditors.LabelControl();
            this.numTutar = new DevExpress.XtraEditors.SpinEdit();
            this.txtAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.btnGonder = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpMusteriArama = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpKaynak = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpHedef = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpTransfer = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemMusteriArama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemMusteriler = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemKaynakHesaplar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemKaynakInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHedefHesaplar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHedefInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemTutar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAciklama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGonder = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemKapat = new DevExpress.XtraLayout.LayoutControlItem();
            
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridKaynakHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKaynakHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHedefHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHedefHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMusteriArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpKaynak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHedef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKaynakHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKaynakInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHedefHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHedefInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTutar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAciklama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKapat)).BeginInit();
            this.SuspendLayout();
            
            // layoutControl1
            this.layoutControl1.Controls.Add(this.txtMusteriArama);
            this.layoutControl1.Controls.Add(this.gridMusteriler);
            this.layoutControl1.Controls.Add(this.gridKaynakHesaplar);
            this.layoutControl1.Controls.Add(this.lblKaynakInfo);
            this.layoutControl1.Controls.Add(this.gridHedefHesaplar);
            this.layoutControl1.Controls.Add(this.lblHedefInfo);
            this.layoutControl1.Controls.Add(this.numTutar);
            this.layoutControl1.Controls.Add(this.txtAciklama);
            this.layoutControl1.Controls.Add(this.btnGonder);
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
            this.txtMusteriArama.Size = new System.Drawing.Size(1200, 20);
            this.txtMusteriArama.StyleController = this.layoutControl1;
            this.txtMusteriArama.TabIndex = 0;
            this.txtMusteriArama.TextChanged += new System.EventHandler(this.TxtMusteriArama_TextChanged);
            
            // gridMusteriler
            this.gridMusteriler.Location = new System.Drawing.Point(24, 69);
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.Name = "gridMusteriler";
            this.gridMusteriler.Size = new System.Drawing.Size(1352, 120);
            this.gridMusteriler.TabIndex = 1;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewMusteriler });
            
            // gridViewMusteriler
            this.gridViewMusteriler.GridControl = this.gridMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewMusteriler_RowClick);
            
            // ========== KAYNAK HESAP (SOL PANEL) ==========
            // gridKaynakHesaplar
            this.gridKaynakHesaplar.Location = new System.Drawing.Point(24, 220);
            this.gridKaynakHesaplar.MainView = this.gridViewKaynakHesaplar;
            this.gridKaynakHesaplar.Name = "gridKaynakHesaplar";
            this.gridKaynakHesaplar.Size = new System.Drawing.Size(650, 150);
            this.gridKaynakHesaplar.TabIndex = 2;
            this.gridKaynakHesaplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewKaynakHesaplar });
            
            // gridViewKaynakHesaplar
            this.gridViewKaynakHesaplar.GridControl = this.gridKaynakHesaplar;
            this.gridViewKaynakHesaplar.Name = "gridViewKaynakHesaplar";
            this.gridViewKaynakHesaplar.OptionsView.ShowGroupPanel = false;
            this.gridViewKaynakHesaplar.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewKaynakHesaplar_RowClick);
            
            // lblKaynakInfo
            this.lblKaynakInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKaynakInfo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblKaynakInfo.Location = new System.Drawing.Point(24, 374);
            this.lblKaynakInfo.Name = "lblKaynakInfo";
            this.lblKaynakInfo.Size = new System.Drawing.Size(650, 25);
            this.lblKaynakInfo.StyleController = this.layoutControl1;
            this.lblKaynakInfo.TabIndex = 3;
            this.lblKaynakInfo.Text = "📤 Kaynak Hesap: Seçilmedi";
            
            // ========== HEDEF HESAP (SAĞ PANEL) ==========
            // gridHedefHesaplar
            this.gridHedefHesaplar.Location = new System.Drawing.Point(700, 220);
            this.gridHedefHesaplar.MainView = this.gridViewHedefHesaplar;
            this.gridHedefHesaplar.Name = "gridHedefHesaplar";
            this.gridHedefHesaplar.Size = new System.Drawing.Size(650, 150);
            this.gridHedefHesaplar.TabIndex = 4;
            this.gridHedefHesaplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewHedefHesaplar });
            
            // gridViewHedefHesaplar
            this.gridViewHedefHesaplar.GridControl = this.gridHedefHesaplar;
            this.gridViewHedefHesaplar.Name = "gridViewHedefHesaplar";
            this.gridViewHedefHesaplar.OptionsView.ShowGroupPanel = false;
            this.gridViewHedefHesaplar.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewHedefHesaplar_RowClick);
            
            // lblHedefInfo
            this.lblHedefInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHedefInfo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblHedefInfo.Location = new System.Drawing.Point(700, 374);
            this.lblHedefInfo.Name = "lblHedefInfo";
            this.lblHedefInfo.Size = new System.Drawing.Size(650, 25);
            this.lblHedefInfo.StyleController = this.layoutControl1;
            this.lblHedefInfo.TabIndex = 5;
            this.lblHedefInfo.Text = "📥 Hedef Hesap: Seçilmedi";
            
            // ========== TRANSFER BİLGİLERİ ==========
            // numTutar
            this.numTutar.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.numTutar.Location = new System.Drawing.Point(100, 430);
            this.numTutar.Name = "numTutar";
            this.numTutar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numTutar.Properties.DisplayFormat.FormatString = "N2";
            this.numTutar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numTutar.Properties.EditFormat.FormatString = "N2";
            this.numTutar.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numTutar.Properties.MaxValue = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numTutar.Size = new System.Drawing.Size(300, 20);
            this.numTutar.StyleController = this.layoutControl1;
            this.numTutar.TabIndex = 6;
            
            // txtAciklama
            this.txtAciklama.Location = new System.Drawing.Point(500, 430);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Properties.NullText = "Transfer açıklaması...";
            this.txtAciklama.Size = new System.Drawing.Size(850, 40);
            this.txtAciklama.StyleController = this.layoutControl1;
            this.txtAciklama.TabIndex = 7;
            
            // btnGonder
            this.btnGonder.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnGonder.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGonder.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnGonder.Appearance.Options.UseBackColor = true;
            this.btnGonder.Appearance.Options.UseFont = true;
            this.btnGonder.Appearance.Options.UseForeColor = true;
            this.btnGonder.Location = new System.Drawing.Point(24, 500);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(660, 50);
            this.btnGonder.StyleController = this.layoutControl1;
            this.btnGonder.TabIndex = 8;
            this.btnGonder.Text = "🔄 VİRMAN YAP";
            this.btnGonder.Click += new System.EventHandler(this.BtnGonder_Click);
            
            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(688, 500);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(660, 50);
            this.btnKapat.StyleController = this.layoutControl1;
            this.btnKapat.TabIndex = 9;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            // ========== LAYOUT GROUPS & ITEMS ==========
            // layoutControlGroup1
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.grpMusteriArama,
                this.grpKaynak,
                this.grpHedef,
                this.grpTransfer,
                this.layoutItemGonder,
                this.layoutItemKapat});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1400, 750);
            this.layoutControlGroup1.TextVisible = false;
            
            // grpMusteriArama
            this.grpMusteriArama.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpMusteriArama.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.grpMusteriArama.AppearanceGroup.Options.UseFont = true;
            this.grpMusteriArama.AppearanceGroup.Options.UseForeColor = true;
            this.grpMusteriArama.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemMusteriArama,
                this.layoutItemMusteriler});
            this.grpMusteriArama.Location = new System.Drawing.Point(0, 0);
            this.grpMusteriArama.Name = "grpMusteriArama";
            this.grpMusteriArama.Size = new System.Drawing.Size(1380, 190);
            this.grpMusteriArama.Text = "👤 MÜŞTERİ SEÇİMİ";
            
            // grpKaynak
            this.grpKaynak.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpKaynak.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpKaynak.AppearanceGroup.Options.UseFont = true;
            this.grpKaynak.AppearanceGroup.Options.UseForeColor = true;
            this.grpKaynak.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemKaynakHesaplar,
                this.layoutItemKaynakInfo});
            this.grpKaynak.Location = new System.Drawing.Point(0, 190);
            this.grpKaynak.Name = "grpKaynak";
            this.grpKaynak.Size = new System.Drawing.Size(690, 210);
            this.grpKaynak.Text = "📤 KAYNAK HESAP (Gönderen)";
            
            // grpHedef
            this.grpHedef.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpHedef.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.grpHedef.AppearanceGroup.Options.UseFont = true;
            this.grpHedef.AppearanceGroup.Options.UseForeColor = true;
            this.grpHedef.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemHedefHesaplar,
                this.layoutItemHedefInfo});
            this.grpHedef.Location = new System.Drawing.Point(690, 190);
            this.grpHedef.Name = "grpHedef";
            this.grpHedef.Size = new System.Drawing.Size(690, 210);
            this.grpHedef.Text = "📥 HEDEF HESAP (Alıcı)";
            
            // grpTransfer
            this.grpTransfer.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTransfer.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.grpTransfer.AppearanceGroup.Options.UseFont = true;
            this.grpTransfer.AppearanceGroup.Options.UseForeColor = true;
            this.grpTransfer.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemTutar,
                this.layoutItemAciklama});
            this.grpTransfer.Location = new System.Drawing.Point(0, 400);
            this.grpTransfer.Name = "grpTransfer";
            this.grpTransfer.Size = new System.Drawing.Size(1380, 80);
            this.grpTransfer.Text = "💰 TRANSFER BİLGİLERİ";
            
            // Layout Items
            this.layoutItemMusteriArama.Control = this.txtMusteriArama;
            this.layoutItemMusteriArama.Location = new System.Drawing.Point(0, 0);
            this.layoutItemMusteriArama.Name = "layoutItemMusteriArama";
            this.layoutItemMusteriArama.Size = new System.Drawing.Size(1356, 24);
            this.layoutItemMusteriArama.Text = "Ara:";
            this.layoutItemMusteriArama.TextSize = new System.Drawing.Size(75, 13);
            
            this.layoutItemMusteriler.Control = this.gridMusteriler;
            this.layoutItemMusteriler.Location = new System.Drawing.Point(0, 24);
            this.layoutItemMusteriler.Name = "layoutItemMusteriler";
            this.layoutItemMusteriler.Size = new System.Drawing.Size(1356, 124);
            this.layoutItemMusteriler.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemMusteriler.TextVisible = false;
            
            this.layoutItemKaynakHesaplar.Control = this.gridKaynakHesaplar;
            this.layoutItemKaynakHesaplar.Location = new System.Drawing.Point(0, 0);
            this.layoutItemKaynakHesaplar.Name = "layoutItemKaynakHesaplar";
            this.layoutItemKaynakHesaplar.Size = new System.Drawing.Size(666, 140);
            this.layoutItemKaynakHesaplar.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemKaynakHesaplar.TextVisible = false;
            
            this.layoutItemKaynakInfo.Control = this.lblKaynakInfo;
            this.layoutItemKaynakInfo.Location = new System.Drawing.Point(0, 140);
            this.layoutItemKaynakInfo.Name = "layoutItemKaynakInfo";
            this.layoutItemKaynakInfo.Size = new System.Drawing.Size(666, 27);
            this.layoutItemKaynakInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemKaynakInfo.TextVisible = false;
            
            this.layoutItemHedefHesaplar.Control = this.gridHedefHesaplar;
            this.layoutItemHedefHesaplar.Location = new System.Drawing.Point(0, 0);
            this.layoutItemHedefHesaplar.Name = "layoutItemHedefHesaplar";
            this.layoutItemHedefHesaplar.Size = new System.Drawing.Size(666, 140);
            this.layoutItemHedefHesaplar.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemHedefHesaplar.TextVisible = false;
            
            this.layoutItemHedefInfo.Control = this.lblHedefInfo;
            this.layoutItemHedefInfo.Location = new System.Drawing.Point(0, 140);
            this.layoutItemHedefInfo.Name = "layoutItemHedefInfo";
            this.layoutItemHedefInfo.Size = new System.Drawing.Size(666, 27);
            this.layoutItemHedefInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemHedefInfo.TextVisible = false;
            
            this.layoutItemTutar.Control = this.numTutar;
            this.layoutItemTutar.Location = new System.Drawing.Point(0, 0);
            this.layoutItemTutar.Name = "layoutItemTutar";
            this.layoutItemTutar.Size = new System.Drawing.Size(400, 34);
            this.layoutItemTutar.Text = "Tutar (TL):";
            this.layoutItemTutar.TextSize = new System.Drawing.Size(75, 13);
            
            this.layoutItemAciklama.Control = this.txtAciklama;
            this.layoutItemAciklama.Location = new System.Drawing.Point(400, 0);
            this.layoutItemAciklama.Name = "layoutItemAciklama";
            this.layoutItemAciklama.Size = new System.Drawing.Size(956, 34);
            this.layoutItemAciklama.Text = "Açıklama:";
            this.layoutItemAciklama.TextSize = new System.Drawing.Size(75, 13);
            
            this.layoutItemGonder.Control = this.btnGonder;
            this.layoutItemGonder.Location = new System.Drawing.Point(0, 480);
            this.layoutItemGonder.Name = "layoutItemGonder";
            this.layoutItemGonder.Size = new System.Drawing.Size(690, 54);
            this.layoutItemGonder.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemGonder.TextVisible = false;
            
            this.layoutItemKapat.Control = this.btnKapat;
            this.layoutItemKapat.Location = new System.Drawing.Point(690, 480);
            this.layoutItemKapat.Name = "layoutItemKapat";
            this.layoutItemKapat.Size = new System.Drawing.Size(690, 54);
            this.layoutItemKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemKapat.TextVisible = false;
            
            // FrmVirman
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmVirman";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Virman (Hesaplar Arası Transfer)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmVirman_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridKaynakHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKaynakHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHedefHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHedefHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMusteriArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpKaynak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHedef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKaynakHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKaynakInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHedefHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHedefInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTutar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAciklama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKapat)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
