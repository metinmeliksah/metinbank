namespace MetinBank.Core.Entities.Payment;

/// <summary>
/// Dekont ve belge yönetimi
/// </summary>
public class Document : BaseEntity
{
    /// <summary>
    /// Referans ID (Transaction, Customer, vb.)
    /// </summary>
    public Guid ReferenceId { get; set; }
    
    /// <summary>
    /// Referans tipi (TRANSACTION/CUSTOMER/LOAN/EKYC)
    /// </summary>
    public string ReferenceType { get; set; } = string.Empty;
    
    /// <summary>
    /// Dosya adı
    /// </summary>
    public string FileName { get; set; } = string.Empty;
    
    /// <summary>
    /// Dosya yolu (NFS/S3)
    /// </summary>
    public string FilePath { get; set; } = string.Empty;
    
    /// <summary>
    /// Dosya tipi (PDF/JPEG/PNG)
    /// </summary>
    public string FileType { get; set; } = string.Empty;
    
    /// <summary>
    /// Dosya boyutu (byte)
    /// </summary>
    public long FileSize { get; set; }
    
    /// <summary>
    /// Belge tipi (RECEIPT/CONTRACT/IDENTITY/STATEMENT)
    /// </summary>
    public string DocumentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Yükleyen kullanıcı ID
    /// </summary>
    public Guid? UploadedBy { get; set; }
    
    /// <summary>
    /// Yüklenme zamanı
    /// </summary>
    public DateTime UploadedAt { get; set; }

    public Document()
    {
        UploadedAt = DateTime.UtcNow;
    }
}


