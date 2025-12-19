using System;
using System.Collections.Generic;
using MetinBank.Entities;
using MetinBank.Modul.Business;
using MetinBank.Modul.Interface;

namespace MetinBank.Modul.Service
{
    /// <summary>
    /// Hesap servisi - Hata yönetimi ve API/Db çağrıları
    /// Service katmanı her zaman string döner (null = başarılı, değilse hata mesajı)
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly AccountBusiness _accountBusiness;

        public AccountService()
        {
            _accountBusiness = new AccountBusiness();
        }

        /// <summary>
        /// Müşteriye ait hesapları getirir
        /// </summary>
        public string? GetAccountsByCustomerId(int customerId, out List<Account>? accounts)
        {
            accounts = null;

            try
            {
                if (customerId <= 0)
                    return "Geçersiz müşteri ID!";

                accounts = _accountBusiness.GetAccountsByCustomerId(customerId);
                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Hesaplar listelenirken hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Hesap detayını getirir
        /// </summary>
        public string? GetAccountById(int accountId, out Account? account)
        {
            account = null;

            try
            {
                if (accountId <= 0)
                    return "Geçersiz hesap ID!";

                account = _accountBusiness.GetAccountById(accountId);

                if (account == null)
                    return "Hesap bulunamadı!";

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Hesap bilgisi alınırken hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Yeni hesap açar
        /// </summary>
        public string? CreateAccount(Account account)
        {
            try
            {
                if (account == null)
                    return "Hesap bilgisi boş olamaz!";

                string? result = _accountBusiness.CreateAccount(account);
                return result; // null ise başarılı, değilse hata mesajı
            }
            catch (Exception ex)
            {
                return $"Hesap açılırken hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Hesap bakiyesini getirir
        /// </summary>
        public string? GetAccountBalance(int accountId, out decimal balance)
        {
            balance = 0;

            try
            {
                if (accountId <= 0)
                    return "Geçersiz hesap ID!";

                balance = _accountBusiness.GetAccountBalance(accountId);
                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Bakiye bilgisi alınırken hata: {ex.Message}";
            }
        }
    }
}
