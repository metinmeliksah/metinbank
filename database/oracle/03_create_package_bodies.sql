/*
 * MetinBank - Oracle Package Bodies (Implementation)
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Oracle stored procedure implementasyonları
 * Standart: Package body isimleri package ile aynı
 */

-- =====================================================
-- PACKAGE BODY: PKG_MUSTERI
-- =====================================================

CREATE OR REPLACE PACKAGE BODY PKG_MUSTERI AS

    -- Müşteri ekleme
    PROCEDURE P_MUSTERI_EKLE(
        p_tc_kimlik_no IN VARCHAR2,
        p_ad IN VARCHAR2,
        p_soyad IN VARCHAR2,
        p_eposta IN VARCHAR2,
        p_telefon IN VARCHAR2,
        p_musteri_no OUT NUMBER
    ) AS
        v_musteri_no NUMBER;
        v_musteri_tip NUMBER := 1; -- Bireysel
    BEGIN
        -- Sequence'dan müşteri no al
        SELECT SEQ_MUSTERI.NEXTVAL INTO v_musteri_no FROM DUAL;
        
        -- Müşteri ekle
        INSERT INTO musteriler (
            musteri_no,
            tc_kimlik_no,
            ad,
            soyad,
            eposta,
            telefon,
            musteri_tip,
            durum,
            kayit_tarih,
            aktif
        ) VALUES (
            v_musteri_no,
            p_tc_kimlik_no,
            p_ad,
            p_soyad,
            p_eposta,
            p_telefon,
            v_musteri_tip,
            1, -- Aktif
            SYSDATE,
            1
        );
        
        p_musteri_no := v_musteri_no;
        
        -- Log ekle
        PKG_LOG.P_LOG_EKLE('MUSTERI_EKLE', 'Musteri No: ' || v_musteri_no, 'SYSTEM');
        
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_MUSTERI.P_MUSTERI_EKLE', 'SYSTEM');
            RAISE;
    END P_MUSTERI_EKLE;

    -- Müşteri güncelleme
    PROCEDURE P_MUSTERI_GUNCELLE(
        p_musteri_no IN NUMBER,
        p_ad IN VARCHAR2,
        p_soyad IN VARCHAR2,
        p_eposta IN VARCHAR2,
        p_telefon IN VARCHAR2,
        p_sonuc OUT VARCHAR2
    ) AS
    BEGIN
        UPDATE musteriler
        SET ad = p_ad,
            soyad = p_soyad,
            eposta = p_eposta,
            telefon = p_telefon,
            guncelleme_tarih = SYSDATE
        WHERE musteri_no = p_musteri_no
          AND aktif = 1;
        
        IF SQL%ROWCOUNT = 0 THEN
            p_sonuc := 'Müşteri bulunamadı';
        ELSE
            p_sonuc := NULL; -- Başarılı
            PKG_LOG.P_LOG_EKLE('MUSTERI_GUNCELLE', 'Musteri No: ' || p_musteri_no, 'SYSTEM');
            COMMIT;
        END IF;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            p_sonuc := 'Hata: ' || SQLERRM;
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_MUSTERI.P_MUSTERI_GUNCELLE', 'SYSTEM');
    END P_MUSTERI_GUNCELLE;

    -- Müşteri silme (soft delete)
    PROCEDURE P_MUSTERI_SIL(
        p_musteri_no IN NUMBER,
        p_sonuc OUT VARCHAR2
    ) AS
    BEGIN
        UPDATE musteriler
        SET aktif = 0,
            guncelleme_tarih = SYSDATE
        WHERE musteri_no = p_musteri_no;
        
        IF SQL%ROWCOUNT = 0 THEN
            p_sonuc := 'Müşteri bulunamadı';
        ELSE
            p_sonuc := NULL; -- Başarılı
            PKG_LOG.P_LOG_EKLE('MUSTERI_SIL', 'Musteri No: ' || p_musteri_no, 'SYSTEM');
            COMMIT;
        END IF;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            p_sonuc := 'Hata: ' || SQLERRM;
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_MUSTERI.P_MUSTERI_SIL', 'SYSTEM');
    END P_MUSTERI_SIL;

    -- Müşteri toplam bakiye
    FUNCTION get_bakiye(
        p_musteri_no IN NUMBER
    ) RETURN NUMBER AS
        v_toplam_bakiye NUMBER := 0;
    BEGIN
        SELECT NVL(SUM(bakiye), 0)
        INTO v_toplam_bakiye
        FROM hesaplar
        WHERE musteri_no = p_musteri_no
          AND aktif = 1
          AND durum = 1; -- Aktif hesaplar
        
        RETURN v_toplam_bakiye;
    EXCEPTION
        WHEN OTHERS THEN
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_MUSTERI.GET_BAKIYE', 'SYSTEM');
            RETURN 0;
    END get_bakiye;

