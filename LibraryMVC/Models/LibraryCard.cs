using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models;

public class LibraryCard
{
    public int LibraryCardId { get; set; }

    [Required]
    [Display(Name = "Card Number")]
    public string? CardNumber { get; set; }

    [Required]
    [Display(Name = "Issued At")]
    public DateTime IssuedAt { get; set; }

    [Display(Name = "Expiry Date")]
    public DateTime? ExpiryDate { get; set; }

    // Klucz obcy dla relacji z użytkownikiem (User)
    public int UserId { get; set; }

    // Właściwość nawigacyjna do użytkownika
    public User User { get; set; }
}