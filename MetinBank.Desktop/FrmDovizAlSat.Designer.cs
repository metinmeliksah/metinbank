namespace MetinBank.Desktop
{
    partial class FrmDovizAlSat
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

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblGBP = new DevExpress.XtraEditors.LabelControl();
            this.lblEUR = new DevExpress.XtraEditors.LabelControl();
            this.lblUSD = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtMusteriArama = new DevExpress.XtraEditors.TextEdit();
            this.gridMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridHesaplar = new DevExpress.XtraGrid.GridControl();
            this.gridViewHesaplar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.lblOzet = new DevExpress.XtraEditors.LabelControl();
            this.cmbTRYHesap = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbDovizHesap = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbDovizCinsi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.rgIslemTipi = new DevExpress.XtraEditors.RadioGroup();
            this.numTutar = new DevExpress.XtraEditors.SpinEdit();
            this.btnIslemYap = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTRYHesap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDovizHesap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDovizCinsi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgIslemTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Controls.Add(this.groupControl2);
            this.layoutControl1.Controls.Add(this.groupControl3);
            this.layoutControl1.Controls.Add(this.btnIslemYap);
            this.layoutControl1.Controls.Add(this.btnKapat);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1200, 800);
            this.layoutControl1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblGBP);
            this.panelControl1.Controls.Add(this.lblEUR);
            this.panelControl1.Controls.Add(this.lblUSD);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1176, 60);
            this.panelControl1.TabIndex = 0;
            // 
            // lblUSD
            // 
            this.lblUSD.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUSD.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblUSD.Appearance.Options.UseFont = true;
            this.lblUSD.Appearance.Options.UseForeColor = true;
            this.lblUSD.Location = new System.Drawing.Point(20, 20);
            this.lblUSD.Name = "lblUSD";
            this.lblUSD.Size = new System.Drawing.Size(300, 19);
            this.lblUSD.TabIndex = 0;
            this.lblUSD.Text = "💵 USD: --";
            // 
            // lblEUR
            // 
            this.lblEUR.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEUR.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(137)))), ((int)(((byte)(62)))));
            this.lblEUR.Appearance.Options.UseFont = true;
            this.lblEUR.Appearance.Options.UseForeColor = true;
            this.lblEUR.Location = new System.Drawing.Point(400, 20);
            this.lblEUR.Name = "lblEUR";
            this.lblEUR.Size = new System.Drawing.Size(300, 19);
            this.lblEUR.TabIndex = 1;
            this.lblEUR.Text = "💶 EUR: --";
            // 
            // lblGBP
            // 
            this.lblGBP.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGBP.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblGBP.Appearance.Options.UseFont = true;
            this.lblGBP.Appearance.Options.UseForeColor = true;
            this.lblGBP.Location = new System.Drawing.Point(780, 20);
            this.lblGBP.Name = "lblGBP";
            this.lblGBP.Size = new System.Drawing.Size(300, 19);
            this.lblGBP.TabIndex = 2;
            this.lblGBP.Text = "💷 GBP: --";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtMusteriArama);
            this.groupControl1.Controls.Add(this.gridMusteriler);
            this.groupControl1.Location = new System.Drawing.Point(12, 76);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(576, 350);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "1️⃣ Müşteri Seçimi";
            // 
            // txtMusteriArama
            // 
            this.txtMusteriArama.Location = new System.Drawing.Point(10, 30);
            this.txtMusteriArama.Name = "txtMusteriArama";
            this.txtMusteriArama.Properties.NullValuePrompt = "🔍 Müşteri adı, TCKN veya müşteri no ile arayın...";
            this.txtMusteriArama.Size = new System.Drawing.Size(556, 20);
            this.txtMusteriArama.TabIndex = 0;
            this.txtMusteriArama.TextChanged += new System.EventHandler(this.TxtMusteriArama_TextChanged);
            // 
            // gridMusteriler
            // 
            this.gridMusteriler.Location = new System.Drawing.Point(10, 60);
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.Name = "gridMusteriler";
            this.gridMusteriler.Size = new System.Drawing.Size(556, 280);
            this.gridMusteriler.TabIndex = 1;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewMusteriler });
            // 
            // gridViewMusteriler
            // 
            this.gridViewMusteriler.GridControl = this.gridMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            this.gridViewMusteriler.OptionsBehavior.Editable = false;
            this.gridViewMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewMusteriler_RowClick);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridHesaplar);
            this.groupControl2.Location = new System.Drawing.Point(592, 76);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(596, 350);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "2️⃣ Müşteri Hesapları";
            // 
            // gridHesaplar
            // 
            this.gridHesaplar.Location = new System.Drawing.Point(10, 30);
            this.gridHesaplar.MainView = this.gridViewHesaplar;
            this.gridHesaplar.Name = "gridHesaplar";
            this.gridHesaplar.Size = new System.Drawing.Size(576, 310);
            this.gridHesaplar.TabIndex = 0;
            this.gridHesaplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewHesaplar });
            // 
            // gridViewHesaplar
            // 
            this.gridViewHesaplar.GridControl = this.gridHesaplar;
            this.gridViewHesaplar.Name = "gridViewHesaplar";
            this.gridViewHesaplar.OptionsBehavior.Editable = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.lblOzet);
            this.groupControl3.Controls.Add(this.cmbTRYHesap);
            this.groupControl3.Controls.Add(this.cmbDovizHesap);
            this.groupControl3.Controls.Add(this.cmbDovizCinsi);
            this.groupControl3.Controls.Add(this.rgIslemTipi);
            this.groupControl3.Controls.Add(this.numTutar);
            this.groupControl3.Location = new System.Drawing.Point(12, 430);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1176, 290);
            this.groupControl3.TabIndex = 3;
            this.groupControl3.Text = "3️⃣ Döviz İşlemi";
            // 
            // cmbTRYHesap
            // 
            this.cmbTRYHesap.Location = new System.Drawing.Point(20, 50);
            this.cmbTRYHesap.Name = "cmbTRYHesap";
            this.cmbTRYHesap.Properties.NullValuePrompt = "TRY Hesabı Seçin";
            this.cmbTRYHesap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbTRYHesap.Size = new System.Drawing.Size(550, 20);
            this.cmbTRYHesap.TabIndex = 0;
            // 
            // cmbDovizHesap
            // 
            this.cmbDovizHesap.Location = new System.Drawing.Point(600, 50);
            this.cmbDovizHesap.Name = "cmbDovizHesap";
            this.cmbDovizHesap.Properties.NullValuePrompt = "Döviz Hesabı Seçin";
            this.cmbDovizHesap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDovizHesap.Size = new System.Drawing.Size(550, 20);
            this.cmbDovizHesap.TabIndex = 1;
            // 
            // rgIslemTipi
            // 
            this.rgIslemTipi.Location = new System.Drawing.Point(20, 100);
            this.rgIslemTipi.Name = "rgIslemTipi";
            this.rgIslemTipi.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "💰 Döviz Al (TRY → Döviz)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "💵 Döviz Sat (Döviz → TRY)")});
            this.rgIslemTipi.Size = new System.Drawing.Size(400, 60);
            this.rgIslemTipi.TabIndex = 2;
            this.rgIslemTipi.SelectedIndexChanged += new System.EventHandler(this.RgIslemTipi_SelectedIndexChanged);
            // 
            // cmbDovizCinsi
            // 
            this.cmbDovizCinsi.Location = new System.Drawing.Point(450, 110);
            this.cmbDovizCinsi.Name = "cmbDovizCinsi";
            this.cmbDovizCinsi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDovizCinsi.Size = new System.Drawing.Size(150, 20);
            this.cmbDovizCinsi.TabIndex = 3;
            this.cmbDovizCinsi.SelectedIndexChanged += new System.EventHandler(this.CmbDovizCinsi_SelectedIndexChanged);
            // 
            // numTutar
            // 
            this.numTutar.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.numTutar.Location = new System.Drawing.Point(450, 140);
            this.numTutar.Name = "numTutar";
            this.numTutar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numTutar.Properties.DisplayFormat.FormatString = "N2";
            this.numTutar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numTutar.Properties.MaxValue = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numTutar.Size = new System.Drawing.Size(200, 20);
            this.numTutar.TabIndex = 4;
            this.numTutar.EditValueChanged += new System.EventHandler(this.NumTutar_EditValueChanged);
            // 
            // lblOzet
            // 
            this.lblOzet.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOzet.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblOzet.Appearance.Options.UseFont = true;
            this.lblOzet.Appearance.Options.UseForeColor = true;
            this.lblOzet.Location = new System.Drawing.Point(20, 190);
            this.lblOzet.Name = "lblOzet";
            this.lblOzet.Size = new System.Drawing.Size(1136, 40);
            this.lblOzet.TabIndex = 5;
            this.lblOzet.Text = "";
            // 
            // btnIslemYap
            // 
            this.btnIslemYap.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnIslemYap.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnIslemYap.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnIslemYap.Appearance.Options.UseBackColor = true;
            this.btnIslemYap.Appearance.Options.UseFont = true;
            this.btnIslemYap.Appearance.Options.UseForeColor = true;
            this.btnIslemYap.Location = new System.Drawing.Point(12, 724);
            this.btnIslemYap.Name = "btnIslemYap";
            this.btnIslemYap.Size = new System.Drawing.Size(250, 64);
            this.btnIslemYap.TabIndex = 4;
            this.btnIslemYap.Text = "✓ İşlemi Gerçekleştir";
            this.btnIslemYap.Click += new System.EventHandler(this.BtnIslemYap_Click);
            // 
            // btnKapat
            // 
            this.btnKapat.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnKapat.Appearance.Options.UseFont = true;
            this.btnKapat.Location = new System.Drawing.Point(1038, 724);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(150, 64);
            this.btnKapat.TabIndex = 5;
            this.btnKapat.Text = "✕ Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1200, 800);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1180, 64);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(580, 354);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.groupControl2;
            this.layoutControlItem3.Location = new System.Drawing.Point(580, 64);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(600, 354);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.groupControl3;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 418);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1180, 294);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnIslemYap;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 712);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(254, 68);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnKapat;
            this.layoutControlItem6.Location = new System.Drawing.Point(1026, 712);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(154, 68);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(254, 712);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(772, 68);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmDovizAlSat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmDovizAlSat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Döviz Al / Sat";
            this.Load += new System.EventHandler(this.FrmDovizAlSat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTRYHesap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDovizHesap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDovizCinsi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgIslemTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblUSD;
        private DevExpress.XtraEditors.LabelControl lblEUR;
        private DevExpress.XtraEditors.LabelControl lblGBP;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridHesaplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHesaplar;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LabelControl lblOzet;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTRYHesap;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDovizHesap;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDovizCinsi;
        private DevExpress.XtraEditors.RadioGroup rgIslemTipi;
        private DevExpress.XtraEditors.SpinEdit numTutar;
        private DevExpress.XtraEditors.SimpleButton btnIslemYap;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
