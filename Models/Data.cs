using Domain;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace BooksCatalog.Models
{
    public static class Data
    {
        public static event Action<BookDTO> OnSelectedBookChanged;
        public static event Action<Book> OnAddOrEditedBook;
        public static event Action<string> OnViewChangeRequest;

        public static void ChangeSelectedBook(BookDTO book) => OnSelectedBookChanged?.Invoke(book);
        public static void AddedOrEdited(Book book) => OnAddOrEditedBook?.Invoke(book);
        public static void RequestChangeView(string request) => OnViewChangeRequest?.Invoke(request);

        public static BitmapImage ToImage(byte[] array)
        {
            if (array == null) return null;
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        public static byte[] ToBytes(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
    }
}
