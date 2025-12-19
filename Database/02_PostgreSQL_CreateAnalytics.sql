-- =========================================
-- MetinBank - PostgreSQL Veritabanı Script
-- Aşama 1: Analytics için temel yapı
-- =========================================

-- Veritabanı oluştur
-- CREATE DATABASE metinbank_analytics;

-- Bağlantıyı değiştir
-- \c metinbank_analytics;

-- =========================================
-- TABLOLAR (Analytics için)
-- =========================================

-- 1. customer_analytics: Müşteri analitiği
CREATE TABLE IF NOT EXISTS customer_analytics (
    id SERIAL PRIMARY KEY,
    customer_id INT NOT NULL,
    total_accounts INT DEFAULT 0,
    total_balance DECIMAL(18,2) DEFAULT 0,
    total_transactions INT DEFAULT 0,
    last_transaction_date TIMESTAMP NULL,
    risk_score DECIMAL(5,2) DEFAULT 0,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 2. transaction_analytics: İşlem analitiği
CREATE TABLE IF NOT EXISTS transaction_analytics (
    id SERIAL PRIMARY KEY,
    branch_id INT NOT NULL,
    transaction_date DATE NOT NULL,
    transaction_type VARCHAR(50) NOT NULL,
    currency_code VARCHAR(3) NOT NULL,
    total_count INT DEFAULT 0,
    total_amount DECIMAL(18,2) DEFAULT 0,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 3. branch_performance: Şube performansı
CREATE TABLE IF NOT EXISTS branch_performance (
    id SERIAL PRIMARY KEY,
    branch_id INT NOT NULL,
    performance_date DATE NOT NULL,
    customer_count INT DEFAULT 0,
    account_count INT DEFAULT 0,
    total_deposits DECIMAL(18,2) DEFAULT 0,
    total_withdrawals DECIMAL(18,2) DEFAULT 0,
    net_balance DECIMAL(18,2) DEFAULT 0,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 4. currency_rates: Döviz kurları (AI için)
CREATE TABLE IF NOT EXISTS currency_rates (
    id SERIAL PRIMARY KEY,
    currency_code VARCHAR(3) NOT NULL,
    buy_rate DECIMAL(18,4) NOT NULL,
    sell_rate DECIMAL(18,4) NOT NULL,
    rate_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    source VARCHAR(50),
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 5. ai_predictions: AI tahminleri
CREATE TABLE IF NOT EXISTS ai_predictions (
    id SERIAL PRIMARY KEY,
    prediction_type VARCHAR(50) NOT NULL,
    entity_id INT NOT NULL,
    prediction_value DECIMAL(18,2),
    confidence_score DECIMAL(5,2),
    prediction_data JSONB,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- =========================================
-- İNDEKSLER
-- =========================================

CREATE INDEX IF NOT EXISTS idx_customer_analytics_customer_id ON customer_analytics(customer_id);
CREATE INDEX IF NOT EXISTS idx_transaction_analytics_branch_date ON transaction_analytics(branch_id, transaction_date);
CREATE INDEX IF NOT EXISTS idx_branch_performance_branch_date ON branch_performance(branch_id, performance_date);
CREATE INDEX IF NOT EXISTS idx_currency_rates_code_date ON currency_rates(currency_code, rate_date);
CREATE INDEX IF NOT EXISTS idx_ai_predictions_type_entity ON ai_predictions(prediction_type, entity_id);

-- =========================================
-- FONKSİYONLAR
-- =========================================

-- Müşteri risk skoru hesaplama fonksiyonu
CREATE OR REPLACE FUNCTION calculate_customer_risk_score(
    p_customer_id INT,
    p_total_transactions INT,
    p_total_balance DECIMAL
)
RETURNS DECIMAL AS $$
DECLARE
    v_risk_score DECIMAL;
BEGIN
    -- Basit risk skoru hesaplama (AI entegrasyonunda geliştirilecek)
    v_risk_score := 0;
    
    IF p_total_transactions > 100 THEN
        v_risk_score := v_risk_score + 20;
    END IF;
    
    IF p_total_balance > 100000 THEN
        v_risk_score := v_risk_score + 30;
    END IF;
    
    IF p_total_balance < 0 THEN
        v_risk_score := v_risk_score + 50;
    END IF;
    
    RETURN v_risk_score;
END;
$$ LANGUAGE plpgsql;

-- =========================================
-- TEST VERİLERİ
-- =========================================

-- Döviz kuru test verileri
INSERT INTO currency_rates (currency_code, buy_rate, sell_rate, source)
VALUES 
    ('USD', 32.50, 32.55, 'TCMB'),
    ('EUR', 35.20, 35.25, 'TCMB'),
    ('GBP', 41.10, 41.15, 'TCMB'),
    ('CHF', 37.80, 37.85, 'TCMB'),
    ('JPY', 0.22, 0.23, 'TCMB');

-- Şube performans örnek verisi
INSERT INTO branch_performance (branch_id, performance_date, customer_count, account_count, total_deposits, total_withdrawals)
VALUES 
    (1, CURRENT_DATE, 150, 320, 5000000.00, 3500000.00),
    (2, CURRENT_DATE, 200, 450, 7500000.00, 5200000.00),
    (3, CURRENT_DATE, 120, 280, 4200000.00, 3100000.00);

-- =========================================
-- NOTLAR
-- =========================================

-- PostgreSQL bu veritabanında:
-- 1. Müşteri analitiği (customer_analytics)
-- 2. İşlem analitiği (transaction_analytics)
-- 3. Şube performans raporları (branch_performance)
-- 4. Döviz kurları (currency_rates)
-- 5. AI tahminleri (ai_predictions)
-- saklanacaktır.

-- MsSQL'den PostgreSQL'e veri senkronizasyonu 
-- için ETL süreçleri Aşama 4'te eklenecektir.

COMMENT ON TABLE customer_analytics IS 'Müşteri analitik verileri';
COMMENT ON TABLE transaction_analytics IS 'İşlem analitik verileri';
COMMENT ON TABLE branch_performance IS 'Şube performans verileri';
COMMENT ON TABLE currency_rates IS 'Döviz kuru verileri';
COMMENT ON TABLE ai_predictions IS 'AI tahmin sonuçları';

-- Başarılı mesajı
SELECT 'MetinBank Analytics veritabanı başarıyla oluşturuldu!' AS message;
