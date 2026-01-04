// Authentication check
function checkAuth() {
    if (!isAuthenticated()) {
        window.location.href = 'login.html';
        return false;
    }
    return true;
}

// Logout function
function logout() {
    if (confirm('Çıkış yapmak istediğinizden emin misiniz?')) {
        clearAuthData();
        window.location.href = 'login.html';
    }
}

// Format currency
function formatCurrency(amount, currency = 'TL') {
    if (amount === null || amount === undefined) return '-';
    const formatted = new Intl.NumberFormat('tr-TR', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    }).format(amount);
    return `${formatted} ${currency}`;
}

// Format date
function formatDate(dateString) {
    if (!dateString) return '-';
    const date = new Date(dateString);
    return date.toLocaleDateString('tr-TR', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit'
    });
}

// Format IBAN
function formatIBAN(iban) {
    if (!iban) return '-';
    // Remove spaces
    iban = iban.replace(/\s/g, '');
    // Format as XXXX XXXX XXXX XXXX XXXX XXXX XX
    return iban.match(/.{1,4}/g)?.join(' ') || iban;
}

// Show loading spinner
function showLoading(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.innerHTML = '<div class="loading">Yükleniyor...</div>';
    }
}

// Hide loading spinner
function hideLoading(elementId) {
    const element = document.getElementById(elementId);
    if (element && element.querySelector('.loading')) {
        element.innerHTML = '';
    }
}

// Show toast notification
function showToast(message, type = 'info') {
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    toast.textContent = message;

    document.body.appendChild(toast);

    setTimeout(() => {
        toast.classList.add('show');
    }, 100);

    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => {
            document.body.removeChild(toast);
        }, 300);
    }, 3000);
}

// Validate IBAN
function validateIBAN(iban) {
    iban = iban.replace(/\s/g, '').toUpperCase();

    if (iban.length !== 26) {
        return false;
    }

    if (!iban.startsWith('TR')) {
        return false;
    }

    return true;
}

// Validate Turkish phone number
function validatePhone(phone) {
    phone = phone.replace(/\s/g, '');
    const phoneRegex = /^0[0-9]{10}$/;
    return phoneRegex.test(phone);
}

// Validate email
function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

// Format phone number
function formatPhone(phone) {
    if (!phone) return '-';
    phone = phone.replace(/\s/g, '');
    if (phone.length === 11) {
        return `${phone.substr(0, 4)} ${phone.substr(4, 3)} ${phone.substr(7, 2)} ${phone.substr(9, 2)}`;
    }
    return phone;
}

// Debounce function
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Initialize page
document.addEventListener('DOMContentLoaded', function () {
    // Add active class to current page nav link
    const currentPath = window.location.pathname.split('/').pop();
    document.querySelectorAll('.nav-link').forEach(link => {
        if (link.getAttribute('href') === currentPath) {
            link.classList.add('active');
        }
    });
});
