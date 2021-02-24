using AgentsApp.Commands;
using AgentsApp.Models;
using System.Windows.Input;

namespace AgentsApp.ViewModels
{
    /// <summary>
    /// Модель представления добавления или редактирования агентво
    /// </summary>
    class AddOrEditAgentViewModel : BaseViewModel
    {
        public string NameTextBoxText { get; set; } = string.Empty;
        public string ContactNumberTextBoxText { get; set; } = string.Empty;
        public string EmailTextBoxText { get; set; } = string.Empty;

        private string photoNameTextBoxText = "Фотография не выбрана";

        AddOrEditAgentModel addOrEditAgentModel = new AddOrEditAgentModel();

        public AgentModel Agent { get; set; }

        public bool IsEdit { get; set; } = false;

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

        public ICommand SaveButton_Clicked => new DelegateCommand(OnSaveButton_ClickedAsync);

        public ICommand AddAPhotoButton_Clicked => new DelegateCommand(OpenFile);

        public ICommand CancelButton_Clicked => new DelegateCommand(Navigation.GoToMainPage);

        private void OnSaveButton_ClickedAsync()
        {
            addOrEditAgentModel.OnSaveButton_ClickedAsync(Agent,NameTextBoxText,ContactNumberTextBoxText,EmailTextBoxText,IsEdit);
        }

        private async void OpenFile()
        {
            await addOrEditAgentModel.OpenFile();
            if (addOrEditAgentModel.PhotoName != null)
            {
                PhotoNameTextBoxText = addOrEditAgentModel.PhotoName;
            }
        }

    }
}