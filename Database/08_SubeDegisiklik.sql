-- ================================================
-- ŞUBE DEĞİŞİKLİĞİ TALEP SİSTEMİ
-- ================================================

USE MetinBankDB;

-- Şube Değişiklik Talep Tablosu
CREATE TABLE IF NOT EXISTS SubeDegisiklikTalep (
    TalepID INT AUTO_INCREMENT PRIMARY KEY,
    KullaniciID INT NOT NULL COMMENT 'Talep eden kullanıcı',
    MevcutSubeID INT NOT NULL COMMENT 'Kullanıcının mevcut şubesi',
    YeniSubeID INT NOT NULL COMMENT 'Talep edilen yeni şube',
    TalepNedeni TEXT COMMENT 'Şube değişikliği nedeni',
    OnayDurumu ENUM('Beklemede', 'Onaylandı', 'Reddedildi') DEFAULT 'Beklemede',
    OnaylayanID INT NULL COMMENT 'Onaylayan müdür/yönetici',
    TalepTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    OnayTarihi DATETIME NULL,
    RedNedeni TEXT NULL COMMENT 'Red gerekçesi',
    FOREIGN KEY (KullaniciID) REFERENCES Kullanici(KullaniciID),
    FOREIGN KEY (MevcutSubeID) REFERENCES Sube(SubeID),
    FOREIGN KEY (YeniSubeID) REFERENCES Sube(SubeID),
    FOREIGN KEY (OnaylayanID) REFERENCES Kullanici(KullaniciID),
    INDEX idx_sube_degisiklik_kullanici (KullaniciID),
    INDEX idx_sube_degisiklik_durum (OnayDurumu),
    INDEX idx_sube_degisiklik_tarih (TalepTarihi)
) ENGINE=InnoDB COMMENT='Çalışan şube değişikliği talepleri';

-- ================================================
-- VIEW: Şube Değişiklik Talepleri Detaylı Görünüm
-- ================================================
CREATE OR REPLACE VIEW vw_SubeDegisiklikTalepleri AS
SELECT 
    sdt.TalepID,
    sdt.KullaniciID,
    k.KullaniciAdi,
    CONCAT(k.Ad, ' ', k.Soyad) AS TalepEdenAdSoyad,
    sdt.MevcutSubeID,
    s1.SubeAdi AS MevcutSubeAdi,
    s1.SubeKodu AS MevcutSubeKodu,
    sdt.YeniSubeID,
    s2.SubeAdi AS YeniSubeAdi,
    s2.SubeKodu AS YeniSubeKodu,
    sdt.TalepNedeni,
    sdt.OnayDurumu,
    sdt.OnaylayanID,
    CONCAT(o.Ad, ' ', o.Soyad) AS OnaylayanAdSoyad,
    sdt.TalepTarihi,
    sdt.OnayTarihi,
    sdt.RedNedeni,
    TIMESTAMPDIFF(HOUR, sdt.TalepTarihi, COALESCE(sdt.OnayTarihi, NOW())) AS BeklemeSuresi
FROM SubeDegisiklikTalep sdt
INNER JOIN Kullanici k ON sdt.KullaniciID = k.KullaniciID
INNER JOIN Sube s1 ON sdt.MevcutSubeID = s1.SubeID
INNER JOIN Sube s2 ON sdt.YeniSubeID = s2.SubeID
LEFT JOIN Kullanici o ON sdt.OnaylayanID = o.KullaniciID;

-- ================================================
-- STORED PROCEDURE: Bekleyen Talepleri Getir
-- ================================================
DELIMITER //

CREATE PROCEDURE IF NOT EXISTS sp_BekleyenSubeDegisiklikTalepleri()
BEGIN
    SELECT * FROM vw_SubeDegisiklikTalepleri
    WHERE OnayDurumu = 'Beklemede'
    ORDER BY TalepTarihi ASC;
END //

DELIMITER ;

-- ================================================
-- İNDEX AÇIKLAMALARI
-- ================================================
-- idx_sube_degisiklik_kullanici: Kullanıcının talep geçmişini hızlı sorgulamak için
-- idx_sube_degisiklik_durum: Bekleyen talepleri filtrelemek için
-- idx_sube_degisiklik_tarih: Tarih bazlı raporlama için

-- ================================================
-- ÖRNEK KULLANIM
-- ================================================
-- Bekleyen tüm talepleri getir:
-- SELECT * FROM vw_SubeDegisiklikTalepleri WHERE OnayDurumu = 'Beklemede';

-- Belirli bir kullanıcının tüm taleplerini getir:
-- SELECT * FROM vw_SubeDegisiklikTalepleri WHERE KullaniciID = 5 ORDER BY TalepTarihi DESC;

-- ================================================
-- END OF MIGRATION
-- ================================================
