namespace MetinBank.Forms
{
    partial class FrmMusteriIslem
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.TextEdit txtArama;
        private DevExpress.XtraEditors.SimpleButton btnAra;
        private DevExpress.XtraEditors.SimpleButton btnYeniMusteri;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraGrid.GridControl dgvMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;

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
            this.txtArama = new DevExpress.XtraEditors.TextEdit();
            this.btnAra = new DevExpress.XtraEditors.SimpleButton();
            this.btnYeniMusteri = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.dgvMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            this.SuspendLayout();
            
            this.txtArama.Location = new System.Drawing.Point(20, 20);
            this.txtArama.Name = "txtArama";
            this.txtArama.Size = new System.Drawing.Size(300, 20);
            this.txtArama.TabIndex = 0;
            
            this.btnAra.Location = new System.Drawing.Point(330, 18);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(100, 25);
            this.btnAra.TabIndex = 1;
            this.btnAra.Text = "Ara";
            this.btnAra.Click += new System.EventHandler(this.BtnAra_Click);
            
            this.btnYeniMusteri.Location = new System.Drawing.Point(440, 18);
            this.btnYeniMusteri.Name = "btnYeniMusteri";
            this.btnYeniMusteri.Size = new System.Drawing.Size(100, 25);
            this.btnYeniMusteri.TabIndex = 2;
            this.btnYeniMusteri.Text = "Yeni Müşteri";
            this.btnYeniMusteri.Click += new System.EventHandler(this.BtnYeniMusteri_Click);
            
            this.btnKapat.Location = new System.Drawing.Point(550, 18);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(100, 25);
            this.btnKapat.TabIndex = 3;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            this.dgvMusteriler.Location = new System.Drawing.Point(20, 60);
            this.dgvMusteriler.MainView = this.gridViewMusteriler;
            this.dgvMusteriler.Name = "dgvMusteriler";
            this.dgvMusteriler.Size = new System.Drawing.Size(630, 330);
            this.dgvMusteriler.TabIndex = 4;
            this.dgvMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMusteriler});
            
            this.gridViewMusteriler.GridControl = this.dgvMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            
            this.ClientSize = new System.Drawing.Size(670, 410);
            this.Controls.Add(this.dgvMusteriler);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnYeniMusteri);
            this.Controls.Add(this.btnAra);
            this.Controls.Add(this.txtArama);
            this.Name = "FrmMusteriIslem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Müşteri İşlemleri";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMusteriIslem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
