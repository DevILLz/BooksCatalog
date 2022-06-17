using BooksCatalog.ViewModels;
using System.Windows.Controls;

namespace BooksCatalog.Views
{
    /// <summary>
    /// Interaction logic for BookInfoView.xaml
    /// </summary>
    public partial class BookInfoView : UserControl
    {
        public BookInfoView()
        {
            InitializeComponent();
            this.DataContext = new BookInfoViewModel();
        }
    }
}
