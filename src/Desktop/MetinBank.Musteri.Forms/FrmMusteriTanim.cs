/*
 * MetinBank - Müşteri Tanımlama Formu
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Müşteri ekleme/düzeltme/silme işlemleri
 * Standart: Frm[kisa_ad] formatında class ismi
 * Namespace: Musteri.Forms
 */

using System;
using System.Data;
using System.Windows.Forms;
using MetinBank.Common.ControlLib;

namespace MetinBank.Musteri.Forms
{
    /// <summary>
    /// Müşteri Tanımlama Formu
    /// Standart: Frm prefix ile başlar (FrmMusteriTanim)
    /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
    /// </summary>
    public partial class FrmMusteriTanim : Form
    {
        // Private değişkenler - class başında tanımlı (Standart)
        private long _musteriNo;
        private bool _islemModu; // true: Yeni, false: Düzeltme

        /// <summary>
        /// Constructor
        /// </summary>
        public FrmMusteriTanim()
        {
            InitializeComponent();
            FormAyarlari();
        }

        /// <summary>
        /// Form ayarları
        /// Standart: Form size 770x700'den büyük olamaz, AutoScroll = true
        /// </summary>
        private void FormAyarlari()
        {
            // Form özellikleri - Standart
            this.Size = new System.Drawing.Size(750, 600); // Max 770x700
            this.AutoScroll = true; // Standart: true olmalı
            this.Text = "Müşteri Tanımlama"; // Standart: Büyük harfle başla
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Yeni kayıt modu
            _islemModu = true;
            _musteriNo = 0;
            
            // Grid ayarları
            GridAyarla();
        }

        /// <summary>
        /// DataGridView ayarları
        /// Standart: grd prefix (grdMusteriler)
        /// </summary>
        private void GridAyarla()
        {
            grdMusteriler.AutoGenerateColumns = false;
            grdMusteriler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdMusteriler.MultiSelect = false;
            grdMusteriler.AllowUserToAddRows = false;
            grdMusteriler.ReadOnly = true;
            
            // Kolonlar tanımla
            grdMusteriler.Columns.Clear();
            grdMusteriler.Columns.Add("MusteriNo", "Müşteri No");
            grdMusteriler.Columns.Add("TcKimlikNo", "TC Kimlik No");
            grdMusteriler.Columns.Add("Ad", "Ad");
            grdMusteriler.Columns.Add("Soyad", "Soyad");
            grdMusteriler.Columns.Add("Eposta", "E-posta");
            grdMusteriler.Columns.Add("Telefon", "Telefon");
            
            // Standart: DataGridView çift tıklamada düzeltme
            grdMusteriler.DoubleClick += grdMusteriler_DoubleClick;
        }

        /// <summary>
        /// Kaydet butonu Click event
        /// Standart: btnKaydet
        /// </summary>
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string hata = null; // Method içinde tanımlama - Standart

