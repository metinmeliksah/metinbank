namespace MetinBank.Modul.Forms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // Login formunu aç
            FrmLogin frmLogin = new FrmLogin();
            
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                // Başarılı giriş - Ana formu aç
                Application.Run(new FrmMain());
            }
        }
    }
}
