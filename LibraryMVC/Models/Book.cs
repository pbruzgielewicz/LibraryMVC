using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models;

public class Book
{    
    public int BookId { get; set; }

    [Required]
    public string? Title { get; set; }

    [Display(Name = "Author")]
    public int AuthorId { get; set; }

    // [Display(Name = "Genre")]
   // Właściwość nawigacyjna do autora książki
    public Author Author { get; set; }

    // Właściwość nawigacyjna do wypożyczeń (Loan)
    public ICollection<Loan> Loans { get; set; }
    
}