namespace MetinBank.Forms
{
    partial class FrmParaYatir
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.LabelControl lblHesapID;
        private DevExpress.XtraEditors.TextEdit txtHesapID;
        private DevExpress.XtraEditors.LabelControl lblTutar;
        private System.Windows.Forms.NumericUpDown numTutar;
        private DevExpress.XtraEditors.LabelControl lblAciklama;
        private DevExpress.XtraEditors.TextEdit txtAciklama;
        private DevExpress.XtraEditors.SimpleButton btnYatir;
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
            this.lblHesapID = new DevExpress.XtraEditors.LabelControl();
            this.txtHesapID = new DevExpress.XtraEditors.TextEdit();
            this.lblTutar = new DevExpress.XtraEditors.LabelControl();
            this.numTutar = new System.Windows.Forms.NumericUpDown();
            this.lblAciklama = new DevExpress.XtraEditors.LabelControl();
            this.txtAciklama = new DevExpress.XtraEditors.TextEdit();
            this.btnYatir = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtHesapID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            this.SuspendLayout();
            
            this.lblHesapID.Location = new System.Drawing.Point(20, 20);
            this.lblHesapID.Name = "lblHesapID";
            this.lblHesapID.Size = new System.Drawing.Size(50, 13);
            this.lblHesapID.TabIndex = 0;
            this.lblHesapID.Text = "Hesap ID:";
            
            this.txtHesapID.Location = new System.Drawing.Point(120, 17);
            this.txtHesapID.Name = "txtHesapID";
            this.txtHesapID.Size = new System.Drawing.Size(200, 20);
            this.txtHesapID.TabIndex = 1;
            
            this.lblTutar.Location = new System.Drawing.Point(20, 50);
            this.lblTutar.Name = "lblTutar";
            this.lblTutar.Size = new System.Drawing.Size(30, 13);
            this.lblTutar.TabIndex = 2;
            this.lblTutar.Text = "Tutar:";
            
            this.numTutar.DecimalPlaces = 2;
            this.numTutar.Location = new System.Drawing.Point(120, 48);
            this.numTutar.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numTutar.Name = "numTutar";
            this.numTutar.Size = new System.Drawing.Size(200, 20);
            this.numTutar.TabIndex = 3;
            
            this.lblAciklama.Location = new System.Drawing.Point(20, 80);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(50, 13);
            this.lblAciklama.TabIndex = 4;
            this.lblAciklama.Text = "Açıklama:";
            
            this.txtAciklama.Location = new System.Drawing.Point(120, 77);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(300, 20);
            this.txtAciklama.TabIndex = 5;
            
            this.btnYatir.Location = new System.Drawing.Point(120, 120);
            this.btnYatir.Name = "btnYatir";
            this.btnYatir.Size = new System.Drawing.Size(140, 30);
            this.btnYatir.TabIndex = 6;
            this.btnYatir.Text = "Para Yatır";
            this.btnYatir.Click += new System.EventHandler(this.BtnYatir_Click);
            
            this.btnKapat.Location = new System.Drawing.Point(280, 120);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(140, 30);
            this.btnKapat.TabIndex = 7;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            this.ClientSize = new System.Drawing.Size(450, 180);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnYatir);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.numTutar);
            this.Controls.Add(this.lblTutar);
            this.Controls.Add(this.txtHesapID);
            this.Controls.Add(this.lblHesapID);
            this.Name = "FrmParaYatir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Para Yatır";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmParaYatir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtHesapID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
