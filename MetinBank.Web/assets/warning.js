/* assets/warning.js
   Injects a site-wide warning banner used across pages.
   Usage: include <script src="/assets/warning.js" defer></script> in pages.
*/
(function(){
  const bannerHTML = `
    <div class="bg-yellow-300 text-yellow-900 p-3 text-center text-sm font-medium" role="status" aria-live="polite">
      <div class="container mx-auto px-4">
        <p><strong>Uyarı:</strong> Bu sistem bir eğitim projesidir. Gerçek bir banka veya finans kuruluşu değildir. Sistemdeki veriler örnek amaçlıdır; hiçbir gerçek işlem veya para transferi yapılmaz.</p>
      </div>
    </div>
  `;

  function injectBanner() {
    try {
      // Insert as first child of body so it appears above header
      if (!document.body) return;
      // Avoid duplicate insertions
      if (document.querySelector('.metinbank-warning-banner')) return;

      const wrapper = document.createElement('div');
      wrapper.className = 'metinbank-warning-banner';
      wrapper.innerHTML = bannerHTML;

      // Insert before first element in body
      if (document.body.firstChild) {
        document.body.insertBefore(wrapper, document.body.firstChild);
      } else {
        document.body.appendChild(wrapper);
      }
    } catch (err) {
      // fail silently
      console.error('Warning banner injection failed', err);
    }
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', injectBanner);
  } else {
    injectBanner();
  }
})();
