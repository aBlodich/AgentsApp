using AgentsApp.Commands;
using AgentsApp.DataBase;
using AgentsApp.Models;
using AgentsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace AgentsApp.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        public Agent Agent { get; set; }
        private List<Agent> agents;
        public ObservableCollection<MainPageViewModel> AgentsCollection { get; set; }
        public ObservableCollection<MainPageViewModel> InfoCollection { get; set; } = new ObservableCollection<MainPageViewModel>();

        private MainPageViewModel selectedAgent = null;
        public MainPageViewModel SelectedAgent
        {
            get => selectedAgent;
            set
            { 
                selectedAgent = value;
                if (selectedAgent != null && !InfoCollection.Contains(selectedAgent))
                {
                    GetPhoto();
                    InfoCollection.Clear();
                    InfoCollection.Add(selectedAgent);
                }
            }
        }

        private BitmapImage _photo;

        public BitmapImage Photo
        {
            get => _photo;

            set
            {
                _photo = value;
                OnPropertyChanged();
            }
        }

        private async void  GetPhoto()
        {
            StorageFile file = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(SelectedAgent.ImageToken);
            var fileStream = await file.OpenAsync(FileAccessMode.Read);
            var bmi = new BitmapImage();
            SelectedAgent.Photo = bmi;
            await bmi.SetSourceAsync(fileStream);
            fileStream.Dispose();
        }

        public ICommand GoToAddPage
        {
            get => new DelegateCommand(NavigateToAddPage);
        }

        public ICommand GoToEditPage
        {
            get => new DelegateCommand(NavigateToEditPage);
        }

        public void NavigateToAddPage()
        {
            Navigation.Navigate(typeof(AddOrEditAgentPage), null, new DrillInNavigationTransitionInfo());
        }


        public void OnAgentsListItem_Clicked()
        {
            if (SelectedAgent == null) return;
            //SelectedAgent = SelectedAgent.Agent;
            //InfoCollection = new ObservableCollection<MainPageViewModel>();
            //InfoCollection.Add(new MainPageViewModel(info));

        }

        public void NavigateToEditPage()
        {
            int agentId = ((MainPageViewModel)SelectedAgent).Agent.Id;
            Navigation.Navigate(typeof(AddOrEditAgentPage), agentId, new DrillInNavigationTransitionInfo());
        }

        public MainPageViewModel()
        {
            using (var db = new AgentContext())
            {
                agents = db.Agents.ToList();
            }
            agents.Sort();

            AgentsCollection = new ObservableCollection<MainPageViewModel>(agents.Select(c => new MainPageViewModel(c)));

        }

        public MainPageViewModel(Agent agent)
        {
            this.Agent = agent;
        }

        public string Name
        {
            get
            {
                return Agent.Name;
            }
            set
            {
                Agent.Name = value;
                OnPropertyChanged();

            }
        }

        public string ContactNumber
        {
            get
            {
                return Agent.ContactNumber;
            }
            set
            {
                Agent.ContactNumber = value;
                OnPropertyChanged();
            }
        }

        public string ImageToken
        {
            get => Agent.ImageToken;
            set => Agent.ImageToken = value;
        }

        public string Email
        {
            get
            {
                return Agent.Email;
            }
            set
            {
                Agent.Email = value;
                OnPropertyChanged();
            }
        }

        public string ImagePath
        {
            get
            {
                return Agent.ImagePath;
            }
            set
            {
                Agent.ImagePath = value;
                OnPropertyChanged();
            }
        }
    }
}
