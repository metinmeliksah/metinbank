/*
 * MetinBank - Şube Kodu User Control
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Şube kodu seçimi için User Control
 * Standart: CtrlLib prefix, property ve metodlar x ile başlar
 */

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace MetinBank.Common.ControlLib
{
    /// <summary>
    /// Şube Kodu User Control
    /// Standart: CtrlLib[İsim] formatında class ismi
    /// Property ve metodlar x ile başlamalı (xValue, xSetParams, vb.)
    /// Created by: Metin Melikşah Dermencioğlu, 04/11/2025
    /// </summary>
    public partial class CtrlLibSubeKod : UserControl
    {
        // Private değişkenler - class başında tanımlı
        private int _subeKod;
        private string _subeAd;
        private DataTable _dtSubeler;

        /// <summary>
        /// Constructor
        /// </summary>
        public CtrlLibSubeKod()
        {
            InitializeComponent();
            xInit();
        }

        /// <summary>
        /// User Control property ve metodları x ile başlamalı
        /// Şube kodu değeri
        /// </summary>
        [Browsable(true)]
        [Category("xData")]
        [Description("Şube kodu değeri")]
        public int xValue
        {
            get { return _subeKod; }
            set
            {
                _subeKod = value;
                txtSubeKod.Text = value.ToString();
                SubeAdGetir();
            }
        }

        /// <summary>
        /// Şube adı (ReadOnly)
        /// </summary>
        [Browsable(true)]
        [Category("xData")]
        [Description("Şube adı")]
        public string xSubeAd
        {
            get { return _subeAd; }
        }

        /// <summary>
        /// Ekran parametresi (DataTable)
        /// Standart: Her kontrolün xEkranParam property'si olmalı
        /// </summary>
        [Browsable(false)]
        public DataTable xEkranParam
        {
            get { return _dtSubeler; }
            set
            {
                _dtSubeler = value;
                ComboFill();
            }
        }

        /// <summary>
        /// Parametreleri set eden metod
        /// Standart: xSetParams ismi kullanılmalı
        /// </summary>
        /// <param name="subeKod">Şube kodu</param>
        /// <param name="subeAd">Şube adı</param>
        public void xSetParams(int subeKod, string subeAd)
        {
            _subeKod = subeKod;
            _subeAd = subeAd;
            txtSubeKod.Text = subeKod.ToString();
            lblSubeAd.Text = subeAd;
        }

        /// <summary>
        /// Validasyon metodu
        /// Standart: xValidate ismi kullanılmalı
        /// </summary>
        /// <returns>Geçerli ise true, değilse false</returns>
        public bool xValidate()
        {
            if (_subeKod <= 0)
            {
                MessageBox.Show("Şube kodu seçilmedi!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubeKod.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Temizleme metodu
        /// </summary>
        public void xClear()
        {
            _subeKod = 0;
            _subeAd = string.Empty;
            txtSubeKod.Text = string.Empty;
            lblSubeAd.Text = string.Empty;
            cmbSubeAd.SelectedIndex = -1;
        }

        /// <summary>
        /// Enabled property
        /// </summary>
        public bool xEnabled
        {
            get { return txtSubeKod.Enabled; }
            set
            {
                txtSubeKod.Enabled = value;
                cmbSubeAd.Enabled = value;
                btnAra.Enabled = value;
            }
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void xInit()
        {
            // Kontrol başlangıç ayarları
            txtSubeKod.MaxLength = 5;
            lblSubeAd.Text = string.Empty;
        }

        /// <summary>
        /// Şube adını getir
        /// </summary>
        private void SubeAdGetir()
        {
            if (_dtSubeler == null || _dtSubeler.Rows.Count == 0)
                return;

            // DataTable'dan şube adını bul
            DataRow[] rows = _dtSubeler.Select($"sube_kod = {_subeKod}");
            if (rows.Length > 0)
            {
                _subeAd = rows[0]["sube_ad"].ToString();
                lblSubeAd.Text = _subeAd;
            }
        }

        /// <summary>
        /// ComboBox'ı doldur
        /// </summary>
        private void ComboFill()
        {
            if (_dtSubeler == null)
                return;

            cmbSubeAd.DataSource = _dtSubeler;
            cmbSubeAd.DisplayMember = "sube_ad";
            cmbSubeAd.ValueMember = "sube_kod";
        }

        /// <summary>
        /// TextBox KeyPress event
        /// </summary>
        private void txtSubeKod_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam girişi
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// TextBox Leave event
        /// </summary>
        private void txtSubeKod_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtSubeKod.Text, out int subeKod))
            {
                _subeKod = subeKod;
                SubeAdGetir();
            }
        }

        /// <summary>
        /// ComboBox SelectedIndexChanged event
        /// </summary>
        private void cmbSubeAd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSubeAd.SelectedValue != null && 
                int.TryParse(cmbSubeAd.SelectedValue.ToString(), out int subeKod))
            {
                _subeKod = subeKod;
                txtSubeKod.Text = subeKod.ToString();
                SubeAdGetir();
            }
        }

        /// <summary>
        /// Ara butonu Click event
        /// </summary>
        private void btnAra_Click(object sender, EventArgs e)
        {
            // Şube arama popup'ı açılır (örnek)
            MessageBox.Show("Şube arama ekranı açılacak", "Bilgi");
        }

        #region Designer Code
        /// <summary>
        /// Required designer variable
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// TextBox: txtSubeKod - Standart isimlendirme
        /// </summary>
        private TextBox txtSubeKod;

        /// <summary>
        /// Label: lblSubeAd - Standart isimlendirme
        /// </summary>
        private Label lblSubeAd;

        /// <summary>
        /// ComboBox: cmbSubeAd - Standart isimlendirme
        /// </summary>
        private ComboBox cmbSubeAd;

        /// <summary>
        /// Button: btnAra - Standart isimlendirme
        /// </summary>
        private Button btnAra;

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
        /// Required method for Designer support
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSubeKod = new System.Windows.Forms.TextBox();
            this.lblSubeAd = new System.Windows.Forms.Label();
            this.cmbSubeAd = new System.Windows.Forms.ComboBox();
            this.btnAra = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // txtSubeKod
            this.txtSubeKod.Location = new System.Drawing.Point(3, 3);
            this.txtSubeKod.Name = "txtSubeKod";
            this.txtSubeKod.Size = new System.Drawing.Size(60, 23);
            this.txtSubeKod.TabIndex = 0;
            this.txtSubeKod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubeKod_KeyPress);
            this.txtSubeKod.Leave += new System.EventHandler(this.txtSubeKod_Leave);
            
            // cmbSubeAd
            this.cmbSubeAd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubeAd.FormattingEnabled = true;
            this.cmbSubeAd.Location = new System.Drawing.Point(69, 3);
            this.cmbSubeAd.Name = "cmbSubeAd";
            this.cmbSubeAd.Size = new System.Drawing.Size(150, 23);
            this.cmbSubeAd.TabIndex = 1;
            this.cmbSubeAd.SelectedIndexChanged += new System.EventHandler(this.cmbSubeAd_SelectedIndexChanged);
            
            // btnAra
            this.btnAra.Location = new System.Drawing.Point(225, 2);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(30, 24);
            this.btnAra.TabIndex = 2;
            this.btnAra.Text = "...";
            this.btnAra.UseVisualStyleBackColor = true;
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);
            
            // lblSubeAd
            this.lblSubeAd.AutoSize = true;
            this.lblSubeAd.Location = new System.Drawing.Point(3, 29);
            this.lblSubeAd.Name = "lblSubeAd";
            this.lblSubeAd.Size = new System.Drawing.Size(0, 15);
            this.lblSubeAd.TabIndex = 3;
            
            // CtrlLibSubeKod
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSubeAd);
            this.Controls.Add(this.btnAra);
            this.Controls.Add(this.cmbSubeAd);
            this.Controls.Add(this.txtSubeKod);
            this.Name = "CtrlLibSubeKod";
            this.Size = new System.Drawing.Size(260, 50);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}


