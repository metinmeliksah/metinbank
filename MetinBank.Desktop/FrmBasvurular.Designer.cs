namespace MetinBank.Desktop
{
    partial class FrmBasvurular
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        private DevExpress.XtraEditors.ComboBoxEdit cmbBasvuruTipi;
        private DevExpress.XtraEditors.GroupControl groupHesaplar;
        private DevExpress.XtraGrid.GridControl gridBasvurular;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBasvurular;
        private DevExpress.XtraEditors.SimpleButton btnYeniBasvuru;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;

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
            this.groupHesaplar = new DevExpress.XtraEditors.GroupControl();
            this.gridBasvurular = new DevExpress.XtraGrid.GridControl();
            this.gridViewBasvurular = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbBasvuruTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnYeniBasvuru = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupHesaplar)).BeginInit();
            this.groupHesaplar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBasvurular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBasvurular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBasvuruTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtMusteriArama);
            this.layoutControl1.Controls.Add(this.gridMusteriler);
            this.layoutControl1.Controls.Add(this.groupHesaplar);
            this.layoutControl1.Controls.Add(this.cmbBasvuruTipi);
            this.layoutControl1.Controls.Add(this.btnYeniBasvuru);
            this.layoutControl1.Controls.Add(this.btnYenile);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControl1.TabIndex = 0;
            // 
            // txtMusteriArama
            // 
            this.txtMusteriArama.Location = new System.Drawing.Point(87, 12);
            this.txtMusteriArama.Name = "txtMusteriArama";
            this.txtMusteriArama.Properties.NullValuePrompt = "Müşteri No, TCKN veya Ad Soyad ile ara...";
            this.txtMusteriArama.Size = new System.Drawing.Size(1101, 20);
            this.txtMusteriArama.StyleController = this.layoutControl1;
            this.txtMusteriArama.TabIndex = 0;
            this.txtMusteriArama.TextChanged += new System.EventHandler(this.TxtMusteriArama_TextChanged);
            // 
            // gridMusteriler
            // 
            this.gridMusteriler.Location = new System.Drawing.Point(12, 36);
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.Name = "gridMusteriler";
            this.gridMusteriler.Size = new System.Drawing.Size(1176, 225);
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
            this.gridViewMusteriler.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridViewMusteriler_FocusedRowChanged);
            // 
            // groupHesaplar
            // 
            this.groupHesaplar.Controls.Add(this.gridBasvurular);
            this.groupHesaplar.Location = new System.Drawing.Point(12, 265);
            this.groupHesaplar.Name = "groupHesaplar";
            this.groupHesaplar.Size = new System.Drawing.Size(1176, 371);
            this.groupHesaplar.TabIndex = 2;
            this.groupHesaplar.Text = "📋  Müşteri Hesapları - Kart için hesap seçiniz";
            // 
            // gridBasvurular
            // 
            this.gridBasvurular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBasvurular.Location = new System.Drawing.Point(2, 23);
            this.gridBasvurular.MainView = this.gridViewBasvurular;
            this.gridBasvurular.Name = "gridBasvurular";
            this.gridBasvurular.Size = new System.Drawing.Size(1172, 346);
            this.gridBasvurular.TabIndex = 0;
            this.gridBasvurular.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBasvurular});
            // 
            // gridViewBasvurular
            // 
            this.gridViewBasvurular.GridControl = this.gridBasvurular;
            this.gridViewBasvurular.Name = "gridViewBasvurular";
            this.gridViewBasvurular.OptionsView.ShowGroupPanel = false;
            // 
            // cmbBasvuruTipi
            // 
            this.cmbBasvuruTipi.Location = new System.Drawing.Point(87, 640);
            this.cmbBasvuruTipi.Name = "cmbBasvuruTipi";
            this.cmbBasvuruTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBasvuruTipi.Size = new System.Drawing.Size(313, 20);
            this.cmbBasvuruTipi.StyleController = this.layoutControl1;
            this.cmbBasvuruTipi.TabIndex = 3;
            // 
            // btnYeniBasvuru
            // 
            this.btnYeniBasvuru.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnYeniBasvuru.Appearance.Options.UseFont = true;
            this.btnYeniBasvuru.Location = new System.Drawing.Point(404, 640);
            this.btnYeniBasvuru.Name = "btnYeniBasvuru";
            this.btnYeniBasvuru.Size = new System.Drawing.Size(784, 22);
            this.btnYeniBasvuru.StyleController = this.layoutControl1;
            this.btnYeniBasvuru.TabIndex = 4;
            this.btnYeniBasvuru.Text = "💳  Kart Başvurusu Yap";
            this.btnYeniBasvuru.Click += new System.EventHandler(this.BtnYeniBasvuru_Click);
            // 
            // btnYenile
            // 
            this.btnYenile.Location = new System.Drawing.Point(12, 666);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(1176, 22);
            this.btnYenile.StyleController = this.layoutControl1;
            this.btnYenile.TabIndex = 5;
            this.btnYenile.Text = "🔄  Yenile";
            this.btnYenile.Click += new System.EventHandler(this.BtnYenile_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtMusteriArama;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1180, 24);
            this.layoutControlItem1.Text = "Müşteri Ara:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridMusteriler;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1180, 229);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.groupHesaplar;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 253);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1180, 375);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbBasvuruTipi;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 628);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(392, 26);
            this.layoutControlItem4.Text = "Kart Markası:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnYeniBasvuru;
            this.layoutControlItem5.Location = new System.Drawing.Point(392, 628);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(788, 26);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnYenile;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 654);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1180, 26);
            this.layoutControlItem6.TextVisible = false;
            // 
            // FrmBasvurular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmBasvurular";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Başvurular";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBasvurular_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupHesaplar)).EndInit();
            this.groupHesaplar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBasvurular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBasvurular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBasvuruTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
