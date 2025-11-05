using MetinBank.Core.Enums;

namespace MetinBank.Core.Entities.Card;

/// <summary>
/// Banka/Kredi Kartı
/// </summary>
public class Card : BaseEntity
{
    /// <summary>
    /// Kart numarası (Token - gerçek PAN değil)
    /// </summary>
    public string CardToken { get; set; } = string.Empty;
    
    /// <summary>
    /// Kart numarasının son 4 hanesi (Görüntüleme için)
    /// </summary>
    public string LastFourDigits { get; set; } = string.Empty;
    
    /// <summary>
    /// Kart tipi
    /// </summary>
    public CardType CardType { get; set; }
    
    /// <summary>
    /// Kart durumu
    /// </summary>
    public CardStatus Status { get; set; }
    
    /// <summary>
    /// Müşteri ID
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual Customer.Customer Customer { get; set; } = null!;
    
    /// <summary>
    /// Bağlı hesap ID (Banka kartı için)
    /// </summary>
    public Guid? AccountId { get; set; }
    
    /// <summary>
    /// Bağlı hesap
    /// </summary>
    public virtual Account.Account? Account { get; set; }
    
    /// <summary>
    /// Kart sahibi adı
    /// </summary>
    public string CardHolderName { get; set; } = string.Empty;
    
    /// <summary>
    /// Son kullanma tarihi
    /// </summary>
    public DateTime ExpiryDate { get; set; }
    
    /// <summary>
    /// CVV (Şifrelenmiş)
    /// </summary>
    public string CvvHash { get; set; } = string.Empty;
    
    /// <summary>
    /// PIN (Şifrelenmiş)
    /// </summary>
    public string PinHash { get; set; } = string.Empty;
    
    /// <summary>
    /// Günlük limit
    /// </summary>
    public decimal DailyLimit { get; set; }
    
    /// <summary>
    /// Bugün kullanılan tutar
    /// </summary>
    public decimal TodayUsed { get; set; }
    
    /// <summary>
    /// E-ticaret izni
    /// </summary>
    public bool AllowECommerce { get; set; } = true;
    
    /// <summary>
    /// POS izni
    /// </summary>
    public bool AllowPOS { get; set; } = true;
    
    /// <summary>
    /// ATM izni
    /// </summary>
    public bool AllowATM { get; set; } = true;
    
    /// <summary>
    /// Yurtdışı kullanım izni
    /// </summary>
    public bool AllowInternational { get; set; } = false;
    
    /// <summary>
    /// Temassız ödeme izni
    /// </summary>
    public bool AllowContactless { get; set; } = true;
    
    /// <summary>
    /// Kredi kartı bilgileri (Kredi kartı ise)
    /// </summary>
    public virtual CreditCardInfo? CreditCardInfo { get; set; }
    
    /// <summary>
    /// Kart işlemleri
    /// </summary>
    public virtual ICollection<Transaction.Transaction> Transactions { get; set; } = new List<Transaction.Transaction>();

    public Card()
    {
        Status = CardStatus.Active;
    }
}


