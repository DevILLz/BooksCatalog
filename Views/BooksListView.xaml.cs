using BooksCatalog.ViewModels;
using System.Windows.Controls;

namespace BooksCatalog.Views
{
    /// <summary>
    /// Interaction logic for BooksListView.xaml
    /// </summary>
    public partial class BooksListView : UserControl
    {
        public BooksListView()
        {
            InitializeComponent();
            this.DataContext = new BookListViewModel();
        }
    }
}
