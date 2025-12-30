using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;

namespace MetinBank.Forms
{
    public partial class FrmKartlar : XtraForm
    {
        private KullaniciModel _kullanici;

        public FrmKartlar(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
        }

        private void FrmKartlar_Load(object sender, EventArgs e)
        {
            this.Text = "Kartlar";
            // TODO: Kart listesini yükle
        }
    }
}

