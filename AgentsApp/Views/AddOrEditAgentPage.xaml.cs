using AgentsApp.Database;
using AgentsApp.Models;
using AgentsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        //Настраивает страницу в зависимости от того, какой параметр был передан при переходе на нее
        //Если параметра нет - добавление агента
        //Если параметр есть (Id агента) - редактирование агента
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                int agentId = (int)e.Parameter;
                AgentModel agent = null;
                using (var db = new AgentContext())
                {
                    agent = db.Agents.FirstOrDefault(c => c.Id == agentId);
                }
                if (agent != null)
                {
                    this.Title.Text = "Редактирование агента";
                    this.NameTextBox.Text = agent.Name;
                    this.ContactNumberTextBox.Text = agent.ContactNumber;
                    this.EmailTextBox.Text = agent.Email;
                    vm.IsEdit = true;
                    vm.Agent = agent;
                }
            }
            else
                this.Title.Text = "Добавление агента";
        }
    }
}
