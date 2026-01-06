USE MetinBankDB;

-- ================================================
-- 5. KREDILER TABLOSU (Aktif Krediler)
-- Onaylanan ve kullandırılan kredilerin ana kaydı
-- ================================================
CREATE TABLE IF NOT EXISTS Krediler (
    KrediID INT AUTO_INCREMENT PRIMARY KEY,
    MusteriID INT NOT NULL,
    BasvuruID INT NOT NULL COMMENT 'Hangi başvurudan oluştuğu',
    KrediTutari DECIMAL(18,2) NOT NULL,
    Vade INT NOT NULL,
    FaizOrani DECIMAL(5,4) NOT NULL,
    ToplamGeriOdeme DECIMAL(18,2) NOT NULL,
    BaslangicTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    BitisTarihi DATETIME NOT NULL,
    KalanAnaPara DECIMAL(18,2) NOT NULL,
    Durum VARCHAR(20) DEFAULT 'Aktif' COMMENT 'Aktif/Kapandi/Takipte',
    FOREIGN KEY (MusteriID) REFERENCES Musteri(MusteriID),
    FOREIGN KEY (BasvuruID) REFERENCES KrediBasvuru(BasvuruID)
) ENGINE=InnoDB;

-- ================================================
-- 6. KREDI ODEME PLANI TABLOSU
-- Kredinin taksit detayları
-- ================================================
CREATE TABLE IF NOT EXISTS KrediOdemePlani (
    PlanID INT AUTO_INCREMENT PRIMARY KEY,
    KrediID INT NOT NULL,
    TaksitNo INT NOT NULL,
    VadeTarihi DATE NOT NULL,
    TaksitTutari DECIMAL(18,2) NOT NULL,
    AnaParaTutari DECIMAL(18,2) NOT NULL,
    FaizTutari DECIMAL(18,2) NOT NULL,
    VergiTutari DECIMAL(18,2) NOT NULL COMMENT 'KKDF + BSMV Toplamı',
    KalanAnaPara DECIMAL(18,2) NOT NULL,
    OdenenTutar DECIMAL(18,2) DEFAULT 0,
    OdemeTarihi DATETIME NULL,
    OdendiMi BOOLEAN DEFAULT FALSE,
    GecikmeGun INT DEFAULT 0,
    FOREIGN KEY (KrediID) REFERENCES Krediler(KrediID)
) ENGINE=InnoDB;
