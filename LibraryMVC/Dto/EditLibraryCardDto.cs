namespace LibraryMVC.Dto;

public class EditLibraryCardDto
{
    public int LibraryCardId { get; set; }
    
    public string? CardNumber { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime? ExpiryDate { get; set; }
    
    public int UserId { get; set; }
}