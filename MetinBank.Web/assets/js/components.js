/**
 * MetinBank UI Components
 * Handles dynamic loading of Header and Footer
 */

function getPathPrefix() {
    const depth = window.location.pathname.split('/').length - 2; // Rough estimation: /index.html = 0 depth relative to root if served from server, but file system is different.
    // Better approach for file:// protocol or simple folder structures:
    // Check if we are in 'kartlar', 'para-transferleri', or root.

    // Simple heuristic based on known structure
    const path = window.location.pathname;
    if (path.includes('/kartlar/banka-kartlari/mastercard/') || path.includes('/kartlar/banka-kartlari/troy/')) {
        return '../../../../';
    }
    if (path.includes('/para-transferleri/')) {
        return '../';
    }
    // Default to root
    return '';
}

// More robust relative path calculator
function getRelativePath() {
    // We assume the script is loaded from assets/js/components.js
    // So we can try to find where assets is relative to current page
    // But since we can't easily know that without looking at script tag...
    // Let's stick to the manual check for this project size.

    // Check for nested levels
    if (document.location.href.indexOf('/kartlar/banka-kartlari/') > -1) {
        return '../../../../';
    }
    if (document.location.href.indexOf('/para-transferleri/') > -1) {
        return '../';
    }
    return '';
}

const ASSET_PREFIX = getRelativePath();

const HEADER_HTML = `
    <!-- Top Bar (Extra Info) -->
    <div class="bg-gray-900 text-gray-300 text-xs py-2 border-b border-gray-800">
        <div class="container mx-auto px-4 sm:px-6 lg:px-8 flex justify-end gap-6">
            <a href="${ASSET_PREFIX}ucretler.html" class="hover:text-white transition-colors flex items-center gap-1">
                <svg class="w-3 h-3" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path></svg>
                Ürün ve Hizmet Ücretleri
            </a>
            <a href="#" class="hover:text-white transition-colors">İletişim</a>
            <a href="#" class="hover:text-white transition-colors">ATM / Şube</a>
        </div>
    </div>

    <!-- Main Header -->
    <header class="bg-white/95 backdrop-blur-md shadow-lg sticky top-0 z-50">
        <nav class="container mx-auto px-4 sm:px-6 lg:px-8">
            <div class="flex justify-between items-center h-20">
                <!-- Logo -->
                <div class="flex-shrink-0 flex items-center gap-3">
                    <a href="${ASSET_PREFIX}index.html" class="flex items-center gap-2 group">
                        <img src="${ASSET_PREFIX}assets/images/metinbanklogo.png" alt="MetinBank" class="h-10 w-auto group-hover:scale-105 transition-transform" />
                    </a>
                </div>
                
                <!-- Desktop Menu -->
                <div class="hidden lg:flex items-center space-x-1">
                    <div class="relative group">
                        <button class="px-4 py-2 text-gray-700 font-semibold group-hover:text-blue-700 flex items-center gap-1 transition-colors">
                            Kartlar
                            <svg class="w-4 h-4 text-gray-400 group-hover:text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7"></path></svg>
                        </button>
                        <!-- Dropdown -->
                        <div class="absolute left-0 mt-0 w-64 bg-white rounded-xl shadow-2xl opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-all duration-200 transform translate-y-2 group-hover:translate-y-0 border border-gray-100 overflow-hidden">
                            <div class="p-2">
                                <a href="${ASSET_PREFIX}kartlar/banka-kartlari/mastercard/index.html" class="block px-4 py-3 rounded-lg hover:bg-gray-50 transition">
                                    <div class="text-gray-900 font-bold">Mastercard</div>
                                    <div class="text-xs text-gray-500">Dünyanın her yerinde</div>
                                </a>
                                <a href="${ASSET_PREFIX}kartlar/banka-kartlari/troy/index.html" class="block px-4 py-3 rounded-lg hover:bg-red-50 transition">
                                    <div class="text-gray-900 font-bold">Troy</div>
                                    <div class="text-xs text-gray-500">Türkiyenin ödeme yöntemi</div>
                                </a>
                            </div>
                        </div>
                    </div>

                    <a href="${ASSET_PREFIX}para-transferleri/index.html" class="px-4 py-2 text-gray-700 font-semibold hover:text-blue-700 transition-colors">Para Transferleri</a>
                    <a href="#" class="px-4 py-2 text-gray-700 font-semibold hover:text-blue-700 transition-colors">Krediler</a>
                    <a href="${ASSET_PREFIX}ucretler.html" class="px-4 py-2 text-gray-700 font-semibold hover:text-blue-700 transition-colors">Ücretler</a>
                </div>

                <!-- Right Actions -->
                <div class="hidden md:flex items-center space-x-4">
                    <a href="${ASSET_PREFIX}internet-sube.html" class="bg-blue-600 text-white px-6 py-2.5 rounded-full font-bold shadow-lg hover:bg-blue-700 hover:shadow-xl transition-all duration-300 flex items-center gap-2 transform hover:-translate-y-0.5">
                        <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z"></path></svg>
                        İnternet Şube
                    </a>
                </div>
                
                <!-- Mobile Menu Button -->
                <button class="md:hidden text-gray-600">
                    <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"></path></svg>
                </button>
            </div>
        </nav>
    </header>
`;

