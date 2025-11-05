# MetinBank Python Analytics Service

Bu servis, MetinBank sisteminin analitik ve risk değerlendirme işlemlerini yürütür.

## 🎯 Özellikler

### 1. Real-Time Risk Analysis
- İşlem bazlı risk skorlaması (0-100)
- Çok faktörlü risk analizi
- Anlık approve/review/decline kararları

### 2. Credit Scoring
- **Bireysel Müşteriler:** Gelir, yaş, hesap geçmişi bazlı
- **Kurumsal Müşteriler:** Ciro, şirket yaşı, sektör bazlı
- Otomatik kredi limit önerisi
- Faiz oranı hesaplama

### 3. Customer Analytics (Gelecekte)
- Harcama alışkanlıkları analizi
- Gelir/ciro tahmini
- Segmentasyon
- Churn prediction

## 📦 Kurulum

### Gereksinimler
- Python 3.11+
- pip
- virtualenv (önerilir)

### Adımlar

```bash
# 1. Virtual environment oluştur
python -m venv venv

# 2. Aktif et
# Windows:
venv\Scripts\activate
# Linux/Mac:
source venv/bin/activate

# 3. Bağımlılıkları yükle
pip install -r requirements.txt

# 4. Environment değişkenlerini ayarla
cp .env.example .env
# .env dosyasını düzenle

# 5. Çalıştır
python app.py
```

Servis `http://localhost:5001` adresinde başlayacak.

## 🔌 API Endpoints

### Health Check
```bash
GET /health

Response:
{
  "status": "healthy",
  "service": "MetinBank Analytics Service",
  "version": "1.0",
  "timestamp": "2025-11-04T12:00:00Z"
}
```

### Risk Analysis
```bash
POST /api/risk/analyze

Request Body:
{
  "transaction_id": "uuid",
  "customer_id": "uuid",
  "transaction_type": "TRANSFER",
  "amount": 50000,
  "currency": "TRY",
  "channel": "MOBILE",
  "customer_age_days": 365,
  "is_first_time": false
}

Response:
{
  "transaction_id": "uuid",
  "risk_score": 45.5,
  "risk_level": "medium",
  "result": "approve",
  "factors": [...],
  "recommendation": "...",
  "analyzed_at": "2025-11-04T12:00:00Z"
}
```

### Retail Credit Score
```bash
POST /api/credit/score-retail

Request Body:
{
  "customer_id": "uuid",
  "monthly_income": 15000,
  "age": 35,
  "existing_loans": 2,
  "total_debt": 50000,
  "account_age_months": 24
}

Response:
{
  "customer_id": "uuid",
  "credit_score": 750,
  "score_level": "excellent",
  "max_loan_amount": 250000,
  "recommended_interest_rate": 1.99,
  "result": "approve"
}
```

### Commercial Credit Score
```bash
POST /api/credit/score-commercial

Request Body:
{
  "customer_id": "uuid",
  "annual_revenue": 5000000,
  "company_age_years": 10,
  "employee_count": 50
}

Response:
{
  "customer_id": "uuid",
  "credit_score": 750,
  "max_loan_amount": 2500000,
  "result": "approve"
}
```

## 🧪 Test

```bash
# Basit test
curl http://localhost:5001/health

# Risk analysis test
curl -X POST http://localhost:5001/api/risk/analyze \
  -H "Content-Type: application/json" \
  -d '{
    "transaction_id": "test-123",
    "customer_id": "customer-456",
    "amount": 50000,
    "transaction_type": "TRANSFER",
    "customer_age_days": 365
  }'
```

## 📊 Risk Scoring Logic

### Faktörler ve Ağırlıklar

1. **İşlem Tutarı (0-30 puan)**
   - > 100,000 TL: 30 puan
   - > 50,000 TL: 20 puan
   - > 10,000 TL: 10 puan

2. **Müşteri Yaşı (0-15 puan)**
   - < 30 gün: 15 puan (Yeni müşteri)
   - < 90 gün: 10 puan
   - < 180 gün: 5 puan

3. **İşlem Saati (0-10 puan)**
   - 00:00-06:00: 10 puan (Gece)
   - 06:00-08:00 veya 20:00-23:00: 5 puan

4. **İlk Kez İşlem (0-15 puan)**
   - İlk kez: 15 puan

5. **Kanal (0-10 puan)**
   - ATM: 5 puan
   - Web/Mobile: 3 puan
   - Şube: 0 puan

6. **Müşteri Risk Profili (0-20 puan)**
   - Yüksek: 20 puan
   - Orta: 10 puan
   - Düşük: 0 puan

### Risk Seviyeleri

- **Düşük (0-30):** Otomatik onay
- **Orta (30-70):** 2FA veya manuel inceleme
- **Yüksek (70-100):** Manuel inceleme veya red

## 🔮 Gelecek Geliştirmeler

### Kısa Vadeli
- [ ] PostgreSQL loglama entegrasyonu
- [ ] Redis caching
- [ ] Batch analytics job
- [ ] Customer profiling

### Orta Vadeli
- [ ] Gerçek ML modelleri (sklearn, tensorflow)
- [ ] A/B testing framework
- [ ] Fraud detection
- [ ] Anomaly detection

### Uzun Vadeli
- [ ] Deep learning modelleri
- [ ] Real-time streaming analytics (Kafka)
- [ ] Grafana dashboard
- [ ] Auto-scaling

## 📝 Notlar

- **ÖNEMLİ:** Şu anki risk ve kredi skoru algoritmaları simülasyondur.
- Production'da gerçek ML modelleri kullanılmalıdır.
- Model eğitimi için geçmiş veri gereklidir.
- Düzenli model re-training yapılmalıdır.

## 🔒 Güvenlik

- API key authentication eklenecek
- Rate limiting uygulanacak
- Input validation yapılmalı
- HTTPS kullanılmalı

## 📞 Destek

Sorularınız için:
- [Ana README](../../README.md)
- [Kurulum Rehberi](../../docs/KURULUM_REHBERI.md)

---

**Versiyon:** 1.0  
**Son Güncelleme:** 4 Kasım 2025


