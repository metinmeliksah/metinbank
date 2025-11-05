"""
MetinBank Python Analytics Service
Versiyon: 1.0
Tarih: 4 Kasım 2025

Bu servis şu işlemleri yapar:
1. Real-time risk analizi
2. Kredi skoru hesaplama (Bireysel ve Kurumsal)
3. Müşteri analitikleri (Batch)
4. Gelir/Ciro tahmini
"""

from flask import Flask, request, jsonify
from flask_cors import CORS
import numpy as np
from datetime import datetime
import logging
import os
from typing import Dict, Any

# Logging yapılandırması
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# Flask app
app = Flask(__name__)
CORS(app)

# Yapılandırma
PORT = int(os.getenv('PORT', 5001))
DEBUG = os.getenv('DEBUG', 'False').lower() == 'true'

# ============================================
# Health Check
# ============================================

@app.route('/health', methods=['GET'])
def health_check():
    """Servis sağlık kontrolü"""
    return jsonify({
        'status': 'healthy',
        'service': 'MetinBank Analytics Service',
        'version': '1.0',
        'timestamp': datetime.utcnow().isoformat()
    }), 200

# ============================================
# Risk Analysis - Real-time
# ============================================

@app.route('/api/risk/analyze', methods=['POST'])
def analyze_risk():
    """
    İşlem risk analizi yapar
    
    Request Body:
    {
        "transaction_id": "uuid",
        "customer_id": "uuid",
        "transaction_type": "TRANSFER",
        "amount": 50000,
        "currency": "TRY",
        "to_iban": "TR...",
        "channel": "MOBILE",
        "ip_address": "192.168.1.1",
        "device_id": "...",
        "time_of_day": 14,
        "is_first_time": false,
        "customer_age_days": 365,
        "customer_risk_profile": "Low"
    }
    
    Response:
    {
        "transaction_id": "uuid",
        "risk_score": 45.5,
        "risk_level": "medium",
        "result": "approve|decline|review",
        "factors": [...],
        "recommendation": "...",
        "analyzed_at": "..."
    }
    """
    try:
        data = request.get_json()
        
        # Validasyon
        required_fields = ['transaction_id', 'customer_id', 'amount', 'transaction_type']
        for field in required_fields:
            if field not in data:
                return jsonify({'error': f'Missing required field: {field}'}), 400
        
        # Risk skoru hesapla (Simülasyon - Gerçek ML modeli entegre edilecek)
        risk_score = calculate_risk_score(data)
        
        # Risk seviyesi belirle
        if risk_score < 30:
            risk_level = 'low'
            result = 'approve'
        elif risk_score < 70:
            risk_level = 'medium'
            result = 'approve' if risk_score < 50 else 'review'
        else:
            risk_level = 'high'
            result = 'review' if risk_score < 85 else 'decline'
        
        # Risk faktörleri
        factors = identify_risk_factors(data, risk_score)
        
        response = {
            'transaction_id': data['transaction_id'],
            'risk_score': round(risk_score, 2),
            'risk_level': risk_level,
            'result': result,
            'factors': factors,
            'recommendation': generate_recommendation(risk_level, factors),
            'analyzed_at': datetime.utcnow().isoformat()
        }
        
        logger.info(f"Risk analysis completed for transaction {data['transaction_id']}: {result}")
        
        return jsonify(response), 200
        
    except Exception as e:
        logger.error(f"Error in risk analysis: {str(e)}")
        return jsonify({'error': 'Internal server error'}), 500

