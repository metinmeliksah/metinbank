namespace MetinBank.Desktop
{
    partial class FrmEFT
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        
        // Gönderen (Sol Panel)
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        private DevExpress.XtraGrid.GridControl gridHesaplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHesaplar;
        private DevExpress.XtraEditors.LabelControl lblGonderenInfo;
        
        // Alıcı (Sağ Panel)
        private DevExpress.XtraEditors.TextEdit txtHedefIBAN;
        private DevExpress.XtraEditors.TextEdit txtAliciAdi;
        private DevExpress.XtraEditors.LabelControl lblAliciInfo;
        
        // Transfer Bilgileri
        private DevExpress.XtraEditors.SpinEdit numTutar;
        private DevExpress.XtraEditors.MemoEdit txtAciklama;
        private DevExpress.XtraEditors.SimpleButton btnGonder;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        
        // Layout Items
        private DevExpress.XtraLayout.LayoutControlGroup grpGonderen;
        private DevExpress.XtraLayout.LayoutControlGroup grpAlici;
        private DevExpress.XtraLayout.LayoutControlGroup grpTransfer;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriArama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriler;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHesaplar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGonderenInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemHedefIBAN;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAliciAdi;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAliciInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTutar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAciklama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGonder;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemKapat;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;

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
            this.lblGonderenInfo = new DevExpress.XtraEditors.LabelControl();
            this.txtHedefIBAN = new DevExpress.XtraEditors.TextEdit();
            this.txtAliciAdi = new DevExpress.XtraEditors.TextEdit();
            this.lblAliciInfo = new DevExpress.XtraEditors.LabelControl();
            this.numTutar = new DevExpress.XtraEditors.SpinEdit();
            this.txtAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.btnGonder = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpGonderen = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpAlici = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpTransfer = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemMusteriArama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemMusteriler = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHesaplar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGonderenInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemHedefIBAN = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAliciAdi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAliciInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemTutar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAciklama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGonder = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemKapat = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHedefIBAN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpGonderen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAlici)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHedefIBAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciAdi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTutar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAciklama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKapat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            
            // layoutControl1
            this.layoutControl1.Controls.Add(this.txtMusteriArama);
            this.layoutControl1.Controls.Add(this.gridMusteriler);
            this.layoutControl1.Controls.Add(this.gridHesaplar);
            this.layoutControl1.Controls.Add(this.lblGonderenInfo);
            this.layoutControl1.Controls.Add(this.txtHedefIBAN);
            this.layoutControl1.Controls.Add(this.txtAliciAdi);
            this.layoutControl1.Controls.Add(this.lblAliciInfo);
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
            
            // ========== GÖNDEREN (SOL PANEL) ==========
            // txtMusteriArama
            this.txtMusteriArama.Location = new System.Drawing.Point(100, 45);
            this.txtMusteriArama.Name = "txtMusteriArama";
            this.txtMusteriArama.Properties.NullValuePrompt = "Gönderen müşteri ara (Ad, TCKN, Müşteri No)...";
            this.txtMusteriArama.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMusteriArama.Size = new System.Drawing.Size(560, 20);
            this.txtMusteriArama.StyleController = this.layoutControl1;
            this.txtMusteriArama.TabIndex = 0;
            this.txtMusteriArama.TextChanged += new System.EventHandler(this.TxtMusteriArama_TextChanged);
            
            // gridMusteriler
            this.gridMusteriler.Location = new System.Drawing.Point(24, 69);
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.Name = "gridMusteriler";
            this.gridMusteriler.Size = new System.Drawing.Size(660, 130);
            this.gridMusteriler.TabIndex = 1;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewMusteriler });
            
            // gridViewMusteriler
            this.gridViewMusteriler.GridControl = this.gridMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewMusteriler_RowClick);
            
            // gridHesaplar
            this.gridHesaplar.Location = new System.Drawing.Point(24, 203);
            this.gridHesaplar.MainView = this.gridViewHesaplar;
            this.gridHesaplar.Name = "gridHesaplar";
            this.gridHesaplar.Size = new System.Drawing.Size(660, 120);
            this.gridHesaplar.TabIndex = 2;
            this.gridHesaplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewHesaplar });
            
            // gridViewHesaplar
            this.gridViewHesaplar.GridControl = this.gridHesaplar;
            this.gridViewHesaplar.Name = "gridViewHesaplar";
            this.gridViewHesaplar.OptionsView.ShowGroupPanel = false;
            this.gridViewHesaplar.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewHesaplar_RowClick);
            
            // lblGonderenInfo
            this.lblGonderenInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGonderenInfo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblGonderenInfo.Location = new System.Drawing.Point(24, 327);
            this.lblGonderenInfo.Name = "lblGonderenInfo";
            this.lblGonderenInfo.Size = new System.Drawing.Size(660, 25);
            this.lblGonderenInfo.StyleController = this.layoutControl1;
            this.lblGonderenInfo.TabIndex = 3;
            this.lblGonderenInfo.Text = "📤 Gönderen: Seçilmedi";
            
            // ========== ALICI (SAĞ PANEL) ==========
            // txtHedefIBAN
            this.txtHedefIBAN.Location = new System.Drawing.Point(760, 45);
            this.txtHedefIBAN.Name = "txtHedefIBAN";
            this.txtHedefIBAN.Properties.NullValuePrompt = "Harici banka IBAN girin (TR ile başlar)";
            this.txtHedefIBAN.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtHedefIBAN.Properties.Mask.EditMask = "TR00 0000 0000 0000 0000 0000 00";
            this.txtHedefIBAN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtHedefIBAN.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtHedefIBAN.Size = new System.Drawing.Size(560, 20);
            this.txtHedefIBAN.StyleController = this.layoutControl1;
            this.txtHedefIBAN.TabIndex = 4;
            this.txtHedefIBAN.Leave += new System.EventHandler(this.TxtHedefIBAN_Leave);
            
            // txtAliciAdi
            this.txtAliciAdi.Location = new System.Drawing.Point(760, 93);
            this.txtAliciAdi.Name = "txtAliciAdi";
            this.txtAliciAdi.Properties.NullValuePrompt = "Alıcı ad soyad girin";
            this.txtAliciAdi.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtAliciAdi.Size = new System.Drawing.Size(560, 20);
            this.txtAliciAdi.StyleController = this.layoutControl1;
            this.txtAliciAdi.TabIndex = 5;
            
            // lblAliciInfo
            this.lblAliciInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAliciInfo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblAliciInfo.Location = new System.Drawing.Point(700, 327);
            this.lblAliciInfo.Name = "lblAliciInfo";
            this.lblAliciInfo.Size = new System.Drawing.Size(660, 25);
            this.lblAliciInfo.StyleController = this.layoutControl1;
            this.lblAliciInfo.TabIndex = 6;
            this.lblAliciInfo.Text = "📥 Alıcı: Harici banka hesabına EFT";
            
            // ========== TRANSFER BİLGİLERİ ==========
            // numTutar
            this.numTutar.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.numTutar.Location = new System.Drawing.Point(100, 400);
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
            this.numTutar.TabIndex = 7;
            
            // txtAciklama
            this.txtAciklama.Location = new System.Drawing.Point(500, 400);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Properties.NullText = "Transfer açıklaması...";
            this.txtAciklama.Size = new System.Drawing.Size(850, 40);
            this.txtAciklama.StyleController = this.layoutControl1;
            this.txtAciklama.TabIndex = 8;
            
            // btnGonder
            this.btnGonder.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.btnGonder.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGonder.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnGonder.Appearance.Options.UseBackColor = true;
            this.btnGonder.Appearance.Options.UseFont = true;
            this.btnGonder.Appearance.Options.UseForeColor = true;
            this.btnGonder.Location = new System.Drawing.Point(24, 480);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(660, 50);
            this.btnGonder.StyleController = this.layoutControl1;
            this.btnGonder.TabIndex = 9;
            this.btnGonder.Text = "🏦 EFT GÖNDER";
            this.btnGonder.Click += new System.EventHandler(this.BtnGonder_Click);
            
            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(688, 480);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(660, 50);
            this.btnKapat.StyleController = this.layoutControl1;
            this.btnKapat.TabIndex = 10;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            // ========== LAYOUT GROUPS & ITEMS ==========
            // layoutControlGroup1
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.grpGonderen,
                this.grpAlici,
                this.grpTransfer,
                this.layoutItemGonder,
                this.layoutItemKapat});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1400, 750);
            this.layoutControlGroup1.TextVisible = false;
            
            // grpGonderen
            this.grpGonderen.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpGonderen.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpGonderen.AppearanceGroup.Options.UseFont = true;
            this.grpGonderen.AppearanceGroup.Options.UseForeColor = true;
            this.grpGonderen.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemMusteriArama,
                this.layoutItemMusteriler,
                this.layoutItemHesaplar,
                this.layoutItemGonderenInfo});
            this.grpGonderen.Location = new System.Drawing.Point(0, 0);
            this.grpGonderen.Name = "grpGonderen";
            this.grpGonderen.Size = new System.Drawing.Size(690, 350);
            this.grpGonderen.Text = "📤 GÖNDEREN (MetinBank Müşterisi)";
            
            // grpAlici
            this.grpAlici.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpAlici.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.grpAlici.AppearanceGroup.Options.UseFont = true;
            this.grpAlici.AppearanceGroup.Options.UseForeColor = true;
            this.grpAlici.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemHedefIBAN,
                this.layoutItemAliciAdi,
                this.emptySpaceItem1,
                this.layoutItemAliciInfo});
            this.grpAlici.Location = new System.Drawing.Point(690, 0);
            this.grpAlici.Name = "grpAlici";
            this.grpAlici.Size = new System.Drawing.Size(690, 350);
            this.grpAlici.Text = "📥 ALICI (Harici Banka Hesabı)";
            
            // grpTransfer
            this.grpTransfer.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTransfer.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.grpTransfer.AppearanceGroup.Options.UseFont = true;
            this.grpTransfer.AppearanceGroup.Options.UseForeColor = true;
            this.grpTransfer.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemTutar,
                this.layoutItemAciklama});
            this.grpTransfer.Location = new System.Drawing.Point(0, 350);
            this.grpTransfer.Name = "grpTransfer";
            this.grpTransfer.Size = new System.Drawing.Size(1380, 80);
            this.grpTransfer.Text = "💰 TRANSFER BİLGİLERİ";
            
            // Layout Items
            this.layoutItemMusteriArama.Control = this.txtMusteriArama;
            this.layoutItemMusteriArama.Location = new System.Drawing.Point(0, 0);
            this.layoutItemMusteriArama.Name = "layoutItemMusteriArama";
            this.layoutItemMusteriArama.Size = new System.Drawing.Size(666, 24);
            this.layoutItemMusteriArama.Text = "Ara:";
            this.layoutItemMusteriArama.TextSize = new System.Drawing.Size(75, 13);
            
            this.layoutItemMusteriler.Control = this.gridMusteriler;
            this.layoutItemMusteriler.Location = new System.Drawing.Point(0, 24);
            this.layoutItemMusteriler.Name = "layoutItemMusteriler";
            this.layoutItemMusteriler.Size = new System.Drawing.Size(666, 134);
            this.layoutItemMusteriler.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemMusteriler.TextVisible = false;
            
            this.layoutItemHesaplar.Control = this.gridHesaplar;
            this.layoutItemHesaplar.Location = new System.Drawing.Point(0, 158);
            this.layoutItemHesaplar.Name = "layoutItemHesaplar";
            this.layoutItemHesaplar.Size = new System.Drawing.Size(666, 124);
            this.layoutItemHesaplar.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemHesaplar.TextVisible = false;
            
            this.layoutItemGonderenInfo.Control = this.lblGonderenInfo;
            this.layoutItemGonderenInfo.Location = new System.Drawing.Point(0, 282);
            this.layoutItemGonderenInfo.Name = "layoutItemGonderenInfo";
            this.layoutItemGonderenInfo.Size = new System.Drawing.Size(666, 25);
            this.layoutItemGonderenInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemGonderenInfo.TextVisible = false;
            
            this.layoutItemHedefIBAN.Control = this.txtHedefIBAN;
            this.layoutItemHedefIBAN.Location = new System.Drawing.Point(0, 0);
            this.layoutItemHedefIBAN.Name = "layoutItemHedefIBAN";
            this.layoutItemHedefIBAN.Size = new System.Drawing.Size(666, 24);
            this.layoutItemHedefIBAN.Text = "IBAN:";
            this.layoutItemHedefIBAN.TextSize = new System.Drawing.Size(75, 13);
            
            this.layoutItemAliciAdi.Control = this.txtAliciAdi;
            this.layoutItemAliciAdi.Location = new System.Drawing.Point(0, 24);
            this.layoutItemAliciAdi.Name = "layoutItemAliciAdi";
            this.layoutItemAliciAdi.Size = new System.Drawing.Size(666, 24);
            this.layoutItemAliciAdi.Text = "Alıcı Adı:";
            this.layoutItemAliciAdi.TextSize = new System.Drawing.Size(75, 13);
            
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 48);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(666, 234);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            
            this.layoutItemAliciInfo.Control = this.lblAliciInfo;
            this.layoutItemAliciInfo.Location = new System.Drawing.Point(0, 282);
            this.layoutItemAliciInfo.Name = "layoutItemAliciInfo";
            this.layoutItemAliciInfo.Size = new System.Drawing.Size(666, 25);
            this.layoutItemAliciInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemAliciInfo.TextVisible = false;
            
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
            this.layoutItemGonder.Location = new System.Drawing.Point(0, 430);
            this.layoutItemGonder.Name = "layoutItemGonder";
            this.layoutItemGonder.Size = new System.Drawing.Size(690, 54);
            this.layoutItemGonder.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemGonder.TextVisible = false;
            
            this.layoutItemKapat.Control = this.btnKapat;
            this.layoutItemKapat.Location = new System.Drawing.Point(690, 430);
            this.layoutItemKapat.Name = "layoutItemKapat";
            this.layoutItemKapat.Size = new System.Drawing.Size(690, 54);
            this.layoutItemKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemKapat.TextVisible = false;
            
            // FrmEFT
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmEFT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EFT İşlemi (Elektronik Fon Transferi)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmEFT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHedefIBAN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpGonderen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAlici)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemHedefIBAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciAdi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTutar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAciklama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKapat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
