/*
 * MetinBank - Oracle Package Definitions
 * Created by: Metin Melikşah Dermencioğlu, 04/11/2025
 * Oracle stored procedure paketleri
 * Standart: Package isimleri database'de ne ise aynı (PKG_MUSTERI, PKG_HESAP)
 */

-- =====================================================
-- PACKAGE: PKG_MUSTERI (Müşteri İşlemleri)
-- =====================================================

CREATE OR REPLACE PACKAGE PKG_MUSTERI AS
    /*
     * Müşteri paket tanımı
     * SP Object katmanından çağrılacak prosedürler
     */
    
    -- Müşteri ekleme
    PROCEDURE P_MUSTERI_EKLE(
        p_tc_kimlik_no IN VARCHAR2,
        p_ad IN VARCHAR2,
        p_soyad IN VARCHAR2,
        p_eposta IN VARCHAR2,
        p_telefon IN VARCHAR2,
        p_musteri_no OUT NUMBER
    );
    
    -- Müşteri güncelleme
    PROCEDURE P_MUSTERI_GUNCELLE(
        p_musteri_no IN NUMBER,
        p_ad IN VARCHAR2,
        p_soyad IN VARCHAR2,
        p_eposta IN VARCHAR2,
        p_telefon IN VARCHAR2,
        p_sonuc OUT VARCHAR2
    );
    
    -- Müşteri silme (soft delete)
    PROCEDURE P_MUSTERI_SIL(
        p_musteri_no IN NUMBER,
        p_sonuc OUT VARCHAR2
    );
    
    -- Müşteri toplam bakiye (Standart: database ile aynı isim)
    FUNCTION get_bakiye(
        p_musteri_no IN NUMBER
    ) RETURN NUMBER;
    
END PKG_MUSTERI;
/

-- =====================================================
-- PACKAGE: PKG_HESAP (Hesap İşlemleri)
-- =====================================================

CREATE OR REPLACE PACKAGE PKG_HESAP AS
    /*
     * Hesap paket tanımı
     * SP Object katmanından çağrılacak prosedürler
     */
    
    -- Hesap açma
    PROCEDURE P_HESAP_AC(
        p_musteri_no IN NUMBER,
        p_hesap_no IN VARCHAR2,
        p_hesap_tip IN NUMBER,
        p_doviz_kod IN NUMBER,
        p_ilk_yatirim IN NUMBER DEFAULT 0,
        p_sonuc OUT VARCHAR2
    );
    
    -- Para yatırma
    PROCEDURE P_PARA_YATIR(
        p_hesap_no IN VARCHAR2,
        p_tutar IN NUMBER,
        p_yeni_bakiye OUT NUMBER
    );
    
    -- Para çekme
    PROCEDURE P_PARA_CEK(
        p_hesap_no IN VARCHAR2,
        p_tutar IN NUMBER,
        p_yeni_bakiye OUT NUMBER
    );
    
    -- Hesap bakiye (Standart: database ile aynı isim)
    FUNCTION get_bakiye(
        p_hesap_no IN VARCHAR2
    ) RETURN NUMBER;
    
    -- Hesap kapatma
    PROCEDURE P_HESAP_KAPAT(
        p_hesap_no IN VARCHAR2,
        p_sonuc OUT VARCHAR2
    );
    
END PKG_HESAP;
/

-- =====================================================
-- PACKAGE: PKG_KART (Kart İşlemleri)
-- =====================================================

CREATE OR REPLACE PACKAGE PKG_KART AS
    /*
     * Kart paket tanımı
     * Banka kartı ve kredi kartı işlemleri
     */
    
    -- Kart oluşturma
    PROCEDURE P_KART_OLUSTUR(
        p_musteri_no IN NUMBER,
        p_hesap_no IN VARCHAR2,
        p_kart_tip IN NUMBER,
        p_kart_no OUT VARCHAR2
    );
    
    -- Kart bloke etme
    PROCEDURE P_KART_BLOKE(
        p_kart_no IN VARCHAR2,
        p_bloke_sebep IN VARCHAR2,
        p_sonuc OUT VARCHAR2
    );
    
    -- Kart limit güncelleme
    PROCEDURE P_KART_LIMIT_GUNCELLE(
        p_kart_no IN VARCHAR2,
        p_gunluk_limit IN NUMBER,
        p_aylik_limit IN NUMBER,
        p_sonuc OUT VARCHAR2
    );
    
    -- Kredi kartı borcu
    FUNCTION get_kredi_kart_borc(
        p_kart_no IN VARCHAR2
    ) RETURN NUMBER;
    
END PKG_KART;
/

-- =====================================================
-- PACKAGE: PKG_KREDI (Kredi İşlemleri)
-- =====================================================

