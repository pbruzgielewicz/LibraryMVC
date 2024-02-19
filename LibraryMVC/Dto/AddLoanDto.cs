namespace LibraryMVC.Dto;

public class AddLoanDto
{
    public int BookId { get; set; }

    public int LibraryCardId { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }
}