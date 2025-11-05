-- ============================================
-- MetinBank - Oracle Database Schema
-- Ana İşlemsel Veritabanı
-- Versiyon: 1.0
-- Tarih: 4 Kasım 2025
-- ============================================

-- Bağlan
CONNECT metinbank/YourSecurePassword123!@localhost:1521/XEPDB1

-- ============================================
-- CUSTOMERS (Müşteriler)
-- ============================================
CREATE TABLE customers (
    id RAW(16) DEFAULT SYS_GUID() PRIMARY KEY,
    customer_number VARCHAR2(20) UNIQUE NOT NULL,
    customer_type NUMBER(1) NOT NULL, -- 1:Retail, 2:Corporate
    status NUMBER(1) NOT NULL, -- 1:Active, 2:Passive, 3:Suspended, 4:PendingEKYC
    tc_kimlik_no VARCHAR2(500), -- Encrypted
    vergi_kimlik_no VARCHAR2(500), -- Encrypted
    first_name NVARCHAR2(100),
    last_name NVARCHAR2(100),
    company_name NVARCHAR2(200),
    email VARCHAR2(500) NOT NULL, -- Encrypted
    phone_number VARCHAR2(500) NOT NULL, -- Encrypted
    address NVARCHAR2(500),
    city NVARCHAR2(100),
    country VARCHAR2(2) DEFAULT 'TR',
    date_of_birth DATE,
    password_hash VARCHAR2(255) NOT NULL,
    last_login_at TIMESTAMP,
    registered_via_ekyc NUMBER(1) DEFAULT 0,
    ekyc_completed_at TIMESTAMP,
    is_active NUMBER(1) DEFAULT 1,
    created_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    updated_at TIMESTAMP,
    created_by VARCHAR2(100),
    updated_by VARCHAR2(100),
    CONSTRAINT chk_customer_type CHECK (customer_type IN (1, 2)),
    CONSTRAINT chk_customer_status CHECK (status IN (1, 2, 3, 4))
);

-- Index'ler
CREATE INDEX idx_customers_number ON customers(customer_number);
CREATE INDEX idx_customers_email ON customers(email);
CREATE INDEX idx_customers_type ON customers(customer_type);
CREATE INDEX idx_customers_status ON customers(status);

-- Sequence
CREATE SEQUENCE seq_customer_number START WITH 10000001 INCREMENT BY 1;

-- ============================================
-- CUSTOMER_ANALYTICS (Müşteri Analitikleri)
-- ============================================
CREATE TABLE customer_analytics (
    id RAW(16) DEFAULT SYS_GUID() PRIMARY KEY,
    customer_id RAW(16) NOT NULL,
    risk_profile VARCHAR2(50) DEFAULT 'Medium',
    risk_score NUMBER(5,2),
    predicted_income NUMBER(15,2),
    predicted_revenue NUMBER(15,2),
    spending_category VARCHAR2(100),
    average_monthly_spending NUMBER(15,2),
    last_analyzed_at TIMESTAMP,
    credit_eligibility_score NUMBER(5,2),
    is_active NUMBER(1) DEFAULT 1,
    created_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    updated_at TIMESTAMP,
    CONSTRAINT fk_analytics_customer FOREIGN KEY (customer_id) REFERENCES customers(id)
);

CREATE INDEX idx_analytics_customer ON customer_analytics(customer_id);

-- ============================================
-- AUTH_DEVICES (Cihaz Kayıtları)
-- ============================================
CREATE TABLE auth_devices (
    id RAW(16) DEFAULT SYS_GUID() PRIMARY KEY,
    user_id RAW(16) NOT NULL,
    device_fingerprint VARCHAR2(255) NOT NULL,
    device_name NVARCHAR2(100) NOT NULL,
    device_type VARCHAR2(50) NOT NULL,
    os VARCHAR2(100),
    browser VARCHAR2(100),
    ip_address VARCHAR2(45) NOT NULL,
    is_verified NUMBER(1) DEFAULT 0,
    last_used_at TIMESTAMP,
    fcm_token VARCHAR2(500),
    is_active NUMBER(1) DEFAULT 1,
    created_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    updated_at TIMESTAMP
);

