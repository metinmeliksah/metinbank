-- =========================================
-- MetinBank - MsSQL Veritabanı Script
-- Aşama 1: Temel Tablolar ve Stored Procedures
-- =========================================

USE master;
GO

-- Veritabanı oluştur
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MetinBankDB')
BEGIN
    CREATE DATABASE MetinBankDB;
    PRINT 'MetinBankDB veritabanı oluşturuldu.';
END
GO

USE MetinBankDB;
GO

-- =========================================
-- TABLOLAR
-- =========================================

-- 1. TBL_USERS: Kullanıcı tablosu
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TBL_USERS')
BEGIN
    CREATE TABLE TBL_USERS (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        UserName NVARCHAR(50) NOT NULL UNIQUE,
        Password NVARCHAR(100) NOT NULL,
        FullName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100),
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        LastLoginDate DATETIME NULL,
        RoleId INT NULL,
        BranchId INT NULL
    );
    PRINT 'TBL_USERS tablosu oluşturuldu.';
END
GO

-- 2. TBL_KUL_EKRAN: Kullanıcı ekran yetkileri
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TBL_KUL_EKRAN')
BEGIN
    CREATE TABLE TBL_KUL_EKRAN (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        ScreenCode NVARCHAR(50) NOT NULL,
        ScreenName NVARCHAR(100) NOT NULL,
        CanView BIT DEFAULT 1,
        CanAdd BIT DEFAULT 0,
        CanEdit BIT DEFAULT 0,
        CanDelete BIT DEFAULT 0,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_KulEkran_Users FOREIGN KEY (UserId) REFERENCES TBL_USERS(UserId)
    );
    PRINT 'TBL_KUL_EKRAN tablosu oluşturuldu.';
END
GO

-- 3. TBL_BRANCH: Şube tablosu
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TBL_BRANCH')
BEGIN
    CREATE TABLE TBL_BRANCH (
        BranchId INT IDENTITY(1,1) PRIMARY KEY,
        BranchCode NVARCHAR(10) NOT NULL UNIQUE,
        BranchName NVARCHAR(100) NOT NULL,
        City NVARCHAR(50),
        District NVARCHAR(50),
        Address NVARCHAR(500),
        Phone NVARCHAR(20),
        Email NVARCHAR(100),
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE()
    );
    PRINT 'TBL_BRANCH tablosu oluşturuldu.';
END
GO

-- 4. TBL_CUSTOMER: Müşteri tablosu
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TBL_CUSTOMER')
BEGIN
    CREATE TABLE TBL_CUSTOMER (
        CustomerId INT IDENTITY(1,1) PRIMARY KEY,
        IdentityNumber NVARCHAR(11) NOT NULL UNIQUE,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        BirthDate DATETIME NULL,
        Gender NVARCHAR(10),
        Email NVARCHAR(100),
        Phone NVARCHAR(20),
        Address NVARCHAR(500),
        BranchId INT NULL,
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NULL,
        Photo VARBINARY(MAX) NULL,
        Signature VARBINARY(MAX) NULL,
        CONSTRAINT FK_Customer_Branch FOREIGN KEY (BranchId) REFERENCES TBL_BRANCH(BranchId)
    );
    PRINT 'TBL_CUSTOMER tablosu oluşturuldu.';
END
GO

-- 5. TBL_ACCOUNT: Hesap tablosu
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TBL_ACCOUNT')
BEGIN
    CREATE TABLE TBL_ACCOUNT (
        AccountId INT IDENTITY(1,1) PRIMARY KEY,
        AccountNumber NVARCHAR(20) NOT NULL UNIQUE,
        CustomerId INT NOT NULL,
        BranchId INT NOT NULL,
        AccountType NVARCHAR(50) NOT NULL, -- 'Vadesiz', 'Vadeli', 'Altın'
        CurrencyCode NVARCHAR(3) NOT NULL DEFAULT 'TRY',
        Balance DECIMAL(18,2) DEFAULT 0,
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NULL,
        CONSTRAINT FK_Account_Customer FOREIGN KEY (CustomerId) REFERENCES TBL_CUSTOMER(CustomerId),
        CONSTRAINT FK_Account_Branch FOREIGN KEY (BranchId) REFERENCES TBL_BRANCH(BranchId)
    );
    PRINT 'TBL_ACCOUNT tablosu oluşturuldu.';
