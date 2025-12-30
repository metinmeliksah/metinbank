using System;
using System.Windows.Forms;
using MetinBank.Models;
using DevExpress.XtraBars.Ribbon;

namespace MetinBank.Forms
{
    public partial class FrmAnaSayfa : RibbonForm
    {
        private KullaniciModel _kullanici;

        public FrmAnaSayfa(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini göster
            barStaticKullanici.Caption = $"Kullanıcı: {_kullanici.TamAd}";
            barStaticRol.Caption = $"Rol: {_kullanici.RolAdi}";
            barStaticSube.Caption = $"Şube: {(_kullanici.SubeAdi ?? "Genel Merkez")}";
            
            // Yetkilere göre menü öğelerini gizle/göster
            YetkiKontrol();
        }

        private void YetkiKontrol()
        {
            // Yönetim sayfası sadece Müdür ve Genel Müdür için
            if (_kullanici.RolID == 3) // Şube Çalışanı
            {
                ribbonPageYonetim.Visible = false;
            }
        }

        private void BtnMusteriIslem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMusteriIslem frm = new FrmMusteriIslem(_kullanici);
            frm.MdiParent = this;
            frm.Show();
        }

        private void BtnHesapIslem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmHesapIslem frm = new FrmHesapIslem(_kullanici);
            frm.MdiParent = this;
            frm.Show();
        }

        private void BtnParaYatir_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmParaYatir frm = new FrmParaYatir(_kullanici);
            frm.MdiParent = this;
            frm.Show();
        }

        private void BtnParaCek_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmParaCek frm = new FrmParaCek(_kullanici);
            frm.MdiParent = this;
            frm.Show();
        }

        private void BtnHavaleEFT_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmHavaleEFT frm = new FrmHavaleEFT(_kullanici);
            frm.MdiParent = this;
            frm.Show();
        }

        private void BtnKartlar_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmKartlar frm = new FrmKartlar(_kullanici);
            frm.MdiParent = this;
            frm.Show();
        }

        private void BtnBasvurular_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmBasvurular frm = new FrmBasvurular(_kullanici);
            frm.MdiParent = this;
            frm.Show();
        }

        private void BtnOnayBekleyenler_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmOnayBekleyenler frm = new FrmOnayBekleyenler(_kullanici);
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
