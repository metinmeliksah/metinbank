/**
 * MetinBank UI Components
 * Handles dynamic loading of Header and Footer
 */

function getRelativePath() {
    const path = window.location.pathname;

    // Depth 3: kartlar/banka-kartlari/mastercard/
    if (path.includes('/kartlar/banka-kartlari/mastercard/') || path.includes('/kartlar/banka-kartlari/troy/')) {
        return '../../../';
    }

    // Depth 1: para-transferleri/
    if (path.includes('/para-transferleri/')) {
        return '../';
    }

    // Default: Root
    return '';
}

const ASSET_PREFIX = getRelativePath();
const IS_TICARI = window.location.href.indexOf('ticari.html') > -1;

// Define active/inactive classes for the Bireysel/Sirketim switch
const SWITCH_ACTIVE_CLASS = "text-purple-700 relative py-1";
const SWITCH_INACTIVE_CLASS = "text-gray-400 hover:text-blue-500 transition-colors";
const SWITCH_INDICATOR = `<span class="absolute bottom-0 left-0 w-full h-0.5 bg-purple-600 rounded-full"></span>`;

function getHeaderHtml() {
    // Determine switch states
    const bireyselClass = IS_TICARI ? SWITCH_INACTIVE_CLASS : SWITCH_ACTIVE_CLASS;
    const bireyselIndicator = IS_TICARI ? '' : SWITCH_INDICATOR;

    const sirketimClass = IS_TICARI ? SWITCH_ACTIVE_CLASS : SWITCH_INACTIVE_CLASS;
    const sirketimIndicator = IS_TICARI ? SWITCH_INDICATOR : '';

    // Determine Menu Content
    let menuContent = '';

    if (IS_TICARI) {
        // Ticari Menu Content
        menuContent = '';
    } else {
        // Bireysel Menu Content (Standard)
        menuContent = `
            <a href="${ASSET_PREFIX}mevduat.html" class="px-4 py-2 text-gray-700 font-semibold hover:text-blue-700 transition-colors">Mevduat</a>
            <a href="${ASSET_PREFIX}yatirim.html" class="px-4 py-2 text-gray-700 font-semibold hover:text-blue-700 transition-colors">Yatırım</a>
            <a href="${ASSET_PREFIX}krediler.html" class="px-4 py-2 text-gray-700 font-semibold hover:text-blue-700 transition-colors">Krediler</a>

            <div class="relative group">
                <button class="px-4 py-2 text-gray-700 font-semibold group-hover:text-blue-700 flex items-center gap-1 transition-colors">
                    Kartlar
                    <svg class="w-4 h-4 text-gray-400 group-hover:text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7"></path></svg>
                </button>
                <div class="absolute left-0 mt-0 w-64 bg-white rounded-xl shadow-2xl opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-all duration-200 transform translate-y-2 group-hover:translate-y-0 border border-gray-100 overflow-hidden z-50">
                    <div class="p-2">
                        <a href="${ASSET_PREFIX}kredi-kartlari.html" class="block px-4 py-3 rounded-lg hover:bg-blue-50 transition border-b border-gray-50">
                            <div class="text-blue-700 font-bold">Tüm Kartlar</div>
                            <div class="text-xs text-gray-500">Avantajları keşfedin</div>
                        </a>
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
        `;
    }

    return `
    <!-- Top Bar -->
    <div class="bg-gray-900 text-gray-300 text-xs py-2 border-b border-gray-800">
        <div class="container mx-auto px-4 sm:px-6 lg:px-8 flex justify-end gap-6">
            <a href="${ASSET_PREFIX}ucretler.html" class="hover:text-white transition-colors flex items-center gap-1">
                <svg class="w-3 h-3" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path></svg>
                Ürün ve Hizmet Ücretleri
            </a>
            <a href="${ASSET_PREFIX}iletisim.html" class="hover:text-white transition-colors">İletişim</a>
            <a href="${ASSET_PREFIX}atm-sube.html" class="hover:text-white transition-colors">ATM / Şube</a>
        </div>
    </div>

    <!-- Main Header -->
    <header class="bg-white/95 backdrop-blur-md shadow-lg sticky top-0 z-50">
        <nav class="container mx-auto px-4 sm:px-6 lg:px-8">
            <div class="flex justify-between items-center h-20">
                <!-- Logo & Switcher -->
                <div class="flex-shrink-0 flex items-center gap-6">
                    <a href="${ASSET_PREFIX}index.html" class="flex items-center gap-2 group">
                        <img src="${ASSET_PREFIX}assets/images/metinbanklogo.png" alt="MetinBank" class="h-10 w-auto group-hover:scale-105 transition-transform" />
                    </a>
                    
                    <div class="hidden lg:flex items-center gap-4 text-sm font-bold tracking-wider ml-4 border-l border-gray-200 pl-6 h-10">
                        <a href="${ASSET_PREFIX}index.html" class="${bireyselClass}">
                            BİREYSEL
                            ${bireyselIndicator}
                        </a>
                        <a href="${ASSET_PREFIX}ticari.html" class="${sirketimClass}">
                            ŞİRKETİM
                            ${sirketimIndicator}
                        </a>
                    </div>
                </div>
                
                <!-- Desktop Menu Area -->
                <div class="hidden lg:flex items-center space-x-1">
                    ${menuContent}
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
}

const FOOTER_HTML = `
    <footer class="bg-gray-900 border-t border-gray-800 pt-16 pb-8 text-gray-400 mt-auto">
        <div class="container mx-auto px-4 sm:px-6 lg:px-8">
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-12 mb-12">
                <div class="lg:col-span-2">
                     <a href="${ASSET_PREFIX}index.html" class="block mb-6">
                        <img src="${ASSET_PREFIX}assets/images/metinbanklogo.png" alt="MetinBank" class="h-12 w-auto brightness-0 invert" /> 
                    </a>
                    <p class="text-gray-500 mb-6 max-w-sm">
                        Güven, Birikim, Gelecek sloganıyla çıktığımız bu yolda, finansal ihtiyaçlarınıza en hızlı ve güvenli çözümleri sunuyoruz.
                    </p>
                </div>

                <div>
                    <h4 class="text-white font-bold mb-4">Hakkımızda</h4>
                    <ul class="space-y-2 text-sm">
                        <li><a href="${ASSET_PREFIX}hakkimizda.html" class="hover:text-white transition">Biz Kimiz?</a></li>
                        <li><a href="${ASSET_PREFIX}yonetim.html" class="hover:text-white transition">Yönetim</a></li>
                        <li><a href="${ASSET_PREFIX}kariyer.html" class="hover:text-white transition">Kariyer</a></li>
                        <li><a href="${ASSET_PREFIX}iletisim.html" class="hover:text-white transition">İletişim</a></li>
                    </ul>
                </div>
                <div>
                    <h4 class="text-white font-bold mb-4">Ürünler</h4>
                    <ul class="space-y-2 text-sm">
                        <li><a href="${ASSET_PREFIX}krediler.html" class="hover:text-white transition">Krediler</a></li>
                        <li><a href="${ASSET_PREFIX}kredi-kartlari.html" class="hover:text-white transition">Kredi Kartları</a></li>
                        <li><a href="${ASSET_PREFIX}mevduat.html" class="hover:text-white transition">Mevduat</a></li>
                        <li><a href="${ASSET_PREFIX}yatirim.html" class="hover:text-white transition">Yatırım</a></li>
                    </ul>
                </div>
                <div>
                    <h4 class="text-white font-bold mb-4">Yardım</h4>
                    <ul class="space-y-2 text-sm">
                        <li><a href="${ASSET_PREFIX}sss.html" class="hover:text-white transition">Sıkça Sorulan Sorular</a></li>
                        <li><a href="${ASSET_PREFIX}ucretler.html" class="hover:text-white transition">Ücretler ve Limitler</a></li>
                        <li><a href="${ASSET_PREFIX}sozlesmeler.html" class="hover:text-white transition">Sözleşmeler</a></li>
                        <li><a href="${ASSET_PREFIX}guvenlik.html" class="hover:text-white transition">Güvenlik</a></li>
                    </ul>
                </div>
            </div>
            <div class="border-t border-gray-800 pt-8 flex flex-col md:flex-row justify-between items-center text-sm">
                <p>&copy; 2026 MetinBank A.Ş. Tüm hakları saklıdır.</p>
                <div class="flex space-x-6 mt-4 md:mt-0">
                    <a href="${ASSET_PREFIX}gizlilik-politikasi.html" class="hover:text-white transition">Gizlilik Politikası</a>
                    <a href="${ASSET_PREFIX}kullanim-kosullari.html" class="hover:text-white transition">Kullanım Koşulları</a>
                    <a href="${ASSET_PREFIX}kvkk.html" class="hover:text-white transition">KVKK Aydınlatma</a>
                </div>
            </div>
        </div>
    </footer>
`;

function loadHeader(placeholderId = 'header-placeholder') {
    const el = document.getElementById(placeholderId);
    if (el) el.outerHTML = getHeaderHtml(); // Call function to get dynamic state
    else document.body.insertAdjacentHTML('afterbegin', getHeaderHtml());
}

function loadFooter(placeholderId = 'footer-placeholder') {
    const el = document.getElementById(placeholderId);
    if (el) el.outerHTML = FOOTER_HTML;
    else document.body.insertAdjacentHTML('beforeend', FOOTER_HTML);
}

document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementById('header-placeholder')) loadHeader();
    if (document.getElementById('footer-placeholder')) loadFooter();
});
