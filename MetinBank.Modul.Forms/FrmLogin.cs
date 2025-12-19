using System;
using System.Windows.Forms;
using MetinBank.Entities;
using MetinBank.Modul.Service;

namespace MetinBank.Modul.Forms
{
    /// <summary>
    /// Login formu
    /// NOT: DevExpress kurulumu yapıldığında XtraForm'dan türetilecek
    /// </summary>
    public partial class FrmLogin : Form
    {
        private readonly UserService _userService;

        // DevExpress kontrolleri (kurulum sonrası kullanılacak)
        // private PictureEdit pctLogo;
        // private TextEdit txtUserName;
        // private TextEdit txtPassword;
        // private SimpleButton btnLogin;

        // Geçici standart kontroller
        private TextBox txtUserName;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTitle;
        private Label lblUserName;
        private Label lblPassword;

        public FrmLogin()
        {
            _userService = new UserService();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblUserName = new Label();
            this.lblPassword = new Label();
            this.txtUserName = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(100, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(200, 30);
            this.lblTitle.Text = "MetinBank Giriş";
            
            // lblUserName
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new Point(50, 90);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new Size(100, 20);
            this.lblUserName.Text = "Kullanıcı Adı:";
            
            // txtUserName
            this.txtUserName.Location = new Point(160, 87);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new Size(200, 27);
            this.txtUserName.TabIndex = 0;
            
            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(50, 130);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(50, 20);
            this.lblPassword.Text = "Şifre:";
            
            // txtPassword
            this.txtPassword.Location = new Point(160, 127);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(200, 27);
            this.txtPassword.TabIndex = 1;
            
            // btnLogin
            this.btnLogin.Location = new Point(160, 180);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(200, 35);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Giriş Yap";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            
            // FrmLogin
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(420, 250);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "MetinBank - Kullanıcı Girişi";
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            string? error = _userService.Login(userName, password, out User? user);

            if (error != null)
            {
                MessageBox.Show(error, "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kullanıcı bilgilerini session'a kaydet
            SessionManager.CurrentUser = user;

            // Kullanıcının ekran yetkilerini al
            error = _userService.GetUserScreens(user!.UserId, out List<UserScreen>? screens);
            
            if (error != null)
            {
                MessageBox.Show(error, "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SessionManager.UserScreens = screens;

            // Ana forma geç
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
