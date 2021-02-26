using AgentsApp.Database;
using AgentsApp.Models;
using AgentsApp.ViewModels;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AgentsApp.Views
{
    public sealed partial class AddOrEditAgentPage : Page
    {
        AddOrEditAgentViewModel vm;

        public AddOrEditAgentPage()
        {
            this.InitializeComponent();
            vm = new AddOrEditAgentViewModel();
            DataContext = vm;
        }

        /// <summary>
        ///Настраивает страницу в зависимости от того, какой параметр был передан при переходе на нее.
        ///Если параметра нет - добавление агента.
        ///Если параметр есть (Id агента) - редактирование агента.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                int agentId = (int)e.Parameter;
                Agent agent = null;
                using (var db = new AgentContext())
                {
                    agent = db.Agents.FirstOrDefault(c => c.Id == agentId);
                }
                if (agent != null)
                {
                    this.Title.Text = "Редактирование агента";
                    vm.IsEdit = true;
                    vm.Agent = agent;
                    vm.NameTextBoxText = agent.Name;
                    vm.ContactNumberTextBoxText = agent.ContactNumber;
                    vm.EmailTextBoxText = agent.Email;
                }
                else this.Title.Text = "Добавление агента";
            }
            else
                this.Title.Text = "Добавление агента";
        }
    }
}