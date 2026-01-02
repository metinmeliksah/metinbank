namespace MetinBank.Desktop
{
    partial class FrmMusteriEkle
    {
        private System.ComponentModel.IContainer components = null;
        
        // Layout Control
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        
        // Kişisel Bilgiler
        private DevExpress.XtraEditors.TextEdit txtTCKN;
        private DevExpress.XtraEditors.TextEdit txtAd;
        private DevExpress.XtraEditors.TextEdit txtSoyad;
        private DevExpress.XtraEditors.DateEdit dtDogumTarihi;
        private DevExpress.XtraEditors.TextEdit txtDogumYeri;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCinsiyet;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMedeniDurum;
        private DevExpress.XtraEditors.TextEdit txtAnneAdi;
        private DevExpress.XtraEditors.TextEdit txtBabaAdi;
        
        // İletişim Bilgileri
        private DevExpress.XtraEditors.TextEdit txtTelefon;
        private DevExpress.XtraEditors.TextEdit txtCepTelefon;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.MemoEdit txtAdres;
        private DevExpress.XtraEditors.TextEdit txtIl;
        private DevExpress.XtraEditors.TextEdit txtIlce;
        private DevExpress.XtraEditors.TextEdit txtPostaKodu;
        
        // Müşteri Bilgileri
        private DevExpress.XtraEditors.ComboBoxEdit cmbMusteriTipi;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMusteriSegmenti;
        private DevExpress.XtraEditors.TextEdit txtMeslekBilgisi;
        private DevExpress.XtraEditors.SpinEdit numGelirDurumu;
        
        // Butonlar
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
        
        // Layout Items
        private DevExpress.XtraLayout.LayoutControlGroup grpKisiselBilgiler;
        private DevExpress.XtraLayout.LayoutControlGroup grpIletisimBilgileri;
        private DevExpress.XtraLayout.LayoutControlGroup grpMusteriBilgileri;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTCKN;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAd;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemSoyad;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemDogumTarihi;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemDogumYeri;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemCinsiyet;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMedeniDurum;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAnneAdi;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemBabaAdi;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemTelefon;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemCepTelefon;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemEmail;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemAdres;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemIl;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemIlce;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemPostaKodu;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriTipi;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMusteriSegmenti;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemMeslekBilgisi;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGelirDurumu;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemKaydet;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemIptal;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtTCKN = new DevExpress.XtraEditors.TextEdit();
            this.txtAd = new DevExpress.XtraEditors.TextEdit();
            this.txtSoyad = new DevExpress.XtraEditors.TextEdit();
            this.dtDogumTarihi = new DevExpress.XtraEditors.DateEdit();
            this.txtDogumYeri = new DevExpress.XtraEditors.TextEdit();
            this.cmbCinsiyet = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbMedeniDurum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtAnneAdi = new DevExpress.XtraEditors.TextEdit();
            this.txtBabaAdi = new DevExpress.XtraEditors.TextEdit();
            this.txtTelefon = new DevExpress.XtraEditors.TextEdit();
            this.txtCepTelefon = new DevExpress.XtraEditors.TextEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtAdres = new DevExpress.XtraEditors.MemoEdit();
            this.txtIl = new DevExpress.XtraEditors.TextEdit();
            this.txtIlce = new DevExpress.XtraEditors.TextEdit();
            this.txtPostaKodu = new DevExpress.XtraEditors.TextEdit();
            this.cmbMusteriTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbMusteriSegmenti = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtMeslekBilgisi = new DevExpress.XtraEditors.TextEdit();
            this.numGelirDurumu = new DevExpress.XtraEditors.SpinEdit();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpKisiselBilgiler = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpIletisimBilgileri = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grpMusteriBilgileri = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemTCKN = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAd = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemSoyad = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemDogumTarihi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemDogumYeri = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemCinsiyet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemMedeniDurum = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAnneAdi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemBabaAdi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemTelefon = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemCepTelefon = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemEmail = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemAdres = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemIl = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemIlce = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemPostaKodu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemMusteriTipi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemMusteriSegmenti = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemMeslekBilgisi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGelirDurumu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemKaydet = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemIptal = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTCKN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDogumTarihi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDogumTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDogumYeri.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCinsiyet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMedeniDurum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnneAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBabaAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCepTelefon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdres.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIlce.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostaKodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMusteriTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMusteriSegmenti.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeslekBilgisi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGelirDurumu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpKisiselBilgiler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIletisimBilgileri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMusteriBilgileri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTCKN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSoyad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDogumTarihi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDogumYeri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCinsiyet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMedeniDurum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAnneAdi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBabaAdi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTelefon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCepTelefon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAdres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIlce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemPostaKodu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriTipi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriSegmenti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMeslekBilgisi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGelirDurumu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKaydet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIptal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            
            // layoutControl1
            this.layoutControl1.Controls.Add(this.txtTCKN);
            this.layoutControl1.Controls.Add(this.txtAd);
            this.layoutControl1.Controls.Add(this.txtSoyad);
            this.layoutControl1.Controls.Add(this.dtDogumTarihi);
            this.layoutControl1.Controls.Add(this.txtDogumYeri);
            this.layoutControl1.Controls.Add(this.cmbCinsiyet);
            this.layoutControl1.Controls.Add(this.cmbMedeniDurum);
            this.layoutControl1.Controls.Add(this.txtAnneAdi);
            this.layoutControl1.Controls.Add(this.txtBabaAdi);
            this.layoutControl1.Controls.Add(this.txtTelefon);
            this.layoutControl1.Controls.Add(this.txtCepTelefon);
            this.layoutControl1.Controls.Add(this.txtEmail);
            this.layoutControl1.Controls.Add(this.txtAdres);
            this.layoutControl1.Controls.Add(this.txtIl);
            this.layoutControl1.Controls.Add(this.txtIlce);
            this.layoutControl1.Controls.Add(this.txtPostaKodu);
            this.layoutControl1.Controls.Add(this.cmbMusteriTipi);
            this.layoutControl1.Controls.Add(this.cmbMusteriSegmenti);
            this.layoutControl1.Controls.Add(this.txtMeslekBilgisi);
            this.layoutControl1.Controls.Add(this.numGelirDurumu);
            this.layoutControl1.Controls.Add(this.btnKaydet);
            this.layoutControl1.Controls.Add(this.btnIptal);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(900, 700);
            this.layoutControl1.TabIndex = 0;
            
            // txtTCKN
            this.txtTCKN.Location = new System.Drawing.Point(110, 45);
            this.txtTCKN.Name = "txtTCKN";
            this.txtTCKN.Properties.MaxLength = 11;
            this.txtTCKN.Properties.Mask.EditMask = "\\d{11}";
            this.txtTCKN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtTCKN.Size = new System.Drawing.Size(200, 20);
            this.txtTCKN.StyleController = this.layoutControl1;
            this.txtTCKN.TabIndex = 1;
            
            // txtAd
            this.txtAd.Location = new System.Drawing.Point(110, 69);
            this.txtAd.Name = "txtAd";
            this.txtAd.Properties.MaxLength = 50;
            this.txtAd.Size = new System.Drawing.Size(200, 20);
            this.txtAd.StyleController = this.layoutControl1;
            this.txtAd.TabIndex = 2;
            
            // txtSoyad
            this.txtSoyad.Location = new System.Drawing.Point(410, 69);
            this.txtSoyad.Name = "txtSoyad";
            this.txtSoyad.Properties.MaxLength = 50;
            this.txtSoyad.Size = new System.Drawing.Size(200, 20);
            this.txtSoyad.StyleController = this.layoutControl1;
            this.txtSoyad.TabIndex = 3;
            
            // dtDogumTarihi
            this.dtDogumTarihi.Location = new System.Drawing.Point(110, 93);
            this.dtDogumTarihi.Name = "dtDogumTarihi";
            this.dtDogumTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDogumTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDogumTarihi.Size = new System.Drawing.Size(200, 20);
            this.dtDogumTarihi.StyleController = this.layoutControl1;
            this.dtDogumTarihi.TabIndex = 4;
            
            // txtDogumYeri
            this.txtDogumYeri.Location = new System.Drawing.Point(410, 93);
            this.txtDogumYeri.Name = "txtDogumYeri";
            this.txtDogumYeri.Properties.MaxLength = 50;
            this.txtDogumYeri.Size = new System.Drawing.Size(200, 20);
            this.txtDogumYeri.StyleController = this.layoutControl1;
            this.txtDogumYeri.TabIndex = 5;
            
            // cmbCinsiyet
            this.cmbCinsiyet.Location = new System.Drawing.Point(110, 117);
            this.cmbCinsiyet.Name = "cmbCinsiyet";
            this.cmbCinsiyet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCinsiyet.Properties.Items.AddRange(new object[] { "Erkek", "Kadın" });
            this.cmbCinsiyet.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbCinsiyet.Size = new System.Drawing.Size(200, 20);
            this.cmbCinsiyet.StyleController = this.layoutControl1;
            this.cmbCinsiyet.TabIndex = 6;
            
            // cmbMedeniDurum
            this.cmbMedeniDurum.Location = new System.Drawing.Point(410, 117);
            this.cmbMedeniDurum.Name = "cmbMedeniDurum";
            this.cmbMedeniDurum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMedeniDurum.Properties.Items.AddRange(new object[] { "Bekar", "Evli", "Boşanmış", "Dul" });
            this.cmbMedeniDurum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMedeniDurum.Size = new System.Drawing.Size(200, 20);
            this.cmbMedeniDurum.StyleController = this.layoutControl1;
            this.cmbMedeniDurum.TabIndex = 7;
            
            // txtAnneAdi
            this.txtAnneAdi.Location = new System.Drawing.Point(110, 141);
            this.txtAnneAdi.Name = "txtAnneAdi";
            this.txtAnneAdi.Properties.MaxLength = 50;
            this.txtAnneAdi.Size = new System.Drawing.Size(200, 20);
            this.txtAnneAdi.StyleController = this.layoutControl1;
            this.txtAnneAdi.TabIndex = 8;
            
            // txtBabaAdi
            this.txtBabaAdi.Location = new System.Drawing.Point(410, 141);
            this.txtBabaAdi.Name = "txtBabaAdi";
            this.txtBabaAdi.Properties.MaxLength = 50;
            this.txtBabaAdi.Size = new System.Drawing.Size(200, 20);
            this.txtBabaAdi.StyleController = this.layoutControl1;
            this.txtBabaAdi.TabIndex = 9;
            
            // txtTelefon
            this.txtTelefon.Location = new System.Drawing.Point(110, 210);
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Properties.Mask.EditMask = "+90 (000) 000-0000";
            this.txtTelefon.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtTelefon.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTelefon.Size = new System.Drawing.Size(200, 20);
            this.txtTelefon.StyleController = this.layoutControl1;
            this.txtTelefon.TabIndex = 10;
            
            // txtCepTelefon
            this.txtCepTelefon.Location = new System.Drawing.Point(410, 210);
            this.txtCepTelefon.Name = "txtCepTelefon";
            this.txtCepTelefon.Properties.Mask.EditMask = "+90 (000) 000-0000";
            this.txtCepTelefon.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.txtCepTelefon.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCepTelefon.Size = new System.Drawing.Size(200, 20);
            this.txtCepTelefon.StyleController = this.layoutControl1;
            this.txtCepTelefon.TabIndex = 11;
            
            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(110, 234);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.MaxLength = 100;
            this.txtEmail.Size = new System.Drawing.Size(500, 20);
            this.txtEmail.StyleController = this.layoutControl1;
            this.txtEmail.TabIndex = 12;
            
            // txtAdres
            this.txtAdres.Location = new System.Drawing.Point(110, 258);
            this.txtAdres.Name = "txtAdres";
            this.txtAdres.Properties.MaxLength = 500;
            this.txtAdres.Size = new System.Drawing.Size(500, 60);
            this.txtAdres.StyleController = this.layoutControl1;
            this.txtAdres.TabIndex = 13;
            
            // txtIl
            this.txtIl.Location = new System.Drawing.Point(110, 322);
            this.txtIl.Name = "txtIl";
            this.txtIl.Properties.MaxLength = 50;
            this.txtIl.Size = new System.Drawing.Size(150, 20);
            this.txtIl.StyleController = this.layoutControl1;
            this.txtIl.TabIndex = 14;
            
            // txtIlce
            this.txtIlce.Location = new System.Drawing.Point(360, 322);
            this.txtIlce.Name = "txtIlce";
            this.txtIlce.Properties.MaxLength = 50;
            this.txtIlce.Size = new System.Drawing.Size(150, 20);
            this.txtIlce.StyleController = this.layoutControl1;
            this.txtIlce.TabIndex = 15;
            
            // txtPostaKodu
            this.txtPostaKodu.Location = new System.Drawing.Point(610, 322);
            this.txtPostaKodu.Name = "txtPostaKodu";
            this.txtPostaKodu.Properties.MaxLength = 10;
            this.txtPostaKodu.Size = new System.Drawing.Size(100, 20);
            this.txtPostaKodu.StyleController = this.layoutControl1;
            this.txtPostaKodu.TabIndex = 16;
            
            // cmbMusteriTipi
            this.cmbMusteriTipi.Location = new System.Drawing.Point(110, 390);
            this.cmbMusteriTipi.Name = "cmbMusteriTipi";
            this.cmbMusteriTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMusteriTipi.Properties.Items.AddRange(new object[] { "Bireysel", "Kurumsal" });
            this.cmbMusteriTipi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMusteriTipi.Size = new System.Drawing.Size(200, 20);
            this.cmbMusteriTipi.StyleController = this.layoutControl1;
            this.cmbMusteriTipi.TabIndex = 17;
            
            // cmbMusteriSegmenti
            this.cmbMusteriSegmenti.Location = new System.Drawing.Point(410, 390);
            this.cmbMusteriSegmenti.Name = "cmbMusteriSegmenti";
            this.cmbMusteriSegmenti.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMusteriSegmenti.Properties.Items.AddRange(new object[] { "Standart", "Premium", "VIP" });
            this.cmbMusteriSegmenti.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMusteriSegmenti.Size = new System.Drawing.Size(200, 20);
            this.cmbMusteriSegmenti.StyleController = this.layoutControl1;
            this.cmbMusteriSegmenti.TabIndex = 18;
            
            // txtMeslekBilgisi
            this.txtMeslekBilgisi.Location = new System.Drawing.Point(110, 414);
            this.txtMeslekBilgisi.Name = "txtMeslekBilgisi";
            this.txtMeslekBilgisi.Properties.MaxLength = 100;
            this.txtMeslekBilgisi.Size = new System.Drawing.Size(300, 20);
            this.txtMeslekBilgisi.StyleController = this.layoutControl1;
            this.txtMeslekBilgisi.TabIndex = 19;
            
            // numGelirDurumu
            this.numGelirDurumu.Location = new System.Drawing.Point(510, 414);
            this.numGelirDurumu.Name = "numGelirDurumu";
            this.numGelirDurumu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numGelirDurumu.Properties.DisplayFormat.FormatString = "N2";
            this.numGelirDurumu.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numGelirDurumu.Properties.EditFormat.FormatString = "N2";
            this.numGelirDurumu.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numGelirDurumu.Properties.MaxValue = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numGelirDurumu.Size = new System.Drawing.Size(200, 20);
            this.numGelirDurumu.StyleController = this.layoutControl1;
            this.numGelirDurumu.TabIndex = 20;
            
            // btnKaydet
            this.btnKaydet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Appearance.Options.UseBackColor = true;
            this.btnKaydet.Appearance.Options.UseFont = true;
            this.btnKaydet.Appearance.Options.UseForeColor = true;
            this.btnKaydet.Location = new System.Drawing.Point(12, 650);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(430, 38);
            this.btnKaydet.StyleController = this.layoutControl1;
            this.btnKaydet.TabIndex = 21;
            this.btnKaydet.Text = "💾  MÜŞTERİ KAYDET";
            this.btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            
            // btnIptal
            this.btnIptal.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnIptal.Appearance.Options.UseFont = true;
            this.btnIptal.Location = new System.Drawing.Point(446, 650);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(442, 38);
            this.btnIptal.StyleController = this.layoutControl1;
            this.btnIptal.TabIndex = 22;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            
            // layoutControlGroup1
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.grpKisiselBilgiler,
                this.grpIletisimBilgileri,
                this.grpMusteriBilgileri,
                this.layoutItemKaydet,
                this.layoutItemIptal,
                this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(900, 700);
            this.layoutControlGroup1.TextVisible = false;
            
            // grpKisiselBilgiler
            this.grpKisiselBilgiler.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpKisiselBilgiler.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpKisiselBilgiler.AppearanceGroup.Options.UseFont = true;
            this.grpKisiselBilgiler.AppearanceGroup.Options.UseForeColor = true;
            this.grpKisiselBilgiler.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemTCKN,
                this.layoutItemAd,
                this.layoutItemSoyad,
                this.layoutItemDogumTarihi,
                this.layoutItemDogumYeri,
                this.layoutItemCinsiyet,
                this.layoutItemMedeniDurum,
                this.layoutItemAnneAdi,
                this.layoutItemBabaAdi});
            this.grpKisiselBilgiler.Location = new System.Drawing.Point(0, 0);
            this.grpKisiselBilgiler.Name = "grpKisiselBilgiler";
            this.grpKisiselBilgiler.Size = new System.Drawing.Size(880, 175);
            this.grpKisiselBilgiler.Text = "👤 Kişisel Bilgiler";
            
            // grpIletisimBilgileri
            this.grpIletisimBilgileri.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpIletisimBilgileri.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpIletisimBilgileri.AppearanceGroup.Options.UseFont = true;
            this.grpIletisimBilgileri.AppearanceGroup.Options.UseForeColor = true;
            this.grpIletisimBilgileri.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemTelefon,
                this.layoutItemCepTelefon,
                this.layoutItemEmail,
                this.layoutItemAdres,
                this.layoutItemIl,
                this.layoutItemIlce,
                this.layoutItemPostaKodu});
            this.grpIletisimBilgileri.Location = new System.Drawing.Point(0, 175);
            this.grpIletisimBilgileri.Name = "grpIletisimBilgileri";
            this.grpIletisimBilgileri.Size = new System.Drawing.Size(880, 185);
            this.grpIletisimBilgileri.Text = "📞 İletişim Bilgileri";
            
            // grpMusteriBilgileri
            this.grpMusteriBilgileri.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpMusteriBilgileri.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.grpMusteriBilgileri.AppearanceGroup.Options.UseFont = true;
            this.grpMusteriBilgileri.AppearanceGroup.Options.UseForeColor = true;
            this.grpMusteriBilgileri.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                this.layoutItemMusteriTipi,
                this.layoutItemMusteriSegmenti,
                this.layoutItemMeslekBilgisi,
                this.layoutItemGelirDurumu});
            this.grpMusteriBilgileri.Location = new System.Drawing.Point(0, 360);
            this.grpMusteriBilgileri.Name = "grpMusteriBilgileri";
            this.grpMusteriBilgileri.Size = new System.Drawing.Size(880, 100);
            this.grpMusteriBilgileri.Text = "🏦 Müşteri Bilgileri";
            
            // Layout Items
            this.layoutItemTCKN.Control = this.txtTCKN;
            this.layoutItemTCKN.Location = new System.Drawing.Point(0, 0);
            this.layoutItemTCKN.Name = "layoutItemTCKN";
            this.layoutItemTCKN.Size = new System.Drawing.Size(440, 24);
            this.layoutItemTCKN.Text = "TCKN:";
            this.layoutItemTCKN.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemAd.Control = this.txtAd;
            this.layoutItemAd.Location = new System.Drawing.Point(0, 24);
            this.layoutItemAd.Name = "layoutItemAd";
            this.layoutItemAd.Size = new System.Drawing.Size(220, 24);
            this.layoutItemAd.Text = "Ad:";
            this.layoutItemAd.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemSoyad.Control = this.txtSoyad;
            this.layoutItemSoyad.Location = new System.Drawing.Point(220, 24);
            this.layoutItemSoyad.Name = "layoutItemSoyad";
            this.layoutItemSoyad.Size = new System.Drawing.Size(220, 24);
            this.layoutItemSoyad.Text = "Soyad:";
            this.layoutItemSoyad.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemDogumTarihi.Control = this.dtDogumTarihi;
            this.layoutItemDogumTarihi.Location = new System.Drawing.Point(0, 48);
            this.layoutItemDogumTarihi.Name = "layoutItemDogumTarihi";
            this.layoutItemDogumTarihi.Size = new System.Drawing.Size(220, 24);
            this.layoutItemDogumTarihi.Text = "Doğum Tarihi:";
            this.layoutItemDogumTarihi.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemDogumYeri.Control = this.txtDogumYeri;
            this.layoutItemDogumYeri.Location = new System.Drawing.Point(220, 48);
            this.layoutItemDogumYeri.Name = "layoutItemDogumYeri";
            this.layoutItemDogumYeri.Size = new System.Drawing.Size(220, 24);
            this.layoutItemDogumYeri.Text = "Doğum Yeri:";
            this.layoutItemDogumYeri.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemCinsiyet.Control = this.cmbCinsiyet;
            this.layoutItemCinsiyet.Location = new System.Drawing.Point(0, 72);
            this.layoutItemCinsiyet.Name = "layoutItemCinsiyet";
            this.layoutItemCinsiyet.Size = new System.Drawing.Size(220, 24);
            this.layoutItemCinsiyet.Text = "Cinsiyet:";
            this.layoutItemCinsiyet.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemMedeniDurum.Control = this.cmbMedeniDurum;
            this.layoutItemMedeniDurum.Location = new System.Drawing.Point(220, 72);
            this.layoutItemMedeniDurum.Name = "layoutItemMedeniDurum";
            this.layoutItemMedeniDurum.Size = new System.Drawing.Size(220, 24);
            this.layoutItemMedeniDurum.Text = "Medeni Durum:";
            this.layoutItemMedeniDurum.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemAnneAdi.Control = this.txtAnneAdi;
            this.layoutItemAnneAdi.Location = new System.Drawing.Point(0, 96);
            this.layoutItemAnneAdi.Name = "layoutItemAnneAdi";
            this.layoutItemAnneAdi.Size = new System.Drawing.Size(220, 24);
            this.layoutItemAnneAdi.Text = "Anne Adı:";
            this.layoutItemAnneAdi.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemBabaAdi.Control = this.txtBabaAdi;
            this.layoutItemBabaAdi.Location = new System.Drawing.Point(220, 96);
            this.layoutItemBabaAdi.Name = "layoutItemBabaAdi";
            this.layoutItemBabaAdi.Size = new System.Drawing.Size(220, 24);
            this.layoutItemBabaAdi.Text = "Baba Adı:";
            this.layoutItemBabaAdi.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemTelefon.Control = this.txtTelefon;
            this.layoutItemTelefon.Location = new System.Drawing.Point(0, 0);
            this.layoutItemTelefon.Name = "layoutItemTelefon";
            this.layoutItemTelefon.Size = new System.Drawing.Size(220, 24);
            this.layoutItemTelefon.Text = "Telefon:";
            this.layoutItemTelefon.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemCepTelefon.Control = this.txtCepTelefon;
            this.layoutItemCepTelefon.Location = new System.Drawing.Point(220, 0);
            this.layoutItemCepTelefon.Name = "layoutItemCepTelefon";
            this.layoutItemCepTelefon.Size = new System.Drawing.Size(220, 24);
            this.layoutItemCepTelefon.Text = "Cep Telefon:";
            this.layoutItemCepTelefon.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemEmail.Control = this.txtEmail;
            this.layoutItemEmail.Location = new System.Drawing.Point(0, 24);
            this.layoutItemEmail.Name = "layoutItemEmail";
            this.layoutItemEmail.Size = new System.Drawing.Size(440, 24);
            this.layoutItemEmail.Text = "Email:";
            this.layoutItemEmail.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemAdres.Control = this.txtAdres;
            this.layoutItemAdres.Location = new System.Drawing.Point(0, 48);
            this.layoutItemAdres.Name = "layoutItemAdres";
            this.layoutItemAdres.Size = new System.Drawing.Size(440, 64);
            this.layoutItemAdres.Text = "Adres:";
            this.layoutItemAdres.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemIl.Control = this.txtIl;
            this.layoutItemIl.Location = new System.Drawing.Point(0, 112);
            this.layoutItemIl.Name = "layoutItemIl";
            this.layoutItemIl.Size = new System.Drawing.Size(150, 24);
            this.layoutItemIl.Text = "İl:";
            this.layoutItemIl.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemIlce.Control = this.txtIlce;
            this.layoutItemIlce.Location = new System.Drawing.Point(150, 112);
            this.layoutItemIlce.Name = "layoutItemIlce";
            this.layoutItemIlce.Size = new System.Drawing.Size(150, 24);
            this.layoutItemIlce.Text = "İlçe:";
            this.layoutItemIlce.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemPostaKodu.Control = this.txtPostaKodu;
            this.layoutItemPostaKodu.Location = new System.Drawing.Point(300, 112);
            this.layoutItemPostaKodu.Name = "layoutItemPostaKodu";
            this.layoutItemPostaKodu.Size = new System.Drawing.Size(140, 24);
            this.layoutItemPostaKodu.Text = "Posta Kodu:";
            this.layoutItemPostaKodu.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemMusteriTipi.Control = this.cmbMusteriTipi;
            this.layoutItemMusteriTipi.Location = new System.Drawing.Point(0, 0);
            this.layoutItemMusteriTipi.Name = "layoutItemMusteriTipi";
            this.layoutItemMusteriTipi.Size = new System.Drawing.Size(220, 24);
            this.layoutItemMusteriTipi.Text = "Müşteri Tipi:";
            this.layoutItemMusteriTipi.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemMusteriSegmenti.Control = this.cmbMusteriSegmenti;
            this.layoutItemMusteriSegmenti.Location = new System.Drawing.Point(220, 0);
            this.layoutItemMusteriSegmenti.Name = "layoutItemMusteriSegmenti";
            this.layoutItemMusteriSegmenti.Size = new System.Drawing.Size(220, 24);
            this.layoutItemMusteriSegmenti.Text = "Segment:";
            this.layoutItemMusteriSegmenti.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemMeslekBilgisi.Control = this.txtMeslekBilgisi;
            this.layoutItemMeslekBilgisi.Location = new System.Drawing.Point(0, 24);
            this.layoutItemMeslekBilgisi.Name = "layoutItemMeslekBilgisi";
            this.layoutItemMeslekBilgisi.Size = new System.Drawing.Size(220, 24);
            this.layoutItemMeslekBilgisi.Text = "Meslek:";
            this.layoutItemMeslekBilgisi.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemGelirDurumu.Control = this.numGelirDurumu;
            this.layoutItemGelirDurumu.Location = new System.Drawing.Point(220, 24);
            this.layoutItemGelirDurumu.Name = "layoutItemGelirDurumu";
            this.layoutItemGelirDurumu.Size = new System.Drawing.Size(220, 24);
            this.layoutItemGelirDurumu.Text = "Gelir (TL):";
            this.layoutItemGelirDurumu.TextSize = new System.Drawing.Size(85, 13);
            
            this.layoutItemKaydet.Control = this.btnKaydet;
            this.layoutItemKaydet.Location = new System.Drawing.Point(0, 638);
            this.layoutItemKaydet.Name = "layoutItemKaydet";
            this.layoutItemKaydet.Size = new System.Drawing.Size(434, 42);
            this.layoutItemKaydet.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemKaydet.TextVisible = false;
            
            this.layoutItemIptal.Control = this.btnIptal;
            this.layoutItemIptal.Location = new System.Drawing.Point(434, 638);
            this.layoutItemIptal.Name = "layoutItemIptal";
            this.layoutItemIptal.Size = new System.Drawing.Size(446, 42);
            this.layoutItemIptal.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemIptal.TextVisible = false;
            
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 460);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(880, 178);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            
            // FrmMusteriEkle
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmMusteriEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Müşteri Ekle";
            this.Load += new System.EventHandler(this.FrmMusteriEkle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTCKN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDogumTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDogumTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDogumYeri.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCinsiyet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMedeniDurum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnneAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBabaAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCepTelefon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdres.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIlce.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostaKodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMusteriTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMusteriSegmenti.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeslekBilgisi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGelirDurumu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpKisiselBilgiler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIletisimBilgileri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpMusteriBilgileri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTCKN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemSoyad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDogumTarihi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemDogumYeri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCinsiyet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMedeniDurum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAnneAdi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBabaAdi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemTelefon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemCepTelefon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemAdres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIlce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemPostaKodu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriTipi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMusteriSegmenti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemMeslekBilgisi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGelirDurumu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemKaydet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemIptal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