CREATE INDEX idx_devices_user ON auth_devices(user_id);
CREATE INDEX idx_devices_fingerprint ON auth_devices(device_fingerprint);

-- ============================================
-- ACCOUNTS (Hesaplar)
-- ============================================
CREATE TABLE accounts (
    id RAW(16) DEFAULT SYS_GUID() PRIMARY KEY,
    account_number VARCHAR2(34) UNIQUE NOT NULL, -- IBAN
    customer_id RAW(16) NOT NULL,
    account_type NUMBER(1) NOT NULL, -- 1:DemandDeposit, 2:TimeDeposit, 3:ForeignCurrency, 4:Overdraft, 5:Investment
    currency NUMBER(1) NOT NULL, -- 1:TRY, 2:USD, 3:EUR, 4:GBP, 5:XAU, 6:XAG
    balance NUMBER(18,2) DEFAULT 0,
    available_balance NUMBER(18,2) DEFAULT 0,
    account_name NVARCHAR2(100),
    maturity_date DATE,
    interest_rate NUMBER(5,2),
    opened_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    closed_at TIMESTAMP,
    is_active NUMBER(1) DEFAULT 1,
    created_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    updated_at TIMESTAMP,
    created_by VARCHAR2(100),
    updated_by VARCHAR2(100),
    CONSTRAINT fk_account_customer FOREIGN KEY (customer_id) REFERENCES customers(id),
    CONSTRAINT chk_account_type CHECK (account_type IN (1, 2, 3, 4, 5)),
    CONSTRAINT chk_currency CHECK (currency IN (1, 2, 3, 4, 5, 6))
);

CREATE INDEX idx_accounts_customer ON accounts(customer_id);
CREATE INDEX idx_accounts_number ON accounts(account_number);
CREATE INDEX idx_accounts_type ON accounts(account_type);

-- Sequence
CREATE SEQUENCE seq_account_number START WITH 100000001 INCREMENT BY 1;

-- ============================================
-- ACCOUNT_OVERDRAFTS (KMH Bilgileri)
-- ============================================
CREATE TABLE account_overdrafts (
    id RAW(16) DEFAULT SYS_GUID() PRIMARY KEY,
    account_id RAW(16) NOT NULL,
    overdraft_limit NUMBER(15,2) NOT NULL,
    used_amount NUMBER(15,2) DEFAULT 0,
    interest_rate NUMBER(5,2) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE,
    is_active NUMBER(1) DEFAULT 1,
    created_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    updated_at TIMESTAMP,
    CONSTRAINT fk_overdraft_account FOREIGN KEY (account_id) REFERENCES accounts(id)
);

CREATE INDEX idx_overdrafts_account ON account_overdrafts(account_id);

-- ============================================
-- CARDS (Kartlar)
-- ============================================
CREATE TABLE cards (
    id RAW(16) DEFAULT SYS_GUID() PRIMARY KEY,
    card_token VARCHAR2(500) NOT NULL, -- Tokenized PAN
    last_four_digits VARCHAR2(4) NOT NULL,
    card_type NUMBER(1) NOT NULL, -- 1:Debit, 2:Credit, 3:Virtual, 4:Corporate
    status NUMBER(1) NOT NULL, -- 1:Active, 2:Inactive, 3:Blocked, 4:Lost, 5:Expired, 6:Cancelled
    customer_id RAW(16) NOT NULL,
    account_id RAW(16),
    card_holder_name NVARCHAR2(100) NOT NULL,
    expiry_date DATE NOT NULL,
    cvv_hash VARCHAR2(255) NOT NULL,
    pin_hash VARCHAR2(255) NOT NULL,
    daily_limit NUMBER(15,2) DEFAULT 10000,
    today_used NUMBER(15,2) DEFAULT 0,
    allow_ecommerce NUMBER(1) DEFAULT 1,
    allow_pos NUMBER(1) DEFAULT 1,
    allow_atm NUMBER(1) DEFAULT 1,
    allow_international NUMBER(1) DEFAULT 0,
    allow_contactless NUMBER(1) DEFAULT 1,
    is_active NUMBER(1) DEFAULT 1,
    created_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    updated_at TIMESTAMP,
    created_by VARCHAR2(100),
    updated_by VARCHAR2(100),
    CONSTRAINT fk_card_customer FOREIGN KEY (customer_id) REFERENCES customers(id),
    CONSTRAINT fk_card_account FOREIGN KEY (account_id) REFERENCES accounts(id),
    CONSTRAINT chk_card_type CHECK (card_type IN (1, 2, 3, 4)),
    CONSTRAINT chk_card_status CHECK (status IN (1, 2, 3, 4, 5, 6))
);

