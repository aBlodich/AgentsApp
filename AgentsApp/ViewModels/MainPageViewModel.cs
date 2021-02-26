using AgentsApp.Commands;
using AgentsApp.Database;
using AgentsApp.Models;
using AgentsApp.Services;
using AgentsApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace AgentsApp.ViewModels
{
    /// <summary>
    /// Модель представения главной страницы
    /// </summary>
    class MainPageViewModel : BaseViewModel
    {
        private string _name;
        private string _contactNumber;
        private string _email;
        private string _imageToken;
        private MainPageViewModel _selectedAgent = null;
        private AgentModel _agent { get; set; }
        private List<AgentModel> _agents;
        
        public ObservableCollection<MainPageViewModel> AgentsCollection { get; private set; }
        
        public ObservableCollection<MainPageViewModel> InfoCollection { get; private set; } = new ObservableCollection<MainPageViewModel>();
        
        public NotifyTaskCompletion<BitmapImage> Photo { get; private set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();

            }
        }

        public string ContactNumber
        {
            get => _contactNumber;

            set
            {
                _contactNumber = value;
                OnPropertyChanged();
            }
        }

        public string ImageToken
        {
            get => _imageToken;
            set => _imageToken = value;
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel SelectedAgent
        {
            get => _selectedAgent;
            set
            {
                _selectedAgent = value;
                if (_selectedAgent != null && !InfoCollection.Contains(_selectedAgent))
                {
                    _selectedAgent.Photo = new NotifyTaskCompletion<BitmapImage>(FileService.GetPhoto(_selectedAgent.ImageToken));
                    InfoCollection.Clear();
                    InfoCollection.Add(_selectedAgent);
                }
            }
        }

        /// <summary>
        /// Переход к странице добалвения агента
        /// </summary>
        public ICommand GoToAddPage
        {
            get => new DelegateCommand(NavigateToAddPage);
        }

        /// <summary>
        /// Переход к странице редактирования агента
        /// </summary>
        public ICommand GoToEditPage
        {
            get => new DelegateCommand(NavigateToEditPage);
        }

        /// <summary>
        /// Удаление агента
        /// </summary>
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
                db.Agents.Remove(agent._agent.ToAgent());
                AgentsCollection.Remove(AgentsCollection.Where(c => c._agent.Id == agent._agent.Id).Single());
                db.SaveChanges();
            }
        }

        public void NavigateToAddPage()
        {
            Navigation.Navigate(typeof(AddOrEditAgentPage), null, new DrillInNavigationTransitionInfo());
        }

        public void NavigateToEditPage()
        {
            int agentId = SelectedAgent._agent.Id;
            Navigation.Navigate(typeof(AddOrEditAgentPage), agentId, new DrillInNavigationTransitionInfo());
        }

        public MainPageViewModel()
        {
            LoadData();
        }

        public MainPageViewModel(AgentModel agentModel)
        {
            this.Name = agentModel.Name;
            this.ContactNumber = agentModel.ContactNumber;
            this.Email = agentModel.Email;
            this.ImageToken = agentModel.ImageToken;
            this._agent = agentModel;
        }

        /// <summary>
        /// Загрузка данных из базы данных
        /// </summary>
        private void LoadData()
        {
            _agent = AgentModel.LoadDataFromDataBase();
            _agents = _agent.AgentModels;
            AgentsCollection = new ObservableCollection<MainPageViewModel>(_agents.Select(c => new MainPageViewModel(c)));
        }
    }
}