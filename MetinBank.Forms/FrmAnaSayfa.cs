using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;

namespace MetinBank.Forms
{
    public partial class FrmAnaSayfa : XtraForm
    {
        private KullaniciModel _kullanici;
        private SAuth _sAuth;

        public FrmAnaSayfa(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sAuth = new SAuth();
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            this.Text = $"Metin Bank - Ana Sayfa - {_kullanici.TamAd}";
            lblHosgeldin.Text = $"Hoş geldiniz, {_kullanici.TamAd}";
            lblRol.Text = $"Rol: {_kullanici.RolAdi}";
            lblSube.Text = $"Şube: {_kullanici.SubeAdi ?? "Genel Merkez"}";

            // Rol bazlı buton görünürlüğü
            bool calisanMi = _kullanici.RolID >= 2;
            bool mudurMu = _kullanici.RolID >= 3;
            bool genelMerkezMi = _kullanici.RolID >= 4;

            btnMusteriIslem.Visible = calisanMi;
            btnHesapIslem.Visible = calisanMi;
            btnParaYatir.Visible = calisanMi;
            btnParaCek.Visible = calisanMi;
            btnHavaleEFT.Visible = calisanMi;
            btnOnayBekleyenler.Visible = mudurMu;
        }

        private void BtnMusteriIslem_Click(object sender, EventArgs e)
        {
            FrmMusteriIslem frm = new FrmMusteriIslem(_kullanici);
            frm.ShowDialog();
        }

        private void BtnHesapIslem_Click(object sender, EventArgs e)
        {
            FrmHesapIslem frm = new FrmHesapIslem(_kullanici);
            frm.ShowDialog();
        }

        private void BtnParaYatir_Click(object sender, EventArgs e)
        {
            FrmParaYatir frm = new FrmParaYatir(_kullanici);
            frm.ShowDialog();
        }

        private void BtnParaCek_Click(object sender, EventArgs e)
        {
            FrmParaCek frm = new FrmParaCek(_kullanici);
            frm.ShowDialog();
        }

        private void BtnHavaleEFT_Click(object sender, EventArgs e)
        {
            FrmHavaleEFT frm = new FrmHavaleEFT(_kullanici);
            frm.ShowDialog();
        }

        private void BtnOnayBekleyenler_Click(object sender, EventArgs e)
        {
            FrmOnayBekleyenler frm = new FrmOnayBekleyenler(_kullanici);
            frm.ShowDialog();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            _sAuth.Logout(_kullanici.KullaniciID, Util.CommonFunctions.GetLocalIPAddress());
            this.Close();
        }
    }
}
            