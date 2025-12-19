using System;
using System.Collections.Generic;
using MetinBank.Entities;
using MetinBank.Modul.Business;
using MetinBank.Modul.Interface;

namespace MetinBank.Modul.Service
{
    /// <summary>
    /// Müşteri servisi - Hata yönetimi ve API/Db çağrıları
    /// Service katmanı her zaman string döner (null = başarılı, değilse hata mesajı)
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly CustomerBusiness _customerBusiness;

        public CustomerService()
        {
            _customerBusiness = new CustomerBusiness();
        }

        /// <summary>
        /// Tüm müşterileri getirir
        /// </summary>
        public string? GetAllCustomers(out List<Customer>? customers)
        {
            customers = null;

            try
            {
                customers = _customerBusiness.GetAllCustomers();
                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Müşteriler listelenirken hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşteri detayını getirir
        /// </summary>
        public string? GetCustomerById(int customerId, out Customer? customer)
        {
            customer = null;

            try
            {
                if (customerId <= 0)
                    return "Geçersiz müşteri ID!";

                customer = _customerBusiness.GetCustomerById(customerId);

                if (customer == null)
                    return "Müşteri bulunamadı!";

                return null; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Müşteri bilgisi alınırken hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşteri kaydeder (ekle/güncelle)
        /// </summary>
        public string? SaveCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                    return "Müşteri bilgisi boş olamaz!";

                // Business katmanındaki validasyon ve kaydetme
                string? result = _customerBusiness.SaveCustomer(customer);
                return result; // null ise başarılı, değilse hata mesajı
            }
            catch (Exception ex)
            {
                return $"Müşteri kaydedilirken hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşteri siler
        /// </summary>
        public string? DeleteCustomer(int customerId)
        {
            try
            {
                if (customerId <= 0)
                    return "Geçersiz müşteri ID!";

                string? result = _customerBusiness.DeleteCustomer(customerId);
                return result; // null ise başarılı, değilse hata mesajı
            }
            catch (Exception ex)
            {
                return $"Müşteri silinirken hata: {ex.Message}";
            }
        }
    }
}
