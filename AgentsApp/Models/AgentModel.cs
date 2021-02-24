using AgentsApp.Constants;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Media.Imaging;

namespace AgentsApp.Models
{
    //Модель агентов
    class AgentModel : IComparable<AgentModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public string ImageToken { get; set; }

        public async Task<BitmapImage> GetPhoto()
        {
            var bmi = new BitmapImage();

            if (ImageToken == null) return bmi = new BitmapImage(new Uri(StringConstants.PLACEHOLDERPATH));

            try
            {
                StorageFile file = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(ImageToken);
                var fileStream = await file.OpenAsync(FileAccessMode.Read);
                await bmi.SetSourceAsync(fileStream);
                fileStream.Dispose();
                return bmi;
            }
            catch
            {
                return bmi = bmi = new BitmapImage(new Uri(StringConstants.PLACEHOLDERPATH));
            }
        }

        public int CompareTo(AgentModel other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
