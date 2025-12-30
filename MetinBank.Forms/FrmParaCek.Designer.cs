namespace MetinBank.Forms
{
    partial class FrmParaCek
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        private DevExpress.XtraGrid.GridControl gridHesaplar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHesaplar;
        private DevExpress.XtraEditors.TextEdit txtHesapID;
        private DevExpress.XtraEditors.TextEdit txtIBAN;
        private DevExpress.XtraEditors.TextEdit txtBakiye;
        private System.Windows.Forms.NumericUpDown numTutar;
        private DevExpress.XtraEditors.MemoEdit txtAciklama;
        private DevExpress.XtraEditors.SimpleButton btnCek;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;

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
            this.txtHesapID = new DevExpress.XtraEditors.TextEdit();
            this.txtIBAN = new DevExpress.XtraEditors.TextEdit();
            this.txtBakiye = new DevExpress.XtraEditors.TextEdit();
            this.numTutar = new System.Windows.Forms.NumericUpDown();
            this.txtAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.btnCek = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHesaplar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHesapID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIBAN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBakiye.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            
            // layoutControl1
            this.layoutControl1.Controls.Add(this.txtMusteriArama);
            this.layoutControl1.Controls.Add(this.gridMusteriler);
            this.layoutControl1.Controls.Add(this.gridHesaplar);
            this.layoutControl1.Controls.Add(this.txtHesapID);
            this.layoutControl1.Controls.Add(this.txtIBAN);
            this.layoutControl1.Controls.Add(this.txtBakiye);
            this.layoutControl1.Controls.Add(this.numTutar);
            this.layoutControl1.Controls.Add(this.txtAciklama);
            this.layoutControl1.Controls.Add(this.btnCek);
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
            this.txtMusteriArama.Size = new System.Drawing.Size(1088, 20);
            this.txtMusteriArama.StyleController = this.layoutControl1;
            this.txtMusteriArama.TabIndex = 0;
            this.txtMusteriArama.TextChanged += new System.EventHandler(this.TxtMusteriArama_TextChanged);
            
            // gridMusteriler
            this.gridMusteriler.Location = new System.Drawing.Point(12, 38);
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.Name = "gridMusteriler";
            this.gridMusteriler.Size = new System.Drawing.Size(1176, 250);
            this.gridMusteriler.TabIndex = 1;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gridViewMusteriler});
            
            // gridViewMusteriler
            this.gridViewMusteriler.GridControl = this.gridMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewMusteriler_RowClick);
            
            // gridHesaplar
            this.gridHesaplar.Location = new System.Drawing.Point(12, 292);
            this.gridHesaplar.MainView = this.gridViewHesaplar;
            this.gridHesaplar.Name = "gridHesaplar";
            this.gridHesaplar.Size = new System.Drawing.Size(1176, 250);
            this.gridHesaplar.TabIndex = 2;
            this.gridHesaplar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                this.gridViewHesaplar});
            
            // gridViewHesaplar
            this.gridViewHesaplar.GridControl = this.gridHesaplar;
            this.gridViewHesaplar.Name = "gridViewHesaplar";
            this.gridViewHesaplar.OptionsView.ShowGroupPanel = false;
            this.gridViewHesaplar.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewHesaplar_RowClick);
            
            // txtHesapID
            this.txtHesapID.Location = new System.Drawing.Point(100, 546);
            this.txtHesapID.Name = "txtHesapID";
            this.txtHesapID.Properties.ReadOnly = true;
            this.txtHesapID.Size = new System.Drawing.Size(200, 20);
            this.txtHesapID.StyleController = this.layoutControl1;
            this.txtHesapID.TabIndex = 3;
            
            // txtIBAN
            this.txtIBAN.Location = new System.Drawing.Point(400, 546);
            this.txtIBAN.Name = "txtIBAN";
            this.txtIBAN.Properties.ReadOnly = true;
            this.txtIBAN.Size = new System.Drawing.Size(400, 20);
            this.txtIBAN.StyleController = this.layoutControl1;
            this.txtIBAN.TabIndex = 4;
            
            // txtBakiye
            this.txtBakiye.Location = new System.Drawing.Point(900, 546);
            this.txtBakiye.Name = "txtBakiye";
            this.txtBakiye.Properties.ReadOnly = true;
            this.txtBakiye.Size = new System.Drawing.Size(288, 20);
            this.txtBakiye.StyleController = this.layoutControl1;
            this.txtBakiye.TabIndex = 5;
            
            // numTutar
            this.numTutar.DecimalPlaces = 2;
            this.numTutar.Location = new System.Drawing.Point(100, 570);
            this.numTutar.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numTutar.Name = "numTutar";
            this.numTutar.Size = new System.Drawing.Size(200, 20);
            this.numTutar.TabIndex = 6;
            
            // txtAciklama
            this.txtAciklama.Location = new System.Drawing.Point(400, 570);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(788, 20);
            this.txtAciklama.StyleController = this.layoutControl1;
            this.txtAciklama.TabIndex = 7;
            
            // btnCek
            this.btnCek.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCek.Appearance.Options.UseFont = true;
            this.btnCek.Location = new System.Drawing.Point(12, 594);
            this.btnCek.Name = "btnCek";
            this.btnCek.Size = new System.Drawing.Size(588, 40);
            this.btnCek.StyleController = this.layoutControl1;
            this.btnCek.TabIndex = 8;
            this.btnCek.Text = "PARA ÇEK";
            this.btnCek.Click += new System.EventHandler(this.BtnCek_Click);
            
            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(604, 594);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(584, 40);
            this.btnKapat.StyleController = this.layoutControl1;
            this.btnKapat.TabIndex = 9;
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
                this.layoutControlItem7,
                this.layoutControlItem8});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControlGroup1.TextVisible = false;
            
            // layoutControlItem1
            this.layoutControlItem1.Control = this.txtMusteriArama;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1180, 26);
            this.layoutControlItem1.Text = "Müşteri Ara:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 13);
            
            // layoutControlItem2
            this.layoutControlItem2.Control = this.gridMusteriler;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1180, 254);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            
            // layoutControlItem3
            this.layoutControlItem3.Control = this.gridHesaplar;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 280);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1180, 254);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            
            // layoutControlItem4
            this.layoutControlItem4.Control = this.txtHesapID;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 534);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(288, 24);
            this.layoutControlItem4.Text = "Hesap ID:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(76, 13);
            
            // layoutControlItem5
            this.layoutControlItem5.Control = this.txtIBAN;
            this.layoutControlItem5.Location = new System.Drawing.Point(288, 534);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(488, 24);
            this.layoutControlItem5.Text = "IBAN:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(76, 13);
            
            // layoutControlItem6
            this.layoutControlItem6.Control = this.txtBakiye;
            this.layoutControlItem6.Location = new System.Drawing.Point(776, 534);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(404, 24);
            this.layoutControlItem6.Text = "Bakiye:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(76, 13);
            
            // layoutControlItem7
            this.layoutControlItem7.Control = this.numTutar;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 558);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(288, 24);
            this.layoutControlItem7.Text = "Çekilecek Tutar:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(76, 13);
            
            // layoutControlItem8
            this.layoutControlItem8.Control = this.txtAciklama;
            this.layoutControlItem8.Location = new System.Drawing.Point(288, 558);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(892, 24);
            this.layoutControlItem8.Text = "Açıklama:";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(76, 13);
            
            // FrmParaCek
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmParaCek";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Para Çek";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmParaCek_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHesaplar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHesapID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIBAN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBakiye.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
