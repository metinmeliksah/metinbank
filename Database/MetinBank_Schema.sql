-- ================================================
-- METIN BANK - DATABASE SCHEMA
-- MySQL 8.0
-- ================================================

-- Drop existing database if exists
DROP DATABASE IF EXISTS MetinBankDB;

-- Create database
CREATE DATABASE MetinBankDB CHARACTER SET utf8mb4 COLLATE utf8mb4_turkish_ci;
USE MetinBankDB;

-- ================================================
-- 1. ROL TABLOSU
-- ================================================
CREATE TABLE Rol (
    RolID INT AUTO_INCREMENT PRIMARY KEY,
    RolAdi VARCHAR(50) NOT NULL UNIQUE,
    RolAciklama VARCHAR(200),
    YetkiSeviyesi INT NOT NULL COMMENT '1:Müşteri, 2:Çalışan, 3:Müdür, 4:Genel Merkez',
    INDEX idx_rol_yetki (YetkiSeviyesi)
) ENGINE=InnoDB;

-- ================================================
-- 2. SUBE TABLOSU
-- ================================================
CREATE TABLE Sube (
    SubeID INT AUTO_INCREMENT PRIMARY KEY,
    SubeKodu VARCHAR(5) NOT NULL UNIQUE COMMENT '00001 formatında',
    SubeAdi VARCHAR(100) NOT NULL,
    BolgeKodu VARCHAR(10),
    Sehir VARCHAR(30) NOT NULL,
    Ilce VARCHAR(30),
    Adres TEXT,
    Telefon VARCHAR(15),
    Email VARCHAR(100),
    MudurID INT NULL,
    KasaBakiyesi DECIMAL(18,2) DEFAULT 0.00,
    AcilisTarihi DATE NOT NULL,
    AktifMi BOOLEAN DEFAULT TRUE,
    CalisanSayisi INT DEFAULT 0,
    CalismaBaslangic TIME DEFAULT '09:00:00',
    CalismaBitis TIME DEFAULT '17:00:00',
    INDEX idx_sube_kod (SubeKodu),
    INDEX idx_sube_aktif (AktifMi)
) ENGINE=InnoDB;

-- ================================================
-- 3. KULLANICI TABLOSU
-- ================================================
CREATE TABLE Kullanici (
    KullaniciID INT AUTO_INCREMENT PRIMARY KEY,
    KullaniciAdi VARCHAR(50) NOT NULL UNIQUE,
    Sifre VARCHAR(64) NOT NULL COMMENT 'SHA256 Hash',
    SifreTuzu VARCHAR(32) NOT NULL COMMENT 'Salt',
    RolID INT NOT NULL,
    SubeID INT NULL,
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    Email VARCHAR(100),
    Telefon VARCHAR(15),
    AktifMi BOOLEAN DEFAULT TRUE,
    SonGirisTarihi DATETIME NULL,
    BasarisizGirisSayisi INT DEFAULT 0,
    HesapKilitliMi BOOLEAN DEFAULT FALSE,
    SonSifreDegistirmeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    GuncellemeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (RolID) REFERENCES Rol(RolID),
    FOREIGN KEY (SubeID) REFERENCES Sube(SubeID),
    INDEX idx_kullanici_ad (KullaniciAdi),
    INDEX idx_kullanici_rol (RolID),
    INDEX idx_kullanici_sube (SubeID),
    INDEX idx_kullanici_aktif (AktifMi)
) ENGINE=InnoDB;

-- Şube tablosuna foreign key ekleme
ALTER TABLE Sube ADD FOREIGN KEY (MudurID) REFERENCES Kullanici(KullaniciID);

