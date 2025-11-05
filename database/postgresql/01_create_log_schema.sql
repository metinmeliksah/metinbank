/*
 * MetinBank - PostgreSQL Log Database Schema
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * PostgreSQL log ve analitik veritabanı şemaları
 * Standart: Tablo isimleri küçük harf, _ ile ayrılmış
 */

-- =====================================================
-- DATABASE OLUŞTURMA
-- =====================================================

-- Veritabanı oluştur (pgAdmin veya psql ile)
-- CREATE DATABASE metinbank_log
--     WITH OWNER = postgres
--     ENCODING = 'UTF8'
--     LC_COLLATE = 'Turkish_Turkey.1254'
--     LC_CTYPE = 'Turkish_Turkey.1254'
--     TABLESPACE = pg_default
--     CONNECTION LIMIT = -1;

-- =====================================================
-- SCHEMA OLUŞTURMA
-- =====================================================

-- Log şeması
CREATE SCHEMA IF NOT EXISTS log;

-- Analitik şeması
CREATE SCHEMA IF NOT EXISTS analitik;

-- Audit şeması
CREATE SCHEMA IF NOT EXISTS audit;

-- =====================================================
-- LOG TABLOLARI (log schema)
-- =====================================================

-- Sistem log tablosu
CREATE TABLE IF NOT EXISTS log.sistem_log (
    log_id BIGSERIAL PRIMARY KEY,
    islem_tip VARCHAR(50) NOT NULL,        -- MUSTERI_EKLE, HESAP_AC, PARA_YATIR
    islem_detay TEXT,                      -- Detaylı açıklama
    musteri_no BIGINT,                     -- İşlemi yapan/etkilenen müşteri
    hesap_no VARCHAR(50),                  -- İşlem yapılan hesap
    tutar NUMERIC(18,2),                   -- İşlem tutarı (varsa)
    op_ad VARCHAR(100),                    -- Operatör adı
    ip_adres VARCHAR(50),                  -- IP adresi
    session_id VARCHAR(100),               -- Session ID
    kayit_tarih TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Index'ler için
    INDEX idx_sistem_log_tarih (kayit_tarih DESC),
    INDEX idx_sistem_log_islem_tip (islem_tip),
    INDEX idx_sistem_log_musteri_no (musteri_no)
);

