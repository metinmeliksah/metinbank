namespace MetinBank.Desktop
{
    partial class FrmVadeliHesapAc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            
            // LEFT PANEL: Musteri Secimi
            this.groupMusteri = new DevExpress.XtraEditors.GroupControl();
            this.gridMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelMusteriTop = new DevExpress.XtraEditors.PanelControl();
            this.txtMusteriArama = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblSeciliMusteri = new DevExpress.XtraEditors.LabelControl();

            // RIGHT PANEL: Hesap İşlemleri
            this.tablePanelRight = new DevExpress.Utils.Layout.TablePanel();
            this.groupHesap = new DevExpress.XtraEditors.GroupControl();
            this.groupSonuc = new DevExpress.XtraEditors.GroupControl();

            // Hesap Detaylari
            this.txtTutar = new DevExpress.XtraEditors.TextEdit();
            this.txtGun = new DevExpress.XtraEditors.TextEdit();
            this.cmbParaBirimi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnHesapla = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            
            // Ödeme Yöntemi
            this.rgOdemeYontemi = new DevExpress.XtraEditors.RadioGroup();
            this.lblKaynakHesap = new DevExpress.XtraEditors.LabelControl();
            this.cmbKaynakHesap = new DevExpress.XtraEditors.ComboBoxEdit();

            // Sonuc Detaylari
            this.lblFaiz = new DevExpress.XtraEditors.LabelControl();
            this.lblNetKazanc = new DevExpress.XtraEditors.LabelControl();
            this.lblToplam = new DevExpress.XtraEditors.LabelControl();
            this.btnHesapAc = new DevExpress.XtraEditors.SimpleButton();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupMusteri)).BeginInit();
            this.groupMusteri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMusteriTop)).BeginInit();
            this.panelMusteriTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanelRight)).BeginInit();
            this.tablePanelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupHesap)).BeginInit();
            this.groupHesap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupSonuc)).BeginInit();
            this.groupSonuc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTutar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGun.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbParaBirimi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgOdemeYontemi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKaynakHesap.Properties)).BeginInit();
            this.SuspendLayout();

            // Split Container
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            // Splittable property removed
            this.splitContainerControl1.SplitterPosition = 450;
            this.splitContainerControl1.Panel1.Controls.Add(this.groupMusteri);
            this.splitContainerControl1.Panel2.Controls.Add(this.tablePanelRight);
            
            // Group Musteri
            this.groupMusteri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupMusteri.Text = "Müşteri Seçimi";
            this.groupMusteri.Controls.Add(this.gridMusteriler);
            this.groupMusteri.Controls.Add(this.panelMusteriTop);

            // Panel Musteri Top
            this.panelMusteriTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMusteriTop.Height = 100;
            this.panelMusteriTop.Controls.Add(this.txtMusteriArama);
            this.panelMusteriTop.Controls.Add(this.labelControl1);
            this.panelMusteriTop.Controls.Add(this.lblSeciliMusteri);

            this.labelControl1.Text = "Müşteri Ara:";
            this.labelControl1.Location = new System.Drawing.Point(10, 15);
            this.txtMusteriArama.Location = new System.Drawing.Point(10, 35);
            this.txtMusteriArama.Size = new System.Drawing.Size(300, 20);

            this.lblSeciliMusteri.Text = "Seçili: -";
            this.lblSeciliMusteri.Location = new System.Drawing.Point(10, 70);
            this.lblSeciliMusteri.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSeciliMusteri.Appearance.ForeColor = System.Drawing.Color.Green;

            // Grid
            this.gridMusteriler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewMusteriler });
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.OptionsBehavior.Editable = false;

            // Table Panel Right
            this.tablePanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelRight.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
                new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F) });
            this.tablePanelRight.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
                new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 320F),
                new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F) });
            
            this.tablePanelRight.Controls.Add(this.groupHesap);
            this.tablePanelRight.SetRow(this.groupHesap, 0);
            this.tablePanelRight.SetColumn(this.groupHesap, 0);
            
            this.tablePanelRight.Controls.Add(this.groupSonuc);
            this.tablePanelRight.SetRow(this.groupSonuc, 1);
            this.tablePanelRight.SetColumn(this.groupSonuc, 0);

            // Group Hesap (Ust)
            this.groupHesap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupHesap.Text = "Vadeli Hesap Parametreleri";
            this.groupHesap.Controls.Add(this.txtTutar);
            this.groupHesap.Controls.Add(this.labelControl2);
            this.groupHesap.Controls.Add(this.txtGun);
            this.groupHesap.Controls.Add(this.labelControl3);
            this.groupHesap.Controls.Add(this.cmbParaBirimi);
            this.groupHesap.Controls.Add(this.labelControl4);
            this.groupHesap.Controls.Add(this.rgOdemeYontemi);
            this.groupHesap.Controls.Add(this.lblKaynakHesap);
            this.groupHesap.Controls.Add(this.cmbKaynakHesap);
            this.groupHesap.Controls.Add(this.btnHesapla);

            this.labelControl2.Text = "Yatırılacak Tutar:";
            this.labelControl2.Location = new System.Drawing.Point(30, 40);
            this.txtTutar.Location = new System.Drawing.Point(150, 37);
            this.txtTutar.Size = new System.Drawing.Size(200, 20);
            this.txtTutar.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtTutar.Properties.MaskSettings.Set("mask", "n2");

            this.labelControl3.Text = "Vade (Gün):";
            this.labelControl3.Location = new System.Drawing.Point(30, 80);
            this.txtGun.Location = new System.Drawing.Point(150, 77);
            this.txtGun.Size = new System.Drawing.Size(200, 20);
            this.txtGun.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtGun.Properties.MaskSettings.Set("mask", "d");

            this.labelControl4.Text = "Para Birimi:";
            this.labelControl4.Location = new System.Drawing.Point(30, 120);
            this.cmbParaBirimi.Location = new System.Drawing.Point(150, 117);
            this.cmbParaBirimi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbParaBirimi.Properties.Items.AddRange(new object[] { "TL", "USD", "EUR", "GBP" });
            this.cmbParaBirimi.SelectedIndex = 0;
            this.cmbParaBirimi.Size = new System.Drawing.Size(200, 20);

            this.btnHesapla.Text = "Getiri Hesapla";
            this.btnHesapla.Location = new System.Drawing.Point(150, 240);
            this.btnHesapla.Size = new System.Drawing.Size(200, 40);
            this.btnHesapla.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);

            // Ödeme Yöntemi RadioGroup
            this.rgOdemeYontemi.Location = new System.Drawing.Point(30, 160);
            this.rgOdemeYontemi.Name = "rgOdemeYontemi";
            this.rgOdemeYontemi.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "💵 Nakit Yatırma"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "🔄 Vadesiz Hesaptan Virman")});
            this.rgOdemeYontemi.Size = new System.Drawing.Size(320, 30);
            this.rgOdemeYontemi.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Near;

            // Kaynak Hesap Label ve ComboBox
            this.lblKaynakHesap.Text = "Kaynak Hesap:";
            this.lblKaynakHesap.Location = new System.Drawing.Point(30, 200);
            this.lblKaynakHesap.Visible = false;
            
            this.cmbKaynakHesap.Location = new System.Drawing.Point(150, 197);
            this.cmbKaynakHesap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbKaynakHesap.Size = new System.Drawing.Size(350, 20);
            this.cmbKaynakHesap.Visible = false;

            // Group Sonuc (Alt)
            this.groupSonuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSonuc.Text = "Tahmini Kazanç Tablosu";
            this.groupSonuc.Visible = false;
            this.groupSonuc.Controls.Add(this.lblFaiz);
            this.groupSonuc.Controls.Add(this.lblNetKazanc);
            this.groupSonuc.Controls.Add(this.lblToplam);
            this.groupSonuc.Controls.Add(this.btnHesapAc);

            this.lblFaiz.Text = "Faiz Oranı: %0.00";
            this.lblFaiz.Location = new System.Drawing.Point(30, 50);
            this.lblFaiz.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);

            this.lblNetKazanc.Text = "Net Kazanç: 0.00 TL";
            this.lblNetKazanc.Location = new System.Drawing.Point(30, 90);
            this.lblNetKazanc.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblNetKazanc.Appearance.ForeColor = System.Drawing.Color.Green;

            this.lblToplam.Text = "Vade Sonu Toplam: 0.00 TL";
            this.lblToplam.Location = new System.Drawing.Point(30, 130);
            this.lblToplam.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);

            this.btnHesapAc.Text = "VADELİ HESABI ONAYLA VE AÇ";
            this.btnHesapAc.Location = new System.Drawing.Point(30, 180);
            this.btnHesapAc.Size = new System.Drawing.Size(320, 50);
            this.btnHesapAc.Appearance.BackColor = System.Drawing.Color.Green;
            this.btnHesapAc.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnHesapAc.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnHesapAc.Appearance.Options.UseBackColor = true;
            this.btnHesapAc.Appearance.Options.UseForeColor = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmVadeliHesapAc";
            this.Text = "Vadeli Hesap Oluştur";

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupMusteri)).EndInit();
            this.groupMusteri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMusteriTop)).EndInit();
            this.panelMusteriTop.ResumeLayout(false);
            this.panelMusteriTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanelRight)).EndInit();
            this.tablePanelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupHesap)).EndInit();
            this.groupHesap.ResumeLayout(false);
            this.groupHesap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupSonuc)).EndInit();
            this.groupSonuc.ResumeLayout(false);
            this.groupSonuc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTutar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGun.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbParaBirimi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgOdemeYontemi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKaynakHesap.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupMusteri;
        private DevExpress.XtraGrid.GridControl gridMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        private DevExpress.XtraEditors.PanelControl panelMusteriTop;
        private DevExpress.XtraEditors.TextEdit txtMusteriArama;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblSeciliMusteri;
        
        private DevExpress.Utils.Layout.TablePanel tablePanelRight;
        private DevExpress.XtraEditors.GroupControl groupHesap;
        private DevExpress.XtraEditors.TextEdit txtTutar;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtGun;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbParaBirimi;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnHesapla;
        
        private DevExpress.XtraEditors.GroupControl groupSonuc;
        private DevExpress.XtraEditors.LabelControl lblNetKazanc;
        private DevExpress.XtraEditors.LabelControl lblFaiz;
        private DevExpress.XtraEditors.LabelControl lblToplam;
        private DevExpress.XtraEditors.SimpleButton btnHesapAc;
        
        private DevExpress.XtraEditors.RadioGroup rgOdemeYontemi;
        private DevExpress.XtraEditors.LabelControl lblKaynakHesap;
        private DevExpress.XtraEditors.ComboBoxEdit cmbKaynakHesap;
    }
}