def calculate_risk_score(data: Dict[str, Any]) -> float:
    """
    Risk skoru hesapla (0-100)
    
    Faktörler:
    - İşlem tutarı
    - Müşteri yaşı
    - İşlem saati
    - Kanal
    - İlk kez yapılan işlem mi?
    - Müşterinin mevcut risk profili
    """
    score = 0.0
    
    # 1. İşlem tutarı riski (0-30 puan)
    amount = data.get('amount', 0)
    if amount > 100000:
        score += 30
    elif amount > 50000:
        score += 20
    elif amount > 10000:
        score += 10
    elif amount > 5000:
        score += 5
    
    # 2. Müşteri yaşı riski (0-15 puan)
    customer_age_days = data.get('customer_age_days', 0)
    if customer_age_days < 30:
        score += 15  # Yeni müşteri
    elif customer_age_days < 90:
        score += 10
    elif customer_age_days < 180:
        score += 5
    
    # 3. İşlem saati riski (0-10 puan)
    time_of_day = data.get('time_of_day', 12)
    if time_of_day < 6 or time_of_day > 23:
        score += 10  # Gece vakti
    elif time_of_day < 8 or time_of_day > 20:
        score += 5
    
    # 4. İlk kez yapılan işlem (0-15 puan)
    if data.get('is_first_time', False):
        score += 15
    
    # 5. Kanal riski (0-10 puan)
    channel = data.get('channel', 'WEB')
    if channel == 'ATM':
        score += 5
    elif channel == 'BRANCH':
        score += 0  # Şube güvenli
    else:
        score += 3
    
    # 6. Müşteri risk profili (0-20 puan)
    customer_risk = data.get('customer_risk_profile', 'Medium')
    if customer_risk == 'High':
        score += 20
    elif customer_risk == 'Medium':
        score += 10
    
    # Rastgele varyasyon ekle (daha gerçekçi simülasyon için)
    score += np.random.uniform(-5, 5)
    
    return max(0, min(100, score))

def identify_risk_factors(data: Dict[str, Any], risk_score: float) -> list:
    """Risk faktörlerini tanımla"""
    factors = []
    
    amount = data.get('amount', 0)
    if amount > 50000:
        factors.append({
            'factor': 'high_amount',
            'description': f'Yüksek işlem tutarı: {amount:,.2f} TL',
            'weight': 'high'
        })
    
    customer_age_days = data.get('customer_age_days', 0)
    if customer_age_days < 90:
        factors.append({
            'factor': 'new_customer',
            'description': f'Yeni müşteri: {customer_age_days} gündür müşteri',
            'weight': 'medium'
        })
    
    if data.get('is_first_time', False):
        factors.append({
            'factor': 'first_time_transaction',
            'description': 'İlk kez yapılan işlem tipi',
            'weight': 'medium'
        })
    
    time_of_day = data.get('time_of_day', 12)
    if time_of_day < 6 or time_of_day > 23:
        factors.append({
            'factor': 'unusual_time',
            'description': f'Olağandışı işlem saati: {time_of_day}:00',
            'weight': 'medium'
        })
    
    return factors

def generate_recommendation(risk_level: str, factors: list) -> str:
    """Öneri oluştur"""
    if risk_level == 'low':
        return 'İşlem düşük riskli, onaylanabilir.'
    elif risk_level == 'medium':
        if len(factors) > 2:
            return 'İşlem orta riskli, ek doğrulama (2FA) önerilir.'
        else:
            return 'İşlem orta riskli ancak onaylanabilir.'
    else:
        return f'İşlem yüksek riskli, {len(factors)} risk faktörü tespit edildi. Manuel inceleme gerekli.'

# ============================================
# Credit Score - Retail (Bireysel)
# ============================================

