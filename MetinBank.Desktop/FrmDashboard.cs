using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using MetinBank.Models;
using MetinBank.Util;

namespace MetinBank.Desktop
{
    public partial class FrmDashboard : XtraForm
    {
        private readonly KullaniciModel _kullanici;
        private readonly DataAccess _dataAccess;

        public FrmDashboard(KullaniciModel kullanici)
        {
            InitializeComponent();
            _kullanici = kullanici;
            _dataAccess = new DataAccess();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            // Welcome message
            lblWelcome.Text = $"Hoş Geldiniz, {_kullanici.Ad} {_kullanici.Soyad}!";
            lblRole.Text = _kullanici.RolAdi;
            
            // Load role-specific dashboard
            switch (_kullanici.YetkiSeviyesi)
            {
                case 2: // Şube Çalışanı
                    LoadEmployeeDashboard();
                    break;
                case 3: // Şube Müdürü
                    LoadManagerDashboard();
                    break;
                case 4: // Genel Merkez
                    LoadHeadquartersDashboard();
                    break;
                default:
                    LoadEmployeeDashboard(); // Default to employee view
                    break;
            }
        }

        #region Employee Dashboard (Şube Çalışanı)
        
        private void LoadEmployeeDashboard()
        {
            try
            {
                ApplyEmployeeTheme();
                
                // Stat Panel 1: Today's transactions processed by this employee
                lblStat1Title.Text = "Bugün İşlem Sayım";
                string todayTransQuery = $@"
                    SELECT COUNT(*) as Total 
                    FROM Islem 
                    WHERE KullaniciID = {_kullanici.KullaniciID} 
                    AND DATE(IslemTarihi) = CURDATE()
                    AND BasariliMi = 1";
                lblStat1Value.Text = GetScalarValue(todayTransQuery);

                // Stat Panel 2: Today's total amount processed
                lblStat2Title.Text = "Bugün Toplam Tutar";
                string todayAmountQuery = $@"
                    SELECT COALESCE(SUM(Tutar), 0) as Total 
                    FROM Islem 
                    WHERE KullaniciID = {_kullanici.KullaniciID} 
                    AND DATE(IslemTarihi) = CURDATE()
                    AND BasariliMi = 1 
                    AND ParaBirimi = 'TL'";
                decimal todayAmount = Convert.ToDecimal(GetScalarValue(todayAmountQuery));
                lblStat2Value.Text = todayAmount.ToString("N0") + " ₺";

                // Stat Panel 3: This week's transactions
                lblStat3Title.Text = "Bu Hafta İşlemlerim";
                string weekTransQuery = $@"
                    SELECT COUNT(*) as Total 
                    FROM Islem 
                    WHERE KullaniciID = {_kullanici.KullaniciID} 
                    AND YEARWEEK(IslemTarihi, 1) = YEARWEEK(CURDATE(), 1)
                    AND BasariliMi = 1";
                lblStat3Value.Text = GetScalarValue(weekTransQuery);

                // Stat Panel 4: Active customers served today
                lblStat4Title.Text = "Bugün Hizmet Verilen";
                string activeCustomersQuery = $@"
                    SELECT COUNT(DISTINCT KaynakHesapID) as Total 
                    FROM Islem 
                    WHERE KullaniciID = {_kullanici.KullaniciID} 
                    AND DATE(IslemTarihi) = CURDATE()
                    AND BasariliMi = 1
                    AND KaynakHesapID IS NOT NULL";
                lblStat4Value.Text = GetScalarValue(activeCustomersQuery) + " Müşteri";

                // Chart 1: Last 7 days transaction trend
                LoadEmployeeTrendChart();

                // Chart 2: Transaction type distribution (Pie)
                LoadEmployeeDistributionChart();

                // Chart 3: Hide comparison chart for employees
                chartComparison.Visible = false;



                // Branch info
                LoadBranchInfo();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dashboard yüklenirken hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void ApplyEmployeeTheme()
        {
            // Lighter Pastel Colors for Dark Text
            Color color1 = Color.FromArgb(210, 180, 222);   // Lavender (was Purple)
            Color color2 = Color.FromArgb(221, 190, 233);   // Plum (was Light Purple)
            Color color3 = Color.FromArgb(174, 182, 191);   // Light Gray-Blue (was Dark Blue-Gray)
            Color color4 = Color.FromArgb(245, 183, 177);   // Light Salmon (was Red)

            panelStat1.Appearance.BackColor = color1;
            panelStat2.Appearance.BackColor = color2;
            panelStat3.Appearance.BackColor = color3;
            panelStat4.Appearance.BackColor = color4;

            Color darkText = Color.FromArgb(64, 64, 64);
            lblStat1Title.Appearance.ForeColor = darkText;
            lblStat1Value.Appearance.ForeColor = darkText;
            lblStat2Title.Appearance.ForeColor = darkText;
            lblStat2Value.Appearance.ForeColor = darkText;
            lblStat3Title.Appearance.ForeColor = darkText;
            lblStat3Value.Appearance.ForeColor = darkText;
            lblStat4Title.Appearance.ForeColor = darkText;
            lblStat4Value.Appearance.ForeColor = darkText;

            lblWelcome.ForeColor = Color.FromArgb(142, 68, 173); // Keep title dark purple
        }

        private void LoadEmployeeTrendChart()
        {
            try
            {
                string query = $@"
                    SELECT DATE(IslemTarihi) as Tarih, COUNT(*) as Sayi
                    FROM Islem
                    WHERE KullaniciID = {_kullanici.KullaniciID}
                    AND IslemTarihi >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)
                    AND BasariliMi = 1
                    GROUP BY DATE(IslemTarihi)
                    ORDER BY Tarih";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    var series = new Series("Günlük İşlemlerim", ViewType.Line);
                    series.ArgumentScaleType = ScaleType.DateTime;

                    foreach (DataRow row in dt.Rows)
                    {
                        series.Points.Add(new SeriesPoint(
                            Convert.ToDateTime(row["Tarih"]),
                            Convert.ToInt32(row["Sayi"])
                        ));
                    }

                    LineSeriesView view = (LineSeriesView)series.View;
                    view.LineMarkerOptions.Kind = MarkerKind.Circle;
                    view.LineMarkerOptions.Size = 8;
                    view.Color = Color.FromArgb(142, 68, 173);

                    chartTrend.Series.Clear();
                    chartTrend.Series.Add(series);
                    
                    chartTrend.Titles.Clear();
                    ChartTitle title = new ChartTitle();
                    title.Text = "Son 7 Gün İşlem Trendi";
                    title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    chartTrend.Titles.Add(title);

                    ((XYDiagram)chartTrend.Diagram).EnableAxisXScrolling = false;
                    ((XYDiagram)chartTrend.Diagram).EnableAxisXZooming = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Trend grafiği yüklenemedi: {ex.Message}", "Hata");
            }
        }

        private void LoadEmployeeDistributionChart()
        {
            try
            {
                string query = $@"
                    SELECT IslemTipi, COUNT(*) as Sayi
                    FROM Islem
                    WHERE KullaniciID = {_kullanici.KullaniciID}
                    AND IslemTarihi >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
                    AND BasariliMi = 1
                    GROUP BY IslemTipi
                    ORDER BY Sayi DESC";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    var series = new Series("İşlem Dağılımı", ViewType.Pie);

                    foreach (DataRow row in dt.Rows)
                    {
                        series.Points.Add(new SeriesPoint(
                            row["IslemTipi"].ToString(),
                            Convert.ToInt32(row["Sayi"])
                        ));
                    }

                    PieSeriesView view = (PieSeriesView)series.View;
                    view.RuntimeExploding = true;
                    series.LegendTextPattern = "{A}: {V} ({VP:P0})";

                    chartDistribution.Series.Clear();
                    chartDistribution.Series.Add(series);
                    
                    chartDistribution.Titles.Clear();
                    ChartTitle title = new ChartTitle();
                    title.Text = "İşlem Tipi Dağılımı (Son 30 Gün)";
                    title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    chartDistribution.Titles.Add(title);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dağılım grafiği yüklenemedi: {ex.Message}", "Hata");
            }
        }



        #endregion

        #region Manager Dashboard (Şube Müdürü)

        private void LoadManagerDashboard()
        {
            try
            {
                ApplyManagerTheme();

                // Stat Panel 1: Branch customer count
                lblStat1Title.Text = "Şube Müşteri Sayısı";
                string customerQuery = $@"
                    SELECT COUNT(*) as Total 
                    FROM Musteri 
                    WHERE KayitSubeID = {_kullanici.SubeID} 
                    AND AktifMi = 1";
                lblStat1Value.Text = GetScalarValue(customerQuery);

                // Stat Panel 2: Branch active accounts
                lblStat2Title.Text = "Aktif Hesap Sayısı";
                string accountQuery = $@"
                    SELECT COUNT(*) as Total 
                    FROM Hesap 
                    WHERE SubeID = {_kullanici.SubeID} 
                    AND Durum = 'Aktif'";
                lblStat2Value.Text = GetScalarValue(accountQuery);

                // Stat Panel 3: Total branch balance
                lblStat3Title.Text = "Toplam Şube Bakiyesi";
                string balanceQuery = $@"
                    SELECT COALESCE(SUM(Bakiye), 0) as Total 
                    FROM Hesap 
                    WHERE SubeID = {_kullanici.SubeID} 
                    AND Durum = 'Aktif' 
                    AND HesapTipi = 'TL'";
                decimal balance = Convert.ToDecimal(GetScalarValue(balanceQuery));
                lblStat3Value.Text = balance.ToString("N0") + " ₺";

                // Stat Panel 4: Pending approvals
                lblStat4Title.Text = "Onay Bekleyenler";
                string approvalQuery = $@"
                    SELECT COUNT(*) as Total 
                    FROM Islem 
                    WHERE SubeID = {_kullanici.SubeID} 
                    AND OnayDurumu = 'Beklemede'";
                lblStat4Value.Text = GetScalarValue(approvalQuery);

                // Charts
                LoadManagerTrendChart();
                LoadManagerDistributionChart();
                LoadManagerComparisonChart();



                // Branch info
                LoadBranchInfo();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dashboard yüklenirken hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void ApplyManagerTheme()
        {
            // Lighter Pastel Colors for Dark Text
            Color color1 = Color.FromArgb(163, 228, 215);   // Light Teal (was Teal)
            Color color2 = Color.FromArgb(171, 235, 198);   // Pale Green (was Green)
            Color color3 = Color.FromArgb(162, 217, 206);   // Mint (was Light Teal)
            Color color4 = Color.FromArgb(249, 231, 159);   // Wheat (was Orange)

            panelStat1.Appearance.BackColor = color1;
            panelStat2.Appearance.BackColor = color2;
            panelStat3.Appearance.BackColor = color3;
            panelStat4.Appearance.BackColor = color4;

            Color darkText = Color.FromArgb(64, 64, 64);
            lblStat1Title.Appearance.ForeColor = darkText;
            lblStat1Value.Appearance.ForeColor = darkText;
            lblStat2Title.Appearance.ForeColor = darkText;
            lblStat2Value.Appearance.ForeColor = darkText;
            lblStat3Title.Appearance.ForeColor = darkText;
            lblStat3Value.Appearance.ForeColor = darkText;
            lblStat4Title.Appearance.ForeColor = darkText;
            lblStat4Value.Appearance.ForeColor = darkText;

            lblWelcome.ForeColor = Color.FromArgb(22, 160, 133); // Keep title dark teal
        }

        private void LoadManagerTrendChart()
        {
            try
            {
                string query = $@"
                    SELECT DATE(IslemTarihi) as Tarih, COUNT(*) as Sayi, SUM(Tutar) as Toplam
                    FROM Islem
                    WHERE SubeID = {_kullanici.SubeID}
                    AND IslemTarihi >= DATE_SUB(CURDATE(), INTERVAL 14 DAY)
                    AND BasariliMi = 1
                    AND ParaBirimi = 'TL'
                    GROUP BY DATE(IslemTarihi)
                    ORDER BY Tarih";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    var series = new Series("Günlük İşlem Hacmi (₺)", ViewType.Line);
                    series.ArgumentScaleType = ScaleType.DateTime;

                    foreach (DataRow row in dt.Rows)
                    {
                        series.Points.Add(new SeriesPoint(
                            Convert.ToDateTime(row["Tarih"]),
                            Convert.ToDecimal(row["Toplam"])
                        ));
                    }

                    LineSeriesView view = (LineSeriesView)series.View;
                    view.LineMarkerOptions.Kind = MarkerKind.Diamond;
                    view.LineMarkerOptions.Size = 10;
                    view.Color = Color.FromArgb(22, 160, 133);
                    view.LineStyle.Thickness = 3;

                    chartTrend.Series.Clear();
                    chartTrend.Series.Add(series);
                    
                    chartTrend.Titles.Clear();
                    ChartTitle title = new ChartTitle();
                    title.Text = "Şube Günlük İşlem Hacmi (Son 14 Gün)";
                    title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    chartTrend.Titles.Add(title);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Trend grafiği yüklenemedi: {ex.Message}", "Hata");
            }
        }

        private void LoadManagerDistributionChart()
        {
            try
            {
                string query = $@"
                    SELECT IslemTipi, COUNT(*) as Sayi
                    FROM Islem
                    WHERE SubeID = {_kullanici.SubeID}
                    AND IslemTarihi >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
                    AND BasariliMi = 1
                    GROUP BY IslemTipi
                    ORDER BY Sayi DESC";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    var series = new Series("İşlem Dağılımı", ViewType.Doughnut);

                    foreach (DataRow row in dt.Rows)
                    {
                        series.Points.Add(new SeriesPoint(
                            row["IslemTipi"].ToString(),
                            Convert.ToInt32(row["Sayi"])
                        ));
                    }

                    PieSeriesView view = (PieSeriesView)series.View;
                    view.RuntimeExploding = true;
                    series.LegendTextPattern = "{A}: {V} ({VP:P0})";

                    chartDistribution.Series.Clear();
                    chartDistribution.Series.Add(series);
                    
                    chartDistribution.Titles.Clear();
                    ChartTitle title = new ChartTitle();
                    title.Text = "Şube İşlem Tipi Dağılımı";
                    title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    chartDistribution.Titles.Add(title);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dağılım grafiği yüklenemedi: {ex.Message}", "Hata");
            }
        }

        private void LoadManagerComparisonChart()
        {
            try
            {
                chartComparison.Visible = true;

                string query = $@"
                    SELECT 
                        CONCAT(k.Ad, ' ', k.Soyad) as Calisan,
                        COUNT(*) as IslemSayisi
                    FROM Islem i
                    INNER JOIN Kullanici k ON i.KullaniciID = k.KullaniciID
                    WHERE i.SubeID = {_kullanici.SubeID}
                    AND i.IslemTarihi >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)
                    AND i.BasariliMi = 1
                    AND k.RolID = 2
                    GROUP BY k.KullaniciID, k.Ad, k.Soyad
                    ORDER BY IslemSayisi DESC
                    LIMIT 10";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    var series = new Series("Çalışan Performansı", ViewType.Bar);

                    foreach (DataRow row in dt.Rows)
                    {
                        series.Points.Add(new SeriesPoint(
                            row["Calisan"].ToString(),
                            Convert.ToInt32(row["IslemSayisi"])
                        ));
                    }

                    BarSeriesView view = (BarSeriesView)series.View;
                    view.Color = Color.FromArgb(39, 174, 96);

                    chartComparison.Series.Clear();
                    chartComparison.Series.Add(series);
                    
                    chartComparison.Titles.Clear();
                    ChartTitle title = new ChartTitle();
                    title.Text = "Çalışan Performans Karşılaştırması (Son 7 Gün)";
                    title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    chartComparison.Titles.Add(title);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Karşılaştırma grafiği yüklenemedi: {ex.Message}", "Hata");
            }
        }



        #endregion

        #region Headquarters Dashboard (Genel Merkez)

        private void LoadHeadquartersDashboard()
        {
            try
            {
                ApplyHeadquartersTheme();

                // Stat Panel 1: Total system customers
                lblStat1Title.Text = "Toplam Müşteri";
                string customerQuery = "SELECT COUNT(*) as Total FROM Musteri WHERE AktifMi = 1";
                lblStat1Value.Text = GetScalarValue(customerQuery);

                // Stat Panel 2: Total system accounts
                lblStat2Title.Text = "Toplam Hesap";
                string accountQuery = "SELECT COUNT(*) as Total FROM Hesap WHERE Durum = 'Aktif'";
                lblStat2Value.Text = GetScalarValue(accountQuery);

                // Stat Panel 3: Total system balance
                lblStat3Title.Text = "Toplam Bakiye";
                string balanceQuery = @"
                    SELECT COALESCE(SUM(Bakiye), 0) as Total 
                    FROM Hesap 
                    WHERE Durum = 'Aktif' 
                    AND HesapTipi = 'TL'";
                decimal balance = Convert.ToDecimal(GetScalarValue(balanceQuery));
                lblStat3Value.Text = balance.ToString("N0") + " ₺";

                // Stat Panel 4: High-value pending approvals
                lblStat4Title.Text = "Yüksek Tutarlı Onaylar";
                string approvalQuery = @"
                    SELECT COUNT(*) as Total 
                    FROM Islem 
                    WHERE OnayDurumu = 'Beklemede' 
                    AND Tutar >= 250000";
                lblStat4Value.Text = GetScalarValue(approvalQuery);

                // Charts
                LoadHeadquartersTrendChart();
                LoadHeadquartersDistributionChart();
                LoadHeadquartersComparisonChart();



                // System-wide info
                LoadSystemInfo();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dashboard yüklenirken hata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void ApplyHeadquartersTheme()
        {
            // Lighter Pastel Colors for Dark Text
            Color color1 = Color.FromArgb(174, 214, 241);   // Light Blue (was Blue)
            Color color2 = Color.FromArgb(214, 234, 248);   // Sky Blue (was Light Blue)
            Color color3 = Color.FromArgb(252, 243, 207);   // Pale Goldenrod (was Gold)
            Color color4 = Color.FromArgb(248, 196, 113);   // Peach (was Orange)

            panelStat1.Appearance.BackColor = color1;
            panelStat2.Appearance.BackColor = color2;
            panelStat3.Appearance.BackColor = color3;
            panelStat4.Appearance.BackColor = color4;

            Color darkText = Color.FromArgb(64, 64, 64);
            lblStat1Title.Appearance.ForeColor = darkText;
            lblStat1Value.Appearance.ForeColor = darkText;
            lblStat2Title.Appearance.ForeColor = darkText;
            lblStat2Value.Appearance.ForeColor = darkText;
            lblStat3Title.Appearance.ForeColor = darkText;
            lblStat3Value.Appearance.ForeColor = darkText;
            lblStat4Title.Appearance.ForeColor = darkText;
            lblStat4Value.Appearance.ForeColor = darkText;

            lblWelcome.ForeColor = Color.FromArgb(41, 128, 185); // Keep title dark blue
        }

        private void LoadHeadquartersTrendChart()
        {
            try
            {
                string query = @"
                    SELECT DATE(IslemTarihi) as Tarih, 
                           SUM(CASE WHEN ParaBirimi = 'TL' THEN Tutar ELSE 0 END) as Toplam
                    FROM Islem
                    WHERE IslemTarihi >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
                    AND BasariliMi = 1
                    GROUP BY DATE(IslemTarihi)
                    ORDER BY Tarih";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    var series = new Series("Sistem Geneli İşlem Hacmi", ViewType.Area);
                    series.ArgumentScaleType = ScaleType.DateTime;

                    foreach (DataRow row in dt.Rows)
                    {
                        series.Points.Add(new SeriesPoint(
                            Convert.ToDateTime(row["Tarih"]),
                            Convert.ToDecimal(row["Toplam"])
                        ));
                    }

                    AreaSeriesView view = (AreaSeriesView)series.View;
                    view.Color = Color.FromArgb(100, 41, 128, 185);
                    view.Border.Color = Color.FromArgb(41, 128, 185);
                    view.Border.Thickness = 3;
                    view.MarkerOptions.Kind = MarkerKind.Circle;
                    view.MarkerOptions.Size = 8;

                    chartTrend.Series.Clear();
                    chartTrend.Series.Add(series);
                    
                    chartTrend.Titles.Clear();
                    ChartTitle title = new ChartTitle();
                    title.Text = "Sistem Geneli Günlük İşlem Hacmi (Son 30 Gün)";
                    title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    chartTrend.Titles.Add(title);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Trend grafiği yüklenemedi: {ex.Message}", "Hata");
            }
        }

        private void LoadHeadquartersDistributionChart()
        {
            try
            {
                string query = @"
                    SELECT IslemTipi, COUNT(*) as Sayi
                    FROM Islem
                    WHERE IslemTarihi >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
                    AND BasariliMi = 1
                    GROUP BY IslemTipi
                    ORDER BY Sayi DESC";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    var series = new Series("Sistem İşlem Dağılımı", ViewType.Doughnut);

                    foreach (DataRow row in dt.Rows)
                    {
                        series.Points.Add(new SeriesPoint(
                            row["IslemTipi"].ToString(),
                            Convert.ToInt32(row["Sayi"])
                        ));
                    }

                    PieSeriesView view = (PieSeriesView)series.View;
                    view.RuntimeExploding = true;
                    series.LegendTextPattern = "{A}: {V} ({VP:P0})";

                    chartDistribution.Series.Clear();
                    chartDistribution.Series.Add(series);
                    
                    chartDistribution.Titles.Clear();
                    ChartTitle title = new ChartTitle();
                    title.Text = "Sistem Geneli İşlem Tipi Dağılımı";
                    title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    chartDistribution.Titles.Add(title);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Dağılım grafiği yüklenemedi: {ex.Message}", "Hata");
            }
        }

        private void LoadHeadquartersComparisonChart()
        {
            try
            {
                chartComparison.Visible = true;

                string query = @"
                    SELECT 
                        s.SubeAdi as Sube,
                        COUNT(*) as IslemSayisi,
                        SUM(CASE WHEN i.ParaBirimi = 'TL' THEN i.Tutar ELSE 0 END) as ToplamTutar
                    FROM Islem i
                    INNER JOIN Sube s ON i.SubeID = s.SubeID
                    WHERE i.IslemTarihi >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
                    AND i.BasariliMi = 1
                    GROUP BY s.SubeID, s.SubeAdi
                    ORDER BY ToplamTutar DESC";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    var seriesCount = new Series("İşlem Sayısı", ViewType.Bar);
                    var seriesAmount = new Series("Toplam Tutar (₺)", ViewType.Bar);

                    foreach (DataRow row in dt.Rows)
                    {
                        string branchName = row["Sube"].ToString();
                        seriesCount.Points.Add(new SeriesPoint(branchName, Convert.ToInt32(row["IslemSayisi"])));
                        seriesAmount.Points.Add(new SeriesPoint(branchName, Convert.ToDecimal(row["ToplamTutar"])));
                    }

                    ((BarSeriesView)seriesCount.View).Color = Color.FromArgb(52, 152, 219);
                    ((BarSeriesView)seriesAmount.View).Color = Color.FromArgb(241, 196, 15);

                    chartComparison.Series.Clear();
                    chartComparison.Series.Add(seriesCount);
                    chartComparison.Series.Add(seriesAmount);
                    
                    chartComparison.Titles.Clear();
                    ChartTitle title = new ChartTitle();
                    title.Text = "Şube Performans Karşılaştırması (Son 30 Gün)";
                    title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    chartComparison.Titles.Add(title);

                    // Create secondary Y-axis for amount
                    XYDiagram diagram = (XYDiagram)chartComparison.Diagram;
                    SecondaryAxisY axisY = new SecondaryAxisY();
                    diagram.SecondaryAxesY.Add(axisY);
                    ((BarSeriesView)seriesAmount.View).AxisY = axisY;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Karşılaştırma grafiği yüklenemedi: {ex.Message}", "Hata");
            }
        }

        private void LoadSystemInfo()
        {
            try
            {
                lblBranchName.Text = "Sistem Geneli İstatistikler";

                string query = @"
                    SELECT 
                        COUNT(DISTINCT SubeID) as SubeSayisi,
                        (SELECT COUNT(*) FROM Kullanici WHERE AktifMi = 1 AND RolID IN (2,3)) as ToplamCalisan,
                        (SELECT COUNT(*) FROM Islem WHERE DATE(IslemTarihi) = CURDATE() AND BasariliMi = 1) as BugunIslem
                    FROM Sube 
                    WHERE AktifMi = 1";

                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblBranchDetails.Text = $"Aktif Şube: {row["SubeSayisi"]} | " +
                                          $"Toplam Çalışan: {row["ToplamCalisan"]} | " +
                                          $"Bugün İşlem: {row["BugunIslem"]}";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Sistem bilgileri yüklenemedi: {ex.Message}", "Hata");
            }
        }

        #endregion

        #region Common Methods

        private void LoadBranchInfo()
        {
            try
            {
                if (_kullanici.SubeID.HasValue)
                {
                    lblBranchName.Text = _kullanici.SubeAdi ?? "Bilinmiyor";

                    string query = $@"
                        SELECT 
                            SubeKodu,
                            Sehir,
                            Telefon,
                            CalisanSayisi
                        FROM Sube 
                        WHERE SubeID = {_kullanici.SubeID.Value}";

                    DataTable dt;
                    string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                    if (hata == null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        lblBranchDetails.Text = $"Kod: {row["SubeKodu"]} | " +
                                              $"Şehir: {row["Sehir"]} | " +
                                              $"Tel: {row["Telefon"]} | " +
                                              $"Çalışan: {row["CalisanSayisi"]}";
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Şube bilgileri yüklenemedi: {ex.Message}", "Hata");
            }
        }

        private string GetScalarValue(string query)
        {
            try
            {
                DataTable dt;
                string hata = _dataAccess.ExecuteQuery(query, null, out dt);

                if (hata == null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
            }
            catch
            {
                // Silent fail
            }

            return "0";
        }



        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            // Reload based on role
            switch (_kullanici.YetkiSeviyesi)
            {
                case 2:
                    LoadEmployeeDashboard();
                    break;
                case 3:
                    LoadManagerDashboard();
                    break;
                case 4:
                    LoadHeadquartersDashboard();
                    break;
            }

            XtraMessageBox.Show("Dashboard güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
