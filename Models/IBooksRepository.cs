using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksCatalog.Models
{
    public interface IBooksRepository
    {
        Task<Book> AddBook(Book book);
        Task<bool> DeleteBook(int id);
        Task<Book> EditBook(int id, Book book);
        Task<Book> GetBook(int id);
        Task<IEnumerable<Book>> GetList();
    }
}