const FOOTER_HTML = `
    <footer class="bg-gray-900 border-t border-gray-800 pt-16 pb-8 text-gray-400 mt-auto">
        <div class="container mx-auto px-4 sm:px-6 lg:px-8">
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-12 mb-12">
                
                <!-- Brand & Logo -->
                <div class="lg:col-span-2">
                     <a href="${ASSET_PREFIX}index.html" class="block mb-6">
                        <img src="${ASSET_PREFIX}assets/images/metinbanklogo.png" alt="MetinBank" class="h-12 w-auto brightness-0 invert" /> <!-- White filter applied -->
                    </a>
                    <p class="text-gray-500 mb-6 max-w-sm">
                        Güven, Birikim, Gelecek sloganıyla çıktığımız bu yolda, finansal ihtiyaçlarınıza en hızlı ve güvenli çözümleri sunuyoruz.
                    </p>
                </div>

                <div>
                    <h4 class="text-white font-bold mb-4">Hakkımızda</h4>
                    <ul class="space-y-2 text-sm">
                        <li><a href="#" class="hover:text-white transition">Biz Kimiz?</a></li>
                        <li><a href="#" class="hover:text-white transition">Yönetim</a></li>
                        <li><a href="#" class="hover:text-white transition">Kariyer</a></li>
                        <li><a href="#" class="hover:text-white transition">İletişim</a></li>
                    </ul>
                </div>
                <div>
                    <h4 class="text-white font-bold mb-4">Ürünler</h4>
                    <ul class="space-y-2 text-sm">
                        <li><a href="#" class="hover:text-white transition">Krediler</a></li>
                        <li><a href="#" class="hover:text-white transition">Kredi Kartları</a></li>
                        <li><a href="#" class="hover:text-white transition">Mevduat</a></li>
                        <li><a href="#" class="hover:text-white transition">Yatırım</a></li>
                    </ul>
                </div>
                <div>
                    <h4 class="text-white font-bold mb-4">Yardım</h4>
                    <ul class="space-y-2 text-sm">
                        <li><a href="#" class="hover:text-white transition">Sıkça Sorulan Sorular</a></li>
                        <li><a href="${ASSET_PREFIX}ucretler.html" class="hover:text-white transition">Ücretler ve Limitler</a></li>
                        <li><a href="#" class="hover:text-white transition">Sözleşmeler</a></li>
                        <li><a href="#" class="hover:text-white transition">Güvenlik</a></li>
                    </ul>
                </div>
            </div>
            
            <div class="border-t border-gray-800 pt-8 flex flex-col md:flex-row justify-between items-center text-sm">
                <p>&copy; 2026 MetinBank A.Ş. Tüm hakları saklıdır.</p>
                <div class="flex space-x-6 mt-4 md:mt-0">
                    <a href="#" class="hover:text-white transition">Gizlilik Politikası</a>
                    <a href="#" class="hover:text-white transition">Kullanım Koşulları</a>
                    <a href="#" class="hover:text-white transition">KVKK Aydınlatma</a>
                </div>
            </div>
        </div>
    </footer>
`;

function loadHeader(placeholderId = 'header-placeholder') {
    const el = document.getElementById(placeholderId);
    if (el) el.outerHTML = HEADER_HTML;
    else document.body.insertAdjacentHTML('afterbegin', HEADER_HTML);
}

function loadFooter(placeholderId = 'footer-placeholder') {
    const el = document.getElementById(placeholderId);
    if (el) el.outerHTML = FOOTER_HTML;
    else document.body.insertAdjacentHTML('beforeend', FOOTER_HTML);
}

// Auto-init if placeholders exist
document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementById('header-placeholder')) loadHeader();
    if (document.getElementById('footer-placeholder')) loadFooter();
    // Fallback if no placeholders but we want to force distinct sections? 
    // Usually manual calling is safer if no placeholders.
});
