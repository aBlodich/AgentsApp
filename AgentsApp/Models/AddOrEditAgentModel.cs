using AgentsApp.Commands;
using AgentsApp.Database;
using System;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;

namespace AgentsApp.Models
{
    /// <summary>
    /// Модель добавления или редактирования агентов
    /// </summary>
    class AddOrEditAgentModel
    {
        public string Token { get; private set; } = null;
        public string PhotoName { get; private set; } = null;

        private string photoPath = null;

        public async Task OpenFile()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            var photo = await picker.PickSingleFileAsync();
            if (photo != null)
            {
                photoPath = photo.Path;
                PhotoName = photo.Name;
                Token = StorageApplicationPermissions.FutureAccessList.Add(photo);
            }
        }

        public async void OnSaveButton_ClickedAsync(AgentModel agent, string name, string contactNumber, string email, bool isEdit)
        {
            using (var db = new AgentContext())
            {
                if (agent == null)
                {
                    agent = new AgentModel()
                    {
                        Name = name,
                        ContactNumber = contactNumber,
                        Email = email
                    };
                }
                else
                {
                    agent.Name = name;
                    agent.ContactNumber = contactNumber;
                    agent.Email = email;
                }
                if (Token != null)
                {
                    agent.ImagePath = photoPath;
                    agent.ImageToken = Token;
                }
                if (isEdit)
                {
                    db.Update(agent);
                }
                else
                {
                    await db.AddAsync(agent);
                }
                db.SaveChanges();
            }
            Navigation.GoToMainPage();
        }
    }
}