# MetinBank İnternet Şube

## Genel Bakış
MetinBank İnternet Şube uygulaması, müşterilere tam özellikli online bankacılık hizmeti sunar.

## Özellikler

### 🔐 Giriş ve Güvenlik
- Müşteri numarası veya TC kimlik numarası ile giriş
- İlk kez şifre oluşturma
- Şifre sıfırlama
- Güvenli JWT token tabanlı authentication

### 💳 Hesap İşlemleri
- Tüm hesapların listelenmesi
- Hesap detaylarının görüntülenmesi
- Bakiye sorgulama
- Hesap ekstreleri (tarih filtreli)

### 💸 Para Transferi
- **Havale**: MetinBank hesaplarına IBAN ile transfer
- **EFT**: Diğer bankalara IBAN ile transfer
- **Virman**: Kendi hesaplarınız arasında transfer
- IBAN sorgulama ve alıcı doğrulama
- İşlem ücreti hesaplama
- Gerçek zamanlı bakiye kontrolü

### 🎴 Kart Yönetimi
- Banka kartlarının listelenmesi
- Kart detayları görüntüleme
- Kart durumu (Aktif/Bloke)
- Mastercard ve Troy kart desteği

### 💰 Kredi Başvurusu
- İhtiyaç, Taşıt, Konut kredisi türleri
- Kredi hesaplama (tutar, vade, faiz oranı)
- Online kredi başvurusu
- Başvuru durumu takibi

### 👤 Profil Yönetimi
- Kişisel bilgilerin görüntülenmesi
- Şifre değiştirme
- İletişim bilgileri

### 💱 Döviz Kurları
- Anlık döviz kurları (USD, EUR, GBP)
- Alış ve satış fiyatları

## Teknolojiler

### Frontend
- HTML5, CSS3
- Vanilla JavaScript (ES6+)
- Modern CSS (CSS Variables, Flexbox, Grid)
- Responsive Design

### Backend API
- ASP.NET Core Web API
- JWT Authentication
- RESTful API Design

## Kurulum

### Gereksinimler
1. .NET 8 SDK veya üzeri
2. MySQL Server
3. Modern web tarayıcı (Chrome, Firefox, Edge)

### API Kurulumu
```bash
cd MetinBank.WebAPI
dotnet restore
dotnet run
```

API varsayılan olarak `http://localhost:5000` adresinde çalışır.

### Web Sitesi
Web sitesi statik HTML dosyalarından oluşur. Herhangi bir web sunucusu ile çalıştırılabilir:

```bash
cd MetinBank.Web
# Live Server veya benzeri bir tool kullanın
```

Alternatif olarak Visual Studio Code'da Live Server extension'ı kullanabilirsiniz.

## API Endpoints

### Authentication
- `POST /api/auth/musteri-login` - Müşteri girişi
- `POST /api/auth/musteri-dogrula` - Müşteri doğrulama
- `POST /api/auth/sifre-sifirla` - Şifre sıfırlama
- `POST /api/auth/sifre-degistir` - Şifre değiştirme
- `GET /api/auth/profil/{musteriID}` - Profil bilgileri

### Hesap İşlemleri
- `GET /api/hesap/musteri/{musteriID}` - Müşterinin hesapları
- `GET /api/hesap/{hesapID}` - Hesap detayı
- `GET /api/hesap/{hesapID}/bakiye` - Bakiye sorgulama
- `POST /api/hesap/iban-sorgula` - IBAN sorgulama

### Transfer İşlemleri
- `POST /api/islem/havale` - Havale işlemi
- `POST /api/islem/eft` - EFT işlemi
- `POST /api/islem/virman` - Virman işlemi
- `GET /api/islem/musteri/{musteriID}/gecmis` - İşlem geçmişi
- `GET /api/islem/hesap/{hesapID}/ekstre` - Hesap ekstresi

### Kart İşlemleri
- `GET /api/kart/musteri/{musteriID}` - Müşterinin kartları

### Döviz İşlemleri
- `GET /api/doviz/kurlar` - Güncel döviz kurları
- `GET /api/doviz/kur/{paraBirimi}` - Belirli döviz kuru

