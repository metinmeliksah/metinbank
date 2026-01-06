-- İşlem Ücretleri Tablosu
CREATE TABLE IF NOT EXISTS IslemUcretleri (
    IslemUcretiID INT AUTO_INCREMENT PRIMARY KEY,
    IslemTipi VARCHAR(50) NOT NULL COMMENT 'Havale, EFT, Virman, ParaYatirma, ParaCekme',
    IslemKanali VARCHAR(50) NOT NULL COMMENT 'Internet, Mobil, Sube',
    MinTutar DECIMAL(18, 2) NOT NULL DEFAULT 0,
    MaxTutar DECIMAL(18, 2) NULL COMMENT 'NULL ise sınırsız',
    Ucret DECIMAL(18, 2) NOT NULL DEFAULT 0,
    Aktif BIT(1) DEFAULT 1,
    OlusturmaTarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_islem_kanal (IslemTipi, IslemKanali)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- İnternet/Mobil Ücretleri (0 TL ama gözüksün diye)
-- Havale Ücretleri - İnternet/Mobil
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('Havale', 'Internet', 0, 7900, 0.00),
('Havale', 'Internet', 7900, 382500, 0.00),
('Havale', 'Internet', 382500, NULL, 0.00),
('Havale', 'Mobil', 0, 7900, 0.00),
('Havale', 'Mobil', 7900, 382500, 0.00),
('Havale', 'Mobil', 382500, NULL, 0.00);

-- EFT Ücretleri - İnternet/Mobil
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('EFT', 'Internet', 0, 7900, 0.00),
('EFT', 'Internet', 7900, 382500, 0.00),
('EFT', 'Internet', 382500, NULL, 0.00),
('EFT', 'Mobil', 0, 7900, 0.00),
('EFT', 'Mobil', 7900, 382500, 0.00),
('EFT', 'Mobil', 382500, NULL, 0.00);

-- Virman Ücretleri - İnternet/Mobil (Ücretsiz)
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('Virman', 'Internet', 0, NULL, 0.00),
('Virman', 'Mobil', 0, NULL, 0.00);

-- Para Yatırma - İnternet (ATM kabul edersek)
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('ParaYatirma', 'Internet', 0, NULL, 0.00);

-- Para Çekme - İnternet (ATM)
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('ParaCekme', 'Internet', 0, NULL, 0.00);

-- ============================================
-- ŞUBE ÜCRETLERİ (ÜCRETLENDIRME YAPILACAK)
-- ============================================

-- Havale Ücretleri - Şube
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('Havale', 'Sube', 0, 7900, 40.15),
('Havale', 'Sube', 7900, 382500, 80.30),
('Havale', 'Sube', 382500, NULL, 501.88);

-- EFT Ücretleri - Şube
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('EFT', 'Sube', 0, 7900, 40.15),
('EFT', 'Sube', 7900, 382500, 80.30),
('EFT', 'Sube', 382500, NULL, 501.88);

-- Virman Ücretleri - Şube
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('Virman', 'Sube', 0, 7900, 20.00),
('Virman', 'Sube', 7900, 382500, 40.00),
('Virman', 'Sube', 382500, NULL, 250.00);

-- Para Yatırma - Şube
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('ParaYatirma', 'Sube', 0, 7900, 10.00),
('ParaYatirma', 'Sube', 7900, 382500, 20.00),
('ParaYatirma', 'Sube', 382500, NULL, 100.00);

-- Para Çekme - Şube
INSERT INTO IslemUcretleri (IslemTipi, IslemKanali, MinTutar, MaxTutar, Ucret) VALUES
('ParaCekme', 'Sube', 0, 7900, 10.00),
('ParaCekme', 'Sube', 7900, 382500, 20.00),
('ParaCekme', 'Sube', 382500, NULL, 100.00);

-- Ücret hesaplama için view
CREATE OR REPLACE VIEW VW_IslemUcretleri AS
SELECT 
    IslemUcretiID,
    IslemTipi,
    IslemKanali,
    MinTutar,
    COALESCE(MaxTutar, 999999999) AS MaxTutar,
    Ucret,
    Aktif
FROM IslemUcretleri
WHERE Aktif = 1;
