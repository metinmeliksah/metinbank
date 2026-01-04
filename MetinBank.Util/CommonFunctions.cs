using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace MetinBank.Util
{
    /// <summary>
    /// Ortak kullanılan yardımcı fonksiyonlar
    /// </summary>
    public static class CommonFunctions
    {
        /// <summary>
        /// Sistem IP adresini döndürür
        /// </summary>
        /// <returns>IP adresi</returns>
        public static string GetLocalIPAddress()
        {
            try
            {
                string hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);
                
                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// MAC adresini döndürür
        /// </summary>
        /// <returns>MAC adresi</returns>
        public static string GetMacAddress()
        {
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        PhysicalAddress address = nic.GetPhysicalAddress();
                        byte[] bytes = address.GetAddressBytes();
                        
                        if (bytes.Length > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < bytes.Length; i++)
                            {
                                sb.Append(bytes[i].ToString("X2"));
                                if (i != bytes.Length - 1)
                                    sb.Append("-");
                            }
                            return sb.ToString();
                        }
                    }
                }
                return "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Unique işlem referans numarası üretir (TRX+timestamp)
        /// </summary>
        /// <returns>İşlem referans numarası</returns>
        public static string GenerateTransactionReference()
        {
            return $"TRX{DateTime.Now:yyyyMMddHHmmssfff}{new Random().Next(100, 999)}";
        }

        /// <summary>
        /// Session ID üretir
        /// </summary>
        /// <returns>Session ID</returns>
        public static string GenerateSessionId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Para formatı (2 ondalık basamak)
        /// </summary>
        /// <param name="tutar">Tutar</param>
        /// <returns>Formatlanmış tutar</returns>
        public static string FormatCurrency(decimal tutar)
        {
            return tutar.ToString("N2") + " TL";
        }

        /// <summary>
        /// Para formatı (para birimi ile)
        /// </summary>
        /// <param name="tutar">Tutar</param>
        /// <param name="paraBirimi">Para birimi (TL, USD, EUR)</param>
        /// <returns>Formatlanmış tutar</returns>
        public static string FormatCurrency(decimal tutar, string paraBirimi)
        {
            return tutar.ToString("N2") + " " + paraBirimi;
        }

        /// <summary>
        /// Tarih formatı (dd.MM.yyyy HH:mm:ss)
        /// </summary>
        /// <param name="tarih">Tarih</param>
        /// <returns>Formatlanmış tarih</returns>
        public static string FormatDateTime(DateTime tarih)
        {
            return tarih.ToString("dd.MM.yyyy HH:mm:ss");
        }

        /// <summary>
        /// Tarih formatı (dd.MM.yyyy)
        /// </summary>
        /// <param name="tarih">Tarih</param>
        /// <returns>Formatlanmış tarih</returns>
        public static string FormatDate(DateTime tarih)
        {
            return tarih.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// TCKN maskeleme (***45678**)
        /// </summary>
        /// <param name="tckn">TCKN</param>
        /// <returns>Maskelenmiş TCKN</returns>
        public static string MaskTCKN(string tckn)
        {
            if (string.IsNullOrEmpty(tckn) || tckn.Length != 11)
                return tckn;

            return $"***{tckn.Substring(3, 5)}**";
        }

        /// <summary>
        /// Kart numarası maskeleme (**** **** **** 1234)
        /// </summary>
        /// <param name="kartNo">Kart numarası</param>
        /// <returns>Maskelenmiş kart numarası</returns>
        public static string MaskKartNo(string kartNo)
        {
            if (string.IsNullOrEmpty(kartNo) || kartNo.Length != 16)
                return kartNo;

            return $"**** **** **** {kartNo.Substring(12, 4)}";
        }

        /// <summary>
        /// Telefon maskeleme (0555***4567)
        /// </summary>
        /// <param name="telefon">Telefon</param>
        /// <returns>Maskelenmiş telefon</returns>
        public static string MaskTelefon(string telefon)
        {
            if (string.IsNullOrEmpty(telefon) || telefon.Length < 7)
                return telefon;

            int maskLength = telefon.Length - 7;
            return telefon.Substring(0, 4) + new string('*', maskLength) + telefon.Substring(telefon.Length - 3);
        }

        /// <summary>
        /// Random müşteri numarası üretir (M000000001 formatında)
        /// </summary>
        /// <param name="sonMusteriNo">Son müşteri numarası</param>
        /// <returns>Yeni müşteri numarası</returns>
        public static string GenerateMusteriNo(long sonMusteriNo)
        {
            long yeniNo = sonMusteriNo + 1;
            return $"M{yeniNo:D9}";
        }

        /// <summary>
        /// İş günü kontrolü (Hafta sonu değil mi)
        /// </summary>
        /// <param name="tarih">Kontrol edilecek tarih</param>
        /// <returns>İş günü ise true</returns>
        public static bool IsBusinessDay(DateTime tarih)
        {
            return tarih.DayOfWeek != DayOfWeek.Saturday && tarih.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// Sonraki iş gününü döndürür
        /// </summary>
        /// <param name="tarih">Başlangıç tarihi</param>
        /// <param name="isGunuSayisi">Kaç iş günü sonra</param>
        /// <returns>Hesaplanan tarih</returns>
        public static DateTime AddBusinessDays(DateTime tarih, int isGunuSayisi)
        {
            int addedDays = 0;
            while (addedDays < isGunuSayisi)
            {
                tarih = tarih.AddDays(1);
                if (IsBusinessDay(tarih))
                    addedDays++;
            }
            return tarih;
        }

        /// <summary>
        /// String'i güvenli şekilde int'e çevirir
        /// </summary>
        /// <param name="value">String değer</param>
        /// <param name="defaultValue">Varsayılan değer</param>
        /// <returns>Integer değer</returns>
        public static int SafeParseInt(string value, int defaultValue = 0)
        {
            int result;
            return int.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// String'i güvenli şekilde decimal'e çevirir
        /// </summary>
        /// <param name="value">String değer</param>
        /// <param name="defaultValue">Varsayılan değer</param>
        /// <returns>Decimal değer</returns>
        public static decimal SafeParseDecimal(string value, decimal defaultValue = 0)
        {
            decimal result;
            return decimal.TryParse(value, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Null string'i boş string'e çevirir
        /// </summary>
        /// <param name="value">String değer</param>
        /// <returns>Boş string veya değer</returns>
        public static string NullToEmpty(string value)
        {
            return value ?? string.Empty;
        }

        /// <summary>
        /// DBNull'ı string'e çevirir
        /// </summary>
        /// <param name="value">Veritabanı değeri</param>
        /// <returns>String değer</returns>
        public static string DbNullToString(object value)
        {
            return value == DBNull.Value ? string.Empty : value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// DBNull'ı int'e çevirir
        /// </summary>
        /// <param name="value">Veritabanı değeri</param>
        /// <param name="defaultValue">Varsayılan değer</param>
        /// <returns>Integer değer</returns>
        public static int DbNullToInt(object value, int defaultValue = 0)
        {
            if (value == DBNull.Value || value == null)
                return defaultValue;
            
            int result;
            return int.TryParse(value.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// DBNull'ı decimal'e çevirir
        /// </summary>
        /// <param name="value">Veritabanı değeri</param>
        /// <param name="defaultValue">Varsayılan değer</param>
        /// <returns>Decimal değer</returns>
        public static decimal DbNullToDecimal(object value, decimal defaultValue = 0)
        {
            if (value == DBNull.Value || value == null)
                return defaultValue;
            
            decimal result;
            return decimal.TryParse(value.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// DBNull'ı DateTime'a çevirir
        /// </summary>
        /// <param name="value">Veritabanı değeri</param>
        /// <returns>DateTime değer veya null</returns>
        public static DateTime? DbNullToDateTime(object value)
        {
            if (value == DBNull.Value || value == null)
                return null;
            
            DateTime result;
            return DateTime.TryParse(value.ToString(), out result) ? (DateTime?)result : null;
        }

        /// <summary>
        /// DBNull'ı bool'a çevirir
        /// </summary>
        /// <param name="value">Veritabanı değeri</param>
        /// <param name="defaultValue">Varsayılan değer</param>
        /// <returns>Boolean değer</returns>
        public static bool DbNullToBool(object value, bool defaultValue = false)
        {
            if (value == DBNull.Value || value == null)
                return defaultValue;
            
            bool result;
            return bool.TryParse(value.ToString(), out result) ? result : defaultValue;
        }

        /// <summary>
        /// Hesap ekstresinde gösterilecek işlem açıklamasını oluşturur
        /// </summary>
        /// <param name="islemTipi">İşlem tipi</param>
        /// <param name="kaynakHedef">Kaynak veya hedef bilgisi</param>
        /// <returns>Açıklama metni</returns>
        public static string GenerateIslemAciklama(string islemTipi, string kaynakHedef)
        {
            return $"{islemTipi} - {kaynakHedef} - {DateTime.Now:dd.MM.yyyy HH:mm}";
        }

        /// <summary>
        /// Hata mesajını kullanıcı dostu hale getirir
        /// </summary>
        /// <param name="teknikHata">Teknik hata mesajı</param>
        /// <returns>Kullanıcı dostu mesaj</returns>
        public static string GetUserFriendlyError(string teknikHata)
        {
            if (string.IsNullOrEmpty(teknikHata))
                return "Bir hata oluştu.";

            if (teknikHata.Contains("Duplicate entry"))
                return "Bu kayıt zaten mevcut.";
            
            if (teknikHata.Contains("foreign key"))
                return "İlişkili kayıtlar bulunduğu için işlem yapılamadı.";
            
            if (teknikHata.Contains("Connection"))
                return "Veritabanı bağlantı hatası. Lütfen sistem yöneticisine başvurun.";

            return "İşlem sırasında bir hata oluştu. Lütfen tekrar deneyin.";
        }

        /// <summary>
        /// DataTable'ı Dictionary listesine çevirir (JSON serialization için)
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List of Dictionary</returns>
        public static System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> DataTableToDictionaryList(System.Data.DataTable dt)
        {
            var list = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
            if (dt == null) return list;

            foreach (System.Data.DataRow row in dt.Rows)
            {
                var dict = new System.Collections.Generic.Dictionary<string, object>();
                foreach (System.Data.DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            return list;
        }
    }
}

