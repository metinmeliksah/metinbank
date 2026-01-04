using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;

namespace MetinBank.Desktop
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girişi
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // DevExpress Tema Ayarları
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            Application.Run(new FrmGiris());
        }
    }
}

