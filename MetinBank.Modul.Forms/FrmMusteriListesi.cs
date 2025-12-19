using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MetinBank.Entities;
using MetinBank.Modul.Service;

namespace MetinBank.Modul.Forms
{
    /// <summary>
    /// Müşteri listesi formu
    /// NOT: DevExpress kurulumu yapıldığında XtraForm'dan türetilecek
    /// ve GridControl kullanılacak
    /// </summary>
    public partial class FrmMusteriListesi : Form
    {
        private readonly CustomerService _customerService;

        // DevExpress kontrolleri (kurulum sonrası kullanılacak)
        // private GridControl grdMusteri;
        // private GridView grdwMusteri;
        // private SimpleButton btnYeni;
        // private SimpleButton btnDuzenle;
        // private SimpleButton btnSil;

        // Geçici standart kontroller
        private DataGridView dgvMusteri;
        private Button btnYeni;
        private Button btnDuzenle;
        private Button btnSil;
        private Button btnYenile;

        public FrmMusteriListesi()
        {
            _customerService = new CustomerService();
            InitializeComponent();
            LoadCustomers();
        }

        private void InitializeComponent()
        {
            this.dgvMusteri = new DataGridView();
            this.btnYeni = new Button();
            this.btnDuzenle = new Button();
            this.btnSil = new Button();
            this.btnYenile = new Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusteri)).BeginInit();
            this.SuspendLayout();
            
            // dgvMusteri
            this.dgvMusteri.AllowUserToAddRows = false;
            this.dgvMusteri.AllowUserToDeleteRows = false;
            this.dgvMusteri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMusteri.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMusteri.Dock = DockStyle.Top;
            this.dgvMusteri.Location = new Point(0, 0);
            this.dgvMusteri.MultiSelect = false;
            this.dgvMusteri.Name = "dgvMusteri";
            this.dgvMusteri.ReadOnly = true;
            this.dgvMusteri.RowHeadersWidth = 51;
            this.dgvMusteri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMusteri.Size = new Size(1200, 600);
            this.dgvMusteri.TabIndex = 0;
            this.dgvMusteri.DoubleClick += new EventHandler(this.dgvMusteri_DoubleClick);
            
            // btnYeni
            this.btnYeni.Location = new Point(20, 620);
            this.btnYeni.Name = "btnYeni";
            this.btnYeni.Size = new Size(120, 35);
            this.btnYeni.TabIndex = 1;
            this.btnYeni.Text = "Yeni Müşteri";
            this.btnYeni.UseVisualStyleBackColor = true;
            this.btnYeni.Click += new EventHandler(this.btnYeni_Click);
            
            // btnDuzenle
            this.btnDuzenle.Location = new Point(150, 620);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new Size(120, 35);
            this.btnDuzenle.TabIndex = 2;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.UseVisualStyleBackColor = true;
            this.btnDuzenle.Click += new EventHandler(this.btnDuzenle_Click);
            
            // btnSil
            this.btnSil.Location = new Point(280, 620);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new Size(120, 35);
            this.btnSil.TabIndex = 3;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new EventHandler(this.btnSil_Click);
            
            // btnYenile
            this.btnYenile.Location = new Point(410, 620);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new Size(120, 35);
            this.btnYenile.TabIndex = 4;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.UseVisualStyleBackColor = true;
            this.btnYenile.Click += new EventHandler(this.btnYenile_Click);
            
            // FrmMusteriListesi
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 670);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnDuzenle);
            this.Controls.Add(this.btnYeni);
            this.Controls.Add(this.dgvMusteri);
            this.Name = "FrmMusteriListesi";
            this.Text = "Müşteri Listesi";
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusteri)).EndInit();
            this.ResumeLayout(false);
        }

        private void LoadCustomers()
        {
            string? error = _customerService.GetAllCustomers(out List<Customer>? customers);

            if (error != null)
            {
                MessageBox.Show(error, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvMusteri.DataSource = customers;

            // Gereksiz kolonları gizle
            if (dgvMusteri.Columns.Contains("Photo"))
                dgvMusteri.Columns["Photo"]!.Visible = false;
            if (dgvMusteri.Columns.Contains("Signature"))
                dgvMusteri.Columns["Signature"]!.Visible = false;
            if (dgvMusteri.Columns.Contains("CreatedBy"))
                dgvMusteri.Columns["CreatedBy"]!.Visible = false;
        }

        private void btnYeni_Click(object? sender, EventArgs e)
        {
            // Yeni müşteri formu açılacak (sonraki aşamada)
            MessageBox.Show("Müşteri kart formu Aşama 2'de eklenecek", "Bilgi", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDuzenle_Click(object? sender, EventArgs e)
        {
            if (dgvMusteri.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlemek için bir müşteri seçin!", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Müşteri düzenleme formu açılacak (sonraki aşamada)
            MessageBox.Show("Müşteri düzenleme Aşama 2'de eklenecek", "Bilgi", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object? sender, EventArgs e)
        {
            if (dgvMusteri.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir müşteri seçin!", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Seçili müşteriyi silmek istediğinize emin misiniz?", 
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Customer selectedCustomer = (Customer)dgvMusteri.SelectedRows[0].DataBoundItem;
                string? error = _customerService.DeleteCustomer(selectedCustomer.CustomerId);

                if (error != null)
                {
                    MessageBox.Show(error, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Müşteri başarıyla silindi!", "Başarılı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
                }
            }
        }

        private void btnYenile_Click(object? sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void dgvMusteri_DoubleClick(object? sender, EventArgs e)
        {
            // Çift tıklamada müşteri kartı açılacak (sonraki aşamada)
            btnDuzenle_Click(sender, e);
        }
    }
}