            try
            {
                // Validasyon
                if (!Validate())
                    return;

                // User Control validasyonu - Standart: xValidate() kullan
                if (!ucSubeKod.xValidate())
                    return;

                // Service çağrısı yapılacak
                // Standart: Interface kullan, if(hata!=null) kontrolü yap
                // ISMusteriService service = new SMusteriService();
                // hata = service.MusteriEkle(
                //     txtTcKimlikNo.Text,
                //     txtAd.Text,
                //     txtSoyad.Text,
                //     txtEposta.Text,
                //     txtTelefon.Text
                // );

                // Standart: Hata kontrolü mutlaka yapılmalı
                // if (hata != null)
                // {
                //     MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //     return;
                // }

                // Başarılı mesajı - Standart: İlk harf büyük
                MessageBox.Show("Müşteri başarıyla kaydedildi", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Temizle ve listeyi yenile
                Temizle();
                MusterileriGetir();
            }
            catch (Exception ex) // Exception standart: ex
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sil butonu Click event
        /// </summary>
        private void btnSil_Click(object sender, EventArgs e)
        {
            string hata = null;

            if (_musteriNo == 0)
            {
                MessageBox.Show("Silinecek müşteri seçilmedi!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult sonuc = MessageBox.Show(
                "Müşteriyi silmek istediğinizden emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (sonuc == DialogResult.No)
                return;

            try
            {
                // Service çağrısı
                // hata = service.MusteriSil(_musteriNo);
                
                // if (hata != null)
                // {
                //     MessageBox.Show(hata, "Hata");
                //     return;
                // }

                MessageBox.Show("Müşteri başarıyla silindi", "Bilgi");
                Temizle();
                MusterileriGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata");
            }
        }

        /// <summary>
        /// İptal/Temizle butonu
        /// </summary>
        private void btnIptal_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        /// <summary>
        /// Kapat butonu
        /// </summary>
        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Ara butonu
        /// </summary>
        private void btnAra_Click(object sender, EventArgs e)
        {
            MusteriAra();
        }

        /// <summary>
        /// DataGridView çift tıklama - Standart: Düzeltme yapılmalı
        /// </summary>
        private void grdMusteriler_DoubleClick(object sender, EventArgs e)
        {
            if (grdMusteriler.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = grdMusteriler.SelectedRows[0];
                
                // Seçili satırdan verileri al
                _musteriNo = Convert.ToInt64(selectedRow.Cells["MusteriNo"].Value);
                txtTcKimlikNo.Text = selectedRow.Cells["TcKimlikNo"].Value.ToString();
                txtAd.Text = selectedRow.Cells["Ad"].Value.ToString();
                txtSoyad.Text = selectedRow.Cells["Soyad"].Value.ToString();
                txtEposta.Text = selectedRow.Cells["Eposta"].Value.ToString();
                txtTelefon.Text = selectedRow.Cells["Telefon"].Value.ToString();
                
                _islemModu = false; // Düzeltme modu
                btnKaydet.Text = "Güncelle";
            }
        }

        /// <summary>
        /// Müşteri arama
        /// </summary>
        private void MusteriAra()
        {
            string hata = null;

            try
            {
                // Service çağrısı
                // DataTable dt = service.MusterileriGetir();
                // if (dt != null)
                // {
                //     grdMusteriler.DataSource = dt;
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata");
            }
        }

        /// <summary>
        /// Müşterileri getir
        /// </summary>
        private void MusterileriGetir()
        {
            // Service çağrısı ile müşteri listesi getirilir
            // Standart: DataTable döndürülür, ROWNUM<100 kontrolü yapılır
        }

        /// <summary>
        /// Formu temizle
        /// </summary>
        private void Temizle()
        {
            _musteriNo = 0;
            _islemModu = true;
            
            txtTcKimlikNo.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtEposta.Clear();
            txtTelefon.Clear();
            
            // User Control temizle - Standart: xClear() kullan
            ucSubeKod.xClear();
            
            btnKaydet.Text = "Kaydet";
            txtTcKimlikNo.Focus();
        }

        /// <summary>
        /// Validasyon
        /// </summary>
        private new bool Validate()
        {
            if (string.IsNullOrWhiteSpace(txtTcKimlikNo.Text))
            {
                MessageBox.Show("TC Kimlik No girilmedi!", "Uyarı");
                txtTcKimlikNo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Ad girilmedi!", "Uyarı");
                txtAd.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSoyad.Text))
            {
                MessageBox.Show("Soyad girilmedi!", "Uyarı");
                txtSoyad.Focus();
                return false;
            }

            return true;
        }

        #region Designer Code
        /// <summary>
        /// Required designer variable
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Standart kontrol isimlendirmeleri
        private Label lblTcKimlikNo;
        private Label lblAd;
        private Label lblSoyad;
        private Label lblEposta;
        private Label lblTelefon;
        private Label lblSubeKod;
        
        private TextBox txtTcKimlikNo;
        private TextBox txtAd;
        private TextBox txtSoyad;
        private TextBox txtEposta;
        private TextBox txtTelefon;
        
        private Button btnKaydet;
        private Button btnSil;
        private Button btnIptal;
        private Button btnKapat;
        private Button btnAra;
        
        private DataGridView grdMusteriler;
        
        // User Control - Standart: uc prefix (ucSubeKod)
        private CtrlLibSubeKod ucSubeKod;
        
        private GroupBox grpMusteriBilgi;
        private GroupBox grpMusteriListesi;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpMusteriBilgi = new System.Windows.Forms.GroupBox();
            this.grpMusteriListesi = new System.Windows.Forms.GroupBox();
            
            // Labels - Standart: lbl prefix
            this.lblTcKimlikNo = new System.Windows.Forms.Label();
            this.lblAd = new System.Windows.Forms.Label();
            this.lblSoyad = new System.Windows.Forms.Label();
            this.lblEposta = new System.Windows.Forms.Label();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.lblSubeKod = new System.Windows.Forms.Label();
            
            // TextBoxes - Standart: txt prefix
            this.txtTcKimlikNo = new System.Windows.Forms.TextBox();
            this.txtAd = new System.Windows.Forms.TextBox();
            this.txtSoyad = new System.Windows.Forms.TextBox();
            this.txtEposta = new System.Windows.Forms.TextBox();
            this.txtTelefon = new System.Windows.Forms.TextBox();
            
            // Buttons - Standart: btn prefix, Text büyük harfle başlar
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnKapat = new System.Windows.Forms.Button();
            this.btnAra = new System.Windows.Forms.Button();
            
            // DataGridView - Standart: grd prefix
            this.grdMusteriler = new System.Windows.Forms.DataGridView();
            
            // User Control
            this.ucSubeKod = new MetinBank.Common.ControlLib.CtrlLibSubeKod();
            
            this.grpMusteriBilgi.SuspendLayout();
            this.grpMusteriListesi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMusteriler)).BeginInit();
            this.SuspendLayout();
            
