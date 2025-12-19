using System;
using System.Windows.Forms;

namespace MetinBank.Modul.Forms
{
    /// <summary>
    /// Ana form
    /// NOT: DevExpress kurulumu yapıldığında XtraForm'dan türetilecek
    /// ve NavBarControl veya RibbonControl eklenecek
    /// </summary>
    public partial class FrmMain : Form
    {
        // DevExpress kontrolleri (kurulum sonrası kullanılacak)
        // private NavBarControl navBarControl1;
        // private RibbonControl ribbonControl1;

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuMusteri;
        private ToolStripMenuItem mnuMusteriListesi;
        private ToolStripMenuItem mnuHesap;
        private ToolStripMenuItem mnuHesapIslemleri;
        private ToolStripMenuItem mnuCikis;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblUser;

        public FrmMain()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new MenuStrip();
            this.mnuMusteri = new ToolStripMenuItem();
            this.mnuMusteriListesi = new ToolStripMenuItem();
            this.mnuHesap = new ToolStripMenuItem();
            this.mnuHesapIslemleri = new ToolStripMenuItem();
            this.mnuCikis = new ToolStripMenuItem();
            this.statusStrip1 = new StatusStrip();
            this.lblUser = new ToolStripStatusLabel();
            
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            
            // menuStrip1
            this.menuStrip1.ImageScalingSize = new Size(20, 20);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.mnuMusteri,
                this.mnuHesap,
                this.mnuCikis});
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(1200, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            
            // mnuMusteri
            this.mnuMusteri.DropDownItems.AddRange(new ToolStripItem[] {
                this.mnuMusteriListesi});
            this.mnuMusteri.Name = "mnuMusteri";
            this.mnuMusteri.Size = new Size(77, 24);
            this.mnuMusteri.Text = "Müşteri";
            
            // mnuMusteriListesi
            this.mnuMusteriListesi.Name = "mnuMusteriListesi";
            this.mnuMusteriListesi.Size = new Size(190, 26);
            this.mnuMusteriListesi.Text = "Müşteri Listesi";
            this.mnuMusteriListesi.Click += new EventHandler(this.mnuMusteriListesi_Click);
            
            // mnuHesap
            this.mnuHesap.DropDownItems.AddRange(new ToolStripItem[] {
                this.mnuHesapIslemleri});
            this.mnuHesap.Name = "mnuHesap";
            this.mnuHesap.Size = new Size(65, 24);
            this.mnuHesap.Text = "Hesap";
            
            // mnuHesapIslemleri
            this.mnuHesapIslemleri.Name = "mnuHesapIslemleri";
            this.mnuHesapIslemleri.Size = new Size(196, 26);
            this.mnuHesapIslemleri.Text = "Hesap İşlemleri";
            
            // mnuCikis
            this.mnuCikis.Name = "mnuCikis";
            this.mnuCikis.Size = new Size(53, 24);
            this.mnuCikis.Text = "Çıkış";
            this.mnuCikis.Click += new EventHandler(this.mnuCikis_Click);
            
            // statusStrip1
            this.statusStrip1.ImageScalingSize = new Size(20, 20);
            this.statusStrip1.Items.AddRange(new ToolStripItem[] {
                this.lblUser});
            this.statusStrip1.Location = new Point(0, 728);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(1200, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            
            // lblUser
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new Size(0, 16);
            
            // FrmMain
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 750);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "MetinBank - Ana Menü";
            this.WindowState = FormWindowState.Maximized;
            
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadUserInfo()
        {
            if (SessionManager.CurrentUser != null)
            {
                lblUser.Text = $"Kullanıcı: {SessionManager.CurrentUser.FullName} | Şube: {SessionManager.CurrentUser.BranchId ?? 0}";
            }
        }

        private void mnuMusteriListesi_Click(object? sender, EventArgs e)
        {
            // Ekran yetkisi kontrolü
            string? error = CheckScreenPermission("MUSTERI_LISTESI");
            if (error != null)
            {
                MessageBox.Show(error, "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Form zaten açık mı kontrol et
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is FrmMusteriListesi)
                {
                    frm.Activate();
                    return;
                }
            }

            // Yeni form aç
            FrmMusteriListesi frmMusteri = new FrmMusteriListesi();
            frmMusteri.MdiParent = this;
            frmMusteri.Show();
        }

        private void mnuCikis_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Çıkış", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.Logout();
                Application.Exit();
            }
        }

        private string? CheckScreenPermission(string screenCode)
        {
            if (!SessionManager.HasScreenPermission(screenCode))
            {
                return "Bu ekrana erişim yetkiniz bulunmamaktadır!";
            }
            return null;
        }
    }
}
