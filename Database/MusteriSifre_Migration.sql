-- ================================================
-- MÜŞTER İŞİFRE VE ŞSIFRE GEÇMİŞİ TABLOLARI
-- ================================================

USE MetinBankDB;

-- ================================================
-- MÜŞTERI ŞIFRE TABLOSU (İnternet Bankacılığı)
-- ================================================
CREATE TABLE IF NOT EXISTS MusteriSifre (
    MusteriSifreID INT AUTO_INCREMENT PRIMARY KEY,
    MusteriID INT NOT NULL UNIQUE,
    Sifre VARCHAR(64) NOT NULL COMMENT 'SHA256 Hash',
    SifreTuzu VARCHAR(32) NOT NULL COMMENT 'Salt',
    OlusturmaTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    SonGuncellemeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    SonKullanimTarihi DATETIME NULL,
    BasarisizGirisSayisi INT DEFAULT 0,
    HesapKilitliMi BOOLEAN DEFAULT FALSE,
    KilitlenmeTarihi DATETIME NULL,
    AktifMi BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (MusteriID) REFERENCES Musteri(MusteriID) ON DELETE CASCADE,
    INDEX idx_musteri_sifremusteri (MusteriID),
    INDEX idx_musteri_sifre_aktif (AktifMi)
) ENGINE=InnoDB COMMENT='İnternet bankacılığı müşteri şifreleri';

-- ====== ==========================================
-- ŞIFRE GEÇMİŞİ TABLOSU
-- ================================================
CREATE TABLE IF NOT EXISTS SifreGecmisi (
    SifreGecmisiID INT AUTO_INCREMENT PRIMARY KEY,
    MusteriID INT NOT NULL,
    EskiSifre VARCHAR(64) NOT NULL COMMENT 'SHA256 Hash',
    EskiSifreTuzu VARCHAR(32) NOT NULL,
    DegistirmeTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    DegistirenKullaniciID INT NULL COMMENT 'NULL ise müşteri kendisi değiştirdi',
    DegistirmeNedeni VARCHAR(100) COMMENT 'İlkKayit/KullaniciIstegi/ZorunluDegisim/SifreUnuttum',
    IPAdresi VARCHAR(45),
    FOREIGN KEY (MusteriID) REFERENCES Musteri(MusteriID) ON DELETE CASCADE,
    FOREIGN KEY (DegistirenKullaniciID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_sifre_gecmisi_musteri (MusteriID),
    INDEX idx_sifre_gecmisi_tarih (DegistirmeTarihi)
) ENGINE=InnoDB COMMENT='Müşteri şifre değişiklik geçmişi - son 5 şifre saklanır';

-- ================================================
-- İLK VERİ: Örnek müşteri şifreleri
-- ================================================
-- Şifre: Test123!
-- Bu şifreler SecurityHelper ile oluşturulmalı
-- Örnek için placeholder değerler

INSERT INTO MusteriSifre (MusteriID, Sifre, SifreTuzu, AktifMi) 
SELECT M usteriID, 'TEMP_PASSWORD_HASH', 'TEMP_PASSWORD_SALT', TRUE
FROM Musteri 
WHERE MusteriID IN (1, 2)
ON DUPLICATE KEY UPDATE Sifre = Sifre; -- Eğer zaten varsa güncelleme

-- ================================================
-- VIEW: Müşteri Şifre Durumu
-- ================================================
CREATE OR REPLACE VIEW vw_MusteriSifreDurumu AS
SELECT 
    m.MusteriID,
    m.MusteriNo,
    CONCAT(m.Ad, ' ', m.Soyad) AS MusteriAdi,
    m.TCKN,
    ms.AktifMi AS SifreAktif,
    ms.HesapKilitliMi AS SifreKilitli,
    ms.BasarisizGirisSayisi,
    ms.SonKullanimTarihi,
    ms.SonGuncellemeTarihi AS SifreSonGuncellemeTarihi,
    DATEDIFF(CURDATE(), DATE(ms.SonGuncellemeTarihi)) AS SifreYasiGun,
    CASE 
        WHEN DATEDIFF(CURDATE(), DATE(ms.SonGuncellemeTarihi)) > 90 THEN TRUE
        ELSE FALSE
    END AS SifreDegistirilmeli
FROM Musteri m
LEFT JOIN MusteriSifre ms ON m.MusteriID = ms.MusteriID
WHERE m.AktifMi = TRUE;

-- ================================================
-- STORED PROCEDURE: Şifre Geçmişi Temizleme
-- (Son 5 şifre hariç eskilerini sil)
-- ================================================
DELIMITER //

CREATE PROCEDURE sp_SifreGecmisiTemizle(IN p_MusteriID INT)
BEGIN
    DELETE FROM SifreGecmisi
    WHERE MusteriID = p_MusteriID
    AND SifreGecmisiID NOT IN (
        SELECT SifreGecmisiID FROM (
            SELECT SifreGecmisiID 
            FROM SifreGecmisi 
            WHERE MusteriID = p_MusteriID
            ORDER BY DegistirmeTarihi DESC
            LIMIT 5
        ) AS KeepRecords
    );
END //

DELIMITER ;

-- ================================================
-- TRIGGER: Şifre değiştiğinde geçmişe kaydet
-- ================================================
DELIMITER //

CREATE TRIGGER trg_MusteriSifre_AfterUpdate
AFTER UPDATE ON MusteriSifre
FOR EACH ROW
BEGIN
    IF OLD.Sifre != NEW.Sifre THEN
        -- Eski şifreyi geçmişe kaydet
        INSERT INTO SifreGecmisi (MusteriID, EskiSifre, EskiSifreTuzu, DegistirmeNedeni)
        VALUES (OLD.MusteriID, OLD.Sifre, OLD.SifreTuzu, 'KullaniciIstegi');
        
        -- Geçmiş temizliği yap
        CALL sp_SifreGecmisiTemizle(OLD.MusteriID);
    END IF;
END //

DELIMITER ;

-- ================================================
-- END
-- ================================================
