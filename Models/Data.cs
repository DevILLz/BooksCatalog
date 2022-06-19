using Domain;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BooksCatalog.Models
{
    public static class Data
    {
        public static event Action<BookDTO> OnSelectedBookChanged;
        public static event Action<Book> OnAddOrEditedBook;
        public static event Action<string> OnViewChangeRequest;

        public static async Task ChangeSelectedBook(BookDTO book) => await Task.Factory.StartNew(() => OnSelectedBookChanged?.Invoke(book));
        public static async Task AddedOrEdited(Book book) => await Task.Factory.StartNew(() => OnAddOrEditedBook?.Invoke(book));
        public static async Task RequestChangeView(string request) => await Task.Factory.StartNew(() => OnViewChangeRequest?.Invoke(request));

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
        public static byte[] ToBytes(Bitmap image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }
    }
}
