using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models;

public class Author
{
    public int AuthorId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    // Właściwość nawigacyjna do relacji z książkami (Book)
    public ICollection<Book> Books { get; set; }
}