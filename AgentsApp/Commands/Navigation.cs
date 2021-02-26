using AgentsApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace AgentsApp.Commands
{
    /// <summary>
    /// Сервис навигации по страницам
    /// </summary>
    class Navigation
    {
        public static void Navigate(Type page, object param, NavigationTransitionInfo info)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(page, param, info);
        }

        public static void GoToMainPage()
        {
            var frame = (Frame)Window.Current.Content;
            if (frame.CanGoBack)
                frame.GoBack();
            else
                frame.Navigate(typeof(MainPage));
        }

        private Navigation() { }

        private static readonly Lazy<Navigation> instance =
            new Lazy<Navigation>(() => new Navigation());

        public static Navigation Instance => instance.Value;
    }
}
