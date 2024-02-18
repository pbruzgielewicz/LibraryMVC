namespace LibraryMVC.Dto;

public class EditAuthorDto
{
    public int AuthorId { get; set; }
    
    public string? Name { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}