-- ================================================
-- 4. MUSTERI TABLOSU
-- ================================================
CREATE TABLE Musteri (
    MusteriID INT AUTO_INCREMENT PRIMARY KEY,
    MusteriNo VARCHAR(10) NOT NULL UNIQUE,
    TCKN BIGINT NOT NULL UNIQUE COMMENT '11 haneli',
    VergiNo BIGINT NULL COMMENT 'Kurumsal için',
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    DogumTarihi DATE,
    DogumYeri VARCHAR(50),
    AnneAdi VARCHAR(50),
    BabaAdi VARCHAR(50),
    Cinsiyet CHAR(1) COMMENT 'E/K',
    MedeniDurum VARCHAR(20),
    Telefon VARCHAR(15),
    CepTelefon VARCHAR(15) NOT NULL,
    Email VARCHAR(100),
    Adres TEXT,
    Il VARCHAR(30),
    Ilce VARCHAR(30),
    PostaKodu VARCHAR(10),
    GelirDurumu DECIMAL(18,2),
    MeslekBilgisi VARCHAR(100),
    MusteriTipi VARCHAR(20) DEFAULT 'Bireysel' COMMENT 'Bireysel/Kurumsal',
    MusteriSegmenti VARCHAR(20) DEFAULT 'Standart' COMMENT 'Standart/Premium/VIP',
    KayitTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    KayitSubeID INT NOT NULL,
    AktifMi BOOLEAN DEFAULT TRUE,
    SonGuncellemeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (KayitSubeID) REFERENCES Sube(SubeID),
    INDEX idx_musteri_no (MusteriNo),
    INDEX idx_musteri_tckn (TCKN),
    INDEX idx_musteri_sube (KayitSubeID),
    INDEX idx_musteri_aktif (AktifMi)
) ENGINE=InnoDB;

