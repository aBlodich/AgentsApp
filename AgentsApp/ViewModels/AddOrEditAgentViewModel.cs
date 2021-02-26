using AgentsApp.Commands;
using AgentsApp.Database;
using AgentsApp.Models;
using AgentsApp.Services;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Controls;

namespace AgentsApp.ViewModels
{
    /// <summary>
    /// Модель представления добавления или редактирования агентво
    /// </summary>
    class AddOrEditAgentViewModel : BaseViewModel
    {

        private string _name;
        private string _contactNumber;
        private string _email;
        private StorageFile _photo;
        private string _photoNameTextBoxText = "Фотография не выбрана";
        private AddOrEditAgentModel addOrEditAgentModel = new AddOrEditAgentModel();

        public string NameTextBoxText
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public string ContactNumberTextBoxText
        {
            get => _contactNumber;
            set { _contactNumber = value; OnPropertyChanged(); }
        }
        public string EmailTextBoxText
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public Agent Agent { get; set; } = null;

        public bool IsEdit { get; set; } = false;

        public string PhotoNameTextBoxText
        {
            get => _photoNameTextBoxText;
            set
            {
                this._photoNameTextBoxText = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveButton_Clicked => new DelegateCommand(OnSaveButton_ClickedAsync);

        public ICommand AddAPhotoButton_Clicked => new DelegateCommand(OpenPhotoAsync);

        public ICommand CancelButton_Clicked => new DelegateCommand(Navigation.GoToMainPage);

        /// <summary>
        /// Сохранение введенных данных 
        /// </summary>
        private void OnSaveButton_ClickedAsync()
        {
            addOrEditAgentModel.SaveDataToDataBaseAsync(Agent,NameTextBoxText,ContactNumberTextBoxText,EmailTextBoxText,IsEdit);
        }

        /// <summary>
        /// Открывает фото и записывает имя фото в окно
        /// </summary>
        private async void OpenPhotoAsync()
        {
            _photo = await FileService.OpenPhotoAsync();
            PhotoNameTextBoxText = _photo.Name;
            addOrEditAgentModel.Token = StorageApplicationPermissions.FutureAccessList.Add(_photo);
        }

        /// <summary>
        /// Проверяет валидность введенной строки в поле с именем.
        /// Не дает не вводить ничего кроме букв.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            string str = tb.Text;
            string pattern = @"[\d\!\@\""\#\№\$\;\%\^\:\&\?\*\(\)\-_\+\=\\\|\/\'\.\,\~\`\<\>\[\]\{\}]";
            str = Regex.Replace(str, pattern, "");
            tb.Text = str;
            //tb.Select(str.Length, str.Length);
        }

        /// <summary>
        /// Проверяет валидность номера.
        /// Не дает вводить ничего кроме цифр.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ContactNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            string str = tb.Text;
            string pattern = @"[\D]";
            str = Regex.Replace(str, pattern, "");
            tb.Text = str;
            //tb.Select(str.Length, str.Length);
        }
    }
}