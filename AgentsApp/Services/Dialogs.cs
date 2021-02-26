using System;
using Windows.UI.Xaml.Controls;

namespace AgentsApp.Services
{
    /// <summary>
    /// Содержит диалоговые окна
    /// </summary>
    class Dialogs
    {
        /// <summary>
        /// Показывает дилог при невалидном email на страинце добавления/редактирования email
        /// </summary>
        public static async void ShowEmailDialog()
        {
            var dialog = new ContentDialog()
            {
                Title = "Не корректный Email-адрес",
                Content = "Проверьте введенный Email-адрес",
                CloseButtonText = "Ок"
            };
            await dialog.ShowAsync();
        }

        /// <summary>
        /// Показывает диалог при ненахождении фото агента в локальной папке
        /// </summary>
        public static async void ShowErrorFileExicting()
        {
            var fileExictingDialog = new ContentDialog()
            {
                Title = "Фотография отсутствует",
                Content = "Возможно файл был удален.",
                CloseButtonText = "Ок"
            };

            await fileExictingDialog.ShowAsync();
        }
    }
}