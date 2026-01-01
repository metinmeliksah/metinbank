namespace MetinBank.Desktop
{
    partial class FrmMusteriIslem
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtArama;
        private DevExpress.XtraEditors.SimpleButton btnAra;
        private DevExpress.XtraEditors.SimpleButton btnYeniMusteri;
        private DevExpress.XtraEditors.SimpleButton btnDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraGrid.GridControl dgvMusteriler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMusteriler;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemArama;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemBtnAra;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemBtnYeniMusteri;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemBtnDuzenle;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemBtnKapat;
        private DevExpress.XtraLayout.LayoutControlItem layoutItemGrid;

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
            this.txtArama = new DevExpress.XtraEditors.TextEdit();
            this.btnAra = new DevExpress.XtraEditors.SimpleButton();
            this.btnYeniMusteri = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.dgvMusteriler = new DevExpress.XtraGrid.GridControl();
            this.gridViewMusteriler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutItemArama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemBtnAra = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemBtnYeniMusteri = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemBtnDuzenle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemBtnKapat = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemArama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBtnAra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBtnYeniMusteri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBtnDuzenle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBtnKapat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGrid)).BeginInit();
            this.SuspendLayout();
            
            // layoutControl1
            this.layoutControl1.Controls.Add(this.txtArama);
            this.layoutControl1.Controls.Add(this.btnAra);
            this.layoutControl1.Controls.Add(this.btnYeniMusteri);
            this.layoutControl1.Controls.Add(this.btnDuzenle);
            this.layoutControl1.Controls.Add(this.btnKapat);
            this.layoutControl1.Controls.Add(this.dgvMusteriler);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControl1.TabIndex = 0;
            
            // txtArama
            this.txtArama.Location = new System.Drawing.Point(100, 12);
            this.txtArama.Name = "txtArama";
            this.txtArama.Properties.NullValuePrompt = "Müşteri No, TCKN, Ad veya Soyad ile ara...";
            this.txtArama.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtArama.Size = new System.Drawing.Size(600, 20);
            this.txtArama.StyleController = this.layoutControl1;
            this.txtArama.TabIndex = 0;
            this.txtArama.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtArama_KeyPress);
            
            // btnAra
            this.btnAra.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnAra.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAra.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnAra.Appearance.Options.UseBackColor = true;
            this.btnAra.Appearance.Options.UseFont = true;
            this.btnAra.Appearance.Options.UseForeColor = true;
            this.btnAra.Location = new System.Drawing.Point(704, 12);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(120, 26);
            this.btnAra.StyleController = this.layoutControl1;
            this.btnAra.TabIndex = 1;
            this.btnAra.Text = "🔍 Ara";
            this.btnAra.Click += new System.EventHandler(this.BtnAra_Click);
            
            // btnYeniMusteri
            this.btnYeniMusteri.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnYeniMusteri.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnYeniMusteri.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnYeniMusteri.Appearance.Options.UseBackColor = true;
            this.btnYeniMusteri.Appearance.Options.UseFont = true;
            this.btnYeniMusteri.Appearance.Options.UseForeColor = true;
            this.btnYeniMusteri.Location = new System.Drawing.Point(828, 12);
            this.btnYeniMusteri.Name = "btnYeniMusteri";
            this.btnYeniMusteri.Size = new System.Drawing.Size(140, 26);
            this.btnYeniMusteri.StyleController = this.layoutControl1;
            this.btnYeniMusteri.TabIndex = 2;
            this.btnYeniMusteri.Text = "➕ Yeni Müşteri";
            this.btnYeniMusteri.Click += new System.EventHandler(this.BtnYeniMusteri_Click);
            
            // btnDuzenle
            this.btnDuzenle.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnDuzenle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDuzenle.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnDuzenle.Appearance.Options.UseBackColor = true;
            this.btnDuzenle.Appearance.Options.UseFont = true;
            this.btnDuzenle.Appearance.Options.UseForeColor = true;
            this.btnDuzenle.Location = new System.Drawing.Point(972, 12);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(110, 26);
            this.btnDuzenle.StyleController = this.layoutControl1;
            this.btnDuzenle.TabIndex = 3;
            this.btnDuzenle.Text = "✏️ Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.BtnDuzenle_Click);
            
            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(1086, 12);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(102, 26);
            this.btnKapat.StyleController = this.layoutControl1;
            this.btnKapat.TabIndex = 4;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            
            // dgvMusteriler
            this.dgvMusteriler.Location = new System.Drawing.Point(12, 42);
            this.dgvMusteriler.MainView = this.gridViewMusteriler;
            this.dgvMusteriler.Name = "dgvMusteriler";
            this.dgvMusteriler.Size = new System.Drawing.Size(1176, 646);
            this.dgvMusteriler.TabIndex = 5;
            this.dgvMusteriler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMusteriler});
            
            // gridViewMusteriler
            this.gridViewMusteriler.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.gridViewMusteriler.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gridViewMusteriler.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            this.gridViewMusteriler.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridViewMusteriler.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewMusteriler.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridViewMusteriler.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridViewMusteriler.Appearance.Row.Options.UseFont = true;
            this.gridViewMusteriler.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.gridViewMusteriler.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridViewMusteriler.GridControl = this.dgvMusteriler;
            this.gridViewMusteriler.Name = "gridViewMusteriler";
            this.gridViewMusteriler.OptionsBehavior.Editable = false;
            this.gridViewMusteriler.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMusteriler.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewMusteriler.OptionsView.ShowGroupPanel = false;
            this.gridViewMusteriler.OptionsView.ColumnAutoWidth = true;
            this.gridViewMusteriler.RowHeight = 28;
            
            // layoutControlGroup1
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemArama,
            this.layoutItemBtnAra,
            this.layoutItemBtnYeniMusteri,
            this.layoutItemBtnDuzenle,
            this.layoutItemBtnKapat,
            this.layoutItemGrid});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1200, 700);
            this.layoutControlGroup1.TextVisible = false;
            
            // layoutItemArama
            this.layoutItemArama.Control = this.txtArama;
            this.layoutItemArama.Location = new System.Drawing.Point(0, 0);
            this.layoutItemArama.Name = "layoutItemArama";
            this.layoutItemArama.Size = new System.Drawing.Size(692, 30);
            this.layoutItemArama.Text = "Müşteri Ara:";
            this.layoutItemArama.TextSize = new System.Drawing.Size(76, 13);
            
            // layoutItemBtnAra
            this.layoutItemBtnAra.Control = this.btnAra;
            this.layoutItemBtnAra.Location = new System.Drawing.Point(692, 0);
            this.layoutItemBtnAra.Name = "layoutItemBtnAra";
            this.layoutItemBtnAra.Size = new System.Drawing.Size(124, 30);
            this.layoutItemBtnAra.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemBtnAra.TextVisible = false;
            
            // layoutItemBtnYeniMusteri
            this.layoutItemBtnYeniMusteri.Control = this.btnYeniMusteri;
            this.layoutItemBtnYeniMusteri.Location = new System.Drawing.Point(816, 0);
            this.layoutItemBtnYeniMusteri.Name = "layoutItemBtnYeniMusteri";
            this.layoutItemBtnYeniMusteri.Size = new System.Drawing.Size(144, 30);
            this.layoutItemBtnYeniMusteri.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemBtnYeniMusteri.TextVisible = false;
            
            // layoutItemBtnDuzenle
            this.layoutItemBtnDuzenle.Control = this.btnDuzenle;
            this.layoutItemBtnDuzenle.Location = new System.Drawing.Point(960, 0);
            this.layoutItemBtnDuzenle.Name = "layoutItemBtnDuzenle";
            this.layoutItemBtnDuzenle.Size = new System.Drawing.Size(114, 30);
            this.layoutItemBtnDuzenle.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemBtnDuzenle.TextVisible = false;
            
            // layoutItemBtnKapat
            this.layoutItemBtnKapat.Control = this.btnKapat;
            this.layoutItemBtnKapat.Location = new System.Drawing.Point(1074, 0);
            this.layoutItemBtnKapat.Name = "layoutItemBtnKapat";
            this.layoutItemBtnKapat.Size = new System.Drawing.Size(106, 30);
            this.layoutItemBtnKapat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemBtnKapat.TextVisible = false;
            
            // layoutItemGrid
            this.layoutItemGrid.Control = this.dgvMusteriler;
            this.layoutItemGrid.Location = new System.Drawing.Point(0, 30);
            this.layoutItemGrid.Name = "layoutItemGrid";
            this.layoutItemGrid.Size = new System.Drawing.Size(1180, 650);
            this.layoutItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutItemGrid.TextVisible = false;
            
            // FrmMusteriIslem
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmMusteriIslem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Müşteri Listesi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMusteriIslem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMusteriler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemArama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBtnAra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBtnYeniMusteri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBtnDuzenle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemBtnKapat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutItemGrid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
