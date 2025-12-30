namespace MetinBank.Forms
{
    partial class FrmAnaSayfa
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.LabelControl lblHosgeldin;
        private DevExpress.XtraEditors.LabelControl lblRol;
        private DevExpress.XtraEditors.LabelControl lblSube;
        private DevExpress.XtraEditors.SimpleButton btnMusteriIslem;
        private DevExpress.XtraEditors.SimpleButton btnHesapIslem;
        private DevExpress.XtraEditors.SimpleButton btnParaYatir;
        private DevExpress.XtraEditors.SimpleButton btnParaCek;
        private DevExpress.XtraEditors.SimpleButton btnHavaleEFT;
        private DevExpress.XtraEditors.SimpleButton btnOnayBekleyenler;

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
            this.lblHosgeldin = new DevExpress.XtraEditors.LabelControl();
            this.lblRol = new DevExpress.XtraEditors.LabelControl();
            this.lblSube = new DevExpress.XtraEditors.LabelControl();
            this.btnMusteriIslem = new DevExpress.XtraEditors.SimpleButton();
            this.btnHesapIslem = new DevExpress.XtraEditors.SimpleButton();
            this.btnParaYatir = new DevExpress.XtraEditors.SimpleButton();
            this.btnParaCek = new DevExpress.XtraEditors.SimpleButton();
            this.btnHavaleEFT = new DevExpress.XtraEditors.SimpleButton();
            this.btnOnayBekleyenler = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            
            this.lblHosgeldin.Location = new System.Drawing.Point(20, 20);
            this.lblHosgeldin.Name = "lblHosgeldin";
            this.lblHosgeldin.Size = new System.Drawing.Size(200, 13);
            this.lblHosgeldin.TabIndex = 0;
            this.lblHosgeldin.Text = "Hoş geldiniz";
            
            this.lblRol.Location = new System.Drawing.Point(20, 50);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(200, 13);
            this.lblRol.TabIndex = 1;
            this.lblRol.Text = "Rol:";
            
            this.lblSube.Location = new System.Drawing.Point(20, 70);
            this.lblSube.Name = "lblSube";
            this.lblSube.Size = new System.Drawing.Size(200, 13);
            this.lblSube.TabIndex = 2;
            this.lblSube.Text = "Şube:";
            
            this.btnMusteriIslem.Location = new System.Drawing.Point(20, 100);
            this.btnMusteriIslem.Name = "btnMusteriIslem";
            this.btnMusteriIslem.Size = new System.Drawing.Size(150, 40);
            this.btnMusteriIslem.TabIndex = 3;
            this.btnMusteriIslem.Text = "Müşteri İşlemleri";
            this.btnMusteriIslem.Click += new System.EventHandler(this.BtnMusteriIslem_Click);
            
            this.btnHesapIslem.Location = new System.Drawing.Point(190, 100);
            this.btnHesapIslem.Name = "btnHesapIslem";
            this.btnHesapIslem.Size = new System.Drawing.Size(150, 40);
            this.btnHesapIslem.TabIndex = 4;
            this.btnHesapIslem.Text = "Hesap İşlemleri";
            this.btnHesapIslem.Click += new System.EventHandler(this.BtnHesapIslem_Click);
            
            this.btnParaYatir.Location = new System.Drawing.Point(20, 160);
            this.btnParaYatir.Name = "btnParaYatir";
            this.btnParaYatir.Size = new System.Drawing.Size(150, 40);
            this.btnParaYatir.TabIndex = 5;
            this.btnParaYatir.Text = "Para Yatır";
            this.btnParaYatir.Click += new System.EventHandler(this.BtnParaYatir_Click);
            
            this.btnParaCek.Location = new System.Drawing.Point(190, 160);
            this.btnParaCek.Name = "btnParaCek";
            this.btnParaCek.Size = new System.Drawing.Size(150, 40);
            this.btnParaCek.TabIndex = 6;
            this.btnParaCek.Text = "Para Çek";
            this.btnParaCek.Click += new System.EventHandler(this.BtnParaCek_Click);
            
            this.btnHavaleEFT.Location = new System.Drawing.Point(20, 220);
            this.btnHavaleEFT.Name = "btnHavaleEFT";
            this.btnHavaleEFT.Size = new System.Drawing.Size(150, 40);
            this.btnHavaleEFT.TabIndex = 7;
            this.btnHavaleEFT.Text = "Havale / EFT";
            this.btnHavaleEFT.Click += new System.EventHandler(this.BtnHavaleEFT_Click);
            
            this.btnOnayBekleyenler.Location = new System.Drawing.Point(190, 220);
            this.btnOnayBekleyenler.Name = "btnOnayBekleyenler";
            this.btnOnayBekleyenler.Size = new System.Drawing.Size(150, 40);
            this.btnOnayBekleyenler.TabIndex = 8;
            this.btnOnayBekleyenler.Text = "Onay Bekleyenler";
            this.btnOnayBekleyenler.Click += new System.EventHandler(this.BtnOnayBekleyenler_Click);
            
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnOnayBekleyenler);
            this.Controls.Add(this.btnHavaleEFT);
            this.Controls.Add(this.btnParaCek);
            this.Controls.Add(this.btnParaYatir);
            this.Controls.Add(this.btnHesapIslem);
            this.Controls.Add(this.btnMusteriIslem);
            this.Controls.Add(this.lblSube);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.lblHosgeldin);
            this.Name = "FrmAnaSayfa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ana Sayfa";
            this.Load += new System.EventHandler(this.FrmAnaSayfa_Load);
            this.ResumeLayout(false);
        }
    }
}
