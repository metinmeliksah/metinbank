namespace MetinBank.Desktop
{
    partial class FrmKrediBasvuru
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain)); // Main'den ikon alabiliriz veya standart
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            
            // Refactored: Customer Search Panel
            this.groupMusteri = new DevExpress.XtraEditors.GroupControl();
            this.gridMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelMusteriArama = new DevExpress.XtraEditors.PanelControl();
            this.txtMusteriArama = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblSeciliMusteri = new DevExpress.XtraEditors.LabelControl();

            // Refactored: Main Action Panel
            this.tablePanelMain = new DevExpress.Utils.Layout.TablePanel();
            this.groupKrediBilgi = new DevExpress.XtraEditors.GroupControl();
            this.groupOdemePlani = new DevExpress.XtraEditors.GroupControl();
            
            // Kredi Bilgileri Controls
            this.txtTutar = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtVade = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtGelir = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnHesapla = new DevExpress.XtraEditors.SimpleButton();
            
            // Sonuç / Grid Controls
            this.gridOdemePlani = new DevExpress.XtraGrid.GridControl();
            this.gridViewOdemePlani = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelSonucOzet = new DevExpress.XtraEditors.PanelControl();
            this.lblAylikTaksit = new DevExpress.XtraEditors.LabelControl();
            this.lblToplamOdeme = new DevExpress.XtraEditors.LabelControl();
            this.btnBasvur = new DevExpress.XtraEditors.SimpleButton();
            
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupMusteri)).BeginInit();
            this.groupMusteri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMusteriArama)).BeginInit();
            this.panelMusteriArama.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanelMain)).BeginInit();
            this.tablePanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupKrediBilgi)).BeginInit();
            this.groupKrediBilgi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupOdemePlani)).BeginInit();
            this.groupOdemePlani.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTutar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGelir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOdemePlani)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOdemePlani)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSonucOzet)).BeginInit();
            this.panelSonucOzet.SuspendLayout();
            this.SuspendLayout();

            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupMusteri);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.tablePanelMain);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1200, 700);
            this.splitContainerControl1.SplitterPosition = 350;
            this.splitContainerControl1.TabIndex = 0;

            // 
            // groupMusteri (SOL PANEL)
            // 
            this.groupMusteri.Controls.Add(this.gridMusteriler);
            this.groupMusteri.Controls.Add(this.panelMusteriArama);
            this.groupMusteri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupMusteri.Text = "Müşteri Seçimi";
            this.groupMusteri.CaptionImageOptions.SvgImage = null; // Adding icons manually is tricky without resources

            // panelMusteriArama
            this.panelMusteriArama.Controls.Add(this.lblSeciliMusteri);
            this.panelMusteriArama.Controls.Add(this.txtMusteriArama);
            this.panelMusteriArama.Controls.Add(this.labelControl1);
            this.panelMusteriArama.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMusteriArama.Height = 100;
            
            this.labelControl1.Location = new System.Drawing.Point(10, 15);
            this.labelControl1.Text = "Müşteri Ara (Ad/TC/No):";
            
            this.txtMusteriArama.Location = new System.Drawing.Point(10, 35);
            this.txtMusteriArama.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMusteriArama.Properties.Appearance.Options.UseFont = true;
            this.txtMusteriArama.Size = new System.Drawing.Size(320, 24);

            this.lblSeciliMusteri.Location = new System.Drawing.Point(10, 70);
            this.lblSeciliMusteri.Text = "Seçili: -";
            this.lblSeciliMusteri.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSeciliMusteri.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

            // gridMusteriler
            this.gridMusteriler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewMusteriler });

            this.gridViewMusteriler.GridControl = this.gridMusteriler;
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.OptionsBehavior.Editable = false;

            // 
            // tablePanelMain (SAG PANEL)
            // 
            this.tablePanelMain.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
                new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F)
            });
            this.tablePanelMain.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
                new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 200F),
                new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F)
            });
            this.tablePanelMain.Controls.Add(this.groupKrediBilgi);
            this.tablePanelMain.Controls.Add(this.groupOdemePlani);
            this.tablePanelMain.Dock = System.Windows.Forms.DockStyle.Fill;

            // 
            // groupKrediBilgi (UST KISIM)
            // 
            this.tablePanelMain.SetColumn(this.groupKrediBilgi, 0);
            this.tablePanelMain.SetRow(this.groupKrediBilgi, 0);
            this.groupKrediBilgi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupKrediBilgi.Text = "Kredi Hesaplama Kriterleri";
            
            this.groupKrediBilgi.Controls.Add(this.labelControl2); // Tutar Lbl
            this.groupKrediBilgi.Controls.Add(this.txtTutar);
            this.groupKrediBilgi.Controls.Add(this.labelControl3); // Vade Lbl
            this.groupKrediBilgi.Controls.Add(this.txtVade);
            this.groupKrediBilgi.Controls.Add(this.labelControl5); // Gelir Lbl
            this.groupKrediBilgi.Controls.Add(this.txtGelir);
            this.groupKrediBilgi.Controls.Add(this.btnHesapla);

            int startY = 35;
            int gapY = 30;
            int col2X = 300;
            
            this.labelControl2.Text = "İstenen Tutar (TL):";
            this.labelControl2.Location = new System.Drawing.Point(20, startY);
            
            this.txtTutar.Location = new System.Drawing.Point(120, startY - 3);
            this.txtTutar.Size = new System.Drawing.Size(150, 20);
            this.txtTutar.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtTutar.Properties.MaskSettings.Set("mask", "n2");

            this.labelControl3.Text = "Vade (Ay):";
            this.labelControl3.Location = new System.Drawing.Point(300, startY);
            
            this.txtVade.Location = new System.Drawing.Point(360, startY - 3);
            this.txtVade.Size = new System.Drawing.Size(100, 20);
            this.txtVade.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtVade.Properties.Items.AddRange(new object[] { "12", "24", "36" });

            this.labelControl5.Text = "Aylık Gelir (TL):";
            this.labelControl5.Location = new System.Drawing.Point(500, startY);

            this.txtGelir.Location = new System.Drawing.Point(580, startY - 3);
            this.txtGelir.Size = new System.Drawing.Size(150, 20);
            this.txtGelir.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtGelir.Properties.MaskSettings.Set("mask", "n2");

            this.btnHesapla.Text = "Ödeme Planı Hesapla";
            this.btnHesapla.Location = new System.Drawing.Point(20, startY + 50);
            this.btnHesapla.Size = new System.Drawing.Size(710, 40);
            this.btnHesapla.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);


            // 
            // groupOdemePlani (ALT KISIM)
            // 
            this.tablePanelMain.SetColumn(this.groupOdemePlani, 0);
            this.tablePanelMain.SetRow(this.groupOdemePlani, 1);
            this.groupOdemePlani.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupOdemePlani.Text = "Ödeme Planı ve Onay";
            this.groupOdemePlani.Controls.Add(this.gridOdemePlani);
            this.groupOdemePlani.Controls.Add(this.panelSonucOzet);

            // panelSonucOzet (Sag taraf veya Alt taraf) -> Dock Bottom yapalım
            this.panelSonucOzet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSonucOzet.Height = 80;
            this.panelSonucOzet.Controls.Add(this.lblAylikTaksit);
            this.panelSonucOzet.Controls.Add(this.lblToplamOdeme);
            this.panelSonucOzet.Controls.Add(this.btnBasvur);

            this.lblAylikTaksit.Text = "Taksit: 0,00 TL";
            this.lblAylikTaksit.Location = new System.Drawing.Point(20, 20);
            this.lblAylikTaksit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblAylikTaksit.Appearance.ForeColor = System.Drawing.Color.Blue;

            this.lblToplamOdeme.Text = "Toplam Geri Ödeme: 0,00 TL";
            this.lblToplamOdeme.Location = new System.Drawing.Point(20, 50);

            this.btnBasvur.Text = "KREDİ BAŞVURUSUNU TAMAMLA";
            this.btnBasvur.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBasvur.Width = 250;
            this.btnBasvur.Appearance.BackColor = System.Drawing.Color.Green;
            this.btnBasvur.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnBasvur.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnBasvur.Appearance.Options.UseBackColor = true;
            this.btnBasvur.Appearance.Options.UseForeColor = true;

            // gridOdemePlani
            this.gridOdemePlani.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOdemePlani.MainView = this.gridViewOdemePlani;
            this.gridOdemePlani.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewOdemePlani });
            
            this.gridViewOdemePlani.OptionsView.ShowGroupPanel = false;
            this.gridViewOdemePlani.OptionsBehavior.Editable = false;

            // Formatlama
            this.gridViewOdemePlani.FormatConditions.Clear();
            this.gridViewOdemePlani.RowHeight = 25;
            this.gridViewOdemePlani.ViewCaption = "Ödeme Planı";
            
            // Kod tarafında sütunlar otomatik oluşacağı için, DataSource değiştiğinde formatı ayarlamak daha iyi.
            // Ancak InitializeComponent'te olduğumuz için olay ekleyebiliriz.
            this.gridViewOdemePlani.DataSourceChanged += (s, e) => {
                var view = s as DevExpress.XtraGrid.Views.Grid.GridView;
                if(view.Columns["TaksitTutari"] != null) view.Columns["TaksitTutari"].DisplayFormat.FormatString = "n2";
                if(view.Columns["TaksitTutari"] != null) view.Columns["TaksitTutari"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                
                if(view.Columns["AnaPara"] != null) view.Columns["AnaPara"].DisplayFormat.FormatString = "n2";
                if(view.Columns["AnaPara"] != null) view.Columns["AnaPara"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                if(view.Columns["Faiz"] != null) view.Columns["Faiz"].DisplayFormat.FormatString = "n2";
                if(view.Columns["Faiz"] != null) view.Columns["Faiz"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                if(view.Columns["KalanAnaPara"] != null) view.Columns["KalanAnaPara"].DisplayFormat.FormatString = "n2";
                if(view.Columns["KalanAnaPara"] != null) view.Columns["KalanAnaPara"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                
                if(view.Columns["Tarih"] != null) view.Columns["Tarih"].DisplayFormat.FormatString = "dd.MM.yyyy";
                if(view.Columns["Tarih"] != null) view.Columns["Tarih"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                
                view.BestFitColumns();
            };


            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmKrediBasvuru";
            this.Text = "Kredi Başvuru ve Hesaplama Ekranı";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupMusteri)).EndInit();
            this.groupMusteri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMusteriArama)).EndInit();
            this.panelMusteriArama.ResumeLayout(false);
            this.panelMusteriArama.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanelMain)).EndInit();
            this.tablePanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupKrediBilgi)).EndInit();
            this.groupKrediBilgi.ResumeLayout(false);
            this.groupKrediBilgi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupOdemePlani)).EndInit();
            this.groupOdemePlani.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTutar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGelir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOdemePlani)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOdemePlani)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSonucOzet)).EndInit();
            this.panelSonucOzet.ResumeLayout(false);
            this.panelSonucOzet.PerformLayout();
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupMusteri;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        private DevExpress.XtraEditors.PanelControl panelMusteriArama;
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblSeciliMusteri;
        private DevExpress.Utils.Layout.TablePanel tablePanelMain;
        private DevExpress.XtraEditors.GroupControl groupKrediBilgi;
        private DevExpress.XtraEditors.GroupControl groupOdemePlani;
        private DevExpress.XtraEditors.SimpleButton btnHesapla;
        private DevExpress.XtraEditors.TextEdit txtGelir;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit txtVade;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTutar;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridOdemePlani;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOdemePlani;
        private DevExpress.XtraEditors.PanelControl panelSonucOzet;
        private DevExpress.XtraEditors.LabelControl lblToplamOdeme;
        private DevExpress.XtraEditors.LabelControl lblAylikTaksit;
        private DevExpress.XtraEditors.SimpleButton btnBasvur;
    }
}
