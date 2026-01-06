namespace MetinBank.Desktop
{
    partial class FrmVadesizHesapAc
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
            
            // LEFT: Musteri
            this.groupMusteri = new DevExpress.XtraEditors.GroupControl();
            this.gridMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelMusteriTop = new DevExpress.XtraEditors.PanelControl();
            this.txtMusteriArama = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblSeciliMusteri = new DevExpress.XtraEditors.LabelControl();

            // RIGHT: Islem
            this.groupIslem = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbParaBirimi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnHesapAc = new DevExpress.XtraEditors.SimpleButton();
            this.labelBilgi = new DevExpress.XtraEditors.LabelControl();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupMusteri)).BeginInit();
            this.groupMusteri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelMusteriTop)).BeginInit();
            this.panelMusteriTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupIslem)).BeginInit();
            this.groupIslem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbParaBirimi.Properties)).BeginInit();
            this.SuspendLayout();

            // Spilt
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.SplitterPosition = 350;
            this.splitContainerControl1.Panel1.Controls.Add(this.groupMusteri);
            this.splitContainerControl1.Panel2.Controls.Add(this.groupIslem);

            // Group Musteri
            this.groupMusteri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupMusteri.Text = "Müşteri Seçimi";
            this.groupMusteri.Controls.Add(this.gridMusteriler);
            this.groupMusteri.Controls.Add(this.panelMusteriTop);

            // Panel Top
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
            this.lblSeciliMusteri.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblSeciliMusteri.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

            // Grid
            this.gridMusteriler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMusteriler.MainView = this.gridViewMusteriler;
            this.gridMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridViewMusteriler });
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.OptionsBehavior.Editable = false;

            // Group Islem
            this.groupIslem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupIslem.Text = "Vadesiz Hesap Açılış";
            this.groupIslem.Controls.Add(this.labelControl2);
            this.groupIslem.Controls.Add(this.cmbParaBirimi);
            this.groupIslem.Controls.Add(this.btnHesapAc);
            this.groupIslem.Controls.Add(this.labelBilgi);

            this.labelControl2.Text = "Para Birimi:";
            this.labelControl2.Location = new System.Drawing.Point(50, 60);

            this.cmbParaBirimi.Location = new System.Drawing.Point(150, 57);
            this.cmbParaBirimi.Properties.Items.AddRange(new object[] { "TL", "USD", "EUR", "GAU" });
            this.cmbParaBirimi.SelectedIndex = 0;
            this.cmbParaBirimi.Size = new System.Drawing.Size(200, 20);

            this.labelBilgi.Text = "Uyarı: Vadesiz hesap açılışı ücretsizdir ve anında aktif olur.\nHerhangi bir alt limit yoktur.";
            this.labelBilgi.Location = new System.Drawing.Point(50, 110);
            this.labelBilgi.Appearance.ForeColor = System.Drawing.Color.Gray;

            this.btnHesapAc.Text = "VADESİZ HESAP AÇ";
            this.btnHesapAc.Location = new System.Drawing.Point(50, 160);
            this.btnHesapAc.Size = new System.Drawing.Size(300, 50);
            this.btnHesapAc.Appearance.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnHesapAc.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnHesapAc.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnHesapAc.Appearance.Options.UseBackColor = true;
            this.btnHesapAc.Appearance.Options.UseForeColor = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmVadesizHesapAc";
            this.Text = "Vadesiz Hesap Açılışı";

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
            ((System.ComponentModel.ISupportInitialize)(this.groupIslem)).EndInit();
            this.groupIslem.ResumeLayout(false);
            this.groupIslem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbParaBirimi.Properties)).EndInit();
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
        private DevExpress.XtraEditors.GroupControl groupIslem;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbParaBirimi;
        private DevExpress.XtraEditors.SimpleButton btnHesapAc;
        private DevExpress.XtraEditors.LabelControl labelBilgi;
    }
}
