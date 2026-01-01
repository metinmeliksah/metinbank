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
        private DevExpress.XtraGrid.GridControl gridBasvurular;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBasvurular;
        private DevExpress.XtraEditors.SimpleButton btnYeniBasvuru;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;

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
            this.cmbBasvuruTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridBasvurular = new DevExpress.XtraGrid.GridControl();
            this.gridViewBasvurular = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnYeniBasvuru = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBasvuruTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBasvurular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBasvurular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            
            // layoutControl1
            this.layoutControl1.Controls.Add(this.txtMusteriArama);
            this.layoutControl1.Controls.Add(this.gridMusteriler);
            this.layoutControl1.Controls.Add(this.cmbBasvuruTipi);
            this.layoutControl1.Controls.Add(this.gridBasvurular);
            this.layoutControl1.Controls.Add(this.btnYeniBasvuru);
            this.layoutControl1.Controls.Add(this.btnYenile);
            this.layoutControl1.Controls.Add(this.btnKapat);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControl1.TabIndex = 0;
            
            // txtMusteriArama
            this.txtMusteriArama.Location = new System.Drawing.Point(100, 12);
            this.txtMusteriArama.Name = "txtMusteriArama";
            this.txtMusteriArama.Properties.NullValuePrompt = "Müşteri No, TCKN veya Ad Soyad ile ara...";
            this.txtMusteriArama.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMusteriArama.Size = new System.Drawing.Size(988, 20);
            this.txtMusteriArama.StyleController = this.layoutControl1;
            this.txtMusteriArama.TabIndex = 0;
            this.txtMusteriArama.TextChanged += new System.EventHandler(this.TxtMusteriArama_TextChanged);
            
            // gridMusteriler
            this.gridMusteriler.Location = new System.Drawing.Point(12, 38);
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.Name = "gridMusteriler";
            this.gridMusteriler.Size = new System.Drawing.Size(1176, 200);
            this.gridMusteriler.TabIndex = 1;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gridViewMusteriler});
            
            // gridViewMusteriler
            this.gridViewMusteriler.GridControl = this.gridMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewMusteriler_RowClick);
            
            // cmbBasvuruTipi
            this.cmbBasvuruTipi.Location = new System.Drawing.Point(100, 242);
            this.cmbBasvuruTipi.Name = "cmbBasvuruTipi";
            this.cmbBasvuruTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBasvuruTipi.Size = new System.Drawing.Size(200, 20);
            this.cmbBasvuruTipi.StyleController = this.layoutControl1;
            this.cmbBasvuruTipi.TabIndex = 2;
            
            // gridBasvurular
            this.gridBasvurular.Location = new System.Drawing.Point(12, 266);
            this.gridBasvurular.MainView = this.gridViewBasvurular;
            this.gridBasvurular.Name = "gridBasvurular";
            this.gridBasvurular.Size = new System.Drawing.Size(1176, 350);
            this.gridBasvurular.TabIndex = 3;
            this.gridBasvurular.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gridViewBasvurular});
            
            // gridViewBasvurular
            this.gridViewBasvurular.GridControl = this.gridBasvurular;
            this.gridViewBasvurular.Name = "gridViewBasvurular";
            this.gridViewBasvurular.OptionsView.ShowGroupPanel = false;
            
            // btnYeniBasvuru
            this.btnYeniBasvuru.Location = new System.Drawing.Point(400, 242);
            this.btnYeniBasvuru.Name = "btnYeniBasvuru";
            this.btnYeniBasvuru.Size = new System.Drawing.Size(200, 22);
            this.btnYeniBasvuru.StyleController = this.layoutControl1;
            this.btnYeniBasvuru.TabIndex = 4;
            this.btnYeniBasvuru.Text = "Yeni Başvuru";
            this.btnYeniBasvuru.Click += new System.EventHandler(this.BtnYeniBasvuru_Click);
            
            // btnYenile
            this.btnYenile.Location = new System.Drawing.Point(12, 620);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(588, 30);
            this.btnYenile.StyleController = this.layoutControl1;
            this.btnYenile.TabIndex = 5;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.BtnYenile_Click);
            
            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(604, 620);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(584, 30);
            this.btnKapat.StyleController = this.layoutControl1;
            this.btnKapat.TabIndex = 6;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            // layoutControlGroup1
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutControlItem1,
                this.layoutControlItem2,
                this.layoutControlItem3,
                this.layoutControlItem4,
                this.layoutControlItem5,
                this.layoutControlItem6,
                this.layoutControlItem7});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControlGroup1.TextVisible = false;
            
            // layoutControlItem1
            this.layoutControlItem1.Control = this.txtMusteriArama;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1080, 26);
            this.layoutControlItem1.Text = "Müşteri Ara:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 13);
            
            // layoutControlItem2
            this.layoutControlItem2.Control = this.gridMusteriler;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1180, 204);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            
            // layoutControlItem3
            this.layoutControlItem3.Control = this.cmbBasvuruTipi;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 230);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(288, 24);
            this.layoutControlItem3.Text = "Başvuru Tipi:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(76, 13);
            
            // layoutControlItem4
            this.layoutControlItem4.Control = this.btnYeniBasvuru;
            this.layoutControlItem4.Location = new System.Drawing.Point(288, 230);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(204, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            
            // layoutControlItem5
            this.layoutControlItem5.Control = this.gridBasvurular;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 254);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1180, 354);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            
            // layoutControlItem6
            this.layoutControlItem6.Control = this.btnYenile;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 608);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(592, 34);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            
            // layoutControlItem7
            this.layoutControlItem7.Control = this.btnKapat;
            this.layoutControlItem7.Location = new System.Drawing.Point(592, 608);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(588, 34);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            
            // FrmBasvurular
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
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBasvuruTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBasvurular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBasvurular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
