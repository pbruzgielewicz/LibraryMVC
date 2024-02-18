using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models;

public class Loan
{
    public int LoanId { get; set; }

    [Required]
    [Display(Name = "Book")]
    public int BookId { get; set; }

    [Required]
    [Display(Name = "Library Card")]
    public int LibraryCardId { get; set; }

    [Required]
    [Display(Name = "Loan Date")]
    public DateTime LoanDate { get; set; }

    [Required]
    [Display(Name = "Due Date")]
    public DateTime DueDate { get; set; }

    [Display(Name = "Return Date")]
    public DateTime? ReturnDate { get; set; }

    // Właściwość nawigacyjna do książki
    public Book Book { get; set; }

    // Właściwość nawigacyjna do karty bibliotecznej
    public LibraryCard LibraryCard { get; set; }
}