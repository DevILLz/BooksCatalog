using BooksCatalog.Models;
using Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BooksCatalog.ViewModels
{
    public class BookListViewModel : INPC
    {
        #region fields
        private ObservableCollection<BookDTO> _booksList;
        private BookDTO _selectedBook;
        private readonly IBooksRepository repos;

        #endregion
        #region Props
        public ObservableCollection<BookDTO> BooksList { get => _booksList; set => SetProperty(ref _booksList, value); }
        public BookDTO SelectedItem
        {
            get => _selectedBook;
            set
            {
                if (SetProperty(ref _selectedBook, value))
                    Data.ChangeSelectedBook(value);
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion
        public BookListViewModel()
        {
            repos = new BooksRepository();
            BooksList = new ObservableCollection<BookDTO>(GetBookDTOList(repos.GetList().Result));
            Data.OnAddOrEditedBook += OnAddOrEditedBook;
            #region Commands
            AddCommand = new Command((o) => Data.RequestChangeView("add"));
            DeleteCommand = new Command(Delete);
            #endregion
        }
        private IEnumerable<BookDTO> GetBookDTOList(IEnumerable<Book> list)
        {
            var newList = new List<BookDTO>();
            foreach (var book in list)
                newList.Add(new BookDTO(book));
            return newList;
        }
        private void OnAddOrEditedBook(Book book)
        {
            var localBook = BooksList.FirstOrDefault(b => b.Id == book.Id);
            if (localBook == null)
                BooksList.Add(new BookDTO(book));
            else
            {
                localBook.Name = book.Name;
                localBook.Author = book.Author;
                localBook.Description = book.Description;
                localBook.Created = book.Created;
                localBook.Cover = book.Cover;
                localBook.ISBN = book.ISBN;
                RaisePropertyChanged(localBook, "Image"); // для отображения изменений
            }
            SelectedItem = localBook;
        }

        private async void Delete(object book)
        {
            if (book == null) return;
            var b = book as BookDTO;
            BooksList.Remove(b);
            await repos.DeleteBook(b.Id);
        }
    }
}
