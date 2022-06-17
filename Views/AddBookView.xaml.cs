using BooksCatalog.Models;
using BooksCatalog.ViewModels;
using System.Windows.Controls;

namespace BooksCatalog.Views
{
    /// <summary>
    /// Interaction logic for AddBookView.xaml
    /// </summary>
    public partial class AddBookView : UserControl
    {
        public AddBookView(BookDTO selectedBook = null)
        {
            InitializeComponent();
            this.DataContext = new AddBookViewModel(selectedBook);
        }
    }
}
