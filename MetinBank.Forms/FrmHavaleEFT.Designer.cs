namespace MetinBank.Forms
{
    partial class FrmHavaleEFT
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.LabelControl lblIslemTipi;
        private DevExpress.XtraEditors.ComboBoxEdit cmbIslemTipi;
        private DevExpress.XtraEditors.LabelControl lblKaynakHesapID;
        private DevExpress.XtraEditors.TextEdit txtKaynakHesapID;
        private DevExpress.XtraEditors.LabelControl lblHedefIBAN;
        private DevExpress.XtraEditors.TextEdit txtHedefIBAN;
        private DevExpress.XtraEditors.LabelControl lblTutar;
        private System.Windows.Forms.NumericUpDown numTutar;
        private DevExpress.XtraEditors.LabelControl lblAciklama;
        private DevExpress.XtraEditors.TextEdit txtAciklama;
        private DevExpress.XtraEditors.LabelControl lblAliciAdi;
        private DevExpress.XtraEditors.TextEdit txtAliciAdi;
        private DevExpress.XtraEditors.SimpleButton btnGonder;
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
            this.lblIslemTipi = new DevExpress.XtraEditors.LabelControl();
            this.cmbIslemTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblKaynakHesapID = new DevExpress.XtraEditors.LabelControl();
            this.txtKaynakHesapID = new DevExpress.XtraEditors.TextEdit();
            this.lblHedefIBAN = new DevExpress.XtraEditors.LabelControl();
            this.txtHedefIBAN = new DevExpress.XtraEditors.TextEdit();
            this.lblTutar = new DevExpress.XtraEditors.LabelControl();
            this.numTutar = new System.Windows.Forms.NumericUpDown();
            this.lblAciklama = new DevExpress.XtraEditors.LabelControl();
            this.txtAciklama = new DevExpress.XtraEditors.TextEdit();
            this.lblAliciAdi = new DevExpress.XtraEditors.LabelControl();
            this.txtAliciAdi = new DevExpress.XtraEditors.TextEdit();
            this.btnGonder = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIslemTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKaynakHesapID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHedefIBAN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliciAdi.Properties)).BeginInit();
            this.SuspendLayout();
            
            this.lblIslemTipi.Location = new System.Drawing.Point(20, 20);
            this.lblIslemTipi.Name = "lblIslemTipi";
            this.lblIslemTipi.Size = new System.Drawing.Size(55, 13);
            this.lblIslemTipi.TabIndex = 0;
            this.lblIslemTipi.Text = "İşlem Tipi:";
            
            this.cmbIslemTipi.Location = new System.Drawing.Point(120, 17);
            this.cmbIslemTipi.Name = "cmbIslemTipi";
            this.cmbIslemTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbIslemTipi.Size = new System.Drawing.Size(200, 20);
            this.cmbIslemTipi.TabIndex = 1;
            
            this.lblKaynakHesapID.Location = new System.Drawing.Point(20, 50);
            this.lblKaynakHesapID.Name = "lblKaynakHesapID";
            this.lblKaynakHesapID.Size = new System.Drawing.Size(85, 13);
            this.lblKaynakHesapID.TabIndex = 2;
            this.lblKaynakHesapID.Text = "Kaynak Hesap ID:";
            
            this.txtKaynakHesapID.Location = new System.Drawing.Point(120, 47);
            this.txtKaynakHesapID.Name = "txtKaynakHesapID";
            this.txtKaynakHesapID.Size = new System.Drawing.Size(200, 20);
            this.txtKaynakHesapID.TabIndex = 3;
            
            this.lblHedefIBAN.Location = new System.Drawing.Point(20, 80);
            this.lblHedefIBAN.Name = "lblHedefIBAN";
            this.lblHedefIBAN.Size = new System.Drawing.Size(65, 13);
            this.lblHedefIBAN.TabIndex = 4;
            this.lblHedefIBAN.Text = "Hedef IBAN:";
            
            this.txtHedefIBAN.Location = new System.Drawing.Point(120, 77);
            this.txtHedefIBAN.Name = "txtHedefIBAN";
            this.txtHedefIBAN.Size = new System.Drawing.Size(300, 20);
            this.txtHedefIBAN.TabIndex = 5;
            
            this.lblTutar.Location = new System.Drawing.Point(20, 110);
            this.lblTutar.Name = "lblTutar";
            this.lblTutar.Size = new System.Drawing.Size(30, 13);
            this.lblTutar.TabIndex = 6;
            this.lblTutar.Text = "Tutar:";
            
            this.numTutar.DecimalPlaces = 2;
            this.numTutar.Location = new System.Drawing.Point(120, 108);
            this.numTutar.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numTutar.Name = "numTutar";
            this.numTutar.Size = new System.Drawing.Size(200, 20);
            this.numTutar.TabIndex = 7;
            
            this.lblAciklama.Location = new System.Drawing.Point(20, 140);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(50, 13);
            this.lblAciklama.TabIndex = 8;
            this.lblAciklama.Text = "Açıklama:";
            
            this.txtAciklama.Location = new System.Drawing.Point(120, 137);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(300, 20);
            this.txtAciklama.TabIndex = 9;
            
            this.lblAliciAdi.Location = new System.Drawing.Point(20, 170);
            this.lblAliciAdi.Name = "lblAliciAdi";
            this.lblAliciAdi.Size = new System.Drawing.Size(45, 13);
            this.lblAliciAdi.TabIndex = 10;
            this.lblAliciAdi.Text = "Alıcı Adı:";
            
            this.txtAliciAdi.Location = new System.Drawing.Point(120, 167);
            this.txtAliciAdi.Name = "txtAliciAdi";
            this.txtAliciAdi.Size = new System.Drawing.Size(300, 20);
            this.txtAliciAdi.TabIndex = 11;
            
            this.btnGonder.Location = new System.Drawing.Point(120, 210);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(140, 30);
            this.btnGonder.TabIndex = 12;
            this.btnGonder.Text = "Gönder";
            this.btnGonder.Click += new System.EventHandler(this.BtnGonder_Click);
            
            this.btnKapat.Location = new System.Drawing.Point(280, 210);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(140, 30);
            this.btnKapat.TabIndex = 13;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            this.ClientSize = new System.Drawing.Size(450, 270);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnGonder);
            this.Controls.Add(this.txtAliciAdi);
            this.Controls.Add(this.lblAliciAdi);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.numTutar);
            this.Controls.Add(this.lblTutar);
            this.Controls.Add(this.txtHedefIBAN);
            this.Controls.Add(this.lblHedefIBAN);
            this.Controls.Add(this.txtKaynakHesapID);
            this.Controls.Add(this.lblKaynakHesapID);
            this.Controls.Add(this.cmbIslemTipi);
            this.Controls.Add(this.lblIslemTipi);
            this.Name = "FrmHavaleEFT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Havale / EFT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmHavaleEFT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbIslemTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKaynakHesapID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHedefIBAN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTutar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliciAdi.Properties)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
