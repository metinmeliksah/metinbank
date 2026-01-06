-- ================================================
-- 06_KrediMevduat.sql
-- Kredi ve Mevduat Sistemi Tabloları (2026 Düzenlemeleri)
-- ================================================

USE MetinBankDB;

-- ================================================
-- 1. KREDI ORANLARI TABLOSU
-- Kredi türüne ve tutarına göre faiz oranları
-- ================================================
CREATE TABLE IF NOT EXISTS KrediOranlari (
    OranID INT AUTO_INCREMENT PRIMARY KEY,
    KrediTipi VARCHAR(50) NOT NULL DEFAULT 'Ihtiyac' COMMENT 'Sadece Ihtiyac olacak',
    BaslangicTutar DECIMAL(18,2) NOT NULL,
    BitisTutar DECIMAL(18,2) NOT NULL,
    MinVade INT NOT NULL,
    MaxVade INT NOT NULL,
    FaizOrani DECIMAL(5,4) NOT NULL COMMENT 'Aylık Faiz Oranı (Örn: 3.50)',
    AktifMi BOOLEAN DEFAULT TRUE,
    GuncellemeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- ================================================
-- 2. MEVDUAT ORANLARI TABLOSU
-- Mevduat vadesine ve tutarına göre faiz oranları
-- ================================================
CREATE TABLE IF NOT EXISTS MevduatOranlari (
    OranID INT AUTO_INCREMENT PRIMARY KEY,
    ParaBirimi VARCHAR(3) NOT NULL DEFAULT 'TL',
    BaslangicTutar DECIMAL(18,2) DEFAULT 0,
    BitisTutar DECIMAL(18,2) DEFAULT 999999999,
    MinGun INT NOT NULL,
    MaxGun INT NOT NULL,
    FaizOrani DECIMAL(5,2) NOT NULL COMMENT 'Yıllık Brüt Faiz (Örn: 45.00)',
    StopajOrani DECIMAL(5,2) NOT NULL COMMENT 'Vergi Oranı (örn: 17.5)',
    Aciklama VARCHAR(100),
    AktifMi BOOLEAN DEFAULT TRUE
) ENGINE=InnoDB;

-- ================================================
-- 3. KREDI BASVURU TABLOSU
-- Web ve Şubeden gelen kredi başvuruları
-- ================================================
CREATE TABLE IF NOT EXISTS KrediBasvuru (
    BasvuruID INT AUTO_INCREMENT PRIMARY KEY,
    MusteriID INT NULL COMMENT 'Mevcut müşteri ise',
    TCKN BIGINT NOT NULL,
    AdSoyad VARCHAR(100),
    CepTelefon VARCHAR(15),
    AylikGelir DECIMAL(18,2) NOT NULL,
    TalepEdilenTutar DECIMAL(18,2) NOT NULL,
    TalepEdilenVade INT NOT NULL,
    OnaylananTutar DECIMAL(18,2) NULL,
    OnaylananVade INT NULL,
    FaizOrani DECIMAL(5,4) NOT NULL,
    BasvuruTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    Kanal VARCHAR(20) DEFAULT 'Web' COMMENT 'Web/Sube/Mobil',
    Durum VARCHAR(20) DEFAULT 'Beklemede' COMMENT 'Beklemede/Onaylandi/Reddedildi/Kullandirildi/Iptal',
    RedNedeni VARCHAR(255),
    OnaylayanKullaniciID INT NULL,
    OnayTarihi DATETIME NULL,
    FOREIGN KEY (MusteriID) REFERENCES Musteri(MusteriID),
    FOREIGN KEY (OnaylayanKullaniciID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_basvuru_tckn (TCKN),
    INDEX idx_basvuru_durum (Durum)
) ENGINE=InnoDB;

-- ================================================
-- 4. BASLANGIC VERILERI
-- ================================================

-- Kredi Faiz Oranları (2026 Mevcut Piyasa Şartları Varsayılanı - Aylık %4.29 vb.)
TRUNCATE TABLE KrediOranlari;

-- 0 - 125.000 TL (Max 36 ay)
INSERT INTO KrediOranlari (KrediTipi, BaslangicTutar, BitisTutar, MinVade, MaxVade, FaizOrani) 
VALUES ('Ihtiyac', 0, 125000, 1, 36, 4.29);

-- 125.001 - 250.000 TL (Max 24 ay)
INSERT INTO KrediOranlari (KrediTipi, BaslangicTutar, BitisTutar, MinVade, MaxVade, FaizOrani) 
VALUES ('Ihtiyac', 125001, 250000, 1, 24, 4.19);

-- 250.001 TL ve Üzeri (Max 12 ay)
INSERT INTO KrediOranlari (KrediTipi, BaslangicTutar, BitisTutar, MinVade, MaxVade, FaizOrani) 
VALUES ('Ihtiyac', 250001, 10000000, 1, 12, 3.99);


-- Mevduat Faiz Oranları (Yıllık Brüt) ve Stopajlar (2026 Kuralları)
TRUNCATE TABLE MevduatOranlari;

-- 32 Günlük (Kısa Vade) - Stopaj %17.5
INSERT INTO MevduatOranlari (MinGun, MaxGun, FaizOrani, StopajOrani, Aciklama)
VALUES (1, 91, 45.00, 17.50, '32-91 Gün Arası (Kısa Vade)');

-- 92-180 Gün (Orta Vade) - Stopaj %17.5 (6 aya kadar dahildir dedi prompt)
INSERT INTO MevduatOranlari (MinGun, MaxGun, FaizOrani, StopajOrani, Aciklama)
VALUES (92, 180, 47.00, 17.50, '92-180 Gün Arası');

-- 181-365 Gün (Uzun Vade) - Stopaj %15 (1 yıla kadar)
INSERT INTO MevduatOranlari (MinGun, MaxGun, FaizOrani, StopajOrani, Aciklama)
VALUES (181, 365, 42.00, 15.00, '6 Ay - 1 Yıl Arası');

-- 366+ Gün (1 yıldan uzun) - Stopaj %12.5
INSERT INTO MevduatOranlari (MinGun, MaxGun, FaizOrani, StopajOrani, Aciklama)
VALUES (366, 9999, 35.00, 12.50, '1 Yıldan Uzun');

-- Döviz (USD/EUR) - Stopaj %25
INSERT INTO MevduatOranlari (ParaBirimi, MinGun, MaxGun, FaizOrani, StopajOrani, Aciklama)
VALUES ('USD', 1, 365, 2.50, 25.00, 'USD Vadeli');

INSERT INTO MevduatOranlari (ParaBirimi, MinGun, MaxGun, FaizOrani, StopajOrani, Aciklama)
VALUES ('EUR', 1, 365, 1.50, 25.00, 'EUR Vadeli');