CREATE INDEX idx_cards_customer ON cards(customer_id);
CREATE INDEX idx_cards_account ON cards(account_id);
CREATE INDEX idx_cards_last_four ON cards(last_four_digits);

-- ============================================
-- CREDIT_CARD_INFO (Kredi Kartı Bilgileri)
-- ============================================
CREATE TABLE credit_card_info (
    id RAW(16) DEFAULT SYS_GUID() PRIMARY KEY,
    card_id RAW(16) NOT NULL,
    credit_limit NUMBER(15,2) NOT NULL,
    available_limit NUMBER(15,2) NOT NULL,
    current_debt NUMBER(15,2) DEFAULT 0,
    minimum_payment NUMBER(15,2) DEFAULT 0,
    statement_day NUMBER(2) NOT NULL,
    payment_due_day NUMBER(2) NOT NULL,
    interest_rate NUMBER(5,2) NOT NULL,
    late_payment_fee_rate NUMBER(5,2) NOT NULL,
    point_balance NUMBER(10,2) DEFAULT 0,
    last_statement_date DATE,
    next_statement_date DATE NOT NULL,
    is_active NUMBER(1) DEFAULT 1,
    created_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    updated_at TIMESTAMP,
    CONSTRAINT fk_creditinfo_card FOREIGN KEY (card_id) REFERENCES cards(id)
);

CREATE INDEX idx_creditinfo_card ON credit_card_info(card_id);

-- ============================================
-- CREDIT_CARD_STATEMENTS (Kredi Kartı Ekstreleri)
-- ============================================
CREATE TABLE credit_card_statements (
    id RAW(16) DEFAULT SYS_GUID() PRIMARY KEY,
    credit_card_info_id RAW(16) NOT NULL,
    period_start DATE NOT NULL,
    period_end DATE NOT NULL,
    total_debt NUMBER(15,2) NOT NULL,
    minimum_payment NUMBER(15,2) NOT NULL,
    due_date DATE NOT NULL,
    previous_balance NUMBER(15,2) DEFAULT 0,
    payments NUMBER(15,2) DEFAULT 0,
    new_transactions NUMBER(15,2) DEFAULT 0,
    interest_amount NUMBER(15,2) DEFAULT 0,
    points_earned NUMBER(10,2) DEFAULT 0,
    pdf_file_path VARCHAR2(500),
    is_paid NUMBER(1) DEFAULT 0,
    paid_at TIMESTAMP,
    is_active NUMBER(1) DEFAULT 1,
    created_at TIMESTAMP DEFAULT SYSTIMESTAMP,
    updated_at TIMESTAMP,
    CONSTRAINT fk_statement_creditinfo FOREIGN KEY (credit_card_info_id) REFERENCES credit_card_info(id)
);

CREATE INDEX idx_statements_creditinfo ON credit_card_statements(credit_card_info_id);

-- NOT: Diğer tablolar devam edecek...
-- transactions, loans, investments, corporate vb.
-- Bu sadece ilk kısım. Tüm tabloları ekleyeceğim.

COMMIT;