CREATE OR REPLACE PACKAGE PKG_KREDI AS
    /*
     * Kredi paket tanımı
     * Kredi başvuru, kullandırma ve ödeme işlemleri
     */
    
    -- Kredi başvurusu
    PROCEDURE P_KREDI_BASVURU(
        p_musteri_no IN NUMBER,
        p_kredi_tip IN NUMBER,
        p_kredi_tutar IN NUMBER,
        p_vade IN NUMBER,
        p_kredi_no OUT VARCHAR2
    );
    
    -- Kredi onaylama
    PROCEDURE P_KREDI_ONAYLA(
        p_kredi_no IN VARCHAR2,
        p_faiz_oran IN NUMBER,
        p_sonuc OUT VARCHAR2
    );
    
    -- Kredi kullandırma
    PROCEDURE P_KREDI_KULLANDIR(
        p_kredi_no IN VARCHAR2,
        p_hesap_no IN VARCHAR2,
        p_sonuc OUT VARCHAR2
    );
    
    -- Taksit ödeme
    PROCEDURE P_TAKSIT_ODE(
        p_kredi_no IN VARCHAR2,
        p_odeme_tutar IN NUMBER,
        p_sonuc OUT VARCHAR2
    );
    
    -- Kalan borç
    FUNCTION get_kalan_borc(
        p_kredi_no IN VARCHAR2
    ) RETURN NUMBER;
    
END PKG_KREDI;
/

-- =====================================================
-- PACKAGE: PKG_TRANSFER (Transfer İşlemleri)
-- =====================================================

CREATE OR REPLACE PACKAGE PKG_TRANSFER AS
    /*
     * Transfer paket tanımı
     * Havale, EFT, Virman işlemleri
     */
    
    -- Virman (Kendi hesaplar arası)
    PROCEDURE P_VIRMAN(
        p_gonderen_hesap_no IN VARCHAR2,
        p_alici_hesap_no IN VARCHAR2,
        p_tutar IN NUMBER,
        p_aciklama IN VARCHAR2,
        p_transfer_no OUT VARCHAR2
    );
    
    -- Havale (Aynı banka)
    PROCEDURE P_HAVALE(
        p_gonderen_hesap_no IN VARCHAR2,
        p_alici_hesap_no IN VARCHAR2,
        p_alici_ad IN VARCHAR2,
        p_tutar IN NUMBER,
        p_aciklama IN VARCHAR2,
        p_transfer_no OUT VARCHAR2
    );
    
    -- EFT (Farklı banka)
    PROCEDURE P_EFT(
        p_gonderen_hesap_no IN VARCHAR2,
        p_alici_hesap_no IN VARCHAR2,
        p_alici_banka_kod IN VARCHAR2,
        p_alici_ad IN VARCHAR2,
        p_tutar IN NUMBER,
        p_aciklama IN VARCHAR2,
        p_transfer_no OUT VARCHAR2
    );
    
    -- Transfer durumu güncelleme
    PROCEDURE P_TRANSFER_DURUM_GUNCELLE(
        p_transfer_no IN VARCHAR2,
        p_durum IN NUMBER,
        p_hata_mesaj IN VARCHAR2 DEFAULT NULL
    );
    
END PKG_TRANSFER;
/

-- =====================================================
-- PACKAGE: PKG_LOG (Log İşlemleri)
-- =====================================================

CREATE OR REPLACE PACKAGE PKG_LOG AS
    /*
     * Log paket tanımı
     * Sistem log kayıtları
     */
    
    -- Log ekleme
    PROCEDURE P_LOG_EKLE(
        p_islem_tip IN VARCHAR2,
        p_islem_detay IN VARCHAR2,
        p_op_ad IN VARCHAR2,
        p_ip_adres IN VARCHAR2 DEFAULT NULL
    );
    
    -- Hata log
    PROCEDURE P_HATA_LOG(
        p_hata_mesaj IN VARCHAR2,
        p_sp_ad IN VARCHAR2,
        p_op_ad IN VARCHAR2
    );
    
END PKG_LOG;
/

COMMIT;

/*
 * Standart Notlar:
 * 1. Package isimleri: PKG_ prefix'i ile (PKG_MUSTERI, PKG_HESAP)
 * 2. Procedure isimleri: P_ prefix'i ile (P_MUSTERI_EKLE, P_HESAP_AC)
 * 3. Function isimleri: database'de ne ise aynı (get_bakiye, get_kalan_borc)
 * 4. Parametre isimleri: p_ prefix'i ile (p_musteri_no, p_hesap_no)
 * 5. OUT parametreler: sonuç döndürmek için kullanılır
 * 6. .NET tarafında: SpMusteri.MusteriEkle() -> PKG_MUSTERI.P_MUSTERI_EKLE
 * 7. .NET tarafında: SpMusteri.get_bakiye() -> PKG_MUSTERI.GET_BAKIYE
 */


