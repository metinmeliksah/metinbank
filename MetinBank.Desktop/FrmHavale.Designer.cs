namespace MetinBank.Desktop
{
    partial class FrmHavale
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        
        // Gönderen Müşteri (Sol Panel)
        private DevExpress.XtraEditors.TextEdit txtGonderenArama;
        private DevExpress.XtraGrid.GridControl gridGonderenMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGonderenMusteriler;
        private DevExpress.XtraGrid.GridControl gridGonderenHesaplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGonderenHesaplar;
        private DevExpress.XtraEditors.LabelControl lblGonderenInfo;
        
        // Alıcı Müşteri (Sağ Panel)
        private DevExpress.XtraEditors.TextEdit txtAliciArama;
        private DevExpress.XtraEditors.TextEdit txtAliciIBAN;
        private DevExpress.XtraGrid.GridControl gridAliciMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAliciMusteriler;
        private DevExpress.XtraGrid.GridControl gridAliciHesaplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAliciHesaplar;
        private DevExpress.XtraEditors.LabelControl lblAliciInfo;
        
        // Transfer Bilgileri
        private DevExpress.XtraEditors.SpinEdit numTutar;
        private DevExpress.XtraEditors.TextEdit txtAciklama;
        private DevExpress.XtraEditors.SimpleButton btnGonder;
        
        // Layout Items
        private DevExpress.XtraLayout.LayoutControlGroup grpGonderen;
        private DevExpress.XtraLayout.LayoutControlGroup grpAlici;
        private DevExpress.XtraLayout.LayoutControlGroup grpTransfer;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGonderenArama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGonderenMusteriler;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGonderenHesaplar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGonderenInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAliciArama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAliciIBAN;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAliciMusteriler;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAliciHesaplar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAliciInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTutar;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAciklama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGonder;

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
            this.txtGonderenArama = new DevExpress.XtraEditors.TextEdit();
            this.gridGonderenMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewGonderenMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridGonderenHesaplar = new DevExpress.XtraGrid.GridControl();
            this.gridViewGonderenHesaplar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblGonderenInfo = new DevExpress.XtraEditors.LabelControl();
            this.txtAliciArama = new DevExpress.XtraEditors.TextEdit();
            this.txtAliciIBAN = new DevExpress.XtraEditors.TextEdit();
            this.gridAliciMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewAliciMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridAliciHesaplar = new DevExpress.XtraGrid.GridControl();
            this.gridViewAliciHesaplar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblAliciInfo = new DevExpress.XtraEditors.LabelControl();
            this.numTutar = new DevExpress.XtraEditors.SpinEdit();
            this.txtAciklama = new DevExpress.XtraEditors.TextEdit();
            this.btnGonder = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpGonderen = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemGonderenArama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGonderenMusteriler = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGonderenHesaplar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGonderenInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpAlici = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemAliciIBAN = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAliciArama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAliciMusteriler = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAliciHesaplar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAliciInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.grpTransfer = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemTutar = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAciklama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGonder = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGonderenArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGonderenMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGonderenMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGonderenHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGonderenHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliciArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliciIBAN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAliciMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAliciMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAliciHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAliciHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpGonderen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAlici)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciIBAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTutar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAciklama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonder)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtGonderenArama);
            this.layoutControl1.Controls.Add(this.gridGonderenMusteriler);
            this.layoutControl1.Controls.Add(this.gridGonderenHesaplar);
            this.layoutControl1.Controls.Add(this.lblGonderenInfo);
            this.layoutControl1.Controls.Add(this.txtAliciArama);
            this.layoutControl1.Controls.Add(this.txtAliciIBAN);
            this.layoutControl1.Controls.Add(this.gridAliciMusteriler);
            this.layoutControl1.Controls.Add(this.gridAliciHesaplar);
            this.layoutControl1.Controls.Add(this.lblAliciInfo);
            this.layoutControl1.Controls.Add(this.numTutar);
            this.layoutControl1.Controls.Add(this.txtAciklama);
            this.layoutControl1.Controls.Add(this.btnGonder);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(773, 489, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1400, 750);
            this.layoutControl1.TabIndex = 0;
            // 
            // txtGonderenArama
            // 
            this.txtGonderenArama.Location = new System.Drawing.Point(88, 45);
            this.txtGonderenArama.Name = "txtGonderenArama";
            this.txtGonderenArama.Properties.NullValuePrompt = "Gönderen müşteri ara (Ad, TCKN, Müşteri No)...";
            this.txtGonderenArama.Size = new System.Drawing.Size(590, 20);
            this.txtGonderenArama.StyleController = this.layoutControl1;
            this.txtGonderenArama.TabIndex = 0;
            this.txtGonderenArama.TextChanged += new System.EventHandler(this.TxtGonderenArama_TextChanged);
            // 
            // gridGonderenMusteriler
            // 
            this.gridGonderenMusteriler.Location = new System.Drawing.Point(24, 69);
            this.gridGonderenMusteriler.MainView = this.gridViewGonderenMusteriler;
            this.gridGonderenMusteriler.Name = "gridGonderenMusteriler";
            this.gridGonderenMusteriler.Size = new System.Drawing.Size(654, 271);
            this.gridGonderenMusteriler.TabIndex = 1;
            this.gridGonderenMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGonderenMusteriler});
            // 
            // gridViewGonderenMusteriler
            // 
            this.gridViewGonderenMusteriler.GridControl = this.gridGonderenMusteriler;
            this.gridViewGonderenMusteriler.Name = "gridViewGonderenMusteriler";
            this.gridViewGonderenMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewGonderenMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewGonderenMusteriler_RowClick);
            // 
            // gridGonderenHesaplar
            // 
            this.gridGonderenHesaplar.Location = new System.Drawing.Point(24, 344);
            this.gridGonderenHesaplar.MainView = this.gridViewGonderenHesaplar;
            this.gridGonderenHesaplar.Name = "gridGonderenHesaplar";
            this.gridGonderenHesaplar.Size = new System.Drawing.Size(654, 259);
            this.gridGonderenHesaplar.TabIndex = 2;
            this.gridGonderenHesaplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGonderenHesaplar});
            // 
            // gridViewGonderenHesaplar
            // 
            this.gridViewGonderenHesaplar.GridControl = this.gridGonderenHesaplar;
            this.gridViewGonderenHesaplar.Name = "gridViewGonderenHesaplar";
            this.gridViewGonderenHesaplar.OptionsView.ShowGroupPanel = false;
            this.gridViewGonderenHesaplar.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewGonderenHesaplar_RowClick);
            // 
            // lblGonderenInfo
            // 
            this.lblGonderenInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGonderenInfo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblGonderenInfo.Appearance.Options.UseFont = true;
            this.lblGonderenInfo.Appearance.Options.UseForeColor = true;
            this.lblGonderenInfo.Location = new System.Drawing.Point(24, 607);
            this.lblGonderenInfo.Name = "lblGonderenInfo";
            this.lblGonderenInfo.Size = new System.Drawing.Size(654, 20);
            this.lblGonderenInfo.StyleController = this.layoutControl1;
            this.lblGonderenInfo.TabIndex = 3;
            this.lblGonderenInfo.Text = "📤 Gönderen: Seçilmedi";
            // 
            // txtAliciArama
            // 
            this.txtAliciArama.Location = new System.Drawing.Point(1026, 45);
            this.txtAliciArama.Name = "txtAliciArama";
            this.txtAliciArama.Properties.NullValuePrompt = "veya Alıcı ara...";
            this.txtAliciArama.Size = new System.Drawing.Size(350, 20);
            this.txtAliciArama.StyleController = this.layoutControl1;
            this.txtAliciArama.TabIndex = 5;
            this.txtAliciArama.TextChanged += new System.EventHandler(this.TxtAliciArama_TextChanged);
            // 
            // txtAliciIBAN
            // 
            this.txtAliciIBAN.Location = new System.Drawing.Point(285, 45);
            this.txtAliciIBAN.Name = "txtAliciIBAN";
            this.txtAliciIBAN.Properties.Mask.EditMask = "TR00 0000 0000 0000 0000 0000 00";
            this.txtAliciIBAN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtAliciIBAN.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtAliciIBAN.Properties.NullValuePrompt = "Alıcı IBAN girin (direkt transfer için)...";
            this.txtAliciIBAN.Size = new System.Drawing.Size(673, 20);
            this.txtAliciIBAN.StyleController = this.layoutControl1;
            this.txtAliciIBAN.TabIndex = 4;
            this.txtAliciIBAN.Leave += new System.EventHandler(this.TxtAliciIBAN_Leave);
            // 
            // gridAliciMusteriler
            // 
            this.gridAliciMusteriler.Location = new System.Drawing.Point(221, 69);
            this.gridAliciMusteriler.MainView = this.gridViewAliciMusteriler;
            this.gridAliciMusteriler.Name = "gridAliciMusteriler";
            this.gridAliciMusteriler.Size = new System.Drawing.Size(1155, 302);
            this.gridAliciMusteriler.TabIndex = 6;
            this.gridAliciMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAliciMusteriler});
            // 
            // gridViewAliciMusteriler
            // 
            this.gridViewAliciMusteriler.GridControl = this.gridAliciMusteriler;
            this.gridViewAliciMusteriler.Name = "gridViewAliciMusteriler";
            this.gridViewAliciMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewAliciMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewAliciMusteriler_RowClick);
            // 
            // gridAliciHesaplar
            // 
            this.gridAliciHesaplar.Location = new System.Drawing.Point(221, 375);
            this.gridAliciHesaplar.MainView = this.gridViewAliciHesaplar;
            this.gridAliciHesaplar.Name = "gridAliciHesaplar";
            this.gridAliciHesaplar.Size = new System.Drawing.Size(1155, 228);
            this.gridAliciHesaplar.TabIndex = 7;
            this.gridAliciHesaplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAliciHesaplar});
            // 
            // gridViewAliciHesaplar
            // 
            this.gridViewAliciHesaplar.GridControl = this.gridAliciHesaplar;
            this.gridViewAliciHesaplar.Name = "gridViewAliciHesaplar";
            this.gridViewAliciHesaplar.OptionsView.ShowGroupPanel = false;
            this.gridViewAliciHesaplar.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewAliciHesaplar_RowClick);
            // 
            // lblAliciInfo
            // 
            this.lblAliciInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAliciInfo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblAliciInfo.Appearance.Options.UseFont = true;
            this.lblAliciInfo.Appearance.Options.UseForeColor = true;
            this.lblAliciInfo.Location = new System.Drawing.Point(221, 607);
            this.lblAliciInfo.Name = "lblAliciInfo";
            this.lblAliciInfo.Size = new System.Drawing.Size(130, 20);
            this.lblAliciInfo.StyleController = this.layoutControl1;
            this.lblAliciInfo.TabIndex = 8;
            this.lblAliciInfo.Text = "📥 Alıcı: Seçilmedi";
            // 
            // numTutar
            // 
            this.numTutar.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTutar.Location = new System.Drawing.Point(88, 676);
            this.numTutar.Name = "numTutar";
            this.numTutar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numTutar.Properties.DisplayFormat.FormatString = "N2";
            this.numTutar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numTutar.Properties.EditFormat.FormatString = "N2";
            this.numTutar.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numTutar.Properties.MaxValue = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numTutar.Size = new System.Drawing.Size(357, 20);
            this.numTutar.StyleController = this.layoutControl1;
            this.numTutar.TabIndex = 9;
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(513, 676);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Properties.NullValuePrompt = "Transfer açıklaması...";
            this.txtAciklama.Size = new System.Drawing.Size(863, 20);
            this.txtAciklama.StyleController = this.layoutControl1;
            this.txtAciklama.TabIndex = 10;
            // 
            // btnGonder
            // 
            this.btnGonder.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnGonder.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGonder.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnGonder.Appearance.Options.UseBackColor = true;
            this.btnGonder.Appearance.Options.UseFont = true;
            this.btnGonder.Appearance.Options.UseForeColor = true;
            this.btnGonder.Location = new System.Drawing.Point(12, 712);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(701, 26);
            this.btnGonder.StyleController = this.layoutControl1;
            this.btnGonder.TabIndex = 11;
            this.btnGonder.Text = "💸 HAVALE GÖNDER";
            this.btnGonder.Click += new System.EventHandler(this.BtnGonder_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.grpGonderen,
            this.grpAlici,
            this.grpTransfer,
            this.layoutItemGonder});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1400, 750);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // grpGonderen
            // 
            this.grpGonderen.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpGonderen.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpGonderen.AppearanceGroup.Options.UseFont = true;
            this.grpGonderen.AppearanceGroup.Options.UseForeColor = true;
            this.grpGonderen.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemGonderenArama,
            this.layoutItemGonderenMusteriler,
            this.layoutItemGonderenHesaplar,
            this.layoutItemGonderenInfo});
            this.grpGonderen.Location = new System.Drawing.Point(0, 0);
            this.grpGonderen.Name = "grpGonderen";
            this.grpGonderen.OptionsItemText.TextToControlDistance = 5;
            this.grpGonderen.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.grpGonderen.Size = new System.Drawing.Size(690, 631);
            this.grpGonderen.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.grpGonderen.Text = "📤 GÖNDEREN MÜŞTERİ";
            // 
            // layoutItemGonderenArama
            // 
            this.layoutItemGonderenArama.Control = this.txtGonderenArama;
            this.layoutItemGonderenArama.Location = new System.Drawing.Point(0, 0);
            this.layoutItemGonderenArama.Name = "layoutItemGonderenArama";
            this.layoutItemGonderenArama.Size = new System.Drawing.Size(656, 24);
            this.layoutItemGonderenArama.Text = "Ara:";
            this.layoutItemGonderenArama.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutItemGonderenMusteriler
            // 
            this.layoutItemGonderenMusteriler.Control = this.gridGonderenMusteriler;
            this.layoutItemGonderenMusteriler.Location = new System.Drawing.Point(0, 24);
            this.layoutItemGonderenMusteriler.Name = "layoutItemGonderenMusteriler";
            this.layoutItemGonderenMusteriler.Size = new System.Drawing.Size(656, 250);
            this.layoutItemGonderenMusteriler.TextVisible = false;
            // 
            // layoutItemGonderenHesaplar
            // 
            this.layoutItemGonderenHesaplar.Control = this.gridGonderenHesaplar;
            this.layoutItemGonderenHesaplar.Location = new System.Drawing.Point(0, 274);
            this.layoutItemGonderenHesaplar.Name = "layoutItemGonderenHesaplar";
            this.layoutItemGonderenHesaplar.Size = new System.Drawing.Size(656, 260);
            this.layoutItemGonderenHesaplar.TextVisible = false;
            // 
            // layoutItemGonderenInfo
            // 
            this.layoutItemGonderenInfo.Control = this.lblGonderenInfo;
            this.layoutItemGonderenInfo.Location = new System.Drawing.Point(0, 534);
            this.layoutItemGonderenInfo.Name = "layoutItemGonderenInfo";
            this.layoutItemGonderenInfo.Size = new System.Drawing.Size(656, 28);
            this.layoutItemGonderenInfo.TextVisible = false;
            // 
            // grpAlici
            // 
            this.grpAlici.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpAlici.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.grpAlici.AppearanceGroup.Options.UseFont = true;
            this.grpAlici.AppearanceGroup.Options.UseForeColor = true;
            this.grpAlici.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemAliciIBAN,
            this.layoutItemAliciArama,
            this.layoutItemAliciMusteriler,
            this.layoutItemAliciHesaplar,
            this.layoutItemAliciInfo});
            this.grpAlici.Location = new System.Drawing.Point(690, 0);
            this.grpAlici.Name = "grpAlici";
            this.grpAlici.OptionsItemText.TextToControlDistance = 5;
            this.grpAlici.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.grpAlici.Size = new System.Drawing.Size(690, 631);
            this.grpAlici.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.grpAlici.Text = "📥 ALICI MÜŞTERİ / IBAN";
            // 
            // layoutItemAliciIBAN
            // 
            this.layoutItemAliciIBAN.Control = this.txtAliciIBAN;
            this.layoutItemAliciIBAN.Location = new System.Drawing.Point(0, 0);
            this.layoutItemAliciIBAN.Name = "layoutItemAliciIBAN";
            this.layoutItemAliciIBAN.Size = new System.Drawing.Size(390, 24);
            this.layoutItemAliciIBAN.Text = "IBAN:";
            this.layoutItemAliciIBAN.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutItemAliciArama
            // 
            this.layoutItemAliciArama.Control = this.txtAliciArama;
            this.layoutItemAliciArama.Location = new System.Drawing.Point(390, 0);
            this.layoutItemAliciArama.Name = "layoutItemAliciArama";
            this.layoutItemAliciArama.Size = new System.Drawing.Size(266, 24);
            this.layoutItemAliciArama.Text = "Ara:";
            this.layoutItemAliciArama.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutItemAliciMusteriler
            // 
            this.layoutItemAliciMusteriler.Control = this.gridAliciMusteriler;
            this.layoutItemAliciMusteriler.Location = new System.Drawing.Point(0, 24);
            this.layoutItemAliciMusteriler.Name = "layoutItemAliciMusteriler";
            this.layoutItemAliciMusteriler.Size = new System.Drawing.Size(656, 250);
            this.layoutItemAliciMusteriler.TextVisible = false;
            // 
            // layoutItemAliciHesaplar
            // 
            this.layoutItemAliciHesaplar.Control = this.gridAliciHesaplar;
            this.layoutItemAliciHesaplar.Location = new System.Drawing.Point(0, 274);
            this.layoutItemAliciHesaplar.Name = "layoutItemAliciHesaplar";
            this.layoutItemAliciHesaplar.Size = new System.Drawing.Size(656, 260);
            this.layoutItemAliciHesaplar.TextVisible = false;
            // 
            // layoutItemAliciInfo
            // 
            this.layoutItemAliciInfo.Control = this.lblAliciInfo;
            this.layoutItemAliciInfo.Location = new System.Drawing.Point(0, 534);
            this.layoutItemAliciInfo.Name = "layoutItemAliciInfo";
            this.layoutItemAliciInfo.Size = new System.Drawing.Size(656, 28);
            this.layoutItemAliciInfo.TextVisible = false;
            // 
            // grpTransfer
            // 
            this.grpTransfer.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTransfer.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.grpTransfer.AppearanceGroup.Options.UseFont = true;
            this.grpTransfer.AppearanceGroup.Options.UseForeColor = true;
            this.grpTransfer.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemTutar,
            this.layoutItemAciklama});
            this.grpTransfer.Location = new System.Drawing.Point(0, 631);
            this.grpTransfer.Name = "grpTransfer";
            this.grpTransfer.Size = new System.Drawing.Size(1380, 69);
            this.grpTransfer.Text = "💰 TRANSFER BİLGİLERİ";
            // 
            // layoutItemTutar
            // 
            this.layoutItemTutar.Control = this.numTutar;
            this.layoutItemTutar.Location = new System.Drawing.Point(0, 0);
            this.layoutItemTutar.Name = "layoutItemTutar";
            this.layoutItemTutar.Size = new System.Drawing.Size(425, 24);
            this.layoutItemTutar.Text = "Tutar (TL):";
            this.layoutItemTutar.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutItemAciklama
            // 
            this.layoutItemAciklama.Control = this.txtAciklama;
            this.layoutItemAciklama.Location = new System.Drawing.Point(425, 0);
            this.layoutItemAciklama.Name = "layoutItemAciklama";
            this.layoutItemAciklama.Size = new System.Drawing.Size(931, 24);
            this.layoutItemAciklama.Text = "Açıklama:";
            this.layoutItemAciklama.TextSize = new System.Drawing.Size(52, 13);
            // 
            // layoutItemGonder
            // 
            this.layoutItemGonder.Control = this.btnGonder;
            this.layoutItemGonder.Location = new System.Drawing.Point(0, 700);
            this.layoutItemGonder.Name = "layoutItemGonder";
            this.layoutItemGonder.Size = new System.Drawing.Size(1380, 30);
            this.layoutItemGonder.TextVisible = false;

            // 
            // FrmHavale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmHavale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Havale İşlemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmHavale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGonderenArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGonderenMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGonderenMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGonderenHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGonderenHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliciArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliciIBAN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAliciMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAliciMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAliciHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAliciHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpGonderen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonderenInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAlici)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciIBAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAliciInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTutar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAciklama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGonder)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
