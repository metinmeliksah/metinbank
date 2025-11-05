# MetinBank - Kurulum Rehberi

## 📋 İçindekiler
1. [Sistem Gereksinimleri](#sistem-gereksinimleri)
2. [Veritabanı Kurulumu](#veritabanı-kurulumu)
3. [Backend Kurulumu](#backend-kurulumu)
4. [Python Analytics Servisi](#python-analytics-servisi)
5. [Message Queue Kurulumu](#message-queue-kurulumu)
6. [Frontend Kurulumu](#frontend-kurulumu)
7. [Yapılandırma](#yapılandırma)
8. [İlk Çalıştırma](#ilk-çalıştırma)

## 1. Sistem Gereksinimleri

### Yazılım Gereksinimleri

#### Windows (Önerilen: Windows 10/11 veya Windows Server 2019+)
- **.NET 8 SDK** - [İndir](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Oracle XE 21c** - [İndir](https://www.oracle.com/database/technologies/xe-downloads.html)
- **PostgreSQL 15+** - [İndir](https://www.postgresql.org/download/)
- **Redis** - [İndir](https://github.com/microsoftarchive/redis/releases)
- **RabbitMQ** - [İndir](https://www.rabbitmq.com/download.html)
- **Python 3.11+** - [İndir](https://www.python.org/downloads/)
- **Node.js 18+** (Frontend için) - [İndir](https://nodejs.org/)
- **Visual Studio 2022** veya **VS Code** - [İndir](https://visualstudio.microsoft.com/)

#### Linux (Ubuntu 22.04 LTS)
```bash
# .NET 8 SDK
wget https://dot.net/v1/dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 8.0

# PostgreSQL
sudo apt update
sudo apt install postgresql postgresql-contrib

# Redis
sudo apt install redis-server

# RabbitMQ
sudo apt install rabbitmq-server

# Python
sudo apt install python3.11 python3-pip
```

### Donanım Gereksinimleri

**Minimum:**
- CPU: 4 cores
- RAM: 8 GB
- Disk: 50 GB

**Önerilen (Production):**
- CPU: 8+ cores
- RAM: 16 GB+
- Disk: 200 GB SSD

## 2. Veritabanı Kurulumu

### Oracle XE Kurulumu

#### Windows

1. **Oracle XE 21c'yi indirin ve kurun**

2. **SQL*Plus ile bağlanın:**
```cmd
sqlplus sys/yourpassword@localhost:1521/XE as sysdba
```

3. **MetinBank kullanıcısı ve tablespace oluşturun:**
```sql
-- Tablespace oluştur
CREATE TABLESPACE metinbank_data
  DATAFILE 'D:\oracle\metinbank_data.dbf'
  SIZE 1G
  AUTOEXTEND ON
  NEXT 100M
  MAXSIZE UNLIMITED;

-- Kullanıcı oluştur
CREATE USER metinbank IDENTIFIED BY "YourSecurePassword123!"
  DEFAULT TABLESPACE metinbank_data
  TEMPORARY TABLESPACE temp
  QUOTA UNLIMITED ON metinbank_data;

-- Yetkileri ver
GRANT CREATE SESSION TO metinbank;
GRANT CREATE TABLE TO metinbank;
GRANT CREATE SEQUENCE TO metinbank;
GRANT CREATE VIEW TO metinbank;
GRANT CREATE PROCEDURE TO metinbank;
GRANT CREATE TRIGGER TO metinbank;
GRANT CONNECT, RESOURCE TO metinbank;

-- Pluggable Database'e bağlan (XE 21c için)
ALTER SESSION SET CONTAINER = XEPDB1;

-- Aynı işlemleri XEPDB1'de tekrarla
```

4. **Connection string'i test edin:**
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));User Id=metinbank;Password=YourSecurePassword123!;
```

### PostgreSQL Kurulumu

#### Windows

1. **PostgreSQL'i kurun** (pgAdmin dahil)

2. **pgAdmin veya psql ile bağlanın:**
```cmd
psql -U postgres
```

3. **MetinBank log database'ini oluşturun:**
```sql
-- Database oluştur
CREATE DATABASE metinbank_logs
  WITH ENCODING = 'UTF8'
  LC_COLLATE = 'Turkish_Turkey.1254'
  LC_CTYPE = 'Turkish_Turkey.1254';

-- Kullanıcı oluştur
CREATE USER metinbank_user WITH PASSWORD 'YourSecurePassword123!';

-- Yetkileri ver
GRANT ALL PRIVILEGES ON DATABASE metinbank_logs TO metinbank_user;

-- Database'e bağlan
\c metinbank_logs

-- Schema oluştur
CREATE SCHEMA IF NOT EXISTS logs;

-- Kullanıcıya schema yetkisi ver
GRANT ALL ON SCHEMA logs TO metinbank_user;
GRANT ALL ON ALL TABLES IN SCHEMA logs TO metinbank_user;
GRANT ALL ON ALL SEQUENCES IN SCHEMA logs TO metinbank_user;
```

#### Linux
```bash
# PostgreSQL'e bağlan
sudo -u postgres psql

# Yukarıdaki SQL komutlarını çalıştır
```

### Redis Kurulumu

#### Windows
```cmd
# Redis'i indirip çalıştırın
redis-server

# Test edin
redis-cli ping
# Yanıt: PONG
```

#### Linux
```bash
# Redis'i başlat
sudo systemctl start redis
sudo systemctl enable redis

# Test et
redis-cli ping
```

## 3. Backend Kurulumu

### Adım 1: Projeyi Klonlayın

```bash
git clone https://github.com/yourusername/metinbank.git
cd metinbank
```

### Adım 2: NuGet Paketlerini Yükleyin

```bash
cd src/Backend
dotnet restore
```

### Adım 3: Connection String'leri Güncelleyin

`src/Backend/MetinBank.API/appsettings.json` dosyasını düzenleyin:

```json
{
  "ConnectionStrings": {
    "OracleConnection": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));User Id=metinbank;Password=YourSecurePassword123!;",
    "PostgreSQLConnection": "Host=localhost;Port=5432;Database=metinbank_logs;Username=metinbank_user;Password=YourSecurePassword123!;",
    "RedisConnection": "localhost:6379"
  },
  "JwtSettings": {
    "SecretKey": "CHANGE_THIS_TO_A_VERY_SECURE_256_BIT_KEY_IN_PRODUCTION_12345678901234567890",
    "Issuer": "MetinBankAPI",
    "Audience": "MetinBankClients",
    "TokenExpirationMinutes": 60
  }
}
```

### Adım 4: Database Migration'ları Çalıştırın

```bash
cd src/Backend/MetinBank.Infrastructure

# Oracle migration
dotnet ef migrations add InitialCreate --context OracleDbContext
dotnet ef database update --context OracleDbContext

# PostgreSQL migration
dotnet ef migrations add InitialCreate --context PostgreSqlDbContext
dotnet ef database update --context PostgreSqlDbContext
```

### Adım 5: Backend'i Çalıştırın

```bash
cd src/Backend/MetinBank.API
dotnet run
```

API şu adreste çalışacak: `https://localhost:5000` veya `http://localhost:5001`

Swagger UI: `https://localhost:5000/swagger`

## 4. Python Analytics Servisi

### Adım 1: Virtual Environment Oluşturun

#### Windows
```cmd
cd src\Python
python -m venv venv
venv\Scripts\activate
```

#### Linux
```bash
cd src/Python
python3 -m venv venv
source venv/bin/activate
```

### Adım 2: Gereksinimleri Yükleyin

```bash
pip install -r requirements.txt
```

`requirements.txt` içeriği:
```txt
flask==3.0.0
fastapi==0.104.1
uvicorn==0.24.0
numpy==1.26.2
pandas==2.1.3
scikit-learn==1.3.2
sqlalchemy==2.0.23
psycopg2-binary==2.9.9
requests==2.31.0
pydantic==2.5.0
python-dotenv==1.0.0
```

### Adım 3: Python Servisini Çalıştırın

```bash
# Flask
python app.py

# veya FastAPI
uvicorn main:app --host 0.0.0.0 --port 5001
```

Analytics servisi: `http://localhost:5001`

## 5. Message Queue Kurulumu

### RabbitMQ Kurulumu

#### Windows
```cmd
# RabbitMQ'yu kurun ve servis olarak başlatın
rabbitmq-plugins enable rabbitmq_management

# Management UI'ya erişin: http://localhost:15672
# Varsayılan: guest/guest
```

#### Linux
```bash
sudo systemctl start rabbitmq-server
sudo systemctl enable rabbitmq-server
sudo rabbitmq-plugins enable rabbitmq_management
```

### RabbitMQ Yapılandırması

```bash
# Yeni kullanıcı oluştur
rabbitmqctl add_user metinbank SecurePassword123!
rabbitmqctl set_user_tags metinbank administrator
rabbitmqctl set_permissions -p / metinbank ".*" ".*" ".*"

# Virtual host oluştur
rabbitmqctl add_vhost metinbank_vhost
rabbitmqctl set_permissions -p metinbank_vhost metinbank ".*" ".*" ".*"
```

### Kafka Kurulumu (Alternatif)

```bash
# Docker ile Kafka
docker run -d --name zookeeper -p 2181:2181 zookeeper
docker run -d --name kafka -p 9092:9092 \
  --link zookeeper \
  -e KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181 \
  -e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 \
  -e KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 \
  confluentinc/cp-kafka
```

## 6. Frontend Kurulumu

### Web Frontend (React/Angular)

```bash
cd src/Frontend

# Dependencies yükle
npm install

# Development modunda çalıştır
npm start

# Production build
npm run build
```

### Mobile Application (React Native)

```bash
cd src/Mobile

# Dependencies
npm install

# iOS
npx react-native run-ios

# Android
npx react-native run-android
```

## 7. Yapılandırma

### Ortam Değişkenleri

`.env` dosyası oluşturun (Her servis için):

#### Backend (.NET)
```env
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=https://localhost:5000;http://localhost:5001

# Database
ORACLE_CONNECTION_STRING=...
POSTGRESQL_CONNECTION_STRING=...
REDIS_CONNECTION_STRING=...

# JWT
JWT_SECRET_KEY=...
JWT_ISSUER=MetinBankAPI
JWT_AUDIENCE=MetinBankClients

# External Services
PYTHON_ANALYTICS_URL=http://localhost:5001
SMS_PROVIDER_API_KEY=...
FCM_SERVER_KEY=...

# Security
ENCRYPTION_KEY=...
ENCRYPTION_IV=...
```

#### Python Analytics
```env
FLASK_ENV=development
FLASK_APP=app.py

DATABASE_URL=postgresql://metinbank_user:password@localhost/metinbank_logs
API_PORT=5001

# ML Models
MODEL_PATH=./models
```

### Güvenlik Yapılandırması

1. **Şifreleme Anahtarları Oluşturun**

```bash
# .NET Core'da random key oluşturma
dotnet user-secrets set "Encryption:Key" "YourRandomKey"
dotnet user-secrets set "Encryption:IV" "YourRandomIV"
```

2. **SSL Sertifikaları**

```bash
# Development sertifikası
dotnet dev-certs https --trust
```

## 8. İlk Çalıştırma

### Tüm Servisleri Başlatın

#### Yöntem 1: Manuel
```bash
# Terminal 1: Backend
cd src/Backend/MetinBank.API
dotnet run

# Terminal 2: Python Analytics
cd src/Python
python app.py

# Terminal 3: Frontend
cd src/Frontend
npm start
```

#### Yöntem 2: Docker Compose (Gelecekte)
```bash
docker-compose up -d
```

### İlk Kullanıcı Oluşturma

1. **Swagger UI'ya gidin:** `https://localhost:5000/swagger`

2. **POST /api/auth/register endpoint'ini çağırın:**

```json
{
  "customerType": 1,
  "tcKimlikNo": "12345678901",
  "firstName": "Metin",
  "lastName": "Dermencioğlu",
  "email": "metin@example.com",
  "phoneNumber": "+905551234567",
  "password": "SecurePassword123!",
  "dateOfBirth": "1990-01-01"
}
```

3. **Login yapın:** POST `/api/auth/login`

```json
{
  "email": "metin@example.com",
  "password": "SecurePassword123!"
}
```

4. **JWT token'ı alın ve Swagger'da "Authorize" butonuna yapıştırın**

### Sistem Kontrolü

```bash
# Backend health check
curl https://localhost:5000/health

# Python analytics health check
curl http://localhost:5001/health

# Database bağlantısı test
curl https://localhost:5000/api/system/database-status
```

## 🔧 Sorun Giderme

### Oracle Bağlantı Sorunu
```sql
-- TNS Listener durumunu kontrol et
lsnrctl status

-- Servis adlarını görüntüle
SELECT name FROM v$database;
SELECT instance_name FROM v$instance;
```

### PostgreSQL Bağlantı Sorunu
```bash
# PostgreSQL log'larını kontrol et
tail -f /var/log/postgresql/postgresql-15-main.log

# pg_hba.conf dosyasını kontrol et
sudo nano /etc/postgresql/15/main/pg_hba.conf
```

### Redis Bağlantı Sorunu
```bash
# Redis'in çalışıp çalışmadığını kontrol et
redis-cli ping

# Redis log'larını kontrol et
redis-cli monitor
```

## 📚 Ek Kaynaklar

- [.NET 8 Dokümantasyonu](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Oracle XE Dokümantasyonu](https://docs.oracle.com/en/database/oracle/oracle-database/21/xeinl/)
- [PostgreSQL Dokümantasyonu](https://www.postgresql.org/docs/)
- [RabbitMQ Tutorials](https://www.rabbitmq.com/getstarted.html)

## 📞 Destek

Sorunlarla karşılaşırsanız:
1. [Issue açın](https://github.com/yourusername/metinbank/issues)
2. [Dokümantasyonu kontrol edin](./PROJE_DURUMU.md)
3. Proje sahibiyle iletişime geçin

---

**Son Güncelleme:** 4 Kasım 2025


