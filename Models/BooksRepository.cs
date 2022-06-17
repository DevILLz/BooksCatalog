using Domain;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BooksCatalog.Models
{
    public class BooksRepository : IBooksRepository
    {
        private DataContext context;
        public BooksRepository()
        {
            context = new DataContext();

            if (!context.Books.Any())
                Seed();
        }

        public async Task<IEnumerable<Book>> GetList() => await context.Books.ToListAsync();
        public async Task<Book> GetBook(int id) => await context.Books.FirstOrDefaultAsync(x => x.Id == id);
        public async Task<Book> AddBook(Book book)
        {
            var b = context.Books.Add(book);
            await context.SaveChangesAsync();
            return b;
        }

        public async Task<Book> EditBook(int id, Book book)
        {
            var b = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
            // здесь должен быть автомаппер
            b.Name = book.Name;
            b.Author = book.Author;
            b.Cover = book.Cover;
            b.Created = book.Created;
            b.ISBN = book.ISBN;
            b.Description = book.Description;
            await context.SaveChangesAsync();
            return b;
        }
        public async Task<bool> DeleteBook(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
            context.Books.Remove(book);
            return await context.SaveChangesAsync() == 0;
        }

        private void Seed()
        {
            //var a = new BitmapImage(new Uri("image.png"));
            var r = new Random();
            var date = DateTime.Parse("1/1/1999");
            var books = new Book[10];
            for (int i = 0; i < books.Length; i++)
                books[i] = new Book
                {
                    Name = $"Book {i}",
                    Author = $"Author {i}",
                    Created = date.AddDays(r.Next(1, (i + 1) * 500)),
                    ISBN = long.Parse($"{r.Next(500_000_000, 999_999_999)}{r.Next(100, 999)}"),
                    Description = "Unknown",
                    Cover = null
                };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
