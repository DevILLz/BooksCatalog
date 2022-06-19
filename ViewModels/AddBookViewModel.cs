using BooksCatalog.Models;
using Domain;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BooksCatalog.ViewModels
{
    public class AddBookViewModel : INPC
    {
        private bool IsChangingBook;
        private BookDTO selectedBook;

        private string _coverImageFilePath;
        private string _name;
        private string _author;
        private DateTime _release;
        private long isbn;
        private string _description;
        private BitmapImage _image;

        public string CoverImageFilePath { get => _coverImageFilePath; set => SetProperty(ref _coverImageFilePath, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Author { get => _author; set => SetProperty(ref _author, value); }
        public DateTime Release { get => _release; set => SetProperty(ref _release, value); }
        public long ISBN { get => isbn; set => SetProperty(ref isbn, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public BitmapImage Image { get => _image; set => SetProperty(ref _image, value); }

        public ICommand GetFileCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public AddBookViewModel(BookDTO selectedBook = null)
        {
            if (selectedBook != null)
            {
                this.selectedBook = selectedBook;
                IsChangingBook = true;
                Name = selectedBook.Name;
                Author = selectedBook.Author;
                Release = selectedBook.Created;
                Description = selectedBook.Description;
                Image = Data.ToImage(selectedBook.Cover);
            }
            GetFileCommand = new Command(GetFile);
            AcceptCommand = new Command(Accept);
            CancelCommand = new Command(Cancel);
        }

        private async void Accept(object obj)
        {
            var book = new Book
            {
                Name = Name,
                Author = Author,
                Created = Release,
                ISBN = ISBN,
                Description = Description,
                Cover = Data.ToBytes(Image)
            };

            var repos = new BooksRepository();

            if (IsChangingBook)
                book = await repos.EditBook(selectedBook.Id, book);
            else
                book = await repos.AddBook(book);
            await Data.RequestChangeView("default");
            await Data.AddedOrEdited(book);
            await Data.ChangeSelectedBook(new BookDTO(book));
        }
        private async void Cancel(object obj) => await Data.RequestChangeView("default");
        private void GetFile(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                CoverImageFilePath = openFileDialog.FileName;
                Image = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
