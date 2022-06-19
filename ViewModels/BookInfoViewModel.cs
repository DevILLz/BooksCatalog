using BooksCatalog.Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BooksCatalog.ViewModels
{
    public class BookInfoViewModel : INPC
    {
        #region fields
        private BookDTO _selectedBook;
        #endregion

        #region Props
        public BookDTO SelectedBook { get => _selectedBook; set => SetProperty(ref _selectedBook, value); }

        public ICommand EditCommand { get; set; }
        #endregion
        public BookInfoViewModel()
        {
            Data.OnSelectedBookChanged += (b) => SelectedBook = b != null ? new BookDTO(b) : null;
            EditCommand = new Command(Edit);
        }

        private async void Edit(object obj) => await Data.RequestChangeView("edit");
    }
}