### Kredi İşlemleri
- `GET /api/kredi/oranlar` - Kredi faiz oranları
- `POST /api/kredi/hesapla` - Kredi hesaplama
- `POST /api/kredi/basvuru` - Kredi başvurusu

## Kullanım

### 1. Giriş Yapma
1. `internet-sube/login.html` sayfasını açın
2. Müşteri numaranız veya TC kimlik numaranızı girin
3. Şifrenizi girin
4. "MetinBank İnternet Giriş" butonuna tıklayın

### 2. İlk Kez Şifre Alma
1. Login sayfasında "İlk kez parola almak istiyorum" linkine tıklayın
2. TC kimlik numaranızı girin
3. Doğrulama bilgilerinizi girin (Doğum tarihi, Anne adı, Cep telefonu)
4. Yeni şifrenizi belirleyin

### 3. Para Transferi Yapma
1. Üst menüden "Para Transferi" > "Havale/EFT/Virman" seçin
2. Gönderen hesabınızı seçin
3. Alıcı IBAN'ını girin ve sorgulayın
4. Tutarı girin
5. İşlem özeti kontrol edip "Gönder" butonuna tıklayın

### 4. Hesap Ekstresi Görüntüleme
1. Üst menüden "Ekstre" seçin
2. Hesabınızı seçin
3. Tarih aralığı belirleyin
4. "Filtrele" butonuna tıklayın

## Güvenlik Özellikleri

- ✅ JWT Token tabanlı authentication
- ✅ Şifre hashleme (BCrypt)
- ✅ HTTPS desteği (production)
- ✅ CORS yapılandırması
- ✅ SQL injection koruması
- ✅ XSS koruması
- ✅ İşlem limitleri
- ✅ Oturum yönetimi

## Responsive Tasarım

Uygulama tüm cihazlarda sorunsuz çalışır:
- 📱 Mobil cihazlar (320px - 768px)
- 💻 Tablet cihazlar (768px - 1024px)
- 🖥️ Masaüstü bilgisayarlar (1024px+)

## CSS Yapısı

```
assets/
├── css/
│   └── style.css          # Ana stil dosyası
├── js/
│   ├── api.js            # API iletişimi
│   ├── app.js            # Yardımcı fonksiyonlar
│   └── components.js     # UI bileşenleri
└── images/
    ├── logo.png
    └── ...
```

## Tarayıcı Desteği

- ✅ Chrome 90+
- ✅ Firefox 88+
- ✅ Safari 14+
- ✅ Edge 90+

## Sorun Giderme

### API'ye bağlanılamıyor
1. API'nin çalıştığından emin olun (`http://localhost:5000`)
2. CORS ayarlarını kontrol edin
3. `assets/js/api.js` dosyasındaki `API_BASE_URL` adresini kontrol edin

### Giriş yapılamıyor
1. Müşteri numarası veya TC kimlik numarasının doğru olduğunu kontrol edin
2. Şifrenizin doğru olduğunu kontrol edin
3. Veritabanı bağlantısını kontrol edin
4. Browser console'da hata mesajlarını kontrol edin

### CSS düzgün yüklenmiyor
1. Dosya yollarının doğru olduğunu kontrol edin
2. Browser cache'i temizleyin (Ctrl+F5)
3. Network sekmesinde CSS dosyasının yüklendiğini kontrol edin

## Geliştirici Notları

### Yeni Sayfa Ekleme
1. HTML sayfasını oluşturun
2. CSS linklerini ekleyin:
```html
<link rel="stylesheet" href="../assets/styles.css">
<link rel="stylesheet" href="assets/css/style.css">
```
3. JS dosyalarını ekleyin:
```html
<script src="../assets/js/api.js"></script>
<script src="../assets/js/app.js"></script>
```
4. Authentication kontrolü ekleyin:
```javascript
checkAuth();
```

### API Entegrasyonu
```javascript
// GET isteği
const response = await apiGet('/endpoint');

// POST isteği
const response = await apiPost('/endpoint', { data });

// Response kontrolü
if (response.success) {
    // Başarılı
} else {
    // Hata
}
```

## Lisans
© 2026 MetinBank A.Ş. Tüm hakları saklıdır.

## İletişim
- **E-posta**: info@metinbank.com.tr
- **Tel**: 0850 XXX XX XX
- **Web**: www.metinbank.com.tr
