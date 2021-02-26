using AgentsApp.Constants;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Media.Imaging;

namespace AgentsApp.Services
{
    /// <summary>
    /// Содержит работу с файлами
    /// </summary>
    class FileService
    {
        /// <summary>
        /// Возвращает фотографию по токену 
        /// </summary>
        /// <param name="imageToken">Токен файла</param>
        /// <returns></returns>
        public static async Task<BitmapImage> GetPhoto(string imageToken)
        {
            var bmi = new BitmapImage();

            if (imageToken == null) return bmi = new BitmapImage(new Uri(StringConstants.PLACEHOLDERPATH));

            try
            {
                StorageFile file = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(imageToken);
                var fileStream = await file.OpenAsync(FileAccessMode.Read);
                await bmi.SetSourceAsync(fileStream);
                fileStream.Dispose();
                return bmi;
            }
            catch(Exception)
            {
                Dialogs.ShowErrorFileExicting();
                return bmi = bmi = new BitmapImage(new Uri(StringConstants.PLACEHOLDERPATH));
            }
        }

        /// <summary>
        /// Возвращает выбранный пользователем файл
        /// </summary>
        /// <returns></returns>
        public static async Task<StorageFile> OpenPhotoAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            var photo = await picker.PickSingleFileAsync();
            return photo;
        }
    }
}