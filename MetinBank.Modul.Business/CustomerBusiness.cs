using System;
using System.Collections.Generic;
using System.Data;
using MetinBank.Entities;
using MetinBank.Modul.SPObject;

namespace MetinBank.Modul.Business
{
    /// <summary>
    /// Müşteri iş kurallarını yöneten sınıf
    /// </summary>
    public class CustomerBusiness
    {
        private readonly CustomerSP _customerSP;

        public CustomerBusiness()
        {
            _customerSP = new CustomerSP();
        }

        /// <summary>
        /// Tüm müşterileri getirir
        /// </summary>
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            DataTable dt = _customerSP.GetAllCustomers();

            foreach (DataRow row in dt.Rows)
            {
                customers.Add(MapCustomer(row));
            }

            return customers;
        }

        /// <summary>
        /// Müşteri detayını getirir
        /// </summary>
        public Customer? GetCustomerById(int customerId)
        {
            DataTable dt = _customerSP.GetCustomerById(customerId);
            
            if (dt.Rows.Count > 0)
            {
                return MapCustomer(dt.Rows[0]);
            }

            return null;
        }

        /// <summary>
        /// Müşteri kaydeder (ekle/güncelle)
        /// </summary>
        public string? SaveCustomer(Customer customer)
        {
            // İş kuralları validasyonu
            if (string.IsNullOrWhiteSpace(customer.FirstName))
                return "Müşteri adı boş olamaz!";

            if (string.IsNullOrWhiteSpace(customer.LastName))
                return "Müşteri soyadı boş olamaz!";

            if (string.IsNullOrWhiteSpace(customer.IdentityNumber))
                return "TC Kimlik No boş olamaz!";

            if (customer.IdentityNumber.Length != 11)
                return "TC Kimlik No 11 haneli olmalıdır!";

            try
            {
                int result = _customerSP.SaveCustomer(customer);
                return result > 0 ? null : "Müşteri kaydedilemedi!";
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        /// <summary>
        /// Müşteri siler
        /// </summary>
        public string? DeleteCustomer(int customerId)
        {
            try
            {
                int result = _customerSP.DeleteCustomer(customerId);
                return result > 0 ? null : "Müşteri silinemedi!";
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        private Customer MapCustomer(DataRow row)
        {
            return new Customer
            {
                CustomerId = Convert.ToInt32(row["CustomerId"]),
                IdentityNumber = row["IdentityNumber"].ToString(),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                BirthDate = row["BirthDate"] != DBNull.Value ? Convert.ToDateTime(row["BirthDate"]) : null,
                Gender = row["Gender"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                BranchId = row["BranchId"] != DBNull.Value ? Convert.ToInt32(row["BranchId"]) : null,
                BranchName = row["BranchName"].ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : null,
                Photo = row["Photo"] != DBNull.Value ? (byte[])row["Photo"] : null,
                Signature = row["Signature"] != DBNull.Value ? (byte[])row["Signature"] : null
            };
        }
    }
}
