/*
 * MetinBank - Oracle Sequences and Triggers
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Sequence ve trigger tanımları
 */

-- =====================================================
-- SEQUENCES
-- =====================================================

-- Müşteri sequence
CREATE SEQUENCE SEQ_MUSTERI
    START WITH 100001
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

-- Hesap sequence
CREATE SEQUENCE SEQ_HESAP
    START WITH 1
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

-- Kart sequence
CREATE SEQUENCE SEQ_KART
    START WITH 5000000000000001
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

-- Kredi sequence
CREATE SEQUENCE SEQ_KREDI
    START WITH 1001
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

-- Transfer sequence
CREATE SEQUENCE SEQ_TRANSFER
    START WITH 1
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

-- Log sequence
CREATE SEQUENCE SEQ_LOG
    START WITH 1
    INCREMENT BY 1
    CACHE 100
    NOCYCLE;

-- =====================================================
-- TRIGGERS
-- =====================================================

-- Müşteri kayıt tarihi trigger
CREATE OR REPLACE TRIGGER TRG_MUSTERI_INSERT
BEFORE INSERT ON musteriler
FOR EACH ROW
BEGIN
    IF :NEW.kayit_tarih IS NULL THEN
        :NEW.kayit_tarih := SYSDATE;
    END IF;
    
    IF :NEW.aktif IS NULL THEN
        :NEW.aktif := 1;
    END IF;
END;
/

-- Müşteri güncelleme tarihi trigger
CREATE OR REPLACE TRIGGER TRG_MUSTERI_UPDATE
BEFORE UPDATE ON musteriler
FOR EACH ROW
BEGIN
    :NEW.guncelleme_tarih := SYSDATE;
END;
/

-- Hesap kayıt tarihi trigger
CREATE OR REPLACE TRIGGER TRG_HESAP_INSERT
BEFORE INSERT ON hesaplar
FOR EACH ROW
BEGIN
    IF :NEW.acilis_tarih IS NULL THEN
        :NEW.acilis_tarih := SYSDATE;
    END IF;
    
    IF :NEW.kullanilabilir_bakiye IS NULL THEN
        :NEW.kullanilabilir_bakiye := :NEW.bakiye;
    END IF;
    
    IF :NEW.aktif IS NULL THEN
        :NEW.aktif := 1;
    END IF;
END;
/

-- Hesap güncelleme tarihi trigger
CREATE OR REPLACE TRIGGER TRG_HESAP_UPDATE
BEFORE UPDATE ON hesaplar
FOR EACH ROW
BEGIN
    :NEW.guncelleme_tarih := SYSDATE;
END;
/

-- Kart kayıt tarihi trigger
CREATE OR REPLACE TRIGGER TRG_KART_INSERT
BEFORE INSERT ON kartlar
FOR EACH ROW
BEGIN
    IF :NEW.kayit_tarih IS NULL THEN
        :NEW.kayit_tarih := SYSDATE;
    END IF;
    
    IF :NEW.aktif IS NULL THEN
        :NEW.aktif := 1;
    END IF;
    
    -- Varsayılan limitler
    IF :NEW.gunluk_limit IS NULL THEN
        :NEW.gunluk_limit := 5000;
    END IF;
    
    IF :NEW.aylik_limit IS NULL THEN
        :NEW.aylik_limit := 50000;
    END IF;
END;
/

-- Kredi kayıt tarihi trigger
CREATE OR REPLACE TRIGGER TRG_KREDI_INSERT
BEFORE INSERT ON krediler
FOR EACH ROW
BEGIN
    IF :NEW.basvuru_tarih IS NULL THEN
        :NEW.basvuru_tarih := SYSDATE;
    END IF;
    
    IF :NEW.kalan_borc IS NULL THEN
        :NEW.kalan_borc := :NEW.kredi_tutar;
    END IF;
    
    IF :NEW.kalan_taksit IS NULL THEN
        :NEW.kalan_taksit := :NEW.vade;
    END IF;
    
    IF :NEW.aktif IS NULL THEN
        :NEW.aktif := 1;
    END IF;
END;
/

-- Transfer kayıt tarihi trigger
CREATE OR REPLACE TRIGGER TRG_TRANSFER_INSERT
BEFORE INSERT ON transferler
FOR EACH ROW
BEGIN
    IF :NEW.islem_tarih IS NULL THEN
        :NEW.islem_tarih := SYSDATE;
    END IF;
    
    IF :NEW.komisyon IS NULL THEN
        -- Komisyon hesaplama (basitleştirilmiş)
        CASE :NEW.transfer_tip
            WHEN 1 THEN :NEW.komisyon := 0;      -- Virman ücretsiz
            WHEN 2 THEN :NEW.komisyon := 1;      -- Havale 1 TL
            WHEN 3 THEN :NEW.komisyon := 5;      -- EFT 5 TL
            WHEN 4 THEN :NEW.komisyon := 2;      -- FAST 2 TL
            WHEN 5 THEN :NEW.komisyon := 50;     -- SWIFT 50 TL
            ELSE :NEW.komisyon := 0;
        END CASE;
    END IF;
    
    IF :NEW.aktif IS NULL THEN
        :NEW.aktif := 1;
    END IF;
