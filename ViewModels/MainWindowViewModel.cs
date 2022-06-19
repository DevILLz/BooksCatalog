using BooksCatalog.Models;
using BooksCatalog.Views;
using System.Threading;
using System.Windows.Controls;

namespace BooksCatalog.ViewModels
{
    public class MainWindowViewModel : INPC
    {
        #region fields
        private UserControl _extraView;
        private BookDTO selectedBook;
        private SynchronizationContext context;

        #endregion
        #region Props
        public UserControl ExtraView { get => _extraView; set => SetProperty(ref _extraView, value); }

        #endregion
        public MainWindowViewModel()
        {
            context = SynchronizationContext.Current;
            Data.OnViewChangeRequest += Data_OnViewChangRequest;
            Data.OnSelectedBookChanged += Data_OnSelectedBookChanged;
            Data_OnViewChangRequest("default");
        }

        private void Data_OnSelectedBookChanged(BookDTO book) => this.selectedBook = book;

        private void Data_OnViewChangRequest(string request)
        {
            context.Post(v =>
            { // создавать UC нужно из потока STA
                switch (request.ToLower())
                {
                    case "add":
                        ExtraView = new AddBookView();
                        break;
                    case "edit":
                        ExtraView = new AddBookView(selectedBook);
                        break;
                    case "info":
                    default:
                        ExtraView = new BookInfoView();
                        break;
                }
            }, null);
        }
    }
}