END PKG_MUSTERI;
/

-- =====================================================
-- PACKAGE BODY: PKG_HESAP
-- =====================================================

CREATE OR REPLACE PACKAGE BODY PKG_HESAP AS

    -- Hesap açma
    PROCEDURE P_HESAP_AC(
        p_musteri_no IN NUMBER,
        p_hesap_no IN VARCHAR2,
        p_hesap_tip IN NUMBER,
        p_doviz_kod IN NUMBER,
        p_ilk_yatirim IN NUMBER DEFAULT 0,
        p_sonuc OUT VARCHAR2
    ) AS
        v_hesap_id RAW(16);
    BEGIN
        -- UUID oluştur
        v_hesap_id := SYS_GUID();
        
        -- Hesap aç
        INSERT INTO hesaplar (
            hesap_id,
            musteri_no,
            hesap_no,
            hesap_tip,
            doviz_kod,
            bakiye,
            kullanilabilir_bakiye,
            acilis_tarih,
            durum,
            aktif
        ) VALUES (
            v_hesap_id,
            p_musteri_no,
            p_hesap_no,
            p_hesap_tip,
            p_doviz_kod,
            p_ilk_yatirim,
            p_ilk_yatirim,
            SYSDATE,
            1, -- Aktif
            1
        );
        
        p_sonuc := NULL; -- Başarılı
        PKG_LOG.P_LOG_EKLE('HESAP_AC', 'Hesap No: ' || p_hesap_no, 'SYSTEM');
        
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            p_sonuc := 'Hata: ' || SQLERRM;
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_HESAP.P_HESAP_AC', 'SYSTEM');
    END P_HESAP_AC;

    -- Para yatırma
    PROCEDURE P_PARA_YATIR(
        p_hesap_no IN VARCHAR2,
        p_tutar IN NUMBER,
        p_yeni_bakiye OUT NUMBER
    ) AS
    BEGIN
        UPDATE hesaplar
        SET bakiye = bakiye + p_tutar,
            kullanilabilir_bakiye = kullanilabilir_bakiye + p_tutar,
            guncelleme_tarih = SYSDATE
        WHERE hesap_no = p_hesap_no
          AND aktif = 1
        RETURNING bakiye INTO p_yeni_bakiye;
        
        PKG_LOG.P_LOG_EKLE('PARA_YATIR', 'Hesap: ' || p_hesap_no || ', Tutar: ' || p_tutar, 'SYSTEM');
        
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_HESAP.P_PARA_YATIR', 'SYSTEM');
            RAISE;
    END P_PARA_YATIR;

    -- Para çekme
    PROCEDURE P_PARA_CEK(
        p_hesap_no IN VARCHAR2,
        p_tutar IN NUMBER,
        p_yeni_bakiye OUT NUMBER
    ) AS
        v_bakiye NUMBER;
    BEGIN
        -- Bakiye kontrolü
        SELECT bakiye INTO v_bakiye
        FROM hesaplar
        WHERE hesap_no = p_hesap_no
          AND aktif = 1
        FOR UPDATE;
        
        IF v_bakiye < p_tutar THEN
            RAISE_APPLICATION_ERROR(-20001, 'Yetersiz bakiye');
        END IF;
        
        -- Para çek
        UPDATE hesaplar
        SET bakiye = bakiye - p_tutar,
            kullanilabilir_bakiye = kullanilabilir_bakiye - p_tutar,
            guncelleme_tarih = SYSDATE
        WHERE hesap_no = p_hesap_no
        RETURNING bakiye INTO p_yeni_bakiye;
        
        PKG_LOG.P_LOG_EKLE('PARA_CEK', 'Hesap: ' || p_hesap_no || ', Tutar: ' || p_tutar, 'SYSTEM');
        
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_HESAP.P_PARA_CEK', 'SYSTEM');
            RAISE;
    END P_PARA_CEK;

    -- Hesap bakiye
    FUNCTION get_bakiye(
        p_hesap_no IN VARCHAR2
    ) RETURN NUMBER AS
        v_bakiye NUMBER := 0;
    BEGIN
        SELECT NVL(bakiye, 0)
        INTO v_bakiye
        FROM hesaplar
        WHERE hesap_no = p_hesap_no
          AND aktif = 1;
        
        RETURN v_bakiye;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RETURN 0;
        WHEN OTHERS THEN
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_HESAP.GET_BAKIYE', 'SYSTEM');
            RETURN 0;
    END get_bakiye;

    -- Hesap kapatma
    PROCEDURE P_HESAP_KAPAT(
        p_hesap_no IN VARCHAR2,
        p_sonuc OUT VARCHAR2
    ) AS
        v_bakiye NUMBER;
    BEGIN
        -- Bakiye kontrolü
        SELECT bakiye INTO v_bakiye
        FROM hesaplar
        WHERE hesap_no = p_hesap_no
          AND aktif = 1;
        
        IF v_bakiye > 0 THEN
            p_sonuc := 'Hesap bakiyesi sıfırlanmalı';
            RETURN;
        END IF;
        
        -- Hesap kapat (soft delete)
        UPDATE hesaplar
        SET aktif = 0,
            durum = 2, -- Kapandı
            guncelleme_tarih = SYSDATE
        WHERE hesap_no = p_hesap_no;
        
        p_sonuc := NULL; -- Başarılı
        PKG_LOG.P_LOG_EKLE('HESAP_KAPAT', 'Hesap No: ' || p_hesap_no, 'SYSTEM');
        
        COMMIT;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            p_sonuc := 'Hesap bulunamadı';
        WHEN OTHERS THEN
            ROLLBACK;
            p_sonuc := 'Hata: ' || SQLERRM;
            PKG_LOG.P_HATA_LOG(SQLERRM, 'PKG_HESAP.P_HESAP_KAPAT', 'SYSTEM');
    END P_HESAP_KAPAT;