END
GO

-- 6. TBL_TRANSACTION: İşlem tablosu
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TBL_TRANSACTION')
BEGIN
    CREATE TABLE TBL_TRANSACTION (
        TransactionId INT IDENTITY(1,1) PRIMARY KEY,
        TransactionNumber NVARCHAR(20) NOT NULL UNIQUE,
        AccountId INT NOT NULL,
        TransactionType NVARCHAR(50) NOT NULL, -- 'Yatırma', 'Çekme', 'Havale', 'EFT'
        Amount DECIMAL(18,2) NOT NULL,
        CurrencyCode NVARCHAR(3) NOT NULL DEFAULT 'TRY',
        ExchangeRate DECIMAL(18,4) NULL,
        Description NVARCHAR(500),
        TransactionDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NULL,
        IsApproved BIT DEFAULT 0,
        ApprovedBy INT NULL,
        ApprovedDate DATETIME NULL,
        BranchId INT NOT NULL,
        CONSTRAINT FK_Transaction_Account FOREIGN KEY (AccountId) REFERENCES TBL_ACCOUNT(AccountId),
        CONSTRAINT FK_Transaction_Branch FOREIGN KEY (BranchId) REFERENCES TBL_BRANCH(BranchId)
    );
    PRINT 'TBL_TRANSACTION tablosu oluşturuldu.';
END
GO

-- =========================================
-- STORED PROCEDURES
-- =========================================

-- sp_User_Login: Kullanıcı giriş kontrolü
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_User_Login')
    DROP PROCEDURE sp_User_Login;
GO

CREATE PROCEDURE sp_User_Login
    @UserName NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        UserId, UserName, FullName, Email, IsActive, RoleId, BranchId
    FROM TBL_USERS
    WHERE UserName = @UserName 
        AND Password = @Password
        AND IsActive = 1;
    
    -- Son giriş tarihini güncelle
    IF @@ROWCOUNT > 0
    BEGIN
        UPDATE TBL_USERS 
        SET LastLoginDate = GETDATE()
        WHERE UserName = @UserName;
    END
END
GO
PRINT 'sp_User_Login prosedürü oluşturuldu.';
GO

-- sp_User_GetScreens: Kullanıcının ekran yetkilerini getirir
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_User_GetScreens')
    DROP PROCEDURE sp_User_GetScreens;
GO

CREATE PROCEDURE sp_User_GetScreens
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, UserId, ScreenCode, ScreenName, 
        CanView, CanAdd, CanEdit, CanDelete
    FROM TBL_KUL_EKRAN
    WHERE UserId = @UserId;
END
GO
PRINT 'sp_User_GetScreens prosedürü oluşturuldu.';
GO

-- sp_User_CheckScreenPermission: Ekran erişim yetkisi kontrolü
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_User_CheckScreenPermission')
    DROP PROCEDURE sp_User_CheckScreenPermission;
GO

CREATE PROCEDURE sp_User_CheckScreenPermission
    @UserId INT,
    @ScreenCode NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT COUNT(*)
    FROM TBL_KUL_EKRAN
    WHERE UserId = @UserId 
        AND ScreenCode = @ScreenCode
        AND CanView = 1;
END
GO
PRINT 'sp_User_CheckScreenPermission prosedürü oluşturuldu.';
GO

-- sp_Branch_GetAll: Tüm şubeleri getirir
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Branch_GetAll')
    DROP PROCEDURE sp_Branch_GetAll;
GO