END;
/

-- =====================================================
-- VIEWS (Raporlama için)
-- =====================================================

-- Müşteri özet view
CREATE OR REPLACE VIEW V_MUSTERI_OZET AS
SELECT 
    m.musteri_no,
    m.tc_kimlik_no,
    m.ad || ' ' || m.soyad AS ad_soyad,
    m.eposta,
    m.telefon,
    m.musteri_tip,
    m.durum,
    COUNT(DISTINCT h.hesap_no) AS hesap_sayisi,
    NVL(SUM(h.bakiye), 0) AS toplam_bakiye,
    COUNT(DISTINCT k.kart_no) AS kart_sayisi,
    COUNT(DISTINCT kr.kredi_no) AS kredi_sayisi,
    m.kayit_tarih
FROM musteriler m
LEFT JOIN hesaplar h ON m.musteri_no = h.musteri_no AND h.aktif = 1
LEFT JOIN kartlar k ON m.musteri_no = k.musteri_no AND k.aktif = 1
LEFT JOIN krediler kr ON m.musteri_no = kr.musteri_no AND kr.aktif = 1
WHERE m.aktif = 1
GROUP BY 
    m.musteri_no,
    m.tc_kimlik_no,
    m.ad,
    m.soyad,
    m.eposta,
    m.telefon,
    m.musteri_tip,
    m.durum,
    m.kayit_tarih;

-- Hesap özet view
CREATE OR REPLACE VIEW V_HESAP_OZET AS
SELECT 
    h.hesap_no,
    h.musteri_no,
    m.ad || ' ' || m.soyad AS musteri_ad,
    h.hesap_tip,
    CASE h.hesap_tip
        WHEN 1 THEN 'Vadesiz'
        WHEN 2 THEN 'Vadeli'
        WHEN 3 THEN 'Döviz'
        WHEN 4 THEN 'KMH'
        WHEN 5 THEN 'Yatırım'
    END AS hesap_tip_ad,
    h.doviz_kod,
    h.bakiye,
    h.kullanilabilir_bakiye,
    h.acilis_tarih,
    h.durum
FROM hesaplar h
INNER JOIN musteriler m ON h.musteri_no = m.musteri_no
WHERE h.aktif = 1;

-- Transfer özet view
CREATE OR REPLACE VIEW V_TRANSFER_OZET AS
SELECT 
    t.transfer_no,
    t.gonderen_hesap_no,
    m1.ad || ' ' || m1.soyad AS gonderen_ad,
    t.alici_hesap_no,
    t.alici_ad,
    t.transfer_tip,
    CASE t.transfer_tip
        WHEN 1 THEN 'Virman'
        WHEN 2 THEN 'Havale'
        WHEN 3 THEN 'EFT'
        WHEN 4 THEN 'FAST'
        WHEN 5 THEN 'SWIFT'
    END AS transfer_tip_ad,
    t.tutar,
    t.komisyon,
    t.tutar + t.komisyon AS toplam_tutar,
    t.durum,
    CASE t.durum
        WHEN 1 THEN 'Beklemede'
        WHEN 2 THEN 'Başarılı'
        WHEN 3 THEN 'Başarısız'
        WHEN 4 THEN 'İptal'
        WHEN 5 THEN 'İade'
    END AS durum_ad,
    t.islem_tarih,
    t.gerceklesen_tarih
FROM transferler t
INNER JOIN hesaplar h1 ON t.gonderen_hesap_no = h1.hesap_no
INNER JOIN musteriler m1 ON h1.musteri_no = m1.musteri_no
WHERE t.aktif = 1;

COMMIT;

/*
 * Standart Notlar:
 * 1. Sequence isimleri: SEQ_ prefix'i ile
 * 2. Trigger isimleri: TRG_ prefix'i ile + tablo adı + işlem tipi
 * 3. View isimleri: V_ prefix'i ile
 * 4. Trigger'lar BEFORE INSERT/UPDATE olarak tanımlanır
 * 5. Varsayılan değerler trigger'da set edilir
 * 6. :NEW ve :OLD ile row değerlerine erişilir
 * 7. View'lar raporlama için LEFT JOIN kullanır
 */