            // 
            // grpMusteriBilgi
            // 
            this.grpMusteriBilgi.Controls.Add(this.lblTcKimlikNo);
            this.grpMusteriBilgi.Controls.Add(this.txtTcKimlikNo);
            this.grpMusteriBilgi.Controls.Add(this.lblAd);
            this.grpMusteriBilgi.Controls.Add(this.txtAd);
            this.grpMusteriBilgi.Controls.Add(this.lblSoyad);
            this.grpMusteriBilgi.Controls.Add(this.txtSoyad);
            this.grpMusteriBilgi.Controls.Add(this.lblEposta);
            this.grpMusteriBilgi.Controls.Add(this.txtEposta);
            this.grpMusteriBilgi.Controls.Add(this.lblTelefon);
            this.grpMusteriBilgi.Controls.Add(this.txtTelefon);
            this.grpMusteriBilgi.Controls.Add(this.lblSubeKod);
            this.grpMusteriBilgi.Controls.Add(this.ucSubeKod);
            this.grpMusteriBilgi.Controls.Add(this.btnKaydet);
            this.grpMusteriBilgi.Controls.Add(this.btnSil);
            this.grpMusteriBilgi.Controls.Add(this.btnIptal);
            this.grpMusteriBilgi.Location = new System.Drawing.Point(12, 12);
            this.grpMusteriBilgi.Name = "grpMusteriBilgi";
            this.grpMusteriBilgi.Size = new System.Drawing.Size(710, 200);
            this.grpMusteriBilgi.TabIndex = 0;
            this.grpMusteriBilgi.TabStop = false;
            this.grpMusteriBilgi.Text = "Müşteri Bilgileri";
            
            // Labels - İlk harf büyük (Standart)
            this.lblTcKimlikNo.AutoSize = true;
            this.lblTcKimlikNo.Location = new System.Drawing.Point(20, 30);
            this.lblTcKimlikNo.Text = "TC Kimlik No:";
            
