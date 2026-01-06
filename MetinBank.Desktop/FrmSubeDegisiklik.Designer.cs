namespace MetinBank.Desktop
{
    partial class FrmSubeDegisiklik
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnGonder = new DevExpress.XtraEditors.SimpleButton();
            this.memoTalepNedeni = new DevExpress.XtraEditors.MemoEdit();
            this.lookUpYeniSube = new DevExpress.XtraEditors.LookUpEdit();
            this.lblMevcutSube = new DevExpress.XtraEditors.LabelControl();
            this.panelTalepDurum = new DevExpress.XtraEditors.PanelControl();
            this.lblTalepDurumu = new DevExpress.XtraEditors.LabelControl();
            this.lblOnayTarihi = new DevExpress.XtraEditors.LabelControl();
            this.lblTalepTarihi = new DevExpress.XtraEditors.LabelControl();
            this.lblDurumBaslik = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoTalepNedeni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpYeniSube.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTalepDurum)).BeginInit();
            this.panelTalepDurum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnGonder);
            this.layoutControl1.Controls.Add(this.memoTalepNedeni);
            this.layoutControl1.Controls.Add(this.lookUpYeniSube);
            this.layoutControl1.Controls.Add(this.lblMevcutSube);
            this.layoutControl1.Controls.Add(this.panelTalepDurum);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(700, 500);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnGonder
            // 
            this.btnGonder.Location = new System.Drawing.Point(12, 260);
            this.btnGonder.Name = "btnGonder";
            this.btnGonder.Size = new System.Drawing.Size(676, 32);
            this.btnGonder.StyleController = this.layoutControl1;
            this.btnGonder.TabIndex = 8;
            this.btnGonder.Text = "Talep Gönder";
            this.btnGonder.Click += new System.EventHandler(this.BtnGonder_Click);
            // 
            // memoTalepNedeni
            // 
            this.memoTalepNedeni.Location = new System.Drawing.Point(12, 114);
            this.memoTalepNedeni.Name = "memoTalepNedeni";
            this.memoTalepNedeni.Size = new System.Drawing.Size(676, 142);
            this.memoTalepNedeni.StyleController = this.layoutControl1;
            this.memoTalepNedeni.TabIndex = 7;
            // 
            // lookUpYeniSube
            // 
            this.lookUpYeniSube.Location = new System.Drawing.Point(12, 70);
            this.lookUpYeniSube.Name = "lookUpYeniSube";
            this.lookUpYeniSube.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpYeniSube.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeKodu", "Kod", 60, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeAdi", "Şube Adı", 200, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sehir", "Şehir", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookUpYeniSube.Properties.DisplayMember = "SubeAdi";
            this.lookUpYeniSube.Properties.NullText = "Lütfen şube seçiniz...";
            this.lookUpYeniSube.Properties.ValueMember = "SubeID";
            this.lookUpYeniSube.Size = new System.Drawing.Size(676, 20);
            this.lookUpYeniSube.StyleController = this.layoutControl1;
            this.lookUpYeniSube.TabIndex = 6;
            // 
            // lblMevcutSube
            // 
            this.lblMevcutSube.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblMevcutSube.Appearance.Options.UseFont = true;
            this.lblMevcutSube.Location = new System.Drawing.Point(12, 26);
            this.lblMevcutSube.Name = "lblMevcutSube";
            this.lblMevcutSube.Size = new System.Drawing.Size(103, 16);
            this.lblMevcutSube.StyleController = this.layoutControl1;
            this.lblMevcutSube.TabIndex = 5;
            this.lblMevcutSube.Text = "Mevcut Şube: ";
            // 
            // panelTalepDurum
            // 
            this.panelTalepDurum.Controls.Add(this.lblTalepDurumu);
            this.panelTalepDurum.Controls.Add(this.lblOnayTarihi);
            this.panelTalepDurum.Controls.Add(this.lblTalepTarihi);
            this.panelTalepDurum.Controls.Add(this.lblDurumBaslik);
            this.panelTalepDurum.Location = new System.Drawing.Point(12, 296);
            this.panelTalepDurum.Name = "panelTalepDurum";
            this.panelTalepDurum.Size = new System.Drawing.Size(676, 192);
            this.panelTalepDurum.TabIndex = 4;
            this.panelTalepDurum.Visible = false;
            // 
            // lblTalepDurumu
            // 
            this.lblTalepDurumu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTalepDurumu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblTalepDurumu.Appearance.Options.UseFont = true;
            this.lblTalepDurumu.Appearance.Options.UseForeColor = true;
            this.lblTalepDurumu.Location = new System.Drawing.Point(15, 40);
            this.lblTalepDurumu.Name = "lblTalepDurumu";
            this.lblTalepDurumu.Size = new System.Drawing.Size(96, 14);
            this.lblTalepDurumu.TabIndex = 3;
            this.lblTalepDurumu.Text = "Durum: Beklemede";
            // 
            // lblOnayTarihi
            // 
            this.lblOnayTarihi.Location = new System.Drawing.Point(15, 85);
            this.lblOnayTarihi.Name = "lblOnayTarihi";
            this.lblOnayTarihi.Size = new System.Drawing.Size(64, 13);
            this.lblOnayTarihi.TabIndex = 2;
            this.lblOnayTarihi.Text = "Onay Tarihi: ";
            this.lblOnayTarihi.Visible = false;
            // 
            // lblTalepTarihi
            // 
            this.lblTalepTarihi.Location = new System.Drawing.Point(15, 63);
            this.lblTalepTarihi.Name = "lblTalepTarihi";
            this.lblTalepTarihi.Size = new System.Drawing.Size(64, 13);
            this.lblTalepTarihi.TabIndex = 1;
            this.lblTalepTarihi.Text = "Talep Tarihi: ";
            // 
            // lblDurumBaslik
            // 
            this.lblDurumBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDurumBaslik.Appearance.Options.UseFont = true;
            this.lblDurumBaslik.Location = new System.Drawing.Point(15, 12);
            this.lblDurumBaslik.Name = "lblDurumBaslik";
            this.lblDurumBaslik.Size = new System.Drawing.Size(151, 16);
            this.lblDurumBaslik.TabIndex = 0;
            this.lblDurumBaslik.Text = "Mevcut Talep Durumu";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(700, 500);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelTalepDurum;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 284);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(680, 196);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lblMevcutSube;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(680, 34);
            this.layoutControlItem2.Text = "Mevcut Şube Bilginiz";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lookUpYeniSube;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(680, 44);
            this.layoutControlItem3.Text = "Talep Ettiğiniz Şube";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(99, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.memoTalepNedeni;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(680, 170);
            this.layoutControlItem4.Text = "Talep Nedeni (En az 20 karakter)";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(99, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnGonder;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 248);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(680, 36);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(0, 0);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmSubeDegisiklik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmSubeDegisiklik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Şube Değişikliği Talebi";
            this.Load += new System.EventHandler(this.FrmSubeDegisiklik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.layoutControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoTalepNedeni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpYeniSube.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTalepDurum)).EndInit();
            this.panelTalepDurum.ResumeLayout(false);
            this.panelTalepDurum.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnGonder;
        private DevExpress.XtraEditors.MemoEdit memoTalepNedeni;
        private DevExpress.XtraEditors.LookUpEdit lookUpYeniSube;
        private DevExpress.XtraEditors.LabelControl lblMevcutSube;
        private DevExpress.XtraEditors.PanelControl panelTalepDurum;
        private DevExpress.XtraEditors.LabelControl lblTalepDurumu;
        private DevExpress.XtraEditors.LabelControl lblOnayTarihi;
        private DevExpress.XtraEditors.LabelControl lblTalepTarihi;
        private DevExpress.XtraEditors.LabelControl lblDurumBaslik;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