CREATE PROCEDURE sp_Branch_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        BranchId, BranchCode, BranchName, City, District, 
        Address, Phone, Email, IsActive, CreatedDate
    FROM TBL_BRANCH
    WHERE IsActive = 1
    ORDER BY BranchName;
END
GO
PRINT 'sp_Branch_GetAll prosedürü oluşturuldu.';
GO

-- sp_Branch_GetById: Şube detayını getirir
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Branch_GetById')
    DROP PROCEDURE sp_Branch_GetById;
GO

CREATE PROCEDURE sp_Branch_GetById
    @BranchId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        BranchId, BranchCode, BranchName, City, District, 
        Address, Phone, Email, IsActive, CreatedDate
    FROM TBL_BRANCH
    WHERE BranchId = @BranchId;
END
GO
PRINT 'sp_Branch_GetById prosedürü oluşturuldu.';
GO

-- sp_Customer_GetAll: Tüm müşterileri getirir
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Customer_GetAll')
    DROP PROCEDURE sp_Customer_GetAll;
GO

CREATE PROCEDURE sp_Customer_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CustomerId, c.IdentityNumber, c.FirstName, c.LastName,
        c.BirthDate, c.Gender, c.Email, c.Phone, c.Address,
        c.BranchId, b.BranchName, c.IsActive, c.CreatedDate, 
        c.CreatedBy, c.Photo, c.Signature
    FROM TBL_CUSTOMER c
    LEFT JOIN TBL_BRANCH b ON c.BranchId = b.BranchId
    WHERE c.IsActive = 1
    ORDER BY c.FirstName, c.LastName;
END
GO
PRINT 'sp_Customer_GetAll prosedürü oluşturuldu.';
GO

-- sp_Customer_GetById: Müşteri detayını getirir
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Customer_GetById')
    DROP PROCEDURE sp_Customer_GetById;
GO

CREATE PROCEDURE sp_Customer_GetById
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.CustomerId, c.IdentityNumber, c.FirstName, c.LastName,
        c.BirthDate, c.Gender, c.Email, c.Phone, c.Address,
        c.BranchId, b.BranchName, c.IsActive, c.CreatedDate, 
        c.CreatedBy, c.Photo, c.Signature
    FROM TBL_CUSTOMER c
    LEFT JOIN TBL_BRANCH b ON c.BranchId = b.BranchId
    WHERE c.CustomerId = @CustomerId;
END
GO
PRINT 'sp_Customer_GetById prosedürü oluşturuldu.';
GO

-- sp_Customer_Save: Müşteri ekler/günceller
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Customer_Save')
    DROP PROCEDURE sp_Customer_Save;
GO

CREATE PROCEDURE sp_Customer_Save
    @CustomerId INT,
    @IdentityNumber NVARCHAR(11),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @BirthDate DATETIME,
    @Gender NVARCHAR(10),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(500),
    @BranchId INT,
    @IsActive BIT,
    @CreatedBy INT,
    @Photo VARBINARY(MAX),
    @Signature VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @CustomerId = 0 OR @CustomerId IS NULL
    BEGIN
        -- Yeni kayıt
        INSERT INTO TBL_CUSTOMER (
            IdentityNumber, FirstName, LastName, BirthDate, Gender,
            Email, Phone, Address, BranchId, IsActive, CreatedBy, Photo, Signature
        )
        VALUES (
            @IdentityNumber, @FirstName, @LastName, @BirthDate, @Gender,
            @Email, @Phone, @Address, @BranchId, @IsActive, @CreatedBy, @Photo, @Signature
        );
    END
    ELSE
    BEGIN
        -- Güncelleme
        UPDATE TBL_CUSTOMER
        SET 
            IdentityNumber = @IdentityNumber,
            FirstName = @FirstName,
            LastName = @LastName,
            BirthDate = @BirthDate,
            Gender = @Gender,
            Email = @Email,
            Phone = @Phone,
            Address = @Address,
            BranchId = @BranchId,
            IsActive = @IsActive,
            Photo = ISNULL(@Photo, Photo),
            Signature = ISNULL(@Signature, Signature)
        WHERE CustomerId = @CustomerId;
    END
