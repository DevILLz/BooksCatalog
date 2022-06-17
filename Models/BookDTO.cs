using BooksCatalog.ViewModels;
using Domain;
using System;
using System.Windows.Media.Imaging;

namespace BooksCatalog.Models
{
    public class BookDTO : INPC
    {
        private int id;
        private string name;
        private string author;
        private long iSBN;
        private DateTime created;
        private string description;
        private byte[] cover;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Author { get => author; set => SetProperty(ref author, value); }
        public DateTime Created { get => created; set => SetProperty(ref created, value); }
        public long ISBN { get => iSBN; set => SetProperty(ref iSBN, value); }
        public byte[] Cover { get => cover; set { cover = value; RaisePropertyChanged(nameof(Image)); } }
        public string Description { get => description; set => description = value; }
        public BitmapImage Image => Data.ToImage(Cover);
        public BookDTO(Book book)
        {
            Id = book.Id;
            Name = book.Name;
            Author = book.Author;
            Created = book.Created;
            ISBN = book.ISBN;
            Cover = book.Cover;
            Description = book.Description;
        }
        public BookDTO(BookDTO book)
        {
            Id = book.Id;
            Name = book.Name;
            Author = book.Author;
            Created = book.Created;
            ISBN = book.ISBN;
            Cover = book.Cover;
            Description = book.Description;
        }
        public BookDTO() { }
    }
}
