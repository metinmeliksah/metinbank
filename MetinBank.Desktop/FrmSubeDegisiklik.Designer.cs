namespace MetinBank.Desktop
{
    partial class FrmSubeDegisiklik
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.searchLookUpCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpCustomerView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelCustomerInfo = new DevExpress.XtraEditors.PanelControl();
            this.lblCustomerType = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerEmail = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerPhone = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerTCKN = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerNo = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.lblCurrentBranch = new DevExpress.XtraEditors.LabelControl();
            this.lookUpNewBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.memoReason = new DevExpress.XtraEditors.MemoEdit();
            this.btnTransfer = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpCustomerView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCustomerInfo)).BeginInit();
            this.panelCustomerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpNewBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoReason.Properties)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.searchLookUpCustomer);
            this.layoutControl1.Controls.Add(this.panelCustomerInfo);
            this.layoutControl1.Controls.Add(this.lookUpNewBranch);
            this.layoutControl1.Controls.Add(this.memoReason);
            this.layoutControl1.Controls.Add(this.btnTransfer);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(900, 650);
            this.layoutControl1.TabIndex = 0;
            // 
            // searchLookUpCustomer
            // 
            this.searchLookUpCustomer.Location = new System.Drawing.Point(12, 28);
            this.searchLookUpCustomer.Name = "searchLookUpCustomer";
            this.searchLookUpCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpCustomer.Properties.NullText = "Müşteri ara (İsim, TCKN, Müşteri No...)";
            this.searchLookUpCustomer.Properties.PopupView = this.searchLookUpCustomerView;
            this.searchLookUpCustomer.Size = new System.Drawing.Size(876, 20);
            this.searchLookUpCustomer.StyleController = this.layoutControl1;
            this.searchLookUpCustomer.TabIndex = 0;
            this.searchLookUpCustomer.EditValueChanged += new System.EventHandler(this.SearchLookUpCustomer_EditValueChanged);
            // 
            // searchLookUpCustomerView
            // 
            this.searchLookUpCustomerView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpCustomerView.Name = "searchLookUpCustomerView";
            this.searchLookUpCustomerView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpCustomerView.OptionsView.ShowGroupPanel = false;
            // 
            // panelCustomerInfo
            // 
            this.panelCustomerInfo.Controls.Add(this.lblCustomerType);
            this.panelCustomerInfo.Controls.Add(this.lblCustomerEmail);
            this.panelCustomerInfo.Controls.Add(this.lblCustomerPhone);
            this.panelCustomerInfo.Controls.Add(this.lblCustomerTCKN);
            this.panelCustomerInfo.Controls.Add(this.lblCustomerNo);
            this.panelCustomerInfo.Controls.Add(this.lblCustomerName);
            this.panelCustomerInfo.Controls.Add(this.lblCurrentBranch);
            this.panelCustomerInfo.Location = new System.Drawing.Point(12, 52);
            this.panelCustomerInfo.Name = "panelCustomerInfo";
            this.panelCustomerInfo.Size = new System.Drawing.Size(876, 200);
            this.panelCustomerInfo.TabIndex = 1;
            // 
            // lblCustomerType
            // 
            this.lblCustomerType.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerType.Appearance.Options.UseFont = true;
            this.lblCustomerType.Location = new System.Drawing.Point(15, 120);
            this.lblCustomerType.Name = "lblCustomerType";
            this.lblCustomerType.Size = new System.Drawing.Size(0, 17);
            this.lblCustomerType.TabIndex = 6;
            // 
            // lblCustomerEmail
            // 
            this.lblCustomerEmail.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerEmail.Appearance.Options.UseFont = true;
            this.lblCustomerEmail.Location = new System.Drawing.Point(15, 100);
            this.lblCustomerEmail.Name = "lblCustomerEmail";
            this.lblCustomerEmail.Size = new System.Drawing.Size(0, 17);
            this.lblCustomerEmail.TabIndex = 5;
            // 
            // lblCustomerPhone
            // 
            this.lblCustomerPhone.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerPhone.Appearance.Options.UseFont = true;
            this.lblCustomerPhone.Location = new System.Drawing.Point(15, 80);
            this.lblCustomerPhone.Name = "lblCustomerPhone";
            this.lblCustomerPhone.Size = new System.Drawing.Size(0, 17);
            this.lblCustomerPhone.TabIndex = 4;
            // 
            // lblCustomerTCKN
            // 
            this.lblCustomerTCKN.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerTCKN.Appearance.Options.UseFont = true;
            this.lblCustomerTCKN.Location = new System.Drawing.Point(15, 60);
            this.lblCustomerTCKN.Name = "lblCustomerTCKN";
            this.lblCustomerTCKN.Size = new System.Drawing.Size(0, 17);
            this.lblCustomerTCKN.TabIndex = 3;
            // 
            // lblCustomerNo
            // 
            this.lblCustomerNo.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerNo.Appearance.Options.UseFont = true;
            this.lblCustomerNo.Location = new System.Drawing.Point(15, 40);
            this.lblCustomerNo.Name = "lblCustomerNo";
            this.lblCustomerNo.Size = new System.Drawing.Size(0, 17);
            this.lblCustomerNo.TabIndex = 2;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblCustomerName.Appearance.Options.UseFont = true;
            this.lblCustomerName.Appearance.Options.UseForeColor = true;
            this.lblCustomerName.Location = new System.Drawing.Point(15, 10);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(0, 25);
            this.lblCustomerName.TabIndex = 1;
            // 
            // lblCurrentBranch
            // 
            this.lblCurrentBranch.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCurrentBranch.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblCurrentBranch.Appearance.Options.UseFont = true;
            this.lblCurrentBranch.Appearance.Options.UseForeColor = true;
            this.lblCurrentBranch.Location = new System.Drawing.Point(15, 160);
            this.lblCurrentBranch.Name = "lblCurrentBranch";
            this.lblCurrentBranch.Size = new System.Drawing.Size(0, 20);
            this.lblCurrentBranch.TabIndex = 0;
            // 
            // lookUpNewBranch
            // 
            this.lookUpNewBranch.Location = new System.Drawing.Point(12, 272);
            this.lookUpNewBranch.Name = "lookUpNewBranch";
            this.lookUpNewBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpNewBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeKodu", "Kod", 60, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeAdi", "Şube Adı", 200, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sehir", "Şehir", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookUpNewBranch.Properties.DisplayMember = "SubeAdi";
            this.lookUpNewBranch.Properties.NullText = "Yeni şube seçiniz...";
            this.lookUpNewBranch.Properties.ValueMember = "SubeID";
            this.lookUpNewBranch.Size = new System.Drawing.Size(876, 20);
            this.lookUpNewBranch.StyleController = this.layoutControl1;
            this.lookUpNewBranch.TabIndex = 2;
            // 
            // memoReason
            // 
            this.memoReason.Location = new System.Drawing.Point(12, 312);
            this.memoReason.Name = "memoReason";
            this.memoReason.Properties.MaxLength = 500;
            this.memoReason.Size = new System.Drawing.Size(876, 120);
            this.memoReason.StyleController = this.layoutControl1;
            this.memoReason.TabIndex = 3;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnTransfer.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnTransfer.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnTransfer.Appearance.Options.UseBackColor = true;
            this.btnTransfer.Appearance.Options.UseFont = true;
            this.btnTransfer.Appearance.Options.UseForeColor = true;
            this.btnTransfer.Location = new System.Drawing.Point(12, 436);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(876, 40);
            this.btnTransfer.StyleController = this.layoutControl1;
            this.btnTransfer.TabIndex = 4;
            this.btnTransfer.Text = "🔄 Şube Transferini Gerçekleştir";
            this.btnTransfer.Click += new System.EventHandler(this.BtnTransfer_Click);
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(900, 650);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.searchLookUpCustomer;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(880, 40);
            this.layoutControlItem1.Text = "👤 Müşteri Seçimi";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(130, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelCustomerInfo;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(880, 204);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lookUpNewBranch;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 244);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(880, 40);
            this.layoutControlItem3.Text = "🏢 Yeni Şube";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(130, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.memoReason;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 284);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(880, 140);
            this.layoutControlItem4.Text = "📝 Transfer Nedeni";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(130, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnTransfer;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 424);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(880, 44);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 468);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(880, 162);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmSubeDegisiklik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmSubeDegisiklik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Müşteri Şube Transferi";
            this.Load += new System.EventHandler(this.FrmSubeDegisiklik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpCustomerView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCustomerInfo)).EndInit();
            this.panelCustomerInfo.ResumeLayout(false);
            this.panelCustomerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpNewBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoReason.Properties)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpCustomerView;
        private DevExpress.XtraEditors.PanelControl panelCustomerInfo;
        private DevExpress.XtraEditors.LabelControl lblCustomerName;
        private DevExpress.XtraEditors.LabelControl lblCustomerNo;
        private DevExpress.XtraEditors.LabelControl lblCustomerTCKN;
        private DevExpress.XtraEditors.LabelControl lblCustomerPhone;
        private DevExpress.XtraEditors.LabelControl lblCustomerEmail;
        private DevExpress.XtraEditors.LabelControl lblCustomerType;
        private DevExpress.XtraEditors.LabelControl lblCurrentBranch;
        private DevExpress.XtraEditors.LookUpEdit lookUpNewBranch;
        private DevExpress.XtraEditors.MemoEdit memoReason;
        private DevExpress.XtraEditors.SimpleButton btnTransfer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
