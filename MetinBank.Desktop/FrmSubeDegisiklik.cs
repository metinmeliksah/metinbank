using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MetinBank.Models;
using MetinBank.Service;
using MetinBank.Util;
using MetinBank.Business;

namespace MetinBank.Desktop
{
    public partial class FrmSubeDegisiklik : XtraForm
    {
        private readonly KullaniciModel _kullanici;
        private readonly SMusteri _sMusteri;
        private readonly DataAccess _dataAccess;
        private readonly BLog _bLog;
        private int _selectedMusteriID = 0;

        public FrmSubeDegisiklik(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _sMusteri = new SMusteri();
            _dataAccess = new DataAccess();
            _bLog = new BLog();
        }

        private void FrmSubeDegisiklik_Load(object sender, EventArgs e)
        {
            // Load branches for selection
            LoadBranches();
            
            // Setup customer search
            SetupCustomerSearch();
            
            // Hide customer info panel initially
            panelCustomerInfo.Visible = false;
        }

        private void SetupCustomerSearch()
        {
            try
            {
                // Load all active customers
                DataTable dtCustomers;
                string hata;

                if (_kullanici.SubeID.HasValue)
                {
                    // Branch employees see only their branch customers
                    hata = _sMusteri.SubeninMusterileri(_kullanici.SubeID.Value, out dtCustomers);
                }
                else
                {
                    // Headquarters see all customers
                    hata = _sMusteri.TumMusteriler(out dtCustomers);
                }

                if (hata != null)
                {
                    XtraMessageBox.Show($"Müşteriler yüklenirken hata: {hata}", 
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                searchLookUpCustomer.Properties.DataSource = dtCustomers;
                searchLookUpCustomer.Properties.DisplayMember = "Ad";
                searchLookUpCustomer.Properties.ValueMember = "MusteriID";
                
                // Configure columns for search
                var view = searchLookUpCustomer.Properties.View;
                view.Columns.Clear();
                view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "MusteriNo", Caption = "Müşteri No", Visible = true });
                view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "TCKN", Caption = "TCKN", Visible = true });
                view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "Ad", Caption = "Ad", Visible = true });
                view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "Soyad", Caption = "Soyad", Visible = true });
                view.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "CepTelefon", Caption = "Telefon", Visible = true });
                
                view.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Müşteri araması hazırlanırken hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void LoadBranches()
        {
            try
            {
                string query = "SELECT SubeID, SubeKodu, SubeAdi, Sehir FROM Sube WHERE AktifMi = 1 ORDER BY SubeAdi";
                DataTable dtBranches;
                string hata = _dataAccess.ExecuteQuery(query, null, out dtBranches);

                if (hata != null)
                {
                    XtraMessageBox.Show($"Şubeler yüklenirken hata: {hata}", 
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lookUpNewBranch.Properties.DataSource = dtBranches;
                lookUpNewBranch.Properties.DisplayMember = "SubeAdi";
                lookUpNewBranch.Properties.ValueMember = "SubeID";
                
                lookUpNewBranch.Properties.Columns.Clear();
                lookUpNewBranch.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default));
                lookUpNewBranch.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeKodu", "Kod", 60, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default));
                lookUpNewBranch.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SubeAdi", "Şube Adı", 200, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default));
                lookUpNewBranch.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sehir", "Şehir", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Şubeler yüklenirken hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void SearchLookUpCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpCustomer.EditValue == null)
            {
                panelCustomerInfo.Visible = false;
                _selectedMusteriID = 0;
                return;
            }

            try
            {
                _selectedMusteriID = Convert.ToInt32(searchLookUpCustomer.EditValue);
                LoadCustomerInfo(_selectedMusteriID);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Müşteri bilgileri yüklenirken hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerInfo(int musteriID)
        {
            try
            {
                string query = $@"
                    SELECT 
                        m.MusteriNo,
                        m.TCKN,
                        CONCAT(m.Ad, ' ', m.Soyad) as AdSoyad,
                        m.CepTelefon,
                        m.Email,
                        m.MusteriTipi,
                        m.MusteriSegmenti,
                        m.KayitSubeID,
                        s.SubeAdi,
                        s.SubeKodu,
                        s.Sehir
                    FROM Musteri m
                    INNER JOIN Sube s ON m.KayitSubeID = s.SubeID
                    WHERE m.MusteriID = {musteriID}";

                DataTable dtCustomer;
                string hata = _dataAccess.ExecuteQuery(query, null, out dtCustomer);

                if (hata != null || dtCustomer.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Müşteri bilgileri bulunamadı.", 
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow row = dtCustomer.Rows[0];
                
                // Display customer information
                lblCustomerName.Text = row["AdSoyad"].ToString();
                lblCustomerNo.Text = $"Müşteri No: {row["MusteriNo"]}";
                lblCustomerTCKN.Text = $"TCKN: {row["TCKN"]}";
                lblCustomerPhone.Text = $"Telefon: {row["CepTelefon"]}";
                lblCustomerEmail.Text = $"Email: {row["Email"]}";
                lblCustomerType.Text = $"Tip: {row["MusteriTipi"]} - {row["MusteriSegmenti"]}";
                
                // Display current branch
                int currentBranchID = Convert.ToInt32(row["KayitSubeID"]);
                lblCurrentBranch.Text = $"Mevcut Şube: {row["SubeAdi"]} ({row["SubeKodu"]}) - {row["Sehir"]}";
                
                // Update branch dropdown to exclude current branch
                UpdateBranchList(currentBranchID);
                
                panelCustomerInfo.Visible = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Müşteri bilgileri yüklenirken hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void UpdateBranchList(int currentBranchID)
        {
            try
            {
                string query = $"SELECT SubeID, SubeKodu, SubeAdi, Sehir FROM Sube WHERE AktifMi = 1 AND SubeID != {currentBranchID} ORDER BY SubeAdi";
                DataTable dtBranches;
                string hata = _dataAccess.ExecuteQuery(query, null, out dtBranches);

                if (hata == null)
                {
                    lookUpNewBranch.Properties.DataSource = dtBranches;
                    lookUpNewBranch.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Şube listesi güncellenirken hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                // Validations
                if (_selectedMusteriID == 0)
                {
                    XtraMessageBox.Show("Lütfen bir müşteri seçiniz.", 
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    searchLookUpCustomer.Focus();
                    return;
                }

                if (lookUpNewBranch.EditValue == null)
                {
                    XtraMessageBox.Show("Lütfen yeni şube seçiniz.", 
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lookUpNewBranch.Focus();
                    return;
                }

                int newBranchID = Convert.ToInt32(lookUpNewBranch.EditValue);
                string newBranchName = lookUpNewBranch.Text;
                
                string reason = memoReason.Text?.Trim();
                if (string.IsNullOrWhiteSpace(reason))
                {
                    XtraMessageBox.Show("Lütfen transfer nedenini belirtiniz.", 
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    memoReason.Focus();
                    return;
                }

                // Confirmation
                if (XtraMessageBox.Show(
                    $"Müşterinin şubesini değiştirmek istediğinize emin misiniz?\n\n" +
                    $"Müşteri: {lblCustomerName.Text}\n" +
                    $"Yeni Şube: {newBranchName}\n\n" +
                    $"Bu işlem geri alınamaz!",
                    "Onay", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                // Perform transfer
                string hata = TransferCustomerBranch(_selectedMusteriID, newBranchID, reason);

                if (hata != null)
                {
                    XtraMessageBox.Show($"Şube transferi sırasında hata oluştu:\n\n{hata}", 
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                XtraMessageBox.Show(
                    "Müşteri şube transferi başarıyla gerçekleştirildi.", 
                    "Başarılı", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);

                // Reset form
                searchLookUpCustomer.EditValue = null;
                lookUpNewBranch.EditValue = null;
                memoReason.Text = "";
                panelCustomerInfo.Visible = false;
                _selectedMusteriID = 0;
                
                // Reload customer search
                SetupCustomerSearch();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Transfer işlemi sırasında hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TransferCustomerBranch(int musteriID, int newBranchID, string reason)
        {
            try
            {
                // Get current branch for logging
                string queryOld = $"SELECT KayitSubeID FROM Musteri WHERE MusteriID = {musteriID}";
                DataTable dtOld;
                string hata = _dataAccess.ExecuteQuery(queryOld, null, out dtOld);
                
                if (hata != null || dtOld.Rows.Count == 0)
                {
                    return "Müşteri bilgileri bulunamadı.";
                }
                
                int oldBranchID = Convert.ToInt32(dtOld.Rows[0]["KayitSubeID"]);

                // Update customer branch
                string updateQuery = $"UPDATE Musteri SET KayitSubeID = {newBranchID} WHERE MusteriID = {musteriID}";
                int affectedRows;
                hata = _dataAccess.ExecuteNonQuery(updateQuery, null, out affectedRows);

                if (hata != null)
                {
                    return hata;
                }

                // Log the transfer
                _bLog.IslemLoguKaydet(
                    _kullanici.KullaniciID,
                    "MusteriSubeDegisiklik",
                    "Musteri",
                    musteriID,
                    $"SubeID: {oldBranchID}",
                    $"SubeID: {newBranchID}",
                    $"Şube değişikliği yapıldı. Neden: {reason}",
                    CommonFunctions.GetLocalIPAddress(),
                    true,
                    null
                );

                return null;
            }
            catch (Exception ex)
            {
                return $"Transfer hatası: {ex.Message}";
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }
    }
}