END PKG_HESAP;
/

-- =====================================================
-- PACKAGE BODY: PKG_LOG
-- =====================================================

CREATE OR REPLACE PACKAGE BODY PKG_LOG AS

    -- Log ekleme
    PROCEDURE P_LOG_EKLE(
        p_islem_tip IN VARCHAR2,
        p_islem_detay IN VARCHAR2,
        p_op_ad IN VARCHAR2,
        p_ip_adres IN VARCHAR2 DEFAULT NULL
    ) AS
        PRAGMA AUTONOMOUS_TRANSACTION;
    BEGIN
        INSERT INTO sistem_log (
            log_id,
            islem_tip,
            islem_detay,
            op_ad,
            ip_adres,
            kayit_tarih
        ) VALUES (
            SEQ_LOG.NEXTVAL,
            p_islem_tip,
            p_islem_detay,
            p_op_ad,
            p_ip_adres,
            SYSDATE
        );
        
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
    END P_LOG_EKLE;

    -- Hata log
    PROCEDURE P_HATA_LOG(
        p_hata_mesaj IN VARCHAR2,
        p_sp_ad IN VARCHAR2,
        p_op_ad IN VARCHAR2
    ) AS
        PRAGMA AUTONOMOUS_TRANSACTION;
    BEGIN
        INSERT INTO hata_log (
            log_id,
            hata_mesaj,
            sp_ad,
            op_ad,
            kayit_tarih
        ) VALUES (
            SEQ_LOG.NEXTVAL,
            p_hata_mesaj,
            p_sp_ad,
            p_op_ad,
            SYSDATE
        );
        
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
    END P_HATA_LOG;

END PKG_LOG;
/

COMMIT;

/*
 * Standart Notlar:
 * 1. Her prosedürde exception handling yapılmalı
 * 2. COMMIT/ROLLBACK işlemleri prosedür içinde yapılır
 * 3. Log kayıtları autonomous transaction ile yazılır
 * 4. FOR UPDATE kullanarak row-level lock alınır (bakiye güncelleme)
 * 5. RETURNING INTO ile güncellenen değer alınır
 * 6. RAISE_APPLICATION_ERROR ile custom hata fırlatılır
 * 7. NO_DATA_FOUND exception'ı ayrı handle edilir
 */


