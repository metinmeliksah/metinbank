namespace MetinBank.Forms
{
    partial class FrmOnayBekleyenler
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraGrid.GridControl gridOnaylar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOnaylar;
        private System.Windows.Forms.DataGridView dgvOnaylar;
        private DevExpress.XtraEditors.SimpleButton btnOnayla;
        private DevExpress.XtraEditors.SimpleButton btnReddet;
        private DevExpress.XtraEditors.SimpleButton btnKapat;

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
            this.gridOnaylar = new DevExpress.XtraGrid.GridControl();
            this.gridViewOnaylar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dgvOnaylar = new System.Windows.Forms.DataGridView();
            this.btnOnayla = new DevExpress.XtraEditors.SimpleButton();
            this.btnReddet = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridOnaylar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnaylar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOnaylar)).BeginInit();
            this.SuspendLayout();
            
            this.gridOnaylar.Location = new System.Drawing.Point(20, 20);
            this.gridOnaylar.MainView = this.gridViewOnaylar;
            this.gridOnaylar.Name = "gridOnaylar";
            this.gridOnaylar.Size = new System.Drawing.Size(730, 330);
            this.gridOnaylar.TabIndex = 0;
            this.gridOnaylar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOnaylar});
            
            this.gridViewOnaylar.GridControl = this.gridOnaylar;
            this.gridViewOnaylar.Name = "gridViewOnaylar";
            
            this.dgvOnaylar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOnaylar.Location = new System.Drawing.Point(20, 20);
            this.dgvOnaylar.Name = "dgvOnaylar";
            this.dgvOnaylar.Size = new System.Drawing.Size(730, 330);
            this.dgvOnaylar.TabIndex = 1;
            this.dgvOnaylar.Visible = false;
            
            this.btnOnayla.Location = new System.Drawing.Point(20, 370);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(120, 30);
            this.btnOnayla.TabIndex = 2;
            this.btnOnayla.Text = "Onayla";
            this.btnOnayla.Click += new System.EventHandler(this.BtnOnayla_Click);
            
            this.btnReddet.Location = new System.Drawing.Point(160, 370);
            this.btnReddet.Name = "btnReddet";
            this.btnReddet.Size = new System.Drawing.Size(120, 30);
            this.btnReddet.TabIndex = 3;
            this.btnReddet.Text = "Reddet";
            this.btnReddet.Click += new System.EventHandler(this.BtnReddet_Click);
            
            this.btnKapat.Location = new System.Drawing.Point(630, 370);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(120, 30);
            this.btnKapat.TabIndex = 4;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            this.ClientSize = new System.Drawing.Size(770, 420);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnReddet);
            this.Controls.Add(this.btnOnayla);
            this.Controls.Add(this.dgvOnaylar);
            this.Controls.Add(this.gridOnaylar);
            this.Name = "FrmOnayBekleyenler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Onay Bekleyen İşlemler";
            this.Load += new System.EventHandler(this.FrmOnayBekleyenler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridOnaylar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOnaylar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOnaylar)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