END
GO
PRINT 'sp_Customer_Save prosedürü oluşturuldu.';
GO

-- sp_Customer_Delete: Müşteri siler (soft delete)
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Customer_Delete')
    DROP PROCEDURE sp_Customer_Delete;
GO

CREATE PROCEDURE sp_Customer_Delete
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE TBL_CUSTOMER
    SET IsActive = 0
    WHERE CustomerId = @CustomerId;
END
GO
PRINT 'sp_Customer_Delete prosedürü oluşturuldu.';
GO

-- sp_Account_GetByCustomerId: Müşteriye ait hesapları getirir
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Account_GetByCustomerId')
    DROP PROCEDURE sp_Account_GetByCustomerId;
GO

CREATE PROCEDURE sp_Account_GetByCustomerId
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        a.AccountId, a.AccountNumber, a.CustomerId,
        c.FirstName + ' ' + c.LastName AS CustomerName,
        a.BranchId, b.BranchName, a.AccountType, a.CurrencyCode,
        a.Balance, a.IsActive, a.CreatedDate, a.CreatedBy
    FROM TBL_ACCOUNT a
    INNER JOIN TBL_CUSTOMER c ON a.CustomerId = c.CustomerId
    INNER JOIN TBL_BRANCH b ON a.BranchId = b.BranchId
    WHERE a.CustomerId = @CustomerId
        AND a.IsActive = 1
    ORDER BY a.CreatedDate DESC;
END
GO
PRINT 'sp_Account_GetByCustomerId prosedürü oluşturuldu.';
GO

-- sp_Account_GetById: Hesap detayını getirir
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Account_GetById')
    DROP PROCEDURE sp_Account_GetById;
GO

CREATE PROCEDURE sp_Account_GetById
    @AccountId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        a.AccountId, a.AccountNumber, a.CustomerId,
        c.FirstName + ' ' + c.LastName AS CustomerName,
        a.BranchId, b.BranchName, a.AccountType, a.CurrencyCode,
        a.Balance, a.IsActive, a.CreatedDate, a.CreatedBy
    FROM TBL_ACCOUNT a
    INNER JOIN TBL_CUSTOMER c ON a.CustomerId = c.CustomerId
    INNER JOIN TBL_BRANCH b ON a.BranchId = b.BranchId
    WHERE a.AccountId = @AccountId;
END
GO
PRINT 'sp_Account_GetById prosedürü oluşturuldu.';
GO

-- sp_Account_Create: Yeni hesap açar
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Account_Create')
    DROP PROCEDURE sp_Account_Create;
GO

CREATE PROCEDURE sp_Account_Create
    @AccountNumber NVARCHAR(20),
    @CustomerId INT,
    @BranchId INT,
    @AccountType NVARCHAR(50),
    @CurrencyCode NVARCHAR(3),
    @CreatedBy INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Hesap numarası yoksa oluştur
    IF @AccountNumber IS NULL OR @AccountNumber = ''
    BEGIN
        SET @AccountNumber = 'HES' + CONVERT(NVARCHAR, @BranchId) + 
                            FORMAT(NEXT VALUE FOR seq_AccountNumber, '00000000');
    END
    
    INSERT INTO TBL_ACCOUNT (
        AccountNumber, CustomerId, BranchId, AccountType, 
        CurrencyCode, Balance, CreatedBy
    )
    VALUES (
        @AccountNumber, @CustomerId, @BranchId, @AccountType,
        @CurrencyCode, 0, @CreatedBy
    );
END
GO
PRINT 'sp_Account_Create prosedürü oluşturuldu.';
GO

-- sp_Account_GetBalance: Hesap bakiyesini getirir
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_Account_GetBalance')
    DROP PROCEDURE sp_Account_GetBalance;
GO

CREATE PROCEDURE sp_Account_GetBalance
    @AccountId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Balance
    FROM TBL_ACCOUNT
    WHERE AccountId = @AccountId;
