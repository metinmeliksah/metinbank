using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;

namespace MetinBank.Forms
{
    public partial class FrmBasvurular : XtraForm
    {
        private KullaniciModel _kullanici;

        public FrmBasvurular(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
        }

        private void FrmBasvurular_Load(object sender, EventArgs e)
        {
            this.Text = "Başvurular";
            // TODO: Başvuruları yükle
        }
    }
}