-- ================================================
-- 5. HESAP TABLOSU
-- ================================================
CREATE TABLE Hesap (
    HesapID INT AUTO_INCREMENT PRIMARY KEY,
    HesapNo BIGINT NOT NULL UNIQUE COMMENT '16 haneli',
    IBAN VARCHAR(26) NOT NULL UNIQUE,
    MusteriID INT NOT NULL,
    HesapTipi VARCHAR(20) NOT NULL COMMENT 'TL/USD/EUR',
    HesapCinsi VARCHAR(30) NOT NULL COMMENT 'Vadesiz/Vadeli/Cocuk/Maas/Yatirim',
    Bakiye DECIMAL(18,2) DEFAULT 0.00,
    BlokeBakiye DECIMAL(18,2) DEFAULT 0.00 COMMENT 'Bekleyen işlemler',
    KullanilabilirBakiye DECIMAL(18,2) GENERATED ALWAYS AS (Bakiye - BlokeBakiye) STORED,
    FaizOrani DECIMAL(5,2) DEFAULT 0.00 COMMENT 'Vadeli hesaplar için',
    VadeTarihi DATE NULL,
    AcilisTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    KapanisTarihi DATETIME NULL,
    Durum VARCHAR(20) DEFAULT 'Aktif' COMMENT 'Aktif/Pasif/Kapalı/Blokeli',
    SubeID INT NOT NULL,
    GunlukTransferLimiti DECIMAL(18,2) DEFAULT 20000.00,
    AylikTransferLimiti DECIMAL(18,2) DEFAULT 500000.00,
    OlusturanKullaniciID INT NOT NULL,
    OnaylayanKullaniciID INT NULL,
    OnayTarihi DATETIME NULL,
    FOREIGN KEY (MusteriID) REFERENCES Musteri(MusteriID),
    FOREIGN KEY (SubeID) REFERENCES Sube(SubeID),
    FOREIGN KEY (OlusturanKullaniciID) REFERENCES Kullanici(KullaniciID),
    FOREIGN KEY (OnaylayanKullaniciID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_hesap_no (HesapNo),
    INDEX idx_hesap_iban (IBAN),
    INDEX idx_hesap_musteri (MusteriID),
    INDEX idx_hesap_sube (SubeID),
    INDEX idx_hesap_durum (Durum)
) ENGINE=InnoDB;

-- ================================================
-- 6. ISLEM TABLOSU
-- ================================================
CREATE TABLE Islem (
    IslemID BIGINT AUTO_INCREMENT PRIMARY KEY,
    IslemReferansNo VARCHAR(20) NOT NULL UNIQUE COMMENT 'TRX+timestamp',
    KaynakHesapID INT NULL,
    HedefHesapID INT NULL,
    HedefIBAN VARCHAR(26) NULL COMMENT 'Dış banka için',
    IslemTipi VARCHAR(30) NOT NULL COMMENT 'Yatırma/Çekme/Havale/EFT/Virman/Döviz',
    IslemAltTipi VARCHAR(30) COMMENT 'Nakit/Transfer/ATM/POS',
    Tutar DECIMAL(18,2) NOT NULL,
    ParaBirimi VARCHAR(3) DEFAULT 'TL',
    DovizKuru DECIMAL(10,4) NULL,
    IslemUcreti DECIMAL(18,2) DEFAULT 0.00,
    IslemTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    ValorTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    Aciklama VARCHAR(500),
    AliciAdi VARCHAR(100),
    KullaniciID INT NOT NULL,
    SubeID INT NOT NULL,
    OnayDurumu VARCHAR(20) DEFAULT 'Tamamlandı' COMMENT 'Beklemede/Onaylandı/Reddedildi/Tamamlandı',
    OnaylayanID INT NULL,
    OnayTarihi DATETIME NULL,
    RedNedeni TEXT NULL,
    KanalTipi VARCHAR(20) DEFAULT 'Sube' COMMENT 'Sube/Web/Mobil/ATM',
    IPAdresi VARCHAR(45),
    BasariliMi BOOLEAN DEFAULT TRUE,
    HataMesaji TEXT NULL,
    FOREIGN KEY (KaynakHesapID) REFERENCES Hesap(HesapID),
    FOREIGN KEY (HedefHesapID) REFERENCES Hesap(HesapID),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanici(KullaniciID),
    FOREIGN KEY (SubeID) REFERENCES Sube(SubeID),
    FOREIGN KEY (OnaylayanID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_islem_ref (IslemReferansNo),
    INDEX idx_islem_kaynak (KaynakHesapID),
    INDEX idx_islem_hedef (HedefHesapID),
    INDEX idx_islem_tarih (IslemTarihi),
    INDEX idx_islem_durum (OnayDurumu),
    INDEX idx_islem_tip (IslemTipi)
) ENGINE=InnoDB;

-- ================================================
-- 7. BANKA KARTI TABLOSU
-- ================================================
CREATE TABLE BankaKarti (
    KartID INT AUTO_INCREMENT PRIMARY KEY,
    HesapID INT NOT NULL,
    KartNo BIGINT NOT NULL UNIQUE COMMENT '16 haneli',
    KartTipi VARCHAR(20) DEFAULT 'Debit' COMMENT 'Debit/Credit',
    SonKullanmaTarihi DATE NOT NULL,
    CVV VARCHAR(256) NOT NULL COMMENT 'AES Encrypted',
    Durum VARCHAR(20) DEFAULT 'Aktif' COMMENT 'Aktif/Pasif/Iptal/Blokeli/KayipCalinti',
    BasvuruTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    AktivasyonTarihi DATETIME NULL,
    IptalTarihi DATETIME NULL,
    GunlukHarcamaLimiti DECIMAL(18,2) DEFAULT 5000.00,
    AylikHarcamaLimiti DECIMAL(18,2) DEFAULT 50000.00,
    GunlukCekimLimiti DECIMAL(18,2) DEFAULT 5000.00,
    KartSahibiAdi VARCHAR(100) NOT NULL,
    FOREIGN KEY (HesapID) REFERENCES Hesap(HesapID),
    INDEX idx_kart_no (KartNo),
    INDEX idx_kart_hesap (HesapID),
    INDEX idx_kart_durum (Durum)
) ENGINE=InnoDB;

-- ================================================
-- 8. DOVIZ KUR TABLOSU
-- ================================================
CREATE TABLE DovizKur (
    KurID INT AUTO_INCREMENT PRIMARY KEY,
    ParaBirimi VARCHAR(3) NOT NULL,
    AlisFiyati DECIMAL(10,4) NOT NULL,
    SatisFiyati DECIMAL(10,4) NOT NULL,
    GuncellemeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    KayitTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_kur_para (ParaBirimi),
    INDEX idx_kur_tarih (GuncellemeTarihi)
) ENGINE=InnoDB;

-- ================================================
-- 9. ISLEM LOG TABLOSU
-- ================================================
CREATE TABLE IslemLog (
    LogID BIGINT AUTO_INCREMENT PRIMARY KEY,
    KullaniciID INT NULL,
    LogTipi VARCHAR(50) NOT NULL COMMENT 'Islem/Goruntuleme/Sistem/Onay/Guvenlik',
    IslemTipi VARCHAR(100),
    TabloAdi VARCHAR(50),
    KayitID BIGINT,
    OncekiDeger TEXT COMMENT 'JSON',
    YeniDeger TEXT COMMENT 'JSON',
    IslemDetay TEXT,
    IPAdresi VARCHAR(45),
    MacAdresi VARCHAR(17),
    SessionID VARCHAR(100),
    Tarih DATETIME DEFAULT CURRENT_TIMESTAMP,
    BasariliMi BOOLEAN DEFAULT TRUE,
    HataMesaji TEXT,
    FOREIGN KEY (KullaniciID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_log_tarih (Tarih),
    INDEX idx_log_tip (LogTipi),
    INDEX idx_log_kullanici (KullaniciID)
) ENGINE=InnoDB;

-- ================================================
-- 10. GORUNTULEME LOG TABLOSU
-- ================================================
CREATE TABLE GoruntulemeLog (
    GoruntulemeID BIGINT AUTO_INCREMENT PRIMARY KEY,
    KullaniciID INT NOT NULL,
    GoruntulenenTablo VARCHAR(50),
    GoruntulenenKayitID BIGINT,
    SorguParametreleri TEXT COMMENT 'JSON',
    KayitSayisi INT,
    IPAdresi VARCHAR(45),
    Tarih DATETIME DEFAULT CURRENT_TIMESTAMP,
    IslemSuresi INT COMMENT 'milisaniye',
    FOREIGN KEY (KullaniciID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_gor_tarih (Tarih),
    INDEX idx_gor_kullanici (KullaniciID)
) ENGINE=InnoDB;

-- ================================================
-- 11. LOGIN LOG TABLOSU
-- ================================================
CREATE TABLE LoginLog (
    LoginLogID BIGINT AUTO_INCREMENT PRIMARY KEY,
    KullaniciID INT NULL,
    KullaniciAdi VARCHAR(50),
    IslemTipi VARCHAR(20) NOT NULL COMMENT 'Login/Logout/FailedLogin',
    BasariliMi BOOLEAN DEFAULT TRUE,
    IPAdresi VARCHAR(45),
    MacAdresi VARCHAR(17),
    Tarayici VARCHAR(200),
    Cihaz VARCHAR(100),
    IsletimSistemi VARCHAR(50),
    Tarih DATETIME DEFAULT CURRENT_TIMESTAMP,
    HataMesaji VARCHAR(500),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_login_tarih (Tarih),
    INDEX idx_login_kullanici (KullaniciID),
    INDEX idx_login_ip (IPAdresi)
) ENGINE=InnoDB;

-- ================================================
-- 12. ONAY LOG TABLOSU
-- ================================================
CREATE TABLE OnayLog (
    OnayLogID INT AUTO_INCREMENT PRIMARY KEY,
    IslemID BIGINT NULL,
    IslemTipi VARCHAR(50),
    TalepEdenID INT NOT NULL,
    OnaylayanID INT NULL,
    OnayDurumu VARCHAR(20) DEFAULT 'Beklemede' COMMENT 'Beklemede/Onaylandı/Reddedildi',
    OnayTarihi DATETIME NULL,
    RedNedeni TEXT,
    TalepTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    BeklenenOnaylayanRol VARCHAR(50),
    FOREIGN KEY (IslemID) REFERENCES Islem(IslemID),
    FOREIGN KEY (TalepEdenID) REFERENCES Kullanici(KullaniciID),
    FOREIGN KEY (OnaylayanID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_onay_durum (OnayDurumu),
    INDEX idx_onay_islem (IslemID)
) ENGINE=InnoDB;

-- ================================================
-- 13. GUVENLIK LOG TABLOSU
-- ================================================
CREATE TABLE GuvenlikLog (
    GuvenlikLogID BIGINT AUTO_INCREMENT PRIMARY KEY,
    OlayTipi VARCHAR(50) NOT NULL COMMENT 'YetkiIhlali/LimitAsimi/SuphelijIslem/CokluGiris',
    KullaniciID INT NULL,
    IPAdresi VARCHAR(45),
    OlayDetay TEXT,
    RiskSeviyesi VARCHAR(20) DEFAULT 'Dusuk' COMMENT 'Dusuk/Orta/Yuksek/Kritik',
    Tarih DATETIME DEFAULT CURRENT_TIMESTAMP,
    IslemeAlindiMi BOOLEAN DEFAULT FALSE,
    IslemTarihi DATETIME NULL,
    FOREIGN KEY (KullaniciID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_guv_tarih (Tarih),
    INDEX idx_guv_risk (RiskSeviyesi)
) ENGINE=InnoDB;

-- ================================================
-- 14. BILDIRIM TABLOSU
-- ================================================
CREATE TABLE Bildirim (
    BildirimID INT AUTO_INCREMENT PRIMARY KEY,
    KullaniciID INT NOT NULL,
    BildirimTipi VARCHAR(50) NOT NULL COMMENT 'OnayBekliyor/IslemTamamlandi/Uyari',
    Baslik VARCHAR(200) NOT NULL,
    Mesaj TEXT NOT NULL,
    OkunduMu BOOLEAN DEFAULT FALSE,
    OkunmaTarihi DATETIME NULL,
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    GecerlilikTarihi DATETIME,
    FOREIGN KEY (KullaniciID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_bildirim_kullanici (KullaniciID),
    INDEX idx_bildirim_okundu (OkunduMu)
) ENGINE=InnoDB;

-- ================================================
-- 15. HESAP SAYACI TABLOSU (IBAN Üretimi için)
-- ================================================
CREATE TABLE HesapSayaci (
    SubeID INT PRIMARY KEY,
    SonHesapNo BIGINT DEFAULT 0,
    GuncellemeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (SubeID) REFERENCES Sube(SubeID)
) ENGINE=InnoDB;

-- ================================================
-- INITIAL DATA
-- ================================================

-- Roller
INSERT INTO Rol (RolAdi, RolAciklama, YetkiSeviyesi) VALUES
('Müşteri', 'Banka müşterisi - web portal erişimi', 1),
('Şube Çalışanı', 'Şube görevlisi - temel işlemler', 2),
('Şube Müdürü', 'Şube yöneticisi - orta seviye onaylar', 3),
('Genel Merkez', 'Genel merkez yönetimi - tüm yetkiler', 4);

-- Şubeler
INSERT INTO Sube (SubeKodu, SubeAdi, BolgeKodu, Sehir, Ilce, Adres, Telefon, Email, KasaBakiyesi, AcilisTarihi, CalisanSayisi) VALUES
('00001', 'Metin Bank Merkez Şube', 'MRK', 'İstanbul', 'Beşiktaş', 'Barbaros Bulvarı No:1 Beşiktaş/İstanbul', '02121234567', 'merkez@metinbank.com.tr', 10000000.00, '2020-01-01', 15),
('00002', 'Metin Bank Kadıköy Şubesi', 'ASY', 'İstanbul', 'Kadıköy', 'Bağdat Caddesi No:150 Kadıköy/İstanbul', '02165551234', 'kadikoy@metinbank.com.tr', 5000000.00, '2020-06-15', 10),
('00003', 'Metin Bank Ankara Kızılay Şubesi', 'ANK', 'Ankara', 'Çankaya', 'Atatürk Bulvarı No:50 Kızılay/Ankara', '03124445566', 'kizilay@metinbank.com.tr', 8000000.00, '2021-01-10', 12);

-- Kullanıcılar (Şifreler: Password123! - SHA256 + Salt ile hashlenecek)
-- Genel Merkez
INSERT INTO Kullanici (KullaniciAdi, Sifre, SifreTuzu, RolID, SubeID, Ad, Soyad, Email, Telefon, AktifMi) VALUES
('gm.admin', 'TEMP_HASH', 'TEMP_SALT', 4, NULL, 'Ahmet', 'Yılmaz', 'ahmet.yilmaz@metinbank.com.tr', '05551234567', TRUE);

-- Şube Müdürleri
INSERT INTO Kullanici (KullaniciAdi, Sifre, SifreTuzu, RolID, SubeID, Ad, Soyad, Email, Telefon, AktifMi) VALUES
('merkez.mudur', 'TEMP_HASH', 'TEMP_SALT', 3, 1, 'Mehmet', 'Demir', 'mehmet.demir@metinbank.com.tr', '05559876543', TRUE),
('kadikoy.mudur', 'TEMP_HASH', 'TEMP_SALT', 3, 2, 'Ayşe', 'Kaya', 'ayse.kaya@metinbank.com.tr', '05557894561', TRUE),
('kizilay.mudur', 'TEMP_HASH', 'TEMP_SALT', 3, 3, 'Ali', 'Çelik', 'ali.celik@metinbank.com.tr', '05553216547', TRUE);

-- Şube Çalışanları
INSERT INTO Kullanici (KullaniciAdi, Sifre, SifreTuzu, RolID, SubeID, Ad, Soyad, Email, Telefon, AktifMi) VALUES
('merkez.calisan1', 'TEMP_HASH', 'TEMP_SALT', 2, 1, 'Fatma', 'Şahin', 'fatma.sahin@metinbank.com.tr', '05554567890', TRUE),
('kadikoy.calisan1', 'TEMP_HASH', 'TEMP_SALT', 2, 2, 'Mustafa', 'Arslan', 'mustafa.arslan@metinbank.com.tr', '05556543210', TRUE);

-- Müdür atama
UPDATE Sube SET MudurID = 2 WHERE SubeID = 1;
UPDATE Sube SET MudurID = 3 WHERE SubeID = 2;
UPDATE Sube SET MudurID = 4 WHERE SubeID = 3;

-- Örnek Müşteriler
INSERT INTO Musteri (MusteriNo, TCKN, Ad, Soyad, DogumTarihi, DogumYeri, AnneAdi, BabaAdi, Cinsiyet, MedeniDurum, Telefon, CepTelefon, Email, Adres, Il, Ilce, PostaKodu, GelirDurumu, MeslekBilgisi, MusteriTipi, MusteriSegmenti, KayitSubeID) VALUES
('M000000001', 12345678901, 'Zeynep', 'Yıldız', '1985-05-15', 'İstanbul', 'Emine', 'Hasan', 'K', 'Evli', '02161234567', '05321234567', 'zeynep.yildiz@email.com', 'Bağdat Caddesi No:100 Kadıköy', 'İstanbul', 'Kadıköy', '34740', 15000.00, 'Öğretmen', 'Bireysel', 'Standart', 1),
('M000000002', 98765432109, 'Can', 'Özdemir', '1990-08-20', 'Ankara', 'Ayşe', 'Mehmet', 'E', 'Bekar', '03125556677', '05339876543', 'can.ozdemir@email.com', 'Tunalı Hilmi Caddesi No:50 Çankaya', 'Ankara', 'Çankaya', '06100', 25000.00, 'Mühendis', 'Bireysel', 'Premium', 3);

-- Döviz Kurları
INSERT INTO DovizKur (ParaBirimi, AlisFiyati, SatisFiyati) VALUES
('USD', 32.50, 32.85),
('EUR', 35.20, 35.60),
('GBP', 41.10, 41.55);

-- Hesap Sayaçları
INSERT INTO HesapSayaci (SubeID, SonHesapNo) VALUES
(1, 0),
(2, 0),
(3, 0);

-- ================================================
-- STORED PROCEDURES
-- ================================================

DELIMITER //

-- IBAN Kontrol Rakamı Hesaplama
CREATE PROCEDURE sp_IbanKontrolRakamHesapla(
    IN p_BankaKod VARCHAR(5),
    IN p_SubeKod VARCHAR(5),
    IN p_HesapNo VARCHAR(16),
    OUT p_KontrolRakam VARCHAR(2)
)
BEGIN
    DECLARE v_IbanString VARCHAR(50);
    DECLARE v_Mod97 BIGINT;
    
    -- IBAN string oluştur: Banka+Rezerv+Şube+Hesap+TR(2734)+00
    SET v_IbanString = CONCAT(p_BankaKod, '0', p_SubeKod, p_HesapNo, '2734', '00');
    
    -- Mod 97 hesapla (basitleştirilmiş)
    SET v_Mod97 = CAST(v_IbanString AS UNSIGNED) MOD 97;
    SET p_KontrolRakam = LPAD(98 - v_Mod97, 2, '0');
END //

DELIMITER ;

-- ================================================
-- VIEWS
-- ================================================

-- Aktif Hesaplar Özet View
CREATE VIEW vw_AktifHesaplar AS
SELECT 
    h.HesapID,
    h.HesapNo,
    h.IBAN,
    h.HesapTipi,
    h.HesapCinsi,
    h.Bakiye,
    h.KullanilabilirBakiye,
    h.Durum,
    m.MusteriNo,
    CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdiSoyadi,
    m.TCKN,
    s.SubeAdi,
    h.AcilisTarihi
FROM Hesap h
INNER JOIN Musteri m ON h.MusteriID = m.MusteriID
INNER JOIN Sube s ON h.SubeID = s.SubeID
WHERE h.Durum = 'Aktif' AND m.AktifMi = TRUE;

-- Günlük İşlem Özeti View
CREATE VIEW vw_GunlukIslemOzet AS
SELECT 
    DATE(IslemTarihi) AS IslemGunu,
    IslemTipi,
    COUNT(*) AS IslemSayisi,
    SUM(Tutar) AS ToplamTutar,
    AVG(Tutar) AS OrtalamaTutar,
    SubeID
FROM Islem
WHERE BasariliMi = TRUE
GROUP BY DATE(IslemTarihi), IslemTipi, SubeID;

-- ================================================
-- INDEXES FOR PERFORMANCE
-- ================================================

-- Ek performans indexleri
CREATE INDEX idx_musteri_ad_soyad ON Musteri(Ad, Soyad);
CREATE INDEX idx_hesap_musteri_durum ON Hesap(MusteriID, Durum);
CREATE INDEX idx_islem_tarih_tip ON Islem(IslemTarihi, IslemTipi);

-- ================================================
-- COMMENTS
-- ================================================

ALTER TABLE Kullanici COMMENT = 'Sistem kullanıcıları (çalışan, müdür, genel merkez, müşteri)';
ALTER TABLE Musteri COMMENT = 'Banka müşterileri';
ALTER TABLE Hesap COMMENT = 'Müşteri hesapları';
ALTER TABLE Islem COMMENT = 'Tüm finansal işlemler';
ALTER TABLE BankaKarti COMMENT = 'Banka kartları';
ALTER TABLE IslemLog COMMENT = 'Sistem işlem logları - 7 yıl saklanır';
ALTER TABLE LoginLog COMMENT = 'Kullanıcı giriş/çıkış logları';

-- ================================================
-- END OF SCHEMA
-- ================================================

