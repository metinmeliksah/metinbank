namespace MetinBank.Desktop
{
    partial class FrmHesapIslem
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.LabelControl lblMusteriID;
        private DevExpress.XtraEditors.TextEdit txtMusteriID;
        private DevExpress.XtraEditors.LabelControl lblHesapTipi;
        private DevExpress.XtraEditors.ComboBoxEdit cmbHesapTipi;
        private DevExpress.XtraEditors.LabelControl lblHesapCinsi;
        private DevExpress.XtraEditors.ComboBoxEdit cmbHesapCinsi;
        private DevExpress.XtraEditors.LabelControl lblFaizOrani;
        private System.Windows.Forms.NumericUpDown numFaizOrani;
        private DevExpress.XtraEditors.LabelControl lblIBAN;
        private DevExpress.XtraEditors.TextEdit txtIBAN;
        private DevExpress.XtraEditors.SimpleButton btnHesapAc;
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
            this.lblMusteriID = new DevExpress.XtraEditors.LabelControl();
            this.txtMusteriID = new DevExpress.XtraEditors.TextEdit();
            this.lblHesapTipi = new DevExpress.XtraEditors.LabelControl();
            this.cmbHesapTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblHesapCinsi = new DevExpress.XtraEditors.LabelControl();
            this.cmbHesapCinsi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblFaizOrani = new DevExpress.XtraEditors.LabelControl();
            this.numFaizOrani = new System.Windows.Forms.NumericUpDown();
            this.lblIBAN = new DevExpress.XtraEditors.LabelControl();
            this.txtIBAN = new DevExpress.XtraEditors.TextEdit();
            this.btnHesapAc = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapCinsi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaizOrani)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIBAN.Properties)).BeginInit();
            this.SuspendLayout();
            
            this.lblMusteriID.Location = new System.Drawing.Point(20, 20);
            this.lblMusteriID.Name = "lblMusteriID";
            this.lblMusteriID.Size = new System.Drawing.Size(60, 13);
            this.lblMusteriID.TabIndex = 0;
            this.lblMusteriID.Text = "Müşteri ID:";
            
            this.txtMusteriID.Location = new System.Drawing.Point(120, 17);
            this.txtMusteriID.Name = "txtMusteriID";
            this.txtMusteriID.Size = new System.Drawing.Size(200, 20);
            this.txtMusteriID.TabIndex = 1;
            
            this.lblHesapTipi.Location = new System.Drawing.Point(20, 50);
            this.lblHesapTipi.Name = "lblHesapTipi";
            this.lblHesapTipi.Size = new System.Drawing.Size(60, 13);
            this.lblHesapTipi.TabIndex = 2;
            this.lblHesapTipi.Text = "Hesap Tipi:";
            
            this.cmbHesapTipi.Location = new System.Drawing.Point(120, 47);
            this.cmbHesapTipi.Name = "cmbHesapTipi";
            this.cmbHesapTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHesapTipi.Size = new System.Drawing.Size(200, 20);
            this.cmbHesapTipi.TabIndex = 3;
            
            this.lblHesapCinsi.Location = new System.Drawing.Point(20, 80);
            this.lblHesapCinsi.Name = "lblHesapCinsi";
            this.lblHesapCinsi.Size = new System.Drawing.Size(65, 13);
            this.lblHesapCinsi.TabIndex = 4;
            this.lblHesapCinsi.Text = "Hesap Cinsi:";
            
            this.cmbHesapCinsi.Location = new System.Drawing.Point(120, 77);
            this.cmbHesapCinsi.Name = "cmbHesapCinsi";
            this.cmbHesapCinsi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHesapCinsi.Size = new System.Drawing.Size(200, 20);
            this.cmbHesapCinsi.TabIndex = 5;
            
            this.lblFaizOrani.Location = new System.Drawing.Point(20, 110);
            this.lblFaizOrani.Name = "lblFaizOrani";
            this.lblFaizOrani.Size = new System.Drawing.Size(60, 13);
            this.lblFaizOrani.TabIndex = 6;
            this.lblFaizOrani.Text = "Faiz Oranı:";
            
            this.numFaizOrani.DecimalPlaces = 2;
            this.numFaizOrani.Location = new System.Drawing.Point(120, 108);
            this.numFaizOrani.Name = "numFaizOrani";
            this.numFaizOrani.Size = new System.Drawing.Size(200, 20);
            this.numFaizOrani.TabIndex = 7;
            
            this.lblIBAN.Location = new System.Drawing.Point(20, 140);
            this.lblIBAN.Name = "lblIBAN";
            this.lblIBAN.Size = new System.Drawing.Size(30, 13);
            this.lblIBAN.TabIndex = 8;
            this.lblIBAN.Text = "IBAN:";
            
            this.txtIBAN.Location = new System.Drawing.Point(120, 137);
            this.txtIBAN.Name = "txtIBAN";
            this.txtIBAN.Properties.ReadOnly = true;
            this.txtIBAN.Size = new System.Drawing.Size(300, 20);
            this.txtIBAN.TabIndex = 9;
            
            this.btnHesapAc.Location = new System.Drawing.Point(120, 180);
            this.btnHesapAc.Name = "btnHesapAc";
            this.btnHesapAc.Size = new System.Drawing.Size(140, 30);
            this.btnHesapAc.TabIndex = 10;
            this.btnHesapAc.Text = "Hesap Aç";
            this.btnHesapAc.Click += new System.EventHandler(this.BtnHesapAc_Click);
            
            this.btnKapat.Location = new System.Drawing.Point(280, 180);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(140, 30);
            this.btnKapat.TabIndex = 11;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            this.ClientSize = new System.Drawing.Size(450, 240);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnHesapAc);
            this.Controls.Add(this.txtIBAN);
            this.Controls.Add(this.lblIBAN);
            this.Controls.Add(this.numFaizOrani);
            this.Controls.Add(this.lblFaizOrani);
            this.Controls.Add(this.cmbHesapCinsi);
            this.Controls.Add(this.lblHesapCinsi);
            this.Controls.Add(this.cmbHesapTipi);
            this.Controls.Add(this.lblHesapTipi);
            this.Controls.Add(this.txtMusteriID);
            this.Controls.Add(this.lblMusteriID);
            this.Name = "FrmHesapIslem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hesap İşlemleri";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmHesapIslem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHesapCinsi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFaizOrani)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIBAN.Properties)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
