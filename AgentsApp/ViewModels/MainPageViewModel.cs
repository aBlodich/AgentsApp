using AgentsApp.Commands;
using AgentsApp.Constants;
using AgentsApp.Database;
using AgentsApp.Models;
using AgentsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace AgentsApp.ViewModels
{
    /// <summary>
    /// Модель представения главной страницы
    /// </summary>
    class MainPageViewModel : BaseViewModel
    {
        private AgentModel Agent { get; set; }

        private List<AgentModel> agents;
        
        public ObservableCollection<MainPageViewModel> AgentsCollection { get; set; }
        
        public ObservableCollection<MainPageViewModel> InfoCollection { get; set; } = new ObservableCollection<MainPageViewModel>();

        
        private MainPageViewModel _selectedAgent = null;
        public MainPageViewModel SelectedAgent
        {
            get => _selectedAgent;
            set
            { 
                _selectedAgent = value;
                if (_selectedAgent != null && !InfoCollection.Contains(_selectedAgent))
                {
                    GetPhoto();
                    InfoCollection.Clear();
                    InfoCollection.Add(_selectedAgent);
                }
            }
        }

        private async void GetPhoto()
        {
            BitmapImage bmi = new BitmapImage();
            SelectedAgent.Photo = bmi;
            if (SelectedAgent.ImageToken == null)
            {
                bmi.UriSource = new Uri(StringConstants.PLACEHOLDERPATH);
                bmi = new BitmapImage(new Uri(StringConstants.PLACEHOLDERPATH));
                return;
            }
            StorageFile file = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(SelectedAgent.ImageToken);
            var fileStream = await file.OpenAsync(FileAccessMode.Read);
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

        public ICommand DeleteAgent
        {
            get => new DelegateCommand(OnDeleteAgent);
        }

        private void OnDeleteAgent()
        {
            var agent = SelectedAgent;
            if (agent == null)
                return;
            InfoCollection.Clear();
            Photo = null;
            using (var db = new AgentContext())
            {
                db.Remove(agent.Agent);
                AgentsCollection.Remove(AgentsCollection.Where(c => c.Agent.Id == agent.Agent.Id).Single());
                db.SaveChanges();
            }
        }
        public void NavigateToAddPage()
        {
            Navigation.Navigate(typeof(AddOrEditAgentPage), null, new DrillInNavigationTransitionInfo());
        }

        public void NavigateToEditPage()
        {
            int agentId = SelectedAgent.Agent.Id;
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

        public MainPageViewModel(AgentModel agent)
        {
            this.Agent = agent;
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

        public string Name
        {
            get => Agent.Name;
            set
            {
                Agent.Name = value;
                OnPropertyChanged();

            }
        }

        public string ContactNumber
        {
            get => Agent.ContactNumber;
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