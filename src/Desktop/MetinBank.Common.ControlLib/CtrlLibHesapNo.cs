/*
 * MetinBank - Hesap No User Control
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Hesap numarası girişi için User Control
 * Standart: CtrlLib prefix, property ve metodlar x ile başlar
 */

using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MetinBank.Common.ControlLib
{
    /// <summary>
    /// Hesap No User Control
    /// Standart: CtrlLib[İsim] formatında class ismi
    /// Property ve metodlar x ile başlamalı
    /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
    /// </summary>
    public partial class CtrlLibHesapNo : UserControl
    {
        // Private değişkenler - class başında tanımlı
        private string _hesapNo;
        private long _musteriNo;
        private string _musteriAd;
        private decimal _bakiye;

        /// <summary>
        /// Constructor
        /// </summary>
        public CtrlLibHesapNo()
        {
            InitializeComponent();
            xInit();
        }

        /// <summary>
        /// Hesap numarası değeri
        /// Standart: x prefix
        /// </summary>
        [Browsable(true)]
        [Category("xData")]
        [Description("Hesap numarası (IBAN)")]
        public string xValue
        {
            get { return _hesapNo; }
            set
            {
                _hesapNo = value;
                txtHesapNo.Text = FormatIban(value);
                HesapBilgiGetir();
            }
        }

        /// <summary>
        /// Müşteri numarası (ReadOnly)
        /// </summary>
        [Browsable(true)]
        [Category("xData")]
        public long xMusteriNo
        {
            get { return _musteriNo; }
        }

        /// <summary>
        /// Bakiye (ReadOnly)
        /// </summary>
        [Browsable(true)]
        [Category("xData")]
        public decimal xBakiye
        {
            get { return _bakiye; }
        }

        /// <summary>
        /// Parametreleri set eden metod
        /// Standart: xSetParams
        /// </summary>
        public void xSetParams(string hesapNo, long musteriNo, string musteriAd, decimal bakiye)
        {
            _hesapNo = hesapNo;
            _musteriNo = musteriNo;
            _musteriAd = musteriAd;
            _bakiye = bakiye;

            txtHesapNo.Text = FormatIban(hesapNo);
            lblMusteriAd.Text = musteriAd;
            lblBakiye.Text = bakiye.ToString("N2") + " TL";
        }

        /// <summary>
        /// Validasyon metodu
        /// </summary>
        public bool xValidate()
        {
            if (string.IsNullOrWhiteSpace(_hesapNo))
            {
                MessageBox.Show("Hesap numarası girilmedi!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHesapNo.Focus();
                return false;
            }

            // IBAN validasyonu
            if (!IbanDogrula(_hesapNo))
            {
                MessageBox.Show("Geçersiz IBAN numarası!", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHesapNo.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Temizleme metodu
        /// </summary>
        public void xClear()
        {
            _hesapNo = string.Empty;
            _musteriNo = 0;
            _musteriAd = string.Empty;
            _bakiye = 0;

            txtHesapNo.Text = string.Empty;
            lblMusteriAd.Text = string.Empty;
            lblBakiye.Text = string.Empty;
        }

        /// <summary>
        /// Enabled property
        /// </summary>
        public bool xEnabled
        {
            get { return txtHesapNo.Enabled; }
            set
            {
                txtHesapNo.Enabled = value;
                btnAra.Enabled = value;
            }
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void xInit()
        {
            txtHesapNo.MaxLength = 32; // IBAN: TR + 24 karakter + boşluklar
            lblMusteriAd.Text = string.Empty;
            lblBakiye.Text = string.Empty;
        }

        /// <summary>
        /// IBAN formatla (TR33 0006 2000 0000 0000 1234 56)
        /// </summary>
        private string FormatIban(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                return string.Empty;

            // Boşlukları temizle
            iban = iban.Replace(" ", "");

            if (iban.Length < 4)
                return iban;

            // 4'lü gruplara ayır - StringBuilder kullan (Standart)
            StringBuilder formatted = new StringBuilder();
            for (int i = 0; i < iban.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                    formatted.Append(" ");
                formatted.Append(iban[i]);
            }

            return formatted.ToString();
        }

        /// <summary>
        /// IBAN doğrulama
        /// </summary>
        private bool IbanDogrula(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban))
                return false;

            // Boşlukları temizle
            iban = iban.Replace(" ", "").ToUpper();

            // Türkiye IBAN'ı TR ile başlamalı ve 26 karakter olmalı
            if (!iban.StartsWith("TR") || iban.Length != 26)
                return false;

            // Basit validasyon (Gerçek uygulamada mod 97 algoritması kullanılmalı)
            return true;
        }

        /// <summary>
        /// Hesap bilgilerini getir (Database'den)
        /// </summary>
        private void HesapBilgiGetir()
        {
            if (string.IsNullOrWhiteSpace(_hesapNo))
                return;

            // TODO: Service çağrısı yapılacak
            // Örnek:
            // IHesapService service = new SHesapService();
            // DataTable dt = service.HesapBilgiGetir(_hesapNo);
            // if (dt != null && dt.Rows.Count > 0)
            // {
            //     _musteriNo = Convert.ToInt64(dt.Rows[0]["musteri_no"]);
            //     _musteriAd = dt.Rows[0]["musteri_ad"].ToString();
            //     _bakiye = Convert.ToDecimal(dt.Rows[0]["bakiye"]);
            // }
        }

        /// <summary>
        /// TextBox Leave event
        /// </summary>
        private void txtHesapNo_Leave(object sender, EventArgs e)
        {
            string hesapNo = txtHesapNo.Text.Replace(" ", "");
            if (!string.IsNullOrWhiteSpace(hesapNo))
            {
                _hesapNo = hesapNo;
                txtHesapNo.Text = FormatIban(hesapNo);
                HesapBilgiGetir();
            }
        }

        /// <summary>
        /// Ara butonu Click event
        /// </summary>
        private void btnAra_Click(object sender, EventArgs e)
        {
            // Hesap arama popup'ı açılır
            MessageBox.Show("Hesap arama ekranı açılacak", "Bilgi");
        }

        #region Designer Code
        private System.ComponentModel.IContainer components = null;
        private TextBox txtHesapNo;
        private Label lblMusteriAd;
        private Label lblBakiye;
        private Button btnAra;
        private Label lblMusteriAdBaslik;
        private Label lblBakiyeBaslik;

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
            this.txtHesapNo = new System.Windows.Forms.TextBox();
            this.lblMusteriAdBaslik = new System.Windows.Forms.Label();
            this.lblMusteriAd = new System.Windows.Forms.Label();
            this.lblBakiyeBaslik = new System.Windows.Forms.Label();
            this.lblBakiye = new System.Windows.Forms.Label();
            this.btnAra = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // txtHesapNo
            this.txtHesapNo.Location = new System.Drawing.Point(3, 3);
            this.txtHesapNo.Name = "txtHesapNo";
            this.txtHesapNo.Size = new System.Drawing.Size(250, 23);
            this.txtHesapNo.TabIndex = 0;
            this.txtHesapNo.Leave += new System.EventHandler(this.txtHesapNo_Leave);
            
            // btnAra
            this.btnAra.Location = new System.Drawing.Point(259, 2);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(30, 24);
            this.btnAra.TabIndex = 1;
            this.btnAra.Text = "...";
            this.btnAra.UseVisualStyleBackColor = true;
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);
            
            // lblMusteriAdBaslik
            this.lblMusteriAdBaslik.AutoSize = true;
            this.lblMusteriAdBaslik.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMusteriAdBaslik.Location = new System.Drawing.Point(3, 29);
            this.lblMusteriAdBaslik.Name = "lblMusteriAdBaslik";
            this.lblMusteriAdBaslik.Size = new System.Drawing.Size(75, 13);
            this.lblMusteriAdBaslik.TabIndex = 2;
            this.lblMusteriAdBaslik.Text = "Müşteri Adı:";
            
            // lblMusteriAd
            this.lblMusteriAd.AutoSize = true;
            this.lblMusteriAd.Location = new System.Drawing.Point(85, 29);
            this.lblMusteriAd.Name = "lblMusteriAd";
            this.lblMusteriAd.Size = new System.Drawing.Size(0, 15);
            this.lblMusteriAd.TabIndex = 3;
            
            // lblBakiyeBaslik
            this.lblBakiyeBaslik.AutoSize = true;
            this.lblBakiyeBaslik.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblBakiyeBaslik.Location = new System.Drawing.Point(3, 49);
            this.lblBakiyeBaslik.Name = "lblBakiyeBaslik";
            this.lblBakiyeBaslik.Size = new System.Drawing.Size(48, 13);
            this.lblBakiyeBaslik.TabIndex = 4;
            this.lblBakiyeBaslik.Text = "Bakiye:";
            
            // lblBakiye
            this.lblBakiye.AutoSize = true;
            this.lblBakiye.Location = new System.Drawing.Point(85, 49);
            this.lblBakiye.Name = "lblBakiye";
            this.lblBakiye.Size = new System.Drawing.Size(0, 15);
            this.lblBakiye.TabIndex = 5;
            
            // CtrlLibHesapNo
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBakiye);
            this.Controls.Add(this.lblBakiyeBaslik);
            this.Controls.Add(this.lblMusteriAd);
            this.Controls.Add(this.lblMusteriAdBaslik);
            this.Controls.Add(this.btnAra);
            this.Controls.Add(this.txtHesapNo);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "CtrlLibHesapNo";
            this.Size = new System.Drawing.Size(295, 70);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}