-- Hata log tablosu
CREATE TABLE IF NOT EXISTS log.hata_log (
    log_id BIGSERIAL PRIMARY KEY,
    hata_mesaj TEXT NOT NULL,              -- Hata mesajı
    hata_detay TEXT,                       -- Stack trace
    sp_ad VARCHAR(200),                    -- Stored procedure adı
    method_ad VARCHAR(200),                -- Method adı (C# tarafı)
    op_ad VARCHAR(100),                    -- Operatör adı
    ip_adres VARCHAR(50),                  -- IP adresi
    kayit_tarih TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Index'ler
    INDEX idx_hata_log_tarih (kayit_tarih DESC),
    INDEX idx_hata_log_sp_ad (sp_ad)
);

-- API log tablosu
CREATE TABLE IF NOT EXISTS log.api_log (
    log_id BIGSERIAL PRIMARY KEY,
    endpoint VARCHAR(500) NOT NULL,        -- API endpoint
    http_method VARCHAR(10) NOT NULL,      -- GET, POST, PUT, DELETE
    request_body TEXT,                     -- Request içeriği
    response_body TEXT,                    -- Response içeriği
    status_code INT,                       -- HTTP status code
    response_time_ms INT,                  -- Response süresi (ms)
    ip_adres VARCHAR(50),                  -- IP adresi
    user_agent TEXT,                       -- User agent
    musteri_no BIGINT,                     -- Müşteri no (varsa)
    kayit_tarih TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Index'ler
    INDEX idx_api_log_tarih (kayit_tarih DESC),
    INDEX idx_api_log_endpoint (endpoint),
    INDEX idx_api_log_status (status_code)
);

-- Login/Logout log tablosu
CREATE TABLE IF NOT EXISTS log.giris_log (
    log_id BIGSERIAL PRIMARY KEY,
    musteri_no BIGINT NOT NULL,            -- Müşteri numarası
    giris_tip VARCHAR(20) NOT NULL,        -- WEB, MOBILE, ATM, SUBE
    giris_basarili BOOLEAN NOT NULL,       -- Başarılı mı?
    hata_mesaj VARCHAR(500),               -- Başarısız ise sebep
    ip_adres VARCHAR(50),                  -- IP adresi
    device_info TEXT,                      -- Cihaz bilgisi
    location VARCHAR(200),                 -- Konum bilgisi
    giris_tarih TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    cikis_tarih TIMESTAMP,                 -- Çıkış tarihi
    session_suresi_dk INT,                 -- Session süresi (dakika)
    
    -- Index'ler
    INDEX idx_giris_log_tarih (giris_tarih DESC),
    INDEX idx_giris_log_musteri_no (musteri_no),
    INDEX idx_giris_log_basarili (giris_basarili)
);

-- =====================================================
-- ANALİTİK TABLOLARI (analitik schema)
-- =====================================================

-- Müşteri analitik özet
CREATE TABLE IF NOT EXISTS analitik.musteri_ozet (
    musteri_no BIGINT PRIMARY KEY,
    toplam_bakiye NUMERIC(18,2) DEFAULT 0,
    toplam_hesap_sayisi INT DEFAULT 0,
    toplam_kart_sayisi INT DEFAULT 0,
    toplam_kredi_sayisi INT DEFAULT 0,
    son_islem_tarih TIMESTAMP,
    son_giris_tarih TIMESTAMP,
    giris_sayisi INT DEFAULT 0,
    basarisiz_giris_sayisi INT DEFAULT 0,
    risk_skoru NUMERIC(5,2),               -- 0-100 arası risk skoru
    musteri_degeri VARCHAR(20),            -- DUSUK, ORTA, YUKSEK
    guncelleme_tarih TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    -- Index
    INDEX idx_musteri_ozet_risk (risk_skoru DESC),
    INDEX idx_musteri_ozet_deger (musteri_degeri)
);

-- Günlük işlem özeti
CREATE TABLE IF NOT EXISTS analitik.gunluk_ozet (
    ozet_id BIGSERIAL PRIMARY KEY,
    tarih DATE NOT NULL,
    toplam_musteri_sayisi INT DEFAULT 0,
    yeni_musteri_sayisi INT DEFAULT 0,
    toplam_islem_sayisi INT DEFAULT 0,
    toplam_islem_tutari NUMERIC(18,2) DEFAULT 0,
    havale_sayisi INT DEFAULT 0,
    havale_tutari NUMERIC(18,2) DEFAULT 0,
    eft_sayisi INT DEFAULT 0,
    eft_tutari NUMERIC(18,2) DEFAULT 0,
    para_yatirma_sayisi INT DEFAULT 0,
    para_yatirma_tutari NUMERIC(18,2) DEFAULT 0,
    para_cekme_sayisi INT DEFAULT 0,
    para_cekme_tutari NUMERIC(18,2) DEFAULT 0,
    kredi_basvuru_sayisi INT DEFAULT 0,
    kredi_onay_sayisi INT DEFAULT 0,
    kayit_tarih TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    -- Unique constraint
    UNIQUE (tarih),
    
    -- Index
    INDEX idx_gunluk_ozet_tarih (tarih DESC)
);

-- Hesap işlem analizi
CREATE TABLE IF NOT EXISTS analitik.hesap_islem_ozet (
    hesap_no VARCHAR(50) NOT NULL,
    ay DATE NOT NULL,                      -- Ayın ilk günü (2025-11-01)
    para_yatirma_sayisi INT DEFAULT 0,
    para_yatirma_tutari NUMERIC(18,2) DEFAULT 0,
    para_cekme_sayisi INT DEFAULT 0,
    para_cekme_tutari NUMERIC(18,2) DEFAULT 0,
    transfer_sayisi INT DEFAULT 0,
    transfer_tutari NUMERIC(18,2) DEFAULT 0,
    ortalama_bakiye NUMERIC(18,2) DEFAULT 0,
    minimum_bakiye NUMERIC(18,2) DEFAULT 0,
    maksimum_bakiye NUMERIC(18,2) DEFAULT 0,
    guncelleme_tarih TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (hesap_no, ay),
    
    -- Index
    INDEX idx_hesap_islem_ay (ay DESC)
);

-- =====================================================
-- AUDIT TABLOLARI (audit schema)
-- =====================================================

-- Müşteri değişiklikleri audit
CREATE TABLE IF NOT EXISTS audit.musteri_degisiklik (
    audit_id BIGSERIAL PRIMARY KEY,
    musteri_no BIGINT NOT NULL,
    islem_tip VARCHAR(20) NOT NULL,        -- INSERT, UPDATE, DELETE
    eski_deger JSONB,                      -- Eski değer (JSON)
    yeni_deger JSONB,                      -- Yeni değer (JSON)
    degisen_alanlar TEXT[],                -- Değişen alan isimleri
    op_ad VARCHAR(100),                    -- İşlemi yapan
    ip_adres VARCHAR(50),
    kayit_tarih TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Index
    INDEX idx_musteri_degisiklik_musteri (musteri_no),
    INDEX idx_musteri_degisiklik_tarih (kayit_tarih DESC)
);

-- Hesap değişiklikleri audit
CREATE TABLE IF NOT EXISTS audit.hesap_degisiklik (
    audit_id BIGSERIAL PRIMARY KEY,
    hesap_no VARCHAR(50) NOT NULL,
    islem_tip VARCHAR(20) NOT NULL,        -- INSERT, UPDATE, DELETE
    eski_bakiye NUMERIC(18,2),
    yeni_bakiye NUMERIC(18,2),
    bakiye_degisim NUMERIC(18,2),
    islem_aciklama TEXT,
    op_ad VARCHAR(100),
    ip_adres VARCHAR(50),
    kayit_tarih TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Index
    INDEX idx_hesap_degisiklik_hesap (hesap_no),
    INDEX idx_hesap_degisiklik_tarih (kayit_tarih DESC)
);

-- Transfer işlemleri audit
CREATE TABLE IF NOT EXISTS audit.transfer_audit (
    audit_id BIGSERIAL PRIMARY KEY,
    transfer_no VARCHAR(50) NOT NULL,
    gonderen_hesap_no VARCHAR(50) NOT NULL,
    alici_hesap_no VARCHAR(50) NOT NULL,
    tutar NUMERIC(18,2) NOT NULL,
    transfer_tip VARCHAR(20) NOT NULL,     -- VIRMAN, HAVALE, EFT
    durum VARCHAR(20) NOT NULL,            -- BASARILI, BASARISIZ, BEKLEMEDE
    hata_mesaj TEXT,
    ip_adres VARCHAR(50),
    kayit_tarih TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Index
    INDEX idx_transfer_audit_transfer_no (transfer_no),
    INDEX idx_transfer_audit_gonderen (gonderen_hesap_no),
    INDEX idx_transfer_audit_tarih (kayit_tarih DESC)
);

-- =====================================================
-- FONKSIYONLAR
-- =====================================================

-- Günlük özet güncelleme fonksiyonu
CREATE OR REPLACE FUNCTION analitik.gunluk_ozet_guncelle(p_tarih DATE)
RETURNS void AS $$
BEGIN
    INSERT INTO analitik.gunluk_ozet (
        tarih,
        toplam_islem_sayisi,
        para_yatirma_sayisi,
        para_cekme_sayisi
    )
    SELECT 
        p_tarih,
        COUNT(*),
        COUNT(*) FILTER (WHERE islem_tip = 'PARA_YATIR'),
        COUNT(*) FILTER (WHERE islem_tip = 'PARA_CEK')
    FROM log.sistem_log
    WHERE DATE(kayit_tarih) = p_tarih
    ON CONFLICT (tarih) DO UPDATE SET
        toplam_islem_sayisi = EXCLUDED.toplam_islem_sayisi,
        para_yatirma_sayisi = EXCLUDED.para_yatirma_sayisi,
        para_cekme_sayisi = EXCLUDED.para_cekme_sayisi,
        kayit_tarih = CURRENT_TIMESTAMP;
END;
$$ LANGUAGE plpgsql;

-- =====================================================
-- PARTITIONING (Log tabloları için)
-- =====================================================

-- sistem_log tablosu için aylık partition
-- CREATE TABLE log.sistem_log_y2025m11 PARTITION OF log.sistem_log
--     FOR VALUES FROM ('2025-11-01') TO ('2025-12-01');

-- =====================================================
-- COMMENT'LER
-- =====================================================

COMMENT ON SCHEMA log IS 'Log kayıtları şeması';
COMMENT ON SCHEMA analitik IS 'Analitik ve raporlama şeması';
COMMENT ON SCHEMA audit IS 'Audit ve izleme kayıtları';

COMMENT ON TABLE log.sistem_log IS 'Tüm sistem işlemlerinin log kayıtları';
COMMENT ON TABLE log.hata_log IS 'Hata ve exception kayıtları';
COMMENT ON TABLE log.api_log IS 'API çağrı kayıtları';
COMMENT ON TABLE log.giris_log IS 'Kullanıcı giriş/çıkış kayıtları';

COMMENT ON TABLE analitik.musteri_ozet IS 'Müşteri analitik özet tablosu';
COMMENT ON TABLE analitik.gunluk_ozet IS 'Günlük işlem özet tablosu';
COMMENT ON TABLE analitik.hesap_islem_ozet IS 'Hesap bazlı işlem özeti';

-- =====================================================
-- GRANTS (Yetkiler)
-- =====================================================

-- Log yazma yetkisi (application user için)
GRANT INSERT ON ALL TABLES IN SCHEMA log TO metinbank_app;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA log TO metinbank_app;

-- Analitik okuma yetkisi (reporting user için)
GRANT SELECT ON ALL TABLES IN SCHEMA analitik TO metinbank_report;

-- Audit okuma yetkisi (audit user için)
GRANT SELECT ON ALL TABLES IN SCHEMA audit TO metinbank_audit;

COMMIT;

/*
 * Standart Notlar:
 * 1. PostgreSQL log veritabanı Oracle'dan ayrı
 * 2. Tablo isimleri: lowercase, _ ile ayrılmış (sistem_log, hata_log)
 * 3. Schema yapısı: log, analitik, audit
 * 4. BIGSERIAL: Auto-increment primary key
 * 5. JSONB: JSON veriler için
 * 6. Partition: Büyük tablolar için aylık partition
 * 7. Index'ler: Query performance için
 * 8. Comment: Dokümantasyon için
 */