            this.lblAd.AutoSize = true;
            this.lblAd.Location = new System.Drawing.Point(20, 60);
            this.lblAd.Text = "Ad:";
            
            this.lblSoyad.AutoSize = true;
            this.lblSoyad.Location = new System.Drawing.Point(20, 90);
            this.lblSoyad.Text = "Soyad:";
            
            this.lblEposta.AutoSize = true;
            this.lblEposta.Location = new System.Drawing.Point(350, 30);
            this.lblEposta.Text = "E-posta:";
            
            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Location = new System.Drawing.Point(350, 60);
            this.lblTelefon.Text = "Telefon:";
            
            this.lblSubeKod.AutoSize = true;
            this.lblSubeKod.Location = new System.Drawing.Point(20, 120);
            this.lblSubeKod.Text = "Şube Kodu:";
            
            // TextBoxes
            this.txtTcKimlikNo.Location = new System.Drawing.Point(130, 27);
            this.txtTcKimlikNo.MaxLength = 11;
            this.txtTcKimlikNo.Size = new System.Drawing.Size(200, 23);
            
            this.txtAd.Location = new System.Drawing.Point(130, 57);
            this.txtAd.Size = new System.Drawing.Size(200, 23);
            
            this.txtSoyad.Location = new System.Drawing.Point(130, 87);
            this.txtSoyad.Size = new System.Drawing.Size(200, 23);
            
            this.txtEposta.Location = new System.Drawing.Point(450, 27);
            this.txtEposta.Size = new System.Drawing.Size(240, 23);
            
            this.txtTelefon.Location = new System.Drawing.Point(450, 57);
            this.txtTelefon.Size = new System.Drawing.Size(200, 23);
            
            // User Control
            this.ucSubeKod.Location = new System.Drawing.Point(130, 117);
            this.ucSubeKod.Size = new System.Drawing.Size(260, 50);
            
            // Buttons - Text büyük harfle başlar (Standart)
            this.btnKaydet.Location = new System.Drawing.Point(500, 160);
            this.btnKaydet.Size = new System.Drawing.Size(90, 30);
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            
            this.btnSil.Location = new System.Drawing.Point(596, 160);
            this.btnSil.Size = new System.Drawing.Size(90, 30);
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            
            this.btnIptal.Location = new System.Drawing.Point(404, 160);
            this.btnIptal.Size = new System.Drawing.Size(90, 30);
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            
            // 
            // grpMusteriListesi
            // 
            this.grpMusteriListesi.Controls.Add(this.btnAra);
            this.grpMusteriListesi.Controls.Add(this.grdMusteriler);
            this.grpMusteriListesi.Location = new System.Drawing.Point(12, 218);
            this.grpMusteriListesi.Name = "grpMusteriListesi";
            this.grpMusteriListesi.Size = new System.Drawing.Size(710, 320);
            this.grpMusteriListesi.TabIndex = 1;
            this.grpMusteriListesi.TabStop = false;
            this.grpMusteriListesi.Text = "Müşteri Listesi";
            
            // btnAra
            this.btnAra.Location = new System.Drawing.Point(596, 20);
            this.btnAra.Size = new System.Drawing.Size(90, 30);
            this.btnAra.Text = "Ara";
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);
            
            // grdMusteriler
            this.grdMusteriler.Location = new System.Drawing.Point(10, 56);
            this.grdMusteriler.Size = new System.Drawing.Size(690, 250);
            
            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(632, 544);
            this.btnKapat.Size = new System.Drawing.Size(90, 30);
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            
            // 
            // FrmMusteriTanim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 586);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.grpMusteriListesi);
            this.Controls.Add(this.grpMusteriBilgi);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMusteriTanim";
            this.Text = "Müşteri Tanımlama";
            this.grpMusteriBilgi.ResumeLayout(false);
            this.grpMusteriBilgi.PerformLayout();
            this.grpMusteriListesi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMusteriler)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
    }
}


