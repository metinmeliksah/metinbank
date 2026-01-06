// Kredi Hesaplama ve Başvuru Mantığı

document.addEventListener('DOMContentLoaded', function () {
    initCalculator();
    initApplyButtons();
});

function initCalculator() {
    const btnHesapla = document.getElementById('btnHesapla');
    if (!btnHesapla) return;

    btnHesapla.addEventListener('click', async function () {
        const tutar = parseFloat(document.getElementById('calcTutar').value);
        const vade = parseInt(document.getElementById('calcVade').value);

        if (!tutar || tutar <= 0) {
            alert('Lütfen geçerli bir tutar giriniz.');
            return;
        }

        const hesaplamaSonucdiv = document.getElementById('hesaplamaSonuc');
        const loadingDiv = document.getElementById('calcLoading');

        hesaplamaSonucdiv.classList.add('hidden');
        loadingDiv.classList.remove('hidden');

        try {
            const data = await apiPost('/kredi/hesapla', { tutar, vade });

            // Sonuçları göster
            document.getElementById('resTaksit').textContent = formatCurrency(data.AylikTaksit);
            document.getElementById('resToplam').textContent = formatCurrency(data.ToplamOdeme);
            document.getElementById('resOran').textContent = '%' + data.FaizOrani;

            loadingDiv.classList.add('hidden');
            hesaplamaSonucdiv.classList.remove('hidden');
        } catch (error) {
            alert('Hesaplama hatası: ' + error.message);
            loadingDiv.classList.add('hidden');
        }
    });
}

function initApplyButtons() {
    const buttons = document.querySelectorAll('.btn-basvur');
    buttons.forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();

            if (!isAuthenticated()) {
                if (confirm("Başvuru yapabilmek için giriş yapmalısınız. Giriş sayfasına yönlendirilsin mi?")) {
                    window.location.href = 'login.html';
                }
                return;
            }

            const tur = this.getAttribute('data-tur');
            openBasvuruModal(tur);
        });
    });
}

function openBasvuruModal(krediTuru) {
    // Varsa hesaplanmış değerleri al
    const calcTutar = document.getElementById('calcTutar').value;
    const calcVade = document.getElementById('calcVade').value;

    document.getElementById('modalTutar').value = calcTutar || 10000;
    document.getElementById('modalVade').value = calcVade || 12;
    document.getElementById('modalKrediTuru').value = krediTuru || 'Ihtiyac';

    // Gelir bilgisini kullanıcıdan almalıyız (Profilde yoksa)
    // Modal'ı göster
    const modal = document.getElementById('basvuruModal');
    modal.classList.remove('hidden');
}

function closeBasvuruModal() {
    document.getElementById('basvuruModal').classList.add('hidden');
}

// Modal Form Submit
async function submitBasvuru() {
    const tutar = parseFloat(document.getElementById('modalTutar').value);
    const vade = parseInt(document.getElementById('modalVade').value);
    const gelir = parseFloat(document.getElementById('modalGelir').value);
    const tur = document.getElementById('modalKrediTuru').value;

    if (!gelir || gelir <= 0) {
        alert('Lütfen aylık gelirinizi giriniz.');
        return;
    }

    const user = getUser();
    if (!user) {
        alert('Kullanıcı bilgisi bulunamadı.');
        return;
    }

    const btnSubmit = document.getElementById('btnModalSubmit');
    btnSubmit.disabled = true;
    btnSubmit.textContent = 'İşleniyor...';

    const model = {
        MusteriID: user.MusteriID || 0, // API Token'dan da alabilir ama burada gönderelim
        TCKN: user.TCKN, // API veritabanından bulmalı aslında
        AdSoyad: user.AdSoyad,
        CepTelefon: user.CepTelefon, // Frontend'de olmayabilir, API handle etmeli veya formdan istenmeli. API basit model bekliyor.
        AylikGelir: gelir,
        TalepEdilenTutar: tutar,
        TalepEdilenVade: vade,
        Kanal: 'Web',
        Aciklama: `${tur} Kredisi Başvurusu` // Basitçe türü gönderelim
    };

    try {
        const result = await apiPost('/kredi/basvuru', model);

        alert(result.Mesaj || 'Başvurunuz alındı!');
        closeBasvuruModal();
    } catch (error) {
        alert('Başvuru başarısız: ' + error.message);
    } finally {
        btnSubmit.disabled = false;
        btnSubmit.textContent = 'Başvuruyu Tamamla';
    }
}