@app.route('/api/credit/score-retail', methods=['POST'])
def calculate_retail_credit_score():
    """
    Bireysel müşteri kredi skoru hesapla
    
    Request Body:
    {
        "customer_id": "uuid",
        "monthly_income": 15000,
        "employment_type": "FULL_TIME",
        "age": 35,
        "existing_loans": 2,
        "total_debt": 50000,
        "account_age_months": 24,
        "average_monthly_balance": 20000,
        "transaction_count_6m": 150,
        "late_payment_count": 0
    }
    
    Response:
    {
        "customer_id": "uuid",
        "credit_score": 750,
        "score_level": "excellent",
        "max_loan_amount": 250000,
        "recommended_interest_rate": 1.99,
        "result": "approve|review|decline",
        "factors": [...],
        "calculated_at": "..."
    }
    """
    try:
        data = request.get_json()
        
        # Kredi skoru hesapla (300-900 arası)
        base_score = 500
        
        # Gelir faktörü
        monthly_income = data.get('monthly_income', 0)
        income_score = min(200, monthly_income / 100)
        
        # Yaş faktörü
        age = data.get('age', 0)
        age_score = min(50, age - 18) if age >= 18 else 0
        
        # Hesap yaşı faktörü
        account_age_months = data.get('account_age_months', 0)
        account_score = min(100, account_age_months * 2)
        
        # Borç oranı faktörü
        total_debt = data.get('total_debt', 0)
        debt_ratio = (total_debt / monthly_income) if monthly_income > 0 else 10
        debt_score = max(-200, min(50, (2 - debt_ratio) * 50))
        
        # Ödeme geçmişi
        late_payments = data.get('late_payment_count', 0)
        payment_score = max(-150, -late_payments * 30)
        
        # Toplam skor
        credit_score = int(base_score + income_score + age_score + account_score + debt_score + payment_score)
        credit_score = max(300, min(900, credit_score))
        
        # Skor seviyesi
        if credit_score >= 750:
            score_level = 'excellent'
            result = 'approve'
        elif credit_score >= 650:
            score_level = 'good'
            result = 'approve'
        elif credit_score >= 550:
            score_level = 'fair'
            result = 'review'
        else:
            score_level = 'poor'
            result = 'decline'
        
        # Maksimum kredi tutarı
        max_loan_amount = monthly_income * 12 * (credit_score / 900)
        
        # Faiz oranı önerisi
        if credit_score >= 750:
            interest_rate = 1.99
        elif credit_score >= 650:
            interest_rate = 2.49
        elif credit_score >= 550:
            interest_rate = 2.99
        else:
            interest_rate = 3.49
        
        response = {
            'customer_id': data.get('customer_id'),
            'credit_score': credit_score,
            'score_level': score_level,
            'max_loan_amount': round(max_loan_amount, 2),
            'recommended_interest_rate': interest_rate,
            'result': result,
            'factors': {
                'income_contribution': round(income_score, 2),
                'age_contribution': round(age_score, 2),
                'account_age_contribution': round(account_score, 2),
                'debt_contribution': round(debt_score, 2),
                'payment_history_contribution': round(payment_score, 2)
            },
            'calculated_at': datetime.utcnow().isoformat()
        }
        
        logger.info(f"Retail credit score calculated for customer {data.get('customer_id')}: {credit_score}")
        
        return jsonify(response), 200
        
    except Exception as e:
        logger.error(f"Error in retail credit score calculation: {str(e)}")
        return jsonify({'error': 'Internal server error'}), 500

# ============================================
# Credit Score - Commercial (Ticari)
# ============================================

@app.route('/api/credit/score-commercial', methods=['POST'])
def calculate_commercial_credit_score():
    """
    Ticari/Kurumsal müşteri kredi skoru hesapla
    
    Request Body:
    {
        "customer_id": "uuid",
        "annual_revenue": 5000000,
        "company_age_years": 10,
        "employee_count": 50,
        "existing_loans": 1,
        "total_debt": 1000000,
        "average_monthly_balance": 500000,
        "late_payment_count": 0,
        "sector": "MANUFACTURING"
    }
    """
    try:
        data = request.get_json()
        
        # Ticari kredi skoru (benzer mantık, farklı ağırlıklar)
        base_score = 500
        
        annual_revenue = data.get('annual_revenue', 0)
        revenue_score = min(200, annual_revenue / 50000)
        
        company_age = data.get('company_age_years', 0)
        age_score = min(100, company_age * 10)
        
        # Basit hesaplama
        credit_score = int(base_score + revenue_score + age_score)
        credit_score = max(300, min(900, credit_score))
        
        response = {
            'customer_id': data.get('customer_id'),
            'credit_score': credit_score,
            'score_level': 'good' if credit_score >= 650 else 'fair',
            'max_loan_amount': annual_revenue * 0.5,
            'result': 'approve' if credit_score >= 550 else 'review',
            'calculated_at': datetime.utcnow().isoformat()
        }
        
        return jsonify(response), 200
        
    except Exception as e:
        logger.error(f"Error in commercial credit score: {str(e)}")
        return jsonify({'error': 'Internal server error'}), 500

# ============================================
# Main
# ============================================

if __name__ == '__main__':
    logger.info(f"Starting MetinBank Analytics Service on port {PORT}")
    logger.info(f"Debug mode: {DEBUG}")
    app.run(host='0.0.0.0', port=PORT, debug=DEBUG)


