using LibraryManangerAPI.Enums;

namespace LibraryManangerAPI.Communication.Requests;

public class CreateBookDTO
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public BookGenderEnum Gender { get; set; }
    public double Amount { get; set; }

    public int Qtde { get; set; }
}
