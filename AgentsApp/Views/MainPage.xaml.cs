using AgentsApp.ViewModels;
using Windows.UI.Xaml.Controls;


namespace AgentsApp.Views
{
    public sealed partial class MainPage : Page
    {

        MainPageViewModel vm;

        public MainPage()
        {
            this.InitializeComponent();

            vm = new MainPageViewModel();
            DataContext = vm;
        }
    }
}
