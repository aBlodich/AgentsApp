using AgentsApp.Commands;
using AgentsApp.DataBase;
using AgentsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace AgentsApp.ViewModels
{
    class AddOrEditAgentViewModel : BaseViewModel
    {
        public string NameTextBoxText { get; set; } = string.Empty;
        public string ContactNumberTextBoxText { get; set; } = string.Empty;
        public string EmailTextBoxText { get; set; } = string.Empty;

        private string photoNameTextBoxText = "Фотография не выбрана";

        private string token = null;

        public string PhotoNameTextBoxText
        {
            get
            {
                return photoNameTextBoxText;
            }
            set
            {
                this.photoNameTextBoxText = value;
                OnPropertyChanged();
            }
        }

        private StorageFile photo;

        public Agent Agent { get; set; }

        public bool IsEdit { get; set; } = false;

        public ICommand SaveButton_Clicked
        {
            get => new DelegateCommand(OnSaveButton_ClickedAsync);
        }

        public ICommand AddAPhotoButton_Clicked
        {
            get => new DelegateCommand(OpenFile);
        }

        public ICommand CancelButton_Clicked
        {
            get => new DelegateCommand(Navigation.GoToMainPage);
        }

        public async void OnSaveButton_ClickedAsync()
        {
            using (var db = new AgentContext())
            {
                if (Agent == null)
                {
                    Agent = new Agent()
                    {
                        Name = NameTextBoxText,
                        ContactNumber = ContactNumberTextBoxText,
                        Email = EmailTextBoxText
                    };
                }
                else
                {
                    Agent.Name = NameTextBoxText;
                    Agent.ContactNumber = ContactNumberTextBoxText;
                    Agent.Email = EmailTextBoxText;
                }
                Agent.ImagePath = photo != null ? photo.Path : null;
                Agent.ImageToken = token;
                if (IsEdit)
                {
                    db.Update(Agent);
                }
                else
                {
                    await db.AddAsync(Agent);
                }
                db.SaveChanges();
            }
            Navigation.GoToMainPage();
        }

        private async void OpenFile()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            photo = await picker.PickSingleFileAsync();
            if (photo != null)
            {
                this.PhotoNameTextBoxText = photo.Name;
                token = StorageApplicationPermissions.FutureAccessList.Add(photo);
            }
        }
    }
}
