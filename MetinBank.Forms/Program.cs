using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;

namespace MetinBank.Forms
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
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
            
            Application.Run(new FrmGiris());
        }
    }
}

