using LibraryManangerAPI.Communication.Requests;
using LibraryManangerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManangerAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private static List<BookModel> books =
    [
        new() { Title = "Variety", Id = 1, Gender = Enums.BookGenderEnum.MISTERY, Author = "Ellen", Amount = 70.00, Qtde = 10 },
        new() { Title = "Código Limpo", Id = 2, Gender = Enums.BookGenderEnum.ADVENTURE, Author = "Robert Martin", Amount = 100.00, Qtde = 5 },
    ];

    [HttpPost]
    [ProducesResponseType(typeof(BookModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateBookDTO bookDTO)
    {
        bool booksAlreadyExists = books.Where(book => book.Title == bookDTO.Title).Any();

        if (booksAlreadyExists) return BadRequest("Já existe um livro com esse nome.");

        int newBookId = books.Count > 0 ? books.Max(x => x.Id) + 1 : 1;

        var bookToCreate = new BookModel { 
            Id = newBookId,
            Title = bookDTO.Title,
            Author = bookDTO.Author,
            Amount = bookDTO.Amount,
            Qtde = bookDTO.Qtde,
            Gender = bookDTO.Gender
        };

        books.Add(bookToCreate);

        return Ok(bookDTO);
    }


    [HttpGet]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    public IActionResult GetAllBooks() {  return Ok(books); }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteBook([FromRoute] int id) { 
        var bookFound = books.Where(book => book.Id == id).FirstOrDefault();

        if (bookFound == null) return NotFound("Livro não encontrado.");

        books.Remove(bookFound);

        return Ok();
    }


    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateBook([FromRoute] int id, [FromBody] UpdateBookDTO newBookData)
    {
        var bookFound = books.Where(book => book.Id == id).FirstOrDefault();
        if (bookFound == null) return NotFound();

        bookFound.Title = newBookData.Title;
        bookFound.Author = newBookData.Author;
        bookFound.Amount = newBookData.Amount;
        bookFound.Qtde = newBookData.Qtde;
        bookFound.Gender = newBookData.Gender;

        return Ok(bookFound);
    }


}
