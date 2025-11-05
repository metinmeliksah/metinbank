namespace MetinBank.Core.Entities.Payment;

/// <summary>
/// Bildirim
/// </summary>
public class Notification : BaseEntity
{
    /// <summary>
    /// Alıcı kullanıcı ID
    /// </summary>
    public Guid RecipientId { get; set; }
    
    /// <summary>
    /// Bildirim tipi (LOGIN/TRANSACTION/SECURITY/APPROVAL)
    /// </summary>
    public string NotificationType { get; set; } = string.Empty;
    
    /// <summary>
    /// Başlık
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Mesaj
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Kanallar (PUSH/SMS/EMAIL) - JSON array
    /// </summary>
    public string Channels { get; set; } = string.Empty;
    
    /// <summary>
    /// Öncelik (LOW/MEDIUM/HIGH/CRITICAL)
    /// </summary>
    public string Priority { get; set; } = "MEDIUM";
    
    /// <summary>
    /// Okundu mu?
    /// </summary>
    public bool IsRead { get; set; }
    
    /// <summary>
    /// Gönderildi mi?
    /// </summary>
    public bool IsSent { get; set; }
    
    /// <summary>
    /// Gönderilme zamanı
    /// </summary>
    public DateTime? SentAt { get; set; }
    
    /// <summary>
    /// Okunma zamanı
    /// </summary>
    public DateTime? ReadAt { get; set; }
    
    /// <summary>
    /// Ekstra data (JSON)
    /// </summary>
    public string? ExtraData { get; set; }
}


