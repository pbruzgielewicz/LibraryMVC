using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models;

public class User
{
    public int UserId { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public LibraryCard LibraryCard { get; set; }
}