END
GO
PRINT 'sp_Account_GetBalance prosedürü oluşturuldu.';
GO

-- Hesap numarası için sequence oluştur
IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'seq_AccountNumber')
BEGIN
    CREATE SEQUENCE seq_AccountNumber
        START WITH 1
        INCREMENT BY 1;
    PRINT 'seq_AccountNumber sequence oluşturuldu.';
END
GO

-- =========================================
-- TEST VERİLERİ
-- =========================================

-- Şube test verileri
IF NOT EXISTS (SELECT * FROM TBL_BRANCH)
BEGIN
    INSERT INTO TBL_BRANCH (BranchCode, BranchName, City, District, Phone, Email)
    VALUES 
        ('MRK001', 'Merkez Şube', 'Elazığ', 'Merkez', '0424 123 45 67', 'merkez@metinbank.com'),
        ('IST001', 'İstanbul Şube', 'İstanbul', 'Kadıköy', '0216 123 45 67', 'istanbul@metinbank.com'),
        ('ANK001', 'Ankara Şube', 'Ankara', 'Çankaya', '0312 123 45 67', 'ankara@metinbank.com');
    
    PRINT 'Şube test verileri eklendi.';
END
GO

-- Kullanıcı test verileri (Şifre: 123456)
IF NOT EXISTS (SELECT * FROM TBL_USERS)
BEGIN
    INSERT INTO TBL_USERS (UserName, Password, FullName, Email, IsActive, RoleId, BranchId)
    VALUES 
        ('admin', '123456', 'Admin Kullanıcı', 'admin@metinbank.com', 1, 1, 1),
        ('mudur', '123456', 'Şube Müdürü', 'mudur@metinbank.com', 1, 2, 1),
        ('personel', '123456', 'Banka Personeli', 'personel@metinbank.com', 1, 3, 1);
    
    PRINT 'Kullanıcı test verileri eklendi.';
END
GO

-- Kullanıcı ekran yetkileri
IF NOT EXISTS (SELECT * FROM TBL_KUL_EKRAN)
BEGIN
    -- Admin tüm yetkilere sahip
    INSERT INTO TBL_KUL_EKRAN (UserId, ScreenCode, ScreenName, CanView, CanAdd, CanEdit, CanDelete)
    VALUES 
        (1, 'MUSTERI_LISTESI', 'Müşteri Listesi', 1, 1, 1, 1),
        (1, 'MUSTERI_KARTI', 'Müşteri Kartı', 1, 1, 1, 1),
        (1, 'HESAP_ISLEMLERI', 'Hesap İşlemleri', 1, 1, 1, 1);
    
    -- Müdür görüntüleme ve onay yetkisine sahip
    INSERT INTO TBL_KUL_EKRAN (UserId, ScreenCode, ScreenName, CanView, CanAdd, CanEdit, CanDelete)
    VALUES 
        (2, 'MUSTERI_LISTESI', 'Müşteri Listesi', 1, 0, 1, 0),
        (2, 'HESAP_ISLEMLERI', 'Hesap İşlemleri', 1, 0, 1, 0);
    
    -- Personel sadece görüntüleme yetkisine sahip
    INSERT INTO TBL_KUL_EKRAN (UserId, ScreenCode, ScreenName, CanView, CanAdd, CanEdit, CanDelete)
    VALUES 
        (3, 'MUSTERI_LISTESI', 'Müşteri Listesi', 1, 0, 0, 0);
    
    PRINT 'Kullanıcı ekran yetkileri eklendi.';
END
GO

PRINT '==========================================';
PRINT 'MetinBank veritabanı başarıyla oluşturuldu!';
PRINT 'Test Kullanıcıları:';
PRINT '  - Kullanıcı: admin, Şifre: 123456';
PRINT '  - Kullanıcı: mudur, Şifre: 123456';
PRINT '  - Kullanıcı: personel, Şifre: 123456';
PRINT '==========================================';
GO
